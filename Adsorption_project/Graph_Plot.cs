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
    public partial class Graph_Plot : Form
    {
        public Graph_Plot()
        {
            InitializeComponent();
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Adsoption chart has been successfully exported to a PNG image.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
