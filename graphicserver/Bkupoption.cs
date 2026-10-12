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

namespace graphicserver
{
    public partial class Bkupoption : Form
    {
        public string fileNamewithpath = "";
        string filePath;
       public bool bBkupoptionflag = false;
        private bool _operationPerformed = true;
        public Bkupoption()
        {
            InitializeComponent();
            filePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + "BackupRestore.ini";
            if (File.Exists(filePath))
            {
                fileNamewithpath = BkupPathtextBox.Text = File.ReadAllText(filePath);
            }
        }

        private void BtnBkupOK_Click(object sender, EventArgs e)
        {
            if (fileNamewithpath.Trim() != "")
            {
                fileNamewithpath.Trim();
                filePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + "BackupRestore.ini";
                File.WriteAllText(filePath, fileNamewithpath);
                bBkupoptionflag = false;
                _operationPerformed = false;
            }
            else
            {
                MessageBox.Show("Please provide backup path");

            }
            this.Close();
        }

        private void BtnBKUPBROWSE_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    fileNamewithpath = BkupPathtextBox.Text = folderDlg.SelectedPath;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btpBkupCancel_click(object sender, EventArgs e)
        {
            _operationPerformed = true;
            this.Close();
        }

        private void Bkupoption_FormClosing(object sender, FormClosingEventArgs e)
        {
            bBkupoptionflag = _operationPerformed;
           
        }
    }
}
