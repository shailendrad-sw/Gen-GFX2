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
    public partial class RestoreOption : Form
    {
        public Action<string> RecievedData;
        DataTable _customDatatable = null;
        bool _handleCreated = false;
        private string SourcePath="";
        public RestoreOption()
        {
            InitializeComponent();
           // _customDatatable = new DataTable();
            //_customDatatable.Columns.Add(new DataColumn("RestorePoint"));
            //DataRow dr = _customDatatable.NewRow();
            //dr["RestorePoint"] = "add";
            //_customDatatable.Rows.Add(dr);
         //   custom_dgv.DataSource = _customDatatable.DefaultView;
        }
        private void HandleCreatedHandler(object sender, EventArgs e)
        {
            _handleCreated = true;
        }
        public void SetData(DataTable dt)
        {
            _customDatatable = dt;
            if (_handleCreated)
                this.Invoke(new MethodInvoker(delegate { SetDataInvoker(_customDatatable); }));
            else
                SetDataInvoker(_customDatatable);
        }
        void SetDataInvoker(DataTable dt)
        {
            custom_dgv.DataSource = dt.DefaultView;
        }
        private void DownloadRspoint_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (DataGridViewRow dr in custom_dgv.Rows)
            {
                if (dr.Selected)
                    break;
                index++;
            }
            DataTable temp = _customDatatable.Copy();
            DataRow current = temp.Rows[index];
            StringBuilder str = new StringBuilder();
            foreach (string curr in current.ItemArray)
            {
                str.Append(curr);
                str.Append(":");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
            RecievedData?.Invoke(str.ToString().Trim(':'));
            
        }
    }
}
