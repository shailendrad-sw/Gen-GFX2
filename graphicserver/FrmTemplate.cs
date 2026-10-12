using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class FrmTemplate : Form
    {
        public DataTable TemplateDT;

        Usrcntrlparam objTmplte;
        FrmMainServer ObjMain = null;
        string TemplatePath = "";
        public FrmTemplate()
        {
            InitializeComponent();

        }
        public FrmTemplate(FrmMainServer ObjMain)
        {
            InitializeComponent();
            this.ObjMain = ObjMain;
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(  txtsrno.Text) )
            {                
                ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "SrNo are blank please fill", "OK", "Exclamation");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTemplateName.Text))
            {
                ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "TemplateName are blank please fill", "OK", "Exclamation");
                return;
            }
            int TotalParameter; ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out TotalParameter);
            DataRow Checkdata = TemplateDT.Select("SrNo='" + txtsrno.Text + "'").FirstOrDefault();
            DataRow CheckTemplate = TemplateDT.Select("templatename='" + txtTemplateName.Text + "'").FirstOrDefault();
            if (Checkdata == null && CheckTemplate == null)
            {
                DataRow dr = TemplateDT.NewRow();
                dr.SetField("SrNo", txtsrno.Text.Trim());
                dr.SetField("TemplateName", txtTemplateName.Text.Trim());
                dr.SetField("LayerNo", txtLayerNo.Text.Trim());
                string Checkextension = txtFileExtension.Text.Trim();
                if (!Checkextension.Contains("."))
                {
                    txtFileExtension.Text = "." + Checkextension;
                }
                dr.SetField("FileExtension", txtFileExtension.Text.Trim());

                dr.SetField("ChannelName", txtChannelName.Text.Trim());
                dr.SetField("BugType", txtbugtype.Text.Trim());
                dr.SetField("KillwithPrimary", cmbkillwithprimary.SelectedItem.ToString());
                dr.SetField("ShowOnBreak", cmbshowonbreak.SelectedItem.ToString());
                dr.SetField("BugStatus", cmbbugstatus.SelectedItem.ToString());
                //dr.SetField("Param1", txtparamone.Text.Trim());
                //dr.SetField("Param2", txtparamtwo.Text.Trim());
                //dr.SetField("Param3", txtparamthree.Text.Trim());
                //dr.SetField("Param4", txtparamfour.Text.Trim());
                //dr.SetField("Param5", txtparamfive.Text.Trim());
                //dr.SetField("Param6", txtparamsix.Text.Trim());
                //dr.SetField("Param7", txtparamseven.Text.Trim());
                //dr.SetField("Param8", txtparameight.Text.Trim());
                //dr.SetField("Param9", txtparamnine.Text.Trim());
                //dr.SetField("Param10", txtparamten.Text.Trim());
                int y = 0;
                try
                {
                    for (int j = 0; j < objTmplte.dataGridView1.RowCount - 1; j++)
                    {
                        y = j + 1;
                        String p = "Param" + y.ToString();
                        if (objTmplte.dataGridView1.Rows[j].Cells[0].Value != null)
                        {
                            String str = objTmplte.dataGridView1.Rows[j].Cells[0].Value.ToString();
                            dr.SetField(p, str.Trim());
                        }
                        else if (j != (objTmplte.dataGridView1.RowCount - 1))
                        {
                            String str = "";
                            dr.SetField(p, str.Trim());
                        }
                        else
                        {
                            y = j;
                            break;
                        }
                    }

                    if ((objTmplte.dataGridView1.RowCount - 1) < (TotalParameter))
                    {
                        for (int j = y; j < TotalParameter; j++)
                        {
                            y = j + 1;
                            String p = "Param" + y.ToString();
                            dr.SetField(p, String.Empty);
                        }
                    }
                    objTmplte.dataGridView1.Rows.Clear();
                }
                catch (Exception ex)
                {

                }

                dr.SetField("DBTemplate",cbDbTemplate.SelectedItem.ToString());
                dr.SetField("DBTableName", txtdbTablename.Text);
                dr.SetField("DBColumnName", txtdbcolumn.Text);
                dr.SetField("DBCondition", txtdbcondition.Text);
                TemplateDT.Rows.Add(dr);
                TemplateDT.AcceptChanges();
                TemplateDT.WriteXml(ObjMain.XMLTemplateConfigfilepath, XmlWriteMode.WriteSchema);
                FillGrid();
                ObjMain.objlog.WriteEvents("btn_Add_Click", $"{ txtTemplateName.Text} template add successfully ", ObjMain.ChannelName);
                ClearTextControlData();
            }
            else
            {
                ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "SrNo and TemplateName duplicacy are not allow", "OK", "Exclamation");
                ObjMain.objlog.WriteError("btn_Add_Click", "SrNo and TemplateName duplicacy are not allow", ObjMain.ChannelName);
                return;
            }


        }
        private void FillGrid()
        {
            //string TemplatePath = Application.StartupPath + $"//Channels//{ObjMain.ChannelName}//{ObjMain.ChannelName}_TemplateInfo.xml";
            if (File.Exists(TemplatePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(TemplatePath);
                if (ds.Tables["ConfigData"] != null)
                {
                    TemplateDT = ds.Tables["ConfigData"].Copy();
                }
                DgvTemplate.DataSource = null;
                DgvTemplate.DataSource = TemplateDT;

            }
        }
        private void CreateDatatable(int xcount = 0)
        {
            TemplateDT = new DataTable("ConfigData");
            TemplateDT.Columns.Add("SrNo" ,typeof(int)  );
            TemplateDT.Columns.Add("TemplateName");
            TemplateDT.Columns.Add("LayerNo");
            TemplateDT.Columns.Add("FileExtension");
            TemplateDT.Columns.Add("ChannelName");
            TemplateDT.Columns.Add("BugType");
            TemplateDT.Columns.Add("KillwithPrimary");
            TemplateDT.Columns.Add("ShowOnBreak");
            TemplateDT.Columns.Add("BugStatus");
            //TemplateDT.Columns.Add("Param1");
            //TemplateDT.Columns.Add("Param2");
            //TemplateDT.Columns.Add("Param3");
            //TemplateDT.Columns.Add("Param4");
            //TemplateDT.Columns.Add("Param5");
            //TemplateDT.Columns.Add("Param6");
            //TemplateDT.Columns.Add("Param7");
            //TemplateDT.Columns.Add("Param8");
            //TemplateDT.Columns.Add("Param9");
            //TemplateDT.Columns.Add("Param10");
            if (xcount > 0)
            {
                String a;
                for (int j = 1; j <= xcount; j++)
                {
                    a = j.ToString();
                    a = "Param" + a;
                    TemplateDT.Columns.Add(a);
                }
            }
            TemplateDT.Columns.Add("DBTemplate");
            TemplateDT.Columns.Add("DBTableName");
            TemplateDT.Columns.Add("DBColumnName");
            TemplateDT.Columns.Add("DBCondition");
            TemplateDT.Columns.Add("IntroTime");
            TemplateDT.Columns.Add("ExitTime");
            TemplateDT.Columns.Add("DurationBased");
            TemplateDT.Columns.Add("EndOffset");
            TemplateDT.Columns.Add("StartOffset"); 
        }
        private void FrmTemplate_Load(object sender, EventArgs e)
        {

            try
            {
                objTmplte = new Usrcntrlparam();
                // objTmplte.Location = new Point(200, 25);
                objTmplte.Location = new Point(200,4);
                objTmplte.Width = 350;
                objTmplte.Height = 200;
                HeadePanel.Controls.Add(objTmplte);
                objTmplte.Visible = true;
                this.FormBorderStyle = FormBorderStyle.None;
              //  CreateDatatable();
                int Totalrecord = 0;
              //  DgvTemplate.AutoGenerateColumns = false;
                TemplatePath = Application.StartupPath + $"//Channels//{ObjMain.ChannelName}//{ObjMain.ChannelName}_TemplateInfo.xml";
                txtChannelName.Text = ObjMain.ChannelName;
                cmbkillwithprimary.SelectedIndex = 0;
                cmbshowonbreak.SelectedIndex = 0;
                cmbbugstatus.SelectedIndex = 0;
                cbDbTemplate.SelectedIndex = 0;
                cbDurationBased.SelectedIndex = 0;
                if (File.Exists(TemplatePath))
                {
                    //DataSet ds = new DataSet();
                    //ds.ReadXml(TemplatePath);
                    //if(ds.Tables["ConfigData"]!=null)
                    //{
                    //    TemplateDT=ds.Tables["ConfigData"].Copy();
                    //   // TemplateDT = ds.Tables["ConfigData"].Merge(TemplateDT);
                    //}                     
                    //Totalrecord = TemplateDT.Rows.Count;
                    //DgvTemplate.DataSource = null;
                    //DgvTemplate.DataSource = TemplateDT;
                    int count;
                    ObjMain.MaxParamFinder(TemplatePath, out count);
                    CreateDatatable(count);
                    DgvTemplate.AutoGenerateColumns = true;
                    DgvTemplate.DataSource = TemplateDT;
                    DataSet ds = new DataSet();
                    ds.ReadXml(TemplatePath);
                    if (ds.Tables["ConfigData"] != null)
                    {
                        TemplateDT = ds.Tables["ConfigData"].Copy();
                    }
                    Totalrecord = TemplateDT.Rows.Count;
                    DgvTemplate.DataSource = null;
                    DgvTemplate.DataSource = TemplateDT;

                }
                ObjMain.objlog.WriteEvents("FrmTemplate_Load", $"Template Setting Form Open Successfully Template File Existence={File.Exists(TemplatePath).ToString()} Total Record In Datatable ={Totalrecord}", ObjMain.ChannelName);
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("FrmTemplate_Load", $"Error In Opening Template Config Form .", ObjMain.ChannelName);
            }
        }

        private void btn_updatetemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (TemplateDT == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTemplateName.Text))
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Blank Templetename are not allowed to change !", "OK", "Exclamation");
                    return;
                }
                int TotalParameter; ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out TotalParameter);
                DataRow dr = TemplateDT.Select("SrNo='" + txtsrno.Text + "'").FirstOrDefault();
                if (dr != null)
                {
                    if ((dr["templatename"] + "").Trim().ToUpper() != txtTemplateName.Text.Trim().ToUpper())
                    {
                        ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Templetename are not allowed to change !", "OK", "Exclamation");
                        return;
                    }
                    dr["templatename"] = txtTemplateName.Text.Trim();
                    dr["LayerNo"] = txtLayerNo.Text.Trim();
                    if (!txtFileExtension.Text.Contains("."))
                    {
                        txtFileExtension.Text = "." + txtFileExtension.Text;
                    }
                    dr["FileExtension"] = txtFileExtension.Text;
                    dr["ChannelName"] = txtChannelName.Text.Trim();
                    dr["BugType"] = txtbugtype.Text.Trim();
                    dr["KillWithPrimary"] = cmbkillwithprimary.SelectedItem.ToString();
                    dr["ShowOnBreak"] = cmbshowonbreak.SelectedItem.ToString();
                    dr["BugStatus"] = cmbbugstatus.SelectedItem.ToString();
                    dr["Param1"] = txtparamone.Text.Trim();
                    dr["Param2"] = txtparamtwo.Text.Trim();
                    dr["Param3"] = txtparamthree.Text.Trim();
                    //dr["Param4"] = txtparamfour.Text.Trim();
                    //dr["Param5"] = txtparamfive.Text.Trim();
                    //dr["Param6"] = txtparamsix.Text.Trim();
                    //dr["Param7"] = txtparamseven.Text.Trim();
                    //dr["Param8"] = txtparameight.Text.Trim();
                    //dr["Param9"] = txtparamnine.Text.Trim();
                    //dr["Param10"] = txtparamten.Text.Trim();
                    int y = 0;
                    try
                    {
                        // foreach (DataGridViewRow rw in ParamsConfigGrid.Rows)
                        for (int j = 0; j < objTmplte.dataGridView1.RowCount - 1; j++)
                        {
                            // String str = rw.Cells[0].Value.ToString();
                            y = j + 1;
                            String p = "Param" + y.ToString();
                            if (objTmplte.dataGridView1.Rows[j].Cells[0].Value != null)
                            {
                                String str = objTmplte.dataGridView1.Rows[j].Cells[0].Value.ToString();
                                dr.SetField(p, str.Trim());
                            }
                            else
                            {
                                y = j;
                                break;
                            }
                        }
                        if ((objTmplte.dataGridView1.RowCount - 1) <= (TotalParameter))
                        {
                            for (int j = y; j < TotalParameter; j++)
                            {
                                y = j + 1;
                                String p = "Param" + y.ToString();
                                dr.SetField(p, String.Empty);
                            }
                        }
                        objTmplte.dataGridView1.Rows.Clear();
                    }
                    catch (Exception ex)
                    {

                    }
                    dr["DBTemplate"] = cbDbTemplate.SelectedItem.ToString();
                    dr["DBTableName"] = txtdbTablename.Text.Trim();
                    dr["DBColumnName"] = txtdbcolumn.Text.Trim();
                    dr["DBCondition"] = txtdbcondition.Text.Trim();

                    dr["IntroTime"] = txtIntroTime.Text.Trim();
                    dr["ExitTime"] = txtExitTime.Text.Trim();
                    dr["DurationBased"] = cbDurationBased.SelectedItem.ToString();
                    dr["EndOffset"] = txtEndoffset.Text.Trim();
                    dr["StartOffset"] = txtStartOffset.Text.Trim();

                    TemplateDT.AcceptChanges();
                    TemplateDT.WriteXml(ObjMain.XMLTemplateConfigfilepath, XmlWriteMode.WriteSchema);
                    ObjMain.objlog.WriteEvents("btn_updatetemplate_Click", $"{ txtTemplateName.Text} template update successfully by user ", ObjMain.ChannelName);
                }
                else
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Srno does not exist!", "OK", "Exclamation");
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("btn_updatetemplate_Click", $"{ txtTemplateName.Text} template does not update", ObjMain.ChannelName);
            }


        }

        private void DgvTemplate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int totalParam = 0;
                ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out totalParam);
                txtsrno.Text = DgvTemplate.SelectedRows[0].Cells["SrNo"].Value+"";
                txtTemplateName.Text = DgvTemplate.SelectedRows[0].Cells["Templatename"].Value + "";
                txtLayerNo.Text = DgvTemplate.SelectedRows[0].Cells["layerno"].Value + "";
                txtFileExtension.Text = DgvTemplate.SelectedRows[0].Cells["fileextension"].Value + "";
                txtChannelName.Text = DgvTemplate.SelectedRows[0].Cells["channelname"].Value + "";
                txtbugtype.Text = DgvTemplate.SelectedRows[0].Cells["bugtype"].Value + "";
                cmbkillwithprimary.Text = DgvTemplate.SelectedRows[0].Cells["killwithprimary"].Value + "";
                cmbshowonbreak.Text = DgvTemplate.SelectedRows[0].Cells["showonbreak"].Value + "";
                cmbbugstatus.Text = DgvTemplate.SelectedRows[0].Cells["bugstatus"].Value + "";
                //txtparamone.Text = DgvTemplate.SelectedRows[0].Cells["parameter1"].Value + "";
                //txtparamtwo.Text = DgvTemplate.SelectedRows[0].Cells["parameter2"].Value + "";
                //txtparamthree.Text = DgvTemplate.SelectedRows[0].Cells["parameter3"].Value + "";
                //txtparamfour.Text = DgvTemplate.SelectedRows[0].Cells["parameter4"].Value + "";
                //txtparamfive.Text = DgvTemplate.SelectedRows[0].Cells["parameter5"].Value + "";
                //txtparamsix.Text = DgvTemplate.SelectedRows[0].Cells["parameter6"].Value + "";
                //txtparamseven.Text = DgvTemplate.SelectedRows[0].Cells["parameter7"].Value + "";
                //txtparameight.Text = DgvTemplate.SelectedRows[0].Cells["parameter8"].Value + "";
                //txtparamnine.Text = DgvTemplate.SelectedRows[0].Cells["parameter9"].Value + "";
                //txtparamten.Text = DgvTemplate.SelectedRows[0].Cells["parameter10"].Value + "";
                objTmplte.dataGridView1.Rows.Clear();
                String _str = "param";
                for (int i = 1; i <= totalParam; i++)
                {
                    _str = "Param" + i.ToString();
                    objTmplte.dataGridView1.Rows.Add(DgvTemplate.SelectedRows[0].Cells[_str].Value + "");

                }
                cbDbTemplate.SelectedItem=( DgvTemplate.SelectedRows[0].Cells["DBTemplate"].Value+"").ToLower();
                txtdbTablename.Text = DgvTemplate.SelectedRows[0].Cells["DBTableName"].Value + "";
                txtdbcondition.Text = DgvTemplate.SelectedRows[0].Cells["DBCondition"].Value + "";
                txtdbcolumn.Text = DgvTemplate.SelectedRows[0].Cells["DbColumnName"].Value + "";


                txtIntroTime.Text = DgvTemplate.SelectedRows[0].Cells["IntroTime"].Value + "";
                txtExitTime.Text= DgvTemplate.SelectedRows[0].Cells["ExitTime"].Value + "";
                cbDurationBased.SelectedItem = (DgvTemplate.SelectedRows[0].Cells["DurationBased"].Value.ToString()==string.Empty?"false": DgvTemplate.SelectedRows[0].Cells["DurationBased"].Value.ToString()).ToLower();
                txtEndoffset.Text= DgvTemplate.SelectedRows[0].Cells["Endoffset"].Value+"";
                txtStartOffset.Text= DgvTemplate.SelectedRows[0].Cells["Startoffset"].Value + "";
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("DgvTemplate_DoubleClick", $"Error in filling data in Control.."+ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void ClearTextControlData()
        {
            try
            {
                txtsrno.Text = string.Empty;
                txtTemplateName.Text = string.Empty;
                txtLayerNo.Text = string.Empty;
                txtFileExtension.Text = string.Empty;
                txtbugtype.Text = string.Empty;

                //txtKillwithPrimary.Text = string.Empty;
                //txtshowonbreak.Text = string.Empty;
                //txtbugStatus.Text = string.Empty;

                //txtparamone.Text = string.Empty;
                //txtparamtwo.Text = string.Empty;
                //txtparamthree.Text = string.Empty;
                //txtparamfour.Text = string.Empty;
                //txtparamfive.Text = string.Empty;
                //txtparamsix.Text = string.Empty;
                //txtparamseven.Text = string.Empty;
                //txtparameight.Text = string.Empty;
                //txtparamnine.Text = string.Empty;
                //txtparamten.Text = string.Empty;
                objTmplte.dataGridView1.Rows.Clear();
                txtdbcolumn.Text = string.Empty;
                txtdbTablename.Text = string.Empty;
                txtdbcondition.Text = string.Empty;
                cbDbTemplate.SelectedIndex =0;

                txtExitTime.Text = string.Empty;
                txtIntroTime.Text = string.Empty;
                cbDurationBased.SelectedIndex = 0;
                txtEndoffset.Text = string.Empty;
                txtStartOffset.Text = string.Empty;


            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ClearTextControlData", "Error in Clear Control .", ObjMain.ChannelName);
            }
        }

        private void btndeletetemplate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow rows1 = TemplateDT.Select("SrNo='" + txtsrno.Text.Trim() + "'").FirstOrDefault();
                if (rows1 == null)
                {
                    ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Srno does not exist!", "OK", "Exclamation");
                    return;
                }
                string templatename = txtTemplateName.Text;
                DialogResult result = ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Are you sure delete the Record", "Yes~No", "Exclamation");
                if (result == DialogResult.Yes)
                {
                    TemplateDT.Rows.Remove(rows1);
                    TemplateDT.AcceptChanges();
                    ClearTextControlData();
                    TemplateDT.WriteXml(ObjMain.XMLTemplateConfigfilepath, XmlWriteMode.WriteSchema);
                    ObjMain.objlog.WriteEvents("btndeletetemplate_Click", $"{ templatename} template delete successfully ", ObjMain.ChannelName);
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("btn_updatetemplate_Click", $"Error in delete template= { txtTemplateName.Text} error->  {ex.ToString()}", ObjMain.ChannelName);
            }
        }

        private void btn_uploadtoDB_Click(object sender, EventArgs e)
        {
            if (ObjMain.EnableDb == false) { return; }

            if (ObjMain.DbOnlyForAsrun == true) { return; }

            string Templatename ="", LayerNo="5", FileExtension=".mov", Channelname="", Bugtype="", Killwithprimary="false", Showonbreak="false", Bugstatus="true", Paramone="", Paramtwo="", Paramthree="", Paramfour="", Paramfive="", Paramsix="", Paramseven="", Parameight="", Paramnine="", Paramten="",DbTemplate="false",dbtablename="",dbcolumn="",dbcondition="";
                DialogResult result = ObjMain.CustomMessageBox.ShowMessageBox(Application.ProductName, "Are you sure insert/update the Record in database", "Yes~No", "Exclamation");
                if (result == DialogResult.Yes)
                {

                    try
                    {
                        DbLayer _db = new DbLayer(ObjMain);
                        foreach (DataRow row in TemplateDT.Rows)
                        {
                            Templatename = row["templatename"]+"";
                            LayerNo = row["LayerNo"]+"";
                            FileExtension = row["FileExtension"]+"";
                            Channelname = row["Channelname"]+"";
                            Bugtype = row["BugType"]+"";
                            Killwithprimary = row["KillWithPrimary"]+"";
                            Showonbreak = row["ShowOnBreak"]+"";
                            Bugstatus = row["BugStatus"]+"";
                            Paramone = row["Param1"]+"";
                            Paramtwo = row["Param2"]+"";
                            Paramthree = row["Param3"]+"";
                            Paramfour = row["Param4"]+"";
                            Paramfive = row["Param5"]+"";
                            Paramsix = row["Param6"]+"";
                            Paramseven = row["Param7"]+"";
                            Parameight = row["Param8"]+"";
                            Paramnine = row["Param9"]+"";
                            Paramten = row["Param10"]+"";
                            DbTemplate = row["DBTemplate"]+"";
                            dbtablename= row["DBTableName"] + "";
                            dbcolumn  =row["DBColumnName"] + "";
                            dbcondition= row["DBCondition"] + "";
                            _db.ExecuteQuery("Delete from channelbugmaster where  channelid='" + Channelname + "' AND templatename='" + Templatename + "'");
                            Thread.Sleep(Convert.ToInt32(500));
                           // _db.ExecuteQuery($"insert into channelbugmaster_test(templatename,layerno,fileextension,channelid,bugtype,killwithprimary,showonbreak,bugstatus,param1,param2,param3,param4,param5,param6,param7,param8,param9,param10,dbtemplate,dbtablename,dbcolumnname,dbcondition,) VALUES('{Templatename}','{LayerNo}','{FileExtension}','{Channelname}','{Bugtype}','{Killwithprimary}','{Showonbreak}','{Bugstatus}','{Paramone}','{Paramtwo}','{Paramthree}','{Paramfour}','{Paramfive}','{Paramsix}','{Paramseven}','{Parameight}','{Paramnine}','{Paramten}' ,'{DbTemplate}','{dbtablename}','{dbcolumn}','{dbcondition}')");
                            int count= _db.ExecuteQuery($"insert into channelbugmaster(templatename,layerno,fileextension,channelid,bugtype,killwithprimary,showonbreak,bugstatus,param1,param2,param3,param4,param5,param6,param7,param8,param9,param10,dbtemplate,dbtablename,dbcolumnname,dbcondition) VALUES('{Templatename}','{LayerNo}','{FileExtension}','{Channelname}','{Bugtype}','{Killwithprimary}','{Showonbreak}','{Bugstatus}','{Paramone}','{Paramtwo}','{Paramthree}','{Paramfour}','{Paramfive}' , '{Paramsix}','{Paramseven}','{Parameight}','{Paramnine}','{Paramten}' , '{DbTemplate}','{dbtablename}','{dbcolumn}','{dbcondition}')");
                        }
                    }
                    catch (Exception ex)
                    {
                        ObjMain.objlog.WriteEvents("btn_uploadtoDB_Click", "template data do not save in database "+ ex.ToString(), ObjMain.ChannelName);
                        return;
                    }
                    ObjMain.objlog.WriteEvents("btn_uploadtoDB_Click", "template data saved successfully in database", ObjMain.ChannelName);
                }
           }
        private void txtsrno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLayerNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        private void HeadePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }

        }

        private void HeadePanel_Paint(object sender, PaintEventArgs e)
        {

        }
        ///////End Class////
    }
}