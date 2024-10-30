
namespace Adsorption_project
{
    partial class Run_Simulation_page
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Run_Simulation_page));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AdsorptionStep = new System.Windows.Forms.TabPage();
            this.DesorptionStep = new System.Windows.Forms.TabPage();
            this.ColumnSizing = new System.Windows.Forms.TabPage();
            this.ProcessPerformance = new System.Windows.Forms.TabPage();
            this.EnergyCon = new System.Windows.Forms.TabPage();
            this.MassBalance = new System.Windows.Forms.TabPage();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPlot = new System.Windows.Forms.Button();
            this.lblMode_Result = new System.Windows.Forms.Label();
            this.txtMode_Result = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.AdsorptionStep.SuspendLayout();
            this.DesorptionStep.SuspendLayout();
            this.ColumnSizing.SuspendLayout();
            this.ProcessPerformance.SuspendLayout();
            this.EnergyCon.SuspendLayout();
            this.MassBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AdsorptionStep);
            this.tabControl1.Controls.Add(this.DesorptionStep);
            this.tabControl1.Controls.Add(this.ColumnSizing);
            this.tabControl1.Controls.Add(this.ProcessPerformance);
            this.tabControl1.Controls.Add(this.EnergyCon);
            this.tabControl1.Controls.Add(this.MassBalance);
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(822, 370);
            this.tabControl1.TabIndex = 0;
            // 
            // AdsorptionStep
            // 
            this.AdsorptionStep.Controls.Add(this.pictureBox5);
            this.AdsorptionStep.Location = new System.Drawing.Point(4, 25);
            this.AdsorptionStep.Name = "AdsorptionStep";
            this.AdsorptionStep.Size = new System.Drawing.Size(814, 341);
            this.AdsorptionStep.TabIndex = 5;
            this.AdsorptionStep.Text = "Adsorption step";
            this.AdsorptionStep.UseVisualStyleBackColor = true;
            // 
            // DesorptionStep
            // 
            this.DesorptionStep.Controls.Add(this.pictureBox1);
            this.DesorptionStep.Location = new System.Drawing.Point(4, 25);
            this.DesorptionStep.Name = "DesorptionStep";
            this.DesorptionStep.Padding = new System.Windows.Forms.Padding(3);
            this.DesorptionStep.Size = new System.Drawing.Size(814, 341);
            this.DesorptionStep.TabIndex = 1;
            this.DesorptionStep.Text = "Desorption step";
            this.DesorptionStep.UseVisualStyleBackColor = true;
            // 
            // ColumnSizing
            // 
            this.ColumnSizing.Controls.Add(this.pictureBox2);
            this.ColumnSizing.Location = new System.Drawing.Point(4, 25);
            this.ColumnSizing.Name = "ColumnSizing";
            this.ColumnSizing.Size = new System.Drawing.Size(814, 341);
            this.ColumnSizing.TabIndex = 2;
            this.ColumnSizing.Text = "Column sizing";
            this.ColumnSizing.UseVisualStyleBackColor = true;
            // 
            // ProcessPerformance
            // 
            this.ProcessPerformance.Controls.Add(this.pictureBox3);
            this.ProcessPerformance.Location = new System.Drawing.Point(4, 25);
            this.ProcessPerformance.Name = "ProcessPerformance";
            this.ProcessPerformance.Size = new System.Drawing.Size(814, 341);
            this.ProcessPerformance.TabIndex = 3;
            this.ProcessPerformance.Text = "Process performance";
            this.ProcessPerformance.UseVisualStyleBackColor = true;
            // 
            // EnergyCon
            // 
            this.EnergyCon.Controls.Add(this.pictureBox4);
            this.EnergyCon.Location = new System.Drawing.Point(4, 25);
            this.EnergyCon.Name = "EnergyCon";
            this.EnergyCon.Size = new System.Drawing.Size(814, 341);
            this.EnergyCon.TabIndex = 4;
            this.EnergyCon.Text = "Energy consumption";
            this.EnergyCon.UseVisualStyleBackColor = true;
            // 
            // MassBalance
            // 
            this.MassBalance.Controls.Add(this.pictureBox6);
            this.MassBalance.Location = new System.Drawing.Point(4, 25);
            this.MassBalance.Name = "MassBalance";
            this.MassBalance.Padding = new System.Windows.Forms.Padding(3);
            this.MassBalance.Size = new System.Drawing.Size(814, 341);
            this.MassBalance.TabIndex = 0;
            this.MassBalance.Text = "Overall mass balance";
            this.MassBalance.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(621, 443);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(209, 41);
            this.btnExport.TabIndex = 115;
            this.btnExport.Text = "Export Excel report";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPlot
            // 
            this.btnPlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlot.Location = new System.Drawing.Point(418, 443);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(186, 41);
            this.btnPlot.TabIndex = 116;
            this.btnPlot.Text = "Graph plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // lblMode_Result
            // 
            this.lblMode_Result.AutoSize = true;
            this.lblMode_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode_Result.Location = new System.Drawing.Point(20, 18);
            this.lblMode_Result.Name = "lblMode_Result";
            this.lblMode_Result.Size = new System.Drawing.Size(173, 24);
            this.lblMode_Result.TabIndex = 117;
            this.lblMode_Result.Text = "Operating Mode :";
            // 
            // txtMode_Result
            // 
            this.txtMode_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMode_Result.Location = new System.Drawing.Point(208, 16);
            this.txtMode_Result.Name = "txtMode_Result";
            this.txtMode_Result.Size = new System.Drawing.Size(170, 30);
            this.txtMode_Result.TabIndex = 118;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(30, 33);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(748, 235);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 113;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(748, 235);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 114;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(33, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(748, 235);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 115;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(33, 53);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(748, 235);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 116;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(33, 53);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(748, 235);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 116;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(33, 53);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(748, 235);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 116;
            this.pictureBox6.TabStop = false;
            // 
            // Run_Simulation_page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 496);
            this.Controls.Add(this.txtMode_Result);
            this.Controls.Add(this.lblMode_Result);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.tabControl1);
            this.Name = "Run_Simulation_page";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caluclation Results";
            this.tabControl1.ResumeLayout(false);
            this.AdsorptionStep.ResumeLayout(false);
            this.DesorptionStep.ResumeLayout(false);
            this.ColumnSizing.ResumeLayout(false);
            this.ProcessPerformance.ResumeLayout(false);
            this.EnergyCon.ResumeLayout(false);
            this.MassBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MassBalance;
        private System.Windows.Forms.TabPage DesorptionStep;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.TabPage ColumnSizing;
        private System.Windows.Forms.TabPage ProcessPerformance;
        private System.Windows.Forms.TabPage EnergyCon;
        private System.Windows.Forms.TabPage AdsorptionStep;
        private System.Windows.Forms.Label lblMode_Result;
        private System.Windows.Forms.TextBox txtMode_Result;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}