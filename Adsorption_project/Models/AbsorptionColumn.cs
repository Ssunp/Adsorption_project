using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Adsorption_project.Models
{
    public class PlotData
    {
        public string Label { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public string LineColor { get; set; }
    }

    public class AbsorptionColumn : INotifyPropertyChanged
    {
        private double flowRate;
        private double temperature;
        private double pressure;
        private double[] yF;
        private double[] b0;
        private double[] dU;
        private double[] qs;


        // Constructor
        public AbsorptionColumn()
        {
            InitializeParams();
        }

        public DataTable Calculate()
        {
            double F_T = flowRate;
            double T_f = temperature;
            double P_f = Pressure;
            double[] y_fi = yF;
            double[] q_i_s = qs;
            double[] b_i_0 = b0;
            double[] delta_U_i = dU;

            //// Input from the image
            //double F_T = 56055 * 1000 / 29;  // Total feed flow rate, mol/hr
            //double[] y_fi = { 0.00039, 0.78961, 0.2100, 0.0000 };  // Composition of species in the feed
            //double P_f = 1;  // Feed pressure (1 bar)
            //double T_f = 298.15;  // Feed temperature, K (25 °C)

            //// Isotherm parameters from the image
            //double[] q_i_s = { 5.63, 5.84, 0, 0 }; // mol/kg
            //double[] b_i_0 = { 8.65e-7, 2.5e-6, 0, 0 }; // m^3/mol
            //double[] delta_U_i = { -36641, -15800, 0, 0 }; // J/mol

            // Constants
            double R = 8.314;  // Universal gas constant, J/(mol·K)
            double pi = 3.14;

            // Operating conditions
            double P_ads = 1; // Adsorption pressure(1 bar)
            double T_ads = 298.15;  // Adsorption temperature, K (25 °C)
            double P_des = 1;  // Desorption pressure (1 bar)
            double T_des = 313.15;  // Desorption temperature, K (40 °C)
            double P_drop = 0.001; // bar/m

            string operating_mode = "TSA"; //Fix later

            double P_gas = 1;
            double T_gas = 298.15;

            // Design related inputs
            double rho_s = 1535;  // Density of adsorbent, kg/m^3
            double D_o_limit = 1.0;  // Column diameter upper limit, m (1 meter)
            double L_D_ratio = 5.0;  // Column L/D ratio (length/diameter)
            double epsilon = 0.35;  // Bed voidage

            //Cycle
            double cycle_ads = 0.5; //hr
            double cycle_des = 0.5; //hr

            //Other
            double Cp_per_Cv = 1.4;
            double efficiency = 0.75;
            double s_heat_capacity = 0.92; //adsorbent heat capacity J/g/K
            double g_heat_capacity = 1; //gas heat capacity J/mol/K

            // Step 1: Calculate concentration of each species at adsorption and desorption conditions
            double[] c_i_ads = y_fi.Select(y => (y * P_ads * 1e5) / (R * T_ads)).ToArray();
            double[] c_i_des = y_fi.Select(y => (y * P_des * 1e5) / (R * T_des)).ToArray();


            // Step 2: Calculate b_i under adsorption and desorption conditions
            double[] b_i_ads = b_i_0.Select((b, i) => b * Math.Exp(-delta_U_i[i] / (R * T_ads))).ToArray();
            double[] b_i_des = b_i_0.Select((b, i) => b * Math.Exp(-delta_U_i[i] / (R * T_des))).ToArray();


            // Step 3: Calculate the amount of gas adsorbed during adsorption
            double q_i_ads_denominator = 1 + b_i_ads.Zip(c_i_ads, (a, b) => a * b).Sum();
            double[] q_i_ads = q_i_s.Zip(b_i_ads, (a, b) => a * b).Zip(c_i_ads, (a, b) => a * b / q_i_ads_denominator).ToArray();



            // Step 4: Calculate the specific volumetric amount of gas adsorbed/retained
            double[] n_i_ads = c_i_ads.Select((c, i) => epsilon * c + (1 - epsilon) * rho_s * q_i_ads[i]).ToArray();


            // Step 5: Calculate the amount of gas retained after desorption
            double q_i_des_denominator = 1 + b_i_des.Zip(c_i_des, (a, b) => a * b).Sum();
            double[] q_i_des = q_i_s.Zip(b_i_des, (a, b) => a * b).Zip(c_i_des, (a, b) => a * b / q_i_des_denominator).ToArray();
            double[] n_i_des = c_i_des.Select((c, i) => epsilon * c + (1 - epsilon) * rho_s * q_i_des[i]).ToArray();


            // Step 6: Calculate the working capacity
            double[] delta_n_i = n_i_ads.Zip(n_i_des, (ads, des) => ads - des).ToArray();


            // Step 7: Calculate flow of most-selective gas species
            double[] most_selective = new double[b_i_des.Length];
            int maxIndex = Array.IndexOf(b_i_des, b_i_des.Max());
            most_selective[maxIndex] = 1;
            double f_k = most_selective.Zip(y_fi, (ms, y) => ms * y).Sum() * F_T * cycle_ads;

            //Debug.Print($"f_k={f_k}");

            // Step 8: Calculate required total adsorbent volume
            double delta_n_k = delta_n_i[maxIndex];
            double V_T = f_k / delta_n_k;
            double Total_ads = F_T * cycle_ads;

            //Debug.Print($"f_k={delta_n_k}");
            //Debug.Print($"f_k={V_T}");
            //Debug.Print($"f_k={Total_ads}");

            // Step 9: Column sizing        
            double one_column_diameter = Math.Pow((4 * V_T) / (pi * L_D_ratio), 1.0 / 3.0);
            double column_per_train = Math.Ceiling((4 * V_T) / (pi * L_D_ratio * Math.Pow(Math.Min(one_column_diameter, D_o_limit), 3)));
            double column_diameter = one_column_diameter / Math.Pow(column_per_train, 1.0 / 3.0);
            double column_length = L_D_ratio * column_diameter;
            double adsorbent_amount = V_T * rho_s;


            // Step 10: Calculations for process performance
            double[] nd = delta_n_i.Select(dn => dn * V_T).ToArray();


            double nd_sum = nd.Sum();
            double[] yd = nd.Select(n => n / nd_sum).ToArray();

            double[] na = y_fi.Select((y, i) => Total_ads * y - nd[i]).ToArray();
            double na_sum = na.Sum();
            double[] ya = na.Select(n => n / na_sum).ToArray();

            double[] recovery_ads = y_fi.Select((y, i) => na[i] / (Total_ads * y + 1e-10)).ToArray();
            double[] recovery_des = y_fi.Select((y, i) => nd[i] / (Total_ads * y + 1e-10)).ToArray();


            // Step 11: Output part 1
            string product_withdrawal = string.Empty;
            double product_purity;
            double product_recovery;
            double product_flow_rate;

            if (b_i_ads[0] == b_i_ads.Max())
            {
                product_withdrawal = "During Desorption";
                product_purity = yd[0] * 100;
                product_recovery = recovery_des[0] * 100;
                product_flow_rate = 2 * nd.Sum() / cycle_des;
            }
            else
            {
                product_withdrawal = "During Adsorption";
                product_purity = ya[0] * 100;
                product_recovery = recovery_ads[0] * 100;
                product_flow_rate = 2 * na.Sum() / cycle_ads;
            }


            // Step 12: Calculations for energy consumption
            double feed_compressor_power = (1 / efficiency) * (Total_ads / 3600) * (Cp_per_Cv / (Cp_per_Cv - 1)) * R * T_f
            * (Math.Pow((P_ads + P_drop * column_length) / P_f, (Cp_per_Cv - 1) / Cp_per_Cv) - 1) / 1000;

            double desorption_vacuum_pump = (1 / efficiency) * (nd.Sum() / 3600 / cycle_des) * (Cp_per_Cv / (Cp_per_Cv - 1)) * R * T_des
                * (Math.Pow(1 / P_des, (Cp_per_Cv - 1) / Cp_per_Cv) - 1) / 1000;

            double product_compressor = (1 / efficiency) * (product_flow_rate / 3600) * (Cp_per_Cv / (Cp_per_Cv - 1)) * R * (273.15 + T_gas)
                * (Math.Pow(P_gas / P_des, (Cp_per_Cv - 1) / Cp_per_Cv) - 1) / 1000;

            double[] heat_desorption = delta_U_i.Zip(delta_n_i, (q, b) => q * b) // Element-wise multiplication
                                                .Select(x => -1 * x * V_T * (cycle_ads + cycle_des) / 3600) // Multiply by constant
                                                .ToArray();

            double column_heating_duty = (heat_desorption.Sum() +
                                    delta_n_i.Sum() * V_T / cycle_des / 3600 *
                                    g_heat_capacity * (T_des - T_ads)) / 1000;




            // Step 13: Overall Mass Balance
            double[] input_Train1_step1 = y_fi.Select(y => F_T * y).ToArray();
            double[] output_Train1_step1 = na.Select(n => n / cycle_ads).ToArray();
            double[] output_Train1_step2 = nd.Select(n => n / cycle_des).ToArray();

            double[] input_Train2_step2 = y_fi.Select(y => F_T * y).ToArray();
            double[] output_Train2_step1 = output_Train1_step2;
            double[] output_Train2_step2 = output_Train1_step1;


            // Step 14: Output part 2
            int no_of_columns = (int)(column_per_train * 2);
            //double column_length = ...;
            //double column_diameter = ...;
            double total_adsorbent = 2 * adsorbent_amount;
            double power = 2 * (feed_compressor_power + desorption_vacuum_pump + product_compressor);
            double heat_duty = 2 * column_heating_duty;
            double eq_work = (power + 0.75 * 0.9 * heat_duty) / (F_T * y_fi[0] / 3600);


            // Step 15: Adsorption loading
            double[] pressure = new double[] { 0, 0.01 }.Concat(Enumerable.Range(1, 20).Select(x => x * 0.05)).ToArray();

            // Calculate solid loading at different temperatures
            var solid_loading_25C = CalculateSolidLoading(25, pressure, b_i_0, delta_U_i, R, q_i_s);
            var solid_loading_100C = CalculateSolidLoading(100, pressure, b_i_0, delta_U_i, R, q_i_s);

            PlotDataList = new List<PlotData>
            {
                new PlotData
                {
                    Label = "T=25°C",
                    X = pressure,
                    Y = solid_loading_25C.ToArray(),
                    LineColor = "#0000FF" // Blue color
                },
                new PlotData
                {
                    Label = "T=100°C",
                    X = pressure,
                    Y = solid_loading_100C.ToArray(),
                    LineColor = "#FF0000" // Red color
                }
            };


            // Formatting the table
            var table = new StringBuilder();

            table.AppendLine("┌─────────────────────────────┬─────────────────────────┐");
            table.AppendLine("│ Operating Mode              │ " + operating_mode + "                 │");
            table.AppendLine("│ Product withdrawal          │ " + product_withdrawal + "            │");
            table.AppendLine("│ Product purity (%)          │ " + product_purity.ToString("F2") + "                │");
            table.AppendLine("│ Product recovery (%)        │ " + product_recovery.ToString("F2") + "               │");
            table.AppendLine("│ Product flow rate (mol/hr)  │ " + product_flow_rate.ToString("F2") + "               │");
            table.AppendLine("│                             │                         │");
            table.AppendLine("│ Column sizing               │                         │");
            table.AppendLine("│ No of columns               │ " + no_of_columns + "                     │");
            table.AppendLine("│ Column Length               │ " + column_length.ToString("F2") + " m              │");
            table.AppendLine("│ Column Diameter             │ " + column_diameter.ToString("F2") + " m            │");
            table.AppendLine("│ Total adsorbent             │ " + total_adsorbent.ToString("F2") + " kg             │");
            table.AppendLine("│ L/D ratio                   │ " + L_D_ratio.ToString("F2") + "                │");
            table.AppendLine("│                             │                         │");
            table.AppendLine("│ Utilities                   │                         │");
            table.AppendLine("│ Power                       │ " + power.ToString("F2") + " kW                 │");
            table.AppendLine("│ Heat duty                   │ " + heat_duty.ToString("F5") + " kW              │");
            table.AppendLine("│ Eq. Work                    │ " + eq_work.ToString("F2") + " kJ/mol product      │");
            table.AppendLine("└─────────────────────────────┴─────────────────────────┘");

            // Print the table to debug output
            Debug.Print(table.ToString());

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Define columns for Category and Value
            dataTable.Columns.Add("Category", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));

            // Add rows with your data
            dataTable.Rows.Add("Operating Mode", operating_mode);
            dataTable.Rows.Add("Product Withdrawal", product_withdrawal);
            dataTable.Rows.Add("Product Purity (%)", product_purity.ToString("F2"));
            dataTable.Rows.Add("Product Recovery (%)", product_recovery.ToString("F2"));
            dataTable.Rows.Add("Product Flow Rate (mol/hr)", product_flow_rate.ToString("F2"));
            dataTable.Rows.Add("", "");  // Empty row for spacing
            dataTable.Rows.Add("Column Sizing", "");
            dataTable.Rows.Add("No of Columns", no_of_columns.ToString());
            dataTable.Rows.Add("Column Length (m)", column_length.ToString("F2"));
            dataTable.Rows.Add("Column Diameter (m)", column_diameter.ToString("F2"));
            dataTable.Rows.Add("Total Adsorbent (kg)", total_adsorbent.ToString("F2"));
            dataTable.Rows.Add("L/D Ratio", L_D_ratio.ToString("F2"));
            dataTable.Rows.Add("", "");  // Empty row for spacing
            dataTable.Rows.Add("Utilities", "");
            dataTable.Rows.Add("Power (kW)", power.ToString("F2"));
            dataTable.Rows.Add("Heat Duty (kW)", heat_duty.ToString("F5"));
            dataTable.Rows.Add("Eq. Work (kJ/mol product)", eq_work.ToString("F2"));

            return dataTable;
        }

        static List<double> CalculateSolidLoading(double tempCelsius, double[] pressure, double[] b_i_0, double[] delta_U_i, double R, double[] q_i_s)
        {
            double tempKelvin = tempCelsius + 273.15;
            double b = b_i_0[0] * Math.Exp(-delta_U_i[0] / (R * tempKelvin));
            List<double> solidLoading = new List<double>();

            foreach (var p in pressure)
            {
                double c = (p * 1e5) / (R * tempKelvin); // Convert pressure from bar to pascals
                solidLoading.Add(q_i_s[0] * b * c / (1 + b * c));
            }

            return solidLoading;
        }

        private void InitializeParams()
        {
            FlowRate = 56055 * 1000.0 / 29.0;
            temperature = 298.15;
            pressure = 1.0;
            yF = new double[] { 0.00039, 0.78961, 0.2100, 0.0000 };
            b0 = new double[] { 8.65e-7, 2.5e-6, 0, 0 };
            qs = new double[] { 5.63, 5.84, 0, 0 };
            dU = new double[] { -36641, -15800, 0, 0 };
        }

        // user interface static data--------------------------------------
        public List<PlotData> PlotDataList { get; set; }


        // user interface valication data---------------------------------
        public double FlowRate
        {
            get => flowRate;
            set
            {
                if (flowRate != value)
                {
                    flowRate = value;
                    OnPropertyChanged(nameof(FlowRate));
                }
            }
        }
        public double Temperature
        {
            get => temperature;
            set
            {
                if (temperature != value)
                {
                    temperature = value;
                    OnPropertyChanged(nameof(Temperature));
                }
            }
        }

        public double Pressure
        {
            get => pressure;
            set
            {
                if (pressure != value)
                {
                    pressure = value;
                    OnPropertyChanged(nameof(Pressure));
                }
            }
        }

        public double B01
        {
            get => b0[0];
            set
            {
                if (b0[0] != value)
                {
                    b0[0] = value;
                    OnPropertyChanged(nameof(B01));
                }
            }
        }

        public double B02
        {
            get => b0[1];
            set
            {
                if (b0[1] != value)
                {
                    b0[1] = value;
                    OnPropertyChanged(nameof(B02));
                }
            }
        }

        public double B03
        {
            get => b0[2];
            set
            {
                if (b0[2] != value)
                {
                    b0[2] = value;
                    OnPropertyChanged(nameof(B03));
                }
            }
        }

        public double B04
        {
            get => b0[3];
            set
            {
                if (b0[3] != value)
                {
                    b0[3] = value;
                    OnPropertyChanged(nameof(B04));
                }
            }
        }


        // qs
        public double Qs1
        {
            get => qs[0];
            set
            {
                if (qs[0] != value)
                {
                    qs[0] = value;
                    OnPropertyChanged(nameof(Qs1));
                }
            }
        }

        public double Qs2
        {
            get => qs[1];
            set
            {
                if (qs[1] != value)
                {
                    qs[1] = value;
                    OnPropertyChanged(nameof(Qs2));
                }
            }
        }

        public double Qs3
        {
            get => qs[2];
            set
            {
                if (qs[2] != value)
                {
                    qs[2] = value;
                    OnPropertyChanged(nameof(Qs3));
                }
            }
        }

        public double Qs4
        {
            get => qs[3];
            set
            {
                if (qs[3] != value)
                {
                    qs[3] = value;
                    OnPropertyChanged(nameof(Qs4));
                }
            }
        }

        // dU
        public double DU1
        {
            get => dU[0];
            set
            {
                if (dU[0] != value)
                {
                    dU[0] = value;
                    OnPropertyChanged(nameof(DU1));
                }
            }
        }

        public double DU2
        {
            get => dU[1];
            set
            {
                if (dU[1] != value)
                {
                    dU[1] = value;
                    OnPropertyChanged(nameof(DU2));
                }
            }
        }

        public double DU3
        {
            get => dU[2];
            set
            {
                if (dU[2] != value)
                {
                    dU[2] = value;
                    OnPropertyChanged(nameof(DU3));
                }
            }
        }

        public double DU4
        {
            get => dU[3];
            set
            {
                if (dU[3] != value)
                {
                    dU[3] = value;
                    OnPropertyChanged(nameof(DU4));
                }
            }
        }


        // Mass Fraction
        public double YF1
        {
            get => yF[0];
            set
            {
                if (yF[0] != value)
                {
                    yF[0] = value;
                    OnPropertyChanged(nameof(YF1));
                }
            }
        }

        public double YF2
        {
            get => yF[1];
            set
            {
                if (yF[1] != value)
                {
                    yF[1] = value;
                    OnPropertyChanged(nameof(YF2));
                }
            }
        }

        public double YF3
        {
            get => yF[2];
            set
            {
                if (yF[2] != value)
                {
                    yF[2] = value;
                    OnPropertyChanged(nameof(YF3));
                }
            }
        }

        public double YF4
        {
            get => yF[3];
            set
            {
                if (yF[3] != value)
                {
                    yF[3] = value;
                    OnPropertyChanged(nameof(YF4));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
