namespace graphicserver
{
    partial class RestoreOption
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.custom_dgv = new System.Windows.Forms.DataGridView();
            this.DownloadRspoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.custom_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // custom_dgv
            // 
            this.custom_dgv.AllowUserToAddRows = false;
            this.custom_dgv.AllowUserToDeleteRows = false;
            this.custom_dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.custom_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.custom_dgv.BackgroundColor = System.Drawing.Color.Black;
            this.custom_dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(90)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.custom_dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.custom_dgv.ColumnHeadersHeight = 30;
            this.custom_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.custom_dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.custom_dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.custom_dgv.EnableHeadersVisualStyles = false;
            this.custom_dgv.GridColor = System.Drawing.Color.Black;
            this.custom_dgv.Location = new System.Drawing.Point(1, 3);
            this.custom_dgv.MultiSelect = false;
            this.custom_dgv.Name = "custom_dgv";
            this.custom_dgv.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(90)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.custom_dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.custom_dgv.RowHeadersVisible = false;
            this.custom_dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.custom_dgv.Size = new System.Drawing.Size(291, 238);
            this.custom_dgv.TabIndex = 30;
            // 
            // DownloadRspoint
            // 
            this.DownloadRspoint.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.DownloadRspoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DownloadRspoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadRspoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadRspoint.Location = new System.Drawing.Point(1, 244);
            this.DownloadRspoint.Name = "DownloadRspoint";
            this.DownloadRspoint.Size = new System.Drawing.Size(290, 29);
            this.DownloadRspoint.TabIndex = 31;
            this.DownloadRspoint.Text = "Restore";
            this.DownloadRspoint.UseVisualStyleBackColor = false;
            this.DownloadRspoint.Click += new System.EventHandler(this.DownloadRspoint_Click);
            // 
            // RestoreOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(292, 272);
            this.Controls.Add(this.DownloadRspoint);
            this.Controls.Add(this.custom_dgv);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RestoreOption";
            ((System.ComponentModel.ISupportInitialize)(this.custom_dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView custom_dgv;
        private System.Windows.Forms.Button DownloadRspoint;
    }
}