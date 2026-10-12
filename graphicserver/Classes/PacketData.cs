using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    class PacketData : IDisposable
    {
        FrmMainServer objmainform = null;
        private string Channelname { get; set; }
        private string[] CommercialPrefix { get; set; }
        private string[] PromoPrefix { get; set; }
        private string[] ProgramPrefix { get; set; }
        public List<string> SkipPrefix = new List<string>();
        public PacketData(FrmMainServer objmainform)
        {
            this.objmainform = objmainform;
            this.Channelname = objmainform.ChannelName;
        }
        internal void StorePrefix()
        {
            try
            {
                SkipPrefix.Clear();
                CommercialPrefix = objmainform.CommercialPrefix.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                ProgramPrefix = objmainform.ProgramPrefix.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                PromoPrefix = objmainform.PromoPrefix.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in CommercialPrefix)
                {
                    SkipPrefix.Add(item.ToUpper());
                }
                foreach (string item in ProgramPrefix)
                {
                    SkipPrefix.Add(item.ToUpper());
                }
                foreach (string item in PromoPrefix)
                {
                    SkipPrefix.Add(item.ToUpper());
                }
            }
            catch (Exception ex)
            {
                objmainform.objlog.WriteError("StorePrefix", "Error-- " + ex.ToString(), Channelname);
            }
        }
        internal void FillPrimaryData(string[] Primarydata)
        {
            try
            {
                PrimaryPacket pri;
                pri.Event = (Primarydata[1] + "").ToUpper();
                pri.Channelid = (Primarydata[2] + "").ToUpper();
                pri.EventId = (Primarydata[3] + "").ToUpper();
                pri.EventTime = (Primarydata[4] + "").ToUpper();
                pri.Duration = (Primarydata[5] + "").ToUpper();
             //   pri.InsertDate = DateTime.Now.ToString("dd-MM-yyyy");
#if UTC_MODE
       pri.InsertDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                pri.InsertDate = DateTime.Now.ToString("dd-MM-yyyy");
#endif
                FillPrimaryGrid(pri);
            }
            catch (Exception ex)
            {
                objmainform.objlog.WriteError("FillPrimaryData", "Error-- " + ex.ToString(), Channelname);
            }
        }
        private void FillPrimaryGrid(PrimaryPacket pri)
        {
            try
            {
                objmainform.dgvprievent.Invoke((MethodInvoker)(() =>
                {
                    if (objmainform.dgvprievent.Rows.Count >= 2)
                    {
                        objmainform.dgvprievent.Rows.RemoveAt(0);
                    }
                    objmainform.dgvprievent.Rows.Add(pri.Event, pri.Channelid, pri.EventId, pri.EventTime, pri.Duration, pri.InsertDate);
                    objmainform.dgvprievent.ClearSelection();
                    objmainform.objlog.WriteEvents("FillPrimaryGrid","Adding primary information- ("+ pri.Event + " " + pri.Channelid + " " + pri.EventId + " " + pri.EventTime + " " + pri.Duration + " " + pri.InsertDate+")" + Environment.NewLine, Channelname);
                }));
            }
            catch (Exception ex)
            {
                objmainform.objlog.WriteError("FillPrimaryGrid", "Error In Adding Primary Information- " + pri.Event + " " + pri.Channelid + " " + pri.EventId + " " + pri.EventTime + " " + pri.Duration + " " + pri.InsertDate + " Not Inserted " + Environment.NewLine + ex.ToString(), Channelname);
            }
        }
        private string HanDelMidNightCase(string CalCulateTime)
        {
            try
            {
                if ((objmainform.objlog.LTCTime.Substring(0, 2) == "23" || objmainform.objlog.LTCTime.Substring(0, 2) == "22") && (CalCulateTime.Substring(0, 5) == "00:00" || CalCulateTime.Substring(0, 5) == "00:01"))
                {
                    string[] _splt1 = CalCulateTime.Split(':');
                    TimeSpan ts1 = new TimeSpan(Convert.ToInt32(_splt1[0]), Convert.ToInt32(_splt1[1]), Convert.ToInt32(_splt1[2]), Convert.ToInt32(_splt1[3]), Convert.ToInt32(_splt1[4]) * 40);
                    ts1 = ts1.Add(new TimeSpan(1, 0, 0, 0));
                    return (ts1.Days.ToString("00") + ":" + ts1.Hours.ToString("00") + ":" + ts1.Minutes.ToString("00") + ":" + ts1.Seconds.ToString("00") + ":" + (ts1.Milliseconds / 40).ToString("00"));

                }
                return CalCulateTime;
            }
            catch (Exception)
            {
                return CalCulateTime;
            }
        }
        internal void FillSecondaryData(string[] Secondarydata)
        {
            try
            {

#if UTC_MODE
   string _currentdate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                string _currentdate = DateTime.Now.ToString("dd-MM-yyyy");
#endif
                SecondaryPacket sec = new SecondaryPacket();
                sec.Status = "W";
                sec.TemplateName = (Secondarydata[10] + "").ToUpper();
                sec.EventTime = (Secondarydata[6] + "").ToUpper();
                sec.Offset = (Secondarydata[8] + "").ToUpper();
                sec.Duration = (Secondarydata[9] + "").ToUpper();
                string OutOffset = "";
                string Secondarystarttime = AddTimes.Invoke(sec.EventTime, sec.Offset, "");
                if (Secondarystarttime.Split(':').Length != 5)
                {
                    objmainform.objlog.WriteEvents("FillSecondaryData", "Secondary Start Time is not in Correct Format " + " Event Time-> " + sec.EventTime + "Secondary Offset-> " + sec.Offset + " PACKET NOT ADDED ", Channelname);
                    objmainform.FillEventLog("Secondary Start Time is not in Correct Format " + " Event Time=> " + sec.EventTime + "Secondary Offset=> " + sec.Offset + " PACKET NOT ADDED ", "W");
                    return;
                }
                Secondarystarttime = HanDelMidNightCase(Secondarystarttime);
                DataRow dr = objmainform._dttemplateinfo.Select("TemplateName='" + sec.TemplateName + "'").FirstOrDefault();
                if (dr != null)
                {                   
                    Secondarystarttime = AddSubtractOffset(Secondarystarttime, dr["StartOffset"] + "");
                    OutOffset=  dr["EndOffset"]+"";
                }
                else
                {
                    objmainform.objlog.WriteEvents("FillSecondaryData", "Template Name='" + sec.TemplateName + "'does not exist", Channelname);
                    objmainform.FillEventLog("Template Name = '" + sec.TemplateName + "'does not exist", "W");
                    return;
                }


                string Starttime = Secondarystarttime.Split(':')[1] + ":" + Secondarystarttime.Split(':')[2] + ":" + Secondarystarttime.Split(':')[3] + ":" + Secondarystarttime.Split(':')[4];
                sec.StartTime = (Starttime + "").ToUpper();
                sec.StarttimewithDay = (Secondarystarttime + "").ToUpper();
                string Secondaryendtime = AddTimes.Invoke(sec.EventTime, sec.Offset, sec.Duration);
                if (Secondarystarttime.Split(':').Length != 5)
                {
                    objmainform.objlog.WriteEvents("FillSecondaryData", "Secondary End Time is not in Correct Format " + " Event Time-> " + sec.EventTime + " Secondary Offset-> " + sec.Offset + "Duration " + sec.Duration + " PACKET NOT ADDED ", Channelname);
                    objmainform.FillEventLog("Secondary End Time is not in Correct Format " + " Event Time=> " + sec.EventTime + "Secondary Offset=> " + sec.Offset + "Duration=> " + sec.Duration + " PACKET NOT ADDED ", "W");
                    return;
                }
                Secondaryendtime = HanDelMidNightCase(Secondaryendtime);
                Secondaryendtime = AddSubtractOffset(Secondaryendtime,OutOffset);
                string Endtime = Secondaryendtime.Split(':')[1] + ":" + Secondaryendtime.Split(':')[2] + ":" + Secondaryendtime.Split(':')[3] + ":" + Secondaryendtime.Split(':')[4];
                sec.EndTime = (Endtime + "").ToUpper();
                sec.EndtimewithDay = (Secondaryendtime + "").ToUpper();
                sec.CueOffset = (objmainform.CueOffSet + "").ToUpper();
                string SecondaryLoadTimewithDay = Subtimes.Invoke(sec.StarttimewithDay, sec.CueOffset);
                sec.SecondaryLoadTime = (SecondaryLoadTimewithDay + "").ToUpper();
                double AddStartDays = Convert.ToDouble(Secondarystarttime.Split(':')[0].ToString());
                double AddEndDays = Convert.ToDouble(Secondaryendtime.Split(':')[0].ToString());
                double AddLoadDays = Convert.ToDouble(SecondaryLoadTimewithDay.Split(':')[0].ToString());
#if UTC_MODE
    sec.StartDate = DateTime.UtcNow.AddDays(AddStartDays).ToString("dd-MM-yyyy");
                sec.EndDate = DateTime.UtcNow.AddDays(AddEndDays).ToString("dd-MM-yyyy");
                sec.LoadDate = DateTime.UtcNow.AddDays(AddLoadDays).ToString("dd-MM-yyyy");
#else
                sec.StartDate = DateTime.Now.AddDays(AddStartDays).ToString("dd-MM-yyyy");
                sec.EndDate = DateTime.Now.AddDays(AddEndDays).ToString("dd-MM-yyyy");
                sec.LoadDate = DateTime.Now.AddDays(AddLoadDays).ToString("dd-MM-yyyy");
#endif

                sec.TemplateValue = (Secondarydata[7] + "");
                sec.ReconcileKey = (Secondarydata[5] + "").ToUpper();
                sec.PrimaryId = (Secondarydata[3] + "").ToUpper();
                sec.PrimaryDuration = (Secondarydata[13] + "").ToUpper();
                sec.Channel = (Secondarydata[2] + "").ToUpper();
                sec.DeleteCheck = "N";
                sec.Hide = "N";
                //Handel Scte35 Case
                if(objmainform.ObjScte!=null)
                {
                    if(sec.TemplateName.Equals(objmainform.ScteTemplateName,StringComparison.InvariantCultureIgnoreCase))
                    {
                        objmainform.ObjScte.UpdateSctePacket(sec.PrimaryId,sec.StartTime,sec.Duration,sec.ReconcileKey);
                        return;
                    }
                }

                if(!File.Exists(objmainform.TemplatePath+"\\"+ sec.TemplateName+".ml-cgs"))
                {
                    objmainform.objlog.WriteEvents("FillSecondaryData", "Template file='" + sec.TemplateName + "'does not exist", Channelname);
                    objmainform.FillEventLog("Template file = '" + sec.TemplateName + "'does not exist", "W");
                    return;
                }
                #region Testing
                sec.BreakFreeBug = "N";
                if (objmainform.breakfreetemplatedt.Select("TemplateName='" + sec.TemplateName + "'").FirstOrDefault() != null)
                {
                    sec.BreakFreeBug = "Y";

#if UTC_MODE
     sec.EndDate = DateTime.UtcNow.AddDays(1).ToString("dd-MM-yyyy");
#else
                    sec.EndDate = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
#endif
                    sec.Duration = "24:00:00:00";
                    string breakbugfreetime = AddTimes.Invoke(sec.EventTime, sec.Offset, sec.Duration);

                    sec.EndtimewithDay = breakbugfreetime;
                    sec.EndTime = breakbugfreetime.Split(':')[1] + ":" + breakbugfreetime.Split(':')[2] + ":" + breakbugfreetime.Split(':')[3] + ":" + breakbugfreetime.Split(':')[4];
                    objmainform.objlog.WriteEvents("FillSecondaryData", $"Template Name=>{sec.TemplateName} is a Break Free graphics", Channelname);
                    objmainform.FillEventLog($"Template Name=>{sec.TemplateName} is a Break Free graphics", "W");
                }
                #endregion
                #region
                if (sec.BreakFreeBug != "Y")
                {
                    bool KillWithPrimary = Convert.ToBoolean(dr["KillWithPrimary"]);
                    string PrimaryEndTime = AddTimes.Invoke(sec.EventTime, sec.PrimaryDuration==""?"00:00:00:00": sec.PrimaryDuration, "");
                    if (PrimaryEndTime.Split(':').Length != 5)
                    {
                        objmainform.objlog.WriteEvents("FillSecondaryData", "Primary End Time is not in Correct Format " + " Event Time=> " + sec.EventTime + " Primary Duration=> " + sec.PrimaryDuration + " PACKET NOT ADDED ", Channelname);
                        objmainform.FillEventLog("Primary End Time is not in Correct Format " + " Event Time=> " + sec.EventTime + " Primary Duration=> " + sec.PrimaryDuration + " PACKET NOT ADDED ", "W");
                        return;
                    }
                    if (KillWithPrimary == true && CheckInBetween.Invoke(sec.StarttimewithDay, sec.EndtimewithDay, PrimaryEndTime)&& sec.PrimaryDuration != "")
                    {

#if UTC_MODE
    sec.EndDate = DateTime.UtcNow.AddDays(Convert.ToDouble(PrimaryEndTime.Split(':')[0])).ToString("dd-MM-yyyy");
#else
                        sec.EndDate = DateTime.Now.AddDays(Convert.ToDouble(PrimaryEndTime.Split(':')[0])).ToString("dd-MM-yyyy");
#endif
                        sec.EndTime = PrimaryEndTime.Split(':')[1] + ":" + PrimaryEndTime.Split(':')[2] + ":" + PrimaryEndTime.Split(':')[3] + ":" + PrimaryEndTime.Split(':')[4];
                        sec.EndtimewithDay = (PrimaryEndTime + "").ToUpper();
                        string SecondaryDurationwithday = Subtimes.Invoke(sec.EndtimewithDay, sec.StarttimewithDay);
                        sec.Duration = SecondaryDurationwithday.Split(':')[1] + ":" + SecondaryDurationwithday.Split(':')[2] + ":" + SecondaryDurationwithday.Split(':')[3] + ":" + SecondaryDurationwithday.Split(':')[4];
                        objmainform.objlog.WriteEvents("FillSecondaryData", "EndTime changed of this secondary id due to KillWithPrimary is equal to true", Channelname);
                    }
                    if ((_currentdate == sec.EndDate && objmainform.CheckDateTime.Invoke(sec.EndTime, sec.EndDate, objmainform.objlog.LTCTime)) || objmainform.CompareDate.Invoke(_currentdate, sec.EndDate))
                    {
                        objmainform.FillEventLog("EndTime is less than LTCTime", "W");
                        objmainform.objlog.WriteEvents("FillSecondaryData", "EndTime is less than LTCTime", Channelname);
                        return;
                    }
                }
                #endregion              
                FillSecondaryGrid(sec);
            }
            catch (Exception ex)
            {
                objmainform.FillEventLog("Error-- " + ex.ToString(), "E");
                objmainform.objlog.WriteError("FillSecondaryData", "Error-- " + ex.ToString(), Channelname);
            }
        }

        private string AddSubtractOffset(string MainTime ,string OffsetTime)
        {
            try
            {
                string[] temarr = MainTime.Split(':');
                TimeSpan ts1 = new TimeSpan(Convert.ToInt32(temarr[0]), Convert.ToInt32(temarr[1]), Convert.ToInt32(temarr[2]), Convert.ToInt32(temarr[3]), Convert.ToInt32(temarr[4])*40);
                int result = 0;
                int.TryParse(OffsetTime, out result);
                TimeSpan ts2 = new TimeSpan(0, 0, 0, 0, result * 40);
                return ((ts1 + ts2).Days.ToString("00") + ":" + (ts1 + ts2).Hours.ToString("00") + ":" + (ts1 + ts2).Minutes.ToString("00") + ":" + (ts1 + ts2).Seconds.ToString("00") + ":" + ((ts1 + ts2).Milliseconds / 40).ToString("00"));

            }
            catch
            {
                return MainTime;
            }
        }


        private void ReloadTemplateDetail()
        {   //To get data from channelbugmaster
            try
            {
                objmainform.Fetchtemplatedata();
                objmainform.CreateTemplate();
            }
            catch
            {              
                
            }
        }
        private void FillSecondaryGrid(SecondaryPacket sec)
        {
            try
            {
                if (objmainform.dgvsecevent.Rows.Count > 0)
                {
                    if (objmainform.ReconcileKeyCheck || objmainform.OverlappingCheck)
                    {
                        foreach (DataGridViewRow reconcilerow in objmainform.dgvsecevent.Rows)
                        {
                            string ReconcileKey = reconcilerow.Cells["SecRecKey"].Value + "";
                            string TemplateName = (reconcilerow.Cells["SecTemplate"].Value + "").Trim().ToUpper();
                            DataRow dr = objmainform.breakfreetemplatedt.Select("TemplateName ='" + TemplateName + "'").FirstOrDefault();
                            if (TemplateName == sec.TemplateName && dr != null)
                            {
                                string EndtimeWithDay = AddTimes(sec.EventTime, sec.Offset, "");
                                string Endtime = EndtimeWithDay.Split(':')[1] + ":" + EndtimeWithDay.Split(':')[2] + ":" + EndtimeWithDay.Split(':')[3] + ":" + EndtimeWithDay.Split(':')[4];

                                if (CheckInBetweenDateTime(reconcilerow.Cells["SecStartDate"].Value.ToString(), reconcilerow.Cells["SecStartTime"].Value.ToString(), reconcilerow.Cells["SecEndDate"].Value.ToString(), reconcilerow.Cells["SecEndTime"].Value.ToString(), sec.StartDate, sec.StartTime))
                                {
                                    if (reconcilerow.Cells["SecTemplateValue"].Value.ToString() == sec.TemplateValue)
                                    {
                                        reconcilerow.Cells["SecEndTime"].Value = sec.StartTime;
                                        reconcilerow.Cells["SecEndDate"].Value = sec.StartDate;
                                        return;
                                    }
                                    reconcilerow.Cells["SecEndTime"].Value = sec.StartTime;
                                    reconcilerow.Cells["SecEndDate"].Value = sec.StartDate;
                                }
                            }
                            if (dr == null)
                            {
                                #region Reconcile Check Case
                                if (objmainform.ReconcileKeyCheck)
                                {
                                    if (sec.ReconcileKey == ReconcileKey)
                                    {
                                        objmainform.objlog.WriteEvents("FillSecondaryGrid", $"Packet skipped as already same reconcile key exists, Template Name=>{TemplateName}, ReconcileKey=>{ReconcileKey} ", objmainform.ChannelName);
                                        objmainform.FillEventLog("Packet skipped as already same reconcile key exists", "W");
                                        return;
                                    }
                                }
                                #endregion
                                #region OverLapping Check Case
                                string StartTime = reconcilerow.Cells["SecStartTime"].Value + "";
                                string StartDate = reconcilerow.Cells["SecStartDate"].Value + "";
                                string EndTime = reconcilerow.Cells["SecEndTime"].Value + "";
                                string EndDate = reconcilerow.Cells["SecEndDate"].Value + "";
                                string DelFlg = reconcilerow.Cells["SecDeleteCheck"].Value + ""; 

                                if (objmainform.OverlappingCheck)
                                {
                                    if (string.Compare(sec.TemplateName.Trim(), TemplateName, true) == 0 && DelFlg !="Y"&& (CheckInBetweenDateTime.Invoke(StartDate, StartTime, EndDate, EndTime, sec.StartDate, sec.StartTime) || CheckInBetweenDateTime.Invoke(StartDate, StartTime, EndDate, EndTime, sec.EndDate, sec.EndTime) || CheckInBetweenDateTime.Invoke(sec.StartDate, sec.StartTime, sec.EndDate, sec.EndTime, StartDate, StartTime)))
                                    {
                                        objmainform.objlog.WriteEvents("FillSecondaryGrid", $"Packet skipped due to overlapping, Template Name=>{TemplateName},StartDate of Previous Template=>{StartDate}, StartTime of Previous Template=>{StartTime} ,EndDate of Previous Template=>{EndDate}, EndTime of Previous Template=>{EndTime},StartDate of Current Template=>{sec.StartDate}, StartTime of Current Template=>{sec.StartTime},EndDate of Current Template=>{sec.EndDate} ,EndTime of Current Template=>{sec.EndTime}", objmainform.ChannelName);
                                        objmainform.FillEventLog("Packet skipped due to overlapping", "W");
                                        return;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                lock (objmainform.m_objGridLock)
                {
                    objmainform.dgvsecevent.Invoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            objmainform.dgvsecevent.Rows.Add
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
                            objmainform.dgvsecevent.ClearSelection();
                            objmainform.objlog.WriteEvents("FillSecondaryGrid", $"Adding Template Info(templatename={sec.TemplateName},eventtime={sec.EventTime},offset={sec.Offset},duration={sec.Duration},startdatetime={sec.StartDate} {sec.StartTime} ,enddatetime={sec.EndDate} {sec.EndTime}, templatevalue={ sec.TemplateValue}){Environment.NewLine}", Channelname);
                        }
                        catch (Exception ex)
                        {
                            objmainform.objlog.WriteError("FillSecondaryGrid objmainform.dgvsecevent.Invoke", "error-- " + ex.ToString(), Channelname);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                objmainform.objlog.WriteError("FillSecondaryGrid", $"Error In Adding Template Info-(templatename={sec.TemplateName},eventtime={sec.EventTime},offset={sec.Offset},duration={sec.Duration},startdatetime={sec.StartDate} {sec.StartTime} ,enddatetime={sec.EndDate} {sec.EndTime}, templatevalue={ sec.TemplateValue}) {Environment.NewLine}", Channelname);
            }
        }
     
        public void Dispose()
        {
            objmainform = null;
            SkipPrefix.Clear();
        }

        #region Anonymous Functions
        internal Func<string, string, string, string> AddTimes = (x, y, z) =>
        {
            string[] _splt1 = x.Split(':');
            string[] _splt2 = y.Split(':');
            if (_splt1.Length != 4 || _splt2.Length != 4) { return ""; }
            TimeSpan ts1 = new TimeSpan(0, Convert.ToInt32(_splt1[0]), Convert.ToInt32(_splt1[1]), Convert.ToInt32(_splt1[2]), Convert.ToInt32(_splt1[3]) * 40);
            TimeSpan ts2 = new TimeSpan(0, Convert.ToInt32(_splt2[0]), Convert.ToInt32(_splt2[1]), Convert.ToInt32(_splt2[2]), Convert.ToInt32(_splt2[3]) * 40);
            if (z.Length >= 1)
            {
                string[] _splt3 = z.Split(':');
                TimeSpan ts3 = new TimeSpan(0, Convert.ToInt32(_splt3[0]), Convert.ToInt32(_splt3[1]), Convert.ToInt32(_splt3[2]), Convert.ToInt32(_splt3[3]));
                return ((ts1 + ts2 + ts3).Days.ToString("00") + ":" + (ts1 + ts2 + ts3).Hours.ToString("00") + ":" + (ts1 + ts2 + ts3).Minutes.ToString("00") + ":" + (ts1 + ts2 + ts3).Seconds.ToString("00") + ":" + ((ts1 + ts2 + ts3).Milliseconds / 40).ToString("00"));
            }
            return ((ts1 + ts2).Days.ToString("00") + ":" + (ts1 + ts2).Hours.ToString("00") + ":" + (ts1 + ts2).Minutes.ToString("00") + ":" + (ts1 + ts2).Seconds.ToString("00") + ":" + ((ts1 + ts2).Milliseconds / 40).ToString("00"));
        };
        private Func<string, string, string> Subtimes = (x, y) =>
        {
            string[] _splt1 = x.Split(':');
            if (_splt1.Length != 5) { return ""; }
            string[] _splt2 = y.Split(':');
            TimeSpan ts1 = new TimeSpan(Convert.ToInt32(_splt1[0]), Convert.ToInt32(_splt1[1]), Convert.ToInt32(_splt1[2]), Convert.ToInt32(_splt1[3]), Convert.ToInt32(_splt1[4]) * 40);
            if (_splt2.Length == 1)
            {
                TimeSpan ts3 = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(_splt2[0]) * 40);
                return ((ts1 - ts3).Days.ToString("00") + ":" + (ts1 - ts3).Hours.ToString("00") + ":" + (ts1 - ts3).Minutes.ToString("00") + ":" + (ts1 - ts3).Seconds.ToString("00") + ":" + (((ts1 - ts3).Milliseconds) / 40).ToString("00"));
            }
            TimeSpan ts2 = new TimeSpan(Convert.ToInt32(_splt2[0]), Convert.ToInt32(_splt2[1]), Convert.ToInt32(_splt2[2]), Convert.ToInt32(_splt2[3]), Convert.ToInt32(_splt2[4]) * 40);
            return ((ts1 - ts2).Days.ToString("00") + ":" + (ts1 - ts2).Hours.ToString("00") + ":" + (ts1 - ts2).Minutes.ToString("00") + ":" + (ts1 - ts2).Seconds.ToString("00") + ":" + (((ts1 - ts2).Milliseconds) / 40).ToString("00"));
        };
        //Checks whether Z is in between X or Y
        internal Func<string, string, string, bool> CheckInBetween = (x, y, z) =>
        {
            try
            {
                string[] _starttime = x.Split(':');
                if (_starttime.Length != 5) return false;
                TimeSpan Starttime = new TimeSpan(Convert.ToInt32(_starttime[0]), Convert.ToInt32(_starttime[1]), Convert.ToInt32(_starttime[2]), Convert.ToInt32(_starttime[3]), Convert.ToInt32(_starttime[4]));
                string[] _endtime = y.Split(':');
                if (_endtime.Length != 5) return false;
                TimeSpan Endtime = new TimeSpan(Convert.ToInt32(_endtime[0]), Convert.ToInt32(_endtime[1]), Convert.ToInt32(_endtime[2]), Convert.ToInt32(_endtime[3]), Convert.ToInt32(_endtime[4]));
                string[] Second_Time = z.Split(':');
                if (Second_Time.Length != 5) return false;
                TimeSpan _Second_Time = new TimeSpan(Convert.ToInt32(Second_Time[0]), Convert.ToInt32(Second_Time[1]), Convert.ToInt32(Second_Time[2]), Convert.ToInt32(Second_Time[3]), Convert.ToInt32(Second_Time[4]));

                if (Starttime.TotalMilliseconds <= _Second_Time.TotalMilliseconds && _Second_Time.TotalMilliseconds < Endtime.TotalMilliseconds)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        };
        //Checks whether Z is in between X or Y
        private Func<string, string, string, string, string, string, bool> CheckInBetweenDateTime = (x0, x1, y0, y1, z0, z1) =>
         {
             try
             {
                 DateTime StartDatetime = DateTime.ParseExact(x0, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                 string[] _starttime = x1.Split(':');
                 if (_starttime.Length != 4) return false;
                 StartDatetime = StartDatetime.AddHours(Convert.ToInt32(_starttime[0])).AddMinutes(Convert.ToInt32(_starttime[1])).AddSeconds(Convert.ToInt32(_starttime[2])).AddMilliseconds(Convert.ToInt32(_starttime[3]) * 40);

                 DateTime EndDatetime = DateTime.ParseExact(y0, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                 string[] _endtime = y1.Split(':');
                 if (_endtime.Length != 4) return false;
                 EndDatetime = EndDatetime.AddHours(Convert.ToInt32(_endtime[0])).AddMinutes(Convert.ToInt32(_endtime[1])).AddSeconds(Convert.ToInt32(_endtime[2])).AddMilliseconds(Convert.ToInt32(_endtime[3]) * 40);

                 DateTime _Second_Time = DateTime.ParseExact(z0, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                 string[] Second_Time = z1.Split(':');
                 if (Second_Time.Length != 4) return false;
                 _Second_Time = _Second_Time.AddHours(Convert.ToInt32(Second_Time[0])).AddMinutes(Convert.ToInt32(Second_Time[1])).AddSeconds(Convert.ToInt32(Second_Time[2])).AddMilliseconds(Convert.ToInt32(Second_Time[3]) * 40);

                 if (StartDatetime <= _Second_Time && _Second_Time < EndDatetime)
                 {
                     return true;
                 }
                 return false;
             }
             catch (Exception)
             {
                 return false;
             }
         };
        #endregion
    }
    #region PacketData
    public struct PrimaryPacket
    {
        public string Event;
        public string Channelid;
        public string EventId;
        public string EventTime;
        public string Duration;
        public string InsertDate;
    }
    public struct SecondaryPacket
    {
        public string Status;
        public string ReconcileKey;
        public string EventTime;
        public string TemplateName;
        public string Offset;
        public string Duration;
        public string TemplateValue;
        public string StartTime;
        public string EndTime;
        public string StartDate;
        public string Channel;
        public string PrimaryId;
        public string EntryDateTime;
        public string EndDate;
        public string StartType;
        public string EntryType;
        public string Hide;
        public string PrimaryDuration;
        public string Layer;
        public string EndOffset;
        public string FileExt;
        public string BugInfoFields;
        public string FilePath;
        public string TypeOfBug;
        public bool SendKillCommand;
        public string DeleteCheck;
        public string StarttimewithDay;
        public string EndtimewithDay;
        public string CueOffset;
        public string SecondaryLoadTime;
        public string LoadDate;
        public string BreakFreeBug;//Testing
    }
    #endregion
}
