using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using MFORMATSLib;

namespace graphicserver
{
    public partial class manualgraphicsload : Form
    {
        FrmMainServer objmainserver;
        Graphicscontrol objusercontrol;
        DataTable DtContentList;
        public manualgraphicsload(FrmMainServer obj)
        {
            InitializeComponent();
            this.objmainserver = obj;
            CreateDtForFile();
            CheckandFillContent();
        }
        public Graphicscontrol GetCGItem()
        {
            return objusercontrol;
        }
        private void manualgraphicsload_Load(object sender, EventArgs e)
        {
            try
            {
                flp_manualloadtemplate.AllowDrop = true;
                CheckandFillContent();                
               // this.FormBorderStyle = FormBorderStyle.None;
            }
            catch
            { }
        }
        private void lb_templatelist_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                SelectItem(ref e);
            }
            catch
            {
                SelectItem(ref e);
            }
        }
        private void flp_manualloadtemplate_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                InsertControl(str);
            }
            catch (Exception)
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                InsertControl(str);
            }
        }
        private void flp_manualloadtemplate_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.All;
            }
            catch
            { }
        }
        private void SelectItem(ref MouseEventArgs e)
        {
            try
            {
                if (lb_templatelist.Items.Count == 0)
                    return;

                int index = lb_templatelist.IndexFromPoint(e.X, e.Y);
                if (index >= 0)
                {
                    string s = lb_templatelist.Items[index].ToString();
                    DragDropEffects dde1 = DoDragDrop(s, DragDropEffects.All);
                }
            }
            catch (Exception)
            {
                SelectItem(ref e);
            }
        }
        private void InsertControl(string str)
        {
            try
            {
                DataRow dr = objmainserver._dttemplateinfo.Select("TemplateName='" + str + "'").FirstOrDefault();
                if (dr != null)
                {
                    objusercontrol = new Graphicscontrol(objmainserver, str, dr);
                    if (flp_manualloadtemplate.Controls.Count < 8 && !objmainserver.usedcgitemsmanually.Contains(str))
                    {
                        flp_manualloadtemplate.Controls.Add(objusercontrol);
                        objmainserver.usedcgitemsmanually.Add(str.ToUpper());
                    }
                    return;
                }
                objmainserver.objlog.WriteEvents("InsertControl", "DataTable does not contain data for this template", objmainserver.ChannelName);
                objmainserver.FillEventLog("DataTable does not contain data for this template", "E");

            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("InsertControl", "Error-- " + ex.ToString(), objmainserver.ChannelName);
            }
        }
        private void CheckandFillContent()
        {
            if(!File.Exists(objmainserver.ChannelNamepath + "\\Content.xml"))
            {
                GetAllContent();
            }
            else
            {
                try
                {
                    String ContentXmlPath = objmainserver.ChannelNamepath + "\\Content.xml";
                    if (File.Exists(ContentXmlPath))
                    {
                        DataSet Ds = new DataSet();
                        Ds.ReadXml(ContentXmlPath);
                        if (Ds.Tables["ContentList"] == null)
                        {
                            GetAllContent();
                        }
                    }
                }
                catch (Exception ex)
                {

                    objmainserver.objlog.WriteError("checkandfillContent", "Error--" + ex.ToString(), objmainserver.ChannelName);
                }
            }

        }


        public void InsertControlForFreeze(string str, string TemplatevalueValue)
        {
            try
            {
                DataRow dr = objmainserver._dttemplateinfo.Select("TemplateName='" + str + "'").FirstOrDefault();
                if (dr != null)
                {
                    #region                         
                    string[] _Type = (dr["Param1"].ToString() + "").Split('~');
                    string arrtype = "";
                    if (_Type.Length > 1)
                    {
                        arrtype = _Type[1];
                    }
                    if (!string.IsNullOrEmpty(arrtype))
                    {
                       
                        string bugtype = "";
                        if (arrtype == "TEXT")
                        {
                            bugtype = dr["BugType"].ToString().Trim();

                            if (bugtype.ToUpper().Contains("TICKER"))
                            {
                                string TickerType = bugtype.Split('-')[1];
                                if (TickerType == "FILE")
                                {
                                    arrtype = bugtype;
                                }
                            }
                        }
                    }
                    #endregion
                    objusercontrol = new Graphicscontrol(objmainserver, str, dr, TemplatevalueValue);

                    if (flp_manualloadtemplate.Controls.Count < 8 && !objmainserver.usedcgitemsmanually.Contains(str))
                    {
                        flp_manualloadtemplate.Controls.Add(objusercontrol);
                        objmainserver.usedcgitemsmanually.Add(str.ToUpper());
                    }
                    return;
                }
                objmainserver.objlog.WriteEvents("InsertControl", "DataTable does not contain data for this template", objmainserver.ChannelName);
                objmainserver.FillEventLog("DataTable does not contain data for this template", "E");

            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("InsertControl", "Error-- " + ex.ToString(), objmainserver.ChannelName);
            }
        }
        public void Kill(Graphicscontrol objusercontrol)
        {
            try
            {
                flp_manualloadtemplate.Controls.Remove(objusercontrol);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("Kill Graphicscontrol Error-- " + ex.ToString(), objmainserver.ChannelName);
            }
        }

        private void refreshConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetAllContent();
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("refreshConfigToolStripMenuItem_Click" + ex.ToString(), objmainserver.ChannelName);
            }
        }
        private void CreateDtForFile()
        {
            DtContentList = new DataTable("ContentList");
            DtContentList.Columns.Add("FileName");
            DtContentList.Columns.Add("Duration");
            DtContentList.Columns.Add("Extension");
            DtContentList.Columns.Add("Type");
        }

        private void GetAllContent()
        {
            string log = " Start refressing All Content Starttime=";
            string dirname = "";
            MFReaderClass objReader = new MFReaderClass();
            try
            {
                log = log + $"({objmainserver.objlog.LTCTime})";
              
                DtContentList.Clear();
                //fetching all video content.
                dirname = objmainserver.ContentPath + @"\ANIMATION";
                GetContentDetail(dirname,ref objReader,"VIDEO");
                //Geting all Image Video content
                dirname = objmainserver.ContentPath + @"\IMAGE";
                GetContentDetail(dirname, ref objReader,"IMAGE");
                //Geting all File content
                dirname = objmainserver.ContentPath + @"\File";
                GetContentDetail(dirname, ref objReader,"FILE");
                log = log+"End Time =" + $"({objmainserver.objlog.LTCTime}) ";
                objmainserver.objlog.WriteEvents("GetAllContent", log, objmainserver.ChannelName);
            }
            catch (Exception ex)
            {
                objmainserver.objlog.WriteError("GetAnimationContent", "Error in Getting Animation Content " + ex.ToString(), objmainserver.ChannelName);
            }
            finally
            {
                DtContentList.WriteXml(objmainserver.ChannelNamepath+ "\\Content.xml");
                if (objReader != null)
                {
                    objReader.ReaderClose();
                    objmainserver.releaseComObj(objReader);
                }
            }
        }
        private void GetContentDetail(string dirname,ref MFReaderClass objReader,string Type)
        {
            string FileName = "", Duration = "00:00:00:00" ,Extension="";
         
            if (Directory.Exists(dirname))
            {
                string[] files = Directory.GetFiles(dirname);
                foreach (string file in files)
                {
                    try
                    {
                        FileName = Path.GetFileNameWithoutExtension(file);
                        Extension = Path.GetExtension(file);
                        objReader.ReaderOpen(file, "");
                        double DoubleDuration = 0;
                        objReader.ReaderDurationGet(out DoubleDuration);
                        if (DoubleDuration < 1)
                        {
                            DataRow objrow=  DtContentList.NewRow();
                            objrow["FileName"] = FileName;
                            objrow["Duration"] = "00:00:00:00";
                            objrow["Extension"] = Extension;
                            objrow["Type"] = Type;
                            DtContentList.Rows.Add(objrow);
                        }
                        else
                        {
                            TimeSpan ts_dur = TimeSpan.FromSeconds(DoubleDuration);
                            Duration = ts_dur.Hours.ToString("00") + ":" + ts_dur.Minutes.ToString("00") + ":" + ts_dur.Seconds.ToString("00") + ":" + (ts_dur.Milliseconds / 40).ToString("00");
                            DataRow objrow = DtContentList.NewRow();
                            objrow["FileName"] = FileName;
                            objrow["Duration"] = Duration;
                            objrow["Extension"] = Extension;
                            objrow["Type"] = Type;
                            DtContentList.Rows.Add(objrow);
                        }

                    }
                    catch
                    {
                        DataRow objrow = DtContentList.NewRow();
                        objrow["FileName"] = FileName;
                        objrow["Duration"] = "00:00:00:00";
                        objrow["Extension"] = Extension;
                        objrow["Type"] = Type;
                        DtContentList.Rows.Add(objrow);
                    }
                }
            }
        }

        private void contentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmContentList obj = new FrmContentList();
                obj.ShowDialog();
            }
            catch
            {

                
            }
        }

        //end of class
    }
}
