namespace graphicserver
{
    partial class frmTrigger
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
            this.Headpanel = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.srnotxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtIntroTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.DgvTrigger = new System.Windows.Forms.DataGridView();
            this.colSrNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colElementid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyvalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGfxObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colevent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIntroTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEvent = new System.Windows.Forms.ComboBox();
            this.cmbGfxObject = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.txtelementid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTrigername = new System.Windows.Forms.TextBox();
            this.txtsrno = new System.Windows.Forms.TextBox();
            this.BtClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.Heading = new System.Windows.Forms.Label();
            this.Headpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTrigger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Headpanel
            // 
            this.Headpanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Headpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Headpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Headpanel.Controls.Add(this.btnDelete);
            this.Headpanel.Controls.Add(this.btnUpdate);
            this.Headpanel.Controls.Add(this.srnotxt);
            this.Headpanel.Controls.Add(this.label9);
            this.Headpanel.Controls.Add(this.btnAdd);
            this.Headpanel.Controls.Add(this.txtIntroTime);
            this.Headpanel.Controls.Add(this.label8);
            this.Headpanel.Controls.Add(this.pictureBox2);
            this.Headpanel.Controls.Add(this.BtnLoad);
            this.Headpanel.Controls.Add(this.DgvTrigger);
            this.Headpanel.Controls.Add(this.cbType);
            this.Headpanel.Controls.Add(this.label7);
            this.Headpanel.Controls.Add(this.cmbEvent);
            this.Headpanel.Controls.Add(this.cmbGfxObject);
            this.Headpanel.Controls.Add(this.label6);
            this.Headpanel.Controls.Add(this.label5);
            this.Headpanel.Controls.Add(this.txtPropertyValue);
            this.Headpanel.Controls.Add(this.label4);
            this.Headpanel.Controls.Add(this.txtPropertyName);
            this.Headpanel.Controls.Add(this.txtelementid);
            this.Headpanel.Controls.Add(this.label3);
            this.Headpanel.Controls.Add(this.txtTrigername);
            this.Headpanel.Controls.Add(this.txtsrno);
            this.Headpanel.Controls.Add(this.BtClose);
            this.Headpanel.Controls.Add(this.pictureBox1);
            this.Headpanel.Controls.Add(this.label2);
            this.Headpanel.Controls.Add(this.label1);
            this.Headpanel.Controls.Add(this.BtnSave);
            this.Headpanel.Controls.Add(this.Heading);
            this.Headpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Headpanel.ForeColor = System.Drawing.Color.White;
            this.Headpanel.Location = new System.Drawing.Point(0, 0);
            this.Headpanel.Name = "Headpanel";
            this.Headpanel.Size = new System.Drawing.Size(815, 461);
            this.Headpanel.TabIndex = 6;
            this.Headpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Headpanel_MouseMove_1);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnDelete.ForeColor = System.Drawing.Color.Azure;
            this.btnDelete.Location = new System.Drawing.Point(474, 204);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnUpdate.ForeColor = System.Drawing.Color.Azure;
            this.btnUpdate.Location = new System.Drawing.Point(390, 204);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(78, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // srnotxt
            // 
            this.srnotxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.srnotxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.srnotxt.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.srnotxt.ForeColor = System.Drawing.Color.White;
            this.srnotxt.Location = new System.Drawing.Point(123, 80);
            this.srnotxt.Name = "srnotxt";
            this.srnotxt.Size = new System.Drawing.Size(197, 25);
            this.srnotxt.TabIndex = 0;
            this.srnotxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.srnotxt_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(10, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 20);
            this.label9.TabIndex = 110;
            this.label9.Text = "SrNo";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnAdd.ForeColor = System.Drawing.Color.Azure;
            this.btnAdd.Location = new System.Drawing.Point(306, 204);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtIntroTime
            // 
            this.txtIntroTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtIntroTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIntroTime.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtIntroTime.ForeColor = System.Drawing.Color.White;
            this.txtIntroTime.Location = new System.Drawing.Point(559, 78);
            this.txtIntroTime.Name = "txtIntroTime";
            this.txtIntroTime.Size = new System.Drawing.Size(197, 25);
            this.txtIntroTime.TabIndex = 4;
            this.txtIntroTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.srnotxt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(439, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 107;
            this.label8.Text = "Intro Time";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(2, 69);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(878, 1);
            this.pictureBox2.TabIndex = 106;
            this.pictureBox2.TabStop = false;
            // 
            // BtnLoad
            // 
            this.BtnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.BtnLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnLoad.FlatAppearance.BorderSize = 0;
            this.BtnLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtnLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BtnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoad.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.BtnLoad.ForeColor = System.Drawing.Color.Azure;
            this.BtnLoad.Location = new System.Drawing.Point(439, 40);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(78, 23);
            this.BtnLoad.TabIndex = 105;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = false;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // DgvTrigger
            // 
            this.DgvTrigger.AllowUserToAddRows = false;
            this.DgvTrigger.AllowUserToDeleteRows = false;
            this.DgvTrigger.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTrigger.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvTrigger.ColumnHeadersHeight = 30;
            this.DgvTrigger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DgvTrigger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSrNO,
            this.colElementid,
            this.colPropertyname,
            this.colPropertyvalue,
            this.colGfxObject,
            this.colevent,
            this.colType,
            this.colIntroTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvTrigger.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvTrigger.EnableHeadersVisualStyles = false;
            this.DgvTrigger.Location = new System.Drawing.Point(6, 231);
            this.DgvTrigger.MultiSelect = false;
            this.DgvTrigger.Name = "DgvTrigger";
            this.DgvTrigger.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTrigger.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvTrigger.RowHeadersVisible = false;
            this.DgvTrigger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTrigger.Size = new System.Drawing.Size(804, 223);
            this.DgvTrigger.TabIndex = 104;
            this.DgvTrigger.DoubleClick += new System.EventHandler(this.DgvTrigger_DoubleClick);
            // 
            // colSrNO
            // 
            this.colSrNO.DataPropertyName = "srno";
            this.colSrNO.HeaderText = "Sr No";
            this.colSrNO.Name = "colSrNO";
            this.colSrNO.ReadOnly = true;
            // 
            // colElementid
            // 
            this.colElementid.DataPropertyName = "ItemId";
            this.colElementid.HeaderText = "Element ID";
            this.colElementid.Name = "colElementid";
            this.colElementid.ReadOnly = true;
            // 
            // colPropertyname
            // 
            this.colPropertyname.DataPropertyName = "PropertyName";
            this.colPropertyname.HeaderText = "Property Name";
            this.colPropertyname.Name = "colPropertyname";
            this.colPropertyname.ReadOnly = true;
            // 
            // colPropertyvalue
            // 
            this.colPropertyvalue.DataPropertyName = "PropertyValue";
            this.colPropertyvalue.HeaderText = "Property Value";
            this.colPropertyvalue.Name = "colPropertyvalue";
            this.colPropertyvalue.ReadOnly = true;
            // 
            // colGfxObject
            // 
            this.colGfxObject.DataPropertyName = "GfxObject";
            this.colGfxObject.HeaderText = "Graphics Object";
            this.colGfxObject.Name = "colGfxObject";
            this.colGfxObject.ReadOnly = true;
            // 
            // colevent
            // 
            this.colevent.DataPropertyName = "Event";
            this.colevent.HeaderText = "Event";
            this.colevent.Name = "colevent";
            this.colevent.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            // 
            // colIntroTime
            // 
            this.colIntroTime.DataPropertyName = "ChangeTime";
            this.colIntroTime.HeaderText = "Intro Time";
            this.colIntroTime.Name = "colIntroTime";
            this.colIntroTime.ReadOnly = true;
            // 
            // cbType
            // 
            this.cbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbType.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cbType.ForeColor = System.Drawing.Color.White;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "STATIC"});
            this.cbType.Location = new System.Drawing.Point(559, 165);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(197, 25);
            this.cbType.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(439, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 102;
            this.label7.Text = "Type";
            // 
            // cmbEvent
            // 
            this.cmbEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEvent.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbEvent.ForeColor = System.Drawing.Color.White;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Items.AddRange(new object[] {
            "START",
            "STOP",
            "AFTERSTART",
            "AFTERSTOP"});
            this.cmbEvent.Location = new System.Drawing.Point(559, 136);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(197, 25);
            this.cmbEvent.TabIndex = 6;
            // 
            // cmbGfxObject
            // 
            this.cmbGfxObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cmbGfxObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGfxObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGfxObject.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbGfxObject.ForeColor = System.Drawing.Color.White;
            this.cmbGfxObject.FormattingEnabled = true;
            this.cmbGfxObject.Items.AddRange(new object[] {
            "DEFAULT",
            "LOGO",
            "SQUEEZEE"});
            this.cmbGfxObject.Location = new System.Drawing.Point(559, 107);
            this.cmbGfxObject.Name = "cmbGfxObject";
            this.cmbGfxObject.Size = new System.Drawing.Size(197, 25);
            this.cmbGfxObject.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(439, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 20);
            this.label6.TabIndex = 98;
            this.label6.Text = "Event";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(439, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 96;
            this.label5.Text = "Graphics Object";
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtPropertyValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPropertyValue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPropertyValue.ForeColor = System.Drawing.Color.White;
            this.txtPropertyValue.Location = new System.Drawing.Point(123, 164);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(197, 25);
            this.txtPropertyValue.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(10, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 94;
            this.label4.Text = "Property Value";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtPropertyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPropertyName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPropertyName.ForeColor = System.Drawing.Color.White;
            this.txtPropertyName.Location = new System.Drawing.Point(123, 136);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(197, 25);
            this.txtPropertyName.TabIndex = 2;
            // 
            // txtelementid
            // 
            this.txtelementid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtelementid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtelementid.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtelementid.ForeColor = System.Drawing.Color.White;
            this.txtelementid.Location = new System.Drawing.Point(123, 108);
            this.txtelementid.Name = "txtelementid";
            this.txtelementid.Size = new System.Drawing.Size(197, 25);
            this.txtelementid.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 91;
            this.label3.Text = "Element Id";
            // 
            // txtTrigername
            // 
            this.txtTrigername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtTrigername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrigername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrigername.ForeColor = System.Drawing.Color.White;
            this.txtTrigername.Location = new System.Drawing.Point(266, 39);
            this.txtTrigername.Multiline = true;
            this.txtTrigername.Name = "txtTrigername";
            this.txtTrigername.Size = new System.Drawing.Size(165, 24);
            this.txtTrigername.TabIndex = 90;
            // 
            // txtsrno
            // 
            this.txtsrno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsrno.Location = new System.Drawing.Point(-141, -168);
            this.txtsrno.Name = "txtsrno";
            this.txtsrno.Size = new System.Drawing.Size(113, 20);
            this.txtsrno.TabIndex = 89;
            // 
            // BtClose
            // 
            this.BtClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtClose.FlatAppearance.BorderSize = 0;
            this.BtClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.BtClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Salmon;
            this.BtClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtClose.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold);
            this.BtClose.ForeColor = System.Drawing.Color.White;
            this.BtClose.Location = new System.Drawing.Point(775, 2);
            this.BtClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtClose.Name = "BtClose";
            this.BtClose.Size = new System.Drawing.Size(29, 28);
            this.BtClose.TabIndex = 88;
            this.BtClose.Text = "X";
            this.BtClose.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BtClose.UseVisualStyleBackColor = true;
            this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(0, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(878, 1);
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Property Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(155, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Trigger Name";
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.BtnSave.ForeColor = System.Drawing.Color.Azure;
            this.BtnSave.Location = new System.Drawing.Point(529, 40);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(115, 23);
            this.BtnSave.TabIndex = 50;
            this.BtnSave.Text = "SAVE";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Heading
            // 
            this.Heading.AutoSize = true;
            this.Heading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Heading.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Heading.ForeColor = System.Drawing.Color.Azure;
            this.Heading.Location = new System.Drawing.Point(3, 9);
            this.Heading.Name = "Heading";
            this.Heading.Size = new System.Drawing.Size(63, 21);
            this.Heading.TabIndex = 1;
            this.Heading.Text = "Trigger";
            this.Heading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 461);
            this.Controls.Add(this.Headpanel);
            this.Name = "frmTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTrigger";
            this.Load += new System.EventHandler(this.frmTrigger_Load);
            this.Headpanel.ResumeLayout(false);
            this.Headpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTrigger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Headpanel;
        private System.Windows.Forms.Button BtClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label Heading;
        private System.Windows.Forms.TextBox txtsrno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPropertyValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.TextBox txtelementid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrigername;
        private System.Windows.Forms.ComboBox cmbEvent;
        private System.Windows.Forms.ComboBox cmbGfxObject;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.DataGridView DgvTrigger;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtIntroTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox srnotxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElementid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyvalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGfxObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colevent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntroTime;
    }
}