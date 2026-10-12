using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace graphicserver.ContentForm
{
    public partial class ChannelBug : Form
    {
        FrmMainServer _mainServerObj;
        String _LogoPath = "";
        String _Logotyp = "";
        public ChannelBug(FrmMainServer obj)
        {
            _mainServerObj = obj;
            InitializeComponent();
            string presel = "";
            textBox1.Text = "";
            if (_mainServerObj.GetLatestBugdetails(2, out presel))
                if (presel == "" || string.IsNullOrWhiteSpace(presel))
                {
                    textBox1.Visible = false;
                    label1.Visible = false;
                    checkBox1.Location = new Point(280, 51);
                }
                else
                {
                    textBox1.Visible = true;
                    label1.Visible = true;
                    checkBox1.Location = new Point(280, 21);
                }
            textBox1.Text = presel;
            _mainServerObj.objlog.WriteEvents("Channelbug", "Received bugvalue at freezeTemplate="+presel, _mainServerObj.ChannelName);
           // objlog.WriteEvents("RenderAutoStart", $"File Not Found For Set Auto Render Out FileName= {RenderFile} ", ChannelName);
            _mainServerObj.ReadHistoricdata(ref listBox1);
         //   listBox1.Enabled= false;
            presel = "";
            if (_mainServerObj.GetLatestBugdetails(3, out presel))
            {
                _mainServerObj.objlog.WriteEvents("Channelbug", "Received channelbugtype=" + presel, _mainServerObj.ChannelName);
                if (presel.ToUpper().Contains("VIDEO"))
                {
                    _LogoPath = _mainServerObj.ContentPath + "\\" + "ANIMATION";                    
                    _mainServerObj.GetContentDetail(_LogoPath, ref listBox2);
                }
                else if(presel.ToUpper().Contains("IMAGE"))
                {
                    _LogoPath = _mainServerObj.ContentPath + "\\" + presel.Trim();
                    _mainServerObj.GetContentDetail(_LogoPath, ref listBox2);
                }
           
                _mainServerObj.objlog.WriteEvents("Channelbug", "found ContentList at " + _LogoPath, _mainServerObj.ChannelName);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //  DataRow dr = _mainServerObj._dttemplateinfo.Select("TemplateName='" + str + "'").FirstOrDefault();
            // this.Hide();
            // UpdateXML("D:\\RUNNINGCHANNELS\\GFX\\TEMPLATE\\LOGO.ml-cgs",_mainServerObj.ContentPath+"\\abc.mov");

            //  return;
            string preselValue = string.Empty;
            if (checkBox1.Checked)
            {
                string presel = "LOGO2";
                //    DataRow dr = objmainserver._dttemplateinfo.Select("TemplateName='" + str + "'").FirstOrDefault();
                preselValue = listBox2.SelectedItem.ToString();
                //  if (_mainServerObj.GetLatestBugdetails(1, out preselValue))
                _mainServerObj.objlog.WriteEvents("button1_Click", "going to run logo2 user control", _mainServerObj.ChannelName);
                _mainServerObj.load.InsertControlForFreeze(presel, preselValue);
            }
            else
            {
                try
                {
                    preselValue = listBox2.SelectedItem.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Invalid file selection");
                    _mainServerObj.objlog.WriteError("button1_Click", "Invalid file selection", _mainServerObj.ChannelName);
                    return;
                }
                    await Task.Run(() =>
                    {
                        string presel = "";
                        if (_mainServerObj.GetLatestBugdetails(1, out presel))
                        {
                            _mainServerObj.FillEventLog($"Killed from channelbug zone in mainwindow {presel}", "G");
                            DataRow dr = _mainServerObj._dttemplateinfo.Select("TemplateName='" + presel + "'").FirstOrDefault();
                            if (dr != null)
                            {
                                _mainServerObj.objlog.WriteEvents("button1_Click", "going to kill template from freeze" + presel, _mainServerObj.ChannelName);
                                _mainServerObj.KillCgItem(presel.ToUpper(), "VIDEO");
                                _mainServerObj.objlog.WriteEvents("button1_Click", "going to kill running autologo user control", _mainServerObj.ChannelName);
                                _mainServerObj.load.Kill(_mainServerObj.load.GetCGItem());
                            //  ObjManualAsRun.PrepareAsRunDetail("STOPKILL");
                            _mainServerObj.usedcgitemsmanually.Remove(presel.ToUpper());
                            // Thread.Sleep(1500);
                            Task.Delay(1000).Wait();

                            //  if (_mainServerObj.GetLatestBugdetails(1, out preselValue))
                            string _gfxPath = _mainServerObj.TemplatePath + "\\" + presel.Trim() + ".ml-cgs";
                                string _filepath = _mainServerObj.ContentPath + "\\ANIMATION\\" + preselValue;
                                string typ = dr["Param1"] + "";
                                if (typ.Split('~').Length == 2)
                                {
                                    _mainServerObj.objlog.WriteEvents("button1_Click", "going to run logo user control", _mainServerObj.ChannelName);
                                    _mainServerObj.load.InsertControlForFreeze(presel, preselValue);
                                    Thread.Sleep(500);
                                    _mainServerObj.ReadHistoricdata(ref listBox1);
                                }
                                else
                                {
                                    if (Directory.Exists(_mainServerObj.ContentPath))
                                    {
                                        string[] extarr = Directory.GetFiles(_mainServerObj.ContentPath + "\\" + _Logotyp, preselValue + ".*");
                                        if (extarr.Length > 0)
                                        {
                                            string ext = extarr[0];
                                            UpdateXML(_gfxPath, ext);
                                            _mainServerObj.objlog.WriteEvents("button1_Click", "going to run logo user control", _mainServerObj.ChannelName);
                                            _mainServerObj.load.InsertControlForFreeze(presel, preselValue);
                                            Thread.Sleep(500);
                                            _mainServerObj.writeHistorydata(preselValue);
                                            _mainServerObj.ReadHistoricdata(ref listBox1);
                                        }
                                    }
                                }

                            }
                        }
                    });
                    //Thread.Sleep(500);
                    //_mainServerObj.ReadHistoricdata(ref listBox1);

                }
                
        }
        public void UpdateXML(string templatepath, string templatedata)
        {
            try
            {
                if (string.IsNullOrEmpty(templatepath))
                    return;

                if (File.Exists(templatepath))
                {
                    XDocument template = XDocument.Load(templatepath);
                    List<XElement> messageitems = (from item in template.Descendants("cg-item")
                                                   where item.Elements().ToList()[0].Name.ToString().ToUpper() == "IMG"
                                                   select item).ToList();
                    foreach (var item in messageitems)
                    {
                        if (item.Elements().ToList()[0].Attribute("path") != null)
                        {
                            item.Elements().ToList()[0].Attribute("path").Value = templatedata;
                            template.Save(templatepath);
                            File.WriteAllText(templatepath, template.ToString().Replace("&lt;", "<").Replace("&gt;", ">"));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void BrowsLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";
            openFileDialog1.Filter = "Video files (*.mov)|*.mov|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _Logotyp = "";
                string Ext = openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf('.')+1);
                if (Ext.ToUpper().Contains("MOV") || Ext.ToUpper().Contains("AVI"))
                {
                    _LogoPath = _mainServerObj.ContentPath + "\\" + "ANIMATION";
                    _Logotyp = "ANIMATION";
                }
                else
                {
                    _LogoPath = _mainServerObj.ContentPath + "\\" + "IMAGE";
                    _Logotyp = "IMAGE";
                }
                try
                {
                   System.IO.File.Copy(openFileDialog1.FileName, _LogoPath+"\\"+ System.IO.Path.GetFileName(openFileDialog1.FileName), true);
                   _mainServerObj.GetContentDetail(_LogoPath, ref listBox2);
                }
                catch (Exception iox)
                {
                    _mainServerObj.objlog.WriteEvents("BrowsLogo_Click", "Exception copying"+iox.Message, _mainServerObj.ChannelName);
                    return;
                }
                _mainServerObj.objlog.WriteEvents("BrowsLogo_Click", "Copied successfully", _mainServerObj.ChannelName);
            }
        }
    }
}
