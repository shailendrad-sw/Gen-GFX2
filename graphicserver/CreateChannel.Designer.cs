namespace graphicserver
{
    partial class CreateChannel
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
            this.btn_CreateChannel = new System.Windows.Forms.Button();
            this.CreateChannel_LBL = new System.Windows.Forms.Label();
            this.txtbox_CreateChannel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_CreateChannel
            // 
            this.btn_CreateChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CreateChannel.ForeColor = System.Drawing.Color.White;
            this.btn_CreateChannel.Location = new System.Drawing.Point(107, 98);
            this.btn_CreateChannel.Name = "btn_CreateChannel";
            this.btn_CreateChannel.Size = new System.Drawing.Size(93, 32);
            this.btn_CreateChannel.TabIndex = 0;
            this.btn_CreateChannel.Text = "Create";
            this.btn_CreateChannel.UseVisualStyleBackColor = true;
            this.btn_CreateChannel.Click += new System.EventHandler(this.btn_CreateChannel_Click);
            // 
            // CreateChannel_LBL
            // 
            this.CreateChannel_LBL.AutoSize = true;
            this.CreateChannel_LBL.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateChannel_LBL.ForeColor = System.Drawing.Color.White;
            this.CreateChannel_LBL.Location = new System.Drawing.Point(31, 37);
            this.CreateChannel_LBL.Name = "CreateChannel_LBL";
            this.CreateChannel_LBL.Size = new System.Drawing.Size(113, 17);
            this.CreateChannel_LBL.TabIndex = 1;
            this.CreateChannel_LBL.Text = "Name of Channel";
            // 
            // txtbox_CreateChannel
            // 
            this.txtbox_CreateChannel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox_CreateChannel.Location = new System.Drawing.Point(150, 34);
            this.txtbox_CreateChannel.Name = "txtbox_CreateChannel";
            this.txtbox_CreateChannel.Size = new System.Drawing.Size(180, 25);
            this.txtbox_CreateChannel.TabIndex = 2;
            // 
            // CreateChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(367, 189);
            this.Controls.Add(this.txtbox_CreateChannel);
            this.Controls.Add(this.CreateChannel_LBL);
            this.Controls.Add(this.btn_CreateChannel);
            this.Name = "CreateChannel";
            this.Text = "CreateChannel";
            this.Load += new System.EventHandler(this.CreateChannel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_CreateChannel;
        private System.Windows.Forms.Label CreateChannel_LBL;
        private System.Windows.Forms.TextBox txtbox_CreateChannel;
    }
}