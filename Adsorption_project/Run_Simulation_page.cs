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
    public partial class Run_Simulation_page : Form
    {
        public Run_Simulation_page()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have successfully exported your results to an excel file", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            Graph_Plot page = new Graph_Plot();
            page.Show();
        }
    }
}
