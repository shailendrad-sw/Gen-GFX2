
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace graphicserver.Classes
{
    public class AgentMode
    {
        FrmMainServer ObjMain = null;
        Loging ObjLog = null;
        string OprationName = "";
        string TemplateName = "";
        string ReceiveVideoInfo = "";
        string ReceiveTextInfo = "";
        string TemplateValue = "";
        string TemplateType = "";
        string ReconsileKey = "";
        string Duration = "00:00:00:00";
        string Templatestarttime = "00:00:00:00";
        string Param1 = "", Param2 = "", Param3 = "", Param4 = "", Param5 = "", Param6 = "", Param7 = "", Param8 = "", Param9 = "", Param10 = "", BugType = "", DefaultExtension = "", DBTableName = "", DBColumnName = "", DBCondition = "", EndOffset = "";
        bool DBTemplate = false;
        public DataTable DtAsRun = null;
        public DataTable DtDirectPlayAsRun = null;
        bool PreOrTake = false;
        string Templatetitle = "";
      //  KeyValuePair<String, String> LastCommandRecvd;
        Dictionary<String, String> LastCommandRecvd1 = new Dictionary<string, string>();
        Dictionary<String, bool> CueTemplates = new Dictionary<string, bool>();
        public AgentMode(FrmMainServer ObjMain)
        {
            this.ObjMain = ObjMain;
            this.ObjLog = ObjMain.objlog;
            //   LastCommandRecvd = new KeyValuePair<String, String>("TmpName", System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
#if UTC_MODE
            LastCommandRecvd1.Add("TmpName",System.DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff"));
#else
            LastCommandRecvd1.Add("TmpName",System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
#endif
        }
        public bool IsTimeDifferenceenough(String s1, String s2)
        {
            DateTime date1 = DateTime.Parse(s1);
            DateTime date2 = DateTime.Parse(s2);
            TimeSpan ts = date2 - date1;
            if (ts.Days > 0)
                return true;
            string[] t1 = s1.Split(' ')[1].Split('.');
            string[] t2 = s2.Split(' ')[1].Split('.');
            String tt1 = t1[0] + ":" + Int64.Parse(t1[1]);
            String tt2 = t2[0] + ":" + Int64.Parse(t2[1]);
            String[] ft1 = tt1.Split(':');
            String[] ft2 = tt2.Split(':');

            long l1 = Int64.Parse(ft1[0]) * 3600 * 1000;
            long l2 = Int64.Parse(ft1[1]) * 60 * 1000;
            long l3 = Int64.Parse(ft1[2]) * 1000;
            long l4 = Int64.Parse(ft1[3]);
            long l5 = Int64.Parse(ft2[0]) * 3600 * 1000;
            long l6 = Int64.Parse(ft2[1]) * 60 * 1000;
            long l7 = Int64.Parse(ft2[2]) * 1000;
            long l8 = Int64.Parse(ft2[3]);
            long a = (l5 - l1) + (l6 - l2) + (l7 - l3) + (l8 - l4);
            if (((l5 - l1) + (l6 - l2) + (l7 - l3) + (l8 - l4)) >= ObjMain.CommandDelay)
                return true;
            return false;
        }
        public void HandelAgentPacket(string ReceivePacket, IPEndPoint remoteinfo)
        {
            //START||TITLE||LINERTEXT||TITLE.TXT||--mov--||ANY WHICH WAY YOU CAN~~AAAAA~~CCCCC||||||
            OprationName = TemplateName = ReceiveTextInfo = ReceiveVideoInfo = TemplateType = TemplateValue = "";
            String hostIp = remoteinfo.Address.ToString();
            String port = remoteinfo.Port.ToString();
            try
            {
                if (!ReceivePacket.Contains("||"))
                {
                    ObjLog.WriteEvents("HandelAgentPacket", "Receive Invalid Packet Structure,Server not accept..", ObjMain.ChannelName);
                    return;
                }
                //+++++++++++++Get All Information Provide By Agent ++++++++++++++++++++   
                string[] PacketInfo = ReceivePacket.Split(new string[] { "||" }, StringSplitOptions.None);
                OprationName = PacketInfo[0];
                //if (OprationName.ToUpper().Contains("SHOWPREVIEW"))
                //{
                //    if (ObjMain.isPreview == false)
                //        ObjMain.Invoke(new MethodInvoker(delegate { ObjMain.Previewwindow(); }));
                //    return;
                //}
                //else if (OprationName.ToUpper().Contains("HIDEPREVIEW"))
                //{
                //    if (ObjMain.isPreview == true)
                //        ObjMain.Invoke(new MethodInvoker(delegate { ObjMain.Previewwindow(); }));
                //    return;
                //}
                TemplateType = PacketInfo[2];
                TemplateName = PacketInfo[3].Split('.')[0].ToUpper();
                ReceiveVideoInfo = PacketInfo[4];
                ReceiveTextInfo = PacketInfo[5];
                Duration = PacketInfo[6];
                ReconsileKey = PacketInfo.Length >= 10 ? PacketInfo[9] : "";
                Templatestarttime = PacketInfo.Length >= 11 ? PacketInfo[10] : "";
                Templatetitle= PacketInfo.Length >= 13 ? PacketInfo[12] : "";
                //if (PacketInfo.Length > 10 && !string.IsNullOrEmpty(PacketInfo[10]) && (PacketInfo[10].Trim().Equals("0") || PacketInfo[10].Trim().Equals("1")))
                //{
                //    PreOrTake = PacketInfo[10].Equals("0") ? true : false;
                //    //PreOrTake = Convert.ToBoolean(PacketInfo[10]);
                //}
                //else
                //{
                //    PreOrTake = false;
                //}
                PreOrTake = false;
                switch (TemplateType.ToUpper())
                {
                    case "LINERTEXT":
                        TemplateValue = ReceiveTextInfo;
                        break;
                    case "TEMPLATE":
                        TemplateValue = ReceiveTextInfo;
                        break;
                    case "TICKERTEXT":
                        TemplateValue = ReceiveTextInfo;
                        break;
                    case "IMAGE":
                        TemplateValue = ReceiveTextInfo;
                        break;
                    case "COUNTDOWN":
                        TemplateValue = ReceiveTextInfo;
                        break;
                    default:
                        TemplateName = PacketInfo[1].ToUpper();
                        TemplateValue = ReceiveVideoInfo;
                        break;
                }
                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                {
                    if (OprationName.ToUpper().Contains("STATUS"))
                    {
                        string che;
                        che = "GFXClient||" + ObjMain.ChannelName + "||STATUS||";
                        if (ObjMain.UsedItems.Count() > 0)
                        {
                            if ((ObjMain.dgvsecevent.RowCount > 0))
                            {
                                string result = ObjMain.UsedItems.Find(item => item == "LOGO");
                                if (result != null)
                                {
                                    string data = che + result.ToString() + "||||";
                                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, data);
                                    ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + data, ObjMain.ChannelName);
                                }
                                foreach (DataGridViewRow row in ObjMain.dgvsecevent.Rows)
                                {
                                    string temp = row.Cells[4].Value.ToString();
                                    if (!string.IsNullOrWhiteSpace(temp))
                                    {
                                        string data = che + row.Cells[1].Value.ToString() + "||" + row.Cells[5].Value.ToString() + "||" + row.Cells[4].Value.ToString();
                                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, data);
                                        ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + data, ObjMain.ChannelName);
                                    }
                                    else
                                    {
                                        string data = che + row.Cells[1].Value.ToString() + "||" + row.Cells[5].Value.ToString() + "||" + "24:00:00:00";
                                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, data);
                                        ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + data, ObjMain.ChannelName);
                                    }

                                }
                            }
                            else
                            {
                                foreach (string str in ObjMain.UsedItems)
                                {
                                    string temp = str;
                                    string data = che + temp.ToString() + "||||";
                                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, data);
                                    ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + data, ObjMain.ChannelName);
                                }
                            }
                            return;
                        }
                        else
                        {
                            che = che + "||||";
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                            ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + che, ObjMain.ChannelName);
                            return;
                        }
                    }
                }
              
                // SCTE added by Anurag 22-4-26
                if ((OprationName.ToUpper() == "START" || OprationName.ToUpper() == "STOP") && TemplateName.ToUpper() == "SCTE")
                {
                    ObjMain.TriggerSCTE104Packet(ReceiveTextInfo, Duration);
                   
                }
                if ((OprationName.ToUpper() == "START" || OprationName.ToUpper() == "STOP") && TemplateName.ToUpper() == "HTMLTICKER")
                {
                   ObjMain.overlayToolStripMenuItem_Click(null, null);
                    Thread.Sleep(1000);
                    return;
                }
                switch (OprationName.ToUpper())
                {
                    case "START":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == true))
                        {
                            ObjMain.objlog.WriteEvents("HandleAgentPacket", $" Template={TemplateName} is already running,ignoring command", ObjMain.ChannelName);
                            return;
                        }
                        break;
                    case "STOP":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == false) && !(TemplateName.ToUpper() == "HTMLTICKER"))
                        {
                            ObjMain.objlog.WriteEvents("HandleAgentPacket", $" Template={TemplateName} is not running,ignoring command", ObjMain.ChannelName);
                            return;
                        }
                        break;
                    default:
                        break;
                }
                if (TemplateName.ToUpper() != "SCTE")
                {

                    if (ObjMain.AddDelayincommand == true)
                    {
                        try
                        {
                            
                            String check = LastCommandRecvd1.ElementAt(0).Key;
                            if (!LastCommandRecvd1.ContainsKey(TemplateName))
                            {
#if UTC_MODE
                                LastCommandRecvd1.Add(TemplateName, System.DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff"));
#else
                             LastCommandRecvd1.Add(TemplateName,System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
#endif
                                ObjMain.objlog.WriteEvents("HandleAgentPacket", $"new Saved template is={TemplateName}", ObjMain.ChannelName);
                            }
                            else
                            {
                                ObjMain.objlog.WriteEvents("HandleAgentPacket", $"Last template matched={TemplateName}", ObjMain.ChannelName);
#if UTC_MODE
                                String tmp = System.DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff");
#else
                            String tmp = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
#endif
                                if (!IsTimeDifferenceenough(LastCommandRecvd1[TemplateName], tmp))
                                {
                                    Thread.Sleep(ObjMain.CommandDelay);
                                    ObjMain.objlog.WriteEvents("HandleAgentPacket", $"Added sleep in operation in Template={TemplateName}", ObjMain.ChannelName);
                                    tmp = AddMillisecondsToNow(tmp, ObjMain.CommandDelay);
                                }
                                ObjMain.objlog.WriteEvents("HandleAgentPacket", $"going to save in LastCommandRecvd= {TemplateName}", ObjMain.ChannelName);

                                //  LastCommandRecvd1.Clear();
                                //   LastCommandRecvd1.Add(TemplateName, tmp);
                                LastCommandRecvd1[TemplateName] = tmp;
                            }
                        }
                        catch (Exception ex)
                        {
                            ObjLog.WriteError("HandelAgentPacket", "Error In saving and checking time of 2 temp command" + ex.ToString(), ObjMain.ChannelName);
                        }
                    }
                    if (TemplateValue.ToUpper() == "F10")
                    {
                        ExecuteDirectOpration(ref OprationName, ref TemplateName);
                    }
                    else
                        ExecuteOpration(ref OprationName, ref TemplateName, TemplateValue, PreOrTake, Templatetitle);
                }
                else
                {
                    FillSCTEDataInGrid(ref OprationName, ref TemplateName, TemplateValue, PreOrTake);
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("HandelAgentPacket", "Error In Handel Agent Packet" + ex.ToString(), ObjMain.ChannelName);
            }
        }
        public static string AddMillisecondsToNow(string nowString,int millisecondsToAdd)
        {
           
            // Parse the string back to DateTime
            DateTime now = DateTime.ParseExact(nowString, "dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

            // Add milliseconds
            DateTime updated = now.AddMilliseconds(millisecondsToAdd);

            // Return updated time as string
            return updated.ToString("dd-MM-yyyy HH:mm:ss.fff");
        }
        private void UpdateHideShowTemplateStatus(Boolean HideTemplate, String TemplateName)
        {
            try
            {
                foreach (DataGridViewRow row in ObjMain.dgvsecevent.Rows)
                {
                    if (row.Cells["SecTemplate"].Value.ToString().ToUpper() == TemplateName.ToUpper())
                    {
                        if (HideTemplate == true)
                        {
                            row.Cells["SecIsHide"].Value = "Y";
                            ObjLog.WriteEvents("UpdateHideShowTemplateStatus", $"templatename={TemplateName} is Hidden now.", ObjMain.ChannelName);
                            break;
                        }
                        else
                        {
                            row.Cells["SecIsHide"].Value = "N";
                            ObjLog.WriteEvents("UpdateHideShowTemplateStatus", $"templatename={TemplateName} is Shown now.", ObjMain.ChannelName);
                            break;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ObjLog.WriteError("UpdateHideShowTemplateStatus", "Error In Updating Show Hide Status ..." + ex.ToString(), ObjMain.ChannelName);
            }
        }

        public string GetTime()
        {
            try
            {
            #if UTC_MODE
                return DateTime.UtcNow.ToString("HH:mm:ss:00");
            #else
                 return DateTime.Now.ToString("HH:mm:ss:00");
            #endif
                //TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
                //return $"{ts.Hours.ToString("00")}:{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}:{Convert.ToInt16(ts.Milliseconds / 40).ToString("00")}";
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("GetTime", "Error In Getting Time ..." + ex.ToString(), ObjMain.ChannelName);
                return "00:00:00:00";
            }
        }


        private void FillInformationInGrid(SecondaryPacket sec)
        {
            try
            {
                ObjMain.dgvsecevent.Invoke((MethodInvoker)(() =>
                {
                    ObjMain.dgvsecevent.Rows.Add
                   (sec.Status + "",
                    sec.TemplateName + "",
                    sec.EventTime + "",
                    sec.Offset + "",
                    sec.Duration + "",
                    sec.StartTime + "",
                    sec.StartDate + "",
                    sec.EndTime + "",
                    sec.EndDate + "",
                    sec.TemplateValue + "",
                    sec.ReconcileKey + "",
                    sec.Hide + "",
                    sec.PrimaryId + "",
                    sec.PrimaryDuration + "",
                    sec.Channel + "",
                    sec.DeleteCheck + "",
                    sec.StarttimewithDay + "",
                    sec.EndtimewithDay + "",
                    sec.SecondaryLoadTime + "",
                    sec.LoadDate + "",
                    sec.BreakFreeBug + "");
                    ObjMain.dgvsecevent.ClearSelection();
                }));
                ObjMain.objlog.WriteEvents("FillSecondaryGrid", $"Adding Template Info(templatename={sec.TemplateName},starttime={sec.StartTime},startdate={sec.StartDate} , templatevalue={ sec.TemplateValue} ,channelname={sec.Channel} ){Environment.NewLine}", ObjMain.ChannelName);
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("FillInformationInGrid", "Error In Adding Detail In Grid   error-- " + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void RemoveRowInGrid(String TemplateName)
        {
            try
            {
                foreach (DataGridViewRow row in ObjMain.dgvsecevent.Rows)
                {
                    if (row.Cells["SecTemplate"].Value.ToString().ToUpper() == TemplateName.ToUpper())
                    {
                        ObjMain.dgvsecevent.Invoke((MethodInvoker)(() =>
                        {
                            try
                            {
                                ObjMain.dgvsecevent.Rows.RemoveAt(row.Index);
                                ObjMain.dgvsecevent.ClearSelection();
                                ObjMain.dgvsecevent.Refresh();
                            }
                            catch { }
                        }));
                        ObjLog.WriteEvents("RowsCheckOnFireEvent", $"templatename={TemplateName} remove in Grid .", ObjMain.ChannelName);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                ObjLog.WriteError("RemoveRowInGrid", $"Error In Remove Row in Grid ,template name {TemplateName} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private bool CueWork(string gfxscene, DataRow DrTempInfo,string Data)
        {
           int LayerNo = Convert.ToInt16(DrTempInfo["LayerNo"] + "");
            BugType = DrTempInfo["BugType"] + "";
            DefaultExtension = DrTempInfo["FileExtension"] + "";
            if (string.IsNullOrEmpty((DrTempInfo["DBTemplate"] + "")))
                DrTempInfo["DBTemplate"] = "false";
            DBTemplate = Convert.ToBoolean((DrTempInfo["DBTemplate"] + "").Trim());
            DBTableName = (DrTempInfo["DBTableName"] + "").Trim();
            DBColumnName = (DrTempInfo["DBColumnName"] + "").Trim();
            DBCondition = (DrTempInfo["DBCondition"] + "").Trim();
            EndOffset = (DrTempInfo["EndOffset"] + "").Trim();
            Duration = GetDurationInDouble(Duration, EndOffset);
            try
            {
                if (UpdateXmlFile1(DrTempInfo) == true)
                {
                    if (CueTemplates.ContainsKey(gfxscene.ToUpper()))
                    {
                        CueTemplates.Remove(gfxscene.ToUpper());
                        ObjLog.WriteEvents("CueWork", "Remooving previous cue for same tamplate=" + gfxscene, ObjMain.ChannelName);
                    }
                    CueTemplates.Add(gfxscene.ToUpper(), true);
                    return true;
                }
                else
                {
                    ObjLog.WriteEvents("CueWork", "Receive Instruction for cue Template," + gfxscene.ToUpper() + " cue fail in server !", ObjMain.ChannelName);
                    return false;
                }
            }
            catch(Exception ex)
            {
                ObjLog.WriteError("CueWork", "Exception at cue work=" + ex.Message, ObjMain.ChannelName);
                return false;
            }
        }

        public bool GetTemplate(String graphicsName)
        {   //To create template and get data from templatemaster
            try
            {
                //   if (DbOnlyForAsrun == false) { return; }
               /* if (dttemplate == null)
                {
                    dttemplate = new DataTable("ConfigData");
                    dttemplate.Columns.Add("SrNo", typeof(int));
                    dttemplate.Columns.Add("TemplateName");
                    dttemplate.Columns.Add("LayerNo");
                    dttemplate.Columns.Add("FileExtension");
                    dttemplate.Columns.Add("ChannelName");
                    dttemplate.Columns.Add("BugType");
                    dttemplate.Columns.Add("KillwithPrimary");
                    dttemplate.Columns.Add("ShowOnBreak");
                    dttemplate.Columns.Add("BugStatus");
                    dttemplate.Columns.Add("Param1");
                    dttemplate.Columns.Add("Param2");
                    dttemplate.Columns.Add("Param3");
                    dttemplate.Columns.Add("Param4");
                    dttemplate.Columns.Add("Param5");
                    dttemplate.Columns.Add("Param6");
                    dttemplate.Columns.Add("Param7");
                    dttemplate.Columns.Add("Param8");
                    dttemplate.Columns.Add("Param9");
                    dttemplate.Columns.Add("Param10");

                    dttemplate.Columns.Add("DBTemplate");
                    dttemplate.Columns.Add("DBTableName");
                    dttemplate.Columns.Add("DBColumnName");
                    dttemplate.Columns.Add("DBCondition");
                    dttemplate.Columns.Add("IntroTime");
                    dttemplate.Columns.Add("ExitTime");
                    dttemplate.Columns.Add("DurationBased");
                    dttemplate.Columns.Add("EndOffset");
                    dttemplate.Columns.Add("StartOffset");

                }*/
                if (ObjMain.EnableDb == false)
                {
                    return false;
                }
                if (ObjMain.DbOnlyForAsrun == true) { return false; }
                DataTable createtemplatedt;
                DbLayer _db = new DbLayer(ObjMain);
                createtemplatedt = _db.FetchDataFromDB("select sno as SrNo, channelid as ChannelName,templatedesc as Templatecontent ,templatename as TemplateName,updateflg  from templatemaster where channelid='" + ObjMain.ChannelName + "'" + "and templatename='" + graphicsName + "'");
                //   createtemplatedt = _db.FetchDataFromDB("select srno as SrNo, channelid as ChannelName, templatename as TemplateName, layerno as LayerNo, param1 as Param1, param2 as Param2, param3 as Param3,param4 as Param4, param5 as Param5 ,param6 as Param6 , param7 as Param7,param8 as Param8,param9 as Param9,param10 as Param10 , bugtype as BugType, killwithprimary as KillWithPrimary, fileextension as FileExtension, showonbreak as ShowOnBreak, bugstatus as BugStatus,dbtemplate as DBTemplate,dbtablename as DBTableName, dbcolumnname as DBColumnName,dbcondition as DBCondition from channelbugmaster where channelid='" + ObjMain.ChannelName + "'");
                if (createtemplatedt != null)
                {
                    createtemplatedt.TableName = "GetTemplate";
                    if (!Directory.Exists(ObjMain.TemplatePath))
                    {
                        ObjLog.WriteEvents("gettemplate", "Template Directory is  not found so skip Download template.", ObjMain.ChannelName);
                        return false;
                    }
 
                   
                        DataRow[] dr = createtemplatedt.Select("TemplateName ='" + graphicsName + "'");
                    if (dr.Length > 0)
                    {
                        string data = dr[0]["Templatecontent"] + "";
                        string templatename = (dr[0]["TemplateName"] + "").ToUpper();
                        int updateflag = Convert.ToInt32(dr[0]["updateflg"]);
                        //  createtemplatedt = _db.FetchDataFromDB("select srno as SrNo, channelid as ChannelName, templatename as TemplateName, layerno as LayerNo, param1 as Param1, param2 as Param2, param3 as Param3,param4 as Param4, param5 as Param5 ,param6 as Param6 , param7 as Param7,param8 as Param8,param9 as Param9,param10 as Param10 , bugtype as BugType, killwithprimary as KillWithPrimary, fileextension as FileExtension, showonbreak as ShowOnBreak, bugstatus as BugStatus,dbtemplate as DBTemplate,dbtablename as DBTableName, dbcolumnname as DBColumnName,dbcondition as DBCondition from channelbugmaster where channelid='" + ObjMain.ChannelName + "'");
                        //  DataRow[] dr1 = createtemplatedt.Select("TemplateName ='" + graphicsName + "'");
                        //  if (dr1.Length > 0)
                        {
                            //   string bugTypfrmDB = (dr1[0]["BugType"] + "").ToUpper();
                            ObjMain.SetBugtype();
                        }
                        if (!File.Exists(ObjMain.TemplatePath + @"\" + templatename + ".ml-cgs"))
                        {
                            File.WriteAllText(ObjMain.TemplatePath + @"\" + templatename + ".ml-cgs", data);
                            ObjLog.WriteEvents("gettemplate", "Template created " + templatename, ObjMain.ChannelName);
                        }
                        else if (updateflag == 1)
                        {
                            File.WriteAllText(ObjMain.TemplatePath + @"\" + templatename + ".ml-cgs", data);
                            ObjLog.WriteEvents("gettemplate", "Template created " + templatename, ObjMain.ChannelName);
                        }

                    }
                    else
                    {
                        ObjLog.WriteEvents("gettemplate", "no template found in database dt " + graphicsName, ObjMain.ChannelName);
                        return false;
                    }
                    _db.ExecuteQuery("UPDATE templatemaster SET updateflg=0  WHERE channelid='" + ObjMain.ChannelName + "'");

                }
                else
                {
                    ObjLog.WriteEvents("gettemplate", "No updated data found in tempatemaster", ObjMain.ChannelName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("gettemplate", "Error in getting template from database " + ex.ToString(), ObjMain.ChannelName);
                return false;
            }
            finally
            {
                //  DirectoryInfo(TemplatePath);
            }
            return true;
        }

        private bool ExecuteDirectOpration(ref string OprationName, ref string templatename)
        {
            try
            {
                switch (OprationName.ToUpper())
                {
                    case "START":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == true))
                        {
                            ObjLog.WriteEvents("ExecuteOpration", "Template: " + TemplateName + " is already running by Server ,so Exit Process!", ObjMain.ChannelName);
                            /*  if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                              {
                                  ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che + "||Template Already Running");
                                  ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che + "||Template Already Running", ObjMain.ChannelName);
                              }*/
                            return false;
                        }

                        if (GetTemplate(templatename.ToUpper()))
                        {
                            SecondaryPacket Sec = new SecondaryPacket();
                            Sec.Status = "R";
                            Sec.TemplateName = templatename.ToUpper();
                            Sec.TemplateValue = "";
                            Sec.Hide = "N";
                            Sec.Channel = ObjMain.ChannelName;
                            //  Sec.StartTime = GetTime();
                            Sec.StartTime = ObjLog.LTCTime;
#if UTC_MODE
                            Sec.StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                            Sec.StartDate = DateTime.Now.ToString("dd-MM-yyyy");
#endif
                            PrepareDirectAsRun(OprationName,Sec.StartTime,"","",Sec.TemplateName,"");

                            Sec.Duration = Duration;
                            if (ObjMain.LoadCgItems(templatename.ToUpper(), Duration, PreOrTake) == true)
                            {
                              /*  if (templatename.ToUpper() == "LOGO")
                                {
                                    ObjMain.SaveLogoFileDetail(templatename,"");
                                  
                                }*/
                                   FillInformationInGrid(Sec);
                            }
                        }
                        break;
                    case "STOP":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == false))
                        {
                            ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for stop Template, Template: " + TemplateName + " is not running in  server !", ObjMain.ChannelName);
                            /*  che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName;
                              if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                              {
                                  ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che + "||Not running in server");
                                  ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                              }*/
                            return false;
                        }
                        ObjLog.WriteEvents("ExecuteDirectOpration", $"Receive Instruction for stop Template:{ TemplateName } ", ObjMain.ChannelName);
                        try
                        {
                            ObjMain.KillCgItem(templatename, BugType, 300, PreOrTake);
                            PrepareDirectAsRun(OprationName,"", "", "", templatename, "");
                            RemoveRowInGrid(templatename);
                            /*   PrepareAsRunSetting("STOPKILL", "", Duration, "", TemplateName);
                               che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName;
                               if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                               {
                                   ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                   ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                               }*/
                        }
                        catch (Exception ex)
                        {
                            ObjLog.WriteError("ExecuteOpration (Kill Operation)", "Template: " + TemplateName + " Error in Kill Template!", ObjMain.ChannelName);
                            /*  che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                              if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                              {
                                  ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                  ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                              }*/
                            return false;
                        }
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("ExecuteOpration", $"Error In Execute Operation , OprationName ={OprationName},templatename {TemplateName}, templateValue = {TemplateValue} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
                string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                {
                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                    ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                }
                return false;
            }

        }



        public void PrepareDirectAsRun(string _OprationName, string starttime, string _timespan, string tempdata, string tmpname, string title = "")
        {
            try
            {
                if (ObjMain.ASRunEnable)
                {
                    if (DtDirectPlayAsRun == null)
                    {
                        DtDirectPlayAsRun = new DataTable("ASRUN");
                        DtDirectPlayAsRun.Columns.Add("AsRunDateTime");
                        DtDirectPlayAsRun.Columns.Add("AsRunTemplateName");
                        DtDirectPlayAsRun.Columns.Add("AsRunFilename");
                        DtDirectPlayAsRun.Columns.Add("AsRunTemplateFileName");
                        DtDirectPlayAsRun.Columns.Add("AsRunStatus");
                        DtDirectPlayAsRun.Columns.Add("AsRunDuration");
                        DtDirectPlayAsRun.Columns.Add("AsRunReconcileKey");
                        DtDirectPlayAsRun.Columns.Add("StartDateTime");
                        DtDirectPlayAsRun.Columns.Add("EndDateTime");
                        DtDirectPlayAsRun.Columns.Add("LocalDate");
                        DtDirectPlayAsRun.Columns.Add("Localtime");
                        DtDirectPlayAsRun.Columns.Add("Title");
                    }
                    if (_OprationName.ToUpper() == "START")
                    {
                        //if (DtDirectPlayAsRun.AsEnumerable().Any(e => e.Field<string>("AsRunTemplateName").ToLower() == TemplateName.ToLower()))
                        //{
                           
                        //    return;
                        //}
                      
                        DataRow Dr = DtDirectPlayAsRun.NewRow();
#if UTC_MODE
                          string _StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                     //   string _StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                        string _StartDate = DateTime.Now.ToString("yyyy-MM-dd");
#endif              
                        _StartDate = _StartDate + "|" + starttime;
                            Dr["AsRunDateTime"] = _StartDate;                        
                            Dr["AsRunTemplateName"] = tmpname;
                            Dr["AsRunTemplateFileName"] = tmpname;
                        
                        Dr["AsRunFilename"] = tempdata;
                        Dr["AsRunStatus"] = "done";
                        Dr["AsRunReconcileKey"] = "Designer";
                        Dr["AsRunDuration"] = _timespan;
                        Dr["Title"] = title;
#if UTC_MODE
                        string StartDateTime = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        string StartDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                        Dr["StartDateTime"] = StartDateTime;
                        Dr["EndDateTime"] = StartDateTime;
                        Dr["LocalDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        Dr["Localtime"] = DateTime.Now.ToString("HH:mm:ss:ff");

                        DtDirectPlayAsRun.Rows.Add(Dr);
                    }
                    if (_OprationName.ToUpper() == "STOP")
                    {
                        DataRow Dr = null;
                            Dr = DtDirectPlayAsRun.Select($"AsRunTemplateName='{tmpname.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunStatus"] = "done";
#if UTC_MODE
                            Dr["EndDateTime"] = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                            Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                            String endtime = ObjLog.LTCTime;
                            //  string  = DateTime.Now.ToString("yyyy-MM-dd");
#if UTC_MODE
                          string _endDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                     //   string _StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                            string _endDate = DateTime.Now.ToString("yyyy-MM-dd");
#endif
                            string _span = "";
                            TimeSpan diff = new TimeSpan();
                            _endDate = _endDate + "|" + endtime;
                            try
                            {
                                 diff = GetTimeDifference(Dr["AsRunDateTime"].ToString(), _endDate, 25);
                                int h = diff.Hours;
                                int m = diff.Minutes;
                                int s = diff.Seconds;
                                int f = (int)(diff.Milliseconds / 40);
                                if (f > 0)
                                    _span = $"{h:D2}:{m:D2}:{s:D2}:{f:D2}";
                                else
                                {
                                    f = 0;
                                    _span = $"{h:D2}:{m:D2}:{s:D2}:{f:D2}";
                                }
                            }
                            catch(Exception ex)
                            {
                                ObjLog.WriteEvents("prepareDirectAsRun", "Exception at getting duration" + ex.Message);
                            }
                               Dr["AsRunDuration"]  = _span.ToString();
                            int daysDiff = Getdaysdiff(Dr);
                            WriteAsRunFile(Dr, 0);
                            DtDirectPlayAsRun.Rows.Remove(Dr);
                        }

                    }

                }
            }
            catch(Exception ex)
            {

            }
         }

        private static DateTime ParseDateTimeWithFrames(string value, int fps)
        {
            // Split into date and time
            string[] parts = value.Split('|');

            if (parts.Length != 2)
                throw new FormatException("Invalid datetime format.");

            string datePart = parts[0];
            string timePart = parts[1];

            // HH:mm:ss:ff
            string[] t = timePart.Split(':');

            if (t.Length != 4)
                throw new FormatException("Invalid time format.");

            int hour = int.Parse(t[0]);
            int minute = int.Parse(t[1]);
            int second = int.Parse(t[2]);
            int frame = int.Parse(t[3]);

            DateTime dt = DateTime.ParseExact(
                datePart,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture);

            dt = dt
                .AddHours(hour)
                .AddMinutes(minute)
                .AddSeconds(second)
                .AddMilliseconds((1000.0 / fps) * frame);

            return dt;
        }
        public static TimeSpan GetTimeDifference(
        string dateTime1,
        string dateTime2,
        int fps)
        {
            DateTime dt1 = ParseDateTimeWithFrames(dateTime1, fps);
            DateTime dt2 = ParseDateTimeWithFrames(dateTime2, fps);

            return dt2 - dt1;
        }
        private bool ExecuteOpration(ref string OprationName, ref string templatename, string TemplateValue = "",bool PreOrTake = false,string templatetitle="")
        {
            try
            {
                string che;
                che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName;
              //  PrepareAsRunSetting(OprationName,Templatestarttime,Duration, TemplateValue, templatename);
                DataRow DrTempInfo = ObjMain._dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault();
                if (DrTempInfo == null)
                {
                    ObjLog.WriteEvents("ExecuteOpration", "Template: " + TemplateName + " is not configured, so Exit Process!", ObjMain.ChannelName);
                  //  PrepareAsRunSetting("UPDATE_STATUS","","","","");
                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                    {
                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che + "||Template Not configured");
                        ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                    }
                    return false;
                }
                Param1 = Param2 = Param3 = Param4 = Param5 = Param6 = Param7 = Param8 = Param9 = Param10 = BugType = EndOffset = "";
                int LayerNo = 0;
                BugType = DrTempInfo["BugType"] + "";
                switch (OprationName.ToUpper())
                {
                    case "CUE":
                        CueWork(templatename,DrTempInfo, TemplateValue);
                        break;
                    case "START":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == true))
                        {
                            ObjLog.WriteEvents("ExecuteOpration", "Template: " + TemplateName + " is already running by Server ,so Exit Process!", ObjMain.ChannelName);
                            if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                            {
                                ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che + "||Template Already Running");
                                ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che+"||Template Already Running", ObjMain.ChannelName);
                            }
                            return false;
                        }
                        PrepareAsRunSetting(OprationName, Templatestarttime, Duration, TemplateValue, templatename, templatetitle);
                        SecondaryPacket Sec = new SecondaryPacket();
                        Sec.Status = "R";
                        Sec.TemplateName = TemplateName.ToUpper();
                        Sec.TemplateValue = TemplateValue;
                        Sec.Hide = "N";
                        Sec.Channel = ObjMain.ChannelName;
                        Sec.StartTime = GetTime();

                        #if UTC_MODE
                        Sec.StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        #else
                        Sec.StartDate = DateTime.Now.ToString("dd-MM-yyyy");
                        #endif
                        Sec.Duration = Duration;
                       // Sec.Duration.Trim();
                        Param1 = DrTempInfo["Param1"] + "";
                        Param2 = DrTempInfo["Param2"] + "";
                        Param3 = DrTempInfo["Param3"] + "";
                        Param4 = DrTempInfo["Param4"] + "";
                        Param5 = DrTempInfo["Param5"] + "";
                        Param6 = DrTempInfo["Param6"] + "";
                        Param7 = DrTempInfo["Param7"] + "";
                        Param8 = DrTempInfo["Param8"] + "";
                        Param9 = DrTempInfo["Param9"] + "";
                        Param10 = DrTempInfo["Param10"] + "";
                        LayerNo = Convert.ToInt16(DrTempInfo["LayerNo"] + "");
                        BugType = DrTempInfo["BugType"] + "";
                        DefaultExtension = DrTempInfo["FileExtension"] + "";
                        if (string.IsNullOrEmpty((DrTempInfo["DBTemplate"] + "")))
                            DrTempInfo["DBTemplate"] = "false";
                        DBTemplate = Convert.ToBoolean((DrTempInfo["DBTemplate"] + "").Trim());
                        DBTableName = (DrTempInfo["DBTableName"] + "").Trim();
                        DBColumnName = (DrTempInfo["DBColumnName"] + "").Trim();
                        DBCondition = (DrTempInfo["DBCondition"] + "").Trim();
                        EndOffset = (DrTempInfo["EndOffset"] + "").Trim();
                        Duration = GetDurationInDouble(Duration, EndOffset);
                        //  UpdateXmlFile1(DrTempInfo);
                        //  if(UpdateXmlFile1(DrTempInfo) == true)
                        if (CueTemplates.Count > 0 && CueTemplates.ContainsKey(TemplateName.ToUpper()) && CueTemplates[TemplateName.ToUpper()] == true)
                        {
                            if (ObjMain.LoadCgItems(templatename.ToUpper(), Duration, PreOrTake) == true)
                            {
                                if (templatename.ToUpper() == "LOGO")
                                {
                                    ObjMain.SaveLogoFileDetail(templatename, TemplateValue);
                                    if (ObjMain.ScteEnable)
                                    {
                                        che = "START||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + "To multiviewer" + "||" + TemplateValue + "||" + "";
                                        ObjMain.SendUdpResponse(ObjMain.ScteIP, ObjMain.SctePort, che);
                                        ObjLog.WriteEvents("ExecuteOpration", "Sent to multiviewer-->" + che, ObjMain.ChannelName);
                                    }
                                }
                                ObjMain.ReadTriggerFile("", "AFTERSTART");
                                FillInformationInGrid(Sec);
                                if (!string.IsNullOrWhiteSpace(Sec.Duration))
                                {
                                    che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + Sec.Duration;
                                }
                                else
                                {
                                    che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + "24:00:00:00";
                                }
                                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                {
                                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                    ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                                }
                            }
                            else
                            {
                                PrepareAsRunSetting("UPDATE_STATUS", "", "", "", templatename);
                                che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                {
                                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                    ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                                }
                            }
                        }
                        else
                        {
                            ObjLog.WriteEvents("ExecuteOpration", "Normal route,cue unavailable", ObjMain.ChannelName);
                            if (UpdateXmlFile1(DrTempInfo) == true)
                            {
                                if (ObjMain.LoadCgItems(templatename.ToUpper(), Duration, PreOrTake) == true)
                                {
                                    if (templatename.ToUpper() == "LOGO")
                                    {
                                        ObjMain.SaveLogoFileDetail(templatename, TemplateValue);
                                        if (ObjMain.ScteEnable)
                                        {
                                            che = "START||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + "To multiviewer" + "||" + TemplateValue + "||" + "";
                                            ObjMain.SendUdpResponse(ObjMain.ScteIP, ObjMain.SctePort, che);
                                            ObjLog.WriteEvents("ExecuteOpration", "Sent to multiviewer-->" + che, ObjMain.ChannelName);
                                        }
                                    }
                                    ObjMain.ReadTriggerFile("", "AFTERSTART");
                                    FillInformationInGrid(Sec);
                                    if (!string.IsNullOrWhiteSpace(Sec.Duration))
                                    {
                                        che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + Sec.Duration;
                                    }
                                    else
                                    {
                                        che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + "24:00:00:00";
                                    }
                                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                    {
                                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                        ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                                    }
                                }
                                else
                                {
                                    PrepareAsRunSetting("UPDATE_STATUS", "", "", "", templatename);
                                    che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                    {
                                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                        ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                                    }
                                }
                            }
                        }
                        break;
                    case "STOP":
                        if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == false) && BugType.ToUpper() != "TRIGGER")
                        {
                            ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for stop Template, Template: " + TemplateName + " is not running in  server !", ObjMain.ChannelName);
                            che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName;
                            if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                            {
                                ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che+"||Not running in server");
                                ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                            }
                            return false;
                        }
                        ObjLog.WriteEvents("ExecuteOpration", $"Receive Instruction for stop Template:{ TemplateName } ,Bugtype {BugType} ", ObjMain.ChannelName);
                        try
                        {
                            // code Added by Anurag 11-3-24
                            //HANDLE NOW Packet for Horizontal Ticker Operation                         
                            ObjMain.KillCgItem(templatename, BugType,300,PreOrTake);
                            RemoveRowInGrid(templatename);
                            PrepareAsRunSetting("STOPKILL","", Duration, "",TemplateName);
                            che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName;
                            if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                            {
                                ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                            }
                        }
                        catch (Exception ex)
                        {
                            ObjLog.WriteError("ExecuteOpration (Kill Operation)", "Template: " + TemplateName + " Error in Kill Template!", ObjMain.ChannelName);
                            che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                            if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                            {
                                ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                            }
                            return false;
                        }
                        break;
                    case "HIDE":
                        ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for HIDE Template: " + TemplateName + "", ObjMain.ChannelName);
                        ObjMain.HideCgItem(templatename, BugType);
                        UpdateHideShowTemplateStatus(true, templatename);
                        break;
                    case "SHOW":
                        ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for Show Template: " + TemplateName + "", ObjMain.ChannelName);
                        ObjMain.ShowCgItem(templatename, BugType);
                        UpdateHideShowTemplateStatus(false, templatename);
                        break;
                    case "KILLALL":
                        ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for KILL All Template: " + TemplateName + "", ObjMain.ChannelName);
                        ObjMain.KillAllCgItem();
                        ObjMain.dgvsecevent.Invoke((MethodInvoker)(() =>
                        {
                            ObjMain.dgvsecevent.Rows.Clear();
                            ObjMain.dgvsecevent.Refresh();
                        }));
                        break;
                    case "REFRESH":
                        ObjLog.WriteEvents("ExecuteOpration", "Receive Instruction for REFRESH Template: " + TemplateName + "", ObjMain.ChannelName);
                        //Param1 = DrTempInfo["Param1"] + "";
                        //Param2 = DrTempInfo["Param2"] + "";
                        //Param3 = DrTempInfo["Param3"] + "";
                        //Param4 = DrTempInfo["Param4"] + "";
                        //Param5 = DrTempInfo["Param5"] + "";
                        //Param6 = DrTempInfo["Param6"] + "";
                        //Param7 = DrTempInfo["Param7"] + "";
                        //Param8 = DrTempInfo["Param8"] + "";
                        //Param9 = DrTempInfo["Param9"] + "";
                        //Param10 = DrTempInfo["Param10"] + "";
                        BugType = DrTempInfo["BugType"] + "";
                        DefaultExtension = DrTempInfo["FileExtension"] + "";
                        PrepareRefressElement(DrTempInfo);                      
                        break;
                    default:
                        ObjLog.WriteEvents("ExecuteOpration", $"OprationName= {OprationName} is not Implemented in Server", ObjMain.ChannelName);
                        che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Operation not found";
                        if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                        {
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                            ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("ExecuteOpration", $"Error In Execute Operation , OprationName ={OprationName},templatename {TemplateName}, templateValue = {TemplateValue} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
                string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                {
                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                    ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                }
                return false;
            }
            
        }
        /*     private bool UpdateXmlFile1(DataRow TmpRow)
             {
                 string ElementId = "";
                 string ElementType = "";
                 string[] TextData = new string[50];
                 string ElementValue = "";
                 bool isAvailableOnNas = false;
                 bool localavailable;
                 bool isAllright = true;
                 int x;
                 string[] ParamArray;
                 string[] subdataArray;
                 TextData = TemplateValue.Split(new string[] { "~~" }, StringSplitOptions.None);

                 int TotalParam;
                 ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out TotalParam);
                 for (int j = 1 ,i=0; j <= TotalParam; j++,i++)
                 {
                     String str = "Param" + j.ToString() + "";
                     String Parameter = TmpRow[str] + "";
                     if ((ParamArray = Parameter.Split('~')).Length == 2)
                     {
                         ElementId = ParamArray[0];
                         ElementType = ParamArray[1];
                         localavailable = false;
                         //if (TextData.Length < j)
                         //{
                         //    ObjLog.WriteEvents("UpdateXmlFile1", $"continueing because no value {ElementId} of type{ElementType}", ObjMain.ChannelName);
                         //    ObjMain.UpdateXMLComposition(TemplateName, ElementId, "", ElementType, BugType, true);
                         //    continue;
                         //}
                         ObjLog.WriteEvents("UpdateXmlFile1", $"updating value {ElementId} of type{ElementType} by {TextData[i]}", ObjMain.ChannelName);
                         if ((TextData[i] != "") && (subdataArray = TextData[i].Split('-')).Length == 2)
                         {
                             ObjLog.WriteEvents("UpdateXmlFile1", $"Entered in - mode  {ElementId} of type{ElementType} by {TextData[i]}", ObjMain.ChannelName);
                             try
                             {
                                 for (int k = 0; k < subdataArray.Length; k++)
                                 {
                                     if (k == 1)
                                     {
                                         String sstr = "Param" + (j + 1).ToString() + "";
                                         String sParameter = TmpRow[sstr] + "";
                                         string[] sParamArray;
                                         if ((sParamArray = sParameter.Split('~')).Length == 2)
                                         {
                                             ElementId = sParamArray[0];
                                             ElementType = sParamArray[1];
                                         }

                                     }

                                     ElementValue = ContentInfo((subdataArray.Length == 2 ? subdataArray[k] : ""), ElementType, str, out isAvailableOnNas, out localavailable);
                                     if ((isAvailableOnNas == false) && (localavailable == false))
                                     {
                                         isAllright = false;
                                         return false;
                                     }
                                     ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                                     ObjLog.WriteEvents("UpdateXmlFile1", $"updated  {ElementId} of type{ElementType} by {ElementValue}", ObjMain.ChannelName);
                                 }
                                 j = j + 1;
                             }
                             catch(Exception ex)
                             {
                                 ObjLog.WriteEvents("UpdateXmlFile1", $"Exception in - mode *****************"+ex.Message, ObjMain.ChannelName);
                             }
                         }
                         else
                         {
                             ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[i] : ""), ElementType, str, out isAvailableOnNas, out localavailable);
                             if ((isAvailableOnNas == false) && (localavailable == false))
                             {
                                 isAllright = false;
                                 return false;
                             }
                             ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                             j = j + 1;
                             String sstr = "Param" + (j).ToString() + "";
                             String sParameter = TmpRow[sstr] + "";
                             string[] sParamArray;
                             if ((sParamArray = sParameter.Split('~')).Length == 2)
                             {
                                 ElementId = sParamArray[0];
                                 ElementType = sParamArray[1];
                                 ObjMain.UpdateXMLComposition(TemplateName, ElementId, "", ElementType, BugType, isAvailableOnNas);
                             }

                         }

                     }

                 }
                 return true;
             }*/

        private bool UpdateXmlFile1(DataRow TmpRow)
        {
            string ElementId = "";
            string ElementType = "";
            string[] TextData = new string[200];
            string ElementValue = "";
            bool isAvailableOnNas = false;
            bool localavailable;
            bool isAllright = true;
            int x;
            string[] ParamArray;
            TextData = TemplateValue.Split(new string[] { "~~" }, StringSplitOptions.None);

            int TotalParam;
            ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out TotalParam);
            for (int j = 1; j <= TotalParam; j++)
            {
                String str = "Param" + j.ToString() + "";
                String Parameter = TmpRow[str] + "";
                if ((ParamArray = Parameter.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    if(TextData.Length >= 1 && TextData.Length >= j)
                    ObjLog.WriteEvents("UpdateXmlFile1", $"updating value {ElementId} of type{ElementType} by {TextData[j - 1]}", ObjMain.ChannelName);
                    else
                        ObjLog.WriteEvents("UpdateXmlFile1", $"updating value {ElementId} of type{ElementType} by blank", ObjMain.ChannelName);
                    //   ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[j - 1] : ""), ElementType, str, out isAvailableOnNas, out localavailable);
                    if (TemplateName.ToUpper() == "OSC" && (ElementType == "VIDEO" || ElementType == "IMAGE"))
                    {
                        ElementValue = Contentocta(((TextData.Length >= 1 && TextData.Length >= j) ? TextData[j - 1] : ""), ElementType, str, out isAvailableOnNas, out localavailable);
                    }
                    else
                    {
                        ElementValue = ContentInfo(((TextData.Length >= 1 && TextData.Length >= j) ? TextData[j - 1] : ""), ElementType, str, out isAvailableOnNas, out localavailable);
                        string bugtyp = TmpRow["BugType"] + "";
                        if (bugtyp.Trim() == "dbticker")
                        {
                            ElementValue = TextData[j - 1];
                            localavailable = true;
                        }
                    }
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        isAllright = false;
                        ElementValue = "";
                      //  return false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas, localavailable);
                }
            }
            return true;
        }
        private string GetDurationInDouble(string duration, string endoffset)
        {  //receive time formate "00:00:00"
            try
            {
                if (string.IsNullOrEmpty(duration))
                {
                    return "00:00:00:00";
                }


                string[] arrTime = duration.Split(':');
                int melesecond = 0;
                if (arrTime.Length == 4)
                {
                    melesecond = Convert.ToInt16(arrTime[3]);
                    melesecond = melesecond * 40;
                }
                melesecond = melesecond + Convert.ToInt16(endoffset == string.Empty ? "0" : (Convert.ToInt32(endoffset) * 40).ToString());

                TimeSpan tm = new TimeSpan(0, Convert.ToInt16(arrTime[0]), Convert.ToInt16(arrTime[1]), Convert.ToInt16(arrTime[2]), melesecond);
                return tm.TotalHours.ToString("00") + ":" + tm.Minutes.ToString("00") + ":" + tm.Seconds.ToString("00") + ":" + Convert.ToInt32(tm.Milliseconds / 40).ToString("00");
            }
            catch (Exception ex)
            {
                //ObjLog.WriteError("GetDurationInDouble", $"Error in converting time {duration} Error is -{ ex.ToString()}", ObjMain.ChannelName);
                return "00:00:00:00";
            }

        }

        private bool UpdateXmlFile()
        {
            string ElementId = "";
            string ElementType = "";
            string[] TextData = new string[10];
            string ElementValue = "";
            bool isAvailableOnNas = false;
            bool localavailable;
            bool isAllright = true;
            try
            {
                string[] ParamArray;
                TextData = TemplateValue.Split(new string[] { "~~" }, StringSplitOptions.None);
                if ((ParamArray = Param1.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[0] : ""),ElementType, "param1", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {    
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param2.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[1] : ""), ElementType, "param2", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                       
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param3.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[2] : ""), ElementType, "param3", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param4.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[3] : ""), ElementType, "param4", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param5.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[4] : ""), ElementType, "param5", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param6.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[5] : ""), ElementType, "param6", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param7.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[6] : ""), ElementType, "param7", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param8.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[7] : ""), ElementType, "param8", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param9.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[8] : ""), ElementType, "param9", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if ((ParamArray = Param10.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    localavailable = false;
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[9] : ""), ElementType, "param10", out isAvailableOnNas, out localavailable);
                    if ((isAvailableOnNas == false) && (localavailable == false))
                    {
                        /*  string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName + "||Content not Found";
                          if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                          {
                              ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                              ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                          }
                          return false;
                          */
                        isAllright = false;
                    }
                    ObjMain.UpdateXMLComposition(TemplateName, ElementId, ElementValue, ElementType, BugType, isAvailableOnNas);
                }
                if (isAllright == false)
                {
                    string che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + TemplateName + "||" + OprationName + "||Content not Found";
                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                    {
                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                        ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                    }
                    return false;

                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("UpdateXmlFile", $"Error In Update Xmlfile ,templatename {TemplateName}, bugtype = {BugType} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
                string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + TemplateName + "||" + OprationName+"||Error in Template Updation";
                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                {
                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                    ObjLog.WriteEvents("UpdateXmlFile", "Sent to client-->" + che, ObjMain.ChannelName);
                }
                return false;
            }
        }

        internal string Contentocta(string filename, string type, string parametername, out bool isAvailableinNas, out bool localavailable)
        {
            isAvailableinNas = false;
            localavailable = false;
            try
            {

                string path = null;
           
                if (type == "VIDEO")
                {
                    path = (ObjMain.ContentPath + @"\ANIMATION");
                }
                else if (type == "IMAGE")
                {
                    path = (ObjMain.ContentPath + @"\IMAGE");
                }
                else
                {
                    localavailable = false;
                    isAvailableinNas = false;
                    return filename;
                }

                if (string.IsNullOrEmpty(path))
                {
                       localavailable = false;
                    isAvailableinNas = false;
                    return filename;
                }
                else
                {
                    bool FlgFileFound = false;
                    string firstFile = Directory.EnumerateFiles(path, filename + "*")
                                    .FirstOrDefault();
                    if (firstFile != null)
                    {
                        firstFile = Path.GetFileName(firstFile);
                        FlgFileFound = true;
                        localavailable = true;
                        isAvailableinNas = false;
                        ObjLog.WriteEvents("ContentInfooct", "File found in Local Storage for update template filenameinitials=" + filename+ firstFile, ObjMain.ChannelName);
                        return firstFile;
                    }
                    else
                    {
                        if (!FlgFileFound)
                        {
                            ObjLog.WriteEvents("ContentInfooct", "File not found in Local Storage for update template filename=" + filename, ObjMain.ChannelName);
                            string FileN = Directory.EnumerateFiles(path, filename + "*")
                                    .FirstOrDefault();
                            if (FileN != null)
                            {
                                FileN = Path.GetFileName(FileN);
                                FlgFileFound = true;
                                localavailable = false;
                                isAvailableinNas = true;
                                ObjLog.WriteEvents("ContentInfooct", "File found in Secondary Storage for update template filenameinitials=" + filename + FileN, ObjMain.ChannelName);
                                return FileN;
                            }
                            else
                                ObjLog.WriteEvents("ContentInfooct", "File not found anywhere to update template filenameinitials=" + filename, ObjMain.ChannelName);

                        }
                    }
                    return filename;
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("ContentInfoocta", "Error in fetching file " + ex.ToString(), ObjMain.ChannelName);
                return filename;
            }
        }

        internal string ContentInfo(string filename, string type, string parametername, out bool isAvailableinNas, out bool localavailable)
        {
            isAvailableinNas = false;
            localavailable = false;
            try
            {

                string path = null;
                if (type == "TICKER-FILE")
                {
                    path = (ObjMain.ContentPath + @"\FILE");
                }
                else if (type == "VIDEO")
                {
                    path = (ObjMain.ContentPath + @"\ANIMATION");
                }
                else if (type == "IMAGE")
                {
                    path = (ObjMain.ContentPath + @"\IMAGE");
                }
                else if (type == "DBTEXT")
                {
                    try
                    {
                        string Query = $"select {parametername} from dynamicdbtemplate where  channelid='{ObjMain.ChannelName}' and templatename='{TemplateName}'";
                        DbLayer obj = new DbLayer(ObjMain);
                        DataTable objdt = obj.FetchDataFromDB(Query);
                        if (objdt != null)
                        {
                            localavailable = true;
                            return (objdt.Rows[0][0] + "");
                        }
                        else
                        {
                            return "";
                        }
                    }
                    catch
                    {
                        return "";
                    }
                }
                if (DBTemplate == true)
                {
                    string[] arr = DBColumnName.Split('~');
                    foreach (string str in arr)
                    {
                        if (str.Split(':')[0].ToLower() == parametername.ToLower())
                        {
                            string columnname = str.Split(':')[1];
                            string query = $" Select {columnname} from {DBTableName} ";
                            if (!string.IsNullOrWhiteSpace(DBCondition))
                            {
                                if (DBCondition.Trim().EndsWith("="))
                                {
                                    query = query + $" where {DBCondition} '{filename}'";
                                }
                                else
                                {
                                    query = query + $" where {DBCondition} ";
                                }
                            }
                            query = query + " limit 1 ";
                            DbLayer obj = new DbLayer(ObjMain);
                            DataTable Dt = obj.FetchDataFromDB(query);
                            if (Dt != null)
                            {
                                localavailable = true;
                                isAvailableinNas = true;
                                return Dt.Rows[0][0] + "";
                            }
                            else
                            {
                                return "";
                            }

                        }
                    }

                }
                if (string.IsNullOrEmpty(path))
                {
                    localavailable = true;
                    return filename;
                }
                else
                {
                    bool FlgFileFound = false;
                    string[] files = Directory.GetFiles(path, filename + ".*");
                    foreach (string file in files)
                    {
                        //  if (filename.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper()) //commented for graphicsx
                        if (filename.ToUpper() == Path.GetFileName(file).ToUpper() || filename.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())
                        {
                            FlgFileFound = true;
                          //  PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":done","","","");
                            localavailable = FlgFileFound;
                            return Path.GetFileName(file);
                        }
                    }
                    if (!FlgFileFound)
                    {
                        ObjLog.WriteEvents("ContentInfo", "File not found in Local Storage for update template filename=" + filename, ObjMain.ChannelName);
                        string[] nasfiles = Directory.GetFiles(ObjMain.SecondaryContentPath, filename + ".*");
                        foreach (string nasfilename in nasfiles)
                        {
                            //    if (filename.ToUpper() == Path.GetFileNameWithoutExtension(nasfilename).ToUpper()) ////commented for graphicsx
                            if (filename.ToUpper() == Path.GetFileName(nasfilename).ToUpper() || filename.ToUpper() == Path.GetFileNameWithoutExtension(nasfilename).ToUpper())
                            {
                                isAvailableinNas = true;
                              //  PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":done", "", "", "");
                                return Path.GetFileName(nasfilename);
                            }
                        }
                        if (isAvailableinNas == false)
                        {
                            ObjLog.WriteEvents("ContentInfo", "File not found in Secondary Storage[NAS] for update template filename=" + filename, ObjMain.ChannelName);
                           // PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":fail", "", "", "");
                            if (DefaultExtension != "")
                            {

                                filename = filename + DefaultExtension;
                            }
                        }

                    }
                }
                return filename;
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("ContentInfo", "Error in fetching extension " + ex.ToString(), ObjMain.ChannelName);
                return filename;
            }
        }
public int Getdaysdiff(DataRow Dr)
        {
            try {
                ////////////////////////////////////code added/////////////////////////////////////
                //  string _startdate = (Dr["AsRunDateTime"] + "").Replace("|", " ");
                string _startdate = (Dr["StartDateTime"] + "");
#if UTC_MODE
                string _enddate = DateTime.UtcNow.ToString("dd-MM-yyyy").Trim();
#else
                string _enddate = DateTime.Now.ToString("dd-MM-yyyy").Trim();
#endif
                _startdate = _startdate.Split(' ')[0].Trim();
                  DateTime date1 = DateTime.ParseExact(_startdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                  DateTime date2 = DateTime.ParseExact(_enddate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                // Calculate difference
                TimeSpan diff = date2 - date1;
                int daysdiff = Math.Abs(diff.Days);
                /////////////////////////////////////////end////////////////////////////////////
                return daysdiff;
            }
            catch(Exception ex) {
                ObjLog.WriteError("Getdaysdiff", "Error in getting difference in days" + ex.ToString(), ObjMain.ChannelName);
                return 0;
            }
        }

        public void PrepareAsRunSetting(string _OprationName,string starttime,string _timespan,string tempdata,string tmpname,string title="")
        {
            try
            {
                if (ObjMain.ASRunEnable)
                {
                    if (DtAsRun == null)
                    {
                        DtAsRun = new DataTable("ASRUN");
                        DtAsRun.Columns.Add("AsRunDateTime");
                        DtAsRun.Columns.Add("AsRunTemplateName");
                        DtAsRun.Columns.Add("AsRunFilename");
                        DtAsRun.Columns.Add("AsRunTemplateFileName");
                        DtAsRun.Columns.Add("AsRunStatus");
                        DtAsRun.Columns.Add("AsRunDuration");
                        DtAsRun.Columns.Add("AsRunReconcileKey");
                        DtAsRun.Columns.Add("StartDateTime");
                        DtAsRun.Columns.Add("EndDateTime");
                        DtAsRun.Columns.Add("LocalDate");
                        DtAsRun.Columns.Add("Localtime");
                        DtAsRun.Columns.Add("Title");
                    }
                    if (_OprationName == "START")
                    {
                        if (TemplateName == "")
                            TemplateName = tmpname;
                        if (DtAsRun.AsEnumerable().Any(e => e.Field<string>("AsRunTemplateName").ToLower() == TemplateName.ToLower()))
                        {
                            if (TemplateName.ToUpper() == "LOGO")
                            {
                                ObjMain.LogoTemplatestore[TemplateName.ToUpper()] = Templatestarttime;
                            }
                            return;
                        }
                        else
                        {
                            if (TemplateName.ToUpper() == "LOGO")
                            {
                                String _starttime = ObjMain.objlog.LTCTime.Trim();
                                ObjMain.LogoTemplatestore.Add(TemplateName.ToUpper(), _starttime);
                            }
                        }
                        DataRow Dr = DtAsRun.NewRow();
                        //   Dr["AsRunDateTime"] = GetAsRunDateTime();// DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");// "27-06-2018|10:30:03";
                        //DateTime now = DateTime.Now;
                        //DateTime onlyDate = now.Date;
                        //   string _StartDate = DateTime.Now.ToString("dd-MM-yyyy");
#if UTC_MODE
                          string _StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
                     //   string _StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                         string _StartDate = DateTime.Now.ToString("yyyy-MM-dd");
#endif
                        // var parts = starttime.Split(':');
                        //if (parts.Length == 4)
                        //{
                        //    starttime = parts[0] + ":" + parts[1] + ":" + parts[2];
                        // }
                        _StartDate =_StartDate + "|" + starttime;
                        Dr["AsRunDateTime"] = _StartDate;
                        //  string  Datetime = (Dr["AsRunDateTime"] + "").Replace("|", " ");
                        //   Datetime = DateTime.ParseExact(Datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        if (tmpname == "")
                        {
                            Dr["AsRunTemplateName"] = TemplateName;
                            Dr["AsRunTemplateFileName"] = TemplateName;
                        }
                        else
                        {
                            Dr["AsRunTemplateName"] = tmpname;
                            Dr["AsRunTemplateFileName"] = tmpname;
                        }
                        Dr["AsRunFilename"] = tempdata;
                        Dr["AsRunStatus"] = "done";
                        Dr["AsRunReconcileKey"] = ReconsileKey;
                        Dr["AsRunDuration"] = _timespan;
                        Dr["Title"] = title;
#if UTC_MODE
                        string StartDateTime = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        string StartDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                        Dr["StartDateTime"] = StartDateTime;
                        Dr["EndDateTime"] = StartDateTime;
                        Dr["LocalDate"] = DateTime.Now.ToString("yyyy-MM-dd"); 
                        Dr["Localtime"] = DateTime.Now.ToString("HH:mm:ss:ff"); 
                     
                        DtAsRun.Rows.Add(Dr);
                    }
                    if (_OprationName == "STOPKILL")
                    {
                        DataRow Dr = null;
                        if (tmpname=="")
                       Dr = DtAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        else
                       Dr = DtAsRun.Select($"AsRunTemplateName='{tmpname.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            //         if ((_timespan == "")&& !(_timespan.Equals(Dr["AsRunDuration"])))
                            if ((_timespan != ""))
                             Dr["AsRunDuration"] = _timespan;
                            Dr["AsRunStatus"] = "done";
#if UTC_MODE
                            Dr["EndDateTime"] = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                         //   Dr["AsRunDuration"] = _timespan;
                            int daysDiff = Getdaysdiff(Dr);
                            WriteAsRunFile(Dr, daysDiff);
                            DtAsRun.Rows.Remove(Dr);
                        }

                    }
                    if (_OprationName == "UPDATE_STATUS")
                    {
                        DataRow Dr = DtAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunStatus"] = "fail";
#if UTC_MODE
                            Dr["EndDateTime"] = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                            int daysDiff = Getdaysdiff(Dr);
                            WriteAsRunFile(Dr, daysDiff);
                            DtAsRun.Rows.Remove(Dr);
                        }
                    }
                    if (_OprationName.Contains("UPDATE_VALUE") == true)
                    {
                        DataRow Dr = DtAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            string[] array = _OprationName.Split(':');
                            string temvalue = Dr["AsRunFilename"] + "";
                            temvalue = temvalue + (temvalue == "" ? array[1] : "~" + array[1]);
                            Dr["AsRunFilename"] = temvalue;
                            Dr["AsRunStatus"] = array[2] + "";
                            if (array[2] == "fail")
                            {
#if UTC_MODE
                                Dr["EndDateTime"] = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                                int daysDiff = Getdaysdiff(Dr);
                                WriteAsRunFile(Dr, daysDiff);
                                DtAsRun.Rows.Remove(Dr);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("PrepareAsRunSetting", $"Error In Prepare AsRun Setting ...    error-- " + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private string GetAsRunDateTime()
        {
            try
            {
                if (ObjMain.AsRunTakeTime.ToUpper() == "LTC")
                {
                    string _lTime = ObjMain.objlog.LTCTime.Substring(0, 8);
                    if (_lTime.Trim() == "00:00:00")
                    {
#if UTC_MODE
                        return DateTime.UtcNow.ToString("dd-MM-yyyy|HH:mm:ss");
#else
                       return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
#endif

                    }
                    else
                    {
                        string formate = $"dd-MM-yyyy|{_lTime}";
#if UTC_MODE
                        return DateTime.UtcNow.ToString(formate);
#else
                       return DateTime.Now.ToString(formate);
#endif

                    }
                }
                else
                {
#if UTC_MODE
                    return DateTime.UtcNow.ToString("dd-MM-yyyy|HH:mm:ss");
#else
                       return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
#endif

                }
            }
            catch 
            {
                
#if UTC_MODE
                return DateTime.UtcNow.ToString("dd-MM-yyyy|HH:mm:ss");
#else
                       return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
#endif
            }

        }
        private void WriteAsRunFile(DataRow DrAsrun,int days)
        {
            FileStream fs = null;
            StreamWriter objWriter = null;
            string Message = "";
            string Query = "";
            string Datetime = "";
            string FileName = "";
#if UTC_MODE
            if (days == 0)
            {
                FileName = $@"{ObjMain.AsRunFolderPath}\AsRun_{DateTime.UtcNow.ToString("dd-MM-yyyy")}.txt";
            }
            else
            {
                string _startdate = (DrAsrun["StartDateTime"] + "");
                _startdate = _startdate.Split(' ')[0].Trim();
                FileName = $@"{ObjMain.AsRunFolderPath}\AsRun_{_startdate}.txt";
            }
#else
             FileName = $@"{ObjMain.AsRunFolderPath}\AsRun_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
#endif
            try
            {
                switch (ObjMain.AsRunType.ToUpper())
                {
                    case "BOTH":
                        ///Push Information In File
                        if (Directory.Exists(ObjMain.AsRunFolderPath) == false)
                        {
                            Directory.CreateDirectory(ObjMain.AsRunFolderPath);
                        }
                     //   string date = DateTime.Now.ToString("yyyy-MM-dd");
                     //   string time = DateTime.Now.ToString("HH:mm:ss.fff");
                        Message = $"{DrAsrun["AsRunDateTime"] + "" }|||{DrAsrun["AsRunTemplateName"] + "" }||{DrAsrun["AsRunFilename"] + "" }|{DrAsrun["AsRunTemplateFileName"] + "" }|{DrAsrun["AsRunStatus"] + "" }|{DrAsrun["AsRunDuration"] + ""}|{DrAsrun["AsRunReconcileKey"]}|{DrAsrun["LocalDate"]}|{DrAsrun["Localtime"]}"; 
                        if (File.Exists(FileName))
                        {
                            fs = new FileStream(FileName, FileMode.Append, FileAccess.Write);
                            objWriter = new StreamWriter(fs);
                            objWriter.BaseStream.Seek(0, SeekOrigin.End);
                        }
                        else
                        {
                            fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                            objWriter = new StreamWriter(fs);
                            objWriter.BaseStream.Seek(0, SeekOrigin.End);

                        }
                        objWriter.WriteLine(Message);
                        objWriter.Close();
                        fs.Close();
                        ///Push Information in DB
                        ///                    
                        Datetime = (DrAsrun["AsRunDateTime"] + "").Replace("|", " ");
                      //  Datetime = DateTime.ParseExact(Datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString(@"yyyy-MM-dd HH:mm:ss");
                        Datetime = Datetime.Trim();
                        //  Query = $"insert into  {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                        //        $" '{Datetime}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        Query = $"insert into  {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                               $" '{Datetime+""}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{DrAsrun["AsRunDuration"] + ""}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        DbLayer Db = new DbLayer(ObjMain);
                        Db.ExecuteQuery(Query);
                        break;
                    case "DB":
                        Datetime = (DrAsrun["AsRunDateTime"] + "").Replace("|", " ");
                      // Datetime = DateTime.ParseExact(Datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        Datetime = Datetime.Trim();
                        //   Query = $"insert into {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                        //               $" '{Datetime}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{DrAsrun["AsRunDuration"] + ""}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        Query = $"insert into {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                                      $" '{Datetime+""}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{DrAsrun["AsRunDuration"] + ""}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        DbLayer DbDB = new DbLayer(ObjMain);
                        DbDB.ExecuteQuery(Query);
                        break;
                    case "FILE":
                        if (Directory.Exists(ObjMain.AsRunFolderPath) == false)
                        {
                            Directory.CreateDirectory(ObjMain.AsRunFolderPath);
                        }
                        //    Message = $"{DrAsrun["AsRunDateTime"] + "" }|||{DrAsrun["AsRunTemplateName"] + "" }||{DrAsrun["AsRunFilename"] + "" }|{DrAsrun["AsRunTemplateFileName"] + "" }|{DrAsrun["AsRunStatus"] + "" }|{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}|{DrAsrun["AsRunReconcileKey"]}";
                    //    string date1 = DateTime.Now.ToString("yyyy-MM-dd");
                    //    string time1 = DateTime.Now.ToString("HH:mm:ss.fff");
                        Message = $"{DrAsrun["AsRunDateTime"] + "" }|{DrAsrun["Title"]+""}||{DrAsrun["AsRunTemplateName"] + "" }||{DrAsrun["AsRunFilename"] + "" }|{DrAsrun["AsRunTemplateFileName"] + "" }|{DrAsrun["AsRunStatus"] + "" }|{DrAsrun["AsRunDuration"] +""}|{DrAsrun["AsRunReconcileKey"]}|{DrAsrun["LocalDate"]}|{DrAsrun["Localtime"]}|{DrAsrun["StartDateTime"]}|{DrAsrun["EndDateTime"]}";
                        if (File.Exists(FileName))
                        {
                            fs = new FileStream(FileName, FileMode.Append, FileAccess.Write);
                            objWriter = new StreamWriter(fs);
                            objWriter.BaseStream.Seek(0, SeekOrigin.End);
                        }
                        else
                        {
                            fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                            objWriter = new StreamWriter(fs);
                            objWriter.BaseStream.Seek(0, SeekOrigin.End);
                        }
                        objWriter.WriteLine(Message);
                        objWriter.Close();
                        fs.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("WriteAsRunFile", $"Error In Write AsRun File ...    error-- " + ex.ToString(), ObjMain.ChannelName);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (objWriter != null)
                {
                    objWriter.Close();
                }
            }
        }

        private string GetDuration(string EndDate, string StartDate)
        {
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime DtEndTime = DateTime.ParseExact(EndDate, "dd-MM-yyyy HH:mm:ss:fff", provider);
                DateTime DtStartTime = DateTime.ParseExact(StartDate, "dd-MM-yyyy HH:mm:ss:fff", provider);
                TimeSpan ts = DtEndTime - DtStartTime;
                string time = ts.TotalHours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") + ":" + Convert.ToInt32(ts.Milliseconds / 40).ToString("00");
                return time;
            }
            catch (Exception ex)
            {

                return "00:00:00:00";
            }
        }

        private void PrepareRefressElement(DataRow DrTempInfo)
        {
            /*  string[] TempValue = new string[10];
              string ElementId = "";
              string ElementType = "";
              string PropertyName = "";
              string PropertyValue = "";
              try
              {
                  string[] ParamArray;
                  TempValue = TemplateValue.Split(new string[] { "~~" }, StringSplitOptions.None);
                  if ((ParamArray = Param1.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 1 ? TempValue[0] : ""), ElementType, "param1", out PropertyName, out PropertyValue);                   
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);                   
                  }
                  if ((ParamArray = Param2.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 2 ? TempValue[1] : ""), ElementType, "param2", out PropertyName, out PropertyValue);

                     ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param3.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 3 ? TempValue[2] : ""), ElementType, "param3", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param4.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 4 ? TempValue[3] : ""), ElementType, "param4", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param5.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 5 ? TempValue[4] : ""), ElementType, "param5", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param6.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 6 ? TempValue[5] : ""), ElementType, "param6", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param7.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 7 ? TempValue[6] : ""), ElementType, "param7", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param8.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 8 ? TempValue[7] : ""), ElementType, "param8", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param9.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 9 ? TempValue[8] : ""), ElementType, "param9", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                  }
                  if ((ParamArray = Param10.Split('~')).Length == 2)
                  {
                      ElementId = ParamArray[0];
                      ElementType = ParamArray[1];
                      GetPropertyValue((TempValue.Length >= 10 ? TempValue[9] : ""), ElementType, "param10", out PropertyName, out PropertyValue);
                      ObjMain.RefressElement(TemplateName,ElementId.ToLower(), PropertyName, PropertyValue,BugType);
                  }

              }
              catch (Exception ex)
              {
                  ObjLog.WriteError("PrepareRefressElement", $"Error In Prepare Refress Element ...    error-- " + ex.ToString(), ObjMain.ChannelName);

              }
              */
            string ElementId = "";
            string ElementType = "";
            string[] TextData = new string[50];
            string PropertyName = "";
            string PropertyValue = "";
            string[] ParamArray;
            TextData = TemplateValue.Split(new string[] { "~~" }, StringSplitOptions.None);
            int TotalParam;
            ObjMain.MaxParamFinder(ObjMain.XMLTemplateConfigfilepath, out TotalParam);
            for (int j = 1; j <= TotalParam; j++)
            {
                String str = "Param" + j.ToString() + "";
                String Parameter = DrTempInfo[str] + "";
                if ((ParamArray = Parameter.Split('~')).Length == 2)
                {
                    ElementId = ParamArray[0];
                    ElementType = ParamArray[1];
                    GetPropertyValue(TextData[j-1], ElementType,str, out PropertyName, out PropertyValue);
                    ObjMain.RefressElement(TemplateName, ElementId.ToLower(), PropertyName, PropertyValue, BugType);
                }
            }
        }


        public void updateTemplateOperationTime(string temp)
        {
            //    LastCommandRecvd1.Clear();
            if (LastCommandRecvd1.ContainsKey(temp))
            {
#if UTC_MODE
                LastCommandRecvd1[temp] = System.DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff");
#else
                LastCommandRecvd1[temp]=System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");

#endif
            }
        }



        internal void GetPropertyValue(string _ParameterValue, string _ElementType, string parametername,out string OutPropertyName, out string OutPropertyValue)
        {
            OutPropertyName = "";
            OutPropertyValue = "";
            try
            {
                
                string path = null;
               
                switch (_ElementType.ToUpper())
                {
                    case "TICKER-FILE":                        
                        path = (ObjMain.ContentPath + @"\FILE");
                        OutPropertyName = "ticker::content::content-item::path";
                        OutPropertyValue = GetFileInStor(_ParameterValue,  path);
                        break;
                    case "VIDEO":                       
                        path = (ObjMain.ContentPath + @"\ANIMATION");
                        OutPropertyName = "img::path";
                        OutPropertyValue = GetFileInStor(_ParameterValue, path);
                        break;
                    case "IMAGE":                       
                        path = (ObjMain.ContentPath + @"\IMAGE");
                        OutPropertyName = "img::path";
                        OutPropertyValue = GetFileInStor(_ParameterValue, path);
                        break;
                    case "TEXT":
                        OutPropertyName = "text";
                        OutPropertyValue = _ParameterValue;
                        break;                                       
                    case "DBTEXT":
                        try
                        {
                            OutPropertyName = "text";                          
                            string Query = $"select {parametername} from dynamicdbtemplate where  channelid='{ObjMain.ChannelName}' and templatename='{TemplateName}'";
                            DbLayer obj = new DbLayer(ObjMain);
                            DataTable objdt = obj.FetchDataFromDB(Query);
                            if (objdt != null)
                            {
                                OutPropertyValue = (objdt.Rows[0][0] + "");
                            }
                            else
                            {
                                OutPropertyValue = "";
                            }
                        }
                        catch
                        {
                            OutPropertyValue = "";
                        }
                        break;
                    default:
                        break;
                     
                }
                ObjLog.WriteEvents("GetPropertyValue", $"refreshing value of{_ElementType} with {_ParameterValue} Implemented as {OutPropertyName}and{OutPropertyValue}", ObjMain.ChannelName);
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("GetPropertyValue", "Error in get template Property  and template Value Detail" + ex.ToString(), ObjMain.ChannelName);               
            }
        }

        private string GetFileInStor(string _ParameterValue, string path)
        {
            bool FlgFileFound = false;
            try
            {
                string[] filefiles = Directory.GetFiles(path, _ParameterValue + ".*");
                foreach (string file in filefiles)
                {
                    if (_ParameterValue.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())
                    {
                        FlgFileFound = true;
                        return file;
                    }
                }
                if (FlgFileFound == false)
                {
                    ObjLog.WriteEvents("GetPropertyValue", "File not found in Local FILE Storage for update template filename=" + _ParameterValue, ObjMain.ChannelName);
                    string[] nasfiles = Directory.GetFiles(ObjMain.SecondaryContentPath, _ParameterValue + ".*");
                    foreach (string nasfilename in nasfiles)
                    {
                        if (_ParameterValue.ToUpper() == Path.GetFileNameWithoutExtension(nasfilename).ToUpper())
                        {
                            return nasfilename;
                        }
                    }
                    ObjLog.WriteEvents("ContentInfo", "File not found in Secondary Storage[NAS] for update template filename=" + _ParameterValue, ObjMain.ChannelName);
                }
                return "";
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("GetFileInStor", "Error in fetching Data in Storage... " + ex.ToString(), ObjMain.ChannelName);
                return "";
            }
        }


        public bool FillSCTEDataInGrid(ref string OprationName, ref string templatename, string TemplateValue = "", bool PreOrTake = false)
        {
            // try
            // {
            string che;
            che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName;


            Param1 = Param2 = Param3 = Param4 = Param5 = Param6 = Param7 = Param8 = Param9 = Param10 = BugType = EndOffset = "";


            switch (OprationName.ToUpper())
            {
                case "START":

                    if ((ObjMain.UsedItems.Contains(TemplateName.ToUpper()) == true))
                    {
                        ObjLog.WriteEvents("FillSCTEDataInGrid", "Template: " + TemplateName + " is already running by Server ,so Exit Process!", ObjMain.ChannelName);
                        if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                        {
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che + "||Template Already Running");
                            ObjLog.WriteEvents("FillSCTEDataInGrid", "Sent to client-->" + che + "||Template Already Running", ObjMain.ChannelName);
                        }
                        return false;
                    }
                    PrepareAsRunSetting(OprationName, Templatestarttime, Duration, TemplateValue, templatename);
                    SecondaryPacket Sec = new SecondaryPacket();
                    Sec.Status = "R";
                    Sec.TemplateName = TemplateName.ToUpper();
                    Sec.TemplateValue = TemplateValue;
                    Sec.Hide = "N";
                    Sec.Channel = ObjMain.ChannelName;
                    Sec.StartTime = GetTime();

#if UTC_MODE
                    Sec.StartDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                        Sec.StartDate = DateTime.Now.ToString("dd-MM-yyyy");
#endif
                    Sec.Duration = Duration;
                    // Sec.Duration.Trim();


                    Duration = GetDurationInDouble(Duration, EndOffset);




                    //ObjMain.ReadTriggerFile("", "AFTERSTART");
                    FillInformationInGrid(Sec);
                  //  if (OprationName.ToUpper() == "START")
                        ObjMain.UsedItems.Add(templatename.ToUpper());
                    if (!string.IsNullOrWhiteSpace(Sec.Duration))
                    {
                        che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + Sec.Duration;
                    }
                    else
                    {
                        che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName + "||" + Sec.StartTime + "||" + "24:00:00:00";
                    }
                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                    {
                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                        ObjLog.WriteEvents("FillSCTEDataInGrid", "Sent to client-->" + che, ObjMain.ChannelName);
                    }
                    break;
                case "STOP":
                    ObjLog.WriteEvents("FillSCTEDataInGrid", $"Receive Instruction for stop Template:{ TemplateName } ,Bugtype {BugType} ", ObjMain.ChannelName);
                    try
                    {
                        // code Added by Anurag 11-3-24
                        //HANDLE NOW Packet for Horizontal Ticker Operation                         

                        // ObjMain.KillCgItem(templatename, BugType, 300, PreOrTake);

                        RemoveRowInGrid(templatename);
                        ObjMain.UsedItems.Remove(templatename.ToUpper());
                        PrepareAsRunSetting("STOPKILL", "", Duration, "", TemplateName);
                        che = "GFXClient||" + ObjMain.ChannelName + "||SUCCESS||" + templatename + "||" + OprationName;
                        if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                        {
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                            ObjLog.WriteEvents("FillSCTEDataInGrid", "Sent to client-->" + che, ObjMain.ChannelName);
                        }
                    }
                    catch (Exception ex)
                    {
                        ObjLog.WriteError("FillSCTEDataInGrid (Kill Operation)", "Template: " + TemplateName + " Error in Kill Template!", ObjMain.ChannelName);
                        che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
                        if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                        {
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                            ObjLog.WriteEvents("FillSCTEDataInGrid", "Sent to client-->" + che, ObjMain.ChannelName);
                        }
                        return false;
                    }
                    break;


                default:
                    ObjLog.WriteEvents("ExecuteOpration", $"OprationName= {OprationName} is not Implemented in Server", ObjMain.ChannelName);
                    che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Operation not found";
                    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                    {
                        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                        ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                    }
                    break;
            }
            return true;
            // }
            //catch (Exception ex)
            //{
            //    ObjLog.WriteError("ExecuteOpration", $"Error In Execute Operation , OprationName ={OprationName},templatename {TemplateName}, templateValue = {TemplateValue} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
            //    string che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName + "||Template not found or crupt";
            //    if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
            //    {
            //        ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
            //        ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
            //    }
            //    return false;
            //}
        }


        //End of Class Agent Mode
    }


}
