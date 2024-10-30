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
    public partial class Adsopber_Configulation_page : Form
    {
        public Adsopber_Configulation_page()
        {
            InitializeComponent();
        }

        private void gbOther_Enter(object sender, EventArgs e)
        {

        }

        private void btnDone_AD_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnergyCon_Click(object sender, EventArgs e)
        {

        }

        private void Adsopber_Configulation_page_Load(object sender, EventArgs e)
        {
            cbbcycle_duration.Text = "hr";
            cbbAdsorp.Text = "hr";
            cbbDesorp.Text = "hr";
            cbbAdPressure.Text = "bar";
            cbbDePressure.Text = "bar";
            cbbAdTemp.Text = "C";
            cbbDeTemp.Text = "C";
            cbbMode.Text = "TSA";
            cbbavgCPCV.Text = "γ";
            cbbIsentropic.Text = "η";
            cbbAd_HeatCap.Text = "J/g/K";
            cbbGas_HeatCap.Text = "J/mol/K";
            btnDone.Enabled = false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            btnDone.Enabled = true;
        }
    }
}
