using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver.ContentForm
{
    public partial class ContentDbList : Form
    {
        public ContentDbList()
        {
            InitializeComponent();
        }

        private void ContentDbList_Load(object sender, EventArgs e)
        {

        }
        public void LoadTheam(Color _Color)
        {
            foreach (Control objControl in this.Controls)
            {
                if (objControl.GetType() == typeof(Button))
                {
                    objControl.BackColor = _Color;
                }
                if (objControl.GetType() == typeof(Label) && objControl.Name.ToLower().Contains("heading"))
                {
                    objControl.BackColor = _Color;
                }
            }
        }
    }
}
