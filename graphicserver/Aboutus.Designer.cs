namespace graphicserver
{
    partial class Aboutus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aboutus));
            this.Mainpicturebox = new System.Windows.Forms.PictureBox();
            this.Mainaboutpanel = new System.Windows.Forms.Panel();
            this.Companylbl = new System.Windows.Forms.Label();
            this.Rightslbl = new System.Windows.Forms.Label();
            this.AboutusCloseButton = new System.Windows.Forms.Button();
            this.ProductVersionnumber = new System.Windows.Forms.Label();
            this.Applicationname = new System.Windows.Forms.Label();
            this.lblDongleExpDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Mainpicturebox)).BeginInit();
            this.Mainaboutpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Mainpicturebox
            // 
            this.Mainpicturebox.Image = ((System.Drawing.Image)(resources.GetObject("Mainpicturebox.Image")));
            this.Mainpicturebox.InitialImage = null;
            this.Mainpicturebox.Location = new System.Drawing.Point(11, 49);
            this.Mainpicturebox.Name = "Mainpicturebox";
            this.Mainpicturebox.Size = new System.Drawing.Size(187, 85);
            this.Mainpicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Mainpicturebox.TabIndex = 0;
            this.Mainpicturebox.TabStop = false;
            // 
            // Mainaboutpanel
            // 
            this.Mainaboutpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Mainaboutpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Mainaboutpanel.Controls.Add(this.lblDongleExpDate);
            this.Mainaboutpanel.Controls.Add(this.Companylbl);
            this.Mainaboutpanel.Controls.Add(this.Rightslbl);
            this.Mainaboutpanel.Controls.Add(this.AboutusCloseButton);
            this.Mainaboutpanel.Controls.Add(this.ProductVersionnumber);
            this.Mainaboutpanel.Controls.Add(this.Applicationname);
            this.Mainaboutpanel.Controls.Add(this.Mainpicturebox);
            this.Mainaboutpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mainaboutpanel.Location = new System.Drawing.Point(0, 0);
            this.Mainaboutpanel.Name = "Mainaboutpanel";
            this.Mainaboutpanel.Size = new System.Drawing.Size(496, 189);
            this.Mainaboutpanel.TabIndex = 1;
            this.Mainaboutpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Mainaboutpanel_Paint);
            // 
            // Companylbl
            // 
            this.Companylbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Companylbl.ForeColor = System.Drawing.Color.Azure;
            this.Companylbl.Location = new System.Drawing.Point(220, 98);
            this.Companylbl.Name = "Companylbl";
            this.Companylbl.Size = new System.Drawing.Size(255, 24);
            this.Companylbl.TabIndex = 5;
            this.Companylbl.Text = "Planetcast Media Services Limited";
            // 
            // Rightslbl
            // 
            this.Rightslbl.AutoSize = true;
            this.Rightslbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rightslbl.ForeColor = System.Drawing.Color.Azure;
            this.Rightslbl.Location = new System.Drawing.Point(221, 128);
            this.Rightslbl.Name = "Rightslbl";
            this.Rightslbl.Size = new System.Drawing.Size(125, 17);
            this.Rightslbl.TabIndex = 7;
            this.Rightslbl.Text = "All Rights Reserved";
            this.Rightslbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutusCloseButton
            // 
            this.AboutusCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutusCloseButton.BackColor = System.Drawing.Color.Azure;
            this.AboutusCloseButton.FlatAppearance.BorderSize = 0;
            this.AboutusCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.AboutusCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AboutusCloseButton.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.AboutusCloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.AboutusCloseButton.Location = new System.Drawing.Point(468, 0);
            this.AboutusCloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.AboutusCloseButton.Name = "AboutusCloseButton";
            this.AboutusCloseButton.Size = new System.Drawing.Size(28, 25);
            this.AboutusCloseButton.TabIndex = 3;
            this.AboutusCloseButton.Text = "r";
            this.AboutusCloseButton.UseVisualStyleBackColor = false;
            this.AboutusCloseButton.Click += new System.EventHandler(this.AboutusCloseButton_Click);
            // 
            // ProductVersionnumber
            // 
            this.ProductVersionnumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProductVersionnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductVersionnumber.ForeColor = System.Drawing.Color.White;
            this.ProductVersionnumber.Location = new System.Drawing.Point(224, 68);
            this.ProductVersionnumber.Name = "ProductVersionnumber";
            this.ProductVersionnumber.Size = new System.Drawing.Size(147, 28);
            this.ProductVersionnumber.TabIndex = 2;
            this.ProductVersionnumber.Text = "Version Number:";
            // 
            // Applicationname
            // 
            this.Applicationname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Applicationname.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Applicationname.ForeColor = System.Drawing.Color.White;
            this.Applicationname.Location = new System.Drawing.Point(219, 40);
            this.Applicationname.Name = "Applicationname";
            this.Applicationname.Size = new System.Drawing.Size(231, 24);
            this.Applicationname.TabIndex = 1;
            this.Applicationname.Text = "GFX2.X";
            // 
            // lblDongleExpDate
            // 
            this.lblDongleExpDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDongleExpDate.ForeColor = System.Drawing.Color.Azure;
            this.lblDongleExpDate.Location = new System.Drawing.Point(221, 156);
            this.lblDongleExpDate.Name = "lblDongleExpDate";
            this.lblDongleExpDate.Size = new System.Drawing.Size(255, 24);
            this.lblDongleExpDate.TabIndex = 8;
            this.lblDongleExpDate.Text = "License Expiry Date ";
            // 
            // Aboutus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(496, 189);
            this.Controls.Add(this.Mainaboutpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Aboutus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aboutus";
            this.Load += new System.EventHandler(this.Aboutus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Mainpicturebox)).EndInit();
            this.Mainaboutpanel.ResumeLayout(false);
            this.Mainaboutpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Mainpicturebox;
        private System.Windows.Forms.Panel Mainaboutpanel;
        private System.Windows.Forms.Label ProductVersionnumber;
        private System.Windows.Forms.Label Applicationname;
        private System.Windows.Forms.Button AboutusCloseButton;
        private System.Windows.Forms.Label Companylbl;
        private System.Windows.Forms.Label Rightslbl;
        private System.Windows.Forms.Label lblDongleExpDate;
    }
}