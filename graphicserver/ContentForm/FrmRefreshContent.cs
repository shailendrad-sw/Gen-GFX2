using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MFORMATSLib;
using System.Runtime.InteropServices;

namespace graphicserver.ContentForm
{
    public partial class FrmRefreshContent : Form
    {
        DataTable DtContentList;
        public String PrimaryContentPath { get; set; } = @"E:\GFXSERVER\Content";
        public String SecondaryContentPath { get; set; }= @"D:\assets\9XM\BUZZING NOW GRAPHICS\BUZZING NOW GRAPHICS";
        public FrmRefreshContent()
        {
            InitializeComponent();
        }

        private void FrmRefreshContent_Load(object sender, EventArgs e)
        {
            LoadTheam();

        }

        public void LoadTheam()
        {
            foreach (Control objControl in this.Controls)
            {
                if (objControl.GetType() == typeof(Button))
                {
                    objControl.BackColor = clsGlobalColor.PrimaryColor;
                }
                if (objControl.GetType() == typeof(Label) && objControl.Name.ToLower().Contains("heading"))
                {
                    objControl.BackColor = clsGlobalColor.PrimaryColor;
                }
            }
        }


        private int LoadFile(String _path )
        {
            try
            {
                string[] FileList = Directory.GetFiles(_path);
                if (FileList != null)
                {
                    return FileList.Length;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private  void LoadContent()
        {
            string VertualPath = PrimaryContentPath + @"\Animation";
            lblAnimationCount.Invoke(( MethodInvoker)(()=>
                {
                   lblAnimationCount.Text = LoadFile(VertualPath).ToString();
                })); 
            VertualPath = "";
            VertualPath = PrimaryContentPath + @"\Image";
            lblImageCount.Invoke((MethodInvoker)(()=>
            {
                lblImageCount.Text = LoadFile(VertualPath).ToString();
            }));
          
            VertualPath = "";
            VertualPath = PrimaryContentPath + @"\File";
            lblFileCount.Invoke((MethodInvoker)(() =>
            {
                lblFileCount.Text = LoadFile(VertualPath).ToString();
            }));

            lblSecondaryCount.Invoke((MethodInvoker)(() =>
            {
                lblSecondaryCount.Text = LoadFile(SecondaryContentPath).ToString();
            }));

            
            
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
        
            string dirname = "";
            MFReaderClass objReader = new MFReaderClass();
                try
                {
                    DtContentList.Clear();
                    //fetching all video content.
                    dirname = PrimaryContentPath + @"\ANIMATION";
                    GetContentDetail(dirname, ref objReader, "VIDEO");
                    //Geting all Image Video content
                    dirname = PrimaryContentPath + @"\IMAGE";
                    GetContentDetail(dirname, ref objReader, "IMAGE");
                    //Geting all File content
                    dirname = PrimaryContentPath + @"\File";
                    GetContentDetail(dirname, ref objReader, "FILE");              
                 
                }
                catch (Exception ex)
                {
                   
                }
                finally
                {
                    DtContentList.WriteXml("D:\\Content.xml");
                    if (objReader != null)
                    {
                        objReader.ReaderClose();
                        releaseComObj(objReader);
                    }
                }
        }
        internal void releaseComObj(object comObj)
        {
            if (comObj != null)
            {
                try
                {
                    Marshal.ReleaseComObject(comObj);
                    comObj = null;
                }
                catch { }
            }
        }

        private void GetContentDetail(string dirname, ref MFReaderClass objReader, string Type)
        {
            string FileName = "", Duration = "00:00:00:00", Extension = "";

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
                            DataRow objrow = DtContentList.NewRow();
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


        private void btnLoadContent_Click(object sender, EventArgs e)
        {
            LoadContent();
        }



    }
}
