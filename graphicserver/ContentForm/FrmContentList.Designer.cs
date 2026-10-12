namespace graphicserver
{
    partial class FrmContentList
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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.BtnDbContent = new System.Windows.Forms.Button();
            this.BtnContentDetail = new System.Windows.Forms.Button();
            this.BtnRefreshContentList = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblFormName = new System.Windows.Forms.Label();
            this.BtnClosePairent = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnMinimise = new System.Windows.Forms.Button();
            this.BtnChildClose = new System.Windows.Forms.Button();
            this.pnlDesktop = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.pnlDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.pnlMenu.Controls.Add(this.BtnDbContent);
            this.pnlMenu.Controls.Add(this.BtnContentDetail);
            this.pnlMenu.Controls.Add(this.BtnRefreshContentList);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(200, 477);
            this.pnlMenu.TabIndex = 0;
            // 
            // BtnDbContent
            // 
            this.BtnDbContent.BackColor = System.Drawing.Color.Transparent;
            this.BtnDbContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnDbContent.FlatAppearance.BorderSize = 0;
            this.BtnDbContent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BtnDbContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDbContent.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDbContent.ForeColor = System.Drawing.Color.Gainsboro;
            this.BtnDbContent.Image = global::graphicserver.Properties.Resources.playman;
            this.BtnDbContent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDbContent.Location = new System.Drawing.Point(0, 184);
            this.BtnDbContent.Name = "BtnDbContent";
            this.BtnDbContent.Size = new System.Drawing.Size(200, 49);
            this.BtnDbContent.TabIndex = 3;
            this.BtnDbContent.Text = "Content List In DB";
            this.BtnDbContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDbContent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnDbContent.UseVisualStyleBackColor = false;
            this.BtnDbContent.Click += new System.EventHandler(this.BtnDbContent_Click);
            // 
            // BtnContentDetail
            // 
            this.BtnContentDetail.BackColor = System.Drawing.Color.Transparent;
            this.BtnContentDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnContentDetail.FlatAppearance.BorderSize = 0;
            this.BtnContentDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BtnContentDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnContentDetail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnContentDetail.ForeColor = System.Drawing.Color.Gainsboro;
            this.BtnContentDetail.Image = global::graphicserver.Properties.Resources.playman;
            this.BtnContentDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnContentDetail.Location = new System.Drawing.Point(0, 135);
            this.BtnContentDetail.Name = "BtnContentDetail";
            this.BtnContentDetail.Size = new System.Drawing.Size(200, 49);
            this.BtnContentDetail.TabIndex = 2;
            this.BtnContentDetail.Text = "Content Detail";
            this.BtnContentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnContentDetail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnContentDetail.UseVisualStyleBackColor = false;
            this.BtnContentDetail.Click += new System.EventHandler(this.BtnContentDetail_Click);
            // 
            // BtnRefreshContentList
            // 
            this.BtnRefreshContentList.BackColor = System.Drawing.Color.Transparent;
            this.BtnRefreshContentList.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnRefreshContentList.FlatAppearance.BorderSize = 0;
            this.BtnRefreshContentList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BtnRefreshContentList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefreshContentList.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefreshContentList.ForeColor = System.Drawing.Color.Gainsboro;
            this.BtnRefreshContentList.Image = global::graphicserver.Properties.Resources.playman;
            this.BtnRefreshContentList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRefreshContentList.Location = new System.Drawing.Point(0, 86);
            this.BtnRefreshContentList.Name = "BtnRefreshContentList";
            this.BtnRefreshContentList.Size = new System.Drawing.Size(200, 49);
            this.BtnRefreshContentList.TabIndex = 1;
            this.BtnRefreshContentList.Text = "Refresh Content List";
            this.BtnRefreshContentList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRefreshContentList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnRefreshContentList.UseVisualStyleBackColor = false;
            this.BtnRefreshContentList.Click += new System.EventHandler(this.BtnRefreshContentList_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.pnlLogo.Controls.Add(this.pictureBox1);
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(200, 86);
            this.pnlLogo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::graphicserver.Properties.Resources.LOGGMEICON;
            this.pictureBox1.Location = new System.Drawing.Point(17, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(91, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "GFX2.X";
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.Teal;
            this.pnlTitle.Controls.Add(this.lblFormName);
            this.pnlTitle.Controls.Add(this.BtnClosePairent);
            this.pnlTitle.Controls.Add(this.btnRestore);
            this.pnlTitle.Controls.Add(this.btnMinimise);
            this.pnlTitle.Controls.Add(this.BtnChildClose);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(200, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(770, 86);
            this.pnlTitle.TabIndex = 1;
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // lblFormName
            // 
            this.lblFormName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFormName.AutoSize = true;
            this.lblFormName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.Color.White;
            this.lblFormName.Location = new System.Drawing.Point(302, 25);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(84, 32);
            this.lblFormName.TabIndex = 4;
            this.lblFormName.Text = "HOME";
            // 
            // BtnClosePairent
            // 
            this.BtnClosePairent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnClosePairent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.BtnClosePairent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnClosePairent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClosePairent.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.BtnClosePairent.ForeColor = System.Drawing.Color.White;
            this.BtnClosePairent.Location = new System.Drawing.Point(730, 12);
            this.BtnClosePairent.Name = "BtnClosePairent";
            this.BtnClosePairent.Size = new System.Drawing.Size(30, 28);
            this.BtnClosePairent.TabIndex = 3;
            this.BtnClosePairent.Text = "r";
            this.BtnClosePairent.UseVisualStyleBackColor = true;
            this.BtnClosePairent.Click += new System.EventHandler(this.BtnClosePairent_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(698, 12);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(30, 28);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "1";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnMinimise
            // 
            this.btnMinimise.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMinimise.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.btnMinimise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimise.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnMinimise.ForeColor = System.Drawing.Color.White;
            this.btnMinimise.Location = new System.Drawing.Point(666, 12);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.Size = new System.Drawing.Size(30, 28);
            this.btnMinimise.TabIndex = 1;
            this.btnMinimise.Text = "0";
            this.btnMinimise.UseVisualStyleBackColor = true;
            this.btnMinimise.Click += new System.EventHandler(this.btnMinimise_Click);
            // 
            // BtnChildClose
            // 
            this.BtnChildClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnChildClose.FlatAppearance.BorderSize = 0;
            this.BtnChildClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.BtnChildClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnChildClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChildClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChildClose.ForeColor = System.Drawing.Color.White;
            this.BtnChildClose.Location = new System.Drawing.Point(0, 0);
            this.BtnChildClose.Name = "BtnChildClose";
            this.BtnChildClose.Size = new System.Drawing.Size(49, 86);
            this.BtnChildClose.TabIndex = 0;
            this.BtnChildClose.Text = "X";
            this.BtnChildClose.UseVisualStyleBackColor = true;
            this.BtnChildClose.Visible = false;
            this.BtnChildClose.Click += new System.EventHandler(this.BtnChildClose_Click);
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.Controls.Add(this.pictureBox2);
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(200, 86);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(770, 391);
            this.pnlDesktop.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = global::graphicserver.Properties.Resources.LOGGMEICON;
            this.pictureBox2.Location = new System.Drawing.Point(262, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(215, 166);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // FrmContentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(970, 477);
            this.ControlBox = false;
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlMenu);
            this.Name = "FrmContentList";
            this.Load += new System.EventHandler(this.FrmContentList_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button BtnRefreshContentList;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Button BtnDbContent;
        private System.Windows.Forms.Button BtnContentDetail;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlDesktop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnChildClose;
        private System.Windows.Forms.Button BtnClosePairent;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnMinimise;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblFormName;
    }
}