namespace graphicserver
{
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.HeadingLabel = new System.Windows.Forms.Label();
            this.DisplayMessagelbl = new System.Windows.Forms.Label();
            this.Button2OKNo = new System.Windows.Forms.Button();
            this.YesButton = new System.Windows.Forms.Button();
            this.IconPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HeadingLabel
            // 
            this.HeadingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeadingLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HeadingLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeadingLabel.Location = new System.Drawing.Point(0, 0);
            this.HeadingLabel.Name = "HeadingLabel";
            this.HeadingLabel.Size = new System.Drawing.Size(338, 39);
            this.HeadingLabel.TabIndex = 0;
            this.HeadingLabel.Text = "MBIV Graphic Server";
            this.HeadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HeadingLabel.Click += new System.EventHandler(this.HeadingLabel_Click);
            // 
            // DisplayMessagelbl
            // 
            this.DisplayMessagelbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DisplayMessagelbl.AutoSize = true;
            this.DisplayMessagelbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisplayMessagelbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayMessagelbl.Location = new System.Drawing.Point(42, 54);
            this.DisplayMessagelbl.MaximumSize = new System.Drawing.Size(271, 64);
            this.DisplayMessagelbl.Name = "DisplayMessagelbl";
            this.DisplayMessagelbl.Size = new System.Drawing.Size(271, 21);
            this.DisplayMessagelbl.TabIndex = 1;
            this.DisplayMessagelbl.Text = "Do you want to close this application?";
            this.DisplayMessagelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button2OKNo
            // 
            this.Button2OKNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button2OKNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.Button2OKNo.FlatAppearance.BorderSize = 0;
            this.Button2OKNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button2OKNo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2OKNo.ForeColor = System.Drawing.Color.Azure;
            this.Button2OKNo.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Button2OKNo.Location = new System.Drawing.Point(267, 106);
            this.Button2OKNo.Name = "Button2OKNo";
            this.Button2OKNo.Size = new System.Drawing.Size(50, 32);
            this.Button2OKNo.TabIndex = 2;
            this.Button2OKNo.Text = "OK";
            this.Button2OKNo.UseVisualStyleBackColor = false;
            this.Button2OKNo.Click += new System.EventHandler(this.Button2OKNo_Click);
            // 
            // YesButton
            // 
            this.YesButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.YesButton.FlatAppearance.BorderSize = 0;
            this.YesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.YesButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YesButton.ForeColor = System.Drawing.Color.Azure;
            this.YesButton.Location = new System.Drawing.Point(194, 106);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(50, 32);
            this.YesButton.TabIndex = 3;
            this.YesButton.Text = "Yes";
            this.YesButton.UseVisualStyleBackColor = false;
            this.YesButton.Visible = false;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // IconPictureBox
            // 
            this.IconPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("IconPictureBox.Image")));
            this.IconPictureBox.Location = new System.Drawing.Point(4, 49);
            this.IconPictureBox.Name = "IconPictureBox";
            this.IconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.IconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.IconPictureBox.TabIndex = 4;
            this.IconPictureBox.TabStop = false;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(338, 150);
            this.Controls.Add(this.IconPictureBox);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.Button2OKNo);
            this.Controls.Add(this.DisplayMessagelbl);
            this.Controls.Add(this.HeadingLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Azure;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(338, 150);
            this.MinimumSize = new System.Drawing.Size(338, 150);
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HeadingLabel;
        private System.Windows.Forms.Label DisplayMessagelbl;
        private System.Windows.Forms.Button Button2OKNo;
        private System.Windows.Forms.Button YesButton;
        private System.Windows.Forms.PictureBox IconPictureBox;
    }
}