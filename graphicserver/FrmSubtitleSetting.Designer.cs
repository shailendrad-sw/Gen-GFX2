namespace graphicserver
{
    partial class FrmSubtitleSetting
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
            this.PnlBody = new System.Windows.Forms.Panel();
            this.BtnClosePairent = new System.Windows.Forms.Button();
            this.btnMinimise = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PnlBody
            // 
            this.PnlBody.Location = new System.Drawing.Point(0, 41);
            this.PnlBody.Name = "PnlBody";
            this.PnlBody.Size = new System.Drawing.Size(855, 407);
            this.PnlBody.TabIndex = 106;
            this.PnlBody.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // BtnClosePairent
            // 
            this.BtnClosePairent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnClosePairent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.BtnClosePairent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.BtnClosePairent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClosePairent.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.BtnClosePairent.ForeColor = System.Drawing.Color.White;
            this.BtnClosePairent.Location = new System.Drawing.Point(809, 7);
            this.BtnClosePairent.Name = "BtnClosePairent";
            this.BtnClosePairent.Size = new System.Drawing.Size(30, 28);
            this.BtnClosePairent.TabIndex = 109;
            this.BtnClosePairent.Text = "r";
            this.BtnClosePairent.UseVisualStyleBackColor = true;
            this.BtnClosePairent.Click += new System.EventHandler(this.BtnClosePairent_Click);
            // 
            // btnMinimise
            // 
            this.btnMinimise.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMinimise.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.btnMinimise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimise.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnMinimise.ForeColor = System.Drawing.Color.White;
            this.btnMinimise.Location = new System.Drawing.Point(775, 7);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.Size = new System.Drawing.Size(30, 28);
            this.btnMinimise.TabIndex = 107;
            this.btnMinimise.Text = "0";
            this.btnMinimise.UseVisualStyleBackColor = true;
            this.btnMinimise.Click += new System.EventHandler(this.btnMinimise_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 21);
            this.label1.TabIndex = 110;
            this.label1.Text = "Edit Subtitle Setting";
            // 
            // FrmSubtitleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(855, 448);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClosePairent);
            this.Controls.Add(this.btnMinimise);
            this.Controls.Add(this.PnlBody);
            this.MaximumSize = new System.Drawing.Size(871, 464);
            this.MinimumSize = new System.Drawing.Size(871, 464);
            this.Name = "FrmSubtitleSetting";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FrmSubtitleSetting_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmSubtitleSetting_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PnlBody;
        private System.Windows.Forms.Button BtnClosePairent;
        private System.Windows.Forms.Button btnMinimise;
        private System.Windows.Forms.Label label1;
    }
}