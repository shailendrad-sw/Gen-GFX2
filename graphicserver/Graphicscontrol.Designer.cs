namespace graphicserver
{
    partial class Graphicscontrol
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.templatetext = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.showbutton_manual = new System.Windows.Forms.Button();
            this.hidebutton_manual = new System.Windows.Forms.Button();
            this.killbutton_manual = new System.Windows.Forms.Button();
            this.Loadbutton_manual = new System.Windows.Forms.Button();
            this.Durationlbl = new System.Windows.Forms.Label();
            this.Durationtxtbox = new System.Windows.Forms.MaskedTextBox();
            this.tabControlData = new System.Windows.Forms.TabControl();
            this.PreviewOn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Template Name";
            // 
            // templatetext
            // 
            this.templatetext.AutoSize = true;
            this.templatetext.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templatetext.ForeColor = System.Drawing.Color.Gold;
            this.templatetext.Location = new System.Drawing.Point(116, 9);
            this.templatetext.Name = "templatetext";
            this.templatetext.Size = new System.Drawing.Size(82, 13);
            this.templatetext.TabIndex = 402;
            this.templatetext.Text = "Template Name";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Graphics";
            // 
            // showbutton_manual
            // 
            this.showbutton_manual.BackColor = System.Drawing.Color.LightSeaGreen;
            this.showbutton_manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.showbutton_manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.showbutton_manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showbutton_manual.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showbutton_manual.Image = global::graphicserver.Properties.Resources.show;
            this.showbutton_manual.Location = new System.Drawing.Point(307, 164);
            this.showbutton_manual.Name = "showbutton_manual";
            this.showbutton_manual.Size = new System.Drawing.Size(30, 29);
            this.showbutton_manual.TabIndex = 400;
            this.toolTip1.SetToolTip(this.showbutton_manual, "Show");
            this.showbutton_manual.UseVisualStyleBackColor = false;
            this.showbutton_manual.Click += new System.EventHandler(this.showbutton_manual_Click);
            // 
            // hidebutton_manual
            // 
            this.hidebutton_manual.BackColor = System.Drawing.Color.LightSeaGreen;
            this.hidebutton_manual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hidebutton_manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.hidebutton_manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.hidebutton_manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hidebutton_manual.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hidebutton_manual.Image = global::graphicserver.Properties.Resources.hide;
            this.hidebutton_manual.Location = new System.Drawing.Point(271, 164);
            this.hidebutton_manual.Name = "hidebutton_manual";
            this.hidebutton_manual.Size = new System.Drawing.Size(30, 29);
            this.hidebutton_manual.TabIndex = 300;
            this.toolTip1.SetToolTip(this.hidebutton_manual, "Hide");
            this.hidebutton_manual.UseVisualStyleBackColor = false;
            this.hidebutton_manual.Click += new System.EventHandler(this.hidebutton_manual_Click);
            // 
            // killbutton_manual
            // 
            this.killbutton_manual.BackColor = System.Drawing.Color.LightSeaGreen;
            this.killbutton_manual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.killbutton_manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.killbutton_manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.killbutton_manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.killbutton_manual.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.killbutton_manual.Image = global::graphicserver.Properties.Resources.close;
            this.killbutton_manual.Location = new System.Drawing.Point(235, 164);
            this.killbutton_manual.Name = "killbutton_manual";
            this.killbutton_manual.Size = new System.Drawing.Size(30, 29);
            this.killbutton_manual.TabIndex = 200;
            this.toolTip1.SetToolTip(this.killbutton_manual, "Kill");
            this.killbutton_manual.UseVisualStyleBackColor = false;
            this.killbutton_manual.Click += new System.EventHandler(this.killbutton_manual_Click);
            // 
            // Loadbutton_manual
            // 
            this.Loadbutton_manual.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Loadbutton_manual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Loadbutton_manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SteelBlue;
            this.Loadbutton_manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.Loadbutton_manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Loadbutton_manual.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.Loadbutton_manual.ForeColor = System.Drawing.Color.Black;
            this.Loadbutton_manual.Image = global::graphicserver.Properties.Resources.playman;
            this.Loadbutton_manual.Location = new System.Drawing.Point(199, 164);
            this.Loadbutton_manual.Name = "Loadbutton_manual";
            this.Loadbutton_manual.Size = new System.Drawing.Size(30, 29);
            this.Loadbutton_manual.TabIndex = 100;
            this.toolTip1.SetToolTip(this.Loadbutton_manual, "Play");
            this.Loadbutton_manual.UseVisualStyleBackColor = false;
            this.Loadbutton_manual.Click += new System.EventHandler(this.Loadbutton_manual_Click);
            // 
            // Durationlbl
            // 
            this.Durationlbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Durationlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Durationlbl.ForeColor = System.Drawing.Color.White;
            this.Durationlbl.Location = new System.Drawing.Point(6, 166);
            this.Durationlbl.Name = "Durationlbl";
            this.Durationlbl.Size = new System.Drawing.Size(70, 20);
            this.Durationlbl.TabIndex = 403;
            this.Durationlbl.Text = "DURATION";
            this.Durationlbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Durationtxtbox
            // 
            this.Durationtxtbox.Location = new System.Drawing.Point(86, 167);
            this.Durationtxtbox.Mask = "00:00:00:00";
            this.Durationtxtbox.Name = "Durationtxtbox";
            this.Durationtxtbox.Size = new System.Drawing.Size(97, 20);
            this.Durationtxtbox.TabIndex = 404;
            this.Durationtxtbox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // tabControlData
            // 
            this.tabControlData.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlData.Location = new System.Drawing.Point(9, 30);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedIndex = 0;
            this.tabControlData.Size = new System.Drawing.Size(817, 131);
            this.tabControlData.TabIndex = 405;
            // 
            // PreviewOn
            // 
            this.PreviewOn.BackColor = System.Drawing.Color.LightSeaGreen;
            this.PreviewOn.Location = new System.Drawing.Point(344, 164);
            this.PreviewOn.Name = "PreviewOn";
            this.PreviewOn.Size = new System.Drawing.Size(64, 27);
            this.PreviewOn.TabIndex = 406;
            this.PreviewOn.Text = "Preview";
            this.PreviewOn.UseVisualStyleBackColor = false;
            this.PreviewOn.Visible = false;
            // 
            // Graphicscontrol
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.PreviewOn);
            this.Controls.Add(this.tabControlData);
            this.Controls.Add(this.Durationtxtbox);
            this.Controls.Add(this.Durationlbl);
            this.Controls.Add(this.templatetext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showbutton_manual);
            this.Controls.Add(this.hidebutton_manual);
            this.Controls.Add(this.killbutton_manual);
            this.Controls.Add(this.Loadbutton_manual);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Graphicscontrol";
            this.Size = new System.Drawing.Size(832, 199);
            this.Load += new System.EventHandler(this.Graphicscontrol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Loadbutton_manual;
        private System.Windows.Forms.Button killbutton_manual;
        private System.Windows.Forms.Button hidebutton_manual;
        private System.Windows.Forms.Button showbutton_manual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label templatetext;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label Durationlbl;
        private System.Windows.Forms.MaskedTextBox Durationtxtbox;
        private System.Windows.Forms.TabControl tabControlData;
        private System.Windows.Forms.CheckBox PreviewOn;
    }
}
