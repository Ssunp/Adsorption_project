using Adsorption_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Adsorption_project
{
    public partial class Isotherm_Data_page : Form
    {

        private AbsorptionColumn absorptionColumn;

        public Isotherm_Data_page()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(columnResults);

            absorptionColumn = new AbsorptionColumn();
            BindControls();
        }

        private void BindControls()
        {
            // Bind txtFlowRate to FlowRate
            txtFlowRate.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.FlowRate), true, DataSourceUpdateMode.OnPropertyChanged);
            txtPressure.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Pressure), true, DataSourceUpdateMode.OnPropertyChanged);
            txtTemperature.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Temperature), true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind to yFi[0]
            txtYf1.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.YF1), true, DataSourceUpdateMode.OnPropertyChanged);
            txtYf2.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.YF2), true, DataSourceUpdateMode.OnPropertyChanged);
            txtYf3.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.YF3), true, DataSourceUpdateMode.OnPropertyChanged);
            txtYf4.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.YF4), true, DataSourceUpdateMode.OnPropertyChanged);
            //// Bind to qs[0]
            txtQs1.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Qs1), true, DataSourceUpdateMode.OnPropertyChanged);
            txtQs2.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Qs2), true, DataSourceUpdateMode.OnPropertyChanged);
            txtQs3.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Qs3), true, DataSourceUpdateMode.OnPropertyChanged);
            txtQs4.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.Qs4), true, DataSourceUpdateMode.OnPropertyChanged);
            //// Bind to b0[0]
            txtB01.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.B01), true, DataSourceUpdateMode.OnPropertyChanged);
            txtB02.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.B02), true, DataSourceUpdateMode.OnPropertyChanged);
            txtB03.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.B03), true, DataSourceUpdateMode.OnPropertyChanged);
            txtB04.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.B04), true, DataSourceUpdateMode.OnPropertyChanged);
            //// Bind to YFi[0]
            txtDU1.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.DU1), true, DataSourceUpdateMode.OnPropertyChanged);
            txtDU2.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.DU2), true, DataSourceUpdateMode.OnPropertyChanged);
            txtDU3.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.DU3), true, DataSourceUpdateMode.OnPropertyChanged);
            txtDU4.DataBindings.Add("Text", absorptionColumn, nameof(AbsorptionColumn.DU4), true, DataSourceUpdateMode.OnPropertyChanged);

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void DefineParameter_Click(object sender, EventArgs e)
        {

        }

        private void gbEq_data_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDone_OP_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }


        private void btnBack_parameter_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnEs1_Click(object sender, EventArgs e)
        {
            Isothem_Estimation page = new Isothem_Estimation();
            page.Show();
        }

        private void btnEs2_Click(object sender, EventArgs e)
        {
            Isothem_Estimation page = new Isothem_Estimation();
            page.Show();
        }

        private void btnEs3_Click(object sender, EventArgs e)
        {
            Isothem_Estimation page = new Isothem_Estimation();
            page.Show();
        }

        private void btnEs4_Click(object sender, EventArgs e)
        {
            Isothem_Estimation page = new Isothem_Estimation();
            page.Show();
        }

        private void btnClear_parameter_Click(object sender, EventArgs e)
        {
            
        }

        private void Isotherm_Data_page_Load(object sender, EventArgs e)
        {
            cbbFlowrate.Text = "mol/hr";
            cbbPressure_Feed.Text = "bar";
            cbbTemp_Feed.Text = "K";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //tabControl1
            //> columnResults
            if (!tabControl1.TabPages.Contains(columnResults))
            {
                tabControl1.TabPages.Add(columnResults);
            }

            dataGridView1.DataSource = absorptionColumn.Calculate();
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AllowUserToAddRows = false;    // Prevents the empty row at the bottom

            PlotCustomData(absorptionColumn.PlotDataList);

            tabControl1.SelectedTab = columnResults;

        }

        private void PlotCustomData(List<PlotData> plotDataList)
        {
            // Set up the chart area
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea
            {
                BorderColor = System.Drawing.Color.Black,
                BorderWidth = 1,
                BorderDashStyle = ChartDashStyle.Solid
            };
            chart1.ChartAreas.Add(chartArea);

            // Clear existing series
            chart1.Series.Clear();

            // Loop through each PlotData object and create a series for it
            foreach (var plotData in plotDataList)
            {
                Series series = new Series(plotData.Label)
                {
                    ChartType = SeriesChartType.Line,
                    Color = System.Drawing.ColorTranslator.FromHtml(plotData.LineColor),
                    BorderWidth = 2,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6,
                    MarkerColor = System.Drawing.ColorTranslator.FromHtml(plotData.LineColor)
                };

                // Add points from plotData.X and plotData.Y
                for (int i = 0; i < plotData.X.Length; i++)
                {
                    series.Points.AddXY(plotData.X[i], plotData.Y[i]);
                }

                // Add series to the chart
                chart1.Series.Add(series);
            }

            // Set chart title and labels
            chart1.Titles.Clear();
            chart1.Titles.Add("Adsorption Isotherm");
            chartArea.AxisX.Title = "Pressure (bar)";
            chartArea.AxisY.Title = "Adsorption loading, (mol/kg)";
        }
    }
}
