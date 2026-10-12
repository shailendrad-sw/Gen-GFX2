namespace graphicserver
{
    partial class Renderout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Renderout));
            this.renderoutsrccmbbox = new System.Windows.Forms.ComboBox();
            this.srcoutchkbox = new System.Windows.Forms.CheckBox();
            this.renderoutcombobox = new System.Windows.Forms.ComboBox();
            this.renderoutclosebut = new System.Windows.Forms.Button();
            this.Heading = new System.Windows.Forms.Label();
            this.Headerpanel = new System.Windows.Forms.Panel();
            this.NtwrkStrmchbox = new System.Windows.Forms.CheckBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Headerpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // renderoutsrccmbbox
            // 
            this.renderoutsrccmbbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderoutsrccmbbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderoutsrccmbbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renderoutsrccmbbox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.renderoutsrccmbbox.FormattingEnabled = true;
            this.renderoutsrccmbbox.Items.AddRange(new object[] {
            "NDI",
            "SDI",
            "VIRTUAL"});
            this.renderoutsrccmbbox.Location = new System.Drawing.Point(15, 88);
            this.renderoutsrccmbbox.Name = "renderoutsrccmbbox";
            this.renderoutsrccmbbox.Size = new System.Drawing.Size(264, 21);
            this.renderoutsrccmbbox.TabIndex = 35;
            this.renderoutsrccmbbox.SelectedIndexChanged += new System.EventHandler(this.renderoutsrccmbbox_SelectedIndexChanged);
            // 
            // srcoutchkbox
            // 
            this.srcoutchkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.srcoutchkbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.srcoutchkbox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.srcoutchkbox.ForeColor = System.Drawing.Color.Azure;
            this.srcoutchkbox.Location = new System.Drawing.Point(17, 184);
            this.srcoutchkbox.Name = "srcoutchkbox";
            this.srcoutchkbox.Size = new System.Drawing.Size(94, 23);
            this.srcoutchkbox.TabIndex = 36;
            this.srcoutchkbox.Text = "Renderer";
            this.srcoutchkbox.UseVisualStyleBackColor = true;
            this.srcoutchkbox.CheckedChanged += new System.EventHandler(this.srcoutchkbox_CheckedChanged);
            // 
            // renderoutcombobox
            // 
            this.renderoutcombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderoutcombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderoutcombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renderoutcombobox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.renderoutcombobox.FormattingEnabled = true;
            this.renderoutcombobox.Location = new System.Drawing.Point(15, 137);
            this.renderoutcombobox.Name = "renderoutcombobox";
            this.renderoutcombobox.Size = new System.Drawing.Size(262, 21);
            this.renderoutcombobox.TabIndex = 37;
            this.renderoutcombobox.SelectedIndexChanged += new System.EventHandler(this.renderoutcombobox_SelectedIndexChanged);
            // 
            // renderoutclosebut
            // 
            this.renderoutclosebut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.renderoutclosebut.FlatAppearance.BorderSize = 0;
            this.renderoutclosebut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.renderoutclosebut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.renderoutclosebut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renderoutclosebut.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.renderoutclosebut.ForeColor = System.Drawing.Color.Azure;
            this.renderoutclosebut.Location = new System.Drawing.Point(160, 183);
            this.renderoutclosebut.Name = "renderoutclosebut";
            this.renderoutclosebut.Size = new System.Drawing.Size(112, 23);
            this.renderoutclosebut.TabIndex = 38;
            this.renderoutclosebut.Text = "Close Device";
            this.renderoutclosebut.UseVisualStyleBackColor = false;
            this.renderoutclosebut.Click += new System.EventHandler(this.renderoutclosebut_Click);
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Heading.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Heading.ForeColor = System.Drawing.Color.Azure;
            this.Heading.Location = new System.Drawing.Point(1, 8);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(164, 21);
            this.Heading.TabIndex = 40;
            this.Heading.Text = "Video Source Output";
            this.Heading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Headerpanel
            // 
            this.Headerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Headerpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Headerpanel.Controls.Add(this.NtwrkStrmchbox);
            this.Headerpanel.Controls.Add(this.BtnClose);
            this.Headerpanel.Controls.Add(this.pictureBox1);
            this.Headerpanel.Controls.Add(this.label2);
            this.Headerpanel.Controls.Add(this.label1);
            this.Headerpanel.Controls.Add(this.Heading);
            this.Headerpanel.Controls.Add(this.renderoutclosebut);
            this.Headerpanel.Controls.Add(this.renderoutcombobox);
            this.Headerpanel.Controls.Add(this.renderoutsrccmbbox);
            this.Headerpanel.Controls.Add(this.srcoutchkbox);
            this.Headerpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Headerpanel.Location = new System.Drawing.Point(0, 0);
            this.Headerpanel.Name = "Headerpanel";
            this.Headerpanel.Size = new System.Drawing.Size(308, 283);
            this.Headerpanel.TabIndex = 41;
            this.Headerpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Headerpanel_MouseMove);
            // 
            // NtwrkStrmchbox
            // 
            this.NtwrkStrmchbox.AutoSize = true;
            this.NtwrkStrmchbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NtwrkStrmchbox.ForeColor = System.Drawing.Color.GhostWhite;
            this.NtwrkStrmchbox.Location = new System.Drawing.Point(10, 227);
            this.NtwrkStrmchbox.Name = "NtwrkStrmchbox";
            this.NtwrkStrmchbox.Size = new System.Drawing.Size(114, 19);
            this.NtwrkStrmchbox.TabIndex = 91;
            this.NtwrkStrmchbox.Text = "Network  Stream";
            this.NtwrkStrmchbox.UseVisualStyleBackColor = true;
            this.NtwrkStrmchbox.CheckedChanged += new System.EventHandler(this.NtwrkStrmchbox_CheckedChanged);
            this.NtwrkStrmchbox.Click += new System.EventHandler(this.NtwrkStrmchbox_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Salmon;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold);
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(270, 5);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(29, 28);
            this.BtnClose.TabIndex = 89;
            this.BtnClose.Text = "X";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.Closeformbutton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(-3, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(310, 1);
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 42;
            this.label2.Text = "Output Device";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "Output Type";
            // 
            // Renderout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(308, 283);
            this.Controls.Add(this.Headerpanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Renderout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renderout";
            this.Load += new System.EventHandler(this.Renderout_Load);
            this.Headerpanel.ResumeLayout(false);
            this.Headerpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button renderoutclosebut;
        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.Panel Headerpanel;
        public System.Windows.Forms.ComboBox renderoutsrccmbbox;
        public System.Windows.Forms.CheckBox srcoutchkbox;
        public System.Windows.Forms.ComboBox renderoutcombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnClose;
        public System.Windows.Forms.CheckBox NtwrkStrmchbox;
    }
}