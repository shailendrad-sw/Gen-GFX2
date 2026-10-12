namespace graphicserver
{
    partial class GridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
            this.ColumnNameCmb = new System.Windows.Forms.ComboBox();
            this.DisplayTextbox = new System.Windows.Forms.TextBox();
            this.DisplayTextLabel = new System.Windows.Forms.Label();
            this.Frozenlbl = new System.Windows.Forms.Label();
            this.Resizablelbl = new System.Windows.Forms.Label();
            this.ToolTiplbl = new System.Windows.Forms.Label();
            this.Visiblelbl = new System.Windows.Forms.Label();
            this.ColumnWidthlbl = new System.Windows.Forms.Label();
            this.ColumnWidthTextbox = new System.Windows.Forms.TextBox();
            this.Visiblecheckbox = new System.Windows.Forms.CheckBox();
            this.resizablecheckbox = new System.Windows.Forms.CheckBox();
            this.frozencheckbox = new System.Windows.Forms.CheckBox();
            this.Tooltiptextbox = new System.Windows.Forms.TextBox();
            this.Savebutton = new System.Windows.Forms.Button();
            this.ColumnNamelbl = new System.Windows.Forms.Label();
            this.GridCancelButton = new System.Windows.Forms.Button();
            this.DgvGridForm = new System.Windows.Forms.DataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolTip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resizable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frozen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadPannel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvGridForm)).BeginInit();
            this.HeadPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ColumnNameCmb
            // 
            this.ColumnNameCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColumnNameCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnNameCmb.FormattingEnabled = true;
            this.ColumnNameCmb.Location = new System.Drawing.Point(158, 47);
            this.ColumnNameCmb.Name = "ColumnNameCmb";
            this.ColumnNameCmb.Size = new System.Drawing.Size(165, 23);
            this.ColumnNameCmb.TabIndex = 0;
            this.ColumnNameCmb.SelectedIndexChanged += new System.EventHandler(this.ColumnNameCmb_SelectedIndexChanged);
            // 
            // DisplayTextbox
            // 
            this.DisplayTextbox.Location = new System.Drawing.Point(158, 76);
            this.DisplayTextbox.Name = "DisplayTextbox";
            this.DisplayTextbox.Size = new System.Drawing.Size(165, 23);
            this.DisplayTextbox.TabIndex = 1;
            // 
            // DisplayTextLabel
            // 
            this.DisplayTextLabel.AutoSize = true;
            this.DisplayTextLabel.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayTextLabel.Location = new System.Drawing.Point(14, 79);
            this.DisplayTextLabel.Name = "DisplayTextLabel";
            this.DisplayTextLabel.Size = new System.Drawing.Size(81, 20);
            this.DisplayTextLabel.TabIndex = 2;
            this.DisplayTextLabel.Text = "Display Text";
            this.DisplayTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frozenlbl
            // 
            this.Frozenlbl.AutoSize = true;
            this.Frozenlbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frozenlbl.Location = new System.Drawing.Point(431, 79);
            this.Frozenlbl.Name = "Frozenlbl";
            this.Frozenlbl.Size = new System.Drawing.Size(52, 20);
            this.Frozenlbl.TabIndex = 3;
            this.Frozenlbl.Text = "Frozen";
            this.Frozenlbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Resizablelbl
            // 
            this.Resizablelbl.AutoSize = true;
            this.Resizablelbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resizablelbl.Location = new System.Drawing.Point(338, 79);
            this.Resizablelbl.Name = "Resizablelbl";
            this.Resizablelbl.Size = new System.Drawing.Size(67, 20);
            this.Resizablelbl.TabIndex = 4;
            this.Resizablelbl.Text = "Resizable";
            this.Resizablelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolTiplbl
            // 
            this.ToolTiplbl.AutoSize = true;
            this.ToolTiplbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTiplbl.Location = new System.Drawing.Point(338, 50);
            this.ToolTiplbl.Name = "ToolTiplbl";
            this.ToolTiplbl.Size = new System.Drawing.Size(57, 20);
            this.ToolTiplbl.TabIndex = 5;
            this.ToolTiplbl.Text = "Tool Tip";
            this.ToolTiplbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Visiblelbl
            // 
            this.Visiblelbl.AutoSize = true;
            this.Visiblelbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Visiblelbl.Location = new System.Drawing.Point(505, 79);
            this.Visiblelbl.Name = "Visiblelbl";
            this.Visiblelbl.Size = new System.Drawing.Size(49, 20);
            this.Visiblelbl.TabIndex = 6;
            this.Visiblelbl.Text = "Visible";
            this.Visiblelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ColumnWidthlbl
            // 
            this.ColumnWidthlbl.AutoSize = true;
            this.ColumnWidthlbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnWidthlbl.Location = new System.Drawing.Point(14, 107);
            this.ColumnWidthlbl.Name = "ColumnWidthlbl";
            this.ColumnWidthlbl.Size = new System.Drawing.Size(97, 20);
            this.ColumnWidthlbl.TabIndex = 7;
            this.ColumnWidthlbl.Text = "Column Width";
            this.ColumnWidthlbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ColumnWidthTextbox
            // 
            this.ColumnWidthTextbox.Location = new System.Drawing.Point(158, 105);
            this.ColumnWidthTextbox.Name = "ColumnWidthTextbox";
            this.ColumnWidthTextbox.Size = new System.Drawing.Size(165, 23);
            this.ColumnWidthTextbox.TabIndex = 8;
            // 
            // Visiblecheckbox
            // 
            this.Visiblecheckbox.AutoSize = true;
            this.Visiblecheckbox.Location = new System.Drawing.Point(556, 84);
            this.Visiblecheckbox.Name = "Visiblecheckbox";
            this.Visiblecheckbox.Size = new System.Drawing.Size(15, 14);
            this.Visiblecheckbox.TabIndex = 9;
            this.Visiblecheckbox.UseVisualStyleBackColor = true;
            // 
            // resizablecheckbox
            // 
            this.resizablecheckbox.AutoSize = true;
            this.resizablecheckbox.Location = new System.Drawing.Point(411, 84);
            this.resizablecheckbox.Name = "resizablecheckbox";
            this.resizablecheckbox.Size = new System.Drawing.Size(15, 14);
            this.resizablecheckbox.TabIndex = 10;
            this.resizablecheckbox.UseVisualStyleBackColor = true;
            // 
            // frozencheckbox
            // 
            this.frozencheckbox.AutoSize = true;
            this.frozencheckbox.Location = new System.Drawing.Point(484, 84);
            this.frozencheckbox.Name = "frozencheckbox";
            this.frozencheckbox.Size = new System.Drawing.Size(15, 14);
            this.frozencheckbox.TabIndex = 11;
            this.frozencheckbox.UseVisualStyleBackColor = true;
            // 
            // Tooltiptextbox
            // 
            this.Tooltiptextbox.Location = new System.Drawing.Point(411, 47);
            this.Tooltiptextbox.Name = "Tooltiptextbox";
            this.Tooltiptextbox.Size = new System.Drawing.Size(165, 23);
            this.Tooltiptextbox.TabIndex = 12;
            // 
            // Savebutton
            // 
            this.Savebutton.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Savebutton.FlatAppearance.BorderSize = 0;
            this.Savebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Savebutton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Savebutton.ForeColor = System.Drawing.Color.Azure;
            this.Savebutton.Location = new System.Drawing.Point(217, 135);
            this.Savebutton.Name = "Savebutton";
            this.Savebutton.Size = new System.Drawing.Size(76, 27);
            this.Savebutton.TabIndex = 13;
            this.Savebutton.Text = "Save";
            this.Savebutton.UseVisualStyleBackColor = false;
            this.Savebutton.Click += new System.EventHandler(this.Savebutton_Click);
            // 
            // ColumnNamelbl
            // 
            this.ColumnNamelbl.AutoSize = true;
            this.ColumnNamelbl.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnNamelbl.Location = new System.Drawing.Point(14, 50);
            this.ColumnNamelbl.Name = "ColumnNamelbl";
            this.ColumnNamelbl.Size = new System.Drawing.Size(99, 20);
            this.ColumnNamelbl.TabIndex = 14;
            this.ColumnNamelbl.Text = "Column Name";
            this.ColumnNamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridCancelButton
            // 
            this.GridCancelButton.BackColor = System.Drawing.Color.Salmon;
            this.GridCancelButton.FlatAppearance.BorderSize = 0;
            this.GridCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridCancelButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridCancelButton.ForeColor = System.Drawing.Color.Azure;
            this.GridCancelButton.Location = new System.Drawing.Point(315, 135);
            this.GridCancelButton.Name = "GridCancelButton";
            this.GridCancelButton.Size = new System.Drawing.Size(76, 27);
            this.GridCancelButton.TabIndex = 15;
            this.GridCancelButton.Text = "Cancel";
            this.GridCancelButton.UseVisualStyleBackColor = false;
            this.GridCancelButton.Click += new System.EventHandler(this.GridCancelButton_Click);
            // 
            // DgvGridForm
            // 
            this.DgvGridForm.AllowUserToAddRows = false;
            this.DgvGridForm.AllowUserToDeleteRows = false;
            this.DgvGridForm.AllowUserToResizeRows = false;
            this.DgvGridForm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvGridForm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.DgvGridForm.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DgvGridForm.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvGridForm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvGridForm.ColumnHeadersHeight = 30;
            this.DgvGridForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.DisplayText,
            this.ColumnWidth,
            this.ToolTip,
            this.Resizable,
            this.Frozen,
            this.Visible,
            this.MapIndex});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvGridForm.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvGridForm.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DgvGridForm.EnableHeadersVisualStyles = false;
            this.DgvGridForm.Location = new System.Drawing.Point(5, 169);
            this.DgvGridForm.Margin = new System.Windows.Forms.Padding(0);
            this.DgvGridForm.MultiSelect = false;
            this.DgvGridForm.Name = "DgvGridForm";
            this.DgvGridForm.ReadOnly = true;
            this.DgvGridForm.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DgvGridForm.RowHeadersVisible = false;
            this.DgvGridForm.RowHeadersWidth = 25;
            this.DgvGridForm.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.DgvGridForm.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DgvGridForm.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DgvGridForm.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            this.DgvGridForm.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.DgvGridForm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DgvGridForm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvGridForm.Size = new System.Drawing.Size(698, 191);
            this.DgvGridForm.TabIndex = 16;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "ColumnName";
            this.ColumnName.FillWeight = 113.5417F;
            this.ColumnName.HeaderText = "Column Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            // 
            // DisplayText
            // 
            this.DisplayText.DataPropertyName = "DisplayText";
            this.DisplayText.FillWeight = 103.106F;
            this.DisplayText.HeaderText = "Display Text";
            this.DisplayText.Name = "DisplayText";
            this.DisplayText.ReadOnly = true;
            // 
            // ColumnWidth
            // 
            this.ColumnWidth.DataPropertyName = "ColumnWidth";
            this.ColumnWidth.FillWeight = 123.4095F;
            this.ColumnWidth.HeaderText = "Column Width";
            this.ColumnWidth.Name = "ColumnWidth";
            this.ColumnWidth.ReadOnly = true;
            // 
            // ToolTip
            // 
            this.ToolTip.DataPropertyName = "ToolTipText";
            this.ToolTip.FillWeight = 85.46812F;
            this.ToolTip.HeaderText = "Tool Tip";
            this.ToolTip.Name = "ToolTip";
            this.ToolTip.ReadOnly = true;
            // 
            // Resizable
            // 
            this.Resizable.DataPropertyName = "Resizable";
            this.Resizable.FillWeight = 98.31015F;
            this.Resizable.HeaderText = "Resizable";
            this.Resizable.Name = "Resizable";
            this.Resizable.ReadOnly = true;
            // 
            // Frozen
            // 
            this.Frozen.DataPropertyName = "Frozen";
            this.Frozen.FillWeight = 86.27337F;
            this.Frozen.HeaderText = "Frozen";
            this.Frozen.Name = "Frozen";
            this.Frozen.ReadOnly = true;
            // 
            // Visible
            // 
            this.Visible.DataPropertyName = "Visible";
            this.Visible.FillWeight = 89.89111F;
            this.Visible.HeaderText = "Visible";
            this.Visible.Name = "Visible";
            this.Visible.ReadOnly = true;
            // 
            // MapIndex
            // 
            this.MapIndex.DataPropertyName = "mapindex";
            this.MapIndex.HeaderText = "mapindex";
            this.MapIndex.Name = "MapIndex";
            this.MapIndex.ReadOnly = true;
            this.MapIndex.Visible = false;
            // 
            // HeadPannel
            // 
            this.HeadPannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.HeadPannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeadPannel.Controls.Add(this.BtnClose);
            this.HeadPannel.Controls.Add(this.pictureBox1);
            this.HeadPannel.Controls.Add(this.label14);
            this.HeadPannel.Controls.Add(this.ColumnNameCmb);
            this.HeadPannel.Controls.Add(this.DgvGridForm);
            this.HeadPannel.Controls.Add(this.DisplayTextbox);
            this.HeadPannel.Controls.Add(this.GridCancelButton);
            this.HeadPannel.Controls.Add(this.DisplayTextLabel);
            this.HeadPannel.Controls.Add(this.Tooltiptextbox);
            this.HeadPannel.Controls.Add(this.Frozenlbl);
            this.HeadPannel.Controls.Add(this.ColumnNamelbl);
            this.HeadPannel.Controls.Add(this.Resizablelbl);
            this.HeadPannel.Controls.Add(this.Savebutton);
            this.HeadPannel.Controls.Add(this.ToolTiplbl);
            this.HeadPannel.Controls.Add(this.frozencheckbox);
            this.HeadPannel.Controls.Add(this.Visiblelbl);
            this.HeadPannel.Controls.Add(this.resizablecheckbox);
            this.HeadPannel.Controls.Add(this.ColumnWidthlbl);
            this.HeadPannel.Controls.Add(this.Visiblecheckbox);
            this.HeadPannel.Controls.Add(this.ColumnWidthTextbox);
            this.HeadPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeadPannel.Location = new System.Drawing.Point(0, 0);
            this.HeadPannel.Margin = new System.Windows.Forms.Padding(1);
            this.HeadPannel.Name = "HeadPannel";
            this.HeadPannel.Size = new System.Drawing.Size(690, 443);
            this.HeadPannel.TabIndex = 17;
            this.HeadPannel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(1, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(712, 1);
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(9, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 21);
            this.label14.TabIndex = 82;
            this.label14.Text = "Grid Configuration";
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
            this.BtnClose.Location = new System.Drawing.Point(647, 2);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(29, 28);
            this.BtnClose.TabIndex = 90;
            this.BtnClose.Text = "X";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // GridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(690, 443);
            this.Controls.Add(this.HeadPannel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(706, 382);
            this.Name = "GridForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridForm";
            this.Load += new System.EventHandler(this.GridForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvGridForm)).EndInit();
            this.HeadPannel.ResumeLayout(false);
            this.HeadPannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resizable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frozen;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Visible;
        private System.Windows.Forms.DataGridViewTextBoxColumn MapIndex;
        public System.Windows.Forms.ComboBox ColumnNameCmb;
        public System.Windows.Forms.TextBox DisplayTextbox;
        public System.Windows.Forms.Label DisplayTextLabel;
        public System.Windows.Forms.Label Frozenlbl;
        public System.Windows.Forms.Label Resizablelbl;
        public System.Windows.Forms.Label ToolTiplbl;
        public System.Windows.Forms.Label Visiblelbl;
        public System.Windows.Forms.Label ColumnWidthlbl;
        public System.Windows.Forms.TextBox ColumnWidthTextbox;
        public System.Windows.Forms.CheckBox Visiblecheckbox;
        public System.Windows.Forms.CheckBox resizablecheckbox;
        public System.Windows.Forms.CheckBox frozencheckbox;
        public System.Windows.Forms.TextBox Tooltiptextbox;
        public System.Windows.Forms.Button Savebutton;
        public System.Windows.Forms.Label ColumnNamelbl;
        public System.Windows.Forms.Button GridCancelButton;
        public System.Windows.Forms.DataGridView DgvGridForm;
        private System.Windows.Forms.Panel HeadPannel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnClose;
    }
}