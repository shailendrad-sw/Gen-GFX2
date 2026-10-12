using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class TempPreview : Form
    {
        FrmMainServer ObjMainfrm = null;
        public TempPreview(FrmMainServer obj)
        {
            InitializeComponent();
            ObjMainfrm = obj;
        }

        private void TempPreview_FormClosed(object sender, FormClosedEventArgs e)
        {

           
            
        }

        private void TempPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
        
            ObjMainfrm.isPreview = false;
            this.Hide();
            e.Cancel = true;

          //  if (e.CloseReason == CloseReason.WindowsShutDown) return;
        }

        private void TempPreview_MaximumSizeChanged(object sender, EventArgs e)
        {
           
        }

        private void TempPreview_Resize(object sender, EventArgs e)
        {
          //  ObjMainfrm.m_objPreviewTemp.PreviewWindowSet("", panel1.Handle.ToInt32());             
        }

        private void TempPreview_Load(object sender, EventArgs e)
        {
            this.Text = ObjMainfrm.ChannelName;
        }
    }
}
