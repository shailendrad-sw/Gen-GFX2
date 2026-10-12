using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using MFORMATSLib;
using System.Text;
using graphicserver.Classes;

namespace graphicserver
{
    public partial class Graphicscontrol : UserControl
    {
        //  Action<string, string,int> action;
        FrmMainServer objmainserver;
        public TextBox txtbox;
        private TextBox txtbox1, txtbox2, txtbox3, txtbox4, txtbox5, txtbox6, txtbox7, txtbox8, txtbox9;
        private string[] arrparam1, arrparam2, arrparam3, arrparam4, arrparam5, arrparam6, arrparam7, arrparam8, arrparam9, arrparam10;
        public List<TextBox> TBList = new List<TextBox>();
        private string BugType;
        private string DefaultExtension = "";
        private int LayerNO = 0;
        List<FileDetail> AnimationContentList = new List<FileDetail>();
        List<FileDetail> ImageContentList = new List<FileDetail>();
        List<FileDetail> FileContentList = new List<FileDetail>();
        ManualAsRun ObjManualAsRun = null;
        private void Graphicscontrol_Load(object sender, EventArgs e)
        {

        }

        public Graphicscontrol(FrmMainServer obj)
        {
            InitializeComponent();
            this.objmainserver = obj;
        }
        public Graphicscontrol(FrmMainServer obj, string text, DataRow dr)
        {
            InitializeComponent();
            try
            {
                this.objmainserver = obj;
                templatetext.Text = text;
                TakeContentList();
                CreateControlsOnUserControl(dr);//The Number of TextBox and Label will depend on this method
                if (objmainserver.ExternalAgentMode)
                {
                    Durationtxtbox.Text = "00:00:00:00";
                    Durationtxtbox.ReadOnly = true;
                }
                ObjManualAsRun = new ManualAsRun(text, obj.objlog,obj.AsRunFolderPath, obj.ChannelName,obj.AsRunTakeTime, obj.AsRunManualEnable);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("Graphicscontrol_Constructor", "Error--" + ex.ToString(), objmainserver.ChannelName);
            }
        }
        public Graphicscontrol(FrmMainServer obj, string text, DataRow dr, string templatevalue)
        {
            InitializeComponent();
            try
            {
                this.objmainserver = obj;
                templatetext.Text = text;
                TakeContentList();
                CreateControlsOnUserControl(dr); //The Number of TextBox and Label will depend on this method
                if (TBList.Count > 0)
                {
                    TBList[0].Text = templatevalue;
                    Durationtxtbox.Text = "00:00:00:00";
                }
                if (objmainserver.ExternalAgentMode)
                {
                    Durationtxtbox.Text = "00:00:00:00";
                    Durationtxtbox.ReadOnly = true;
                }
                ObjManualAsRun = new ManualAsRun(text, obj.objlog, obj.AsRunFolderPath, obj.ChannelName,obj.AsRunTakeTime, obj.AsRunManualEnable);
                Loadbutton_manual.PerformClick(); //Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("Graphicscontrol_Constructor", "Error--" + ex.ToString(), objmainserver.ChannelName);
            }
        }
        private void CreateControlsOnUserControl(DataRow dr)
        {
            try
            {
                /* string param1 = (dr["Param1"] + "").Trim();
                 BugType = (dr["BugType"] + "").Trim();
                 DefaultExtension = (dr["fileextension"] + "").Trim();
                 LayerNO = Convert.ToInt16((dr["layerno"] + "").Trim());
                 arrparam1 = param1.Split('~');
                 //if (BugType.ToUpper() == "TIMER")
                 //{
                 //    if (arrparam1.Length < 2)
                 //    {
                 //        Durationlbl.Visible = false;
                 //        Durationtxtbox.Visible = false;
                 //    }
                 //    return;
                 //}
                 if (arrparam1.Length == 2)
                 {
                     DynamicManageUi(ref txtbox, arrparam1);
                 }
                 else
                 {
                     AddBackGroundForStaticTemplate();
                 }

                 string param2 = dr["Param2"].ToString().Trim();
                 arrparam2 = param2.Split('~');
                 if (arrparam2.Length == 2)
                 {
                     DynamicManageUi(ref txtbox1, arrparam2);
                 }
                 string param3 = dr["Param3"].ToString().Trim();
                 arrparam3 = param3.Split('~');
                 if (arrparam3.Length == 2)
                 {
                     DynamicManageUi(ref txtbox2, arrparam3);
                 }

                 string param4 = (dr["Param4"] + "").Trim();
                 arrparam4 = param4.Split('~');
                 if (arrparam4.Length == 2)
                 {
                     DynamicManageUi(ref txtbox3, arrparam4);
                 }
                 string param5 = dr["Param5"].ToString().Trim();
                 arrparam5 = param5.Split('~');
                 if (arrparam5.Length == 2)
                 {
                     DynamicManageUi(ref txtbox4, arrparam5);
                 }
                 string param6 = dr["Param6"].ToString().Trim();
                 arrparam6 = param6.Split('~');
                 if (arrparam6.Length == 2)
                 {
                     DynamicManageUi(ref txtbox5, arrparam6);
                 }
                 string param7 = dr["Param7"].ToString().Trim();
                 arrparam7 = param7.Split('~');
                 if (arrparam7.Length == 2)
                 {
                     DynamicManageUi(ref txtbox6, arrparam7);
                 }
                 string param8 = dr["Param8"].ToString().Trim();
                 arrparam8 = param8.Split('~');
                 if (arrparam8.Length == 2)
                 {
                     DynamicManageUi(ref txtbox7, arrparam8);
                 }
                 string param9 = dr["Param9"].ToString().Trim();
                 arrparam9 = param9.Split('~');
                 if (arrparam9.Length == 2)
                 {
                     DynamicManageUi(ref txtbox8, arrparam9);
                 }
                 string param10 = dr["Param10"].ToString().Trim();
                 arrparam10 = param10.Split('~');
                 if (arrparam10.Length == 2)
                 {
                     DynamicManageUi(ref txtbox9, arrparam10);
                 }

             }
             catch
             { }*/
                int totalParam = 0;
                objmainserver.MaxParamFinder(objmainserver.XMLTemplateConfigfilepath, out totalParam);
                string param1 = "";
                BugType = (dr["BugType"] + "").Trim();
                DefaultExtension = (dr["fileextension"] + "").Trim();
                LayerNO = Convert.ToInt16((dr["layerno"] + "").Trim());
                for (int k = 1; k <= totalParam; k++)
                {
                    param1 = "Param" + k.ToString();
                    string param = (dr[param1] + "").Trim();
                    arrparam1 = param.Split('~');
                    if (arrparam1.Length == 0 || arrparam1.Length == 1)
                        if (k == 1)
                            AddBackGroundForStaticTemplate();
                    //continue;
                    if (arrparam1.Length == 2)
                    {
                        DynamicManageUi(TBList, arrparam1);
                    }
                    //else
                    //{
                    //    AddBackGroundForStaticTemplate();
                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void TakeContentList()
        {
            try
            {
                String ContentXmlPath = objmainserver.ChannelNamepath + "\\Content.xml";
                if (File.Exists(ContentXmlPath))
                {
                    DataSet Ds = new DataSet();
                    Ds.ReadXml(ContentXmlPath);
                    if (Ds.Tables["ContentList"] != null)
                    {
                        AnimationContentList = Ds.Tables["ContentList"].AsEnumerable().Where(e => e.Field<string>("TYPE") == "VIDEO").Select(x => new FileDetail { FileName = x.Field<string>("FileName"), Duration = x.Field<string>("Duration"), Extension = x.Field<string>("Extension") }).ToList();
                        ImageContentList = Ds.Tables["ContentList"].AsEnumerable().Where(e => e.Field<string>("TYPE") == "IMAGE").Select(x => new FileDetail { FileName = x.Field<string>("FileName"), Duration = x.Field<string>("Duration"), Extension = x.Field<string>("Extension") }).ToList();
                        FileContentList = Ds.Tables["ContentList"].AsEnumerable().Where(e => e.Field<string>("TYPE") == "FILE").Select(x => new FileDetail { FileName = x.Field<string>("FileName"), Duration = x.Field<string>("Duration"), Extension = x.Field<string>("Extension") }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                objmainserver.objlog.WriteError("TakeContentList", "Error--" + ex.ToString(), objmainserver.ChannelName);
            }

        }
        private void AddBackGroundForStaticTemplate()
        {
            try
            {
                TabPage ObjTP = new TabPage();
                ObjTP.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                ObjTP.Text = "Static Template";
                tabControlData.TabPages.Add(ObjTP);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("DynamicManageUi", "Error" + ex.ToString(), objmainserver.ChannelName);
            }
        }
        private void DynamicManageUi(List<TextBox> TBList, string[] arrparam)
        {
            /*   string PrameterName = "";
               string PrameterType = "";
               try
               {
                   PrameterName = arrparam[0].ToUpper().Trim();
                   PrameterType = arrparam[1].ToUpper().Trim();
                   TabPage ObjTP = new TabPage();
                   ObjTP.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                   ObjTP.Text = PrameterName + " (" + PrameterType + " )";
                   Label lbl = new Label();
                   lbl.BackColor = System.Drawing.Color.Transparent;
                   lbl.Location = new System.Drawing.Point(5, 11);
                   lbl.ForeColor = System.Drawing.Color.White;
                   lbl.Text = PrameterName;
                   lbl.AutoSize = true;
                   // txtbox = new TextBox();
                   TBList.Add(new TextBox());
                   // txtbox.Text = PrameterType;
                   TBList[TBList.Count - 1].Text = PrameterType;
                   // txtbox.Location = new System.Drawing.Point(5, 38);
                   TBList[TBList.Count - 1].Location = new System.Drawing.Point(5, 38);
                   ListViewItem lvi = null;
                   switch (PrameterType)
                   {

                       case "VIDEO":
                           ListView lst = new ListView();
                           lst.MultiSelect = false;
                           lst.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                           lst.ForeColor = System.Drawing.Color.White;
                           lst.Location = new System.Drawing.Point(50, 5);
                           lst.Dock = DockStyle.Right;
                           lst.Width = 630;
                           lst.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                           lst.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                           lst.View = View.Tile;
                           lst.Tag = "VIDEO";
                           lst.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                           foreach (FileDetail item in AnimationContentList)
                           {
                               lvi = new ListViewItem(item.FileName);
                               lvi.Tag = "VIDEO";
                               lvi.SubItems.Add(item.Duration);
                               if(!item.Duration.Equals("00:00:00:00"))
                               lst.Items.Add(lvi);
                           }
                           ObjTP.Controls.Add(lst);
                           break;
                       case "IMAGE":
                           ListView lstImage = new ListView();
                           lstImage.MultiSelect = false;
                           lstImage.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                           lstImage.ForeColor = System.Drawing.Color.White;
                           lstImage.Location = new System.Drawing.Point(50, 5);
                           lstImage.Dock = DockStyle.Right;
                           lstImage.Width = 630;
                           lstImage.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                           lstImage.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                           lstImage.View = View.Tile;
                           lstImage.Tag = "IMAGE";
                           lstImage.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                           foreach (FileDetail item in ImageContentList)
                           {
                               lvi = new ListViewItem(item.FileName);
                               lvi.Tag = "IMAGE";
                               lvi.SubItems.Add(item.Duration);
                               lstImage.Items.Add(lvi);
                           }
                           ObjTP.Controls.Add(lstImage);
                           break;
                       case "TICKER-FILE":
                           ListView lstFile = new ListView();
                           lstFile.MultiSelect = false;
                           lstFile.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                           lstFile.ForeColor = System.Drawing.Color.White;
                           lstFile.Location = new System.Drawing.Point(50, 5);
                           lstFile.Dock = DockStyle.Right;
                           lstFile.Width = 630;
                           lstFile.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                           lstFile.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                           lstFile.View = View.Tile;
                           lstFile.Tag = "FILE";
                           lstFile.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                           foreach (FileDetail item in FileContentList)
                           {
                               lvi = new ListViewItem(item.FileName);
                               lvi.Tag = "FILE";
                               lvi.SubItems.Add(item.Duration);
                               lstFile.Items.Add(lvi);
                           }
                           ObjTP.Controls.Add(lstFile);
                           break;
                       case "TICKER-TEXT":
                           txtbox.Location = new System.Drawing.Point(110, 11);
                           txtbox.Width = 300;
                           txtbox.Height = 80;
                           txtbox.Multiline = true;
                           break;
                       case "TEXT":
                               txtbox.Width = 300;
                               txtbox.Multiline = true;                  
                           break;
                       case "PATH":
                           txtbox.Width = 450;
                           Button Btn = new Button();
                           Btn.Text = "Browse";
                           Btn.FlatStyle = FlatStyle.Flat;
                           Btn.Location = new System.Drawing.Point(465, 38);
                           Btn.Tag = txtbox;
                           Btn.Click += new EventHandler(ButtonClicked);
                           ObjTP.Controls.Add(Btn);
                           break;
                       default:
                           break;
                   }
                   ObjTP.Controls.Add(lbl);
                   ObjTP.Controls.Add(txtbox);
                   tabControlData.TabPages.Add(ObjTP);
               }
               catch (Exception ex)
               {
                   objmainserver.objlog.WriteError("DynamicManageUi", "Error" + ex.ToString(), objmainserver.ChannelName);
               }
               */
            string PrameterName = "";
            string PrameterType = "";
            try
            {
                PrameterName = arrparam[0].ToUpper().Trim();
                PrameterType = arrparam[1].ToUpper().Trim();
                TabPage ObjTP = new TabPage();
                ObjTP.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                ObjTP.Text = PrameterName + " (" + PrameterType + " )";
                Label lbl = new Label();
                lbl.BackColor = System.Drawing.Color.Transparent;
                lbl.Location = new System.Drawing.Point(5, 11);
                lbl.ForeColor = System.Drawing.Color.White;
                lbl.Text = PrameterName;
                lbl.AutoSize = true;
                TBList.Add(new TextBox());
                // txtbox = new TextBox();
                TBList[TBList.Count - 1].Text = PrameterType;
                //  txtbox.Text = PrameterType;
                TBList[TBList.Count - 1].Location = new System.Drawing.Point(5, 38);
                ListViewItem lvi = null;
                switch (PrameterType)
                {

                    case "VIDEO":
                        ListView lst = new ListView();
                        lst.MultiSelect = false;
                        lst.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                        lst.ForeColor = System.Drawing.Color.White;
                        lst.Location = new System.Drawing.Point(50, 5);
                        lst.Dock = DockStyle.Right;
                        lst.Width = 630;
                        lst.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                        lst.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                        lst.View = View.Tile;
                        lst.Tag = "VIDEO";
                        lst.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                        foreach (FileDetail item in AnimationContentList)
                        {
                            lvi = new ListViewItem(item.FileName);
                            lvi.Tag = "VIDEO";
                            lvi.SubItems.Add(item.Duration);
                            if ((item.Extension.ToUpper() != ".MOV"))
                            {
                                lst.Items.Add(lvi);
                            }
                            else
                            {
                              //  if (!item.Duration.Equals("00:00:00:00"))
                                    lst.Items.Add(lvi);
                            }
                        }
                        //foreach (FileDetail item in ImageContentList)
                        //{
                        //    lvi = new ListViewItem(item.FileName);
                        //    lvi.Tag = "VIDEO";
                        //    lvi.SubItems.Add(item.Duration);
                        //   // if (!item.Duration.Equals("00:00:00:00"))
                        //        lst.Items.Add(lvi);
                        //}
                        ObjTP.Controls.Add(lst);
                        break;
                    case "IMAGE":
                        ListView lstImage = new ListView();
                        lstImage.MultiSelect = false;
                        lstImage.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                        lstImage.ForeColor = System.Drawing.Color.White;
                        lstImage.Location = new System.Drawing.Point(50, 5);
                        lstImage.Dock = DockStyle.Right;
                        lstImage.Width = 630;
                        lstImage.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                        lstImage.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                        lstImage.View = View.Tile;
                        lstImage.Tag = "IMAGE";
                        lstImage.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                        foreach (FileDetail item in ImageContentList)
                        {
                            lvi = new ListViewItem(item.FileName);
                            lvi.Tag = "IMAGE";
                            lvi.SubItems.Add(item.Duration);
                            lstImage.Items.Add(lvi);
                        }
                        ObjTP.Controls.Add(lstImage);
                        break;
                    case "TICKER-FILE":
                        ListView lstFile = new ListView();
                        lstFile.MultiSelect = false;
                        lstFile.BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
                        lstFile.ForeColor = System.Drawing.Color.White;
                        lstFile.Location = new System.Drawing.Point(50, 5);
                        lstFile.Dock = DockStyle.Right;
                        lstFile.Width = 630;
                        lstFile.Columns.Add("FileName", 300, HorizontalAlignment.Left);
                        lstFile.Columns.Add("Duration", 70, HorizontalAlignment.Left);
                        lstFile.View = View.Tile;
                        lstFile.Tag = "FILE";
                        lstFile.SelectedIndexChanged += new EventHandler(ItemSellectionChange);
                        foreach (FileDetail item in FileContentList)
                        {
                            lvi = new ListViewItem(item.FileName);
                            lvi.Tag = "FILE";
                            lvi.SubItems.Add(item.Duration);
                            lstFile.Items.Add(lvi);
                        }
                        ObjTP.Controls.Add(lstFile);
                        break;
                    case "TICKER-TEXT":
                        TBList[TBList.Count - 1].Location = new System.Drawing.Point(110, 11);
                        TBList[TBList.Count - 1].Width = 300;
                        TBList[TBList.Count - 1].Height = 80;
                        TBList[TBList.Count - 1].Multiline = true;
                        break;
                    case "TEXT":
                        TBList[TBList.Count - 1].Width = 300;
                        TBList[TBList.Count - 1].Multiline = true;
                        break;
                    case "PATH":
                        TBList[TBList.Count - 1].Width = 450;
                        Button Btn = new Button();
                        Btn.Text = "Browse";
                        Btn.FlatStyle = FlatStyle.Flat;
                        Btn.Location = new System.Drawing.Point(465, 38);
                        Btn.Tag = TBList[TBList.Count - 1];
                        Btn.Click += new EventHandler(ButtonClicked);
                        ObjTP.Controls.Add(Btn);
                        break;
                    default:
                        break;
                }
                ObjTP.Controls.Add(lbl);
                ObjTP.Controls.Add(TBList[TBList.Count - 1]);
                tabControlData.TabPages.Add(ObjTP);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("DynamicManageUi", "Error" + ex.ToString(), objmainserver.ChannelName);
            }

        }

        void ButtonClicked(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox TextBoxname = (TextBox)((Button)sender).Tag;
                    TextBoxname.Text = dlg.FileName;

                }
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("ButtonClicked", "Error" + ex.ToString(), objmainserver.ChannelName);

            }

        }
        private void ItemSellectionChange(object sender, System.EventArgs e)
        {
            try
            {
                ListView lstData = (sender as ListView);
                string Duration = "00:00:00:00";
                string lstdata = "";
                if (lstData != null)
                {
                    if (lstData.SelectedItems == null || lstData.SelectedItems.Count < 1)
                    {
                        return;
                    }
                    ListViewItem lstItem = lstData.SelectedItems[0];
                    if (lstItem == null) return;
                    lstdata = lstItem.Text;
                    switch (lstItem.Tag.ToString())
                    {
                        case "VIDEO":
                            Duration = AnimationContentList.Find(r => r.FileName == lstdata).Duration;
                            break;
                        case "IMAGE":
                            Duration = ImageContentList.Find(r => r.FileName == lstdata).Duration;
                            break;
                        case "FILE":
                            Duration = FileContentList.Find(r => r.FileName == lstdata).Duration;
                            break;
                        default:
                            break;
                    }
                    TabPage Tp = tabControlData.SelectedTab;
                    foreach (Control Contitem in Tp.Controls)
                    {
                        if (Contitem is TextBox)
                        {
                            Contitem.Text = lstdata;
                            break;
                        }
                    }
                    if (objmainserver.ExternalAgentMode)
                    {
                        Durationtxtbox.Text = "00:00:00:00";
                    }
                    else
                    {
                        Durationtxtbox.Text = Duration;
                    }

                }
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("ItemSellectionChange", "Error in fetching Data " + ex.ToString(), objmainserver.ChannelName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// To Copy Content From Content View to UserControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graphicscontrol_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
                {
                    e.SuppressKeyPress = true;
                    ((TextBox)sender).Text = string.Empty;
                    ((TextBox)sender).Text = Clipboard.GetText();
                }
            }
            catch
            { }
        }

        #region Manual Load,Kill,hide and show
        private async void Loadbutton_manual_Click(object sender, EventArgs e)
        {
            int x = 0;
            await Task.Run(async() =>
           {
               string offset = "00:00:00:00";
               bool IsAvailableOnNas = false;
               bool IsAvaiable = false;
               try
               {
                   int totalParam = 0;
                   objmainserver.MaxParamFinder(objmainserver.XMLTemplateConfigfilepath, out totalParam);
                   DataRow dr = objmainserver._dttemplateinfo.Select("TemplateName='" + templatetext.Text.ToUpper() + "'").FirstOrDefault();
                   if (Durationtxtbox.Text == "__:__:__:__" || Durationtxtbox.Text == "00:00:00:00")
                   {

                       if (objmainserver.UsedItems.Contains(templatetext.Text.ToUpper()))
                       {
                           objmainserver.KillCgItem(templatetext.Text.ToUpper(), BugType, 300, PreviewOn.Checked);
                           ObjManualAsRun.PrepareAsRunDetail("STOPKILL");
                           await Task.Delay(1000);
                       }
                       ObjManualAsRun.PrepareAsRunDetail("LOAD");
                       objmainserver.FillEventLog("Receive Packet Manually Load " + templatetext.Text, "G");
                       //if (arrparam1 != null && arrparam1.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox.Text, arrparam1[1], DefaultExtension,out IsAvailableOnNas, out  IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam1[0], templatevalues, arrparam1[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam2 != null && arrparam2.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox1.Text, arrparam2[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam2[0], templatevalues, arrparam2[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox1.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam3 != null && arrparam3.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox2.Text, arrparam3[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam3[0], templatevalues, arrparam3[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox2.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam4 != null && arrparam4.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox3.Text, arrparam4[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam4[0], templatevalues, arrparam4[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox3.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam5!=null && arrparam5.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox4.Text, arrparam5[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam5[0], templatevalues, arrparam5[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox4.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam6 != null && arrparam6.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox5.Text, arrparam6[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam6[0], templatevalues, arrparam6[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox5.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam7 != null && arrparam7.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox6.Text, arrparam7[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam7[0], templatevalues, arrparam7[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox6.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam8 != null && arrparam8.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox7.Text, arrparam8[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam8[0], templatevalues, arrparam8[1], BugType , IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox7.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam9 != null && arrparam9.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox8.Text, arrparam9[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam9[0], templatevalues, arrparam9[1], BugType , IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox8.Text, arrparam1[1], IsAvaiable);
                       //}
                       //if (arrparam10 != null && arrparam10.Length >= 2)
                       //{
                       //    string templatevalues = objmainserver.ContentInfo(txtbox9.Text, arrparam10[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                       //    objmainserver.UpdateXMLComposition(templatetext.Text, arrparam10[0], templatevalues, arrparam10[1], BugType, IsAvailableOnNas);
                       //    ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", txtbox9.Text, arrparam1[1], IsAvaiable);
                       //}
                       string param1 = "";
                       BugType = (dr["BugType"] + "").Trim();
                       DefaultExtension = (dr["fileextension"] + "").Trim();
                       LayerNO = Convert.ToInt16((dr["layerno"] + "").Trim());
                       string templatevalues = "";
                       for (int k = 1; k <= totalParam; k++)
                       {
                           param1 = "Param" + k.ToString();
                           string param = (dr[param1] + "").Trim();
                           arrparam1 = param.Split('~');
                           if (arrparam1.Length != 2)
                               continue;
                           String PrmData = TBList[k - 1].Text;
                           templatevalues = objmainserver.ContentInfo(PrmData, arrparam1[1], DefaultExtension, out IsAvailableOnNas, out IsAvaiable);
                           objmainserver.UpdateXMLComposition(templatetext.Text, arrparam1[0], templatevalues, arrparam1[1], BugType, IsAvailableOnNas,IsAvaiable);
                           ObjManualAsRun.PrepareAsRunDetail("UPDATE_PARAMETER_VALUE", PrmData, BugType, IsAvaiable);
                       }
                       bool IsLoaded = objmainserver.LoadCgItems(templatetext.Text.ToUpper(), "00:00:00:00", PreviewOn.Checked);
                       string che = String.Empty;
                       if (IsLoaded == false)
                       {
                           ObjManualAsRun.PrepareAsRunDetail("UPDATE_FAIL_STATUS");
                           che = "GFXClient||" + objmainserver.ChannelName + "||FAIL||" + templatetext.Text + "||" + "START";
                           if (objmainserver.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                           {
                               objmainserver.SendUdpResponse(objmainserver.GfxclientIp, objmainserver.GfxclientPort, che);
                               objmainserver.objlog.WriteEvents("Loadbutton_manual_Click", "Sent to client-->" + che, objmainserver.ChannelName);
                           }
                       }
                       else
                       {
                           che = "GFXClient||" + objmainserver.ChannelName + "||SUCCESS||" + templatetext.Text + "||" + "START";
                           if (objmainserver.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                           {
                               objmainserver.SendUdpResponse(objmainserver.GfxclientIp, objmainserver.GfxclientPort, che);
                               objmainserver.objlog.WriteEvents("Loadbutton_manual_Click", "Sent to client-->" + che, objmainserver.ChannelName);
                           }
                           objmainserver.objlog.WriteEvents("Loadbutton_manual_Click", "Template updated and loaded manually ", objmainserver.ChannelName);
                           objmainserver.FillEventLog("Template updated and loaded manually " + templatetext.Text, "G");
                           Durationtxtbox.ReadOnly = true;
                           if (templatetext.Text.ToUpper() == "LOGO" || templatetext.Text.ToUpper() == "FESTLOGO")
                           {
                               // SaveLogoFileDetail(templatetext.Text, txtbox != null ? txtbox.Text : "");
                               SaveLogoFileDetail(templatetext.Text, TBList.Count > 0 ? TBList[0].Text : "");
                             //  String Templatestarttime = DateTime.Now.ToString("HH:mm:ss.fff");
                             //  objmainserver.objAgent.PrepareAsRunSetting("START", Templatestarttime, "24:00:00:00", TBList.Count > 0 ? TBList[0].Text : "", templatetext.Text);
                           }
                       }
                   }
                   else
                   {
                       string templatevalue = "";
                       if (string.Compare(BugType, "TIMER", true) == 0)
                       {
                           templatevalue = Durationtxtbox.Text;//This value will be used in case of TIMER Bugtype
                       }
                       if (BugType.ToUpper() != "TIMER")
                       {
                           //if (arrparam1.Length == 2)
                           //{ templatevalue = txtbox.Text; }
                           //if (BugType == "VIDEO-SQZ")
                           //{
                           //    offset = "00:00:03:00";
                           //}
                           //else
                           //{
                           //    if (arrparam2.Length == 2)
                           //    {
                           //        templatevalue += "~" + txtbox1.Text;
                           //    }
                           //    if (arrparam3.Length == 2)
                           //    {
                           //        templatevalue += "~" + txtbox2.Text;
                           //    }
                           //    if (arrparam4.Length == 2)
                           //    {
                           //        templatevalue += "~" + txtbox3.Text;
                           //    }
                           //    if (arrparam5.Length == 2)
                           //    {
                           //        templatevalue += "~" + txtbox4.Text;
                           //    }
                           //}
                           string param1 = "";
                           for (int k = 1; k <= totalParam; k++)
                           {
                               param1 = "Param" + k.ToString();
                               string param = (dr[param1] + "").Trim();
                               arrparam1 = param.Split('~');
                               if (arrparam1.Length == 2)
                               {
                                   templatevalue = TBList[k - 1].Text;
                               }
                               if (k == 1)
                               {
                                   if (BugType == "VIDEO-SQZ")
                                   {
                                       offset = "00:00:03:00";
                                   }
                               }

                           }
                       }
                       string SecondaryPacket = $"0;SEC;{objmainserver.ChannelName};{objmainserver.RunningPrimary};0;yyyy;{objmainserver.objlog.LTCTime};{templatevalue};{offset};{Durationtxtbox.Text};{templatetext.Text};;;{objmainserver.Duration}";
                       string[] arrsecondarypacket = SecondaryPacket.Split(';');
                       objmainserver.objpacket.FillSecondaryData(arrsecondarypacket);
                       if (templatetext.Text.ToUpper() == "LOGO" || templatetext.Text.ToUpper() == "FESTLOGO")
                       {
                           //  SaveLogoFileDetail(templatetext.Text, txtbox != null ? txtbox.Text : "");
                           //  (dr["Param"] + "").Split("~").Count
                           SaveLogoFileDetail(templatetext.Text, TBList.Count > 0 ? TBList[0].Text : "");
                       }
                   }
                   objmainserver.ReadTriggerFile("", "AFTERSTART");
               }
               catch (Exception ex)
               {
                   objmainserver.objlog.WriteError("Loadbutton_manual_Click", "Graphicscontrol Class " + ex.Message, objmainserver.ChannelName);
                   objmainserver.FillEventLog("Receive Packet Manually Load " + templatetext.Text + ex.Message, "E");
               }
           });
        }

       
        public void SaveLogoFileDetail(string TemplateName, string templateValue)
        {
            try
            {
                DataTable Dt = new DataTable("FreezeTemplate");
                Dt.Columns.Add("TemplateName");
                Dt.Columns.Add("param1ID");
                DataRow Dr = Dt.NewRow();
                Dr["TemplateName"] = TemplateName;
                Dr["param1ID"] = templateValue.Trim();
                Dt.Rows.Add(Dr);
                Dt.WriteXml(Application.StartupPath + $"//Channels//{objmainserver.ChannelName}//{objmainserver.ChannelName}_FreezeTemplate.xml");
                objmainserver.writeHistorydata(templateValue);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("SaveLogoFileDetail", "Save Logo File Detail " + ex.Message, objmainserver.ChannelName);
            }
        }
        private void killbutton_manual_Click(object sender, EventArgs e)
        {

            try
            {
                objmainserver.objlog.WriteEvents("killbutton_manual_Click", "Killed manual="+templatetext.Text, objmainserver.ChannelName);
                if (Durationtxtbox.ReadOnly)
                {
                    objmainserver.FillEventLog($"Receive Packet Manually Kill {templatetext.Text}", "G");
                    objmainserver.KillCgItem(templatetext.Text.ToUpper(), BugType,300, PreviewOn.Checked);
                    objmainserver.load.Kill(this);
                    ObjManualAsRun.PrepareAsRunDetail("STOPKILL");
                    objmainserver.usedcgitemsmanually.Remove(templatetext.Text.ToUpper());
                }
                else
                {
                    objmainserver.FillEventLog($"Receive Packet Manually Kill {templatetext.Text}", "G");

                    var ManualRunningRow = from DataGridViewRow row in objmainserver.dgvsecevent.Rows
                                           where (row.Cells["SecTemplate"].Value.ToString() == templatetext.Text
                                           && row.Cells["SecStatus"].Value.ToString() == "R")
                                           select row;
                    foreach (var row in ManualRunningRow)
                    {
                    row.Cells["SecDeleteCheck"].Value = "Y"; }
                    objmainserver.load.Kill(this);
                    objmainserver.usedcgitemsmanually.Remove(templatetext.Text.ToUpper());
                }
                string che = "GFXClient||" + objmainserver.ChannelName + "||SUCCESS||" + templatetext.Text + "||" + "STOP";
                if (objmainserver.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                {
                    objmainserver.SendUdpResponse(objmainserver.GfxclientIp, objmainserver.GfxclientPort, che);
                    objmainserver.objlog.WriteEvents("Killbutton_manual_Click", "Sent to client-->" + che, objmainserver.ChannelName);
                }
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("killbutton_manual_Click", "Graphicscontrol Class " + ex.Message, objmainserver.ChannelName);
                objmainserver.FillEventLog("Receive Packet Manually Kill " + templatetext.Text + ex.Message, "E");
            }
            // });
        }
        private void hidebutton_manual_Click(object sender, EventArgs e)
        {
            try
            {
                objmainserver.FillEventLog($"Receive Packet Manually hide {templatetext.Text}", "G");
                objmainserver.HideCgItem(templatetext.Text.ToUpper(), BugType);

            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("hidebutton_manual_Click", "Graphicscontrol Class " + ex.Message, objmainserver.ChannelName);
                objmainserver.FillEventLog("Receive Packet Manually hide " + templatetext.Text + ex.Message, "E");
            }
        }
        private void showbutton_manual_Click(object sender, EventArgs e)
        {
            try
            {
                objmainserver.FillEventLog("Receive Packet Manually Show " + templatetext.Text, "G");
                objmainserver.ShowCgItem(templatetext.Text.ToUpper(), BugType);

            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("showbutton_manual_Click", "Graphicscontrol Class " + ex.Message, objmainserver.ChannelName);
                objmainserver.FillEventLog("Receive Packet Manually Show " + templatetext.Text + ex.Message, "E");
            }
        }

        
        #endregion
    }

    public class FileDetail
    {
        public String FileName { get; set; }
        public String Duration { get; set; }
        public String Extension { get; set; }
    }
}