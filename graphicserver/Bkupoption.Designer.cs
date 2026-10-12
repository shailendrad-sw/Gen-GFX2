namespace graphicserver
{
    partial class Bkupoption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bkupoption));
            this.lblBKUPPATH = new System.Windows.Forms.Label();
            this.BkupPathtextBox = new System.Windows.Forms.TextBox();
            this.BtnBKUPBROWSE = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.btnCANCEL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBKUPPATH
            // 
            this.lblBKUPPATH.AutoSize = true;
            this.lblBKUPPATH.ForeColor = System.Drawing.Color.White;
            this.lblBKUPPATH.Location = new System.Drawing.Point(5, 27);
            this.lblBKUPPATH.Name = "lblBKUPPATH";
            this.lblBKUPPATH.Size = new System.Drawing.Size(54, 13);
            this.lblBKUPPATH.TabIndex = 2;
            this.lblBKUPPATH.Text = "BkupPath";
            // 
            // BkupPathtextBox
            // 
            this.BkupPathtextBox.Location = new System.Drawing.Point(69, 25);
            this.BkupPathtextBox.Name = "BkupPathtextBox";
            this.BkupPathtextBox.Size = new System.Drawing.Size(276, 20);
            this.BkupPathtextBox.TabIndex = 3;
            // 
            // BtnBKUPBROWSE
            // 
            this.BtnBKUPBROWSE.Location = new System.Drawing.Point(356, 24);
            this.BtnBKUPBROWSE.Name = "BtnBKUPBROWSE";
            this.BtnBKUPBROWSE.Size = new System.Drawing.Size(75, 23);
            this.BtnBKUPBROWSE.TabIndex = 4;
            this.BtnBKUPBROWSE.Text = "Browse";
            this.BtnBKUPBROWSE.UseVisualStyleBackColor = true;
            this.BtnBKUPBROWSE.Click += new System.EventHandler(this.BtnBKUPBROWSE_Click);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(107, 89);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 5;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnBkupOK_Click);
            // 
            // btnCANCEL
            // 
            this.btnCANCEL.Location = new System.Drawing.Point(208, 88);
            this.btnCANCEL.Name = "btnCANCEL";
            this.btnCANCEL.Size = new System.Drawing.Size(75, 23);
            this.btnCANCEL.TabIndex = 6;
            this.btnCANCEL.Text = "Cancel";
            this.btnCANCEL.UseVisualStyleBackColor = true;
            this.btnCANCEL.Click += new System.EventHandler(this.btpBkupCancel_click);
            // 
            // Bkupoption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(441, 131);
            this.Controls.Add(this.btnCANCEL);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnBKUPBROWSE);
            this.Controls.Add(this.BkupPathtextBox);
            this.Controls.Add(this.lblBKUPPATH);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Bkupoption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bkupoption";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Bkupoption_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBKUPPATH;
        private System.Windows.Forms.TextBox BkupPathtextBox;
        private System.Windows.Forms.Button BtnBKUPBROWSE;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button btnCANCEL;
    }
}