using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    /// <summary>
    /// Added User Manual Document in Help Menu. (13/01/2025)
    /// </summary>
    public partial class UserManual : Form
    {
        private string UserManualPath = "";
        private FrmMainServer FrmMainServer;
        public UserManual(string UserManualPath, FrmMainServer mainServer)
        {
            InitializeComponent();

            this.UserManualPath = UserManualPath;
            this.FrmMainServer = mainServer;
        }



        private void UserManualForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(UserManualPath))
                {
                    userManualViewer.LoadDocument(UserManualPath);
                }
            }
            catch (Exception ex)
            {
                FrmMainServer.objlog.WriteError("CreateChannels", "Error in creating new channels " + ex.ToString(), "");
            }
        }


    }
}
