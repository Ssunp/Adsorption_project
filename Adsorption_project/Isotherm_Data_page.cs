using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adsorption_project
{
    public partial class Isotherm_Data_page : Form
    {
        public Isotherm_Data_page()
        {
            InitializeComponent();
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

        private void btnNextPara_Click(object sender, EventArgs e)
        {
            this.Close();
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
            cbbTemp_Feed.Text = "C";
        }
    }
}
