using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace graphicserver.WPF
{
    /// <summary>
    /// Interaction logic for SubtitleEdit.xaml
    /// </summary>
    public partial class SubtitleEdit : UserControl
    {
        DataTable dtEditSetting = null;
        public string SubtitleInfoFile { get; set; }
        public SubtitleEdit( string path)
        {
            InitializeComponent();
            SubtitleInfoFile = path;
            DesignDt();
            DataContext = this;
            LoadConfig();
            DgvData.ItemsSource = dtEditSetting.DefaultView;
        }

        List<string> bgSource = null;
        public List<string> BgSource
        {
            get
            {
                bgSource = new List<string>();
                bgSource.Add("yes");
                bgSource.Add("no");
                return bgSource;
            }
        }

       
        private void DesignDt()
        {
            if (dtEditSetting == null)
            {
                dtEditSetting = new DataTable("ConfigData");
            }
            dtEditSetting.Columns.Add("SrNo");
            dtEditSetting.Columns.Add("ReceiveText");
            dtEditSetting.Columns.Add("ScreenPosition");
            dtEditSetting.Columns.Add("ItemPosition");
            dtEditSetting.Columns.Add("left");
            dtEditSetting.Columns.Add("right");
            dtEditSetting.Columns.Add("top");
            dtEditSetting.Columns.Add("bottom");
            dtEditSetting.Columns.Add("useforbg");
        }

        private void LoadConfig()
        {
            string path = SubtitleInfoFile;
            DataSet ObjDs = new DataSet();
            ObjDs.ReadXml(path);
            if (ObjDs.Tables["ConfigData"] != null)
            {
                dtEditSetting = ObjDs.Tables["ConfigData"].Copy();
            }            
        }

        private void TakeBackup()
        {
            FileInfo pss = new FileInfo(SubtitleInfoFile);
            string NewDir = pss.DirectoryName+ @"\backupfile";
#if UTC_MODE
           string newpath = NewDir+@"\HITS_SubtitleInfo " + (DateTime.UtcNow).ToString("dd-MM-yyyy HH-mm-ss") + ".xml";
#else
            string newpath = NewDir + @"\HITS_SubtitleInfo " + (DateTime.Now).ToString("dd-MM-yyyy HH-mm-ss") + ".xml";
#endif


            string SourcePath=pss.FullName;
            if(!Directory.Exists(NewDir))
            {
                Directory.CreateDirectory(NewDir);
            }
            File.Copy(SourcePath, newpath);
            
        }
        private async void SaveSubtitleFile()
        {
            try
            {
                TakeBackup();
                btnSave.Content = "Saving..";
                dtEditSetting.WriteXml(SubtitleInfoFile,XmlWriteMode.WriteSchema);
                await Task.Delay(2000);
                btnSave.Content = "Done";
                await Task.Delay(2000);
                btnSave.Content = "Update";
            }
            catch (Exception ex)
            {
                btnSave.Content = "Try Again";
            }
           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSubtitleFile();
        }
    }
}
