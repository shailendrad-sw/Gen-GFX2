namespace graphicserver
{
    partial class FrmMainServer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainServer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ll_Eventlog = new System.Windows.Forms.LinkLabel();
            this.ll_Errorlog = new System.Windows.Forms.LinkLabel();
            this.Maincontainer = new System.Windows.Forms.TableLayoutPanel();
            this.EASPanel = new System.Windows.Forms.Panel();
            this.Lblband = new System.Windows.Forms.Label();
            this.BandWidthTrack = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.TickerFont = new System.Windows.Forms.Button();
            this.EASScrollbar = new System.Windows.Forms.VScrollBar();
            this.EASStart = new System.Windows.Forms.Button();
            this.EASText = new System.Windows.Forms.TextBox();
            this.EASCheck = new System.Windows.Forms.CheckBox();
            this.dgvprievent = new System.Windows.Forms.DataGridView();
            this.priEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priChID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priEventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priInsertDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_strip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualGraphicsLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_contentList = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideEventLogsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showHidePreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secGridConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggerSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keryerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.channelBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvsecevent = new System.Windows.Forms.DataGridView();
            this.lv_eventlog = new System.Windows.Forms.ListView();
            this.eventtime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventDetail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Primarypanel = new System.Windows.Forms.Panel();
            this.LTCtimelbl = new System.Windows.Forms.Label();
            this.ChannelTypelbl = new System.Windows.Forms.Label();
            this.ChannelTypeLabel = new System.Windows.Forms.Label();
            this.LTCtimelabel = new System.Windows.Forms.Label();
            this.panelpreview = new System.Windows.Forms.Panel();
            this.lblpreviewmessage = new System.Windows.Forms.Label();
            this.pnlScte = new System.Windows.Forms.Panel();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChannelConfigStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSecOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.killTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbluptime = new System.Windows.Forms.Label();
            this.lblLicenseText = new System.Windows.Forms.Label();
            this.overlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Maincontainer.SuspendLayout();
            this.EASPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BandWidthTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprievent)).BeginInit();
            this.menu_strip_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsecevent)).BeginInit();
            this.Primarypanel.SuspendLayout();
            this.panelpreview.SuspendLayout();
            this.cmsSecOption.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ll_Eventlog
            // 
            this.ll_Eventlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ll_Eventlog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ll_Eventlog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_Eventlog.LinkColor = System.Drawing.Color.White;
            this.ll_Eventlog.Location = new System.Drawing.Point(5, 649);
            this.ll_Eventlog.Name = "ll_Eventlog";
            this.ll_Eventlog.Size = new System.Drawing.Size(82, 21);
            this.ll_Eventlog.TabIndex = 44;
            this.ll_Eventlog.TabStop = true;
            this.ll_Eventlog.Text = "Event logs";
            this.ll_Eventlog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ll_Eventlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Eventlog_LinkClicked);
            // 
            // ll_Errorlog
            // 
            this.ll_Errorlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ll_Errorlog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ll_Errorlog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_Errorlog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ll_Errorlog.LinkColor = System.Drawing.Color.White;
            this.ll_Errorlog.Location = new System.Drawing.Point(93, 649);
            this.ll_Errorlog.Name = "ll_Errorlog";
            this.ll_Errorlog.Size = new System.Drawing.Size(90, 21);
            this.ll_Errorlog.TabIndex = 45;
            this.ll_Errorlog.TabStop = true;
            this.ll_Errorlog.Text = "Error logs";
            this.ll_Errorlog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ll_Errorlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Errorlog_LinkClicked);
            // 
            // Maincontainer
            // 
            this.Maincontainer.ColumnCount = 3;
            this.Maincontainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Maincontainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.Maincontainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Maincontainer.Controls.Add(this.EASPanel, 2, 3);
            this.Maincontainer.Controls.Add(this.dgvprievent, 0, 1);
            this.Maincontainer.Controls.Add(this.menu_strip_main, 0, 0);
            this.Maincontainer.Controls.Add(this.dgvsecevent, 0, 4);
            this.Maincontainer.Controls.Add(this.lv_eventlog, 0, 4);
            this.Maincontainer.Controls.Add(this.Primarypanel, 0, 3);
            this.Maincontainer.Controls.Add(this.panelpreview, 2, 1);
            this.Maincontainer.Controls.Add(this.pnlScte, 0, 2);
            this.Maincontainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.Maincontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Maincontainer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Maincontainer.Location = new System.Drawing.Point(0, 0);
            this.Maincontainer.Name = "Maincontainer";
            this.Maincontainer.RowCount = 4;
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.92093F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.833F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.72962F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.51208F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Maincontainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Maincontainer.Size = new System.Drawing.Size(905, 640);
            this.Maincontainer.TabIndex = 46;
            this.Maincontainer.Paint += new System.Windows.Forms.PaintEventHandler(this.Maincontainer_Paint);
            // 
            // EASPanel
            // 
            this.EASPanel.Controls.Add(this.Lblband);
            this.EASPanel.Controls.Add(this.BandWidthTrack);
            this.EASPanel.Controls.Add(this.label1);
            this.EASPanel.Controls.Add(this.TickerFont);
            this.EASPanel.Controls.Add(this.EASScrollbar);
            this.EASPanel.Controls.Add(this.EASStart);
            this.EASPanel.Controls.Add(this.EASText);
            this.EASPanel.Controls.Add(this.EASCheck);
            this.EASPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EASPanel.Location = new System.Drawing.Point(513, 266);
            this.EASPanel.Name = "EASPanel";
            this.EASPanel.Size = new System.Drawing.Size(389, 51);
            this.EASPanel.TabIndex = 47;
            // 
            // Lblband
            // 
            this.Lblband.AutoSize = true;
            this.Lblband.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Lblband.Location = new System.Drawing.Point(200, 35);
            this.Lblband.Name = "Lblband";
            this.Lblband.Size = new System.Drawing.Size(70, 13);
            this.Lblband.TabIndex = 58;
            this.Lblband.Text = "Band width";
            // 
            // BandWidthTrack
            // 
            this.BandWidthTrack.LargeChange = 1;
            this.BandWidthTrack.Location = new System.Drawing.Point(270, 33);
            this.BandWidthTrack.Name = "BandWidthTrack";
            this.BandWidthTrack.Size = new System.Drawing.Size(102, 45);
            this.BandWidthTrack.TabIndex = 57;
            this.BandWidthTrack.ValueChanged += new System.EventHandler(this.BandWidthTrack_ValueChanged);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(-1, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "Font configuration";
            // 
            // TickerFont
            // 
            this.TickerFont.BackColor = System.Drawing.Color.DimGray;
            this.TickerFont.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TickerFont.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TickerFont.Location = new System.Drawing.Point(112, 32);
            this.TickerFont.Name = "TickerFont";
            this.TickerFont.Size = new System.Drawing.Size(59, 23);
            this.TickerFont.TabIndex = 55;
            this.TickerFont.Text = "Browse font";
            this.TickerFont.UseVisualStyleBackColor = false;
            this.TickerFont.Click += new System.EventHandler(this.TickerFont_Click);
            // 
            // EASScrollbar
            // 
            this.EASScrollbar.LargeChange = 1;
            this.EASScrollbar.Location = new System.Drawing.Point(375, -5);
            this.EASScrollbar.Maximum = 10;
            this.EASScrollbar.Name = "EASScrollbar";
            this.EASScrollbar.Size = new System.Drawing.Size(17, 61);
            this.EASScrollbar.TabIndex = 54;
            this.EASScrollbar.Value = 10;
            this.EASScrollbar.ValueChanged += new System.EventHandler(this.EASScrollbar_ValueChanged);
            // 
            // EASStart
            // 
            this.EASStart.BackColor = System.Drawing.Color.DimGray;
            this.EASStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EASStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EASStart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EASStart.Location = new System.Drawing.Point(305, 4);
            this.EASStart.Name = "EASStart";
            this.EASStart.Size = new System.Drawing.Size(63, 24);
            this.EASStart.TabIndex = 51;
            this.EASStart.Text = "Start";
            this.EASStart.UseVisualStyleBackColor = false;
            this.EASStart.Click += new System.EventHandler(this.EASStart_Click);
            // 
            // EASText
            // 
            this.EASText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EASText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EASText.ForeColor = System.Drawing.Color.White;
            this.EASText.Location = new System.Drawing.Point(43, 4);
            this.EASText.Multiline = true;
            this.EASText.Name = "EASText";
            this.EASText.Size = new System.Drawing.Size(245, 22);
            this.EASText.TabIndex = 53;
            // 
            // EASCheck
            // 
            this.EASCheck.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.EASCheck.Location = new System.Drawing.Point(0, -1);
            this.EASCheck.Name = "EASCheck";
            this.EASCheck.Size = new System.Drawing.Size(49, 31);
            this.EASCheck.TabIndex = 52;
            this.EASCheck.Text = "EAS";
            this.EASCheck.UseVisualStyleBackColor = true;
            // 
            // dgvprievent
            // 
            this.dgvprievent.AllowUserToAddRows = false;
            this.dgvprievent.AllowUserToDeleteRows = false;
            this.dgvprievent.AllowUserToResizeColumns = false;
            this.dgvprievent.AllowUserToResizeRows = false;
            this.dgvprievent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvprievent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.dgvprievent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvprievent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvprievent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvprievent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvprievent.ColumnHeadersHeight = 30;
            this.dgvprievent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvprievent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.priEvent,
            this.priChID,
            this.priEventID,
            this.priEventTime,
            this.priDuration,
            this.priInsertDate});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvprievent.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvprievent.EnableHeadersVisualStyles = false;
            this.dgvprievent.Location = new System.Drawing.Point(0, 40);
            this.dgvprievent.Margin = new System.Windows.Forms.Padding(0);
            this.dgvprievent.MultiSelect = false;
            this.dgvprievent.Name = "dgvprievent";
            this.dgvprievent.ReadOnly = true;
            this.dgvprievent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvprievent.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvprievent.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgvprievent.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvprievent.RowTemplate.Height = 30;
            this.dgvprievent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvprievent.Size = new System.Drawing.Size(510, 72);
            this.dgvprievent.TabIndex = 44;
            // 
            // priEvent
            // 
            this.priEvent.HeaderText = "Event";
            this.priEvent.Name = "priEvent";
            this.priEvent.ReadOnly = true;
            this.priEvent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priChID
            // 
            this.priChID.HeaderText = "ChannelID";
            this.priChID.Name = "priChID";
            this.priChID.ReadOnly = true;
            this.priChID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priEventID
            // 
            this.priEventID.HeaderText = "EventID";
            this.priEventID.Name = "priEventID";
            this.priEventID.ReadOnly = true;
            this.priEventID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priEventTime
            // 
            this.priEventTime.HeaderText = "EventTime";
            this.priEventTime.Name = "priEventTime";
            this.priEventTime.ReadOnly = true;
            this.priEventTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priDuration
            // 
            this.priDuration.HeaderText = "Duration";
            this.priDuration.Name = "priDuration";
            this.priDuration.ReadOnly = true;
            this.priDuration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priInsertDate
            // 
            this.priInsertDate.HeaderText = "InsertDate";
            this.priInsertDate.Name = "priInsertDate";
            this.priInsertDate.ReadOnly = true;
            this.priInsertDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menu_strip_main
            // 
            this.menu_strip_main.AutoSize = false;
            this.menu_strip_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Maincontainer.SetColumnSpan(this.menu_strip_main, 3);
            this.menu_strip_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu_strip_main.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_strip_main.GripMargin = new System.Windows.Forms.Padding(0);
            this.menu_strip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.backupRestoreToolStripMenuItem,
            this.channelBugToolStripMenuItem});
            this.menu_strip_main.Location = new System.Drawing.Point(0, 0);
            this.menu_strip_main.Name = "menu_strip_main";
            this.menu_strip_main.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menu_strip_main.Size = new System.Drawing.Size(905, 40);
            this.menu_strip_main.TabIndex = 6;
            this.menu_strip_main.Text = "Menu";
            this.menu_strip_main.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_strip_main_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshConfigToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 36);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.MouseEnter += new System.EventHandler(this.fileToolStripMenuItem_MouseEnter);
            this.fileToolStripMenuItem.MouseLeave += new System.EventHandler(this.fileToolStripMenuItem_MouseLeave);
            // 
            // refreshConfigToolStripMenuItem
            // 
            this.refreshConfigToolStripMenuItem.Image = global::graphicserver.Properties.Resources.refresh;
            this.refreshConfigToolStripMenuItem.Name = "refreshConfigToolStripMenuItem";
            this.refreshConfigToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.refreshConfigToolStripMenuItem.Text = "Refresh Config";
            this.refreshConfigToolStripMenuItem.Click += new System.EventHandler(this.refreshConfigToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderToolStripMenuItem,
            this.renderOutToolStripMenuItem,
            this.manualGraphicsLoadToolStripMenuItem,
            this.menu_contentList});
            this.toolToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.toolToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("toolToolStripMenuItem.Image")));
            this.toolToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(61, 36);
            this.toolToolStripMenuItem.Text = "Tool";
            this.toolToolStripMenuItem.MouseEnter += new System.EventHandler(this.toolToolStripMenuItem_MouseEnter);
            this.toolToolStripMenuItem.MouseLeave += new System.EventHandler(this.toolToolStripMenuItem_MouseLeave);
            // 
            // renderToolStripMenuItem
            // 
            this.renderToolStripMenuItem.Image = global::graphicserver.Properties.Resources._in;
            this.renderToolStripMenuItem.Name = "renderToolStripMenuItem";
            this.renderToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.renderToolStripMenuItem.Text = "Set Render";
            this.renderToolStripMenuItem.Click += new System.EventHandler(this.renderToolStripMenuItem_Click);
            // 
            // renderOutToolStripMenuItem
            // 
            this.renderOutToolStripMenuItem.Image = global::graphicserver.Properties.Resources._out;
            this.renderOutToolStripMenuItem.Name = "renderOutToolStripMenuItem";
            this.renderOutToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.renderOutToolStripMenuItem.Text = "Render Out";
            this.renderOutToolStripMenuItem.Click += new System.EventHandler(this.renderOutToolStripMenuItem_Click);
            // 
            // manualGraphicsLoadToolStripMenuItem
            // 
            this.manualGraphicsLoadToolStripMenuItem.Image = global::graphicserver.Properties.Resources.manualload;
            this.manualGraphicsLoadToolStripMenuItem.Name = "manualGraphicsLoadToolStripMenuItem";
            this.manualGraphicsLoadToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.manualGraphicsLoadToolStripMenuItem.Text = "Manual Graphics Load";
            this.manualGraphicsLoadToolStripMenuItem.Click += new System.EventHandler(this.manualGraphicsLoadToolStripMenuItem_Click);
            // 
            // menu_contentList
            // 
            this.menu_contentList.Name = "menu_contentList";
            this.menu_contentList.Size = new System.Drawing.Size(207, 22);
            this.menu_contentList.Text = "Content List";
            this.menu_contentList.Visible = false;
            this.menu_contentList.Click += new System.EventHandler(this.menu_contentList_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideEventLogsToolStripMenuItem1,
            this.showHidePreviewToolStripMenuItem,
            this.overlayToolStripMenuItem});
            this.viewToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.viewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripMenuItem.Image")));
            this.viewToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(63, 36);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.MouseEnter += new System.EventHandler(this.viewToolStripMenuItem_MouseEnter);
            this.viewToolStripMenuItem.MouseLeave += new System.EventHandler(this.viewToolStripMenuItem_MouseLeave);
            // 
            // showHideEventLogsToolStripMenuItem1
            // 
            this.showHideEventLogsToolStripMenuItem1.Image = global::graphicserver.Properties.Resources.hide;
            this.showHideEventLogsToolStripMenuItem1.Name = "showHideEventLogsToolStripMenuItem1";
            this.showHideEventLogsToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.showHideEventLogsToolStripMenuItem1.Text = "Show/Hide Event Logs";
            this.showHideEventLogsToolStripMenuItem1.Visible = false;
            this.showHideEventLogsToolStripMenuItem1.Click += new System.EventHandler(this.showHideEventLogsToolStripMenuItem1_Click);
            // 
            // showHidePreviewToolStripMenuItem
            // 
            this.showHidePreviewToolStripMenuItem.Image = global::graphicserver.Properties.Resources.hide;
            this.showHidePreviewToolStripMenuItem.Name = "showHidePreviewToolStripMenuItem";
            this.showHidePreviewToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.showHidePreviewToolStripMenuItem.Text = "Show/Hide Preview";
            this.showHidePreviewToolStripMenuItem.Click += new System.EventHandler(this.showHidePreviewToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutUsToolStripMenuItem,
            this.userManualToolStripMenu});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
            this.helpToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(63, 36);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.MouseEnter += new System.EventHandler(this.helpToolStripMenuItem_MouseEnter);
            this.helpToolStripMenuItem.MouseLeave += new System.EventHandler(this.helpToolStripMenuItem_MouseLeave);
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Image = global::graphicserver.Properties.Resources.about;
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.aboutUsToolStripMenuItem.Text = "About Us";
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // userManualToolStripMenu
            // 
            this.userManualToolStripMenu.Image = global::graphicserver.Properties.Resources.about;
            this.userManualToolStripMenu.Name = "userManualToolStripMenu";
            this.userManualToolStripMenu.Size = new System.Drawing.Size(150, 22);
            this.userManualToolStripMenu.Text = "User Manual";
            this.userManualToolStripMenu.Click += new System.EventHandler(this.UserManualToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelConfigToolStripMenuItem,
            this.secGridConfigToolStripMenuItem,
            this.templateConfigToolStripMenuItem,
            this.triggerSettingToolStripMenuItem,
            this.keryerToolStripMenuItem});
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.settingsToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings;
            this.settingsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(82, 36);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.MouseEnter += new System.EventHandler(this.settingsToolStripMenuItem_MouseEnter);
            this.settingsToolStripMenuItem.MouseLeave += new System.EventHandler(this.settingsToolStripMenuItem_MouseLeave);
            // 
            // channelConfigToolStripMenuItem
            // 
            this.channelConfigToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.channelConfigToolStripMenuItem.Name = "channelConfigToolStripMenuItem";
            this.channelConfigToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.channelConfigToolStripMenuItem.Text = "Channel Config";
            this.channelConfigToolStripMenuItem.Click += new System.EventHandler(this.ChannelConfigStripMenuItem_Click);
            // 
            // secGridConfigToolStripMenuItem
            // 
            this.secGridConfigToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.secGridConfigToolStripMenuItem.Name = "secGridConfigToolStripMenuItem";
            this.secGridConfigToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.secGridConfigToolStripMenuItem.Text = "Grid Config";
            this.secGridConfigToolStripMenuItem.Click += new System.EventHandler(this.secGridConfigToolStripMenuItem_Click);
            // 
            // templateConfigToolStripMenuItem
            // 
            this.templateConfigToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.templateConfigToolStripMenuItem.Name = "templateConfigToolStripMenuItem";
            this.templateConfigToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.templateConfigToolStripMenuItem.Text = "Template Config";
            this.templateConfigToolStripMenuItem.Click += new System.EventHandler(this.templateConfigToolStripMenuItem_Click);
            // 
            // triggerSettingToolStripMenuItem
            // 
            this.triggerSettingToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.triggerSettingToolStripMenuItem.Name = "triggerSettingToolStripMenuItem";
            this.triggerSettingToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.triggerSettingToolStripMenuItem.Text = "Trigger Setting";
            this.triggerSettingToolStripMenuItem.Click += new System.EventHandler(this.triggerSettingToolStripMenuItem_Click);
            // 
            // keryerToolStripMenuItem
            // 
            this.keryerToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.keryerToolStripMenuItem.Name = "keryerToolStripMenuItem";
            this.keryerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.keryerToolStripMenuItem.Text = "Keyer";
            this.keryerToolStripMenuItem.Click += new System.EventHandler(this.keryerToolStripMenuItem_Click);
            // 
            // backupRestoreToolStripMenuItem
            // 
            this.backupRestoreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.Restore});
            this.backupRestoreToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.backupRestoreToolStripMenuItem.Name = "backupRestoreToolStripMenuItem";
            this.backupRestoreToolStripMenuItem.Size = new System.Drawing.Size(97, 36);
            this.backupRestoreToolStripMenuItem.Text = "Backup&Restore";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // Restore
            // 
            this.Restore.Image = global::graphicserver.Properties.Resources.channelsettings1;
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(113, 22);
            this.Restore.Text = "Restore";
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // channelBugToolStripMenuItem
            // 
            this.channelBugToolStripMenuItem.ForeColor = System.Drawing.Color.Coral;
            this.channelBugToolStripMenuItem.Name = "channelBugToolStripMenuItem";
            this.channelBugToolStripMenuItem.Size = new System.Drawing.Size(84, 36);
            this.channelBugToolStripMenuItem.Text = "ChannelBug";
            this.channelBugToolStripMenuItem.Click += new System.EventHandler(this.channelBugToolStripMenuItem_Click);
            // 
            // dgvsecevent
            // 
            this.dgvsecevent.AllowUserToAddRows = false;
            this.dgvsecevent.AllowUserToResizeRows = false;
            this.dgvsecevent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.dgvsecevent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvsecevent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvsecevent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvsecevent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvsecevent.ColumnHeadersHeight = 25;
            this.dgvsecevent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Maincontainer.SetColumnSpan(this.dgvsecevent, 3);
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvsecevent.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvsecevent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsecevent.EnableHeadersVisualStyles = false;
            this.dgvsecevent.Location = new System.Drawing.Point(6, 326);
            this.dgvsecevent.Margin = new System.Windows.Forms.Padding(6);
            this.dgvsecevent.Name = "dgvsecevent";
            this.dgvsecevent.ReadOnly = true;
            this.dgvsecevent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(59)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvsecevent.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvsecevent.RowHeadersVisible = false;
            this.dgvsecevent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Azure;
            this.dgvsecevent.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvsecevent.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Light", 8.25F);
            this.dgvsecevent.RowTemplate.Height = 26;
            this.dgvsecevent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvsecevent.Size = new System.Drawing.Size(893, 196);
            this.dgvsecevent.TabIndex = 42;
            this.dgvsecevent.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvsecevent_DataError);
            this.dgvsecevent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvsecevent_MouseDown);
            // 
            // lv_eventlog
            // 
            this.lv_eventlog.AutoArrange = false;
            this.lv_eventlog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lv_eventlog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eventtime,
            this.eventDetail});
            this.Maincontainer.SetColumnSpan(this.lv_eventlog, 3);
            this.lv_eventlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_eventlog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_eventlog.ForeColor = System.Drawing.Color.Bisque;
            this.lv_eventlog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lv_eventlog.HideSelection = false;
            this.lv_eventlog.Location = new System.Drawing.Point(6, 534);
            this.lv_eventlog.Margin = new System.Windows.Forms.Padding(6);
            this.lv_eventlog.MultiSelect = false;
            this.lv_eventlog.Name = "lv_eventlog";
            this.Maincontainer.SetRowSpan(this.lv_eventlog, 3);
            this.lv_eventlog.Size = new System.Drawing.Size(893, 100);
            this.lv_eventlog.TabIndex = 41;
            this.lv_eventlog.UseCompatibleStateImageBehavior = false;
            this.lv_eventlog.View = System.Windows.Forms.View.Details;
            this.lv_eventlog.Visible = false;
            // 
            // eventtime
            // 
            this.eventtime.Text = "Event Time";
            this.eventtime.Width = 180;
            // 
            // eventDetail
            // 
            this.eventDetail.Text = "Event Detail";
            this.eventDetail.Width = 1100;
            // 
            // Primarypanel
            // 
            this.Primarypanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Primarypanel.Controls.Add(this.LTCtimelbl);
            this.Primarypanel.Controls.Add(this.ChannelTypelbl);
            this.Primarypanel.Controls.Add(this.ChannelTypeLabel);
            this.Primarypanel.Controls.Add(this.LTCtimelabel);
            this.Primarypanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Primarypanel.Location = new System.Drawing.Point(6, 269);
            this.Primarypanel.Margin = new System.Windows.Forms.Padding(6);
            this.Primarypanel.Name = "Primarypanel";
            this.Primarypanel.Size = new System.Drawing.Size(498, 45);
            this.Primarypanel.TabIndex = 43;
            // 
            // LTCtimelbl
            // 
            this.LTCtimelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LTCtimelbl.Font = new System.Drawing.Font("Segoe UI", 15.25F);
            this.LTCtimelbl.ForeColor = System.Drawing.Color.Gainsboro;
            this.LTCtimelbl.Location = new System.Drawing.Point(44, 1);
            this.LTCtimelbl.Name = "LTCtimelbl";
            this.LTCtimelbl.Size = new System.Drawing.Size(121, 30);
            this.LTCtimelbl.TabIndex = 50;
            this.LTCtimelbl.Text = "00:00:00:00";
            // 
            // ChannelTypelbl
            // 
            this.ChannelTypelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelTypelbl.Font = new System.Drawing.Font("Segoe UI", 15.25F);
            this.ChannelTypelbl.ForeColor = System.Drawing.Color.Gainsboro;
            this.ChannelTypelbl.Location = new System.Drawing.Point(428, 1);
            this.ChannelTypelbl.Name = "ChannelTypelbl";
            this.ChannelTypelbl.Size = new System.Drawing.Size(43, 30);
            this.ChannelTypelbl.TabIndex = 49;
            this.ChannelTypelbl.Text = "HD";
            // 
            // ChannelTypeLabel
            // 
            this.ChannelTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelTypeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ChannelTypeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChannelTypeLabel.Font = new System.Drawing.Font("Segoe UI", 15.25F);
            this.ChannelTypeLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.ChannelTypeLabel.Location = new System.Drawing.Point(265, 1);
            this.ChannelTypeLabel.Name = "ChannelTypeLabel";
            this.ChannelTypeLabel.Size = new System.Drawing.Size(172, 30);
            this.ChannelTypeLabel.TabIndex = 48;
            this.ChannelTypeLabel.Text = "CHANNEL TYPE :";
            this.ChannelTypeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LTCtimelabel
            // 
            this.LTCtimelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LTCtimelabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.LTCtimelabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LTCtimelabel.Font = new System.Drawing.Font("Segoe UI", 15.25F);
            this.LTCtimelabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.LTCtimelabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LTCtimelabel.Location = new System.Drawing.Point(-4, 1);
            this.LTCtimelabel.Margin = new System.Windows.Forms.Padding(0);
            this.LTCtimelabel.Name = "LTCtimelabel";
            this.LTCtimelabel.Size = new System.Drawing.Size(58, 30);
            this.LTCtimelabel.TabIndex = 47;
            this.LTCtimelabel.Text = "LTC :";
            this.LTCtimelabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panelpreview
            // 
            this.panelpreview.BackColor = System.Drawing.Color.Black;
            this.panelpreview.Controls.Add(this.lblpreviewmessage);
            this.panelpreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelpreview.Location = new System.Drawing.Point(516, 46);
            this.panelpreview.Margin = new System.Windows.Forms.Padding(6);
            this.panelpreview.Name = "panelpreview";
            this.Maincontainer.SetRowSpan(this.panelpreview, 2);
            this.panelpreview.Size = new System.Drawing.Size(383, 211);
            this.panelpreview.TabIndex = 0;
            this.panelpreview.SizeChanged += new System.EventHandler(this.panelpreview_SizeChanged);
            // 
            // lblpreviewmessage
            // 
            this.lblpreviewmessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblpreviewmessage.AutoSize = true;
            this.lblpreviewmessage.BackColor = System.Drawing.Color.Transparent;
            this.lblpreviewmessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpreviewmessage.ForeColor = System.Drawing.Color.Coral;
            this.lblpreviewmessage.Location = new System.Drawing.Point(176, 254);
            this.lblpreviewmessage.Name = "lblpreviewmessage";
            this.lblpreviewmessage.Size = new System.Drawing.Size(156, 25);
            this.lblpreviewmessage.TabIndex = 0;
            this.lblpreviewmessage.Text = "Preview Disabled";
            this.lblpreviewmessage.Visible = false;
            // 
            // pnlScte
            // 
            this.pnlScte.BackColor = System.Drawing.Color.Transparent;
            this.pnlScte.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlScte.Location = new System.Drawing.Point(5, 147);
            this.pnlScte.Margin = new System.Windows.Forms.Padding(5);
            this.pnlScte.Name = "pnlScte";
            this.pnlScte.Size = new System.Drawing.Size(500, 111);
            this.pnlScte.TabIndex = 45;
            // 
            // SettingToolStripMenuItem
            // 
            this.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            this.SettingToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ChannelConfigStripMenuItem
            // 
            this.ChannelConfigStripMenuItem.Name = "ChannelConfigStripMenuItem";
            this.ChannelConfigStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // cmsSecOption
            // 
            this.cmsSecOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killTemplateToolStripMenuItem,
            this.removeTemplateToolStripMenuItem,
            this.hideTemplateToolStripMenuItem,
            this.showTemplateToolStripMenuItem,
            this.removeAllTemplatesToolStripMenuItem});
            this.cmsSecOption.Name = "cmsSecOption";
            this.cmsSecOption.Size = new System.Drawing.Size(235, 114);
            // 
            // killTemplateToolStripMenuItem
            // 
            this.killTemplateToolStripMenuItem.Image = global::graphicserver.Properties.Resources.close;
            this.killTemplateToolStripMenuItem.Name = "killTemplateToolStripMenuItem";
            this.killTemplateToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.killTemplateToolStripMenuItem.Text = "Kill Running Template";
            this.killTemplateToolStripMenuItem.Click += new System.EventHandler(this.killTemplateToolStripMenuItem_Click);
            // 
            // removeTemplateToolStripMenuItem
            // 
            this.removeTemplateToolStripMenuItem.Image = global::graphicserver.Properties.Resources.KillWaiting;
            this.removeTemplateToolStripMenuItem.Name = "removeTemplateToolStripMenuItem";
            this.removeTemplateToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.removeTemplateToolStripMenuItem.Text = "Remove Waiting Template";
            this.removeTemplateToolStripMenuItem.Click += new System.EventHandler(this.removeTemplateToolStripMenuItem_Click);
            // 
            // hideTemplateToolStripMenuItem
            // 
            this.hideTemplateToolStripMenuItem.Image = global::graphicserver.Properties.Resources.hide;
            this.hideTemplateToolStripMenuItem.Name = "hideTemplateToolStripMenuItem";
            this.hideTemplateToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.hideTemplateToolStripMenuItem.Text = "Hide Template";
            this.hideTemplateToolStripMenuItem.Click += new System.EventHandler(this.hideTemplateToolStripMenuItem_Click);
            // 
            // showTemplateToolStripMenuItem
            // 
            this.showTemplateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showTemplateToolStripMenuItem.Image")));
            this.showTemplateToolStripMenuItem.Name = "showTemplateToolStripMenuItem";
            this.showTemplateToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.showTemplateToolStripMenuItem.Text = "Show Template";
            this.showTemplateToolStripMenuItem.Click += new System.EventHandler(this.showTemplateToolStripMenuItem_Click);
            // 
            // removeAllTemplatesToolStripMenuItem
            // 
            this.removeAllTemplatesToolStripMenuItem.Image = global::graphicserver.Properties.Resources.close1;
            this.removeAllTemplatesToolStripMenuItem.Name = "removeAllTemplatesToolStripMenuItem";
            this.removeAllTemplatesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.removeAllTemplatesToolStripMenuItem.Text = "Remove All Waiting Templates";
            this.removeAllTemplatesToolStripMenuItem.Click += new System.EventHandler(this.removeAllTemplatesToolStripMenuItem_Click);
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCopyRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblCopyRight.CausesValidation = false;
            this.lblCopyRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCopyRight.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblCopyRight.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblCopyRight.Location = new System.Drawing.Point(174, 620);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(319, 26);
            this.lblCopyRight.TabIndex = 49;
            this.lblCopyRight.Text = "Planetcast Media Services";
            this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCopyRight.Click += new System.EventHandler(this.lblCopyRight_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Maincontainer);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 640);
            this.panel1.TabIndex = 50;
            // 
            // lbluptime
            // 
            this.lbluptime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbluptime.AutoSize = true;
            this.lbluptime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbluptime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbluptime.ForeColor = System.Drawing.Color.White;
            this.lbluptime.Location = new System.Drawing.Point(714, 653);
            this.lbluptime.Name = "lbluptime";
            this.lbluptime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbluptime.Size = new System.Drawing.Size(180, 13);
            this.lbluptime.TabIndex = 51;
            this.lbluptime.Text = "Up Time 10-11-2020 15:22:19";
            // 
            // lblLicenseText
            // 
            this.lblLicenseText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLicenseText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblLicenseText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLicenseText.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.lblLicenseText.ForeColor = System.Drawing.Color.White;
            this.lblLicenseText.Location = new System.Drawing.Point(458, 610);
            this.lblLicenseText.Name = "lblLicenseText";
            this.lblLicenseText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblLicenseText.Size = new System.Drawing.Size(229, 24);
            this.lblLicenseText.TabIndex = 52;
            this.lblLicenseText.Text = "License";
            this.lblLicenseText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLicenseText.Visible = false;
            this.lblLicenseText.Click += new System.EventHandler(this.lblLicenseText_Click);
            // 
            // overlayToolStripMenuItem
            // 
            this.overlayToolStripMenuItem.Name = "overlayToolStripMenuItem";
            this.overlayToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.overlayToolStripMenuItem.Text = "OverlayHTML";
            this.overlayToolStripMenuItem.Click += new System.EventHandler(this.overlayToolStripMenuItem_Click);
            // 
            // FrmMainServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(908, 678);
            this.Controls.Add(this.lblLicenseText);
            this.Controls.Add(this.lbluptime);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ll_Eventlog);
            this.Controls.Add(this.ll_Errorlog);
            this.Controls.Add(this.lblCopyRight);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMainServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MBIV Graphic Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMainServer_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Maincontainer.ResumeLayout(false);
            this.EASPanel.ResumeLayout(false);
            this.EASPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BandWidthTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprievent)).EndInit();
            this.menu_strip_main.ResumeLayout(false);
            this.menu_strip_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsecevent)).EndInit();
            this.Primarypanel.ResumeLayout(false);
            this.panelpreview.ResumeLayout(false);
            this.panelpreview.PerformLayout();
            this.cmsSecOption.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel ll_Eventlog;
        private System.Windows.Forms.LinkLabel ll_Errorlog;
        private System.Windows.Forms.TableLayoutPanel Maincontainer;
        private System.Windows.Forms.Label LTCtimelabel;
        private System.Windows.Forms.ContextMenuStrip cmsSecOption;
        private System.Windows.Forms.ToolStripMenuItem killTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllTemplatesToolStripMenuItem;
        private System.Windows.Forms.Label ChannelTypeLabel;
        public System.Windows.Forms.DataGridView dgvsecevent;
        private System.Windows.Forms.Panel Primarypanel;
        private System.Windows.Forms.ListView lv_eventlog;
        private System.Windows.Forms.ColumnHeader eventtime;
        private System.Windows.Forms.ColumnHeader eventDetail;
        private System.Windows.Forms.MenuStrip menu_strip_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualGraphicsLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideEventLogsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showHidePreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        //code added by anurag 26-11-19
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChannelConfigStripMenuItem;
        //code ended by anurag 26-11-19

        private System.Windows.Forms.Label ChannelTypelbl;
        private System.Windows.Forms.Label LTCtimelbl;
        private System.Windows.Forms.Panel panelpreview;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secGridConfigToolStripMenuItem;
        internal System.Windows.Forms.DataGridView dgvprievent;
        private System.Windows.Forms.DataGridViewTextBoxColumn priEvent;
        private System.Windows.Forms.DataGridViewTextBoxColumn priChID;
        private System.Windows.Forms.DataGridViewTextBoxColumn priEventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn priEventTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn priDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn priInsertDate;
        private System.Windows.Forms.Panel pnlScte;
        private System.Windows.Forms.Label lblpreviewmessage;
        private System.Windows.Forms.Label lblCopyRight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbluptime;
        private System.Windows.Forms.ToolStripMenuItem templateConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triggerSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keryerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_contentList;
        private System.Windows.Forms.Label lblLicenseText;
        private System.Windows.Forms.ToolStripMenuItem userManualToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem backupRestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Restore;
        private System.Windows.Forms.ToolStripMenuItem channelBugToolStripMenuItem;
        private System.Windows.Forms.Panel EASPanel;
        private System.Windows.Forms.Label Lblband;
        private System.Windows.Forms.TrackBar BandWidthTrack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TickerFont;
        private System.Windows.Forms.VScrollBar EASScrollbar;
        private System.Windows.Forms.Button EASStart;
        private System.Windows.Forms.TextBox EASText;
        private System.Windows.Forms.CheckBox EASCheck;
        private System.Windows.Forms.ToolStripMenuItem overlayToolStripMenuItem;
    }
}

