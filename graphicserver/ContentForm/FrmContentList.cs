using graphicserver.ContentForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class FrmContentList : Form
    {
        public FrmContentList()
        {
            InitializeComponent();
        }
        private void DesableButton()
        {
            foreach (Control item in pnlMenu.Controls)
            {
               if( item.GetType()==typeof(Button))
                {
                    item.BackColor = Color.FromArgb(50, 51, 76);
                    item.ForeColor = Color.Gainsboro;
                    item.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); 
                }
            }
        }
        Button CurrentButton;
        private void ActivateButton(object btnSender)
        {
            if(btnSender!=null)
            {
                if(CurrentButton!=(Button) btnSender)
                {
                    DesableButton();
                    CurrentButton = (Button)btnSender;
                    Color col= ClsBackGround.SelectTheamColor();
                    CurrentButton.BackColor = col;
                    CurrentButton.ForeColor = Color.White;
                    CurrentButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnlTitle.BackColor= col;
                    pnlLogo.BackColor = ClsBackGround.ChangeWritness(col, -0.3);
                    clsGlobalColor.PrimaryColor = col;
                }
            }
        }
        private void FrmContentList_Load(object sender, EventArgs e)
        {

        }

        private void BtnRefreshContentList_Click(object sender, EventArgs e)
        {
            OpenClildForm(new FrmRefreshContent(),sender);
            
        }
        Form OpenChidform = null;
        private void OpenClildForm( Form _form ,object Btnsender)
        {
            if(OpenChidform!=null)
            {
                OpenChidform.Close();
            }
            ActivateButton(Btnsender);
            OpenChidform = _form;
            OpenChidform.TopLevel = false;
            OpenChidform.FormBorderStyle = FormBorderStyle.None;
            OpenChidform.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(OpenChidform);
            pnlDesktop.Tag = OpenChidform;
           
            OpenChidform.BringToFront();
            OpenChidform.Show();
            lblFormName.Text = OpenChidform.Text;
            BtnChildClose.Visible = true;

        }
        private void BtnContentDetail_Click(object sender, EventArgs e)
        {
            OpenClildForm(new ContentDetail(),sender);
           
        }

        private void BtnDbContent_Click(object sender, EventArgs e)
        {
            OpenClildForm(new ContentDbList(),sender);
           
        }

        private void BtnClosePairent_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

               
            }
        }

        private void btnMinimise_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch { }
            
        }
        private Boolean _WindowState = false;
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (_WindowState == true)
            {
                _WindowState = false;
                this.WindowState = FormWindowState.Normal;
                btnRestore.Text = "1";
            }
            else
            {
                _WindowState = true;
                this.WindowState = FormWindowState.Maximized;
                btnRestore.Text = "2";
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void BtnChildClose_Click(object sender, EventArgs e)
        {
            if(OpenChidform!=null)
            {
                OpenChidform.Close();
                OpenChidform = null;
            }
            ResetUI();
        }

        private void ResetUI()
        {
            DesableButton();
            pnlTitle.BackColor = Color.Teal;
            pnlLogo.BackColor = Color.FromArgb(0, 89, 89);
            lblFormName.Text = "HOME";
            BtnChildClose.Visible = false;
        }
    }
}
