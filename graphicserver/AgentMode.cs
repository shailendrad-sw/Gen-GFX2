using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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
        string Param1 = "", Param2 = "", Param3 = "", Param4 = "", Param5 = "", Param6 = "", Param7 = "", Param8 = "", Param9 = "", Param10 = "", BugType = "", DefaultExtension = "", DBTableName = "", DBColumnName = "", DBCondition = "", EndOffset = "";
        bool DBTemplate = false;
        DataTable DtAsRun = null;
        bool PreOrTake = false;
      //  KeyValuePair<String, String> LastCommandRecvd;
        Dictionary<String, String> LastCommandRecvd1 = new Dictionary<string, string>();

        public AgentMode(FrmMainServer ObjMain)
        {
            this.ObjMain = ObjMain;
            this.ObjLog = ObjMain.objlog;
         //   LastCommandRecvd = new KeyValuePair<String, String>("TmpName", System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
            LastCommandRecvd1.Add("TmpName",System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
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
            if (((l5 - l1) + (l6 - l2) + (l7 - l3) + (l8 - l4)) >= 400)
                return true;
            return false;
        }
        public void HandelAgentPacket(string ReceivePacket)
        {
            //START||TITLE||LINERTEXT||TITLE.TXT||--mov--||ANY WHICH WAY YOU CAN~~AAAAA~~CCCCC||||||
            OprationName = TemplateName = ReceiveTextInfo = ReceiveVideoInfo = TemplateType = TemplateValue = "";
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

                if (PacketInfo.Length > 10 && !string.IsNullOrEmpty(PacketInfo[10]) && (PacketInfo[10].Trim().Equals("0") || PacketInfo[10].Trim().Equals("1")))
                {
                    PreOrTake = PacketInfo[10].Equals("0") ? true : false;
                    //PreOrTake = Convert.ToBoolean(PacketInfo[10]);
                }
                else
                {
                    PreOrTake = false;
                }
                //if (!OprationName.ToUpper().Contains("STATUS"))
                //    PreOrTake = string.IsNullOrEmpty(PacketInfo[10].Trim()) ? false : !Convert.ToBoolean(PacketInfo[10].Trim());
               
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
                        //string filepath = ObjMain.ContentPath + "\\" + "ANIMATION" + "\\" + TemplateValue;
                        //if (!File.Exists(filepath))
                        //{
                        //    ObjLog.WriteEvents("UpdateXMLComposition", "File: " + ObjMain.ContentPath + "\\" + "ANIMATION" + "\\" + TemplateValue + " doesn't exists.", ObjMain.ChannelName);
                        //    return;
                        //}
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
                                    ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->"+data, ObjMain.ChannelName);
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
                            che = che+ "||||";
                            ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                            ObjLog.WriteEvents("HandelAgentPacket", "Sent to client-->" + che, ObjMain.ChannelName);
                            return;
                        }
                    }
                }
                if (ObjMain.AddDelayincommand == true)
                {
                    try
                    {
                        //   String check = LastCommandRecvd.Key;
                        String check = LastCommandRecvd1.ElementAt(0).Key;
                        ObjMain.objlog.WriteEvents("HandleAgentPacket", $"Saved template is={check}", ObjMain.ChannelName);
                      //  if (!String.Equals(LastCommandRecvd.Key, TemplateName))
                      if(!LastCommandRecvd1.ContainsKey(TemplateName))
                        {
                            // LastCommandRecvd = new KeyValuePair<String, String>(TemplateName, System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
                            LastCommandRecvd1.Clear();
                            LastCommandRecvd1.Add(TemplateName, System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
                        }
                        else
                        {
                            ObjMain.objlog.WriteEvents("HandleAgentPacket", $"Last template matched={TemplateName}", ObjMain.ChannelName);
                            String tmp = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
                            if (!IsTimeDifferenceenough(LastCommandRecvd1.ElementAt(0).Value, tmp))
                            {
                                Thread.Sleep(400);
                                ObjMain.objlog.WriteEvents("HandleAgentPacket", $"Added sleep in operation in Template={TemplateName}", ObjMain.ChannelName);
                            }
                            ObjMain.objlog.WriteEvents("HandleAgentPacket", $"going to save in LastCommandRecvd= {TemplateName}", ObjMain.ChannelName);
                            //  LastCommandRecvd = new KeyValuePair<String, String>(TemplateName, tmp);
                            LastCommandRecvd1.Clear();
                            LastCommandRecvd1.Add(TemplateName, tmp);
                        }
                        /////alternate
                        //  String check1 = LastComm
                        ////////end//
                    }
                    catch (Exception ex)
                    {
                        ObjLog.WriteError("HandelAgentPacket", "Error In saving and checking time of 2 temp command" + ex.ToString(), ObjMain.ChannelName);
                    }
                }
                ExecuteOpration(ref OprationName, ref TemplateName, TemplateValue, PreOrTake);
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("HandelAgentPacket", "Error In Handel Agent Packet" + ex.ToString(), ObjMain.ChannelName);
            }
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
                return DateTime.Now.ToString("HH:mm:ss:00");
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

        private bool ExecuteOpration(ref string OprationName, ref string templatename, string TemplateValue = "",bool PreOrTake = false)
        {
            try
            {
                string che;
                che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName;
                PrepareAsRunSetting(OprationName);
                DataRow DrTempInfo = ObjMain._dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault();
                if (DrTempInfo == null)
                {
                    ObjLog.WriteEvents("ExecuteOpration", "Template: " + TemplateName + " is not configured, so Exit Process!", ObjMain.ChannelName);
                    PrepareAsRunSetting("UPDATE_STATUS");
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
                        SecondaryPacket Sec = new SecondaryPacket();
                        Sec.Status = "R";
                        Sec.TemplateName = TemplateName.ToUpper();
                        Sec.TemplateValue = TemplateValue;
                        Sec.Hide = "N";
                        Sec.Channel = ObjMain.ChannelName;
                        Sec.StartTime = GetTime();
                        Sec.StartDate = DateTime.Now.ToString("dd-MM-yyyy");
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
                        //  if(UpdateXmlFile()==true)
                        UpdateXmlFile();
                        if (ObjMain.LoadCgItems(templatename.ToUpper(), Duration, PreOrTake) == true)
                        {
                            ObjMain.ReadTriggerFile("", "AFTERSTART");
                            FillInformationInGrid(Sec);
                                 if (!string.IsNullOrWhiteSpace (Sec.Duration))
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
                            PrepareAsRunSetting("UPDATE_STATUS");
                            che = "GFXClient||" + ObjMain.ChannelName + "||FAIL||" + templatename + "||" + OprationName+"||Template not found or crupt";
                                if (ObjMain.GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                {
                                    ObjMain.SendUdpResponse(ObjMain.GfxclientIp, ObjMain.GfxclientPort, che);
                                    ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
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
                          
                            ObjMain.KillCgItem(templatename, BugType, 300, PreOrTake);
                            RemoveRowInGrid(templatename);
                            PrepareAsRunSetting("STOPKILL");
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
                        BugType = DrTempInfo["BugType"] + "";
                        DefaultExtension = DrTempInfo["FileExtension"] + "";
                        PrepareRefressElement();                      
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
                    ElementValue = ContentInfo((TextData.Length >= 1 ? TextData[0] : ""), ElementType, "param1", out isAvailableOnNas, out localavailable);
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
                        if (filename.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())
                        {
                            FlgFileFound = true;
                            PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":done");
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
                            if (filename.ToUpper() == Path.GetFileNameWithoutExtension(nasfilename).ToUpper())
                            {
                                isAvailableinNas = true;
                                PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":done");
                                return Path.GetFileName(nasfilename);
                            }
                        }
                        if (isAvailableinNas == false)
                        {
                            ObjLog.WriteEvents("ContentInfo", "File not found in Secondary Storage[NAS] for update template filename=" + filename, ObjMain.ChannelName);
                            PrepareAsRunSetting("UPDATE_VALUE:" + filename + ":fail");
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


        private void PrepareAsRunSetting(string _OprationName)
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
                    }
                    if (_OprationName == "START")
                    {
                        if (DtAsRun.AsEnumerable().Any(e => e.Field<string>("AsRunTemplateName").ToLower() == TemplateName.ToLower()))
                        {
                            return;
                        }
                        DataRow Dr = DtAsRun.NewRow();
                        Dr["AsRunDateTime"] = GetAsRunDateTime();// DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");// "27-06-2018|10:30:03";
                        Dr["AsRunTemplateName"] = TemplateName;
                        Dr["AsRunFilename"] = "";
                        Dr["AsRunTemplateFileName"] = TemplateName;
                        Dr["AsRunStatus"] = "done";
                        Dr["AsRunReconcileKey"] = ReconsileKey;
                        string StartDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
                        Dr["StartDateTime"] = StartDateTime;
                        Dr["EndDateTime"] = StartDateTime;
                        DtAsRun.Rows.Add(Dr);


                    }
                    if (_OprationName == "STOPKILL")
                    {
                        DataRow Dr = DtAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunStatus"] = "done";
                            Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
                            WriteAsRunFile(Dr);
                            DtAsRun.Rows.Remove(Dr);
                        }

                    }
                    if (_OprationName == "UPDATE_STATUS")
                    {
                        DataRow Dr = DtAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunStatus"] = "fail";
                            WriteAsRunFile(Dr);
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
                                WriteAsRunFile(Dr);
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
                        return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
                    }
                    else
                    {
                        string formate = $"dd-MM-yyyy|{_lTime}";
                        return DateTime.Now.ToString(formate);
                    }
                }
                else
                {
                    return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
                }
            }
            catch 
            {
                return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");

            }

        }
        private void WriteAsRunFile(DataRow DrAsrun)
        {
            FileStream fs = null;
            StreamWriter objWriter = null;
            string Message = "";
            string Query = "";
            string Datetime = "";
            string FileName = $@"{ObjMain.AsRunFolderPath}\AsRun_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
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
                        Message = $"{DrAsrun["AsRunDateTime"] + "" }|||{DrAsrun["AsRunTemplateName"] + "" }||{DrAsrun["AsRunFilename"] + "" }|{DrAsrun["AsRunTemplateFileName"] + "" }|{DrAsrun["AsRunStatus"] + "" }|{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}|{DrAsrun["AsRunReconcileKey"]}";
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
                        Datetime = DateTime.ParseExact(Datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        Query = $"insert into  {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                                      $" '{Datetime}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        DbLayer Db = new DbLayer(ObjMain);
                        Db.ExecuteQuery(Query);
                        break;
                    case "DB":
                        Datetime = (DrAsrun["AsRunDateTime"] + "").Replace("|", " ");
                        Datetime = DateTime.ParseExact(Datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        Query = $"insert into {ObjMain.AsRunTableName }  ({ObjMain.AsRunDateTimeColumn},{ObjMain.AsRunTemplateNameColumn},{ObjMain.AsRunFilenameColumn},{ObjMain.AsRunTemplateFileNameColumn},{ObjMain.AsRunStatusColumn},{ObjMain.AsRunDurationColumn},{ObjMain.AsRunReconcileKeyColumn}, channelID ) values( " +
                                      $" '{Datetime}','{DrAsrun["AsRunTemplateName"] + ""}','{DrAsrun["AsRunFilename"] + "" }','{DrAsrun["AsRunTemplateFileName"] + ""}','{DrAsrun["AsRunStatus"] + "" }','{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}','{DrAsrun["AsRunReconcileKey"] + ""}','{ObjMain.AsRunChannelNameMam}')";
                        DbLayer DbDB = new DbLayer(ObjMain);
                        DbDB.ExecuteQuery(Query);
                        break;
                    case "FILE":
                        if (Directory.Exists(ObjMain.AsRunFolderPath) == false)
                        {
                            Directory.CreateDirectory(ObjMain.AsRunFolderPath);
                        }
                        Message = $"{DrAsrun["AsRunDateTime"] + "" }|||{DrAsrun["AsRunTemplateName"] + "" }||{DrAsrun["AsRunFilename"] + "" }|{DrAsrun["AsRunTemplateFileName"] + "" }|{DrAsrun["AsRunStatus"] + "" }|{GetDuration(DrAsrun["EndDateTime"] + "", DrAsrun["StartDateTime"] + "")}|{DrAsrun["AsRunReconcileKey"]}";
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
            catch (Exception)
            {

                return "00:00:00:00";
            }
        }

        private void PrepareRefressElement()
        {
            string[] TempValue = new string[10];
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
                    ObjMain.RefressElement(TemplateName, ElementId.ToLower(), PropertyName, PropertyValue, BugType);                   
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

        //End of Class Agent Mode
    }


}
