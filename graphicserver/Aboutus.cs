using System;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class Aboutus : Form
    {
        FrmMainServer ObjMainServer;
        public Aboutus()
        {
            InitializeComponent();
        }
        public Aboutus(FrmMainServer obj)
        {
            InitializeComponent();
            this.ObjMainServer = obj;
        }
        private void Aboutus_Load(object sender, EventArgs e)
        {
            
            
            ProductVersionnumber.Text = "Version Number: " + Application.ProductVersion;

#if UTC_MODE
 Companylbl.Text ="Planetcast Media Services Ltd © "+DateTime.UtcNow.Year;
#else
            Companylbl.Text = "Planetcast Media Services Ltd © " + DateTime.Now.Year;
#endif
            lblDongleExpDate.Text = "License Expiry Date " + ObjMainServer.LicEndDate;
        }        
        private void AboutusCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Mainaboutpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
