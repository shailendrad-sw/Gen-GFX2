using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;

namespace graphicserver
{
    public partial class ChannelConfig : Form
    {
        string Filename = string.Empty;

        FrmMainServer Objmain;
        string Columnnamevalue = string.Empty;
        public ChannelConfig(string Fiename, FrmMainServer Objmain)
        {
            InitializeComponent();
            this.Filename = Fiename;
            this.Objmain = Objmain;
        }

        private void ChannelConfig_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                if (File.Exists(Filename))
                {
                    ValueFilling();
                }
            }
            catch 
            {

                
            }
        
          
        }
        private void SetColumnValue(DataTable table, string columnName, object value, Type dataType = null)
        {
            if (!table.Columns.Contains(columnName))
            {
                if (dataType == null)
                    dataType = typeof(string);  // default type string
                table.Columns.Add(columnName, dataType);
            }
            // Ensure at least one row exists
            if (table.Rows.Count == 0)
            {
                table.Rows.Add(table.NewRow());
            }

            table.Rows[0][columnName] = value ?? DBNull.Value;
        }

        private DataTable EnsureTableAndRow(DataSet ds, string tableName)
        {
            DataTable table;

            if (!ds.Tables.Contains(tableName))
            {
                table = new DataTable(tableName);
                ds.Tables.Add(table);
            }
            else
            {
                table = ds.Tables[tableName];
            }

            if (table.Rows.Count == 0)
            {
                table.Rows.Add(table.NewRow());
            }

            return table;
        }


        private void Savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet _dschannelconfig = new DataSet();
                _dschannelconfig.ReadXml(Filename);


                #region ApplicationMode
                DataTable appModeTable = EnsureTableAndRow(_dschannelconfig, "ApplicationMode");
                SetColumnValue(appModeTable, "ExternalAgentMode", cmbagentmode.SelectedItem?.ToString().ToUpper());
                SetColumnValue(appModeTable, "PreViewWindow", cmbpreviewwindow.SelectedItem?.ToString().ToUpper());
                #endregion ApplicationMode


                #region ConfigData
                DataTable configDataTable = EnsureTableAndRow(_dschannelconfig, "ConfigData");
                SetColumnValue(configDataTable, "ChannelName", ChannelnameValuelbl.Text);
                SetColumnValue(configDataTable, "ChannelType", sdradiobutton.Checked ? "SD" : "HD");
                SetColumnValue(configDataTable, "VideoFormat", palrad.Checked ? "PAL" : "NTSC");
                SetColumnValue(configDataTable, "EnableDB", cmbenabledb.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "DbType", dbTypeValueCmBx.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "DbIPAddress", dbipadvaluetxtbox.Text);
                SetColumnValue(configDataTable, "DbPort", dbportvaluetxtbox.Text);
                SetColumnValue(configDataTable, "DbUsername", dbuservaluetxtbox.Text);
                SetColumnValue(configDataTable, "DbPassword", dbpassvaluetxtbox.Text);
                SetColumnValue(configDataTable, "DbName", dbnamevaluetxtbox.Text);
                SetColumnValue(configDataTable, "ReceivingPort", recportvaluetxtbox.Text);
                SetColumnValue(configDataTable, "LTCPort", ltcportvaluetxtbox.Text);
                SetColumnValue(configDataTable, "KeyingMode", cmbkeyingmode.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "KeyerPort", txtkeyerport.Text);
                SetColumnValue(configDataTable, "KeyerType", cb_keyerType.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "ReconcileKeyCheck", reconcilekeyvaluecmbbox.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "OverlappingCheck", overlappingvaluecmbbox.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "SubtitleCheck", subtitlevaluecmbbox.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "PMSPacketOn", pmspacketvaluecmbbox.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "KeyerHeartBeat", cmb_keyerheartbeat.SelectedItem?.ToString().ToUpper());
                SetColumnValue(configDataTable, "NdiOutputName", ndioutputnametxtbox.Text);
                SetColumnValue(configDataTable, "CheckEventInterval", txtEventCheckingTime.Text);
                SetColumnValue(configDataTable, "Delayincommand", Txtdelaytimeincmnd.Text);
                // AutoUpdateTemplate
                if (autoupdatetemplatechkbox.Checked)
                {
                    SetColumnValue(configDataTable, "AutoUpdateTemplate", "True");
                    Objmain.AutoUpdateTemplate = true;
                    Objmain.UpdateTemplateThread();
                }
                else
                {
                    SetColumnValue(configDataTable, "AutoUpdateTemplate", "False");
                    Objmain.AutoUpdateTemplate = false;
                }

                // AddDelayInCommand
                if (DelayIncommand.Checked)
                {
                    SetColumnValue(configDataTable, "AddDelayInCommand", "True");
                    Objmain.AddDelayincommand = true;
                }
                else
                {
                    SetColumnValue(configDataTable, "AddDelayInCommand", "False");
                    Objmain.AddDelayincommand = false;
                }

                SetColumnValue(configDataTable, "TemplateRefreshInterval", templaterefreshintervaltxtbox.Text);

                // CueOffSet (splitting and taking first part)
                var cueOffsetValue = CueOffSettxtbox.Text.Split(' ')[0];
                SetColumnValue(configDataTable, "CueOffSet", cueOffsetValue);

                SetColumnValue(configDataTable, "PromoPrefix", promoprevaluetxtbox.Text);
                SetColumnValue(configDataTable, "ProgramPrefix", progprevaluetxtbox.Text);
                SetColumnValue(configDataTable, "CommercialPrefix", commprevaluetxtbox.Text);
                SetColumnValue(configDataTable, "TemplatePath", templatepathvaluetxtbox.Text);
                SetColumnValue(configDataTable, "ContentPath", contentpathvaluetxtbox.Text);
                SetColumnValue(configDataTable, "SecondaryContentPath", txtSecondaryPath.Text);
                SetColumnValue(configDataTable, "SubtitlePath", subtitlepathvaluetxtbox.Text);
                SetColumnValue(configDataTable, "CNFPath", CnfFilePathTxt.Text); // Configurable CNF Path
                SetColumnValue(configDataTable, "MultiCastIP", MulticastIpTxt.Text);

                // EnableMultiCast
                if (onlyForMulticastChbx.Checked)
                {
                    SetColumnValue(configDataTable, "EnableMultiCast", "True");
                    Objmain.MultiCastEnable = true;
                }
                else
                {
                    SetColumnValue(configDataTable, "EnableMultiCast", "False");
                    Objmain.MultiCastEnable = false;
                }

                // GfxSqueeze Setting in ConfigData
                if (CmbGfxSqueez.SelectedItem == null)
                    CmbGfxSqueez.SelectedItem = "FALSE";
                SetColumnValue(configDataTable, "GfxSqueeze", CmbGfxSqueez.SelectedItem?.ToString());
                Objmain.gFxsqueeze = !CmbGfxSqueez.SelectedItem.ToString().Contains("FALSE");


                // DbOnlyForAsrun
                if (onlyForAsrunChbx.Checked)
                {
                    SetColumnValue(configDataTable, "DbOnlyForAsrun", "True");
                    Objmain.DbOnlyForAsrun = true;
                }
                else
                {
                    SetColumnValue(configDataTable, "DbOnlyForAsrun", "False");
                    Objmain.DbOnlyForAsrun = false;
                }
                #endregion ConfigData


                #region SCTE Settings
                DataTable scteTable = EnsureTableAndRow(_dschannelconfig, "ScteSetting");
                SetColumnValue(scteTable, "ScteEnable", cmbscteenable.SelectedItem?.ToString().ToUpper());
                SetColumnValue(scteTable, "ScteTemplateName", txtsctetemplatename.Text);
                SetColumnValue(scteTable, "ScteIP", txtscteip.Text);
                SetColumnValue(scteTable, "SctePort", txtscteport.Text);
                SetColumnValue(scteTable, "SctePreRoll", txtsctepreroll.Text);
                #endregion SCTE Settings


                Objmain.TemplateRefreshInterval = templaterefreshintervaltxtbox.Text;


                #region ASRUN Settings
                DataTable asrunTable = EnsureTableAndRow(_dschannelconfig, "ASRUN");
                SetColumnValue(asrunTable, "ASRunEnable", cbEnableasrun.SelectedItem?.ToString().ToUpper());
                SetColumnValue(asrunTable, "ASRunType", cbType.SelectedItem?.ToString().ToUpper());
                SetColumnValue(asrunTable, "AsRunFolderPath", txtfolderpath.Text);
                SetColumnValue(asrunTable, "AsRunTableName", txttablename.Text);
                SetColumnValue(asrunTable, "AsRunDateTimeColumn", txtdatetimecolumn.Text);
                SetColumnValue(asrunTable, "AsRunTemplateNameColumn", txttemplatename.Text);
                SetColumnValue(asrunTable, "AsRunTemplateFileNameColumn", txttemplatefilename.Text);
                SetColumnValue(asrunTable, "AsRunChannelNameMam", txtchannelnameMam.Text);
                SetColumnValue(asrunTable, "AsRunStatusColumn", txtstatus.Text);
                SetColumnValue(asrunTable, "AsRunFilenameColumn", txtfilename.Text);
                SetColumnValue(asrunTable, "AsRunDurationColumn", txtduration.Text);
                SetColumnValue(asrunTable, "AsRunReconcileKeyColumn", txtreconkey.Text);
                SetColumnValue(asrunTable, "AsRunManualEnable", cb_ManualAsRunEnable.SelectedItem?.ToString().ToUpper());
                SetColumnValue(asrunTable, "AsRunTakeTime", CB_TimeMod.SelectedItem?.ToString().ToUpper());
                #endregion ASRUN Settings


                #region Subtitle Settings
                DataTable subtitleTable = EnsureTableAndRow(_dschannelconfig, "SubtitleSetting");
                SetColumnValue(subtitleTable, "SubtitleMode", cmb_subtitlemode.SelectedItem?.ToString().ToUpper());
                SetColumnValue(subtitleTable, "SubtitleFileType", cmb_filetype.SelectedItem?.ToString().ToUpper());
                SetColumnValue(subtitleTable, "SubtitleEnable", cmb_enablesubtitle.SelectedItem?.ToString().ToUpper());
                SetColumnValue(subtitleTable, "SubtitlePort", textBox_subtitleport.Text);
                SetColumnValue(subtitleTable, "SubtitleStartOffset", textBox_startoffset.Text);
                SetColumnValue(subtitleTable, "SubtitleEndOffset", textBox_endoffset.Text);
                SetColumnValue(subtitleTable, "SubtitleSuffix", textBox_subtitlesuffix.Text);
                SetColumnValue(subtitleTable, "SubtitleTemplate", textbox_subtitletemplate.Text);
                SetColumnValue(subtitleTable, "Priority_BOTH_case", PriorityBox.Text);
                SetColumnValue(subtitleTable, "EncodingChange", EncodingCombo.SelectedItem?.ToString().ToUpper());
                #endregion Subtitle Settings


                #region NetworkStream Settings
                DataTable networkStreamTable = EnsureTableAndRow(_dschannelconfig, "NetworkStream");
                SetColumnValue(networkStreamTable, "StreamEnable", cmbStreamEnble.SelectedItem?.ToString().ToUpper());
                SetColumnValue(networkStreamTable, "StreamType", cmbstrmtyp.SelectedItem?.ToString().ToUpper());
                SetColumnValue(networkStreamTable, "StreamURL", TxtURL.Text);
                SetColumnValue(networkStreamTable, "StreamConfig", TxtConfig.Text);
                SetColumnValue(networkStreamTable, "VideoBitrate", VBitrate.Text);
                #endregion NetworkStream Settings


                #region GFXClient Settings
                DataTable gfxClientTable = EnsureTableAndRow(_dschannelconfig, "GFXClient");
                SetColumnValue(gfxClientTable, "GFXClientEnable", GFXClientEnable.Text);
                SetColumnValue(gfxClientTable, "GFXClientPort", Portgfxclient.Text);
                SetColumnValue(gfxClientTable, "GFXClientIP", Ipgfxclient.Text);
                #endregion GFXClient Settings


                // Save updated XML
                _dschannelconfig.WriteXml(Filename);

                Objmain.CustomMessageBox.ShowMessageBox(Application.ProductName, "AppConfig Updated Successfully", "OK", "Exclamation");
                this.Close();
            }
            catch (Exception ex)
            {
                Objmain.CustomMessageBox.ShowMessageBox(Application.ProductName, $"Error in Updating AppConfig - {ex.Message}", "OK", "Exclamation");
                Objmain.objlog.WriteError("CheckPrimary", "Error-- " + ex.ToString(), Objmain.ChannelName);
            }
        }
        private void Savebuttonold_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet _dschannelconfig = new DataSet();
                _dschannelconfig.ReadXml(Filename);

                _dschannelconfig.Tables["ApplicationMode"].Rows[0]["ExternalAgentMode"] = cmbagentmode.SelectedItem;
                _dschannelconfig.Tables["ApplicationMode"].Rows[0]["PreViewWindow"] = cmbpreviewwindow.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["ChannelName"] = ChannelnameValuelbl.Text;
                if (sdradiobutton.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["ChannelType"] = "SD";
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["ChannelType"] = "HD";
                }
                if (palrad.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["VideoFormat"] = "PAL";
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["VideoFormat"] = "NTSC";
                }
                _dschannelconfig.Tables["ConfigData"].Rows[0]["EnableDB"] = cmbenabledb.SelectedItem;


                //_dschannelconfig.Tables["ConfigData"].Rows[0]["DbType"] = dbtypevaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbType"] = dbTypeValueCmBx.SelectedItem;
                
                
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbIPAddress"] = dbipadvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbPort"] = dbportvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbUsername"] = dbuservaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbPassword"] = dbpassvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["DbName"] = dbnamevaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["ReceivingPort"] = recportvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["LTCPort"] = ltcportvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["Delayincommand"] = Txtdelaytimeincmnd.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyingMode"] = cmbkeyingmode.SelectedItem+"";
                _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerPort"] = txtkeyerport.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerType"] = cb_keyerType.SelectedItem+"";
                _dschannelconfig.Tables["ConfigData"].Rows[0]["ReconcileKeyCheck"] = reconcilekeyvaluecmbbox.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["OverlappingCheck"] = overlappingvaluecmbbox.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["SubtitleCheck"] = subtitlevaluecmbbox.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["PMSPacketOn"] = pmspacketvaluecmbbox.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerHeartBeat"] = cmb_keyerheartbeat.SelectedItem;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["NdiOutputName"] = ndioutputnametxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["CheckEventInterval"] = txtEventCheckingTime.Text;
                if (autoupdatetemplatechkbox.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["AutoUpdateTemplate"] = "True";
                    Objmain.AutoUpdateTemplate = true;
                    Objmain.UpdateTemplateThread();
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["AutoUpdateTemplate"] = "False";
                    Objmain.AutoUpdateTemplate = false;
                }
                if (DelayIncommand.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["AddDelayInCommand"] = "True";
                    Objmain.AddDelayincommand = true;       
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["AddDelayInCommand"] = "False";
                    Objmain.AddDelayincommand = false;
                }
                _dschannelconfig.Tables["ConfigData"].Rows[0]["TemplateRefreshInterval"] = templaterefreshintervaltxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["CueOffSet"] = CueOffSettxtbox.Text.Split(' ')[0];
                _dschannelconfig.Tables["ConfigData"].Rows[0]["PromoPrefix"] = promoprevaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["ProgramPrefix"] = progprevaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["CommercialPrefix"] = commprevaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["TemplatePath"] = templatepathvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["ContentPath"] = contentpathvaluetxtbox.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["SecondaryContentPath"] = txtSecondaryPath.Text;
                _dschannelconfig.Tables["ConfigData"].Rows[0]["SubtitlePath"] = subtitlepathvaluetxtbox.Text;

                _dschannelconfig.Tables["ConfigData"].Rows[0]["CNFPath"] = CnfFilePathTxt.Text; //Chnages by Tushar Pandey on 17-06-2025 for CNF path to be configurable

                _dschannelconfig.Tables["ConfigData"].Rows[0]["MultiCastIP"] = MulticastIpTxt.Text;

                if (onlyForMulticastChbx.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["EnableMultiCast"] = "True";
                    Objmain.MultiCastEnable = true;
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["EnableMultiCast"] = "False";
                    Objmain.MultiCastEnable = false;
                }
                if (_dschannelconfig.Tables["ScteSetting"]!=null)
                {
                    _dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteEnable"] = cmbscteenable.SelectedItem;
                    _dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteTemplateName"] = txtsctetemplatename.Text;
                    _dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteIP"] = txtscteip.Text;
                    _dschannelconfig.Tables["ScteSetting"].Rows[0]["SctePort"] = txtscteport.Text;
                    _dschannelconfig.Tables["ScteSetting"].Rows[0]["SctePreRoll"] = txtsctepreroll.Text;
                }
                Objmain.TemplateRefreshInterval = templaterefreshintervaltxtbox.Text;
                if (_dschannelconfig.Tables["ASRUN"] != null)
                {
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["ASRunEnable"] = cbEnableasrun.SelectedItem;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["ASRunType"] = cbType.SelectedItem;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunFolderPath"] = txtfolderpath.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTableName"] = txttablename.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunDateTimeColumn"] = txtdatetimecolumn.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTemplateNameColumn"] = txttemplatename.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTemplateFileNameColumn"] = txttemplatefilename.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunChannelNameMam"] = txtchannelnameMam.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunStatusColumn"] = txtstatus.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunFilenameColumn"] = txtfilename.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunDurationColumn"] = txtduration.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunReconcileKeyColumn"] = txtreconkey.Text;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunManualEnable"] = cb_ManualAsRunEnable.SelectedItem;
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTakeTime"] = CB_TimeMod.SelectedItem;

                }
                if (_dschannelconfig.Tables["SubtitleSetting"] != null)
                {             
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleMode"] = cmb_subtitlemode.SelectedItem;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleFileType"] = cmb_filetype.SelectedItem;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleEnable"] = cmb_enablesubtitle.SelectedItem;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitlePort"] = textBox_subtitleport.Text;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleStartOffset"] = textBox_startoffset.Text;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleEndOffset"] = textBox_endoffset.Text;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleSuffix"] = textBox_subtitlesuffix.Text;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleTemplate"] = textbox_subtitletemplate.Text;
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["Priority_BOTH_case"] = PriorityBox.Text; //SubtitlePriority //21322
                    _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["EncodingChange"] = EncodingCombo.SelectedItem;
                }
                if (_dschannelconfig.Tables["NetworkStream"] != null)
                {
                    _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamEnable"] = cmbStreamEnble.SelectedItem;
                    _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamType"] = cmbstrmtyp.SelectedItem;
                    _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamURL"] = TxtURL.Text;
                    _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamConfig"] = TxtConfig.Text;
                    _dschannelconfig.Tables["NetworkStream"].Rows[0]["VideoBitrate"] = VBitrate.Text;
                }
                if (_dschannelconfig.Tables["GFXClient"] != null)
                {
                    _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientEnable"] = GFXClientEnable.Text;
                    _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientPort"] = Portgfxclient.Text;
                    _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientIP"] = Ipgfxclient.Text;
                }
                if (_dschannelconfig.Tables["ConfigData"] != null)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["GfxSqueeze"] = CmbGfxSqueez.SelectedItem;
                    if (CmbGfxSqueez.SelectedItem == null)
                        CmbGfxSqueez.SelectedItem = "FALSE";
                    if (CmbGfxSqueez.SelectedItem.ToString().Contains("FALSE"))
                        Objmain.gFxsqueeze = false;
                    else
                        Objmain.gFxsqueeze = true;
                }
                if (onlyForAsrunChbx.Checked == true)
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["DbOnlyForAsrun"] = "True";
                    Objmain.DbOnlyForAsrun = true;
                }
                else
                {
                    _dschannelconfig.Tables["ConfigData"].Rows[0]["DbOnlyForAsrun"] = "False";
                    Objmain.DbOnlyForAsrun = false;
                }
                _dschannelconfig.WriteXml(Filename);
                Objmain.CustomMessageBox.ShowMessageBox(Application.ProductName, "AppConfig Updated Successfully", "OK", "Exclamation");
                this.Close();
            }
            catch (Exception ex)
            {
                Objmain.CustomMessageBox.ShowMessageBox(Application.ProductName, $"Error in Updating AppConfig - {ex.Message}", "OK", "Exclamation");
                Objmain.objlog.WriteError("CheckPrimary", "Error-- " + ex.ToString(), Objmain.ChannelName);
            }
        }
        private void MultiCastIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void onlyForMulticastChbx_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (onlyForMulticastChbx.Checked == true)
                {
                    MulticastIpTxt.Enabled = true;
                }
                else
                {
                    MulticastIpTxt.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objmain.objlog.WriteError("onlyForMulticastChbx_CheckedChanged", "Error-- " + ex.ToString(), Objmain.ChannelName);
            }
        }
        string GetOrAddColumnValue(DataTable table, DataRow row, string columnName, string defaultValue = "")
        {
            if (!table.Columns.Contains(columnName))
            {
                table.Columns.Add(columnName, typeof(string));
                row[columnName] = defaultValue;
            }
            else if (row.IsNull(columnName))
            {
                row[columnName] = defaultValue;
            }
            return row[columnName].ToString();
        }

        private void ValueFilling()
        {
            try
            {
                DataSet _dschannelconfig = new DataSet();
                _dschannelconfig.ReadXml(Filename);

                // Helper function to get or add missing column with default value
             
                #region ---- CONFIG DATA TABLE ----
                //DataTable configTable = _dschannelconfig.Tables["ConfigData"];
                DataTable configTable = EnsureTableAndRow(_dschannelconfig, "ConfigData");
                DataRow configRow = configTable.Rows[0];

                ChannelnameValuelbl.Text = GetOrAddColumnValue(configTable, configRow, "ChannelName", Objmain.ChannelName);

                string channelType = GetOrAddColumnValue(configTable, configRow, "ChannelType").ToUpper();
                sdradiobutton.Checked = channelType == "SD";
                hdradiobutton.Checked = !sdradiobutton.Checked;

                string videoFormat = GetOrAddColumnValue(configTable, configRow, "VideoFormat").ToUpper();
                palrad.Checked = videoFormat == "PAL";
                ntscrad.Checked = !palrad.Checked;

                CueOffSettxtbox.Text = GetOrAddColumnValue(configTable, configRow, "CueOffSet") + " Frames";
                recportvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "ReceivingPort");
                ltcportvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "LTCPort");

                dbTypeValueCmBx.SelectedItem = GetOrAddColumnValue(configTable, configRow, "DbType");

                dbuservaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "DbUsername");
                dbpassvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "DbPassword");
                dbnamevaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "DbName");
                dbportvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "DbPort");
                dbipadvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "DbIPAddress");

                subtitlevaluecmbbox.SelectedItem = GetOrAddColumnValue(configTable, configRow, "SubtitleCheck").ToUpper();
                pmspacketvaluecmbbox.SelectedItem = GetOrAddColumnValue(configTable, configRow, "PMSPacketOn").ToUpper();

                cmb_keyerheartbeat.SelectedItem = GetOrAddColumnValue(configTable, configRow, "KeyerHeartBeat").ToUpper();

                overlappingvaluecmbbox.SelectedItem = GetOrAddColumnValue(configTable, configRow, "OverlappingCheck").ToUpper();
                reconcilekeyvaluecmbbox.SelectedItem = GetOrAddColumnValue(configTable, configRow, "ReconcileKeyCheck").ToUpper();

                cmbenabledb.SelectedItem = GetOrAddColumnValue(configTable, configRow, "EnableDB").ToUpper();

                cmbkeyingmode.SelectedItem = GetOrAddColumnValue(configTable, configRow, "KeyingMode").ToUpper();
                txtkeyerport.Text = GetOrAddColumnValue(configTable, configRow, "KeyerPort");
                cb_keyerType.SelectedItem = GetOrAddColumnValue(configTable, configRow, "KeyerType").ToLower();

                promoprevaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "PromoPrefix");
                progprevaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "ProgramPrefix");
                commprevaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "CommercialPrefix");

                templatepathvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "TemplatePath");
                contentpathvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "ContentPath");
                subtitlepathvaluetxtbox.Text = GetOrAddColumnValue(configTable, configRow, "SubtitlePath");
                CnfFilePathTxt.Text = GetOrAddColumnValue(configTable, configRow, "CNFPath");

                txtSecondaryPath.Text = GetOrAddColumnValue(configTable, configRow, "SecondaryContentPath");
                txtEventCheckingTime.Text = GetOrAddColumnValue(configTable, configRow, "CheckEventInterval");

                string sTempchkbox = GetOrAddColumnValue(configTable, configRow, "AutoUpdateTemplate", "FALSE");
                autoupdatetemplatechkbox.Checked = sTempchkbox.ToUpper() == "TRUE";
               // Txtdelaytimeincmnd.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["Delayincommand"].ToString().Trim();
                Txtdelaytimeincmnd.Text = GetOrAddColumnValue(configTable, configRow, "Delayincommand");
                string sTmpcommandoverlapped = GetOrAddColumnValue(configTable, configRow, "AddDelayInCommand", "FALSE");
                DelayIncommand.Checked = sTmpcommandoverlapped.ToUpper() == "TRUE";

                ndioutputnametxtbox.Text = GetOrAddColumnValue(configTable, configRow, "NdiOutputName");
                templaterefreshintervaltxtbox.Text = GetOrAddColumnValue(configTable, configRow, "TemplateRefreshInterval");

                MulticastIpTxt.Text = GetOrAddColumnValue(configTable, configRow, "MultiCastIP").ToUpper();
                onlyForMulticastChbx.Checked = GetOrAddColumnValue(configTable, configRow, "EnableMultiCast", "FALSE").ToUpper() == "TRUE";

                onlyForAsrunChbx.Checked = GetOrAddColumnValue(configTable, configRow, "DbOnlyForAsrun", "FALSE").ToUpper() == "TRUE";
                CmbGfxSqueez.SelectedItem = GetOrAddColumnValue(configTable, configRow, "GfxSqueeze", "FALSE").ToUpper(); 
                #endregion ---- CONFIG DATA TABLE ----


                #region ---- APPLICATION MODE TABLE ----
                DataTable appModeTable = EnsureTableAndRow(_dschannelconfig, "ApplicationMode");
                DataRow appModeRow = appModeTable.Rows[0];

                if (!appModeTable.Columns.Contains("ExternalAgentMode"))
                    appModeTable.Columns.Add("ExternalAgentMode", typeof(string));
                if (appModeRow.IsNull("ExternalAgentMode"))
                    appModeRow["ExternalAgentMode"] = "TRUE";

                cmbagentmode.SelectedIndex = appModeRow["ExternalAgentMode"].ToString().ToUpper() == "FALSE" ? 0 : 1;

                if (!appModeTable.Columns.Contains("PreviewWindow"))
                    appModeTable.Columns.Add("PreviewWindow", typeof(string));
                if (appModeRow.IsNull("PreviewWindow"))
                    appModeRow["PreviewWindow"] = "FALSE";

                cmbpreviewwindow.SelectedIndex = appModeRow["PreviewWindow"].ToString().ToUpper() == "FALSE" ? 0 : 1;
                #endregion ---- APPLICATION MODE TABLE ----


                #region ---- SCTE SETTING TABLE ----
                DataTable scteTable = EnsureTableAndRow(_dschannelconfig, "ScteSetting");
                DataRow scteRow = scteTable.Rows[0];

                if (!scteTable.Columns.Contains("ScteEnable"))
                    scteTable.Columns.Add("ScteEnable", typeof(string));
                if (scteRow.IsNull("ScteEnable"))
                    scteRow["ScteEnable"] = "FALSE";

                cmbscteenable.SelectedIndex = scteRow["ScteEnable"].ToString().ToUpper() == "FALSE" ? 0 : 1;

                txtsctetemplatename.Text = scteRow.Table.Columns.Contains("ScteTemplateName") ? scteRow["ScteTemplateName"].ToString() : "";
                txtscteip.Text = scteRow.Table.Columns.Contains("ScteIP") ? scteRow["ScteIP"].ToString() : "";
                txtscteport.Text = scteRow.Table.Columns.Contains("SctePort") ? scteRow["SctePort"].ToString() : "";
                txtsctepreroll.Text = scteRow.Table.Columns.Contains("SctePreRoll") ? scteRow["SctePreRoll"].ToString() : "";
                #endregion ---- SCTE SETTING TABLE ----


                #region ---- ASRUN TABLE ----
                DataTable asrunTable = EnsureTableAndRow(_dschannelconfig, "ASRUN");
                DataRow asrunRow = asrunTable.Rows[0];

                if (!asrunTable.Columns.Contains("ASRunEnable"))
                    asrunTable.Columns.Add("ASRunEnable", typeof(string));
                if (asrunRow.IsNull("ASRunEnable"))
                    asrunRow["ASRunEnable"] = "FALSE";

                cbEnableasrun.SelectedItem = asrunRow["ASRunEnable"].ToString().ToUpper();

                if (!asrunTable.Columns.Contains("ASRunType"))
                    asrunTable.Columns.Add("ASRunType", typeof(string));
                if (asrunRow.IsNull("ASRunType"))
                    asrunRow["ASRunType"] = "FILE";

                cbType.SelectedItem = asrunRow["ASRunType"].ToString();

                txtfolderpath.Text = asrunRow.Table.Columns.Contains("AsRunFolderPath") ? asrunRow["AsRunFolderPath"].ToString() : "";
                txttablename.Text = asrunRow.Table.Columns.Contains("AsRunTableName") ? asrunRow["AsRunTableName"].ToString() : "";
                txtdatetimecolumn.Text = asrunRow.Table.Columns.Contains("AsRunDateTimeColumn") ? asrunRow["AsRunDateTimeColumn"].ToString() : "";
                txttemplatename.Text = asrunRow.Table.Columns.Contains("AsRunTemplateNameColumn") ? asrunRow["AsRunTemplateNameColumn"].ToString() : "";
                txttemplatefilename.Text = asrunRow.Table.Columns.Contains("AsRunTemplateFileNameColumn") ? asrunRow["AsRunTemplateFileNameColumn"].ToString() : "";
                txtchannelnameMam.Text = asrunRow.Table.Columns.Contains("AsRunChannelNameMam") ? asrunRow["AsRunChannelNameMam"].ToString() : "";
                txtstatus.Text = asrunRow.Table.Columns.Contains("AsRunStatusColumn") ? asrunRow["AsRunStatusColumn"].ToString() : "";
                txtfilename.Text = asrunRow.Table.Columns.Contains("AsRunFilenameColumn") ? asrunRow["AsRunFilenameColumn"].ToString() : "";
                txtduration.Text = asrunRow.Table.Columns.Contains("AsRunDurationColumn") ? asrunRow["AsRunDurationColumn"].ToString() : "";
                txtreconkey.Text = asrunRow.Table.Columns.Contains("AsRunReconcileKeyColumn") ? asrunRow["AsRunReconcileKeyColumn"].ToString() : "";

                if (!asrunTable.Columns.Contains("AsRunManualEnable"))
                    asrunTable.Columns.Add("AsRunManualEnable", typeof(string));
                if (asrunRow.IsNull("AsRunManualEnable"))
                    asrunRow["AsRunManualEnable"] = "FALSE";

                cb_ManualAsRunEnable.SelectedItem = asrunRow["AsRunManualEnable"];

                if (!asrunTable.Columns.Contains("AsRunTakeTime"))
                    asrunTable.Columns.Add("AsRunTakeTime", typeof(string));
                if (asrunRow.IsNull("AsRunTakeTime"))
                    asrunRow["AsRunTakeTime"] = "FALSE";

                CB_TimeMod.SelectedItem = asrunRow["AsRunTakeTime"].ToString().ToUpper();
                #endregion ---- ASRUN TABLE ----


                #region ---- SUBTITLE SETTING TABLE ----
                DataTable subTable = EnsureTableAndRow(_dschannelconfig, "SubtitleSetting");
                DataRow subRow = subTable.Rows[0];

                cmb_enablesubtitle.SelectedItem = GetOrAddColumnValue(subTable, subRow, "SubtitleEnable", "FALSE").ToUpper();
                cmb_subtitlemode.SelectedItem = GetOrAddColumnValue(subTable, subRow, "SubtitleMode");
                cmb_filetype.SelectedItem = GetOrAddColumnValue(subTable, subRow, "SubtitleFileType");
                textBox_subtitleport.Text = GetOrAddColumnValue(subTable, subRow, "SubtitlePort", "0");
                textBox_startoffset.Text = GetOrAddColumnValue(subTable, subRow, "SubtitleStartOffset", "0");
                textBox_endoffset.Text = GetOrAddColumnValue(subTable, subRow, "SubtitleEndOffset", "0");
                textBox_subtitlesuffix.Text = GetOrAddColumnValue(subTable, subRow, "SubtitleSuffix");
                textbox_subtitletemplate.Text = GetOrAddColumnValue(subTable, subRow, "SubtitleTemplate");
                PriorityBox.SelectedItem = GetOrAddColumnValue(subTable, subRow, "Priority_BOTH_case").ToUpper();

                EncodingCombo.SelectedIndex = 1; // keep your original logic
                EncodingCombo.SelectedItem = GetOrAddColumnValue(subTable, subRow, "EncodingChange", "UTF-8").ToUpper();
                #endregion ---- SUBTITLE SETTING TABLE ----


                #region ---- NETWORK STREAM TABLE ----
                DataTable netTable = EnsureTableAndRow(_dschannelconfig, "NetworkStream");
                DataRow netRow = netTable.Rows[0];

                cmbStreamEnble.SelectedItem = GetOrAddColumnValue(netTable, netRow, "StreamEnable", "FALSE");
                cmbstrmtyp.SelectedItem = GetOrAddColumnValue(netTable, netRow, "StreamType");
                TxtURL.Text = GetOrAddColumnValue(netTable, netRow, "StreamURL");
                TxtConfig.Text = GetOrAddColumnValue(netTable, netRow, "StreamConfig");
                VBitrate.Text = GetOrAddColumnValue(netTable, netRow, "VideoBitrate", "0");
                
                #endregion ---- NETWORK STREAM TABLE ----


                #region ---- GFX CLIENT TABLE ----
                DataTable gfxTable = EnsureTableAndRow(_dschannelconfig, "GFXClient");
                DataRow gfxRow = gfxTable.Rows[0];

                GFXClientEnable.Text = GetOrAddColumnValue(gfxTable, gfxRow, "GFXClientEnable", "FALSE");
                Portgfxclient.Text = GetOrAddColumnValue(gfxTable, gfxRow, "GFXClientPort", "0");
                Ipgfxclient.Text = GetOrAddColumnValue(gfxTable, gfxRow, "GFXClientIP");
                #endregion ---- GFX CLIENT TABLE ----


                _dschannelconfig.ReadXml(Filename);
            }
            catch (Exception ex)
            {
                Objmain.objlog.WriteError("ValueFilling()", "Error In Reading Config File please check all column -- " + ex.ToString(), Objmain.ChannelName);
            }
        }
        private void ValueFilling_old()
        {
            try
            {
                string sTempchkbox;
                DataSet _dschannelconfig = new DataSet();
                _dschannelconfig.ReadXml(Filename);
                ChannelnameValuelbl.Text = (_dschannelconfig.Tables["ConfigData"].Rows[0]["ChannelName"]).ToString();
                if (_dschannelconfig.Tables["ConfigData"].Rows[0]["ChannelType"].ToString().ToUpper() == "SD")
                {
                    sdradiobutton.Checked = true;
                    hdradiobutton.Checked = false;
                }
                else
                {
                    sdradiobutton.Checked = false;
                    hdradiobutton.Checked = true;
                }
                if (_dschannelconfig.Tables["ConfigData"].Rows[0]["VideoFormat"].ToString().ToUpper()=="PAL")
                {
                    palrad.Checked = true;
                    ntscrad.Checked = false;
                }
                else
                {
                    palrad.Checked = false;
                    ntscrad.Checked = true;
                }
                CueOffSettxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["CueOffSet"].ToString() + " Frames";
                recportvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["ReceivingPort"].ToString();
                ltcportvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["LTCPort"].ToString().Trim();
                Txtdelaytimeincmnd.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["Delayincommand"].ToString().Trim();

                //dbtypevaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbType"].ToString();
                dbTypeValueCmBx.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbType"].ToString();
                
                
                dbuservaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbUsername"].ToString();
                dbpassvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbPassword"].ToString();
                dbnamevaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbName"].ToString();
                dbportvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbPort"].ToString();
                dbipadvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbIPAddress"].ToString();

                subtitlevaluecmbbox.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["SubtitleCheck"].ToString().ToUpper() ;
                pmspacketvaluecmbbox.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["PMSPacketOn"].ToString().ToUpper() ;

                cmb_keyerheartbeat.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerHeartBeat"].ToString().ToUpper();

                overlappingvaluecmbbox.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["OverlappingCheck"].ToString().ToUpper();
                reconcilekeyvaluecmbbox.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["ReconcileKeyCheck"].ToString().ToUpper();
                

                cmbenabledb.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["EnableDB"].ToString().ToUpper() ;
                
                
                cmbkeyingmode.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyingMode"].ToString().ToUpper() ;
                txtkeyerport.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerPort"].ToString();
                cb_keyerType.SelectedItem= _dschannelconfig.Tables["ConfigData"].Rows[0]["KeyerType"]+"";
                promoprevaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["PromoPrefix"].ToString();
                progprevaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["ProgramPrefix"].ToString();
                commprevaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["CommercialPrefix"].ToString();
                Size size = TextRenderer.MeasureText(_dschannelconfig.Tables["ConfigData"].Rows[0]["TemplatePath"].ToString(), templatepathvaluetxtbox.Font);
                templatepathvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["TemplatePath"].ToString();
                size = TextRenderer.MeasureText(_dschannelconfig.Tables["ConfigData"].Rows[0]["ContentPath"].ToString(), templatepathvaluetxtbox.Font);
                contentpathvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["ContentPath"].ToString();
                size = TextRenderer.MeasureText(_dschannelconfig.Tables["ConfigData"].Rows[0]["SubtitlePath"].ToString(), templatepathvaluetxtbox.Font);
                subtitlepathvaluetxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["SubtitlePath"].ToString();

                size = TextRenderer.MeasureText(_dschannelconfig.Tables["ConfigData"].Rows[0]["CNFPath"].ToString(), templatepathvaluetxtbox.Font);
                CnfFilePathTxt.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["CNFPath"].ToString(); // Changes by Tushar Pandey on 17-06-2025 for configurable CNF Path

                txtSecondaryPath.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["SecondaryContentPath"].ToString();
                txtEventCheckingTime.Text= _dschannelconfig.Tables["ConfigData"].Rows[0]["CheckEventInterval"].ToString();
                sTempchkbox = _dschannelconfig.Tables["ConfigData"].Rows[0]["AutoUpdateTemplate"].ToString();
                CmbGfxSqueez.SelectedItem = _dschannelconfig.Tables["ConfigData"].Rows[0]["GfxSqueeze"] + "";//
                if (CmbGfxSqueez.SelectedItem == null)
                    CmbGfxSqueez.SelectedItem = "FALSE";
                if (CmbGfxSqueez.SelectedItem.ToString().Contains("FALSE"))
                    Objmain.gFxsqueeze = false;
                else
                    Objmain.gFxsqueeze = true;
                if (sTempchkbox.ToUpper() == "TRUE")
                {
                    autoupdatetemplatechkbox.Checked = true;
                }
                else
                {
                    autoupdatetemplatechkbox.Checked = false;
                }
                String sTmpcommandoverlapped = _dschannelconfig.Tables["ConfigData"].Rows[0]["AddDelayInCommand"].ToString();
                if (sTmpcommandoverlapped.ToUpper() == "TRUE")
                {
                    DelayIncommand.Checked = true;
                }
                else
                {
                    DelayIncommand.Checked = false;
                }
                ndioutputnametxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["NdiOutputName"].ToString();
                templaterefreshintervaltxtbox.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["TemplateRefreshInterval"].ToString();
                cmbagentmode.SelectedIndex = Convert.ToInt32((_dschannelconfig.Tables["ApplicationMode"].Rows[0]["ExternalAgentMode"]).ToString().ToUpper() == "FALSE" ? 0 : 1);
                cmbpreviewwindow.SelectedIndex = Convert.ToInt32((_dschannelconfig.Tables["ApplicationMode"].Rows[0]["PreviewWindow"]).ToString().ToUpper() == "FALSE" ? 0 : 1);
                cmbscteenable.SelectedIndex = Convert.ToInt32((_dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteEnable"]).ToString().ToUpper() == "FALSE" ? 0 : 1);
                txtsctetemplatename.Text = (_dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteTemplateName"]).ToString();
                txtscteip.Text = (_dschannelconfig.Tables["ScteSetting"].Rows[0]["ScteIP"]).ToString();
                txtscteport.Text = (_dschannelconfig.Tables["ScteSetting"].Rows[0]["SctePort"]).ToString();
                txtsctepreroll.Text = (_dschannelconfig.Tables["ScteSetting"].Rows[0]["SctePreRoll"]).ToString();
                MulticastIpTxt.Text = _dschannelconfig.Tables["ConfigData"].Rows[0]["MultiCastIP"].ToString().ToUpper();
                string onlyForMultiCast = _dschannelconfig.Tables["ConfigData"].Rows[0]["EnableMultiCast"].ToString().ToUpper();
                if (onlyForMultiCast == "TRUE")
                {
                    onlyForMulticastChbx.Checked = true;
                }
                else
                {
                    onlyForMulticastChbx.Checked = false;
                }
                if (_dschannelconfig.Tables["ASRUN"] != null)
                {
                    cbEnableasrun.SelectedItem = (_dschannelconfig.Tables["ASRUN"].Rows[0]["ASRunEnable"] + "").ToUpper();
                      cbType.SelectedItem = _dschannelconfig.Tables["ASRUN"].Rows[0]["ASRunType"] + "";
                   // cbType.SelectedItem = "FILE" + "";
                    _dschannelconfig.Tables["ASRUN"].Rows[0]["ASRunType"] = cbType.SelectedItem + "";
                    txtfolderpath.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunFolderPath"] + "";
                    txttablename.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTableName"] + "";
                    txtdatetimecolumn.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunDateTimeColumn"] + "";
                    txttemplatename.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTemplateNameColumn"] + "";
                    txttemplatefilename.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTemplateFileNameColumn"] + "";
                    txtchannelnameMam.Text= _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunChannelNameMam"] + "";  
                    txtstatus.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunStatusColumn"] + "";
                    txtfilename.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunFilenameColumn"] + "";
                    txtduration.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunDurationColumn"] + "";
                    txtreconkey.Text = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunReconcileKeyColumn"] + "";
                    cb_ManualAsRunEnable.SelectedItem = _dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunManualEnable"];
                    CB_TimeMod.SelectedItem = (_dschannelconfig.Tables["ASRUN"].Rows[0]["AsRunTakeTime"] + "").ToUpper();
                }
                if (_dschannelconfig.Tables["SubtitleSetting"] != null)
                {
                    String abc = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleEnable"] + ""; 
                    cmb_enablesubtitle.SelectedItem = (_dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleEnable"] + "").ToString().ToUpper();
                    cmb_subtitlemode.SelectedItem =  (_dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleMode"] + "").ToString();
                    cmb_filetype.SelectedItem   =(_dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleFileType"]+"").ToString();                   
                    textBox_subtitleport.Text = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitlePort"] + "";
                    textBox_startoffset.Text = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleStartOffset"] + "";
                    textBox_endoffset.Text = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleEndOffset"] + "";
                    textBox_subtitlesuffix.Text = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleSuffix"] + "";
                    textbox_subtitletemplate.Text = _dschannelconfig.Tables["SubtitleSetting"].Rows[0]["SubtitleTemplate"] + "";    
                    PriorityBox.SelectedItem = (_dschannelconfig.Tables["SubtitleSetting"].Rows[0]["Priority_BOTH_case"] + "").ToUpper();//21322
                    EncodingCombo.SelectedIndex = 1;
                    EncodingCombo.SelectedItem=(_dschannelconfig.Tables["SubtitleSetting"].Rows[0]["EncodingChange"] + "").ToString().ToUpper(); 
                }
                if (_dschannelconfig.Tables["NetworkStream"] != null)
                {
                    cmbStreamEnble.SelectedItem = _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamEnable"] + "";
                    cmbstrmtyp.SelectedItem = _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamType"] + "";
                    TxtURL.Text = _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamURL"] + "";
                    TxtConfig.Text = _dschannelconfig.Tables["NetworkStream"].Rows[0]["StreamConfig"] + "";
                    VBitrate.Text = _dschannelconfig.Tables["NetworkStream"].Rows[0]["VideoBitrate"] + "";
                }
                if (_dschannelconfig.Tables["GFXClient"] != null)
                {
                    GFXClientEnable.Text = _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientEnable"] + "";
                    Portgfxclient.Text = _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientPort"] + "";
                    Ipgfxclient.Text = _dschannelconfig.Tables["GFXClient"].Rows[0]["GFXClientIP"] + "";
                }
                string onlyForAsRun = _dschannelconfig.Tables["ConfigData"].Rows[0]["DbOnlyForAsrun"].ToString().ToUpper();
                if (onlyForAsRun == "TRUE")
                {
                    onlyForAsrunChbx.Checked = true;
                }
                else
                {
                    onlyForAsrunChbx.Checked = false;
                }

            }
            catch (Exception ex)
            {
                Objmain.objlog.WriteError("ValueFilling()", "Error In Reading Config File please check all column -- " + ex.ToString(), Objmain.ChannelName);
            }
        }

        private void BTN_TEMPPATH_BROWSE_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    templatepathvaluetxtbox.Text = folderDlg.SelectedPath;
                }
                catch (Exception ex)
                {
                    Objmain.objlog.WriteError("BTN_TEMPPATH_BROWSE_Click", "Error-- " + ex.ToString(), Objmain.ChannelName);
                }
            }
        }

        private void BTN_SUBPATH_BROWSE_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    subtitlepathvaluetxtbox.Text = folderDlg.SelectedPath;
                }
                catch (Exception ex)
                {
                    Objmain.objlog.WriteError("BTN_SUBPATH_BROWSE_Click", "Error-- " + ex.ToString(), Objmain.ChannelName);
                }
            }
        }

        private void BTN_CONTENTPATH_BROWSE_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    contentpathvaluetxtbox.Text = folderDlg.SelectedPath;
                }
                catch (Exception ex)
                {
                    Objmain.objlog.WriteError("BTN_CONTENTPATH_BROWSE_Click", "Error-- " + ex.ToString(), Objmain.ChannelName);
                }
            }
        }

        private void templatepathvaluetxtbox_Enter(object sender, EventArgs e)
        {
            try
            {
                ToolTip tt = new ToolTip();
                tt.Show(templatepathvaluetxtbox.Text, templatepathvaluetxtbox, 0, 0, 5000);
            }
            catch (Exception ex)
            {
                Objmain.objlog.WriteError("templatepathvaluetxtbox_Enter", "Error-- " + ex.ToString(), Objmain.ChannelName);
            }
        }

        private void txtkeyerport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtscteport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dbportvaluetxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ltcportvaluetxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void recportvaluetxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dbipadvaluetxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtscteip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtsctepreroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void templaterefreshintervaltxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tp_channelsetting_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowsFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtfolderpath.Text = folderDlg.SelectedPath;
            }
        }

        private void BtClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {

                
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

        private void btnBrowseSeconPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    txtSecondaryPath.Text = folderDlg.SelectedPath;
                }
                catch (Exception ex)
                {
                    Objmain.objlog.WriteError("btnBrowseSeconPath_Click", "Error-- " + ex.ToString(), Objmain.ChannelName);
                }
            }
        }

        private void txtchannelnameMam_TextChanged(object sender, EventArgs e)
        {

        }

        private void dbTypeValueCmBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dbTypeValueCmBx.SelectedIndex == 1)
            {
                dbipadvaluetxtbox.Enabled = false;
                dbpassvaluetxtbox.Enabled = false;
                dbportvaluetxtbox.Enabled = false;
                dbuservaluetxtbox.Enabled = false;
            }
            else
            {
                dbipadvaluetxtbox.Enabled = true;
                dbpassvaluetxtbox.Enabled = true;
                dbportvaluetxtbox.Enabled = true;
                dbuservaluetxtbox.Enabled = true;
            }
        }


        private void BrowseCnfFilePathBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Select CNF File";
                fileDialog.Filter = ".CNF Files (*.cnf)|*.cnf";
                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    CnfFilePathTxt.Text = fileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                Objmain.objlog.WriteError("BrowseCnfFilePathBtn_Click", "Error-- " + ex.ToString(), Objmain.ChannelName);
            }
        }


        //End of Class/////

    }
}
