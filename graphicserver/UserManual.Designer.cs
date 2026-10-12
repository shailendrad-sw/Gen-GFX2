
namespace graphicserver
{
    partial class UserManual
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
            this.userManualViewer = new DevExpress.XtraPdfViewer.PdfViewer();
            this.SuspendLayout();
            // 
            // userManualViewer
            // 
            this.userManualViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userManualViewer.Location = new System.Drawing.Point(0, 0);
            this.userManualViewer.Name = "userManualViewer";
            this.userManualViewer.Size = new System.Drawing.Size(949, 662);
            this.userManualViewer.TabIndex = 0;
            // 
            // UserManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(949, 662);
            this.Controls.Add(this.userManualViewer);
            this.Name = "UserManual";
            this.Text = "UserManual";
            this.Load += new System.EventHandler(this.UserManualForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPdfViewer.PdfViewer userManualViewer;
    }
}