using graphicserver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphicserver.Classes
{
    class ManualAsRun
    {
        DataTable Dt_ManualAsRun = null;
        String TemplateName = "";
        Loging ObjLog = null;
        string AsRunFolderPath = "";
        string ChannelName = "";
        bool ManualAsRunEnable = false;
        String TakeTime = "";
        public ManualAsRun(string TemplateName, Loging ObjLog, string AsRunFolderPath, string ChannelName,String TakeTime, bool ManualAsRunEnable = false)
        {
            this.TemplateName = TemplateName;
            this.AsRunFolderPath = AsRunFolderPath;
            this.ChannelName = ChannelName;
            this.ObjLog = ObjLog;
            this.ManualAsRunEnable = ManualAsRunEnable;
            this.TakeTime = TakeTime;
            if (ManualAsRunEnable == false)
            {
                return;
            }
            CreateStructure();
        }
        private void CreateStructure()
        {
            if (Dt_ManualAsRun == null)
            {
                Dt_ManualAsRun = new DataTable("MANUAL_ASRUN");
                Dt_ManualAsRun.Columns.Add("AsRunDateTime");
                Dt_ManualAsRun.Columns.Add("AsRunTemplateName");
                Dt_ManualAsRun.Columns.Add("AsRunFilename");
                Dt_ManualAsRun.Columns.Add("AsRunTemplateFileName");
                Dt_ManualAsRun.Columns.Add("AsRunStatus");
                Dt_ManualAsRun.Columns.Add("AsRunDuration");
                Dt_ManualAsRun.Columns.Add("AsRunReconcileKey");
                Dt_ManualAsRun.Columns.Add("StartDateTime");
                Dt_ManualAsRun.Columns.Add("EndDateTime");
            }
        }


        internal void PrepareAsRunDetail(string _OprationName, string ParaMeterValue = "", string BugType = "", bool FileStatus = false)
        {
            if(ManualAsRunEnable==false)
            {
                return;
            }

            switch (_OprationName.ToUpper())
            {
                case "LOAD":
                    {
                        if (Dt_ManualAsRun.AsEnumerable().Any(e => e.Field<string>("AsRunTemplateName").ToLower() == TemplateName.ToLower()))
                        {
                            return;
                        }
                        DataRow Dr = Dt_ManualAsRun.NewRow();
                        Dr["AsRunDateTime"] = GetAsRunDateTime();// DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");// "27-06-2018|10:30:03";
                        Dr["AsRunTemplateName"] = TemplateName;
                        Dr["AsRunFilename"] = "";
                        Dr["AsRunTemplateFileName"] = TemplateName;
                        Dr["AsRunStatus"] = "done";
                        Dr["AsRunReconcileKey"] = "";

#if UTC_MODE
                 string StartDateTime = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                        string StartDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                        Dr["StartDateTime"] = StartDateTime;
                        Dr["EndDateTime"] = StartDateTime;
                        Dt_ManualAsRun.Rows.Add(Dr);
                        break;
                    }
                case "STOPKILL":
                    {
                        DataRow Dr = Dt_ManualAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {


#if UTC_MODE
                Dr["EndDateTime"] = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff");
#else
                            Dr["EndDateTime"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff");
#endif
                            WriteAsRunFile(Dr);
                            Dt_ManualAsRun.Rows.Remove(Dr);
                        }
                        break;
                    }
                case "UPDATE_PARAMETER_VALUE":
                    {
                        DataRow Dr = Dt_ManualAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunFilename"] = (Dr["AsRunFilename"] + "") == "" ? ParaMeterValue : ((Dr["AsRunFilename"] + "") + "~" + ParaMeterValue);
                            if (BugType.ToUpper() == "VIDEO" || BugType.ToUpper() == "IMAGE" || BugType.ToUpper() == "TICKER-FILE")
                            {
                                string previousState = Dr["AsRunStatus"] + "";
                                if (previousState.ToLower() == "done")
                                {
                                    Dr["AsRunStatus"] = FileStatus == false ? "fail" : "done";
                                }
                            }
                        }

                        break;
                    }
                case "UPDATE_FAIL_STATUS":
                    {
                        DataRow Dr = Dt_ManualAsRun.Select($"AsRunTemplateName='{TemplateName.Trim()}'").FirstOrDefault();
                        if (Dr != null)
                        {
                            Dr["AsRunStatus"] = "fail";
                            WriteAsRunFile(Dr);
                            Dt_ManualAsRun.Rows.Remove(Dr);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }

        private string GetAsRunDateTime()
        {
            try
            {
                if (TakeTime.ToUpper() == "LTC")
                {
                    string _lTime = ObjLog.LTCTime.Substring(0, 8);
#if UTC_MODE
                         if (_lTime.Trim() == "00:00:00")
                    {
                        return DateTime.UtcNow.ToString("dd-MM-yyyy|HH:mm:ss");
                    }
                    else
                    {
                        string formate = $"dd-MM-yyyy|{_lTime}";
                        return DateTime.UtcNow.ToString(formate);
                    }
#else
                    if (_lTime.Trim() == "00:00:00")
                    {
                        return DateTime.Now.ToString("dd-MM-yyyy|HH:mm:ss");
                    }
                    else
                    {
                        string formate = $"dd-MM-yyyy|{_lTime}";
                        return DateTime.Now.ToString(formate);
                    }
#endif

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
        private void WriteAsRunFile(DataRow DrAsrun)
        {
            FileStream fs = null;
            StreamWriter objWriter = null;
            string Message = "";
            string Query = "";
            string Datetime = "";

#if UTC_MODE
       string FileName = $@"{AsRunFolderPath}\ManualAsRun_{DateTime.UtcNow.ToString("dd-MM-yyyy")}.txt";
#else
            string FileName = $@"{AsRunFolderPath}\ManualAsRun_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
#endif
            try
            {
                if (Directory.Exists(AsRunFolderPath) == false)
                {
                    Directory.CreateDirectory(AsRunFolderPath);
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
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("WriteAsRunFile", $"Error In Write AsRun File ...    error-- " + ex.ToString(), ChannelName);
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
    }
}
