using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class GridForm : Form
    {

        FrmMainServer objmainserver;
        public DataTable _setgriddt;
        public DataRow dr;
        string Columnnamevalue = "";
        public GridForm()
        {
            InitializeComponent();

        }
        public GridForm(FrmMainServer myobj)
        {
            InitializeComponent();
            this.objmainserver = myobj;
        }
        private void Savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ColumnNameCmb.Text))
                {
                    objmainserver.CustomMessageBox.ShowMessageBox(Application.ProductName, "Please Select Column Value First", "OK", "Exclamation");
                    return;
                }
                //Changing Values in DataTable
                dr.SetField("DisplayText", DisplayTextbox.Text);
                dr.SetField("ColumnWidth", ColumnWidthTextbox.Text);
                dr.SetField("Visible", Visiblecheckbox.Checked);
                dr.SetField("ToolTipText", Tooltiptextbox.Text);
                dr.SetField("Resizable", resizablecheckbox.Checked);
                dr.SetField("Frozen", frozencheckbox.Checked);
                _setgriddt.AcceptChanges();
                _setgriddt.WriteXml(objmainserver.XMLGridFormat, XmlWriteMode.WriteSchema);
                objmainserver.WriteSecondaryFile();
                objmainserver.SetGridControl();
                objmainserver.CustomMessageBox.ShowMessageBox(Application.ProductName, "Grid Columns Reset", "OK", "Exclamation");
            }
            catch (Exception)
            { }
        }

        private void GridForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                DgvGridForm.AutoGenerateColumns = false;
                DataSet ds = new DataSet();
                ds.ReadXml(objmainserver.XMLGridFormat);
                _setgriddt = ds.Tables["GridFormate"];
                ColumnNameCmb.DataSource = _setgriddt;
                ColumnNameCmb.DisplayMember = "ColumnName";
                ColumnNameCmb.ValueMember = "ColumnName";
                DgvGridForm.DataSource = _setgriddt;
                GridviewsortDisable(DgvGridForm);
                DgvGridForm.Columns["ToolTip"].Width = 200;
            }
            catch (Exception ex)
            {

                
            }
         

        }

        private void ColumnNameCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Columnnamevalue == ColumnNameCmb.Text) { return; }

                Columnnamevalue = ColumnNameCmb.Text;

                dr = _setgriddt.Select("ColumnName='" + Columnnamevalue + "'").FirstOrDefault();
                if (dr == null)
                {
                    objmainserver.objlog.WriteEvents("ColumnNameCmb_SelectedIndexChanged", $"{Columnnamevalue} Column does not have any value", objmainserver.ChannelName);
                    return;
                }
                //Actual Values
                DisplayTextbox.Text = dr["DisplayText"].ToString();
                ColumnWidthTextbox.Text = dr["ColumnWidth"].ToString();
                Visiblecheckbox.Checked = Convert.ToBoolean(dr["Visible"]);
                Tooltiptextbox.Text = dr["ToolTipText"].ToString();
                resizablecheckbox.Checked = Convert.ToBoolean(dr["Resizable"]);
                frozencheckbox.Checked = Convert.ToBoolean(dr["Frozen"]);
            }
            catch (Exception)
            { }
        }

        private void GridCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GridviewsortDisable(DataGridView dgv)
        {
            try
            {
                dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(Column => Column.SortMode = DataGridViewColumnSortMode.NotSortable);
            }
            catch { }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
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



        ///////////////////////////////////////////////////////////
    }
}
