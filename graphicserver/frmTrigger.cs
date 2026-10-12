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
using System.IO;
namespace graphicserver
{
    public partial class frmTrigger : Form
    {
        DataTable DtTrigger = null;
        string triggerpath ="";
        FrmMainServer ObjMain = null;
        public frmTrigger(FrmMainServer ObjMain)
        {
            InitializeComponent();
            this.ObjMain = ObjMain;
        }
        private void CreateTrigerStructure()
        {
            try
            {
                if (DtTrigger == null)
                {
                    DtTrigger = new DataTable("Task");
                }
                DtTrigger.Columns.Add("Srno");
                DtTrigger.Columns.Add("ItemId");
                DtTrigger.Columns.Add("PropertyName");
                DtTrigger.Columns.Add("PropertyValue");
                DtTrigger.Columns.Add("Type");
                DtTrigger.Columns.Add("ChangeTime");
                DtTrigger.Columns.Add("GfxObject");
                DtTrigger.Columns.Add("Event");
            }
            catch { }
        }

        private void frmTrigger_Load(object sender, EventArgs e)
        {
            try
            {
                triggerpath = "";
                this.FormBorderStyle = FormBorderStyle.None;
                CreateTrigerStructure();
                cmbEvent.SelectedIndex = 0;
                cmbGfxObject.SelectedIndex = 0;
                cbType.SelectedIndex = 0;
                cbType.SelectedIndex = 0;
                DgvTrigger.AutoGenerateColumns = false;
                DgvTrigger.DataSource = DtTrigger;
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

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Trigger File|*.tgr";
                open.Multiselect = false;
                if (DialogResult.OK == open.ShowDialog())
                {
                    txtTrigername.Text = Path.GetFileNameWithoutExtension(open.FileName);
                    triggerpath = open.FileName;
                    DataSet ds = new DataSet();
                    ds.ReadXml(triggerpath);
                    if(ds.Tables["TASK"]!=null && ds.Tables["TASK"].Rows.Count>0)
                    {
                        DtTrigger = ds.Tables["TASK"].Copy();
                    }
                    DgvTrigger.DataSource = DtTrigger;
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("BtnLoad_Click", $" Error in Loading Trigger File... error is  = {ex.ToString()}", ObjMain.ChannelName);
            }
        }


        #region "Code for Moving Form"
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Headpanel_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        #endregion

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Trigger File|*.tgr";
                saveFileDialog1.Title = "Save Trigger File";
                saveFileDialog1.DefaultExt = ".tgr";
                if (DtTrigger.Rows.Count == 0)
                {
                    MessageBox.Show("Add data corrospond to any Event first,no data found to write");
                    return;
                }                                   
                if (DialogResult.OK== saveFileDialog1.ShowDialog())
                {
                    DtTrigger.WriteXml(saveFileDialog1.OpenFile());
                }
              
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            AddTriggerData();
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                if (DtTrigger == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(srnotxt.Text))
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Blank Srno not Allowed !", "OK", "Exclamation");
                    return;
                }
                if (!DtTrigger.AsEnumerable().Any(r => r.Field<string>("SrNo") == srnotxt.Text.Trim()))
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Srno does not Exists!", "OK", "Exclamation");
                    return;
                }
                if (SRno.Trim()!= srnotxt.Text.Trim())
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Srno changing not allowed !", "OK", "Exclamation");
                    return;
                }
                DataRow dr = DtTrigger.Select("SrNo='" + srnotxt.Text + "'").FirstOrDefault();
                if (dr != null)
                {
                    dr["ItemId"] = txtelementid.Text.Trim();
                    dr["PropertyName"] = txtPropertyName.Text.Trim();
                    dr["PropertyValue"] = txtPropertyValue.Text;
                    dr["ChangeTime"] = txtIntroTime.Text.Trim();
                    dr["GfxObject"] = cmbGfxObject.SelectedItem.ToString();
                    dr["Event"] = cmbEvent.SelectedItem.ToString();
                    dr["Type"] = cbType.SelectedItem.ToString();                   
                    DtTrigger.AcceptChanges();
                 //   SaveFile();
                  //  FillGrid();   
                                  
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("btnUpdate_Click", $" Error in update row in trigger = {ex.ToString()}", ObjMain.ChannelName);
            }
        }

        private void SaveFile()
        {
            try
            {
                if(File.Exists(triggerpath))
                {
                    DtTrigger.WriteXml(triggerpath);
                }else
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Trigger File|*.tgr";
                    saveFileDialog1.Title = "Save Trigger File";
                    saveFileDialog1.DefaultExt = ".tgr";
                    if (DialogResult.OK == saveFileDialog1.ShowDialog())
                    {
                        DtTrigger.WriteXml(saveFileDialog1.OpenFile());
                    }
                }

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("btnUpdate_Click", $" Error in saving trigger = {ex.ToString()}", ObjMain.ChannelName);
            }
        }
        
        public void AddTriggerData()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(srnotxt.Text))
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "SrNo are blank please fill", "OK", "Exclamation");
                    return;
                }
                DataRow Checkdata = DtTrigger.Select("SrNo='" + srnotxt.Text + "'").FirstOrDefault();
                if (Checkdata == null )
                {
                    DataRow dr = DtTrigger.NewRow();
                    dr.SetField("SrNo", srnotxt.Text.Trim());
                    dr.SetField("ItemId", txtelementid.Text.Trim());
                    dr.SetField("PropertyName", txtPropertyName.Text.Trim());
                    dr.SetField("PropertyValue", txtPropertyValue.Text.Trim());
                    dr.SetField("ChangeTime", txtIntroTime.Text.Trim());
                    dr.SetField("GfxObject", cmbGfxObject.SelectedItem.ToString());
                    dr.SetField("Event", cmbEvent.SelectedItem.ToString());
                    dr.SetField("Type", cbType.SelectedItem.ToString());
                    DtTrigger.Rows.Add(dr);
                    DtTrigger.AcceptChanges();
                  //  SaveFile();
                    ClearTextBox();
                }
                else
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "SrNo duplicacy are not allow", "OK", "Exclamation");
                    
                    return;
                }
            }
            catch(Exception ex)
            {
                ObjMain.objlog.WriteError("btnUpdate_Click", $" Error in adding row in trigger = {ex.ToString()}", ObjMain.ChannelName);
            }

        }
        public void ClearTextBox()
        {
            try
            {
                srnotxt.Text = "";
                txtelementid.Text = "";
                txtPropertyName.Text = "";
                txtPropertyValue.Text = "";
                txtIntroTime.Text = "";
                cmbEvent.SelectedIndex=0;
                cmbGfxObject.SelectedIndex=0;
                cbType.SelectedIndex=0;
            }
            catch(Exception ex)
            {
                ObjMain.objlog.WriteError("ClearTextBox", "Error In Clear Control ... Error "+ ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow rows1 = DtTrigger.Select("SrNo='" + srnotxt.Text.Trim() + "'").FirstOrDefault();
                if (rows1 == null)
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Srno does not exist!", "OK", "Exclamation");
                    return;
                }
                DialogResult result = ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Are you sure delete the Record", "Yes~No", "Exclamation");
                if (result == DialogResult.Yes)
                {
                    DtTrigger.Rows.Remove(rows1);
                    DtTrigger.AcceptChanges();
                    ClearTextBox();
                   // SaveFile();
                    //DtTrigger.WriteXml(triggerpath);
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("btnDelete_Click", $"Error in delete rows  error->  {ex.ToString()}", ObjMain.ChannelName);
            }

        }
        string SRno = "-1";
        private void DgvTrigger_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(DgvTrigger.SelectedRows==null || DgvTrigger.SelectedRows.Count==0)
                {
                    return;
                }

                SRno= DgvTrigger.SelectedRows[0].Cells["colSrNO"].Value.ToString();
                srnotxt.Text = DgvTrigger.SelectedRows[0].Cells["colSrNO"].Value.ToString();
                txtelementid.Text = DgvTrigger.SelectedRows[0].Cells["colElementid"].Value.ToString();
                txtPropertyName.Text = DgvTrigger.SelectedRows[0].Cells["colPropertyname"].Value.ToString();
                txtPropertyValue.Text = DgvTrigger.SelectedRows[0].Cells["colPropertyvalue"].Value.ToString();
                txtIntroTime.Text = DgvTrigger.SelectedRows[0].Cells["colIntroTime"].Value.ToString();
                cmbGfxObject.SelectedItem = (DgvTrigger.SelectedRows[0].Cells["colGfxObject"].Value.ToString());
                cmbEvent.SelectedItem = (DgvTrigger.SelectedRows[0].Cells["colevent"].Value.ToString());
                cbType.SelectedItem = (DgvTrigger.SelectedRows[0].Cells["colType"].Value.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void srnotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
