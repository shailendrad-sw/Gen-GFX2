namespace graphicserver
{
    partial class Render
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Render));
            this.cmbSourcetype = new System.Windows.Forms.ComboBox();
            this.VidLineComboBox = new System.Windows.Forms.ComboBox();
            this.refreshsource = new System.Windows.Forms.Button();
            this.InitDevice = new System.Windows.Forms.Button();
            this.CloseDevice = new System.Windows.Forms.Button();
            this.Headpanel = new System.Windows.Forms.Panel();
            this.BtClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtNdiHelp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outpreview = new System.Windows.Forms.Button();
            this.Heading = new System.Windows.Forms.Label();
            this.Headpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSourcetype
            // 
            this.cmbSourcetype.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cmbSourcetype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourcetype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSourcetype.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.cmbSourcetype.FormattingEnabled = true;
            this.cmbSourcetype.Items.AddRange(new object[] {
            "NDI",
            "SDI",
            "VIRTUAL"});
            this.cmbSourcetype.Location = new System.Drawing.Point(22, 82);
            this.cmbSourcetype.Name = "cmbSourcetype";
            this.cmbSourcetype.Size = new System.Drawing.Size(290, 21);
            this.cmbSourcetype.TabIndex = 0;
            this.cmbSourcetype.SelectedIndexChanged += new System.EventHandler(this.cmbSourcetype_SelectedIndexChanged);
            // 
            // VidLineComboBox
            // 
            this.VidLineComboBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.VidLineComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VidLineComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VidLineComboBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.VidLineComboBox.FormattingEnabled = true;
            this.VidLineComboBox.Location = new System.Drawing.Point(22, 143);
            this.VidLineComboBox.Name = "VidLineComboBox";
            this.VidLineComboBox.Size = new System.Drawing.Size(290, 21);
            this.VidLineComboBox.TabIndex = 1;
            this.VidLineComboBox.SelectedIndexChanged += new System.EventHandler(this.VidLineComboBox_SelectedIndexChanged);
            // 
            // refreshsource
            // 
            this.refreshsource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.refreshsource.FlatAppearance.BorderSize = 0;
            this.refreshsource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.refreshsource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.refreshsource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshsource.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.refreshsource.ForeColor = System.Drawing.Color.Azure;
            this.refreshsource.Location = new System.Drawing.Point(223, 236);
            this.refreshsource.Name = "refreshsource";
            this.refreshsource.Size = new System.Drawing.Size(86, 23);
            this.refreshsource.TabIndex = 2;
            this.refreshsource.Text = "Refresh";
            this.refreshsource.UseVisualStyleBackColor = false;
            this.refreshsource.Click += new System.EventHandler(this.refreshsource_Click);
            // 
            // InitDevice
            // 
            this.InitDevice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.InitDevice.FlatAppearance.BorderSize = 0;
            this.InitDevice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.InitDevice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.InitDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InitDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitDevice.ForeColor = System.Drawing.Color.Azure;
            this.InitDevice.Location = new System.Drawing.Point(22, 236);
            this.InitDevice.Name = "InitDevice";
            this.InitDevice.Size = new System.Drawing.Size(80, 23);
            this.InitDevice.TabIndex = 3;
            this.InitDevice.Text = "Init Device";
            this.InitDevice.UseVisualStyleBackColor = false;
            this.InitDevice.Click += new System.EventHandler(this.InitDevice_Click);
            // 
            // CloseDevice
            // 
            this.CloseDevice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.CloseDevice.FlatAppearance.BorderSize = 0;
            this.CloseDevice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CloseDevice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.CloseDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.CloseDevice.ForeColor = System.Drawing.Color.Azure;
            this.CloseDevice.Location = new System.Drawing.Point(113, 236);
            this.CloseDevice.Name = "CloseDevice";
            this.CloseDevice.Size = new System.Drawing.Size(96, 23);
            this.CloseDevice.TabIndex = 4;
            this.CloseDevice.Text = "Close Device";
            this.CloseDevice.UseVisualStyleBackColor = false;
            this.CloseDevice.Click += new System.EventHandler(this.CloseDevice_Click);
            // 
            // Headpanel
            // 
            this.Headpanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Headpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Headpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Headpanel.Controls.Add(this.BtClose);
            this.Headpanel.Controls.Add(this.pictureBox1);
            this.Headpanel.Controls.Add(this.txtNdiHelp);
            this.Headpanel.Controls.Add(this.label2);
            this.Headpanel.Controls.Add(this.label1);
            this.Headpanel.Controls.Add(this.outpreview);
            this.Headpanel.Controls.Add(this.Heading);
            this.Headpanel.Controls.Add(this.CloseDevice);
            this.Headpanel.Controls.Add(this.refreshsource);
            this.Headpanel.Controls.Add(this.InitDevice);
            this.Headpanel.Controls.Add(this.cmbSourcetype);
            this.Headpanel.Controls.Add(this.VidLineComboBox);
            this.Headpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Headpanel.Location = new System.Drawing.Point(0, 0);
            this.Headpanel.Name = "Headpanel";
            this.Headpanel.Size = new System.Drawing.Size(331, 308);
            this.Headpanel.TabIndex = 5;
            this.Headpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Headpanel_MouseMove);
            // 
            // BtClose
            // 
            this.BtClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtClose.FlatAppearance.BorderSize = 0;
            this.BtClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.BtClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Salmon;
            this.BtClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtClose.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold);
            this.BtClose.ForeColor = System.Drawing.Color.White;
            this.BtClose.Location = new System.Drawing.Point(291, 5);
            this.BtClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtClose.Name = "BtClose";
            this.BtClose.Size = new System.Drawing.Size(29, 28);
            this.BtClose.TabIndex = 88;
            this.BtClose.Text = "X";
            this.BtClose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BtClose.UseVisualStyleBackColor = true;
            this.BtClose.Click += new System.EventHandler(this.Closeformbutton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(0, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(332, 1);
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // txtNdiHelp
            // 
            this.txtNdiHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtNdiHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNdiHelp.ForeColor = System.Drawing.Color.White;
            this.txtNdiHelp.Location = new System.Drawing.Point(23, 179);
            this.txtNdiHelp.Multiline = true;
            this.txtNdiHelp.Name = "txtNdiHelp";
            this.txtNdiHelp.ReadOnly = true;
            this.txtNdiHelp.Size = new System.Drawing.Size(286, 38);
            this.txtNdiHelp.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Device Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Source Type";
            // 
            // outpreview
            // 
            this.outpreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.outpreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.outpreview.FlatAppearance.BorderSize = 0;
            this.outpreview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.outpreview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.outpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.outpreview.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.outpreview.ForeColor = System.Drawing.Color.Azure;
            this.outpreview.Location = new System.Drawing.Point(22, 270);
            this.outpreview.Name = "outpreview";
            this.outpreview.Size = new System.Drawing.Size(287, 23);
            this.outpreview.TabIndex = 5;
            this.outpreview.Text = "Out Renderer";
            this.outpreview.UseVisualStyleBackColor = false;
            this.outpreview.Click += new System.EventHandler(this.outpreview_Click);
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Heading.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Heading.ForeColor = System.Drawing.Color.Azure;
            this.Heading.Location = new System.Drawing.Point(3, 9);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(166, 21);
            this.Heading.TabIndex = 1;
            this.Heading.Text = "Capture Input Source";
            this.Heading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Render
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(331, 308);
            this.Controls.Add(this.Headpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Render";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Render";
            this.Load += new System.EventHandler(this.Render_Load);
            this.Headpanel.ResumeLayout(false);
            this.Headpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button refreshsource;
        private System.Windows.Forms.Button InitDevice;
        private System.Windows.Forms.Button CloseDevice;
        private System.Windows.Forms.Panel Headpanel;
        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.Button outpreview;
        public System.Windows.Forms.ComboBox cmbSourcetype;
        public System.Windows.Forms.ComboBox VidLineComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNdiHelp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtClose;
    }
}