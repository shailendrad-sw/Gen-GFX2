namespace graphicserver
{
    partial class mainform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
            this.mainpanel = new System.Windows.Forms.Panel();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HeadTabPanel = new System.Windows.Forms.Panel();
            this.MainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainpanel
            // 
            this.mainpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainpanel.Location = new System.Drawing.Point(0, 40);
            this.mainpanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(950, 650);
            this.mainpanel.TabIndex = 4;
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 1;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.mainpanel, 0, 1);
            this.MainTableLayoutPanel.Controls.Add(this.HeadTabPanel, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(950, 690);
            this.MainTableLayoutPanel.TabIndex = 6;
            // 
            // HeadTabPanel
            // 
            this.HeadTabPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.HeadTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeadTabPanel.Location = new System.Drawing.Point(3, 3);
            this.HeadTabPanel.Name = "HeadTabPanel";
            this.HeadTabPanel.Size = new System.Drawing.Size(944, 34);
            this.HeadTabPanel.TabIndex = 5;
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(950, 690);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Azure;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(950, 690);
            this.Name = "mainform";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GFX2.X";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainform_FormClosing);
            this.Load += new System.EventHandler(this.mainform_Load);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Panel HeadTabPanel;
        private System.Windows.Forms.Panel mainpanel;
    }
}