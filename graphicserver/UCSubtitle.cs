using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace graphicserver
{
    public partial class UCSubtitle : UserControl
    {
        FrmMainServer ObjMain = null;
        public UdpClient ReveiSubtitleSocket = null;
        private String LtcTime { get; set; } = "00:00:00:00";
        private String VITCTime { get; set; } = "00:00:00:00";
        private String StartTime { get; set; } = "00:00:00:00";
        private String EndTime { get; set; } = "00:00:00:00";
        private String Text { get; set; }
        private String Status { get; set; }
        private String ElementId { get; set; }
        private String ElementType { get; set; }
        private String PrepareFileName { get; set; }
        private String RunningFileName { get; set; }
        private String FileType { get; set; }

        bool bFileCheckFlag = false;
        string StartFileTime = "00:00:00:00";
        public bool bplay = false;
        string strSubtitleStartTime = "";
        bool IsSubtitleLoaded = false;

        public UCSubtitle()
        {
            InitializeComponent();
        }

        public UCSubtitle(FrmMainServer ObjMain)
        {
            InitializeComponent();
            this.ObjMain = ObjMain;

        }
        private void UCSubtitle_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeSubtitle();
                if (ObjMain.SubtitleMode.ToUpper() == "AUTOMATION")
                {
                    BtnPlay.Visible = false;
                    BtnLoad.Visible = false;
                    BtnStop.Visible = false;
                    txtStartTime.ReadOnly = true;
                }

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("UCSubtitle_Load", "Error In Loading Subtitle Control =" + ex.ToString(), ObjMain.ChannelName);
            }
        }
        public bool InitializeSubtitle()
        {
            try
            {
                ReplaceSubtitleWithbckupfile("", "");
                Thread ObjThread = new Thread(ReceiveSubtitlePacket);
                ObjThread.IsBackground = true;
                ObjThread.Start();
                 Task.Run(() => StartAutomation());
               // Thread ObjSubtitle = new Thread(StartAutomation);
               // ObjSubtitle.IsBackground = true;
               // ObjSubtitle.Start();
                GetSubTEmplateDetail();
                return true;

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("InitializeSubtitle", "Error In Initialize subtitle Thread and Receiving Subtitle Packet =" + ex.ToString(), ObjMain.ChannelName);
                return false;
            }

        }
        bool ReplaceSubtitleWithbckupfile(String _srcfile, String _dstfile)
        {
            try
            {
                String backupfile = ObjMain.TemplatePath + "\\subtitlebkp.ml-cgs";
                String orignalfile = ObjMain.TemplatePath + "\\subtitle.ml-cgs";
                if (File.Exists(backupfile))
                {
                    FileInfo fi = new FileInfo(orignalfile);
                    if (fi.Length <= 0)
                    {
                        File.Copy(backupfile, orignalfile, true);
                        ObjMain.objlog.WriteEvents("ReplaceSubtitleWithbckupfile", "subtitle template is empty,changing template with actual", ObjMain.ChannelName);
                        return true;
                    }
                    else
                    {
                        ObjMain.objlog.WriteEvents("ReplaceSubtitleWithbckupfile", "subtitle template is not empty,not changing template", ObjMain.ChannelName);
                        return false;
                    }
                }
                else
                {
                    ObjMain.objlog.WriteEvents("ReplaceSubtitleWithbckupfile", "subtitle template's backup is not available,not changing template", ObjMain.ChannelName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ReplaceSubtitleWithbckupfile", "Error In replace blank Subtitle template to orignal =" + ex.ToString(), ObjMain.ChannelName);
                return false;
            }
        }
        private void GetSubTEmplateDetail()
        {
            try
            {
                DataRow DrSubtitle = ObjMain._dttemplateinfo.Select("TemplateName='" + ObjMain.SubtitleTemplate + "'").FirstOrDefault();
                String data = DrSubtitle["param1"] + "";
                if (data.Split('~').Length >= 2)
                {
                    ElementId = data.Split('~')[0];
                    ElementType = data.Split('~')[1].ToUpper();
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("GetSubTEmplateDetail", "Error In getting  Subtitle Template setting" + ex.ToString(),ObjMain.ChannelName);
            }
        }
        public void ReceiveSubtitlePacket()
        {
            try
            {
                ReveiSubtitleSocket = new UdpClient(ObjMain.SubtitlePort);
                ReveiSubtitleSocket.BeginReceive(new AsyncCallback(SubtitleInformation), null);

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ReceiveLtcPacket", $"Error In Bind Subtitle  Packet Port {ObjMain.SubtitlePort} " + ex.ToString(), ObjMain.ChannelName);

            }
        }

        private void SubtitleInformation(IAsyncResult res)
        {
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, ObjMain.SubtitlePort);
                byte[] received = ReveiSubtitleSocket.EndReceive(res, ref RemoteIpEndPoint);
                string strSubtitleData = System.Text.Encoding.ASCII.GetString(received);
                ObjMain.objlog.WriteEvents("Received subtitle packet------>>>>>>", strSubtitleData, ObjMain.ChannelName);
                HandleSubtitlePacket(strSubtitleData);

            }
            catch (ObjectDisposedException ex)
            {

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("SubtitleInformation", "Error in Subtitle received data : " + ex.ToString(), ObjMain.ChannelName);
            }
            finally
            {
                try
                {
                    ReveiSubtitleSocket.BeginReceive(new AsyncCallback(SubtitleInformation), null);
                }
                catch
                { }
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog Objdlg = new OpenFileDialog();
                Objdlg.Filter = "Srt File(*.srt)|*.srt|All file(*.*)|*.*|CIP File(*.cip)|*.cip";
                Objdlg.FilterIndex = 2;
                Objdlg.RestoreDirectory = true;
                Objdlg.Multiselect = false;
                if (DialogResult.OK == Objdlg.ShowDialog())
                {
                    PrepareFileName = Objdlg.FileName;
                    lblPrepareId.Text = Path.GetFileNameWithoutExtension(Objdlg.FileName);
                    lblPrepareFileStatus.Text = "Available";
                    lblPrepareFileStatus.ForeColor = Color.Green;
                    lblPrepareFileType.Text = Path.GetExtension(Objdlg.FileName).ToUpper();
                    bFileCheckFlag = true;
                }
                else
                {
                    bFileCheckFlag = false;
                    lblPrepareId.Text = "Not Selected";
                    lblPrepareFileStatus.Text = "Not Selected";
                    lblPrepareFileStatus.ForeColor = Color.Red;
                    lblPrepareFileType.Text = "NA";
                }
                ObjMain.objlog.WriteEvents("BtnLoad_Click", $"Manual Loading by User, Selected file name { lblPrepareId.Text} and Availabilty of File {lblPrepareFileStatus.Text} and filetype={lblPrepareFileType.Text }", ObjMain.ChannelName);

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("BtnLoad_Click", "Error In Loading Subtitle File .." + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (bFileCheckFlag == true)
                {
                    if ((PrepareFileName + "").ToUpper() != (RunningFileName + "").ToUpper())
                    {
                        RunningFileName = PrepareFileName;
                    }
                    StartFileTime = LtcTime;
                    lblRunningId.Text = Path.GetFileNameWithoutExtension(RunningFileName);
                    lblRunningFileStatus.Text = "Running";
                    lblRunningFileStatus.ForeColor = Color.Green;
                    this.FileType = lblRunningFiletype.Text = (Path.GetExtension(RunningFileName) + "").ToUpper();
                    bplay = true;
                    ObjMain.objlog.WriteEvents("BtnPlay_Click", $"Manual Play by User, Bplay flag {bplay} with timecode ={txtStartTime.Text}", ObjMain.ChannelName);
                }
                else
                {
                    bplay = false;
                    ObjMain.objlog.WriteEvents("BtnPlay_Click", $"Manual Play by User without file selected, Bplay flag {bplay} with timecode ={txtStartTime.Text}", ObjMain.ChannelName);
                }
                ObjMain.objlog.WriteEvents("BtnPlay_Click", $"Manual Playing by User, running file name { lblRunningId.Text}, and starttime {StartFileTime} and bPlay Flag {bplay} for playing file. ", ObjMain.ChannelName);
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("BtnPlay_Click", "Error In play Subtitle File .." + ex.ToString(),ObjMain.ChannelName);
            }

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                bplay = false;
                Thread.Sleep(50);
                ObjMain.KillSubtitle(ObjMain.SubtitleTemplate);
                strSubtitleStartTime = "";
                EndTime = "00:00:00:00";
                IsSubtitleLoaded = false;
                lblRunningFileStatus.Text = "Stop";
                lblRunningFileStatus.ForeColor = Color.Red;
                ObjMain.objlog.WriteEvents("BtnStop_Click", $"Manual Stop by User, Bplay flag {bplay}, and remove display subtitle.... ", ObjMain.ChannelName);
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("BtnStop_Click", "Error In stop Subtitle File .." + ex.ToString(),ObjMain.ChannelName);
            }

        }

        private void StartAutomation()
        {

            while (true)
            {
                try
                {
                    Thread.Sleep(35);
                    DoWorkingSrtFile();
                }
                catch (Exception ex)
                {
                    ObjMain.objlog.WriteError("StartAutomation", "Error In calling subtitle starttime and endtime checking method...  " + ex.ToString(),ObjMain.ChannelName);
                }
            }
        }

        private bool DoWorkingSrtFile()
        {
            try
            {
                string eventtime = "";
                LtcTime = ObjMain.objlog.LTCTime;               
                eventtime = strSubtitleStartTime == "" ? txtStartTime.Text : strSubtitleStartTime;
                if (bplay == true)
                {
                    //VITCTime = lblVitcTime.Text = GetTimeFromFrame(GetFrameFromTime(eventtime) + (GetFrameFromTime(LtcTime) - GetFrameFromTime(StartFileTime)));
                    if (GetFrameFromTime(LtcTime) >= GetFrameFromTime(StartFileTime))
                    {
                        VITCTime = lblVitcTime.Text = GetTimeFromFrame(GetFrameFromTime(eventtime) + (GetFrameFromTime(LtcTime) - GetFrameFromTime(StartFileTime)));
                    }
                    else
                    {
                        VITCTime = lblVitcTime.Text = GetTimeFromFrame(GetFrameFromTime(eventtime) + (86400 * 25 - GetFrameFromTime(StartFileTime)) + GetFrameFromTime(LtcTime));
                        ObjMain.objlog.WriteEvents("DoWorkingSrtFile", $"lTC is lower then startof file", ObjMain.ChannelName);
                    }
                  //  VITCTime = ObjMain.objlog.mFrameTC;
                    ReadSubTitleFile();
                }
                return true;
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("DoWorkingSrtFile", "Error In Checking srt File .." + ex.ToString(),ObjMain.ChannelName);
                return false;
            }
        }


        //private void ReadSrtProcessWithPos()
        //{
        //    try
        //    {
        //        using (FileStream fs = new FileStream(RunningFileName, FileMode.Open, FileAccess.Read,FileShare.ReadWrite))
        //        {

        //            using (StreamReader file = new StreamReader(fs, Encoding.UTF8))
        //            {
        //                string singleline, secondline, textline1 = "", textline2 = "", textline3 = "";
        //                string posx = string.Empty; string posy = string.Empty;

        //                while ((singleline = file.ReadLine()) != null)
        //                {
        //                    if (singleline.Trim() == "") { continue; }
        //                    if (GetSrno(singleline) > 0)
        //                    {
        //                        string _StartTime = "", _EndTime = "";
        //                        secondline = file.ReadLine();
        //                        if (secondline.Trim() == "")
        //                        { 
        //                            secondline = file.ReadLine();
        //                        }
        //                        bool defaulttxt = true;
        //                        String[] textcodearr = secondline.Split(new string[] { "-->" }, StringSplitOptions.None);
        //                        textcodearr[1] = textcodearr[1].TrimStart();

        //                        String[] Posarr = textcodearr[1].Split(new string[] { " " }, StringSplitOptions.None);
        //                        if (Posarr.Length > 1)
        //                        {
        //                            if (Posarr.Length > 2 && Posarr.Length < 4)
        //                            {
        //                                secondline = textcodearr[0] + "-->" + Posarr[0];
        //                                posx = Posarr[1].Trim().Split(':')[1];
        //                                posy = Posarr[2].Trim().Split(':')[1];
        //                                defaulttxt = false;
        //                            }
        //                            else if (Posarr.Length > 4)
        //                            {
        //                                secondline = textcodearr[0] + "-->" + Posarr[0];
        //                                posx = Posarr[1].Trim().Split(':')[1];
        //                                posy = Posarr[3].Trim().Split(':')[1];
        //                                defaulttxt = false;
        //                            }
        //                            GetTime(ref secondline, out _StartTime, out _EndTime);
        //                        }
        //                        else
        //                        {
        //                            GetTime(ref secondline, out _StartTime, out _EndTime);
        //                        }
        //                        if (ValidateTime(_StartTime) && ValidateTime(_EndTime))
        //                        {
        //                            _StartTime = AddOffset(_StartTime, ObjMain.SubtitleStartOffset);
        //                            _EndTime = AddOffset(_EndTime, ObjMain.SubtitleEndOffset);
        //                            textline1 = file.ReadLine() + "";
        //                            if(textline1=="")
        //                                textline1 = file.ReadLine() + "";
        //                            textline2 = file.ReadLine() + "";
        //                            if (textline2 == "")
        //                                textline2 = file.ReadLine() + "";
        //                            if (textline2.Trim() != "")
        //                            {
        //                                textline1 = textline1 + "<br/>" + textline2;
        //                                textline3 = file.ReadLine() + "";
        //                                if (textline3 != "")
        //                                {
        //                                    textline1 = textline1 + "<br/>" + textline3;
        //                                }
        //                            }
        //                        }
        //                        if (CheckInBetween.Invoke(_StartTime, _EndTime, VITCTime, IsSubtitleLoaded) == true)
        //                        {
        //                            if (!defaulttxt)
        //                                UpdateSrtSubTitlewithposText(textline1, posx, posy);
        //                            else
        //                                UpdateSrtSubTitleText(textline1);
        //                            LoadSubtitleFile(ref _StartTime, ref _EndTime);

        //                        }
        //                        if (CheckIsGraterEndTime.Invoke(EndTime, VITCTime, IsSubtitleLoaded) == true)
        //                        {
        //                            KillSubtitleFile(ref _StartTime, ref _EndTime);
        //                        }
        //                    }
        //                }

        //                file.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjMain.objlog.WriteError("ReadSrtProcess", "Error in Reading Srt file" + ex.ToString(), ObjMain.ChannelName);
        //    }
        //}

        private void ReadSrtProcessWithPos()
        {
            try
            {
                    using (StreamReader file = new StreamReader(RunningFileName, Encoding.UTF8))
                    {
                        string singleline, secondline, textline1 = "", textline2 = "", textline3 = "";
                        string posx = string.Empty; string posy = string.Empty;

                        while ((singleline = file.ReadLine()) != null)
                        {
                            if (singleline.Trim() == "") { continue; }
                            if (GetSrno(singleline) > 0)
                            {
                                string _StartTime = "", _EndTime = "";
                                secondline = file.ReadLine();
                                bool defaulttxt = true;
                                String[] textcodearr = secondline.Split(new string[] { "-->" }, StringSplitOptions.None);
                                textcodearr[1] = textcodearr[1].TrimStart();

                                String[] Posarr = textcodearr[1].Split(new string[] { " " }, StringSplitOptions.None);
                                if (Posarr.Length > 1)
                                {
                                    if (Posarr.Length > 2 && Posarr.Length < 4)
                                    {
                                        secondline = textcodearr[0] + "-->" + Posarr[0];
                                        posx = Posarr[1].Trim().Split(':')[1];
                                        posy = Posarr[2].Trim().Split(':')[1];
                                        defaulttxt = false;
                                    }
                                    else if (Posarr.Length > 4)
                                    {
                                        secondline = textcodearr[0] + "-->" + Posarr[0];
                                        posx = Posarr[1].Trim().Split(':')[1];
                                        posy = Posarr[3].Trim().Split(':')[1];
                                        defaulttxt = false;
                                    }
                                    GetTime(ref secondline, out _StartTime, out _EndTime);
                                }
                                else
                                {
                                    GetTime(ref secondline, out _StartTime, out _EndTime);
                                }
                                if (ValidateTime(_StartTime) && ValidateTime(_EndTime))
                                {
                                    _StartTime = AddOffset(_StartTime, ObjMain.SubtitleStartOffset);
                                    _EndTime = AddOffset(_EndTime, ObjMain.SubtitleEndOffset);
                                    textline1 = file.ReadLine() + "";
                                    textline2 = file.ReadLine() + "";
                                    if (textline2.Trim() != "")
                                    {
                                    textline1 = textline1 + " <br/> " + textline2;
                                    textline3 = file.ReadLine() + "";
                                    if (textline3 != "")
                                    {
                                        textline1 = textline1 + " <br/> " + textline3;
                                    }
                                    }
                                }
                                if (CheckInBetween.Invoke(_StartTime, _EndTime, VITCTime, IsSubtitleLoaded) == true)
                                {
                                    if (!defaulttxt)
                                        UpdateSrtSubTitlewithposText(textline1, posx, posy);
                                    else
                                        UpdateSrtSubTitleText(textline1);
                                    LoadSubtitleFile(ref _StartTime, ref _EndTime);
                              //  ObjMain.objlog.WriteEvents("ReadSrtProcessWithPos","VideoTimecode= "+ VITCTime+"starttime= "+_StartTime+"endtime= "+_EndTime +"Systime="+ DateTime.Now.ToString("HH:mm:ss.fff")+"Offset Diff LTC="+ GetTimeFromFrame((GetFrameFromTime(LtcTime) - GetFrameFromTime(StartFileTime))), ObjMain.ChannelName);
                             //   ObjMain.objlog.WriteEvents("")
                                }
                                if (CheckIsGraterEndTime.Invoke(EndTime, VITCTime, IsSubtitleLoaded) == true)
                                {
                                    KillSubtitleFile(ref _StartTime, ref _EndTime);
                                }
                            }
                        }

                        file.Close();
                    }
              
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ReadSrtProcess", "Error in Reading Srt file" + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void ReadSrtProcess()
        {
            try
            {
                using (StreamReader file = new StreamReader(RunningFileName, Encoding.UTF8))
                {
                    string singleline, secondline, textline1 = "", textline2 = "", textline3 = "";
                    while ((singleline = file.ReadLine()) != null)
                    {
                        if (singleline.Trim() == "") { continue; }
                        if (GetSrno(singleline) > 0)
                        {
                            string _StartTime = "", _EndTime = "";
                            secondline = file.ReadLine();

                            GetTime(ref secondline, out _StartTime, out _EndTime);
                            if (ValidateTime(_StartTime) && ValidateTime(_EndTime))
                            {
                                _StartTime = AddOffset(_StartTime, ObjMain.SubtitleStartOffset);
                                _EndTime = AddOffset(_EndTime, ObjMain.SubtitleEndOffset);
                                textline1 = file.ReadLine() + "";
                                textline2 = file.ReadLine() + "";
                                if (textline2.Trim() != "")
                                {
                                    textline1 = textline1 + " <br/> " + textline2;
                                    textline3 = file.ReadLine() + "";
                                    if (textline3 != "")
                                    {
                                        textline1 = textline1 + " <br/> " + textline3;
                                    }
                                }
                            }
                            if (CheckInBetween.Invoke(_StartTime, _EndTime, VITCTime, IsSubtitleLoaded) == true)
                            {
                                UpdateSrtSubTitleText(textline1);
                                LoadSubtitleFile(ref _StartTime, ref _EndTime);
                            }
                            if (CheckIsGraterEndTime.Invoke(EndTime, VITCTime, IsSubtitleLoaded) == true)
                            {
                                KillSubtitleFile(ref _StartTime, ref _EndTime);
                            }
                        }
                    }

                    file.Close();
                }

            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ReadSrtProcess", "Error in Reading Srt file" + ex.ToString(), ObjMain.ChannelName);
            }
        }

        private void ReadCipProcess()
        {
            try
            {
                using (StreamReader file = new StreamReader(RunningFileName, Encoding.Default))
                {
                    string singleline = "";
                    //-----Reading File Header FILE_INFO_END Last Of Header
                    while ((singleline = file.ReadLine()) != null)
                    {
                        if (singleline.ToUpper().Trim() == "FILE_INFO_END\\PAR")
                        {
                            break;
                        }
                    }
                    //-----Start Reading 
                    while ((singleline = file.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(singleline) || singleline == "}")
                        {
                            continue;
                        }

                        string _locStartTime = "00:00:00.00";
                        string _locEndTime = "00:00:00.00";

                        if (ValidateCIPTime(singleline))
                        {
                            int StartTimeIndex = singleline.IndexOf(':');
                            string GetTimeString = singleline.Substring(StartTimeIndex - 2, 27);
                            GetStartEndTime(GetTimeString, out _locStartTime, out _locEndTime);
                            _locStartTime = AddOffset(_locStartTime, ObjMain.SubtitleStartOffset);
                            _locEndTime = AddOffset(_locEndTime, ObjMain.SubtitleEndOffset);
                        }
                        if (CheckInBetween.Invoke(_locStartTime, _locEndTime, VITCTime, IsSubtitleLoaded) == true)
                        {
                            string FinalString = "";
                            bool IsUnderLine = false;
                            bool IsItalic = false;
                            int y_pos = GetDefaultPosition(ref singleline);
                           // int clountln = 0;
                            while (((singleline = file.ReadLine()) != null) && (singleline.Contains("fs4") || singleline.Contains("fs3")))
                            {
                                if (singleline == "") { continue; }
                                string[] ArryData = singleline.Split('\\');
                                FinalString = FinalString == "" ? (GetDisplayText(ArryData)) : (FinalString + " <br/> " + GetDisplayText(ArryData));
                                if (singleline.Contains(@"\ul\") && IsUnderLine == false)
                                {
                                    IsUnderLine = true;
                                }
                                if (singleline.Contains(@"\i\") && IsItalic == false)
                                {
                                    IsItalic = true;
                                }
                            }
                            if (UpdateCipFile(FinalString, ref IsUnderLine, ref IsItalic, ref y_pos))
                            {
                                LoadSubtitleFile(ref _locStartTime, ref _locEndTime);
                            }

                        }
                        //---Checking ViTC time  is grater than End Time for close subtitle text----------------------------------------------------------------------
                        if (CheckIsGraterEndTime.Invoke(EndTime, VITCTime, IsSubtitleLoaded) == true)
                        {
                            KillSubtitleFile(ref _locStartTime, ref _locEndTime);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("ReadCIPProcess", "Error in Reading CIP file" + ex.ToString(), ObjMain.ChannelName);

            }
        }

        public void ReadSubTitleFile()
        {
            try
            {
                switch (FileType.ToUpper())
                {

                    case ".SRT":
                        {
                           //   ReadSrtProcess(); //commented for disney
                            ReadSrtProcessWithPos();
                            break;
                        }
                    case ".CIP":
                        {
                            ReadCipProcess();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {

            }

        }

        Predicate<string> ValidateCIPTime = (props) =>
        {
            int StartTimeIndex = props.IndexOf(':');
            int TotalStringLength = props.Length;
            if (StartTimeIndex < 0)
            {
                return false;
            }
            if (TotalStringLength < ((StartTimeIndex - 2) + 27))
            {
                return false;
            }
            int freq = props.Count(f => (f == ':'));
            if (freq < 6)
            {
                return false;
            }
            return true;
        };
        private int GetDefaultPosition(ref string textline1)
        {
            int i = -100;
            try
            {
                String[] arr = textline1.Split('\\');
                string Data = Array.Find<string>(arr, e => e.Trim().StartsWith("tab #F"));
                Data = Regex.Replace(Data, "[^0-9]+", string.Empty);
                Data = Data.Length <= 3 ? Data : Data.Substring(0, 3);
                return Convert.ToInt32(Data);
            }
            catch
            {

                return i;
            }
        }
        private void LoadSubtitleFile(ref string _StartTime, ref string _EndTime)
        {
            try
            {

                ObjMain.LoadSubtitle(ObjMain.SubtitleTemplate);
                // ObjMain.objlog.WriteEvents("LoadingTimeLOG", $"Loading Subtitle Template StartTime= {_StartTime} ,EndTime={_EndTime} ;", ObjMain.ChannelName);

            }
            catch { }
            finally
            {
                IsSubtitleLoaded = true;
                EndTime = _EndTime;
                StartTime = _StartTime;
            }
        }

        private void KillSubtitleFile(ref string _StartTime, ref string _EndTime)
        {
            try
            {
                ObjMain.KillSubtitle(ObjMain.SubtitleTemplate);
            }
            catch { }
            finally
            {
                IsSubtitleLoaded = false;
            }
        }


        private void GetStartEndTime(string TimeFormate, out string StartTime, out string EndTime)
        {
            StartTime = "00:00:00.00";
            EndTime = "00:00:00.00";
            try
            {

                string[] arrTime = TimeFormate.Split(new string[] { "\\tab" }, StringSplitOptions.RemoveEmptyEntries);
                StartTime = arrTime.Length > 0 ? arrTime[0] : "00:00:00.00";
                EndTime = arrTime.Length > 1 ? arrTime[1] : "00:00:00.00";

            }
            catch { }
        }
        private string GetDisplayText(string[] arrdata)
        {
            string FinalStringData = "";
            try
            {
                string Data = Array.Find(arrdata, e => e.StartsWith("fs4")|| e.StartsWith("fs3"));
                FinalStringData = Data.Length > 4 ? Data.Substring(4, Data.Length - 4) : "";
                return FinalStringData;
            }
            catch (Exception ex)
            {
                return FinalStringData;
            }
        }
        private string AddOffset(string MainTime, string OffsetTime)
        {
            try
            {
                string[] temarr = MainTime.Split(new string[] { ":", "." }, StringSplitOptions.None);
                TimeSpan ts1 = new TimeSpan(0, Convert.ToInt32(temarr[0]), Convert.ToInt32(temarr[1]), Convert.ToInt32(temarr[2]), Convert.ToInt32(temarr[3]) * 40);
                int result = 0;
                int.TryParse(OffsetTime, out result);
                TimeSpan ts2 = new TimeSpan(0, 0, 0, 0, result * 40);
                return ((ts1 + ts2).Hours.ToString("00") + ":" + (ts1 + ts2).Minutes.ToString("00") + ":" + (ts1 + ts2).Seconds.ToString("00") + ":" + ((ts1 + ts2).Milliseconds / 40).ToString("00"));

            }
            catch
            {
                return MainTime;
            }
        }
        public long GetFrameFromTime(string sTime)
        {
            try
            {
                long ReturnFrame = 0;
                ReturnFrame = Convert.ToInt64(sTime.ToString().Split(':')[0]) * (60 * 60 * 25);
                ReturnFrame += Convert.ToInt64(sTime.ToString().Split(':')[1]) * (60 * 25);
                ReturnFrame += Convert.ToInt64(sTime.ToString().Split(':')[2]) * 25;
                ReturnFrame += Convert.ToInt64(sTime.ToString().Split(':')[3]);
                return ReturnFrame;

            }
            catch
            {
                return -1;
            }

        }

        public string GetTimeFromFrame(long iFrames)
        {
            try
            {
                string ReturnTime = "00:00:00:00";

                long Hours, Reme, Min, Sec, Frame;
                string FHours, FMin, FSec, FFrame;

                Hours = iFrames / 90000;

                if (Hours.ToString().Length == 1)
                {
                    FHours = "0" + Hours.ToString().Trim();
                }
                else
                {
                    FHours = Hours.ToString().Trim();
                }
                if (Convert.ToInt64(FHours) >= 24)
                {
                    FHours = Convert.ToString(Convert.ToInt64(FHours) % 24);
                    if (FHours.ToString().Length == 1)
                    {
                        FHours = "0" + FHours.ToString().Trim();
                    }
                    else
                    {
                        FHours = FHours.ToString().Trim();
                    }
                }
                Reme = iFrames % 90000;
                Min = Reme / 1500;

                if (Min.ToString().Length == 1)
                {
                    FMin = "0" + Min.ToString().Trim();
                }
                else
                {
                    FMin = Min.ToString().Trim();
                }
                Reme = Reme % 1500;
                Sec = Reme / 25;

                if (Sec.ToString().Length == 1)
                {
                    FSec = "0" + Sec.ToString().Trim();
                }
                else
                {
                    FSec = Sec.ToString().Trim();
                }
                Frame = Reme % 25;


                if (Frame.ToString().Length == 1)
                {
                    FFrame = "0" + Frame.ToString().Trim();
                }
                else
                {
                    FFrame = Frame.ToString().Trim();
                }
                ReturnTime = FHours.ToString() + ":" + FMin.ToString() + ":" + FSec.ToString() + ":" + FFrame.ToString();
                return ReturnTime;
            }
            catch (Exception Ex)
            {
                ObjMain.objlog.WriteError("GetTimeFromFrame", " Frame Receive : " + iFrames + ", Error is: " + Ex.Message.ToString(), ObjMain.ChannelName);
                return "00:00:00:00";
            }

        }

        private int GetSrno(string text)
        {
            int result = -1;
            try
            {

                int.TryParse(text, out result);
                return result;
            }
            catch { return -1; }
        }


        private void GetTime(ref string strtendTimeLine, out String StartTime, out String EndTime)
        {
            StartTime = "00:00:00:00";
            EndTime = "00:00:00:00";
            try
            {
                string[] time = null;
                if ((time = strtendTimeLine.Split(new String[] { "-->" }, StringSplitOptions.None)).Length > 1)
                {
                    StartTime = CalCulateTime(time[0]);
                    EndTime = CalCulateTime(time[1]);
                }
            }
            catch (Exception es)
            {
            }
        }

        private bool ValidateTime(string time)
        {
            if (time == "00:00:00:00" || time.Split(':').Length != 4)
            {
                return false;
            }
            return true;
        }


        private string CalCulateTime(string strtime)
        {
            try
            {

                string[] arr = strtime.Split(':', ',');
                TimeSpan tm = new TimeSpan(0, Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[2]), Convert.ToInt32(arr[3]));
                var tm_result = (tm);
                return (tm_result.Hours.ToString("00") + ":" + tm_result.Minutes.ToString("00") + ":" + tm_result.Seconds.ToString("00") + ":" + (tm_result.Milliseconds / 40).ToString("00"));
            }
            catch
            {
                return "00:00:00:00";
            }
        }

        //Checks whether Z is in between X or Y
        /// <summary>
        /// <para>x=> Start Time.</para> 
        /// <para> Y=>End Time.</para>
        /// <para> Z=> This time is check exist between.</para>
        /// <para> f=> flag for subtitle loaded.</para>
        /// </summary>
        internal Func<string, string, string, bool, bool> CheckInBetween = (x, y, z, f) =>
         {
             try
             {
                 if (f == true)
                 {
                     return false;
                 }
                 string[] _starttime = x.Split(':');
                 if (_starttime.Length != 4) return false;
                 TimeSpan Starttime = new TimeSpan(0, Convert.ToInt32(_starttime[0]), Convert.ToInt32(_starttime[1]), Convert.ToInt32(_starttime[2]), Convert.ToInt32(_starttime[3]) * 40);
                 string[] _endtime = y.Split(':');
                 if (_endtime.Length != 4) return false;
                 TimeSpan Endtime = new TimeSpan(0, Convert.ToInt32(_endtime[0]), Convert.ToInt32(_endtime[1]), Convert.ToInt32(_endtime[2]), Convert.ToInt32(_endtime[3]) * 40);
                 string[] Second_Time = z.Split(':');
                 if (Second_Time.Length != 4) return false;
                 TimeSpan _Second_Time = new TimeSpan(0, Convert.ToInt32(Second_Time[0]), Convert.ToInt32(Second_Time[1]), Convert.ToInt32(Second_Time[2]), Convert.ToInt32(Second_Time[3]) * 40);

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

        /// <summary>
        /// <para>First Parameter Is End Time(_endtime ).</para>
        /// <para>Second Parameter Is Vitc Time .</para>
        ///<para> Third Parameter is Flag for isloaded Subtitle.</para>
        /// </summary>
        Func<string, string, bool, bool> CheckIsGraterEndTime => (_endtime, _vitctime, _isloaded) =>
                 {
                     if (_isloaded == false)
                     {
                         return false;
                     }
                     if (GetFrameFromTime(_endtime) < GetFrameFromTime(_vitctime))
                     {
                         return true;
                     }
                     return false;
                 };




        private bool UpdateSrtSubTitlewithposText(string SubtitleText,string xpos,string ypos)
        {
            XmlDocument xmlDoc = null;
            String _TemplatePath = ObjMain.TemplatePath + "/" + ObjMain.SubtitleTemplate + ".ml-cgs";
            try
            {
                if (!File.Exists(_TemplatePath))
                {
                    ObjMain.objlog.WriteEvents("UpdateSubTitleText", $"Templatepath={_TemplatePath} does not exist so not able to play template.", ObjMain.ChannelName);
                    return false;
                }
                xmlDoc = new XmlDocument();
                xmlDoc.Load(_TemplatePath);
                ScreenDataMapping obj = new ScreenDataMapping();
                bool bfoundPos = false;
                //foreach (var item in ObjMain.lstData)
                //{
                //    if (SubtitleText.Contains(item.ReceiveText))
                //    {
                //        bfoundPos = true;
                //        SubtitleText = SubtitleText.Replace(item.ReceiveText, "");
                //        var Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/anchor");
                //        if (Ancornode != null)
                //        {
                //            if (Ancornode.Attributes["item"] != null) { Ancornode.Attributes["item"].Value = item.ItemPosition; }
                //            if (Ancornode.Attributes["screen"] != null) { Ancornode.Attributes["screen"].Value = item.ScreenPosition; }
                //        }
                //        var Indentnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props/indent");
                //        if (Indentnode != null)
                //        {
                //            if (Indentnode.Attributes["left"] != null) { Indentnode.Attributes["left"].Value = item.left; }
                //            if (Indentnode.Attributes["right"] != null) { Indentnode.Attributes["right"].Value = item.right; }
                //            if (Indentnode.Attributes["top"] != null) { Indentnode.Attributes["top"].Value = item.top; }
                //            if (Indentnode.Attributes["bottom"] != null) { Indentnode.Attributes["bottom"].Value = item.bottom; }
                //            if (Ancornode.Attributes["use-for-bg"] != null) { Ancornode.Attributes["use-for-bg"].Value = item.useforbg; }
                //        }

                //        break;
                //    }
                //}




                var Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/anchor");
                if (Ancornode != null)
                {
                    bfoundPos = true;
                    if (Ancornode.Attributes["item"] != null) { Ancornode.Attributes["item"].Value = ObjMain.lstData[6].ItemPosition; }
                    if (Ancornode.Attributes["screen"] != null) { Ancornode.Attributes["screen"].Value = ObjMain.lstData[6].ScreenPosition; }
                }
                var Indentnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props/indent");
                XmlNode node;
                int leftRightshift = 0;
                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/text[@type ='text']");
                if (node != null & node.Attributes["SubtitleSingleLineShiftOffset"] != null)
                {
                    string leftRight = node.Attributes["SubtitleSingleLineShiftOffset"].Value;
                    leftRightshift = Int16.Parse(leftRight.Trim());
                }
                if (Indentnode != null)
                {
                    if(ObjMain.ChannelType.ToUpper()=="HD")
                    {
                        //  xpos = (1920 - Convert.ToInt32(xpos)).ToString();
                        //  ypos = (1080 - Convert.ToInt32(ypos)).ToString();
                        xpos = (Convert.ToInt32(xpos) + leftRightshift).ToString();
                        ypos = (Convert.ToInt32(ypos)).ToString();
                    }
                        else
                            {
                      //  xpos = (720 - Convert.ToInt32(xpos)).ToString();
                        ypos = (Convert.ToInt32(ypos)).ToString();
                        xpos = (Convert.ToInt32(xpos) + leftRightshift).ToString() ;
                    }
                    if (Indentnode.Attributes["left"] != null) { Indentnode.Attributes["left"].Value =  xpos; }
                    if (Indentnode.Attributes["right"] != null) { Indentnode.Attributes["right"].Value = ObjMain.lstData[6].right; }
                    if (Indentnode.Attributes["top"] != null) { Indentnode.Attributes["top"].Value = ObjMain.lstData[6].top; }
                    if (Indentnode.Attributes["bottom"] != null) { Indentnode.Attributes["bottom"].Value =ypos; }
                    if (Ancornode.Attributes["use-for-bg"] != null) { Ancornode.Attributes["use-for-bg"].Value = ObjMain.lstData[6].useforbg; }
                }



                 if (bfoundPos == false)
                {
                    DeafultSubtitlePos(ref xmlDoc, ElementId);
                }
                //Set Y position as 0;
                var CgProps = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props");
                if (CgProps != null)
                {
                    CgProps.Attributes["pos-y"].Value = "0";
                }
              //  XmlNode node;
                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/text[@type ='text']");
                if (node.Attributes["italic"] != null)
                {
                    node.Attributes["italic"].Value = SubtitleText.Contains("</i>") == true ? "true" : "false";
                }
                if (node.Attributes["bold"] != null)
                {
                    node.Attributes["bold"].Value = SubtitleText.Contains("</b>") == true ? "true" : "false";
                }

                if (node.Attributes["strikeout"] != null)
                {
                    node.Attributes["strikeout"].Value = SubtitleText.Contains("</u>") == true ? "true" : "false";
                }
                if (node != null)
                {
                    node.InnerXml = SubtitleText;
                }
                xmlDoc.Save(_TemplatePath);
                return true;
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("UpdatesrtSubTitlewithposText", "Error in Updating subtitle Template... " + ex.ToString(), ObjMain.ChannelName);
                return false;
            }
        }

        /// <summary>
        /// Provide Text for update in Template File.
        /// </summary>
        /// <param name="SubtitleText"></param>
        /// <returns></returns>
        private bool UpdateSrtSubTitleText(string SubtitleText)
        {
            XmlDocument xmlDoc = null;
            String _TemplatePath = ObjMain.TemplatePath + "/" + ObjMain.SubtitleTemplate + ".ml-cgs";
            try
            {
                if (!File.Exists(_TemplatePath))
                {
                    ObjMain.objlog.WriteEvents("UpdateSubTitleText", $"Templatepath={_TemplatePath} does not exist so not able to play template.", ObjMain.ChannelName);
                    return false;
                }
                xmlDoc = new XmlDocument();
                xmlDoc.Load(_TemplatePath);
                ScreenDataMapping obj = new ScreenDataMapping();
                bool bfoundPos = false;
                foreach (var item in ObjMain.lstData)
                {
                    if (SubtitleText.Contains(item.ReceiveText))
                    {
                        bfoundPos = true;
                        SubtitleText = SubtitleText.Replace(item.ReceiveText, "");
                        var Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/anchor");
                        if (Ancornode != null)
                        {
                            if (Ancornode.Attributes["item"] != null) { Ancornode.Attributes["item"].Value = item.ItemPosition; }
                            if (Ancornode.Attributes["screen"] != null) { Ancornode.Attributes["screen"].Value = item.ScreenPosition; }
                        }
                        var Indentnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props/indent");
                        if (Indentnode != null)
                        {
                            if (Indentnode.Attributes["left"] != null)      { Indentnode.Attributes["left"].Value = item.left; }
                            if (Indentnode.Attributes["right"] != null)     { Indentnode.Attributes["right"].Value = item.right; }
                            if (Indentnode.Attributes["top"] != null)       { Indentnode.Attributes["top"].Value = item.top; }
                            if (Indentnode.Attributes["bottom"] != null)    { Indentnode.Attributes["bottom"].Value = item.bottom; }
                            if (Ancornode.Attributes["use-for-bg"] != null) { Ancornode.Attributes["use-for-bg"].Value = item.useforbg; }
                        }

                        break;
                    }
                }
                if (bfoundPos == false)
                {
                    DeafultSubtitlePos(ref xmlDoc, ElementId);
                }
                //Set Y position as 0;
                var CgProps = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props");
                if (CgProps != null)
                {
                    CgProps.Attributes["pos-y"].Value = "0";
                }
                XmlNode node;
                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/text[@type ='text']");
                if (node.Attributes["italic"] != null)
                {
                    node.Attributes["italic"].Value = SubtitleText.Contains("</i>") == true ? "true" : "false";
                }
                if (node.Attributes["bold"] != null)
                {
                    node.Attributes["bold"].Value = SubtitleText.Contains("</b>") == true ? "true" : "false";
                }

                if (node.Attributes["strikeout"] != null)
                {
                    node.Attributes["strikeout"].Value = SubtitleText.Contains("</u>") == true ? "true" : "false";
                }
                if (node != null)
                {
                    node.InnerXml = SubtitleText;
                }
                xmlDoc.Save(_TemplatePath);
                return true;
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("UpdateSubTitleText", "Error in Updating subtitle Template... " + ex.ToString(), ObjMain.ChannelName);
                return false;
            }
        }

        private bool UpdateCipFile(string SubtitleText, ref bool IsUnderLine, ref bool IsItalic, ref int y_pos)
        {
            XmlDocument xmlDoc = null;
            string sSublitileYposoffset = "0";
            string sLineGap="10";
            string SubtitleSingleLineShiftOffset = "0";
        String _TemplatePath = ObjMain.TemplatePath + "/" + ObjMain.SubtitleTemplate + ".ml-cgs";
            try
            {
                if (!File.Exists(_TemplatePath))
                {
                    ObjMain.objlog.WriteEvents("UpdateSubTitleText", $"Templatepath={_TemplatePath} does not exist so not able to play template.", ObjMain.ChannelName);
                    return false;
                }
               
                 xmlDoc = new XmlDocument();
                xmlDoc.Load(_TemplatePath);
                XmlNode node;
                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/text[@type ='text']");
                if (node.Attributes["italic"] != null) { node.Attributes["italic"].Value = IsItalic == true ? "true" : "false"; }
                if (node.Attributes["bold"] != null) { node.Attributes["bold"].Value = SubtitleText.Contains("</b>") == true ? "true" : "false"; }
                if (node.Attributes["strikeout"] != null) { node.Attributes["strikeout"].Value = IsUnderLine == true ? "true" : "false"; }

                if (node.Attributes["SubtitleYposoffset"] != null)
                {
                    sSublitileYposoffset = node.Attributes["SubtitleYposoffset"].Value;
                    
                }
                if (node.Attributes["SubtitleSingleLineShiftOffset"] != null)
                {
                    SubtitleSingleLineShiftOffset = node.Attributes["SubtitleSingleLineShiftOffset"].Value;
                }
               

               if (node != null) { node.InnerXml = SubtitleText; }
                
                //Update Default Y-Position default-y="500" toooooo pos-y="0"
                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props");


                if (node.Attributes["Gap"] != null)
                {
                    sLineGap = node.Attributes["Gap"].Value;
                }

                if (node.Attributes["default-y"] != null)
                {
                    int parseValue = 0;
                    int.TryParse(node.Attributes["default-y"].Value, out parseValue);

                    if (ObjMain.ChannelType == "HD")
                    {
                        if (y_pos > -1)
                        {
                            //node.Attributes["pos-y"].Value = Convert.ToString(y_pos);
                            if (SubtitleText.Contains("<br/>") == false)
                            {
                                // int nYpos = (Math.Abs(((Math.Abs(1080 - y_pos) + 120)) + Int32.Parse(sSublitileYposoffset)) / 2)  + Int32.Parse(sLineGap) + Int32.Parse(SubtitleSingleLineShiftOffset);
                                int nYpos = (Math.Abs(((Math.Abs(1080 - y_pos) + 120)) + Int32.Parse(sSublitileYposoffset)) / 2)  + Int32.Parse(SubtitleSingleLineShiftOffset);
                                node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                            }
                            else
                            {
                                
                                int occr = CountStringOccurrences(SubtitleText, "<br/>");
                                if (occr == 1)
                                {
                                    int nYpos = Math.Abs(((Math.Abs(1080 - y_pos) + 120)) + Int32.Parse(sSublitileYposoffset)) / 2;
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                                else if (occr == 2)
                                {
                                    int nYpos = Math.Abs(((Math.Abs(1080 - y_pos) + 120)) + Int32.Parse(sSublitileYposoffset)) / 2 - Int32.Parse(sLineGap);
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                                else if(occr == 3)
                                {
                                   int nYpos = Math.Abs(((Math.Abs(1080 - y_pos) + 120)) + Int32.Parse(sSublitileYposoffset)) / 2 - 2 * Int32.Parse(sLineGap);
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                            }
                        }
                        else
                        {
                            if (SubtitleText.Contains("<br/>") == false)
                            {
                                //   node.Attributes["pos-y"].Value = Convert.ToString(parseValue + Int32.Parse(sLineGap) + Int32.Parse(SubtitleSingleLineShiftOffset));
                                node.Attributes["pos-y"].Value = Convert.ToString(parseValue  + Int32.Parse(SubtitleSingleLineShiftOffset));
                            }
                            else
                            {
                                int occr = CountStringOccurrences(SubtitleText, "<br/>");
                                if (occr == 1)
                                    node.Attributes["pos-y"].Value = Convert.ToString(parseValue);
                                else if (occr == 2)
                                    node.Attributes["pos-y"].Value = Convert.ToString(parseValue - Int32.Parse(sLineGap));
                                else if (occr == 3)
                                    node.Attributes["pos-y"].Value = Convert.ToString(parseValue - 2 * Int32.Parse(sLineGap));
                            }
                        }
                    }
                    else
                    {

                        if (y_pos > -1)
                        {

                            //526,410

                            if (SubtitleText.Contains("<br/>") == false)
                            {
                                //   int nYpos = Math.Abs(((Math.Abs(576 - y_pos) + 60)) + Int32.Parse(sSublitileYposoffset) + Int32.Parse(sLineGap)) + Int32.Parse(SubtitleSingleLineShiftOffset);
                                int nYpos = Math.Abs(((Math.Abs(576 - y_pos) + 60)) + Int32.Parse(sSublitileYposoffset)) + Int32.Parse(SubtitleSingleLineShiftOffset);
                                node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                            }
                            else
                            {
                                int occr = CountStringOccurrences(SubtitleText, "<br/>");
                                if (occr == 1)
                                {
                                    int nYpos = Math.Abs(((Math.Abs(576 - y_pos) + 60)) + Int32.Parse(sSublitileYposoffset));
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                                else if(occr ==2)
                                    {
                                    int nYpos = Math.Abs(((Math.Abs(576 - y_pos) + 60)) + Int32.Parse(sSublitileYposoffset)) - Int32.Parse(sLineGap);
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                                else if (occr == 3)
                                {
                                    int nYpos = Math.Abs(((Math.Abs(576 - y_pos) + 60)) + Int32.Parse(sSublitileYposoffset)) - 2*Int32.Parse(sLineGap);
                                    node.Attributes["pos-y"].Value = Convert.ToString(nYpos);
                                }
                            }
                        }
                        else
                        {
                            if (SubtitleText.Contains("<br/>") == false)
                            {
                                //  node.Attributes["pos-y"].Value = Convert.ToString(parseValue + Int32.Parse(sLineGap) + Int32.Parse(SubtitleSingleLineShiftOffset));
                                node.Attributes["pos-y"].Value = Convert.ToString(parseValue  + Int32.Parse(SubtitleSingleLineShiftOffset));
                            }
                            else
                            {
                                int occr = CountStringOccurrences(SubtitleText, "<br/>");
                                if(occr ==1)
                                node.Attributes["pos-y"].Value = Convert.ToString(parseValue);
                                else if(occr ==2)
                                node.Attributes["pos-y"].Value = Convert.ToString(parseValue- Int32.Parse(sLineGap));
                                else if (occr == 3)
                                 node.Attributes["pos-y"].Value = Convert.ToString(parseValue - 2*Int32.Parse(sLineGap));
                            }
                        }

                    }

                }
                var Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/anchor");
                if (Ancornode != null)
                {
                    ScreenDataMapping item = ObjMain.lstData.Where(e => e.ReceiveText.ToUpper() == "CIP").FirstOrDefault();
                    if (item != null)
                    {
                        if (Ancornode.Attributes["item"] != null) { Ancornode.Attributes["item"].Value = item.ItemPosition; }
                        if (Ancornode.Attributes["screen"] != null) { Ancornode.Attributes["screen"].Value = item.ScreenPosition; }
                    }
                }
                Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + ElementId + "']/cg-props/indent");
                if (Ancornode != null)
                {
                    ScreenDataMapping item = ObjMain.lstData.Where(e => e.ReceiveText.ToUpper() == "CIP").FirstOrDefault();
                    if (item != null)
                    {
                        if (Ancornode.Attributes["use-for-bg"] != null) { Ancornode.Attributes["use-for-bg"].Value = item.useforbg; }
                        if (Ancornode.Attributes["left"] != null) { Ancornode.Attributes["left"].Value = item.left; }
                        if (Ancornode.Attributes["right"] != null) { Ancornode.Attributes["right"].Value = item.right; }
                        if (Ancornode.Attributes["top"] != null) { Ancornode.Attributes["top"].Value = item.top; }
                        if (Ancornode.Attributes["bottom"] != null) { Ancornode.Attributes["bottom"].Value = item.bottom; }
                    }
                }

                xmlDoc.Save(_TemplatePath);
                return true;
            }
            catch
            {
                return false;

            }
        }


        public  int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private void DeafultSubtitlePos(ref XmlDocument xmlDoc, string pararmeterID)
        {
            try
            {

                ScreenDataMapping item = ObjMain.lstData.Where(e => e.ReceiveText.ToLower() == "{\\an2}".ToLower()).FirstOrDefault();
                if (item != null)
                {
                    var Ancornode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + pararmeterID + "']/anchor");
                    if (Ancornode != null)
                    {
                        if (Ancornode.Attributes["item"] != null) { Ancornode.Attributes["item"].Value = item.ItemPosition; }
                        if (Ancornode.Attributes["screen"] != null) { Ancornode.Attributes["screen"].Value = item.ScreenPosition; }

                    }
                    var Indentnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + pararmeterID + "']/cg-props/indent");
                    if (Indentnode != null)
                    {
                        if (Indentnode.Attributes["left"] != null) { Indentnode.Attributes["left"].Value = item.left; }
                        if (Indentnode.Attributes["right"] != null) { Indentnode.Attributes["right"].Value = item.right; }
                        if (Indentnode.Attributes["top"] != null) { Indentnode.Attributes["top"].Value = item.top; }
                        if (Indentnode.Attributes["bottom"] != null) { Indentnode.Attributes["bottom"].Value = item.bottom; }
                        if (Indentnode.Attributes["use-for-bg"] != null) { Indentnode.Attributes["use-for-bg"].Value = item.useforbg; }
                    }
                }

            }
            catch (Exception ex)
            {

                ObjMain.objlog.WriteError("DeafultSubtitlePos", $"Error In Updating Default Position of template " + ex.ToString(), ObjMain.ChannelName);
            }
        }

        //end of class -----

        /// <summary>
        //PREPARE,BOXID                            // FOR FILE LOADING
        //STOP                                      //CURENT RUNNING SUBTITLE STOP
        //START,10:00:00:00                         // PLAY SUBTITLE FILE WITH HELP OF TIMECODE
        /// </summary>
        /// <param name="strSubtitleData"></param>
        /// 
        static bool IsUtf8WithBOM(string filePath)
        {
            try
            {
                // Open the file for reading
                using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    // Read the first 3 bytes (which would be the BOM if it's UTF-8 with BOM)
                    byte[] bom = reader.ReadBytes(3);

                    // Check if the BOM matches the UTF-8 BOM signature
                    return bom.Length == 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF;
                }
            }
            catch (Exception ex)
            {
                // Handle file reading errors
                Console.WriteLine("Error reading the file: " + ex.Message);
                return false;
            }
        }
        private bool boolStartActivation = false;
        private void HandleSubtitlePacket(string strSubtitleData)
        {
            try
            {
                lblUdpPacket.Text = strSubtitleData;
                if (ObjMain.SubtitleMode.ToUpper() == "AUTOMATION")
                {
                    ObjMain.objlog.WriteEvents("HandleSubtitlePacket,Received Subtitle packet--------->>>>>", strSubtitleData, ObjMain.ChannelName);
                    string strTimecode = "";
                    string[] strpacket = strSubtitleData.Split(',');
                    string OperationName = strpacket.Length > 0 ? strpacket[0] : "";
                    switch (OperationName.ToUpper())
                    {
                        case "PREPARE":
                            strSubtitleData = strpacket.Length > 1 ? strpacket[1] : "";
                            strSubtitleData += ObjMain.SubtitleSuffix;
                            string filenameWithPath = (ObjMain.SubtitlePath + "\\" + strSubtitleData);
                            if (CheckSubTitleFile(strSubtitleData, ref filenameWithPath))
                            {
                                ObjMain.objlog.WriteEvents("HandleSubtitlePacket", $"Subtitle File Found Going To Verify and Loading Subtitle File ", ObjMain.ChannelName);
                                lblPrepareId.Text = Path.GetFileNameWithoutExtension(filenameWithPath);
                                lblPrepareFileStatus.Text = "Available";
                                lblPrepareFileStatus.ForeColor = Color.Green;
                                lblPrepareFileType.Text = Path.GetExtension(filenameWithPath).ToUpper();
                                lblPrepareFileType.ForeColor = Color.Gray;
                                bFileCheckFlag = true;
                                PrepareFileName = filenameWithPath;
                                if (ObjMain.encodingSRT && (Path.GetExtension(filenameWithPath).ToUpper().Contains("SRT")))
                                {
                                    bool isUtf8WithBOM = IsUtf8WithBOM(filenameWithPath);
                                    //  isUtf8WithBOM = true;
                                    if (isUtf8WithBOM)
                                    {
                                        var utf8WithoutBOM = new System.Text.UTF8Encoding(false);
                                        File.SetAttributes(filenameWithPath, FileAttributes.Normal);
                                        var content = File.ReadAllLines(filenameWithPath);
                                        //   File.Delete(filenameWithPath);
                                        ////////////////////////////////////////
                                        List<string> cleanedLines = new List<string>();

                                        // A flag to keep track of whether the last added line was empty
                                        bool previousWasBlank = false;
                                        foreach (var line in content)
                                        {
                                            if (string.IsNullOrWhiteSpace(line))
                                            {
                                                if (previousWasBlank) // Preserve multiple consecutive blank lines
                                                {
                                                    cleanedLines.Add(line);
                                                }
                                                else if (cleanedLines.Count > 0 && !string.IsNullOrWhiteSpace(cleanedLines.Last())) // Remove single blank line between text
                                                {
                                                    // Skip adding a single blank line
                                                }
                                                previousWasBlank = true; // Mark the current line as blank
                                            }
                                            else
                                            {
                                                cleanedLines.Add(line);
                                                previousWasBlank = false; // Reset the flag when we encounter a non-empty line
                                            }
                                        }
                                        File.Delete(filenameWithPath);
                                        System.IO.File.WriteAllLines(filenameWithPath, cleanedLines);
                                    }
                                    else
                                    {
                                        var utf8WithoutBOM = new System.Text.UTF8Encoding(false);
                                        File.SetAttributes(filenameWithPath, FileAttributes.Normal);
                                        var content = File.ReadAllLines(filenameWithPath);
                                        List<string> cleanedLines = new List<string>();
                                        bool isformatright = true;
                                        // A flag to keep track of whether the last added line was empty
                                        bool previousWasBlank = false;
                                        foreach (var line in content)
                                        {
                                            if (string.IsNullOrWhiteSpace(line))
                                            {
                                                if (previousWasBlank && string.IsNullOrWhiteSpace(cleanedLines.Last()) && !string.IsNullOrWhiteSpace(cleanedLines[cleanedLines.Count() - 2])) // Preserve multiple consecutive blank lines
                                                {
                                                    cleanedLines.Add(line);
                                                }
                                                else if (cleanedLines.Count > 0 && !string.IsNullOrWhiteSpace(cleanedLines.Last())) // Remove single blank line between text
                                                {
                                                    // Skip adding a single blank line
                                                    if (previousWasBlank == true)
                                                        cleanedLines.Add(line);
                                                }
                                                previousWasBlank = true; // Mark the current line as blank
                                            }
                                            else
                                            {
                                                if (previousWasBlank && !string.IsNullOrWhiteSpace(cleanedLines.Last()))
                                                {
                                                    if (cleanedLines.Count() == 3)
                                                    {
                                                        isformatright = false;
                                                        return;
                                                    }
                                                    //   cleanedLines.Add(line);
                                                }
                                                if (!previousWasBlank && cleanedLines.Count > 0 && !string.IsNullOrWhiteSpace(cleanedLines.Last()))
                                                {
                                                    if (cleanedLines.Count() == 4)
                                                    {
                                                        isformatright = false;
                                                        return;
                                                    }
                                                    //   cleanedLines.Add(line);
                                                }
                                                if (!previousWasBlank && (cleanedLines.Count > 0) && !string.IsNullOrWhiteSpace(cleanedLines.Last()))
                                                {
                                                    if (cleanedLines.Count() == 5)
                                                    {
                                                        isformatright = false;
                                                        return;
                                                    }
                                                    //   cleanedLines.Add(line);
                                                }
                                                //else if(!previousWasBlank)
                                                cleanedLines.Add(line);
                                                previousWasBlank = false; // Reset the flag when we encounter a non-empty line
                                            }
                                        }
                                        if (isformatright)
                                        {
                                            File.Delete(filenameWithPath);
                                            System.IO.File.WriteAllLines(filenameWithPath, cleanedLines);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ObjMain.objlog.WriteEvents("HandleSubtitlePacket", $"Subtitle File not found ,FileName= {filenameWithPath} ", ObjMain.ChannelName);
                                bFileCheckFlag = false;
                                lblPrepareId.Text = Path.GetFileNameWithoutExtension(filenameWithPath);
                                lblPrepareFileType.Text = "NA";
                                lblPrepareFileType.ForeColor = Color.Red;
                                lblPrepareFileStatus.Text = "Not Available";
                                lblPrepareFileStatus.ForeColor = Color.Red;
                            }
                            break;
                        case "STOP":
                            bplay = false;
                            EndTime = "00:00:00:00";
                            Thread.Sleep(60);
                            ObjMain.KillSubtitle(ObjMain.SubtitleTemplate);
                            strSubtitleStartTime = "";
                            IsSubtitleLoaded = false;
                            lblRunningFileStatus.Text = "STOP";
                            lblRunningFileStatus.ForeColor = Color.Red;
                            break;
                        case "START":
                            strTimecode = strpacket.Length > 1 ? strpacket[1] : "00:00:00:00";
                            ObjMain.objlog.WriteEvents("HandleSubtitlePacket,StartTimecode of Media=", strTimecode+"LTC time= "+ LtcTime, ObjMain.ChannelName);
                            if (bFileCheckFlag == true)
                            {
                                txtStartTime.Text = strSubtitleStartTime = strTimecode;
                                StartFileTime = LtcTime;
                                ObjMain.objlog.WriteEvents("HandleSubtitlePacket,LTC at startof subtitle=", StartFileTime+"sysTime= "+ DateTime.Now.ToString("HH:mm:ss.fff"), ObjMain.ChannelName);                           
                                if ((PrepareFileName + "").ToUpper() != (RunningFileName + "").ToUpper())
                                {
                                    RunningFileName = PrepareFileName;
                                }
                                ObjMain.objlog.WriteEvents("HandleSubtitlePacket,Subtitle file name=", RunningFileName, ObjMain.ChannelName);
                                lblRunningId.Text = Path.GetFileNameWithoutExtension(RunningFileName);
                                lblRunningFileStatus.Text = "Running";
                                lblRunningFileStatus.ForeColor = Color.Green;
                                this.FileType = lblRunningFiletype.Text = (Path.GetExtension(RunningFileName) + "").ToUpper();
                                bplay = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ObjMain.objlog.WriteError("HandleSubtitlePacket", "Error in Handle Subtitle Packet: " + ex.ToString(), ObjMain.ChannelName);

            }
        }
        private bool CheckSubTitleFile(String FileName, ref string file_path)
        {

            try
            {

                string[] filefiles = Directory.GetFiles(ObjMain.SubtitlePath, FileName + ".*");
                if ((ObjMain.SubtitleFileType.ToUpper() != "BOTH"))
                {
                    foreach (string file in filefiles)
                    {
                        //  if (FileName.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())  //21322
                        string s = Path.GetExtension(file);
                        if (Path.GetExtension(file).ToUpper().Contains(ObjMain.SubtitleFileType))
                        {
                            lblPrepareFileStatus.Text = "Available";
                            lblPrepareFileStatus.ForeColor = Color.Green;
                            file_path = file;
                            return true;
                        }
                    }
                }
                else
                {
                    if (ObjMain.SubtitlePriority.ToUpper() == "SRT")
                    {
                        foreach (string file in filefiles)
                        {
                            //  if (FileName.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())
                            if (Path.GetExtension(file).ToUpper().Contains("SRT"))
                            {
                                lblPrepareFileStatus.Text = "Available";
                                lblPrepareFileStatus.ForeColor = Color.Green;
                                file_path = file;
                                return true;
                            }
                        }
                        foreach (string file in filefiles)
                        {
                            //  if (FileName.ToUpper() == Path.GetFileNameWithoutExtension(file).ToUpper())
                            if (Path.GetExtension(file).ToUpper().Contains("CIP"))
                            {
                                lblPrepareFileStatus.Text = "Available";
                                lblPrepareFileStatus.ForeColor = Color.Green;
                                file_path = file;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        foreach (string file in filefiles)
                        {
                        
                            if (Path.GetExtension(file).ToUpper().Contains("CIP"))
                            {
                                lblPrepareFileStatus.Text = "Available";
                                lblPrepareFileStatus.ForeColor = Color.Green;
                                file_path = file;
                                return true;
                            }
                        }
                        foreach (string file in filefiles)
                        {
                          
                            if (Path.GetExtension(file).ToUpper().Contains("SRT"))
                            {
                                lblPrepareFileStatus.Text = "Available";
                                lblPrepareFileStatus.ForeColor = Color.Green;
                                file_path = file;
                                return true;
                            }
                        }
                    }
                }
                lblPrepareFileStatus.Text = "Not Available";
                lblPrepareFileStatus.ForeColor = Color.Red;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSubtitleSetting obj = new FrmSubtitleSetting();
                obj.SubtitleInfoFile = ObjMain.XMLSubtitleInfo;
                obj.Show();
            }
            catch (Exception ex)
            {
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class ScreenDataMapping
    {
        public string ItemPosition { get; set; }
        public string ScreenPosition { get; set; }
        public string ReceiveText { get; set; }
        public string left { get; set; }
        public string right { get; set; }
        public string top { get; set; }
        public string bottom { get; set; }
        public string useforbg { get; set; }

        
    }
}
