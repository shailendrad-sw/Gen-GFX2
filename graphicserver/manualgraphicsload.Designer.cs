namespace graphicserver
{
    partial class manualgraphicsload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(manualgraphicsload));
            this.availtemp = new System.Windows.Forms.Label();
            this.plytemp = new System.Windows.Forms.Label();
            this.lb_playingtemplatelist = new System.Windows.Forms.ListBox();
            this.lb_templatelist = new System.Windows.Forms.ListBox();
            this.flp_manualloadtemplate = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.menu_strip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menu_strip_main.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // availtemp
            // 
            this.availtemp.AutoSize = true;
            this.availtemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.availtemp.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availtemp.ForeColor = System.Drawing.Color.Maroon;
            this.availtemp.Location = new System.Drawing.Point(3, 0);
            this.availtemp.Name = "availtemp";
            this.availtemp.Size = new System.Drawing.Size(167, 21);
            this.availtemp.TabIndex = 47;
            this.availtemp.Text = "Available Templates";
            this.availtemp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // plytemp
            // 
            this.plytemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plytemp.AutoSize = true;
            this.plytemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plytemp.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold);
            this.plytemp.ForeColor = System.Drawing.Color.Maroon;
            this.plytemp.Location = new System.Drawing.Point(6, 349);
            this.plytemp.Name = "plytemp";
            this.plytemp.Size = new System.Drawing.Size(153, 21);
            this.plytemp.TabIndex = 49;
            this.plytemp.Text = "Playing Templates";
            this.plytemp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lb_playingtemplatelist
            // 
            this.lb_playingtemplatelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_playingtemplatelist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lb_playingtemplatelist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_playingtemplatelist.Font = new System.Drawing.Font("Segoe UI Semilight", 11.25F);
            this.lb_playingtemplatelist.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lb_playingtemplatelist.FormattingEnabled = true;
            this.lb_playingtemplatelist.ItemHeight = 20;
            this.lb_playingtemplatelist.Location = new System.Drawing.Point(5, 373);
            this.lb_playingtemplatelist.Margin = new System.Windows.Forms.Padding(0);
            this.lb_playingtemplatelist.Name = "lb_playingtemplatelist";
            this.lb_playingtemplatelist.Size = new System.Drawing.Size(182, 260);
            this.lb_playingtemplatelist.TabIndex = 46;
            // 
            // lb_templatelist
            // 
            this.lb_templatelist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lb_templatelist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_templatelist.Font = new System.Drawing.Font("Segoe UI Semilight", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_templatelist.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lb_templatelist.FormattingEnabled = true;
            this.lb_templatelist.HorizontalScrollbar = true;
            this.lb_templatelist.ItemHeight = 20;
            this.lb_templatelist.Location = new System.Drawing.Point(5, 26);
            this.lb_templatelist.Margin = new System.Windows.Forms.Padding(0);
            this.lb_templatelist.Name = "lb_templatelist";
            this.lb_templatelist.Size = new System.Drawing.Size(183, 320);
            this.lb_templatelist.Sorted = true;
            this.lb_templatelist.TabIndex = 44;
            this.lb_templatelist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_templatelist_MouseDown);
            // 
            // flp_manualloadtemplate
            // 
            this.flp_manualloadtemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flp_manualloadtemplate.AutoScroll = true;
            this.flp_manualloadtemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.flp_manualloadtemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flp_manualloadtemplate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flp_manualloadtemplate.Location = new System.Drawing.Point(193, 36);
            this.flp_manualloadtemplate.Margin = new System.Windows.Forms.Padding(0);
            this.flp_manualloadtemplate.Name = "flp_manualloadtemplate";
            this.flp_manualloadtemplate.Size = new System.Drawing.Size(864, 620);
            this.flp_manualloadtemplate.TabIndex = 45;
            this.flp_manualloadtemplate.DragDrop += new System.Windows.Forms.DragEventHandler(this.flp_manualloadtemplate_DragDrop);
            this.flp_manualloadtemplate.DragOver += new System.Windows.Forms.DragEventHandler(this.flp_manualloadtemplate_DragOver);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1107, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 51;
            this.button2.Text = "Refress Content";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // menu_strip_main
            // 
            this.menu_strip_main.AutoSize = false;
            this.menu_strip_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menu_strip_main.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_strip_main.GripMargin = new System.Windows.Forms.Padding(0);
            this.menu_strip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menu_strip_main.Location = new System.Drawing.Point(0, 0);
            this.menu_strip_main.Name = "menu_strip_main";
            this.menu_strip_main.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menu_strip_main.Size = new System.Drawing.Size(1060, 35);
            this.menu_strip_main.TabIndex = 52;
            this.menu_strip_main.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshConfigToolStripMenuItem,
            this.contentListToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 31);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // refreshConfigToolStripMenuItem
            // 
            this.refreshConfigToolStripMenuItem.Image = global::graphicserver.Properties.Resources.refresh;
            this.refreshConfigToolStripMenuItem.Name = "refreshConfigToolStripMenuItem";
            this.refreshConfigToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.refreshConfigToolStripMenuItem.Text = "Refresh Content List";
            this.refreshConfigToolStripMenuItem.Click += new System.EventHandler(this.refreshConfigToolStripMenuItem_Click);
            // 
            // contentListToolStripMenuItem
            // 
            this.contentListToolStripMenuItem.Name = "contentListToolStripMenuItem";
            this.contentListToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.contentListToolStripMenuItem.Text = "Content List";
            this.contentListToolStripMenuItem.Visible = false;
            this.contentListToolStripMenuItem.Click += new System.EventHandler(this.contentListToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.lb_templatelist);
            this.panel1.Controls.Add(this.plytemp);
            this.panel1.Controls.Add(this.availtemp);
            this.panel1.Controls.Add(this.lb_playingtemplatelist);
            this.panel1.Location = new System.Drawing.Point(1, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 620);
            this.panel1.TabIndex = 53;
            // 
            // manualgraphicsload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1060, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu_strip_main);
            this.Controls.Add(this.flp_manualloadtemplate);
            this.Controls.Add(this.button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(754, 300);
            this.Name = "manualgraphicsload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Graphics Load";
            this.Load += new System.EventHandler(this.manualgraphicsload_Load);
            this.menu_strip_main.ResumeLayout(false);
            this.menu_strip_main.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label plytemp;
        private System.Windows.Forms.Label availtemp;
        public System.Windows.Forms.ListBox lb_templatelist;
        public System.Windows.Forms.FlowLayoutPanel flp_manualloadtemplate;
        public System.Windows.Forms.ListBox lb_playingtemplatelist;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menu_strip_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshConfigToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem contentListToolStripMenuItem;
    }
}