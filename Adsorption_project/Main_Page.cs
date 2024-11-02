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

namespace Adsorption_project
{
    public partial class Main_Page : Form
    {
        public Main_Page()
        {
            InitializeComponent();
            var absorber = new Utils();
            //aa.Test
        }

        private void Main_Page_Load(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            Isotherm_Data_page page = new Isotherm_Data_page();
            page.Show();
        }

        private void btnAdsorber_Click(object sender, EventArgs e)
        {
            Adsopber_Configulation_page page = new Adsopber_Configulation_page();
            page.Show();
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            Project_Creation_page page = new Project_Creation_page();
            page.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Run_Simulation_page page = new Run_Simulation_page();
            page.Show();
        }

        private void btnCase_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This page is currently under development and will be available in future updates.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This page is currently under development and will be available in future updates.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
