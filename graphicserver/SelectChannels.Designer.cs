namespace graphicserver
{
    partial class SelectChannels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectChannels));
            this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LowerPanel = new System.Windows.Forms.Panel();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.Btn_Create = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.HeadPanel = new System.Windows.Forms.Panel();
            this.Selectchannellbl = new System.Windows.Forms.Label();
            this.TabsPanel = new System.Windows.Forms.Panel();
            this.MainTableLayout.SuspendLayout();
            this.LowerPanel.SuspendLayout();
            this.HeadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayout
            // 
            this.MainTableLayout.ColumnCount = 1;
            this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayout.Controls.Add(this.LowerPanel, 0, 2);
            this.MainTableLayout.Controls.Add(this.HeadPanel, 0, 0);
            this.MainTableLayout.Controls.Add(this.TabsPanel, 0, 1);
            this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Name = "MainTableLayout";
            this.MainTableLayout.RowCount = 3;
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.MainTableLayout.Size = new System.Drawing.Size(432, 203);
            this.MainTableLayout.TabIndex = 1;
            // 
            // LowerPanel
            // 
            this.LowerPanel.Controls.Add(this.RemoveButton);
            this.LowerPanel.Controls.Add(this.Btn_Create);
            this.LowerPanel.Controls.Add(this.SaveButton);
            this.LowerPanel.Controls.Add(this.CloseButton);
            this.LowerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LowerPanel.Location = new System.Drawing.Point(3, 156);
            this.LowerPanel.Name = "LowerPanel";
            this.LowerPanel.Size = new System.Drawing.Size(426, 44);
            this.LowerPanel.TabIndex = 1;
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.Azure;
            this.RemoveButton.FlatAppearance.BorderSize = 0;
            this.RemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.RemoveButton.Location = new System.Drawing.Point(231, 10);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 25);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // Btn_Create
            // 
            this.Btn_Create.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_Create.BackColor = System.Drawing.Color.Azure;
            this.Btn_Create.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Create.FlatAppearance.BorderSize = 0;
            this.Btn_Create.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Btn_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Create.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Btn_Create.Location = new System.Drawing.Point(19, 10);
            this.Btn_Create.Name = "Btn_Create";
            this.Btn_Create.Size = new System.Drawing.Size(75, 25);
            this.Btn_Create.TabIndex = 3;
            this.Btn_Create.Text = "Create";
            this.Btn_Create.UseVisualStyleBackColor = false;
            this.Btn_Create.Click += new System.EventHandler(this.Btn_Create_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveButton.BackColor = System.Drawing.Color.Azure;
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SaveButton.FlatAppearance.BorderSize = 0;
            this.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.SaveButton.Location = new System.Drawing.Point(124, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 25);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Add";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Azure;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.CloseButton.Location = new System.Drawing.Point(333, 10);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 25);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // HeadPanel
            // 
            this.HeadPanel.Controls.Add(this.Selectchannellbl);
            this.HeadPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeadPanel.Location = new System.Drawing.Point(3, 3);
            this.HeadPanel.Name = "HeadPanel";
            this.HeadPanel.Size = new System.Drawing.Size(426, 24);
            this.HeadPanel.TabIndex = 0;
            // 
            // Selectchannellbl
            // 
            this.Selectchannellbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Selectchannellbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Selectchannellbl.Location = new System.Drawing.Point(0, 0);
            this.Selectchannellbl.Name = "Selectchannellbl";
            this.Selectchannellbl.Size = new System.Drawing.Size(426, 24);
            this.Selectchannellbl.TabIndex = 0;
            this.Selectchannellbl.Text = "Select Channels";
            this.Selectchannellbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabsPanel
            // 
            this.TabsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabsPanel.Location = new System.Drawing.Point(3, 33);
            this.TabsPanel.Name = "TabsPanel";
            this.TabsPanel.Size = new System.Drawing.Size(426, 117);
            this.TabsPanel.TabIndex = 1;
            // 
            // SelectChannels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(432, 203);
            this.ControlBox = false;
            this.Controls.Add(this.MainTableLayout);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Azure;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 250);
            this.MinimizeBox = false;
            this.Name = "SelectChannels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectChannels";
            this.Load += new System.EventHandler(this.SelectChannels_Load);
            this.MainTableLayout.ResumeLayout(false);
            this.LowerPanel.ResumeLayout(false);
            this.HeadPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayout;
        private System.Windows.Forms.Panel HeadPanel;
        private System.Windows.Forms.Label Selectchannellbl;
        private System.Windows.Forms.Panel TabsPanel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Panel LowerPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button Btn_Create;
        private System.Windows.Forms.Button RemoveButton;
    }
}