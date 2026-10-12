using System;
using MFORMATSLib;
using MLCHARGENLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using graphicserver.Classes;
using System.Management;
using graphicserver.ContentForm;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Win32;
using MultiAppPmslDLibLib;

namespace graphicserver
{
    public enum StreamStatus
    {
        stoped, playing, ready
    }
    public enum InputSourceMode
    {
        cardNDI, virt
    }
    public partial class FrmMainServer : Form
    {
        #region Global variable
        public CancellationTokenSource SourceToken;
        private string EventId = string.Empty;
        internal string EventTime = string.Empty;
        internal string Duration = string.Empty;
        private string NewEventTime = string.Empty;
        private string PrimaryEndTime = string.Empty;
        private string MainXmlConfig;
        public string ChannelNamepath;
        public string XMLTemplateConfigfilepath;
        public string XMLSubtitleInfo;
        public string AppConfigPath;
        public string FreezTempPath;
        public string SubtitleConfigPath;
        internal DataTable _dttemplateinfo = null;//All template names for particular channel's will be stored in this      
        DataTable hidetemplatedt = null;
        DataTable hideabbreviationdt = null;
        internal DataTable breakfreetemplatedt = null;
        internal DataTable XMLSubtitleInfodt = null;
        internal string RunningPrimary = "";
        internal string XMLGridFormat;
        private string XMLHideAbbreviation;
        private string XMLHideTemplateInfo;
        private string XMLBreakFreeBugInfo;
        //hide and show Objchargen
        protected string m_strID;
        protected bool m_bDirty;
        protected XmlDocument xmlDoc;
        //hide and show objchargen_lband
        protected string m_strID_Lband;
        protected bool m_bDirty_Lband;
        protected XmlDocument xmlDoc_Lband;
        //For render
        public MFLiveClass myLiveSource;
       // public MFLiveClass myLiveSource1;
        public MFSinkClass m_objMFSink;
        public bool ISNDI = false;
        public List<string> listViewRenderers = new List<string>();
        public object m_objLock;
        public object m_objGridLock;
        public bool m_bWork;
        private object objwrite;
        private bool IsShow = true;
        //MFormats Objects
        public CoMLCharGenClass m_objCharGen;
        private CoMLCharGenClass m_objCharGen_Lband;
        private CoMLCharGenClass m_objCharGen_Logo;
        public CoMLCharGenClass m_objCharGenTemp;
        public M_AV_PROPS avprops;
        //Used CG Items List
        public List<string> UsedItems = new List<string>();
        //Garbage Counter
        private int garbagecounter = 0;
        //UDP
        private UdpClient ReceUdpClient;
        private UdpClient ReceiveLTC;
        UdpClient ReveiSubtitleSocket = null;
        private object objreceive = new object();
        //log class object
        public Loging objlog = null;
        public List<string> usedcgitemsmanually = new List<string>();
      //  public MFPreviewClass m_objPreviewTemp;
        private MFPreviewClass m_objPreview;
        public MFReaderClass ObjReader;
        private Render setrender;
        public Renderout outrender;
        private Aboutus aboutfrm;
        public manualgraphicsload load;
        internal PacketData objpacket;
        private GridForm objgrid;
        public MessageForm CustomMessageBox;
        PMSLReader objPmslReader = null;
        PMSLWriter NetworkStreamWriter = null;
        AgentMode objAgent;
        public TempPreview ObjTmpPreviewFrm = null;
        public bool isPreview = false;
        public int tmpcounter = 0;

        MultiAppDongleLib ObjMultiAppDongleLib;

        int iDongleProtectionRemainingDays;
        public string LicEndDate = "";
        public bool ClosedbyDongle = false;
        public bool DbOnlyForAsrun { get; set; } = false;
        #endregion
        #region "Config Value"   
        public bool ExternalAgentMode { get; set; } = true;
        public bool PreviewWindow { get; set; } = false;
        public int LTCPort { get; set; }
        public int CommandDelay { get; set; }
        public int ReceivingPort { get; set; }
        public string TemplatePath { get; set; }
        public string ContentPath { get; set; }
        public string SecondaryContentPath { get; set; } = "";
        public bool EnableDb { get; set; } = false;
        public string DbType { get; set; }
        public string DbIPAddress { get; set; }
        public string DbPort { get; set; }
        public string DbUsername { get; set; }
        public string DbPassword { get; set; }
        public string DbName { get; set; }
        public string GraphicsTable { get; set; }
        public string ChannelName { get; set; }//not fetched from config         
        public bool PMSPacketOn { get; set; }
        public string CueOffSet { get; set; }
        public string ProgramPrefix { get; set; }
        public string PromoPrefix { get; set; }
        public string CommercialPrefix { get; set; }
        public string ChannelType { get; set; }
        public bool ReconcileKeyCheck { get; set; }
        public bool OverlappingCheck { get; set; }
        public string SubtitlePath { get; set; }

        public string CnfPath { get; set; } // CNF Path

        public bool SubtitleCheck { get; set; }
        public string NdiOutputName { get; set; }
        public string TemplateRefreshInterval { get; set; } = "5000";
        public bool AutoUpdateTemplate { get; set; }
        public bool AddDelayincommand { get; set; }
        public bool gFxsqueeze { get; set; }
        public int CheckEventInterval { get; set; } = 250;

        public bool KeyingMode { get; set; }
        public int KeyerPort { get; set; }

        public bool KeyerHeartBeat { get; set; }

        private string _keyrType = "fill";
        public string KeyerType
        {
            get { return _keyrType; }
            set { _keyrType = value; }
        }
        //variable for SCTE35
        public bool ScteEnable { get; set; }
        public string ScteTemplateName { get; set; }
        public string ScteIP { get; set; }
        public string SctePort { get; set; }
        public string SctePreRoll { get; set; }
        public ScteUserControl ObjScte;
        //variable for SCTE35

        /// <variables RtmpStream>
        public StreamStatus StreamMode;
        public InputSourceMode sourceMode;
        public string NetworkMode { get; set; }
        public string StreamType { get; set; }
        public string VideoFormatType { get; set; }
        public string NetworkURL { get; set; }

        public string CodecId { get; set; }
        public string Vbitrate { get; set; }
        /// </RtmpStream>
        /// <Gfx Client
        public string GfxclientIp { get; set; }
        public string GfxclientPort { get; set; }
        public string GfxclientEnable { get; set; }
        //asrun
        public bool ASRunEnable { get; set; }
        public string AsRunType { get; set; }
        public string AsRunFolderPath { get; set; }
        public string AsRunTableName { get; set; }
        public string AsRunDateTimeColumn { get; set; }
        public string AsRunTemplateNameColumn { get; set; }
        public string AsRunFilenameColumn { get; set; }
        public string AsRunTemplateFileNameColumn { get; set; }
        public string AsRunStatusColumn { get; set; }

        public string AsRunChannelNameMam { get; set; } = "";
        public string AsRunDurationColumn { get; set; }
        public string AsRunReconcileKeyColumn { get; set; }
        public bool AsRunManualEnable { get; set; } = false;
        public String AsRunTakeTime { get; set; } = "SYSTEM-TIME";

        //asrun

        //subtitle
        public string SubtitlePriority { get; set; } = "SRT"; //21322
        public string SubtitleMode { get; set; }
        public string SubtitleFileType { get; set; }
        public bool SubtitleEnable { get; set; } = false;
        public int SubtitlePort { get; set; } = 5200;
        public string SubtitleStartOffset { get; set; } = "0";
        public string SubtitleEndOffset { get; set; } = "0";
        public string SubtitleSuffix { get; set; }
        public string SubtitleTemplate { get; set; } = "Subtitle";
        UCSubtitle ObjSubtitle = null;
        public List<ScreenDataMapping> lstData = new List<ScreenDataMapping>();
        public bool encodingSRT = false;
        public string[] ReceiveddatafromPMS { get; set; }
        CustomTrigger CustumOperation = null;
        //AudioSetting

        public string AudioSettingEnable { get; set; }
        public string AudioChannel { get; set; }
        public string AudioSamplePerSecond { get; set; }
        public string AudioBitsPerSample { get; set; }
        public string AudioBitrate { get; set; }

        //AjaSettings  
        public string AJASettingsEnable { get; set; }
        public string AJACardReference { get; set; }
        //Auto closed aston
        public bool DurationBasedAston { get; set; }
        #endregion
        public string EASFontName = "";
        public double EASFontSize = 0;
        public string EASFontStyle = "false";
        public double EASPosY = 0.9;
        public double EASSizeY = 0.1;
        static Dictionary<string, string> KeyIV = new Dictionary<string, string>() { { "S@LT&KEYANIL@LT&KEYNEWS@LT&KEYNE", "@1B2c3D4e5F6g7H@" },
            { "S@LT&KES@VINAYYNEWS@LT&KEYNEWYNE", "@1B2c3F6gD4e57H@" },
            { "S@WS@LT&KNAMANS@LT&KEYNELT&KEYNE", "@c3D4e5F61B2g7H@" },
            { "KEYNEWSSHOBHITS@LT&KEYNEWS@LT&NE", "@17HB2c3D4e5F6g@" },
            { "S@LT&KS@ANMOLYEYNEWS@LT&KEYNEWNE", "c3D4e5F@1B26g7H@" },
            { "SYNE@LT&KEAVANISHT&KEYNEWS@LT&KE", "@5F6g1B2c3D4e7H@" },
            { "S@LT&KEYNEWEYNEABHIMANYUT&KKEYNE", "@e5F6g7H@1B2c3D4" },
            { "S@LT&KENEWS@LT&MANSINEWS@LT&KEYE", "4e5F6g7H@@1B2c3D" },
            { "&KEYNEWS@LT&ASHUTOSHLTS@LT&KEYNE", "@1B25F6c3D4eg7H@" },
            { "SYNEWS@LT&KEYRISHABH&KE@LT&KEYNE", "@1c3D4e5FB26g7H@" }};
        public string MultiCastIP { get; set; }
        public string BkupPath { get; set; }
        public bool MultiCastEnable { get; set; } = false;
        public Dictionary<String, String> LogoTemplatestore = new Dictionary<string, string>();
        double LbandOffset = 0.0;
        bool Htmlshow = false;
        String url = "";
        public MFOverlayHTMLClass m_objOverlayHTML = null;    //MFOverlayHTML plugin
        //--- For CRC Video Black Detection --------

        IntPtr previos_frame = IntPtr.Zero;
        IntPtr current_frame = IntPtr.Zero;

        private bool? lastCrcEqual = null;     // null = first frame
        private int missingFrameCount = 0;
        private bool loggedNotRecovered = false;

        private const int NOT_RECOVERED_FRAMES = 20;

        //----------------- END CRC Video Black Detection --------

        // SCTE added by Anurag 22-4-26


        public static long _Trigger104MessageNumber = 0;
        public static string SCTE104Packet = string.Empty;
        string PrerollTime = string.Empty;
        string TriggerPacket = string.Empty;
        public string BreakDuration = "";
        bool insertscte = false;
        private System.Threading.Timer _scheduledTimer = null;


        public static bool Scte_Splice_ReQuest_Mode = false;
        public static string Scte_Splice_Request_splice_insert_type = "1";  // Possible Values //spliceStart_normal : 1 | spliceStart_immediate : 2 | spliceEnd_normal : 3 | spliceEnd_immediate : 4 | splice_cancel : 5
        public static string Scte_Splice_Request_splice_event_id = "";
        public static string Scte_Splice_Request_unique_program_id = "";
        public static string Scte_Splice_Request_pre_roll_time = "";
        public static string Scte_Splice_Request_break_duration = "";
        public static string Scte_Splice_Request_avail_num = "1";
        public static string Scte_Splice_Request_avails_expected = "3";
        public static string Scte_Splice_Request_auto_return_flag = "1";
        public static string Scte_Splice_Request_DPI_PID_Index = "2";
        public uint spliceEventId;
        public static List<string> furdigit = new List<string>() { "messageSize", "khalihai", "DPI_PID_index", "SCTE35_protocol_version", "timestamp time_type", "opID", "data_length", "unique_program_id", "pre_roll_time", "break_duration", "request opID", "opID", "data_length", "data_length", "segmentation_event_id", "segmentation_event_cancel_indicator", "duration", "segmentation_upid_type", "segmentation_upid_length", "data_length", "request opID", "segmentation_type_id", "segmentation_upid_length", "duration" };
        public static List<string> twodigit = new List<string>() { "protocol_version", "AS_index", "num_ops", "splice_insert_type", "avail_num", "avails_expected", "auto_return_flag", "dtmf_length", "insert_DTMF_descriptor_request_data pre-roll", "device_restrictions", "archive_allowed_flag", "no_regional_blackout_flag", "web_delivery_allowed_flag", "delivery_not_restricted_flag", "duration_extension_frames", "segments_expected", "segments_expected", "segmentation_upid_type", "message_number" };

        // SCTE end by Anurag 22-4-26




        public FrmMainServer()
        {
            InitializeComponent();
           
        }
        public FrmMainServer(string channelname)
        {
            InitializeComponent();
            MFormatsSDKLic.IntializeProtection();
            CharacterGeneratorLic.IntializeProtection();
            ClosedCaptionslibLic.IntializeProtection();
            StreamingPackLic.IntializeProtection();
            ChannelName = channelname;
            DurationBasedAston = false;
            EASFontName = "Arial";
            EASFontSize = 10;
            gFxsqueeze = false;
            CustomMessageBox = new MessageForm(this);
        }
        public void DisposedObjects()
        {
            try
            {
                try
                {
                    if (ReceUdpClient != null) { ReceUdpClient.Close(); }
                    if (ReceiveLTC != null) { ReceiveLTC.Close(); }
                    if (ObjSubtitle != null)
                    {
                        if (ObjSubtitle.ReveiSubtitleSocket != null) { ObjSubtitle.ReveiSubtitleSocket.Close(); }
                    }
                }
                catch
                {
                }
                //releaseComObj(m_objCharGen);
                //releaseComObj(m_objCharGen_Lband);
                //releaseComObj(m_objCharGen_Logo);
                //releaseComObj(myLiveSource);
                //releaseComObj(m_objPreview);

                //setrender.Dispose();
                //setrender = null;
                //outrender.DisposedObjects();
                //outrender.Dispose();
                //outrender = null;
                //load.Dispose();
                //load = null;
                //objpacket = null;
                ////objgrid.Dispose();
                ////objgrid = null;
                //CustomMessageBox.Dispose();
                //CustomMessageBox = null;

                //_dttemplateinfo = null;
                //hidetemplatedt = null;
                //hideabbreviationdt = null;

            }
            catch (Exception)
            { }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }//"format='mpegts' protocol='udp://' video::codec='mpeg2video' video::b='5M' audio::codec='libmp3lame'";
        public bool InitialiseVirtualDevice(string name)
        {
            try
            {
                if (m_objMFSink == null)
                {
                    m_objMFSink = new MFSinkClass();
                    String Nm = ChannelName + "Gfx2Out";
                    m_objMFSink.SinkInit(Nm, "GFX VSource", null);
                    objlog.WriteError("InitialiseVirtualDevice", "Virtual Out put Init done.", ChannelName);

                }

                return true;
            }
            catch (Exception ex)
            {
                objlog.WriteError("InitialiseVirtualDevice", "Virtual Out put Init not done.", ChannelName);
                return false;
            }
        }
        public bool initialiseNetworkWriter()
        {
            string url = NetworkURL;
            string config = CodecId;
            string vbitrate = Vbitrate;
            string configstr = "";
            //  string configstr = "format = 'flv' protocol = '" + StreamType.ToLower() + "://'" + " video::codec = '" + config + "' video::b = '" + vbitrate + "'  audio::codec = 'libmp3lame' audio::ar = '48000'";
            if (string.Equals(StreamType.ToLower(), "udp"))
            {// format='dvb'
             // configstr = "format='mpegts' protocol='udp://' video::codec='mpeg2video' video::b='5M' audio::codec='libmp3lame'";
             //  configstr = "format='mpegts' protocol = '" + StreamType.ToLower() + "://'" + " video::codec = '" + config + "' video::b = '" + vbitrate + "'  audio::codec = 'libmp3lame' audio::ar = '48000'";
                configstr = "format='dvb' protocol = '" + StreamType.ToLower() + "://'" + " video::codec = '" + config + "' video::b = '" + vbitrate + "'  audio::codec = 'libmp3lame' audio::ar = '48000'";
            }
            else if (string.Equals(StreamType.ToLower(), "rtmp"))
            {
                configstr = "format = 'flv' protocol = '" + StreamType.ToLower() + "://'" + " video::codec = '" + config + "' video::b = '" + vbitrate + "'  audio::codec = 'libmp3lame' audio::ar = '48000'";
            }
            else if (string.Equals(StreamType.ToLower(), "srt"))
            {
                //   " format='mpegts' protocol='srt://' video::codec='mpeg2video' video::b='5M' audio::codec='aac'"
                config = "mpeg2video";
                configstr = "format = 'mpegts' protocol = '" + StreamType.ToLower() + "://'" + " video::codec = '" + config + "' video::b = '" + vbitrate + "' audio::codec = 'aac' audio::ar = '48000'";
            }
            //else
            //{
            //    configstr = "format = 'mov' video::codec = 'qtrle' audio::codec = 'null'";
            //}
            NetworkStreamWriter = new PMSLWriter(this, url);
           // NetworkStreamWriter.SetCodec(20, 1, 0);
            NetworkStreamWriter.SetConfiguration(configstr);
            try
            {
             
                NetworkStreamWriter.Setwriter();
                StreamMode = StreamStatus.ready;
                return true;
            }
            catch (Exception ex)
            {
                StreamMode = StreamStatus.stoped;
                objlog.WriteError("FormLoad", "Error while setting Network stream " + ex.ToString(), ChannelName);
                return false;
            }
        }
        public bool closeNetwrkWriter()
        {
            try
            {
                StreamMode = StreamStatus.stoped;
                NetworkStreamWriter.mWriter.WriterClose(1);
                Marshal.ReleaseComObject(NetworkStreamWriter.mWriter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //bool isDonglePerpetual()
        //{
        //    int iResult;
        //    int iRemainingDays = 0;

        //    MultiAppDongleLib ObjMultiAppDongleLib = new MultiAppDongleLib();

        //    //false = without datetimecheck and true = check datetime
        //    iResult = ObjMultiAppDongleLib.InitializeDongleLib(false);
        //    if (iResult == 0)
        //    {
        //        return false;
        //    }

        //    string PatchNameWithPath = $"{Application.StartupPath}\\GFX2.X_License.dll";
        //    if (!File.Exists(PatchNameWithPath))
        //    {
        //        return false;
        //    }


        //    iResult = ObjMultiAppDongleLib.MultiAppPatchApply("APP_1", PatchNameWithPath);
        //    if (iResult == 0)
        //    {
        //        return false;
        //    }

        //    iResult = ObjMultiAppDongleLib.MultiAppDoDongleProtection("APP_1", "GFX2.X", "20.22.6.002", "Noida", out iRemainingDays);

        //    if (iResult == 0)
        //    {
        //        return false;
        //    }

        //    iResult = ObjMultiAppDongleLib.FindDongleWithHID();
        //    if (iResult == 0)
        //    {
        //        return false;
        //    }
        //    string sPerpetual = "";
        //    iResult = ObjMultiAppDongleLib.GetDongleLicenseData(out sPerpetual);
        //    if ((iResult != 0 )&& (sPerpetual.ToLower().Contains("perpetual")))
        //    {
        //        return true;
        //    }
        //    else
        //    return false;
        //}

        //public string CheckDongle()
        //{
        //    int iResult;
        //    int iRemainingDays = 0;
        //    ObjMultiAppDongleLib = new MultiAppDongleLib();

        //    iResult = ObjMultiAppDongleLib.InitializeDongleLib(false);
        //    if (iResult == 0)
        //    {

        //        return "0";
        //        //Environment.Exit(0);
        //    }


        //    string PatchNameWithPath = $"{Application.StartupPath}\\GFX2.X_License.dll";
        //    if (!File.Exists(PatchNameWithPath))
        //    {
        //        iResult = 0;
        //        // MessageBox.Show("Patch file not exist");
        //        return "0";
        //    }


        //    iResult = ObjMultiAppDongleLib.MultiAppPatchApply("APP_1", PatchNameWithPath);
        //    if (iResult == 0)
        //    {
        //        return "0";
        //    }

        //    iResult = ObjMultiAppDongleLib.MultiAppDoDongleProtection("APP_1", "GFX2.X", "20.24.7.1", "Noida", out iRemainingDays);

        //    if (iResult == 0)
        //    {

        //        return "0";
        //    }

        //    iResult = ObjMultiAppDongleLib.FindDongleWithHID();
        //    if (iResult == 0)
        //    {

        //        return "0";
        //    }

        //    if (iResult == 10)
        //    {

        //        return "0";
        //    }
        //    string sPerpetual = "";
        //    iResult = ObjMultiAppDongleLib.GetDongleLicenseData(out sPerpetual);
        //    int val = sPerpetual.CompareTo("TYPE=PERPETUAL");
        //    if (val == 1)
        //    {
        //        return "PERPETUAL";
        //    }
        //    else
        //    {
        //        string sRemainingDays = iRemainingDays.ToString();
        //        return sRemainingDays;
        //    }
        //}
        static string DecimalToBinary(int number)
        {
            if (number == 0) return "0";

            string binary = "";
            while (number > 0)
            {
                binary = (number % 2) + binary;
                number /= 2;
            }
            return binary;
        }
        bool SetProcessAfinity(int no)
        {
            Process currentProcess = Process.GetCurrentProcess();

            // Set affinity to CPU 0 and CPU 1 (binary 00000011 = 3)
            currentProcess.ProcessorAffinity = (IntPtr)0x0000+no;
            return true;
        }

        static string findsigntureID(string keysFile, String fieldtofind)
        {
            HashSet<string> keysToFind = new HashSet<string>(File.ReadAllLines(keysFile));
            // Print matching key-values
            foreach (var key in keysToFind)
            {
                //string[] lineInfo = key.Split(new string[] { "=" }, StringSplitOptions.None);
                //if (lineInfo.Length == 3 && lineInfo[0].Equals(fieldtofind))
                //{
                //    return lineInfo[1] + "=" + lineInfo[2];
                //}
                //if (lineInfo.Length == 2 && lineInfo[0].Equals(fieldtofind))
                //{
                //    return lineInfo[1];
                //}
                if (key.ToString().Contains(fieldtofind))
                {
                    return (key.ToString().Replace(fieldtofind + "=", ""));
                }
            }
            return string.Empty;
        }
        bool Licensethrough(string[] lineInfo)
        {
            if (lineInfo.Length == 8)
            {
                //   if (lineInfo[2].Equals(GetBoardSerialNumbers()))
                string localboard = GetMotherboardSerial();
                if (lineInfo[6].Equals(GetMotherboardSerial()))
                {
                    //   if (lineInfo[3].Trim().Equals("GFX2.X"))
                    //BFEBFBFF000906E9
                  
                    if (lineInfo[2].Equals(GetBoardSerialNumbers())) //GetBoardSerialNumbers
                    {
                        // if (lineInfo[4].Trim().ToUpper().Equals(ChannelName.Trim().ToUpper()))
                        String maxl=  lineInfo[4].Trim();
                        int res = 0;
                        if (Int32.TryParse(maxl, out res))
                        {
                            if (Appinstancepermitble(res))
                            {
                                if (lineInfo[3].Trim().Equals("GFX2.X"))
                                {
                                    // if (lineInfo[7].Equals(GetInstallDate()))
                                    //{
                                    Version version = Assembly.GetExecutingAssembly().GetName().Version;
                                    String[] vars = version.ToString().Split('.');
                                    string twoDigitDay = int.Parse(vars[2]).ToString("D2");
                                    string maj_minar_rev = vars[0] + "." + vars[1] + "." + twoDigitDay;
                                    if (lineInfo[7].Equals(maj_minar_rev))
                                    {
                                        return true;
                                    }
                                    //    else objlog.WriteError("Licensethrough", "Stage 10 error", ChannelName);
                                    //}
                                    else
                                     objlog.WriteError("Licensethrough", "Stage 10 error", ChannelName);
                                }
                                else
                                    objlog.WriteError("Licensethrough", "Stage 7 error", ChannelName);
                            }
                            else
                                objlog.WriteError("Licensethrough", "Stage 6 error", ChannelName);
                        }
                        else
                            objlog.WriteError("Licensethrough", "error in try parse while Licensethrough", ChannelName);
                    }
                    else
                        objlog.WriteError("Licensethrough", "Stage 5 error", ChannelName);
                }
                else
                    objlog.WriteError("Licensethrough","Stage 4 error", ChannelName);
            }
            else
                objlog.WriteError("Licensethrough", "Stage 3 error", ChannelName);
            return false;
        }

        void StartDongleLockProtection()
        {
            //  Please provide patch file for Dongle protection

            if (RockyDongleProtection() == true)   ////  time based license
            {
                //This Timer Call Every 15 Min
                System.Windows.Forms.Timer DongleTimer = new System.Windows.Forms.Timer();
                DongleTimer.Interval = (5 * 60 * 1000); // 5 mins
                DongleTimer.Tick += new EventHandler(DongleTimer_Tick);
                DongleTimer.Start();

                System.Windows.Forms.Timer DongleTimer2 = new System.Windows.Forms.Timer();
                DongleTimer2.Interval = (12 * 60 * 60 * 1000); // 12 hours Times
                DongleTimer2.Tick += new EventHandler(DongleTimer2_Tick);
                DongleTimer2.Start();
            }
            //----------------Dongle perpetual license Code Start----------------

            /*
            string sCheckPerpetual;
            sCheckPerpetual = CheckDongle();
            if (sCheckPerpetual == "PERPETUAL")
            {
                lblLicenseText.Text = "Perpetual" + " License";
                System.Windows.Forms.Timer DongleTimer = new System.Windows.Forms.Timer();
                DongleTimer.Interval = (5 * 60 * 1000); // 5 mins
                DongleTimer.Tick += new EventHandler(DongleTimer_Tick);
                DongleTimer.Start();
            }
            else if (sCheckPerpetual == "0")
            {
                objlog.WriteEvents("Form_Load", "Dongle not found");
                ClosedbyDongle = true;
                Environment.Exit(0);
            }
            else
            {
                string remainingdays = sCheckPerpetual;
                if (Int32.Parse(remainingdays) < 8)
                    lblLicenseText.ForeColor = Color.Red;
                lblLicenseText.Text = $"Expiry : " + LicEndDate + " [" + remainingdays + " Days Left]";
                System.Windows.Forms.Timer DongleTimer = new System.Windows.Forms.Timer();
                DongleTimer.Interval = (5 * 60 * 1000); // 5 mins
                DongleTimer.Tick += new EventHandler(DongleTimer_Tick);
                DongleTimer.Start();
                System.Windows.Forms.Timer DongleTimer2 = new System.Windows.Forms.Timer();
                DongleTimer2.Interval = (12 * 60 * 60 * 1000); // 12 hours Times
                DongleTimer2.Tick += new EventHandler(DongleTimer2_Tick);
                DongleTimer2.Start();
            }
            */
            //----------------Dongle perpetual license Code End----------------
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                objlog = new Loging();
                //---Dongle Protection Code Added By Anurag--

                //  Please provide patch file for Dongle protection

                //Code Added By Anurag 19-5-26

                //     StartDongleLockProtection();

                // Code End By Anurag 19-5-26


                #region   SetMedialooks Registry code

                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFLive"))
                {
                    key.SetValue("no_signal.enabled", "false");
                }

                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFLive\BMD"))
                {
                    key.SetValue("vanc.capture", "true");
                    key.SetValue("vanc.lines", "a");
                }
                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFFactory"))
                {
                    key.SetValue("gpu_pipeline", "true");
                }
                
                #endregion  SetMedialooks Registry code 



                ObjTmpPreviewFrm = new TempPreview(this);
                setrender = new Render(this);
                outrender = new Renderout(this);
                SourceToken = new CancellationTokenSource();
                // aboutfrm = new Aboutus(this);
                m_objOverlayHTML = new MFOverlayHTMLClass();
                AssigningConfigXmlPath();
                CreatingDifferentObjects();
                LoadTemplateConfigration();

#if UTC_MODE
  objlog.WriteEvents("Form_Load", $"{Environment.NewLine}********************Now {ChannelName} Channel Start with {Application.ProductName}  Ver-{Application.ProductVersion} Time is {DateTime.UtcNow.ToString()} ********************", ChannelName);
#else
                objlog.WriteEvents("Form_Load", $"{Environment.NewLine}********************Now {ChannelName} Channel Start with {Application.ProductName}  Ver-{Application.ProductVersion} Time is {DateTime.Now.ToString()} ********************", ChannelName);
#endif
                ReadConfig(ChannelName);
                //if (File.Exists("LicenseKey.md"))

                #region Start Software License Code
                /*
                string licensePath = Path.Combine("C:\\Program Files\\Planetcast", "LicenseKey.md"); ////Added By Tushar Pandey for updated path of LicenseKey on 2025-06-17 at 10:23
                if (File.Exists(licensePath)) //Added By Tushar Pandey for updated path of LicenseKey on 2025-06-16 at 16:06
                {
                    string licKey = findsigntureID(licensePath, "License.SignatureID");

                    if (!String.IsNullOrEmpty(licKey))
                    {
                        string declicKey = Decryption(licKey);
                        if (declicKey != "")
                        {
                            string[] lineInfo = declicKey.Split(new string[] { "||" }, StringSplitOptions.None);
                            if (!Licensethrough(lineInfo))
                            {
                                //  MessageBox.Show("Wrong Licence");
                                string path = AutoGenerateSystemKey();
                                objlog.WriteError("Licensethrough", "wrong license", ChannelName);
                                objlog.WriteError("Licensethrough", $"Please generate the License using the System Key at {path}", ChannelName);
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            string path = AutoGenerateSystemKey();
                            objlog.WriteError("Licensethrough", "Stage 2 error", ChannelName);
                            objlog.WriteError("Licensethrough", $"Please generate the License using the System Key at {path}", ChannelName);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        string path = AutoGenerateSystemKey();
                        objlog.WriteError("Form_load", $"License key empty: Please generate the License using the System Key at {path}", ChannelName);
                        Environment.Exit(0);

                    }
                }
                else
                {
                    string path = AutoGenerateSystemKey();
                    objlog.WriteError("Licensethrough", "Stage 1 error", ChannelName);
                    objlog.WriteError("Licensethrough", $"Please generate the License using the System Key at {path}", ChannelName);
                    Environment.Exit(0);
                }
                  */
                #endregion Stop Software License Code


                load = new manualgraphicsload(this);
                if (ScteEnable == true)
                {
                    ObjScte = new ScteUserControl(ScteIP, SctePort, SctePreRoll, ChannelName, objlog);
                    ObjScte.Anchor = AnchorStyles.Bottom;
                    pnlScte.Controls.Add(ObjScte);
                }
                if (NetworkMode.ToUpper() == "TRUE")
                {
                    initialiseNetworkWriter();
                }
                else { StreamMode = StreamStatus.stoped; }

                String Urlfile = $"{Application.StartupPath}\\URL.txt";
                if (File.Exists(Urlfile))
                {
                    url = File.ReadAllText(Urlfile);
                    //  m_objOverlayHTML = new MFOverlayHTMLClass();
                    //  m_objOverlayHTML.BrowserPageLoad(url);
                    // m_objOverlayHTML.BrowserClose();
                }
                Fetchtemplatedata();
                DirectoryInfo(TemplatePath);
                CreateTemplate();
                AppConfigPath = $"{ChannelNamepath}\\{"AppConfig.xml"}";
                if (!File.Exists(AppConfigPath))
                {
                    CreateAppConfigXml(AppConfigPath);
                }
                SetGridControl("");
                m_objCharGenTemp = new CoMLCharGenClass();
                m_objCharGen_Logo = new CoMLCharGenClass();
                m_objCharGen = new CoMLCharGenClass();
                m_objPreview = new MFPreviewClass();
             //   m_objPreviewTemp = new MFPreviewClass();
                CustumOperation = new CustomTrigger(this);
                m_objCharGen.OnCGEvent += new IMLCharGenEvents_OnCGEventEventHandler(m_objCG_OnCGEvent);              
                m_objCharGen.OnEndOfClip += M_objCharGen_OnEndOfClip;
                m_objCharGen_Lband = new CoMLCharGenClass();
             //   m_objCharGen_Lband.OnEndOfClip += M_objSqz_OnEndOfClip;
                ThreadAndTaskAssignment();
                UpdateTemplateThread();
                if (PreviewWindow == false)
                {
                    IsShow = false;
                    m_objPreview.PreviewEnable("", 0, 0);
                    lblpreviewmessage.Visible = true;
                }
                else
                {
                    m_objPreview.PreviewEnable("", 0, 1);
                    lblpreviewmessage.Visible = false;
                    IsShow = true;
                }

                AutoLogoPlay();
                //Chargen object
                //lblCopyRight.Text = $"{Application.ProductName}  Ver-{Application.ProductVersion} Copyright ©{DateTime.Today.Year} Planetcast Media Services Limited  ";
                lblCopyRight.Text = $"{Application.ProductName}  Ver-{Application.ProductVersion} Copyright ©{DateTime.Today.Year}";

#if UTC_MODE
 lbluptime.Text = "UpTime: " + DateTime.UtcNow.ToString("dd-MM-yyyy | HH:mm:ss");
#else
                lbluptime.Text = "UpTime: " + DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss");
#endif



                ChannelTypelbl.Text = ChannelType;
                objPmslReader = new PMSLReader(this);
                if (SubtitleEnable == true)
                {
                    ObjSubtitle = new UCSubtitle(this);
                    ObjSubtitle.Anchor = AnchorStyles.Left;
                    ObjSubtitle.Dock = DockStyle.Fill;
                    pnlScte.Controls.Add(ObjSubtitle);
                }

            }
            catch (Exception ex)
            {
                objlog.WriteError("SecondaryFormLoad", "Error on Form Load " + ex.ToString(), ChannelName);
            }
        }
        private string AutoGenerateSystemKey()
        {
            try
            {
                string path = $"{Application.StartupPath}\\SystemKey";
                string filePath = Path.Combine($"{Application.StartupPath}\\SystemKey", $"{Environment.MachineName.ToString().Trim()}-SystemKey.md");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                string key = SystemKeyGenerator.GenerateKey();
                if (key != null)
                {
                    File.WriteAllText(filePath, key);
                    objlog.WriteError("AutoGenerateSystemKey", "System key generated successfully! ", ChannelName);
                    return filePath;
                }
                else
                {
                    objlog.WriteError("AutoGenerateSystemKey", "Unable to generate system key! ", ChannelName);
                    return null;
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("AutoGenerateSystemKey", "Error while generatign system key! " + ex.ToString(), ChannelName);
                return null;
            }
        }

        private bool Appinstancepermitble(int maxlimit)
        {
            Process[] processes = Process.GetProcesses();
            int count = 0;
            // Loop through each process
            foreach (var process in processes)
            {
                // Check if the process name starts with "GFX2."
             //   if(process.ProcessName== "GFX2.X")
                if (process.ProcessName.StartsWith("GFX2.X", StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            if ((count >= 0) && (count < (maxlimit + 1)))
            {
                return true;
            }
         //   MessageBox.Show("Exceeding pemissible max instances");
            return false;
        }
      
      
        public static string Decryption(string Text_To_Decrypt)
        {
            string DecryptedText = "";
            try
            {
                //Get the key paire Index
                int KeyIVindex = Convert.ToInt16(Text_To_Decrypt[Text_To_Decrypt.Length - 1]) - '9';
                //Get the KEY/IV paire from the list
                var Key_IV = KeyIV.ElementAt(KeyIVindex);
                //Decrypted the text based on key value paire
                Text_To_Decrypt = Text_To_Decrypt.Substring(0, Text_To_Decrypt.Length - 1);
                string DecryptedTextByte = null;
                using (AesManaged ObjAesManaged = new AesManaged())
                {
                    ICryptoTransform Decryptor = ObjAesManaged.CreateDecryptor(Encoding.ASCII.GetBytes(Key_IV.Key), Encoding.ASCII.GetBytes(Key_IV.Value));
                    using (MemoryStream ObjMemoryStream = new MemoryStream(Convert.FromBase64String(Text_To_Decrypt)))
                    {
                        using (CryptoStream ObjCryptoStream = new CryptoStream(ObjMemoryStream, Decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(ObjCryptoStream))
                                DecryptedTextByte = reader.ReadToEnd();
                        }
                    }
                }
                //Get the Key Length
                int KeyLength = (int)Char.GetNumericValue(DecryptedTextByte[DecryptedTextByte.Length - 1]);
                //Decrypt Text based on Ascii code Key 
                DecryptedText = string.Concat(DecryptedTextByte.Take(DecryptedTextByte.Length - 1).Select(b => (char)(b - KeyLength)));
                //Reverse the string and remove the key
                DecryptedText = new string(DecryptedText.ToCharArray().Select((c, i) => DecryptedText[DecryptedText.Length - i - 1]).ToArray());
                return DecryptedText;
            }
            catch
            {
                DecryptedText = "";
                return DecryptedText;
            }
        }
        private static string GetMotherboardSerial()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        return mo["SerialNumber"]?.ToString().Trim();
                    }
                }
            }
            catch
            {
            }
            return string.Empty;
        }
        private static string GetBoardSerialNumbers()
        {
            string BoardSerialNumber = "";
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"Get-WmiObject Win32_Processor | Select-Object -ExpandProperty ProcessorId\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processStartInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    BoardSerialNumber = reader.ReadToEnd().Trim();
                }
            }
            return BoardSerialNumber.ToString();
        }
        //Get the MAC Address // Format : "004E019D309E"
        private static string GetMacAddress()
        {
            string MacAdrress = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    MacAdrress += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return MacAdrress;
        }

        private static string GetProductID()
        {
            string ProductID = "";
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"(Get-WmiObject Win32_OperatingSystem).SerialNumber\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processStartInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    ProductID = reader.ReadToEnd().Trim();
                }
            }
            return ProductID;
        }

        private static string GetInstallDate()
        {
            string SystemInstallDate = "";
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"Get-CimInstance Win32_OperatingSystem | Select-Object -ExpandProperty InstallDate\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processStartInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    string installDateString = reader.ReadToEnd().Trim();

                    if (!string.IsNullOrEmpty(installDateString))
                    {
                        // Parse and format the date
                        DateTime installDate = DateTime.Parse(installDateString);
                        SystemInstallDate = installDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }

            return SystemInstallDate;
        }
        private void M_objCharGen_OnEndOfClip(string bsItemID)
        {
            if (DurationBasedAston)
            {
                Dictionary<string, string> entries = new Dictionary<string, string>();

                String mastersheetPath = TemplatePath + "\\mastersheet.txt";
                if (File.Exists(mastersheetPath))
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(mastersheetPath);
                        foreach (string line in lines)
                            entries.Add(line.Split(',')[0], line.Split(',')[1]);
                        String composition = "";
                        if ((entries.Count>=1) && entries.ContainsKey(bsItemID))
                        {
                            string templatename = entries[bsItemID];
                            templatename = templatename.Trim();
                            DataRow DrTempInfo = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault();
                            string BugType = DrTempInfo["BugType"] + "";
                            objlog.WriteEvents("EndOfClip", $"going to Kill template={templatename}on file complete id={bsItemID}", ChannelName);


                            // UsedItems.Remove(ab);
                            lock (objreceive)
                            {
                                int dur = 0;
                                string prop = "";
                                m_objCharGen.GetPlayingFrame(bsItemID, out dur, out prop);
                                if (dur > 0)
                                    prop = GetTimeFromFrame(dur + 1);
                                try
                                {
                                    // DataRow Dr = objAgent.DtAsRun.Select($"AsRunTemplateName='{templatename.Trim()}'").FirstOrDefault();
                                    // Dr["AsRunDuration"] = prop;
                                    if (ASRunEnable)
                                    {
                                        int recordno = 0;
                                        bool foundflag = false;
                                        foreach (DataRow row in objAgent.DtAsRun.Rows)
                                        {
                                            if (row["AsRunTemplateName"].ToString() == templatename.Trim())
                                            {
                                                objAgent.DtAsRun.Rows[recordno]["AsRunDuration"] = prop;
                                                foundflag = true;
                                                break;
                                            }
                                            recordno++;
                                        }
                                        if (foundflag)
                                            objAgent.PrepareAsRunSetting("STOPKILL", "", "", "", templatename.Trim());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    objlog.WriteError("EndOfClip", $"template={templatename}during file complete" + ex.Message, ChannelName);
                                    return;
                                }
                                if (UsedItems.Contains(templatename))
                                {
                                    KillCgItem(templatename, BugType);
                                    objlog.WriteEvents("EndOfClip", $"Killed template={templatename}on file complete", ChannelName);

                                    //////////////////////////////////////////////////////update time for last template///////////////////////
                                    objAgent.updateTemplateOperationTime(templatename);

                                    ////////////////////////////////////////////////////////end/////////////////////////////////////////////////


                                    String che = "GFXClient||" + ChannelName + "||SUCCESS||" + templatename + "||" + "STOP" + "||" + "" + "||" + prop;
                                    if (!String.IsNullOrEmpty(GfxclientIp.Trim()))
                                    {
                                        SendUdpResponse(GfxclientIp, GfxclientPort, che);
                                        //   ObjLog.WriteEvents("ExecuteOpration", "Sent to client-->" + che, ObjMain.ChannelName);
                                    }
                                }
                                    }
                            objlog.WriteEvents("Endofclip", $"before remove from grid", ChannelName);
                           // lock (m_objGridLock)
                                {
                                    RemoveRowInGrid(templatename);
                                }
                            objlog.WriteEvents("Endofclip", $"After remove from grid", ChannelName);
                            // }
                        }
                    }
                    catch(Exception ex)
                    {
                        objlog.WriteError("M_objCharGen_OnEndOfClip", "Error in auto endof clip" + ex.ToString(), ChannelName);
                    }
                }
                //  throw new NotImplementedException();
            }
        }
        


        private void RemoveRowInGrid(String TemplateName)
        {
            try
            {
                foreach (DataGridViewRow row in dgvsecevent.Rows)
                {
                    if (row.Cells["SecTemplate"].Value.ToString().ToUpper() == TemplateName.ToUpper())
                    {
                        dgvsecevent.Invoke((MethodInvoker)(() =>
                        {
                            try
                            {
                                dgvsecevent.Rows.RemoveAt(row.Index);
                                dgvsecevent.ClearSelection();
                                dgvsecevent.Refresh();
                            }
                            catch { }
                        }));
                        //   ObjLog.WriteEvents("RowsCheckOnFireEvent", $"templatename={TemplateName} remove in Grid .", ObjMain.ChannelName);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                //  ObjLog.WriteError("RemoveRowInGrid", $"Error In Remove Row in Grid ,template name {TemplateName} ...    error-- " + ex.ToString(), ObjMain.ChannelName);
            }
        }
        private void ThreadAndTaskAssignment()
        {
            CheckForIllegalCrossThreadCalls = false;
            if (ExternalAgentMode == true)
            {
                objAgent = new AgentMode(this);
            }

            //UDP calling
            Thread thread = new Thread(StartUDPReceiver);
            thread.IsBackground = true;
            thread.Start();

            //Main task calling
            Task.Run(() =>
            {
                try
                {
                    DoWork();
                }
                catch { }
            });
            Task.Run(() =>
            {
                try
                {
                    if (ExternalAgentMode == false)
                    {
                        Secondaryevents();
                    }

                }
                catch { }

            });
            Task.Run(() =>
            {
                try
                {
                    if (KeyingMode == false)
                    {
                        SourceAutoStart();
                        keryerToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        SourceAutoStart();
                        renderToolStripMenuItem.Visible = true;
                        keryerToolStripMenuItem.Visible = true;
                        m_bWork = true;
                    }
                    Thread.Sleep(300);
                    RenderAutoStart();
                }
                catch { }

            });

            Task.Run(async () =>
           {
               if (KeyingMode == true && KeyerHeartBeat == true)
               {
                   while (true)
                   {
                       try
                       {
                           SendUDPPacket("g");
                           await Task.Delay(100);
                       }
                       catch { }
                   }
               }
           });



        }

        public void UpdateTemplateThread()
        {
            Thread threadTemplate = new Thread(CheckIfModificationInTemplates);
            threadTemplate.IsBackground = true;
            threadTemplate.Start();
        }
        private void CreatingDifferentObjects()
        {

            m_objLock = new object();
            m_objGridLock = new object();
            objwrite = new object();

        }

        private void AssigningConfigXmlPath()
        {

            MainXmlConfig = $"{Application.StartupPath}\\ChannelConfigs\\Channel_AppConfig.xml";
            ChannelNamepath = $"{Application.StartupPath}\\Channels\\{ChannelName}";
            string GraphicsXMLDirectory = $"{ChannelNamepath}\\{ChannelName}";
            XMLGridFormat = $"{GraphicsXMLDirectory}_GridFormat.xml";
            XMLHideAbbreviation = $"{GraphicsXMLDirectory}_HideAbbreviation.xml";
            XMLHideTemplateInfo = $"{GraphicsXMLDirectory}_HideTemplateInfo.xml";
            XMLBreakFreeBugInfo = $"{GraphicsXMLDirectory}_BreakFreeBugInfo.xml";
            XMLTemplateConfigfilepath = $"{GraphicsXMLDirectory}_TemplateInfo.xml";
            XMLSubtitleInfo = $"{GraphicsXMLDirectory}_SubtitleInfo.xml";
            FreezTempPath = $"{GraphicsXMLDirectory}_FreezeTemplate.xml";
            SubtitleConfigPath = $"{GraphicsXMLDirectory}_SubtitleInfo.xml";
            AppConfigPath = $"{GraphicsXMLDirectory}_AppConfig.xml";
        }

        private void SourceAutoStart()
        {
            try
            {
                string SourceType = "";
                string SourceDeviceName = "";
                string SourceFile = $"{Application.StartupPath}\\Channels\\{ChannelName}\\SourceName.txt";
                if (!File.Exists(SourceFile))
                {
                    objlog.WriteEvents("SourceAutoStart", $"SourceName.txt file is not found so automatic source is not initialize .", ChannelName);
                    return;
                }
                using (FileStream fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string content = sr.ReadToEnd();
                        string[] arrcontent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string data in arrcontent)
                        {
                            string[] arrdata = data.Split('=');
                            switch (arrdata[0])
                            {
                                case "Source Type":
                                    SourceType = arrdata[1];
                                    break;
                                case "Source Name":
                                    SourceDeviceName = arrdata[1];
                                    break;
                            }
                        }
                    }
                }
                if (myLiveSource == null)
                {
                    myLiveSource = new MFLiveClass();
                    Thread.Sleep(100);
                }
                //if (myLiveSource1 == null)
                //{
                //    myLiveSource1 = new MFLiveClass();
                //    Thread.Sleep(100);
                //}
                objlog.WriteEvents("SourceAutoStart", $"Reading Previous Source Type={SourceType},  Source Name={SourceDeviceName} going to auto Start..", ChannelName);
                if (SourceType == "NDI")
                {
                    int CounterForFindSource = 4;
                    setrender.cmbSourcetype.Text = SourceType;
                    bool DeviceFound = setrender.ListNDIDevice.AsEnumerable().Any(e => e.NdiDeviceOptionsName == SourceDeviceName);
                    if (DeviceFound == true)
                    {
                        setrender.VidLineComboBox.Text = SourceDeviceName;
                        objlog.WriteEvents("SourceAutoStart", $"Source Type={SourceType} in VideoLine Source Name={SourceDeviceName} is Set for capture video source.", ChannelName);
                        m_bWork = true;
                    }
                    while (DeviceFound == false && CounterForFindSource > -1)
                    {
                        CounterForFindSource--;
                        setrender.SearchNdiDeviceLineName(-1);
                        DeviceFound = setrender.ListNDIDevice.AsEnumerable().Any(e => e.NdiDeviceOptionsName == SourceDeviceName);
                        if (DeviceFound)
                        {
                            setrender.VidLineComboBox.Text = SourceDeviceName;
                            objlog.WriteEvents("SourceAutoStart", $"Source Type={SourceType} in VideoLine Source Name={SourceDeviceName} is Set for capture video source.", ChannelName);
                            m_bWork = true;
                            break;
                        }
                    }
                    if (DeviceFound == false)
                    {
                        objlog.WriteEvents("SourceAutoStart", $"Source Type={SourceType} in VideoLine Source Name={SourceDeviceName} is not found kindely set Input as Manually.", ChannelName);
                    }
                }
                else if (SourceType == "SDI")
                {
                    setrender.cmbSourcetype.Text = SourceType;
                    setrender.VidLineComboBox.Text = SourceDeviceName;
                    setrender.SetDeviceInput("SDI", SourceDeviceName);
                }else if(SourceType == "VIRTUAL")
                {
                    setrender.cmbSourcetype.Text = SourceType;
                    setrender.VidLineComboBox.Text = SourceDeviceName;
                    setrender.SetDeviceInput("VIRTUAL", SourceDeviceName);
                }
                else
                {
                    objlog.WriteEvents("SourceAutoStart", $"Source Type NDI/SDI Not Match,curent SourceType={SourceType} .", ChannelName);
                }
            }
            catch { }
        }
        private void RenderAutoStart()
        {

            string SourceType = "";
            string SourceName = "";
            try
            {
                string RenderFile = $"{Application.StartupPath}\\Channels\\{ChannelName}\\RenderOut.txt";
                if (!File.Exists(RenderFile))
                {
                    objlog.WriteEvents("RenderAutoStart", $"File Not Found For Set Auto Render Out FileName= {RenderFile} ", ChannelName);
                    return;
                }
                using (FileStream fs = new FileStream(RenderFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string content = sr.ReadToEnd();
                        string[] arrcontent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string data in arrcontent)
                        {
                            string[] arrdata = data.Split('=');
                            switch (arrdata[0])
                            {
                                case "Source Type":
                                    SourceType = arrdata[1];
                                    break;

                                case "Source Name":
                                    SourceName = arrdata[1];
                                    break;
                            }
                        }
                    }
                }
                if (SourceType.ToUpper() == "SDI" || SourceType.ToUpper() == "NDI" || SourceType.ToUpper() == "VIRTUAL")
                {
                    outrender.RendertypeName = SourceType;
                    outrender.renderoutsrccmbbox.Text = SourceType;
                    outrender.renderoutcombobox.Text = SourceName;
                    if (outrender.OutputResponse == true)
                    {
                        ISNDI = true;
                        outrender.srcoutchkbox.Checked = true;
                    }
                }
                //else if(SourceType.ToUpper() == "SDI")
                //{
            
                //}
            }
            catch (Exception ex)
            { }
        }


        private void AutoLogoPlay()
        {
            try
            {
                string _logoConfigPath = Application.StartupPath + $"//Channels//{ChannelName}//{ChannelName}_FreezeTemplate.xml";
                if (File.Exists(_logoConfigPath))
                {
                    DataSet _ds = new DataSet();
                    _ds.ReadXml(_logoConfigPath);
                    string _TemplateName = _ds.Tables["FreezeTemplate"].Rows[0]["TemplateName"] + "";
                    string _TemplateID = _ds.Tables["FreezeTemplate"].Rows[0]["param1ID"] + "";
                    _ds.Dispose();
                    Thread.Sleep(1000);
                    load.InsertControlForFreeze(_TemplateName, _TemplateID);
                    objlog.WriteEvents("AutoLogoPlay", $"Playing Automatic Logo Last Logo Template {_TemplateName} ,and parameter value {_TemplateID}. ", ChannelName);
                }

            }
            catch (Exception ex)
            {
                objlog.WriteError("AutoLogoPlay", "Error on Play Auto Logo" + ex.ToString(), ChannelName);

            }
        }

        private void m_objCG_OnCGEvent(string bsItemOrCompositionID, string bsEventType, string bsEventParam)
        {
            try
            {
#if UTC_MODE
        FillEventLog($"{bsItemOrCompositionID} {bsEventType} {bsEventParam} {DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff")}", "M");
#else
                FillEventLog($"{bsItemOrCompositionID} {bsEventType} {bsEventParam} {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff")}", "M");
#endif
            }
            catch
            { }
        }
        #region File Read and Write             
        #region Config Methods         
        private void LoadTemplateConfigration()
        {   //To Load data from XmlFiles
            try
            {
                int paramcount = 0;
                if (File.Exists(XMLTemplateConfigfilepath))
                {
                    MaxParamFinder(XMLTemplateConfigfilepath, out paramcount);
                    CreateTemplateInfoStruture(paramcount);
                }
                if (Directory.Exists(ChannelNamepath))
                {
                    if (File.Exists(XMLTemplateConfigfilepath)) 
                    {
                        DataSet _ds = new DataSet();
                        _ds.ReadXml(XMLTemplateConfigfilepath);
                        if (_ds.Tables["ConfigData"] != null)
                        {
                            _dttemplateinfo.Clear();
                            foreach (DataRow item in _ds.Tables["ConfigData"].Rows)
                            {
                                _dttemplateinfo.ImportRow(item);
                            }


                        }
                        objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_TemplateConfig.xml file file read successfully", ChannelName);
                        FillEventLog("XMLTemplateConfigfilepath file read successfully", "G");
                    }
                    else
                    {

                        _dttemplateinfo.Rows.Add("1", "Logo", "1", ".mov", ChannelName, "Video", "false", "false", "false", "logo~VIDEO", "", "", "", "", "", "", "", "", "", "False", "", "", "", "0.5", "300", "False", "", "");
                        _dttemplateinfo.WriteXml(XMLTemplateConfigfilepath, XmlWriteMode.WriteSchema);
                    }
                    if (File.Exists(XMLHideTemplateInfo))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(XMLHideTemplateInfo);
                        hidetemplatedt = new DataTable();
                        hidetemplatedt = ds.Tables["HideTemplateData"];
                        objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_HideTemplateInfo.xml file read successfully.", ChannelName);
                        FillEventLog($"{ChannelName}_HideTemplateInfo.xml file read successfully.", "G");
                    }
                    if (File.Exists(XMLHideAbbreviation))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(XMLHideAbbreviation);
                        hideabbreviationdt = new DataTable();
                        hideabbreviationdt = ds.Tables["HideAbbreviationData"];
                        objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_HideAbbreviation.xml file read successfully.", ChannelName);
                        FillEventLog($"{ChannelName}_HideAbbreviation.xml file read successfully.", "G");
                    }
                    if (File.Exists(XMLBreakFreeBugInfo))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml($"{ChannelNamepath}\\{ChannelName}_BreakFreeBugInfo.xml");
                        breakfreetemplatedt = new DataTable();
                        breakfreetemplatedt = ds.Tables["BreakFreeTemplateData"];
                        objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_BreakFreeBugInfo.xml file read successfully", ChannelName);
                        FillEventLog($"{ChannelName}_BreakFreeBugInfo.xml file read successfully", "G");
                    }
                    if (File.Exists(XMLSubtitleInfo))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(XMLSubtitleInfo);
                        XMLSubtitleInfodt = new DataTable();
                        if (ds.Tables["ConfigData"] != null)
                        {
                            XMLSubtitleInfodt = (ds.Tables["ConfigData"].Copy());
                            FillSubtitleInfo();
                        }

                        objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_SubtitleInfo.xml file file read successfully", ChannelName);
                        FillEventLog("XMLTemplateConfigfilepath file read successfully", "G");
                    }
                  
                }
                else
                {
                    CreateChannelDirectory();
                }

            }
            catch (Exception ex)
            {
                objlog.WriteError("LoadTemplateConfigration", "Checktemplate " + ChannelName + "_XMLFILES.xml" + ex.ToString(), ChannelName);
                FillEventLog("Checktemplate " + ChannelName + "_XMLFILES.xml", "E");
            }
        }
        public void MaxParamFinder(String path, out int p)
        {

            String str = "";
            int x = 0;
            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if ("xs:element" == reader.Name.ToString() && (reader.GetAttribute(0) != "NewDataSet" && reader.GetAttribute(0) != "ConfigData"))
                        {
                            try
                            {
                                String Arr = reader.GetAttribute(0);
                                if (Arr.Contains("Param"))
                                {
                                    str = reader.GetAttribute(0);
                                    str = str.Substring(5);
                                    if (x < int.Parse(str))
                                        x = int.Parse(str);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        reader.Read();
                        continue;
                    }

                }
            }
            p = x;
        }
        private void FillSubtitleInfo()
        {
            try
            {
                lstData.Clear();
                foreach (DataRow item in XMLSubtitleInfodt.Rows)
                {
                    ScreenDataMapping obj = new ScreenDataMapping();
                    obj.ReceiveText = item["ReceiveText"] + "";
                    obj.ScreenPosition = item["ScreenPosition"] + "";
                    obj.ItemPosition = item["ItemPosition"] + "";
                    obj.left = item["left"] + "";
                    obj.right = item["right"] + "";
                    obj.top = item["top"] + "";
                    obj.bottom = item["bottom"] + "";
                    obj.useforbg = item["useforbg"] + "";
                    lstData.Add(obj);
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void CreateTemplateInfoStruture(int count = 0)
        {
            try
            {
                if (_dttemplateinfo == null)
                {
                    _dttemplateinfo = new DataTable("ConfigData");
                    _dttemplateinfo.Columns.Add("SrNo", typeof(int));
                    _dttemplateinfo.Columns.Add("TemplateName");
                    _dttemplateinfo.Columns.Add("LayerNo");
                    _dttemplateinfo.Columns.Add("FileExtension");
                    _dttemplateinfo.Columns.Add("ChannelName");
                    _dttemplateinfo.Columns.Add("BugType");
                    _dttemplateinfo.Columns.Add("KillwithPrimary");
                    _dttemplateinfo.Columns.Add("ShowOnBreak");
                    _dttemplateinfo.Columns.Add("BugStatus");
                    if (count > 0)
                    {
                        String a;
                        for (int j = 1; j <= count; j++)
                        {
                            a = j.ToString();
                            a = "Param" + a;
                            _dttemplateinfo.Columns.Add(a);
                        }
                    }
                    else
                    {
                        _dttemplateinfo.Columns.Add("Param1");
                        _dttemplateinfo.Columns.Add("Param2");
                        _dttemplateinfo.Columns.Add("Param3");
                        _dttemplateinfo.Columns.Add("Param4");
                        _dttemplateinfo.Columns.Add("Param5");
                        _dttemplateinfo.Columns.Add("Param6");
                        _dttemplateinfo.Columns.Add("Param7");
                        _dttemplateinfo.Columns.Add("Param8");
                        _dttemplateinfo.Columns.Add("Param9");
                        _dttemplateinfo.Columns.Add("Param10");
                    }
                    _dttemplateinfo.Columns.Add("DBTemplate");
                    _dttemplateinfo.Columns.Add("DBTableName");
                    _dttemplateinfo.Columns.Add("DBColumnName");
                    _dttemplateinfo.Columns.Add("DBCondition");
                    _dttemplateinfo.Columns.Add("IntroTime");
                    _dttemplateinfo.Columns.Add("ExitTime");
                    _dttemplateinfo.Columns.Add("DurationBased");
                    _dttemplateinfo.Columns.Add("EndOffset");
                    _dttemplateinfo.Columns.Add("StartOffset");

                }

            }
            catch (Exception)
            {
                objlog.WriteError("CreateTemplateInfoStruture", $"Error in creating template info structure.", ChannelName);
            }
        }
        private void CreateChannelDirectory()
        {
            try
            {
                Directory.CreateDirectory(ChannelNamepath);
                {
                    //Copy Files from ChannelConfigs to Channel Folder
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_GridFormat.xml", XMLGridFormat);
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_HideAbbreviation.xml", XMLHideAbbreviation);
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_HideTemplateInfo.xml", XMLHideTemplateInfo);
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_BreakFreeBugInfo.xml", XMLBreakFreeBugInfo);
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_TemplateInfo.xml", XMLTemplateConfigfilepath);///Added by shailendra 06.05FreezTempPath
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_FreezeTemplate.xml", FreezTempPath);//SubtitlePath
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_SubtitleInfo.xml", SubtitleConfigPath);
                    File.Copy($"{Application.StartupPath}\\ChannelConfigs\\Channel_AppConfig.xml",AppConfigPath);
                    //Filling different datatables from different XML files
                    if (File.Exists(XMLHideTemplateInfo))
                    {
                        DataSet _ds = new DataSet();
                        _ds.ReadXml(XMLHideTemplateInfo);
                        hidetemplatedt = new DataTable();
                        hidetemplatedt = _ds.Tables["HideTemplateData"];
                    }
                    if (File.Exists(XMLHideAbbreviation))
                    {
                        DataSet _ds = new DataSet();
                        _ds.ReadXml(XMLHideAbbreviation);
                        hideabbreviationdt = new DataTable();
                        hideabbreviationdt = _ds.Tables["HideAbbreviationData"];
                    }
                    if (File.Exists(XMLBreakFreeBugInfo))
                    {
                        DataSet _ds = new DataSet();
                        _ds.ReadXml(XMLBreakFreeBugInfo);
                        breakfreetemplatedt = new DataTable();
                        breakfreetemplatedt = _ds.Tables["BreakFreeTemplateData"];
                    }
                    //////////////////////////////////Code Added by Shailendra/////////////////////////////////// 06.05
                    if (File.Exists(XMLTemplateConfigfilepath))
                    {
                        //DataSet _ds = new DataSet();
                        //_ds.ReadXml(XMLTemplateConfigfilepath);
                        //DataTable XMLTemplatedt = new DataTable();

                        //if (_ds.Tables["ConfigData"] != null)
                        //{
                        //   // XMLTemplatedt = (_ds.Tables["ConfigData"].Copy());
                        //   _ds.Tables["ConfigData"].Rows[0].SetField<String>("ChannelName", ChannelName);
                        //    _ds.WriteXml($"{Application.StartupPath}\\Channels\\{ChannelName}\\{ChannelName}_TemplateInfo.xml");

                        //    objlog.WriteEvents("LoadTemplateConfigration", $"{ChannelName}_TemplateInfo.xml file read successfully", ChannelName);
                        //    FillEventLog($"{ChannelName}_TemplateInfo.xml file read successfully", "G");
                        //}
                        string TemplateConfigXML = File.ReadAllText(XMLTemplateConfigfilepath);

                        if (!string.IsNullOrEmpty(TemplateConfigXML))
                        {

                            string updatedXML = TemplateConfigXML.Replace("<ChannelName>Channel</ChannelName>", $"<ChannelName>{ChannelName}</ChannelName>");

                            if (!string.IsNullOrEmpty(updatedXML))
                            {
                                File.WriteAllText(XMLTemplateConfigfilepath, updatedXML);
                            }

                        }

                    }

                    /////////////////////////////////////////////////end////////////////////////////////////////////////////

                    //Create Directories inside a Channel Folder
                    Directory.CreateDirectory(ChannelNamepath + @"\Template");
                    Directory.CreateDirectory(ChannelNamepath + @"\Content");
                    Directory.CreateDirectory(ChannelNamepath + @"\Content\ANIMATION");
                    Directory.CreateDirectory(ChannelNamepath + @"\Content\IMAGE");
                    Directory.CreateDirectory(ChannelNamepath + @"\Content\FILE");
                    Directory.CreateDirectory(ChannelNamepath + @"\Subtitle");

                    //Setting up the config for the particular Channel
                    //if (File.Exists(AppConfigPath))
                        DataSet ds = new DataSet();
                    ds.ReadXml(AppConfigPath);
                    DataTable dstable = new DataTable();
                    
                    dstable = ds.Tables["ConfigData"];
                    dstable.Rows[0].SetField<string>("ChannelName", ChannelName);
                    //dstable.Rows[0].SetField<string>("TemplatePath", $@"{Application.StartupPath}\Channels\{ChannelName}\Template");
                    //dstable.Rows[0].SetField<string>("ContentPath", $@"{Application.StartupPath}\Channels\{ChannelName}\Content");
                    //dstable.Rows[0].SetField<string>("SubtitlePath", $@"{Application.StartupPath}\Channels\{ChannelName}\Subtitle");


                    dstable.Rows[0].SetField<string>("TemplatePath", "");
                    dstable.Rows[0].SetField<string>("ContentPath", "");
                    dstable.Rows[0].SetField<string>("SubtitlePath", "");


                    // dstable.WriteXml($"{Application.StartupPath}\\Channels\\{ChannelName}\\AppConfig.xml");
                    ds.WriteXml($"{Application.StartupPath}\\Channels\\{ChannelName}\\AppConfig.xml");
                }
                objlog.WriteEvents("CreateChannelDirectory", $"{ChannelName} Directory Created", ChannelName);
                FillEventLog($"{ChannelName} Directory Created", "G");
            }
            catch (Exception ex)
            {
                objlog.WriteError("CreateChannelDirectory", $"Error in creating {ChannelName} Directory {ex.ToString()}", ChannelName);
                FillEventLog($"Error in creating {ChannelName} Directory", "E");
            }
        }
        public bool Fetchtemplatedata()
        {   //To get data from channelbugmaster
            try
            {
             //   if (DbOnlyForAsrun == false) { return false; }
                if (EnableDb == false)
                {
                    return false;
                }
                if (DbOnlyForAsrun == true) { return false; }
                DataTable TempGraphics;
                DbLayer _db = new DbLayer(this);
                TempGraphics = _db.FetchDataFromDB("select srno as SrNo, channelid as ChannelName, templatename as TemplateName, layerno as LayerNo, param1 as Param1, param2 as Param2, param3 as Param3,param4 as Param4, param5 as Param5 ,param6 as Param6 , param7 as Param7,param8 as Param8,param9 as Param9,param10 as Param10 , bugtype as BugType, killwithprimary as KillWithPrimary, fileextension as FileExtension, showonbreak as ShowOnBreak, bugstatus as BugStatus,dbtemplate as DBTemplate,dbtablename as DBTableName, dbcolumnname as DBColumnName,dbcondition as DBCondition from channelbugmaster where channelid='" + ChannelName + "'");
                if (TempGraphics != null)
                {
                    _dttemplateinfo.Clear();
                    foreach (DataRow item in TempGraphics.Rows)
                    {
                        _dttemplateinfo.ImportRow(item);
                    }
                    _dttemplateinfo.TableName = "ConfigData";
                    TempGraphics.TableName = "ConfigData";
                    if (File.Exists(XMLTemplateConfigfilepath))
                    {
                        string Backupfolder = $"{ChannelNamepath}\\BackupXmlFolder";
                        if (!Directory.Exists(Backupfolder))
                        {
                            Directory.CreateDirectory(Backupfolder);
                           
                        }

#if UTC_MODE
          File.Copy(XMLTemplateConfigfilepath, Backupfolder + "\\" + ChannelName + "_TemplateInfo" + DateTime.UtcNow.ToString("dd-MM-yy  HH-mm-ss") + ".xml");
#else
                        File.Copy(XMLTemplateConfigfilepath, Backupfolder + "\\" + ChannelName + "_TemplateInfo" + DateTime.Now.ToString("dd-MM-yy  HH-mm-ss") + ".xml");
#endif
                    }
                    _dttemplateinfo.WriteXml(XMLTemplateConfigfilepath, XmlWriteMode.WriteSchema);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                objlog.WriteError("Fetchtemplatedata", "Error-- " + ex.ToString(), ChannelName);
                return false;
            }
        }
        public void CreateTemplate()
        {   //To create template and get data from templatemaster
            try
            {
             //   if (DbOnlyForAsrun == false) { return; }
                if (EnableDb == false)
                {
                    return;
                }
                if (DbOnlyForAsrun == true) { return; }
                DataTable createtemplatedt;
                DbLayer _db = new DbLayer(this);
                createtemplatedt = _db.FetchDataFromDB("select sno as SrNo, channelid as ChannelName,templatedesc as Templatecontent ,templatename as TemplateName,updateflg  from templatemaster where channelid='" + ChannelName + "'");
                if (createtemplatedt != null)
                {
                    createtemplatedt.TableName = "CreateTemplate";
                    if (!Directory.Exists(TemplatePath))
                    {
                        objlog.WriteEvents("createtemplate", "Template Directory is  not found so skip Download template.", ChannelName);
                        return;
                    }
                    for (int j = 0; j < _dttemplateinfo.Rows.Count; j++)
                    {
                        DataRow[] dr = createtemplatedt.Select("TemplateName ='" + _dttemplateinfo.Rows[j]["TemplateName"] + "'");
                        if (dr.Length > 0)
                        {
                            string data = dr[0]["Templatecontent"] + "";
                            string templatename = (dr[0]["TemplateName"] + "").ToUpper();
                            int updateflag = Convert.ToInt32(dr[0]["updateflg"]);
                            if (!File.Exists(TemplatePath + @"\" + templatename + ".ml-cgs"))
                            {
                                File.WriteAllText(TemplatePath + @"\" + templatename + ".ml-cgs", data);
                                objlog.WriteEvents("createtemplate", "Template created " + templatename, ChannelName);
                            }
                            else if (updateflag == 1)
                            {
                                File.WriteAllText(TemplatePath + @"\" + templatename + ".ml-cgs", data);
                                objlog.WriteEvents("createtemplate", "Template created " + templatename, ChannelName);
                            }
                        }
                    }
                    _db.ExecuteQuery("UPDATE templatemaster SET updateflg=0  WHERE channelid='" + ChannelName + "'");
                }
                else
                {
                    objlog.WriteEvents("createtemplate", "No updated data found in tempatemaster", ChannelName);
                    FillEventLog("No updated data found in tempatemaster", "G");
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("CreateTemplate", "Error in creating template " + ex.ToString(), ChannelName);
                FillEventLog("Error in creating template ", "E");
            }
            finally
            {
                DirectoryInfo(TemplatePath);
            }
        }
        private void DirectoryInfo(string _folder)
        {   //To read directory
            try
            {
                if (!Directory.Exists(_folder))
                {
                    objlog.WriteEvents("DirectoryInfo", "Template File Directory is Not Found path is " + _folder, ChannelName);
                    return;
                }
                DirectoryInfo mydir = new DirectoryInfo(_folder);
                var myItems = new BindingList<string>();
                foreach (FileInfo file in mydir.GetFiles("*.ml-cgs", SearchOption.TopDirectoryOnly).Union(mydir.GetFiles("*.tgr", SearchOption.TopDirectoryOnly)))
                {
                    string[] filename = file.Name.ToUpper().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + filename[0] + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        if (!myItems.Contains(filename[0]))
                        {
                            myItems.Add(filename[0]);
                        }

                    }
                }
                load.lb_templatelist.DataSource = myItems;
                objlog.WriteEvents("DirectoryInfo", mydir.Name + "Folder Contains " + myItems.Count + " templates ", ChannelName);
            }
            catch (Exception ex)
            {
                objlog.WriteError("DirectoryInfo", "Error in reading directory " + ex.ToString(), ChannelName);
            }
        }
        internal string ContentInfo(string filename, string type, string Extesion, out bool isAvailableinNas, out bool IsAvalaible)
        {
            isAvailableinNas = false;
            IsAvalaible = false;
            try
            {
                string path = null;
                if (type == "TICKER-FILE")
                {
                    path = (ContentPath + @"\FILE");
                }
                else if (type == "VIDEO")
                {
                    path = (ContentPath + @"\ANIMATION");
                }
                else if (type == "IMAGE")
                {
                    path = (ContentPath + @"\IMAGE");
                }
                if (string.IsNullOrEmpty(path))
                {
                    IsAvalaible = true;
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
                            IsAvalaible = true;
                            return Path.GetFileName(file);
                        }
                    }
                    if (!FlgFileFound)
                    {
                        objlog.WriteEvents("ContentInfo", "File not found in Local Storage for update template filename=" + filename, ChannelName);
                        string[] nasfiles = Directory.GetFiles(SecondaryContentPath, filename + ".*");
                        foreach (string nasfilename in nasfiles)
                        {
                            if (filename.ToUpper() == Path.GetFileNameWithoutExtension(nasfilename).ToUpper())
                            {
                                isAvailableinNas = true;
                                IsAvalaible = true;
                                return Path.GetFileName(nasfilename);
                            }
                        }
                        if (isAvailableinNas == false)
                        {
                            objlog.WriteEvents("ContentInfo", "File not found in Secondary Storage[NAS] for update template filename=" + filename, ChannelName);
                            if (Extesion != "")
                            {
                                filename = filename + Extesion;
                            }
                        }
                    }
                }
                return filename;
            }
            catch (Exception ex)
            {
                objlog.WriteError("ContentInfo", "Error in fetching extension " + ex.ToString(), ChannelName);
                return filename;
            }
        }
        private void ReadConfig(string channelName)
        {
         //   string t2 = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.ff");
            string log = "";
            try
            {
                string fileName = Path.Combine(Application.StartupPath, "Channels", channelName, "AppConfig.Xml");
                if (!File.Exists(fileName))
                {
                    objlog.WriteError("ReadConfig", $"Config file does not exist at {fileName}", ChannelName);
                    return;
                }

                DataSet ds = new DataSet();
                ds.ReadXml(fileName);


                #region Application Mode
                DataTable appModeTable;
                if (!ds.Tables.Contains("ApplicationMode"))
                {
                    appModeTable = new DataTable("ApplicationMode");
                    ds.Tables.Add(appModeTable);
                    appModeTable.Rows.Add(appModeTable.NewRow());
                }
                else
                {
                    appModeTable = ds.Tables["ApplicationMode"];
                }

                // Ensure there's at least one row
                if (appModeTable.Rows.Count == 0)
                    appModeTable.Rows.Add(appModeTable.NewRow());

                // Read ApplicationMode
                if (ds.Tables.Contains("ApplicationMode"))
                {
                    var row = ds.Tables["ApplicationMode"].Rows[0];

                    ExternalAgentMode = GetValue(row, "ExternalAgentMode", true);
                    PreviewWindow = GetValue(row, "PreviewWindow", true);

                    log += $"ExternalAgentMode = {ExternalAgentMode}\n";
                    log += $"PreviewWindow = {PreviewWindow}\n";
                }
                #endregion Application Mode


                #region  Config Data
                DataTable confgiDataTable;
                if (!ds.Tables.Contains("ConfigData"))
                {
                    confgiDataTable = new DataTable("ConfigData");
                    ds.Tables.Add(confgiDataTable);
                    confgiDataTable.Rows.Add(confgiDataTable.NewRow());
                }
                else
                {
                    confgiDataTable = ds.Tables["ConfigData"];
                }

                // Ensure there's at least one row
                if (confgiDataTable.Rows.Count == 0)
                    confgiDataTable.Rows.Add(confgiDataTable.NewRow());

                // Read ConfigData
                if (ds.Tables.Contains("ConfigData"))
                {
                    var row = ds.Tables["ConfigData"].Rows[0];

                    ChannelType = GetValue(row, "ChannelType", "HD")?.ToUpper() ?? "";
                    VideoFormatType = GetValue(row, "VideoFormat", "PAL")?.ToUpper() ?? "";
                    EnableDb = GetValue(row, "EnableDb", true);
                    DbType = GetValue(row, "DbType", "MYSQL");
                    DbIPAddress = GetValue(row, "DbIPAddress", "192.168.15.251");
                    DbPort = GetValue(row, "DbPort", "3306");
                    DbUsername = GetValue(row, "DbUsername", "db_user");
                    DbPassword = GetValue(row, "DbPassword", "Dbuser@123");
                    DbName = GetValue(row, "DbName", "mediaxdb_cloud2710");
                    GraphicsTable = GetValue(row, "GraphicsTable", "");
                    ReceivingPort = GetValue(row, "ReceivingPort", 11006);
                    LTCPort = GetValue(row, "LTCPort", 18000);
                    CheckEventInterval = GetValue(row, "CheckEventInterval", 10);
                    ReconcileKeyCheck = GetValue(row, "ReconcileKeyCheck", false);
                    OverlappingCheck = GetValue(row, "OverlappingCheck", false);
                    PMSPacketOn = GetValue(row, "PMSPacketOn", false);
                    CueOffSet = GetValue(row, "CueOffSet", "75");
                    NdiOutputName = GetValue(row, "NdiOutputName", "MBIV GRAPHICS SERVER OUTPUT");
                    TemplateRefreshInterval = GetValue(row, "TemplateRefreshInterval", "60000");
                    AutoUpdateTemplate = GetValue(row, "AutoUpdateTemplate", false);
                    AddDelayincommand = GetValue(row, "AddDelayInCommand", false);
                    PromoPrefix = GetValue(row, "PromoPrefix", "");
                    ProgramPrefix = GetValue(row, "ProgramPrefix", "");
                    CommercialPrefix = GetValue(row, "CommercialPrefix", "");
                    TemplatePath = GetValue(row, "TemplatePath", "D:\\RUNNINGCHANNELS\\Template");
                    ContentPath = GetValue(row, "ContentPath", "D:\\RUNNINGCHANNELS\\GFX\\ELEMENT");
                    SubtitleCheck = GetValue(row, "SubtitleCheck", false);
                    SubtitlePath = GetValue(row, "SubtitlePath", "");
                    KeyingMode = GetValue(row, "KeyingMode", false);
                    KeyerPort = GetValue(row, "KeyerPort", 53000);
                    KeyerType = GetValue(row, "KeyerType", "disable");
                    KeyerHeartBeat = GetValue(row, "KeyerHeartBeat", false);
                    SecondaryContentPath = GetValue(row, "SecondaryContentPath", "D:\\RUNNINGCHANNELS\\GFX\\ELEMENT");
                    DurationBasedAston = GetValue(row, "AutoCloseAston", true);

                    MultiCastEnable = GetValue(row, "EnableMultiCast", false);
                    MultiCastIP = GetValue(row, "MultiCastIP", "232.1.1.2");
                    DbOnlyForAsrun = GetValue(row, "DbOnlyForAsrun", false);
                    CnfPath = GetValue(row, "CNFPath", "C:\\Program Files\\Planetcast\\mylogin.cnf");
                    CommandDelay= GetValue(row, "Delayincommand", 1000);
                    log += $"ChannelType = {ChannelType}\n" +
                            $"VideoFormatType = {VideoFormatType}\n" +
                            $"EnableDb = {EnableDb}\n" +
                            $"DbType = {DbType}\n" +
                            $"DbIPAddress = {DbIPAddress}\n" +
                            $"DbPort = {DbPort}\n" +
                            $"DbUsername = {DbUsername}\n" +
                            $"DbPassword = {DbPassword}\n" +
                            $"DbName = {DbName}\n" +
                            $"GraphicsTable = {GraphicsTable}\n" +
                            $"ReceivingPort = {ReceivingPort}\n" +
                            $"LTCPort = {LTCPort}\n" +
                            $"CheckEventInterval = {CheckEventInterval}\n" +
                            $"ReconcileKeyCheck = {ReconcileKeyCheck}\n" +
                            $"OverlappingCheck = {OverlappingCheck}\n" +
                            $"PMSPacketOn = {PMSPacketOn}\n" +
                            $"CueOffSet = {CueOffSet}\n" +
                            $"NdiOutputName = {NdiOutputName}\n" +
                            $"TemplateRefreshInterval = {TemplateRefreshInterval}\n" +
                            $"AutoUpdateTemplate = {AutoUpdateTemplate}\n" +
                            $"AddDelayincommand = {AddDelayincommand}\n" +
                            $"PromoPrefix = {PromoPrefix}\n" +
                            $"ProgramPrefix = {ProgramPrefix}\n" +
                            $"CommercialPrefix = {CommercialPrefix}\n" +
                            $"TemplatePath = {TemplatePath}\n" +
                            $"ContentPath = {ContentPath}\n" +
                            $"SubtitleCheck = {SubtitleCheck}\n" +
                            $"SubtitlePath = {SubtitlePath}\n" +
                            $"KeyingMode = {KeyingMode}\n" +
                            $"KeyerPort = {KeyerPort}\n" +
                            $"KeyerType = {KeyerType}\n" +
                            $"KeyerHeartBeat = {KeyerHeartBeat}\n" +
                            $"SecondaryContentPath = {SecondaryContentPath}\n" +
                            $"DurationBasedAston = {DurationBasedAston}\n" +
                            $"MultiCastEnable = {MultiCastEnable}\n" +
                            $"MultiCastIP = {MultiCastIP}\n" +
                            $"DbOnlyForAsrun = {DbOnlyForAsrun}\n" +
                            $"CnfPath = {CnfPath}\n"+
                            $"CommandDelay={CommandDelay}\n";


                }
                #endregion Config Data


                #region ScteSetting
                DataTable scteSettingTable;
                if (!ds.Tables.Contains("ScteSetting"))
                {
                    scteSettingTable = new DataTable("ScteSetting");
                    ds.Tables.Add(scteSettingTable);
                    scteSettingTable.Rows.Add(scteSettingTable.NewRow());
                }
                else
                {
                    scteSettingTable = ds.Tables["ScteSetting"];
                }

                // Ensure there's at least one row
                if (scteSettingTable.Rows.Count == 0)
                    scteSettingTable.Rows.Add(scteSettingTable.NewRow());

                // Read ScteSetting
                if (ds.Tables.Contains("ScteSetting"))
                {
                    var row = ds.Tables["ScteSetting"].Rows[0];
                    ScteEnable = GetValue(row, "ScteEnable", false);
                    ScteTemplateName = GetValue(row, "ScteTemplateName", "SCTEON");
                    ScteIP = GetValue(row, "ScteIP", "192.168.15.23");
                    SctePort = GetValue(row, "SctePort", "4444");
                    SctePreRoll = GetValue(row, "SctePreRoll", "30");

                    log += $"ScteEnable = {ScteEnable}\n" +
                           $"ScteTemplateName = {ScteTemplateName}\n" +
                           $"ScteIP = {ScteIP}\n" +
                           $"SctePort = {SctePort}\n" +
                           $"SctePreRoll = {SctePreRoll}\n";

                }
                #endregion ScteSetting


                #region SubtitleSetting
                DataTable subtitlesSettingTable;
                if (!ds.Tables.Contains("SubtitleSetting"))
                {
                    subtitlesSettingTable = new DataTable("SubtitleSetting");
                    ds.Tables.Add(subtitlesSettingTable);
                    subtitlesSettingTable.Rows.Add(subtitlesSettingTable.NewRow());
                }
                else
                {
                    subtitlesSettingTable = ds.Tables["SubtitleSetting"];
                }

                // Ensure there's at least one row
                if (subtitlesSettingTable.Rows.Count == 0)
                    subtitlesSettingTable.Rows.Add(subtitlesSettingTable.NewRow());

                // Read SubtitleSetting
                if (ds.Tables.Contains("SubtitleSetting"))
                {
                    var row = ds.Tables["SubtitleSetting"].Rows[0];
                    SubtitleMode = GetValue(row, "SubtitleMode", "AUTOMATION");
                    SubtitleFileType = GetValue(row, "SubtitleFileType", "SRT");
                    SubtitleEnable = GetValue(row, "SubtitleEnable", false);
                    SubtitlePort = GetValue(row, "SubtitlePort", 5100);
                    SubtitleStartOffset = GetValue(row, "SubtitleStartOffset", "0");
                    SubtitleEndOffset = GetValue(row, "SubtitleEndOffset", "0");
                    SubtitleSuffix = GetValue(row, "SubtitleSuffix", "");
                    SubtitleTemplate = GetValue(row, "SubtitleTemplate", "Subtitle");
                    SubtitlePriority = GetValue(row, "Priority_BOTH_case", "SRT");
                    encodingSRT = GetValue(row, "EncodingChange", false);

                    log += $"SubtitleMode = {SubtitleMode}\n" +
                           $"SubtitleFileType = {SubtitleFileType}\n" +
                           $"SubtitleEnable = {SubtitleEnable}\n" +
                           $"SubtitlePort = {SubtitlePort}\n" +
                           $"SubtitleStartOffset = {SubtitleStartOffset}\n" +
                           $"SubtitleEndOffset = {SubtitleEndOffset}\n" +
                           $"SubtitleSuffix = {SubtitleSuffix}\n" +
                           $"SubtitleTemplate = {SubtitleTemplate}\n" +
                           $"SubtitlePriority = {SubtitlePriority}\n" +
                           $"EncodingSRT = {encodingSRT}\n";

                }
                #endregion SubtitleSetting


                #region NetworkStream
                DataTable networkStreamTable;
                if (!ds.Tables.Contains("NetworkStream"))
                {
                    networkStreamTable = new DataTable("NetworkStream");
                    ds.Tables.Add(networkStreamTable);
                    networkStreamTable.Rows.Add(networkStreamTable.NewRow());
                }
                else
                {
                    networkStreamTable = ds.Tables["NetworkStream"];
                }

                // Ensure there's at least one row
                if (networkStreamTable.Rows.Count == 0)
                    networkStreamTable.Rows.Add(networkStreamTable.NewRow());

                // Read NetworkStream
                if (ds.Tables.Contains("NetworkStream"))
                {
                    var row = ds.Tables["NetworkStream"].Rows[0];
                    NetworkMode = GetValue(row, "StreamEnable", "FALSE");
                    StreamType = GetValue(row, "StreamType", "SRT");
                    NetworkURL = GetValue(row, "StreamURL", "srt://192.168.27.154:12000");
                    CodecId = GetValue(row, "StreamConfig", "h264");
                    Vbitrate = GetValue(row, "VideoBitrate", "5M");

                    log += $"NetworkMode = {NetworkMode}\n" +
                           $"StreamType = {StreamType}\n" +
                           $"NetworkURL = {NetworkURL}\n" +
                           $"CodecId = {CodecId}\n" +
                           $"Vbitrate = {Vbitrate}\n";

                }
                #endregion NetworkStream


                #region GFXClient
                DataTable gfxClientTable;
                if (!ds.Tables.Contains("GFXClient"))
                {
                    gfxClientTable = new DataTable("GFXClient");
                    ds.Tables.Add(gfxClientTable);
                    gfxClientTable.Rows.Add(gfxClientTable.NewRow());
                }
                else
                {
                    gfxClientTable = ds.Tables["GFXClient"];
                }

                // Ensure there's at least one row
                if (gfxClientTable.Rows.Count == 0)
                    gfxClientTable.Rows.Add(gfxClientTable.NewRow());

                // Read GFXClient
                if (ds.Tables.Contains("GFXClient"))
                {
                    var row = ds.Tables["GFXClient"].Rows[0];
                    GfxclientEnable = GetValue(row, "GFXClientEnable", "FALSE");
                    GfxclientPort = GetValue(row, "GFXClientPort", "");
                    GfxclientIp = GetValue(row, "GFXClientIP", "");

                    log += $"GfxclientEnable = {GfxclientEnable}\n" +
                           $"GfxclientPort = {GfxclientPort}\n" +
                           $"GfxclientIp = {GfxclientIp}\n";
                }
                #endregion GFXClient


                #region AudioSetting
                DataTable audioSettingTable;
                if (!ds.Tables.Contains("AudioSetting"))
                {
                    audioSettingTable = new DataTable("AudioSetting");
                    ds.Tables.Add(audioSettingTable);
                    audioSettingTable.Rows.Add(audioSettingTable.NewRow());
                }
                else
                {
                    audioSettingTable = ds.Tables["AudioSetting"];
                }

                // Ensure there's at least one row
                if (audioSettingTable.Rows.Count == 0)
                    audioSettingTable.Rows.Add(audioSettingTable.NewRow());

                // Read AudioSetting
                if (ds.Tables.Contains("AudioSetting"))
                {
                    var row = ds.Tables["AudioSetting"].Rows[0];
                    AudioSettingEnable = GetValue(row, "AudioSettingEnable", "FALSE");
                    AudioChannel = GetValue(row, "AudioChannel", "2");
                    AudioSamplePerSecond = GetValue(row, "SamplePerSecond", "48000");
                    AudioBitsPerSample = GetValue(row, "BitsPerSample", "16");
                    AudioBitrate = GetValue(row, "AudioBitrate", "0");

                    log += $"AudioSettingEnable = {AudioSettingEnable}\n" +
                           $"AudioChannel = {AudioChannel}\n" +
                           $"AudioSamplePerSecond = {AudioSamplePerSecond}\n" +
                           $"AudioBitsPerSample = {AudioBitsPerSample}\n" +
                           $"AudioBitrate = {AudioBitrate}\n";

                }
                #endregion AudioSetting


                #region AJASettings
                DataTable ajaSettingTable;
                if (!ds.Tables.Contains("AJASettings"))
                {
                    ajaSettingTable = new DataTable("AJASettings");
                    ds.Tables.Add(ajaSettingTable);
                    ajaSettingTable.Rows.Add(ajaSettingTable.NewRow());
                }
                else
                {
                    ajaSettingTable = ds.Tables["AJASettings"];
                }

                // Ensure there's at least one row
                if (ajaSettingTable.Rows.Count == 0)
                    ajaSettingTable.Rows.Add(ajaSettingTable.NewRow());

                // Read AJASettings
                if (ds.Tables.Contains("AJASettings"))
                {
                    var row = ds.Tables["AJASettings"].Rows[0];
                    AJASettingsEnable = GetValue(row, "AJASettingsEnable", "TRUE");
                    AJACardReference = GetValue(row, "AJACardReference", "1");

                    log += $"AJASettingsEnable = {AJASettingsEnable}\n" +
                           $"AJACardReference = {AJACardReference}\n";

                }
                #endregion AJASettings


                #region AsRun
                DataTable asRunTable;
                if (!ds.Tables.Contains("ASRun"))
                {
                    asRunTable = new DataTable("ASRun");
                    ds.Tables.Add(asRunTable);
                    asRunTable.Rows.Add(asRunTable.NewRow());
                }
                else
                {
                    asRunTable = ds.Tables["ASRun"];
                }

                // Ensure there's at least one row
                if (asRunTable.Rows.Count == 0)
                    asRunTable.Rows.Add(asRunTable.NewRow());

                // Read ASRun
                if (ds.Tables.Contains("ASRun"))
                {
                    var row = ds.Tables["ASRun"].Rows[0];
                    ASRunEnable = GetValue(row, "ASRunEnable", true);
                    AsRunType = GetValue(row, "AsRunType", "FILE");
                    AsRunFolderPath = GetValue(row, "AsRunFolderPath", "D:\\RUNNINGCHANNELS\\ASRUN\\DEMO");
                    AsRunTableName = GetValue(row, "AsRunTableName", "tbl_asrun");
                    AsRunDateTimeColumn = GetValue(row, "AsRunDateTimeColumn", "AsRunDateTime");
                    AsRunTemplateNameColumn = GetValue(row, "AsRunTemplateNameColumn", "AsRunTemplateName");
                    AsRunFilenameColumn = GetValue(row, "AsRunFilenameColumn", "AsRunFilename");
                    AsRunTemplateFileNameColumn = GetValue(row, "AsRunTemplateFileNameColumn", "AsRunTemplateFileName");
                    AsRunStatusColumn = GetValue(row, "AsRunStatusColumn", "AsRunStatus");
                    AsRunChannelNameMam = GetValue(row, "AsRunChannelNameMam", "TEST");
                    AsRunDurationColumn = GetValue(row, "AsRunDurationColumn", "AsRunDuration");
                    AsRunReconcileKeyColumn = GetValue(row, "AsRunReconcileKeyColumn", "AsRunReconcileKey");
                    AsRunManualEnable = GetValue(row, "AsRunManualEnable", true);
                    AsRunTakeTime = GetValue(row, "AsRunTakeTime", "SYSTEM");

                    log += $"ASRunEnable = {ASRunEnable}\n" +
                           $"AsRunType = {AsRunType}\n" +
                           $"AsRunFolderPath = {AsRunFolderPath}\n" +
                           $"AsRunTableName = {AsRunTableName}\n" +
                           $"AsRunDateTimeColumn = {AsRunDateTimeColumn}\n" +
                           $"AsRunTemplateNameColumn = {AsRunTemplateNameColumn}\n" +
                           $"AsRunFilenameColumn = {AsRunFilenameColumn}\n" +
                           $"AsRunTemplateFileNameColumn = {AsRunTemplateFileNameColumn}\n" +
                           $"AsRunStatusColumn = {AsRunStatusColumn}\n" +
                           $"AsRunChannelNameMam = {AsRunChannelNameMam}\n" +
                           $"AsRunDurationColumn = {AsRunDurationColumn}\n" +
                           $"AsRunReconcileKeyColumn = {AsRunReconcileKeyColumn}\n" +
                           $"AsRunManualEnable = {AsRunManualEnable}\n" +
                           $"AsRunTakeTime = {AsRunTakeTime}\n";

                }
                #endregion AsRun


                ds.WriteXml(fileName);  // Save any changes for missing keys

                objlog.WriteEvents("ReadConfig", log + "Config_Reading_Successful", channelName);
                FillEventLog("Config file read successfully", "G");
            }
            catch (Exception ex)
            {
                FillEventLog("Config file is not read successfully", "E");
                objlog.WriteError("ReadConfig", ex.ToString() + Environment.NewLine + log, channelName);
            }
        }

        private T GetValue<T>(DataRow row, string columnName, T defaultValue)
        {
            DataTable table = row.Table;

            // Add column if missing
            if (!table.Columns.Contains(columnName))
            {
                Type colType = typeof(T); // Initially the coltype is generic

                // Handle nullable types gracefully
                if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    colType = Nullable.GetUnderlyingType(colType);

                table.Columns.Add(columnName, colType);
                row[columnName] = defaultValue;
            }

            object value = row[columnName];
            if (value == DBNull.Value || value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                row[columnName] = defaultValue;
                return defaultValue;
            }

            try
            {
                // Convert.ChangeType handles string->int, string->bool, etc.
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
        private void ReadConfig_old(string Channelname)
        {
            string Log = "";
            try
            {
                string FileName = $"{Application.StartupPath}\\Channels\\{ChannelName}\\AppConfig.Xml";
                if (!File.Exists(FileName))
                {
                    return;
                }
                DataSet _ds = new DataSet();
                DataTable _Configdt;
                _ds.ReadXml(FileName);
                if (_ds.Tables["ApplicationMode"] != null)
                {
                    ExternalAgentMode = Convert.ToBoolean(_ds.Tables["ApplicationMode"].Rows[0]["ExternalAgentMode"] + "");
                    Log = Log + $"{Environment.NewLine}ExternalAgentMode =" + ExternalAgentMode + Environment.NewLine;
                    PreviewWindow = Convert.ToBoolean(_ds.Tables["ApplicationMode"].Rows[0]["PreviewWindow"] + "");
                    Log = Log + $"PreviewWindow =" + PreviewWindow + Environment.NewLine;
                }
                _Configdt = _ds.Tables["ConfigData"];
                MultiCastEnable = Convert.ToBoolean((_Configdt.Rows[0]["EnableMultiCast"] + "").Trim());
                Log = Log + "OnlyForMultiCast =" + MultiCastEnable + Environment.NewLine;
                MultiCastIP = (_Configdt.Rows[0]["MultiCastIP"] + "").Trim();
                Log = Log + "DbIPAddress =" + MultiCastIP + Environment.NewLine;
                ChannelType = (_Configdt.Rows[0]["ChannelType"] + "").Trim().ToUpper();
                Log = Log + $"ChannelType =" + ChannelType + Environment.NewLine;
                VideoFormatType = (_Configdt.Rows[0]["VideoFormat"] + "").Trim().ToUpper();
                EnableDb = Convert.ToBoolean((_Configdt.Rows[0]["EnableDb"] + "").Trim());
                Log = Log + "EnableDb =" + EnableDb + Environment.NewLine;
                DbType = (_Configdt.Rows[0]["DbType"] + "").Trim();
                Log = Log + "DbType =" + DbType + Environment.NewLine;
                DbIPAddress = (_Configdt.Rows[0]["DbIPAddress"] + "").Trim();
                Log = Log + "DbIPAddress =" + DbIPAddress + Environment.NewLine;
                DbUsername = (_Configdt.Rows[0]["DbUsername"] + "").Trim();
                Log = Log + "DbUsername =" + DbUsername + Environment.NewLine;
                DbPassword = (_Configdt.Rows[0]["DbPassword"] + "").Trim();
                Log = Log + "DbPassword =" + DbPassword + Environment.NewLine;
                DbName = (_Configdt.Rows[0]["DbName"] + "").Trim();
                Log = Log + "DbName =" + DbName + Environment.NewLine;
                GraphicsTable = (_Configdt.Rows[0]["GraphicsTable"] + "").Trim();
                Log = Log + "GraphicsTable =" + GraphicsTable + Environment.NewLine;
                DbPort = (_Configdt.Rows[0]["DbPort"] + "").Trim();
                Log = Log + "DbPort =" + DbPort + Environment.NewLine;
                ReceivingPort = Convert.ToInt32((_Configdt.Rows[0]["ReceivingPort"] + "").Trim());
                Log = Log + "ReceivingPort =" + ReceivingPort + Environment.NewLine;
                CommandDelay = Convert.ToInt32((_Configdt.Rows[0]["Delayincommand"] + "").Trim().ToUpper());
                Log = Log + "Delayincommand =" + CommandDelay + Environment.NewLine;//Delayincommand
                LTCPort = Convert.ToInt32((_Configdt.Rows[0]["LTCPort"] + "").Trim().ToUpper());
                Log = Log + "LTCPort =" + LTCPort + Environment.NewLine;
                CheckEventInterval = Convert.ToInt32((_Configdt.Rows[0]["CheckEventInterval"] + "").Trim().ToUpper());
                Log = Log + "CheckEventInterval =" + CheckEventInterval + Environment.NewLine;
                ReconcileKeyCheck = Convert.ToBoolean((_Configdt.Rows[0]["ReconcileKeyCheck"] + "").Trim().ToUpper());
                Log = Log + "ReconcileKeyCheck =" + ReconcileKeyCheck + Environment.NewLine;
                OverlappingCheck = Convert.ToBoolean((_Configdt.Rows[0]["OverlappingCheck"] + "").Trim().ToUpper());
                Log = Log + "OverlappingCheck =" + OverlappingCheck + Environment.NewLine;
                PMSPacketOn = Convert.ToBoolean((_Configdt.Rows[0]["PMSPacketOn"] + "").Trim().ToUpper());
                Log = Log + "PMSPacketOn =" + PMSPacketOn + Environment.NewLine;
                CueOffSet = (_Configdt.Rows[0]["CueOffSet"] + "").Trim();
                Log = Log + "CueOffSet =" + CueOffSet + Environment.NewLine;
                NdiOutputName = (_Configdt.Rows[0]["NdiOutputName"] + "").Trim();
                Log = Log + "NdiOutputName =" + NdiOutputName + Environment.NewLine;
                TemplateRefreshInterval = (_Configdt.Rows[0]["TemplateRefreshInterval"] + "").Trim();
                Log = Log + "TemplateRefreshInterval =" + TemplateRefreshInterval + Environment.NewLine;
                AutoUpdateTemplate = Convert.ToBoolean((_Configdt.Rows[0]["AutoUpdateTemplate"] + "").Trim());
                Log = Log + "AutoUpdateTemplate =" + AutoUpdateTemplate + Environment.NewLine;
                AddDelayincommand = Convert.ToBoolean((_Configdt.Rows[0]["AddDelayInCommand"] + "").Trim());
                Log = Log + "AddDelayincommand =" + AddDelayincommand + Environment.NewLine;
                PromoPrefix = (_Configdt.Rows[0]["PromoPrefix"] + "").Trim().ToUpper();
                Log = Log + "PromoPrefix's =" + PromoPrefix + Environment.NewLine;
                ProgramPrefix = (_Configdt.Rows[0]["ProgramPrefix"] + "").Trim().ToUpper();
                Log = Log + "ProgramPrefix's =" + ProgramPrefix + Environment.NewLine;
                CommercialPrefix = (_Configdt.Rows[0]["CommercialPrefix"] + "").Trim().ToUpper();
                Log = Log + "CommercialPrefix's =" + CommercialPrefix + Environment.NewLine;
                TemplatePath = (_Configdt.Rows[0]["TemplatePath"] + "").Trim();
                Log = Log + "TemplatePath =" + TemplatePath + Environment.NewLine;
                ContentPath = (_Configdt.Rows[0]["ContentPath"] + "").Trim();
                Log = Log + "ContentPath =" + ContentPath + Environment.NewLine;
                SubtitleCheck = Convert.ToBoolean((_Configdt.Rows[0]["SubtitleCheck"]));
                Log = Log + "SubtitleCheck =" + SubtitleCheck + Environment.NewLine;
                SubtitlePath = (_Configdt.Rows[0]["SubtitlePath"] + "").Trim();
                Log = Log + "SubtitlePath =" + SubtitlePath + Environment.NewLine;

                CnfPath = (_Configdt.Rows[0]["CNFPath"] + "").Trim();
                Log = Log + "CNFPath =" + CnfPath + Environment.NewLine;

                KeyingMode = Convert.ToBoolean((_Configdt.Rows[0]["KeyingMode"] + "").Trim());
                KeyerPort = Convert.ToInt32((_Configdt.Rows[0]["KeyerPort"] + "").Trim());
                Log = Log + "KeyingMode =" + KeyingMode + Environment.NewLine;
                KeyerType = (_Configdt.Rows[0]["KeyerType"] + "").Trim();
                Log = Log + "KeyerType =" + KeyerType + Environment.NewLine;

                KeyerHeartBeat = Convert.ToBoolean((_Configdt.Rows[0]["KeyerHeartBeat"] != null ? (_Configdt.Rows[0]["KeyerHeartBeat"] + "") : "false"));
                Log = Log + "KeyerHeartBeat =" + KeyerHeartBeat + Environment.NewLine;

                SecondaryContentPath = _Configdt.Rows[0]["SecondaryContentPath"] != null ? (_Configdt.Rows[0]["SecondaryContentPath"] + "").Trim() : "";
                Log = Log + "SecondaryContentPath =" + SecondaryContentPath + Environment.NewLine;
                DurationBasedAston= Convert.ToBoolean((_Configdt.Rows[0]["AutoCloseAston"] + "").Trim().ToUpper());
                Log = Log + "AutoCloseAston =" + DurationBasedAston + Environment.NewLine;
                DataTable Dt_ScteConfig;
                Dt_ScteConfig = _ds.Tables["ScteSetting"];
                if (Dt_ScteConfig != null)
                {
                    ScteEnable = Convert.ToBoolean(Dt_ScteConfig.Rows[0]["ScteEnable"] + "");
                    Log = Log + "ScteEnable =" + ScteEnable + Environment.NewLine;
                    ScteTemplateName = Dt_ScteConfig.Rows[0]["ScteTemplateName"] + "";
                    Log = Log + "ScteTemplateName =" + ScteTemplateName + Environment.NewLine;
                    ScteIP = Dt_ScteConfig.Rows[0]["ScteIP"] + "";
                    Log = Log + "ScteIP =" + ScteIP + Environment.NewLine;
                    SctePort = Dt_ScteConfig.Rows[0]["SctePort"] + "";
                    Log = Log + "SctePort =" + SctePort + Environment.NewLine;
                    SctePreRoll = Dt_ScteConfig.Rows[0]["SctePreRoll"] + "";
                    Log = Log + "SctePreRoll =" + SctePreRoll + Environment.NewLine;
                }
                DataTable Dt_Asrun;
                Dt_Asrun = _ds.Tables["ASRun"];
                if (Dt_Asrun != null)
                {
                    ASRunEnable = Convert.ToBoolean(Dt_Asrun.Rows[0]["ASRunEnable"] + "");
                    Log = Log + "ASRunEnable =" + ASRunEnable + Environment.NewLine;
                    AsRunType = Dt_Asrun.Rows[0]["AsRunType"] + "";
                    Log = Log + "AsRunType =" + AsRunType + Environment.NewLine;
                    AsRunFolderPath = Dt_Asrun.Rows[0]["AsRunFolderPath"] + "";
                    Log = Log + "AsRunFolderPath =" + AsRunFolderPath + Environment.NewLine;
                    AsRunTableName = Dt_Asrun.Rows[0]["AsRunTableName"] + "";
                    Log = Log + "AsRunTableName =" + AsRunTableName + Environment.NewLine;
                    AsRunDateTimeColumn = Dt_Asrun.Rows[0]["AsRunDateTimeColumn"] + "";
                    Log = Log + "AsRunDateTimeColumn =" + AsRunDateTimeColumn + Environment.NewLine;
                    AsRunTemplateNameColumn = Dt_Asrun.Rows[0]["AsRunTemplateNameColumn"] + "";
                    Log = Log + "AsRunTemplateNameColumn =" + AsRunTemplateNameColumn + Environment.NewLine;
                    AsRunFilenameColumn = Dt_Asrun.Rows[0]["AsRunFilenameColumn"] + "";
                    Log = Log + "AsRunFilenameColumn =" + AsRunFilenameColumn + Environment.NewLine;
                    AsRunTemplateFileNameColumn = Dt_Asrun.Rows[0]["AsRunTemplateFileNameColumn"] + "";
                    Log = Log + "AsRunTemplateFileNameColumn =" + AsRunTemplateFileNameColumn + Environment.NewLine;
                    AsRunStatusColumn = Dt_Asrun.Rows[0]["AsRunStatusColumn"] + "";
                    Log = Log + "AsRunStatusColumn =" + AsRunStatusColumn + Environment.NewLine;
                    AsRunChannelNameMam = Dt_Asrun.Rows[0]["AsRunChannelNameMam"] + "";
                    Log = Log + "AsRunChannelNameMam =" + AsRunChannelNameMam + Environment.NewLine;
                    AsRunDurationColumn = Dt_Asrun.Rows[0]["AsRunDurationColumn"] + "";
                    Log = Log + "AsRunDurationColumn =" + AsRunDurationColumn + Environment.NewLine;
                    AsRunReconcileKeyColumn = Dt_Asrun.Rows[0]["AsRunReconcileKeyColumn"] + "";
                    Log = Log + "AsRunReconcileKeyColumn =" + AsRunReconcileKeyColumn + Environment.NewLine;
                    AsRunManualEnable = Convert.ToBoolean(Dt_Asrun.Rows[0]["AsRunManualEnable"] + "");
                    Log = Log + "AsRunManualEnable =" + AsRunManualEnable.ToString() + Environment.NewLine;
                    AsRunTakeTime = Dt_Asrun.Rows[0]["AsRunTakeTime"] + "";
                    Log = Log + "AsRunTakeTime =" + AsRunTakeTime.ToString() + Environment.NewLine;
                }
                DataTable Dt_SubTitle;
                Dt_SubTitle = _ds.Tables["SubTitleSetting"];
                if (Dt_SubTitle != null)
                {
                    SubtitleMode = Dt_SubTitle.Rows[0]["SubtitleMode"] + "";
                    Log = Log + "SubtitleMode =" + SubtitleMode + Environment.NewLine;

                    SubtitleFileType = Dt_SubTitle.Rows[0]["SubtitleFileType"] + "";
                    Log = Log + "SubtitleFileType =" + SubtitleFileType + Environment.NewLine;

                    SubtitleEnable = Convert.ToBoolean(Dt_SubTitle.Rows[0]["SubtitleEnable"] + "");
                    Log = Log + "SubtitleEnable =" + SubtitleEnable + Environment.NewLine;

                    SubtitlePort = Convert.ToInt32(Dt_SubTitle.Rows[0]["SubtitlePort"] + "");
                    Log = Log + "SubtitlePort =" + SubtitlePort + Environment.NewLine;

                    SubtitleStartOffset = Dt_SubTitle.Rows[0]["SubtitleStartOffset"] + "";
                    Log = Log + "SubtitleStartOffset =" + SubtitleStartOffset + Environment.NewLine;

                    SubtitleEndOffset = Dt_SubTitle.Rows[0]["SubtitleEndOffset"] + "";
                    Log = Log + "SubtitleEndOffset =" + SubtitleEndOffset + Environment.NewLine;

                    SubtitleSuffix = Dt_SubTitle.Rows[0]["SubtitleSuffix"] + "";
                    Log = Log + "SubtitleSuffix =" + SubtitleSuffix + Environment.NewLine;

                    SubtitleTemplate = Dt_SubTitle.Rows[0]["SubtitleTemplate"] + "";
                    Log = Log + "SubtitleTemplate =" + SubtitleTemplate + Environment.NewLine;

                    SubtitlePriority = Dt_SubTitle.Rows[0]["Priority_BOTH_case"] + "";
                    Log = Log + "Priority_BOTH_case =" + SubtitlePriority + Environment.NewLine; //21322

                    encodingSRT = Convert.ToBoolean(Dt_SubTitle.Rows[0]["EncodingChange"] + "");
                }

                DataTable Dt_TSStream;
                Dt_TSStream = _ds.Tables["NetworkStream"];
                if (Dt_TSStream != null)
                {
                    NetworkMode = Dt_TSStream.Rows[0]["StreamEnable"] + "";
                    StreamType = Dt_TSStream.Rows[0]["StreamType"] + "";
                    NetworkURL = Dt_TSStream.Rows[0]["StreamURL"] + "";
                    CodecId = Dt_TSStream.Rows[0]["StreamConfig"] + "";
                    Vbitrate = Dt_TSStream.Rows[0]["VideoBitrate"] + "";
                }
                DataTable Dt_GFXClient;
                Dt_GFXClient = _ds.Tables["GFXClient"];
                if (Dt_GFXClient != null)
                {
                    GfxclientEnable = Dt_GFXClient.Rows[0]["GFXClientEnable"] + "";
                    GfxclientPort = Dt_GFXClient.Rows[0]["GFXClientPort"] + "";
                    GfxclientIp = Dt_GFXClient.Rows[0]["GFXClientIP"] + "";

                }
                //------code added by anurag 31-1-2024------ 
                DataTable Dt_AudioSetting;
                Dt_AudioSetting = _ds.Tables["AudioSetting"];
                if(Dt_AudioSetting != null)
                {
                    AudioSettingEnable = Dt_AudioSetting.Rows[0]["AudioSettingEnable"] + "";
                    AudioChannel = Dt_AudioSetting.Rows[0]["AudioChannel"] + "";
                    AudioSamplePerSecond = Dt_AudioSetting.Rows[0]["SamplePerSecond"] + "";
                    AudioBitsPerSample = Dt_AudioSetting.Rows[0]["BitsPerSample"] + "";
                    AudioBitrate = Dt_AudioSetting.Rows[0]["AudioBitrate"] + "";
                }
                DataTable Dt_AJASettings;
                Dt_AJASettings = _ds.Tables["AJASettings"];
                if(Dt_AJASettings != null)
                {
                    AJASettingsEnable = Dt_AJASettings.Rows[0]["AJASettingsEnable"] + "";
                    AJACardReference = Dt_AJASettings.Rows[0]["AJACardReference"] + "";
                }
                //-------------------End------------------------------
                DbOnlyForAsrun = Convert.ToBoolean((_Configdt.Rows[0]["DbOnlyForAsrun"] + "").Trim());
                Log = Log + "DbOnlyForAsrun =" + DbOnlyForAsrun + Environment.NewLine;
                objlog.WriteEvents("ReadConfig", Log + "  Config_Reading_Successful", ChannelName);
                FillEventLog("Config file read successfully", "G");
            }
            catch (Exception ex)
            {
                FillEventLog("Config file is not read successfully", "E");
                objlog.WriteError("ReadConfig", ex.ToString() + Log, ChannelName);
            }
        }
        #endregion
        string _rotateLocation = "0,0,0,0";
        MF_RECT RotateRec = new MF_RECT();
        public string RotateLocation
        {
            get
            {
                return _rotateLocation;
            }
            set
            {
                string rotaterec = value;
                RotateRec.dblPosX = Convert.ToDouble(rotaterec.Split(',')[0]);
                RotateRec.dblPosY = Convert.ToDouble(rotaterec.Split(',')[1]);
                RotateRec.dblWidth = Convert.ToDouble(rotaterec.Split(',')[2]);
                RotateRec.dblHeight = Convert.ToDouble(rotaterec.Split(',')[3]);
            }
        }

        String StartAnimationType = "";
        String EndAnimationType = "";
        int AnimationSpeed = 0;
        int EndAnimationSpeed = 0;
        string ReaderTemplateName = "";
        double RotateNumber = 0;
        double ScaleNumber = 0;
        public string SqzType = "";

        public string _VideoScale = "";
        public String VideoScale
        {
            get
            {
                return _VideoScale;
            }
            set
            {
                try
                {
                    if (value.ToLower() == "eMST_Crop".ToLower())
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_Crop;
                        _VideoScale = "eMST_Crop";
                    }
                    else if (value.ToLower() == "eMST_Default".ToLower())
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_Default;
                        _VideoScale = "eMST_Default";
                    }
                    else if (value.ToLower() == "eMST_IgnoreAR".ToLower())
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_IgnoreAR;
                        _VideoScale = "eMST_IgnoreAR";
                    }
                    else if (value.ToLower() == "eMST_LetterBox".ToLower())
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_LetterBox;
                        _VideoScale = "eMST_LetterBox";
                    }
                    else if (value.ToLower() == "eMST_NoScale".ToLower())
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_NoScale;
                        _VideoScale = "eMST_NoScale";
                    }
                    else
                    {
                        avprops.vidProps.eScaleType = eMScaleType.eMST_Default;
                        _VideoScale = "eMST_Default";
                    }
                }
                catch (Exception ex)
                {
                    objlog.WriteError("VideoScale", "Error In Seting Scale Type=" + value.ToLower() + ex.ToString(), ChannelName);

                }

            }
        }
        public void SendUdpResponse(string IP, string Port, string data)
        {

            UdpClient UdpSock;
            try
            {
                UdpSock = new UdpClient(IP.Trim(), Int32.Parse(Port.Trim()));
                byte[] SendBytes = Encoding.UTF8.GetBytes(data);
                int x = UdpSock.Send(SendBytes, SendBytes.Length);
                int y = 0;
            }
            catch (Exception ex)
            {
                objlog.WriteError("SendUdpResponse", "Error In Sending Packet" + data + ex.ToString(), ChannelName);
            }
        }
        public void UpdateFirstChildByIds(
           string graphics,
           string dataRow,
           string[] textData)
        {
            string templatePath = $"{TemplatePath}\\{graphics}.ml-cgs";
            if (!File.Exists(templatePath))
                return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templatePath);

            XmlElement root = xmlDoc.DocumentElement;
            if (root == null) return;

            string[] items = dataRow.Split(',');

            for (int i = 0; i < items.Length && i < textData.Length; i++)
            {
                // Extract ID before '~'
                string id = items[i].Split('~')[0].Trim();

                // Correct XPath
                string xpath =
                    $"/cg-composition/cg-items/cg-item[@id='{id}']/text[@type='text']";

                XmlNode textNode = xmlDoc.SelectSingleNode(xpath);

                if (textNode != null)
                {
                    textNode.InnerText = textData[i];
                }
            }

            xmlDoc.Save(templatePath);
        }


        public bool UpdateXMLComposition(string graphics, string _Id, string _Value, string _Type, string _BugType, bool IsAvailableOnNas = false, bool Islocalavailable = false)
        {
            try
            {
                string log = " ";
                string _TemplatePath = $"{TemplatePath}\\{graphics}.ml-cgs";
                if (!File.Exists(_TemplatePath))
                {
                    objlog.WriteEvents("UpdateXMLComposition", $"Templatepath={_TemplatePath} does not exist so not able to play {graphics} template.", ChannelName);
                    return false;
                }
                xmlDoc = new XmlDocument();

                xmlDoc.Load(_TemplatePath);
                XmlNode node;
                switch (_Type.ToUpper())
                {
                    case "VIDEO":
                       // try
                       // {
                            node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                       // }
                        //catch (Exception ex)
                        //{
                        //    node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.NextSibling.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        //}
                        if (node != null)
                        {
                           if (Islocalavailable== true && IsAvailableOnNas == false)
                            {
                                node.Attributes["path"].Value = ContentPath + "\\" + "ANIMATION" + "\\" + _Value;
                            }
                            else if(IsAvailableOnNas == true)
                            {
                                  node.Attributes["path"].Value = SecondaryContentPath + "\\" + _Value;
                               
                            }
                           else
                            {
                                node.Attributes["path"].Value = "";
                            }
                            if (_BugType.ToUpper() == "VIDEO-SQZ")
                            {
                                TopMostVideo = node.Attributes["topmostvideo"] != null ? Convert.ToInt32((node.Attributes["topmostvideo"].Value + "").Trim()) : 1;
                            }
                            log = $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, value={node.Attributes["path"].Value} ,TopMostVideo={TopMostVideo} )";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }

                        if (node.Attributes["play_with"] != null && ((node.Attributes["play_with"].Value + "").ToLower() == "reader"))
                        {
                            try
                            {
                                objPmslReader.SetInputFile("", node.Attributes["path"].Value + "");
                                StartAnimationType = (node.Attributes["startanimation"].Value + "").ToUpper();
                                EndAnimationType = (node.Attributes["endanimation"].Value + "").ToUpper();
                                AnimationSpeed = Convert.ToInt32(node.Attributes["animationspeed"].Value + "");
                                EndAnimationSpeed = Convert.ToInt32(node.Attributes["endanimationspeed"].Value + "");
                                XmlNode Propsnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/cg-props");
                                objPmslReader.Pos_X = Convert.ToDouble(Propsnode.Attributes["pos-x"].Value + "");
                                objPmslReader.Pos_Y = Convert.ToDouble(Propsnode.Attributes["pos-y"].Value + "");
                                objPmslReader.Pos_Width = Convert.ToDouble(Propsnode.Attributes["width"].Value + "");
                                objPmslReader.Pos_Hight = Convert.ToDouble(Propsnode.Attributes["height"].Value + "");
                                if (node.Attributes["animate"] != null && ((node.Attributes["animate"].Value + "").ToLower() == "false"))
                                    objPmslReader.animate = "no";
                                objPmslReader.ANIMATION = StartAnimationType;
                                ReaderTemplateName = graphics.ToUpper();
                            }
                            catch
                            {


                            }
                        }
                        else
                        {
                            if (node.Attributes["path"] != null && ((node.Attributes["path"].Value + "").ToLower().Contains(".") && (Path.GetExtension((node.Attributes["path"].Value + "").ToLower()) == ".mov")) && (_BugType.ToUpper() == "VIDEO-SQZ"))
                            {
                                String _path = node.Attributes["path"].Value.ToString();
                                String outOffset = "0";
                                if(node.Attributes["out_offset"] != null)
                                outOffset = node.Attributes["out_offset"].Value.ToString();
                                double out_offset = 0.0;
                                if (!double.TryParse(outOffset, out out_offset))
                                    out_offset = 0.0;
                                try
                                {
                                    if(File.Exists(_path))
                                    {
                                        MFReader filereader = new MFReader();
                                        filereader.ReaderOpen(_path,"");
                                        double fileD = 0.0;
                                        filereader.ReaderDurationGet(out fileD);
                                        LbandOffset = fileD + out_offset/1000;
                                        Task.Run(() => DurationComplete(LbandOffset, graphics,_BugType,fileD));
                                        filereader.ReaderClose();
                                        Marshal.ReleaseComObject(filereader);
                                        LbandOffset = fileD + out_offset/1000;
                                        objlog.WriteEvents("UpdateXMLComposition", $"Find duration for Template Name={graphics},ID={_Id},Type={_Type},BugType={_BugType},Finalduration={LbandOffset.ToString()}", ChannelName);
                                    }
                                    else//return code
                                    {
                                        objlog.WriteEvents("UpdateXMLComposition", "content not found for squeeze band"+ _path, ChannelName);
                                        xmlDoc.Save(_TemplatePath);
                                        return false;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    xmlDoc.Save(_TemplatePath);
                                    objlog.WriteError("UpdateXMLComposition", "Error in getting fileduration in squeeze graphics" + ex.ToString(), ChannelName);
                                    return false;
                                }
                            }
                        }
                        break;
                    case "PLAYLIST":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        if (node.Attributes["play_with"] != null && ((node.Attributes["play_with"].Value + "").ToLower() == "reader"))
                        {
                            try
                            {
                                Task.Run(() => objPmslReader.PerFormTask());

                                StartAnimationType = (node.Attributes["startanimation"].Value + "").ToUpper();
                                EndAnimationType = (node.Attributes["endanimation"].Value + "").ToUpper();
                                AnimationSpeed = Convert.ToInt32(node.Attributes["animationspeed"].Value + "");
                                EndAnimationSpeed = Convert.ToInt32(node.Attributes["endanimationspeed"].Value + "");
                                RotateNumber = node.Attributes["rotatenumber"] != null ? Convert.ToDouble(node.Attributes["rotatenumber"].Value) : 0;
                                ScaleNumber = node.Attributes["scale"] != null ? Convert.ToDouble(node.Attributes["scale"].Value) : 0;
                                RotateLocation = node.Attributes["rotatelocation"] != null ? node.Attributes["rotatelocation"].Value : "0,0,0,0";
                                SqzType = node.Attributes["sqztype"] != null ? node.Attributes["sqztype"].Value.ToUpper() : "";
                                VideoScale = node.Attributes["videoscale"] != null ? node.Attributes["videoscale"].Value.ToUpper() : "";
                                TopMostVideo = node.Attributes["topmostvideo"] != null ? Convert.ToInt32(node.Attributes["topmostvideo"].Value + "") : 1;
                                XmlNode Propsnode = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/cg-props");
                                objPmslReader.Pos_X = Convert.ToDouble(Propsnode.Attributes["pos-x"].Value + "");
                                objPmslReader.Pos_Y = Convert.ToDouble(Propsnode.Attributes["pos-y"].Value + "");
                                objPmslReader.Pos_Width = Convert.ToDouble(Propsnode.Attributes["width"].Value + "");
                                objPmslReader.Pos_Hight = Convert.ToDouble(Propsnode.Attributes["height"].Value + "");

                                objPmslReader.ANIMATION = StartAnimationType;
                                ReaderTemplateName = graphics.ToUpper();
                                objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, Information Get Successfully for reader play", ChannelName);
                            }
                            catch (Exception ex)
                            {
                                objlog.WriteError("UpdateXMLComposition", "Error in Updating Input For Playlist" + ex.ToString(), ChannelName);

                            }
                        }
                        break;
                    case "IMAGE":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        if (node != null)
                        {
                            if (IsAvailableOnNas == false)
                            {
                                node.Attributes["path"].Value = ContentPath + "\\" + "IMAGE" + "\\" + _Value;
                            }
                            else
                            {
                                node.Attributes["path"].Value = SecondaryContentPath + "\\" + _Value;
                            }
                            if (_BugType.ToUpper() == "VIDEO-SQZ")
                            {
                                TopMostVideo = node.Attributes["topmostvideo"] != null ? Convert.ToInt32(node.Attributes["topmostvideo"].Value + "") : 1;
                            }

                            log = $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, value={node.Attributes["path"].Value} ,TopMostVideo={TopMostVideo} )";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "PATH":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        if (node != null)
                        {
                            node.Attributes["path"].Value = _Value;
                            log = $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, value={node.Attributes["path"].Value} )";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "IMAGESEQUENCE":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        if (node != null)
                        {
                            node.Attributes["path"].Value = ContentPath + "\\" + "ANIMATION" + "\\" + _Value;
                            log = $"info-(templatename={graphics},ID={_Id} ,paramtype={_Type}, value={node.Attributes["path"].Value})";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "TIME":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/text[@type ='timer']");
                        if (node != null)
                        {
                            if (_Value.Split(':').Length == 3)
                            {
                                _Value = _Value + ":00";
                            }
                            node.Attributes["start-time"].Value = _Value;
                            log = $"info-(templatename= {graphics},ID={_Id} ,paramtype={_Type},value={_Value})";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "ENDDATETIME":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/text[@type ='timer']");
                        if (node != null)
                        {
                            string[] arr = _Value.Split(' ');
                            DateTime EndDateTime = DateTime.ParseExact(arr[0], "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            string[] _EventTime = arr[1].Split(':');
                            EndDateTime = EndDateTime.AddHours(Convert.ToInt32(_EventTime[0]));
                            EndDateTime = EndDateTime.AddMinutes(Convert.ToInt32(_EventTime[1]));
                            EndDateTime = EndDateTime.AddSeconds(Convert.ToInt32(_EventTime[2]));

#if UTC_MODE
            DateTime CurentDateTime = DateTime.UtcNow;
#else
                            DateTime CurentDateTime = DateTime.Now;
#endif
                            if (EndDateTime < CurentDateTime)
                            {
                                string Coundownvalue = "00:" + "00" + ":" + "00" + ":" + "00";
                                node.Attributes["start-time"].Value = Coundownvalue;
                                log = $"info End time is past time -(templatename= {graphics},ID={_Id} ,paramtype={_Type},value={Coundownvalue}) and returning";
                                break;
                            }
                            TimeSpan t = EndDateTime - CurentDateTime;
                            string hr = (t.Hours).ToString("00");
                            string Timervalue;
                            if (t.Days > 0)
                            {
                                int Hr = (t.Days * 24) + t.Hours;
                                hr = Hr.ToString("00");
                                Timervalue = hr + ":" + (t.Minutes).ToString("00") + ":" + (t.Seconds).ToString("00") + ":";
                            }
                            else
                            {
                                Timervalue = hr + ":" + (t.Minutes).ToString("00") + ":" + (t.Seconds).ToString("00") + ":";
                            }
                            node.Attributes["start-time"].Value = Timervalue;
                            log = $"info-Time has been set for (templatename= {graphics},ID={_Id} ,paramtype={_Type},value={Timervalue})";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "SUBTITLE":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/img");
                        if (node != null)
                        {
                            node.Attributes["path"].Value = SubtitlePath + "\\" + _Value;
                            log = $"info-(templatename={graphics},ID={_Id},paramtype={_Type},bugtype={_BugType},value={_Value})";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "TICKER-TEXT":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content");///content-item");//content-item is added Remove content-item if error occurs
                        //  XmlCDataSection cdata = xmlDoc.CreateCDataSection(_Value);
                        //    node.AppendChild(cdata);
                        //  bool flag = true;
                      //  _Value = _Value + "   " + _Value + "   " + _Value;
                          bool flag = TextTickerUpdate(_Value, node);
                        log = $"info-(templatename={graphics},ID={_Id} ,paramtype={_Type}, bugtype={_BugType}, value={_Value}, updatestatus={flag} )";
                        break;
                    case "TICKER-FILE":
                        if(_BugType.ToLower().Contains("dbticker"))
                        {
                            string fpath = ContentPath + "\\" + "FILE" + "\\" + "ticker.txt";
                            File.WriteAllText(fpath, _Value);
                            _Value = "ticker.txt";
                            IsAvailableOnNas = false;
                        }
                       
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                        if (node != null)
                        {
                            if (IsAvailableOnNas == false)
                            {
                                node.Attributes["path"].Value = ContentPath + "\\" + "FILE" + "\\" + _Value;
                            }
                            else
                            {
                                node.Attributes["path"].Value = SecondaryContentPath + "\\" + _Value;
                            }

                            log = $"info-(templatename={graphics},ID=>{_Id} ,paramtype= {_Type},bugtype={_BugType}, Value={ node.Attributes["path"].Value })";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "TICKER-RSS":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                        if (node != null)
                        {
                            node.Attributes["path"].Value = _Value;
                            log = $"info-(templatename={graphics},ID={_Id},paramtype= {_Type},bugtype={_BugType},value={ node.Attributes["path"].Value })";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "URLTEXT":
                        {
                            node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/text[@type ='text']");
                            if (node != null)
                            {
                                node.InnerText = _Value;
                                log = $"info-(templatename={graphics},ID={_Id} ,paramtype= {_Type},value={_Value} )";
                            }
                            else
                            {
                                objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                            }
                            break;
                        }
                    case "DBTEXT":
                       node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                        if (node != null)
                        {
                            node.InnerText = _Value;
                            log = $"info-(templatename={graphics},ID={_Id} ,paramtype= {_Type},value={_Value} )";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;

                    case "FILETEXT":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/text[@type ='text']");
                        if (node != null)
                        {
                            String str = String.Empty;
                            if (node.Attributes["filepath"] != null)
                            {
                                str = node.Attributes["filepath"].Value;
                                if (File.Exists(str))
                                {
                                    FileInfo fi = new FileInfo(str);
                                    FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                                    StreamReader sr = new StreamReader(fs);
                                    string fileContent = sr.ReadToEnd();
                                    sr.Close();
                                    fs.Close();
                                    node.InnerText = fileContent;
                                }
                            }
                            log = $"info-(templatename={graphics},ID={_Id} ,paramtype= {_Type},value={str} )";
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        break;
                    case "TEXT":
                        node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/text[@type ='text']");
                        if (node != null)
                        {
                            if (_BugType.ToUpper() == "TEXT" )
                            {
                                XmlNode z = node.NextSibling;
                                if (z != null && (z.Attributes["bg-color"] != null && z.Attributes["bg-color"].Value !="Black(0)"))
                                {
                                    Size getsiz;
                                    String fontname = node.Attributes["font"].Value.ToString();
                                    String fonsize = node.Attributes["font-size"].Value.ToString();
                                    if (z.Attributes["maxchar"] != null && z.Attributes["maxchar"].Value != "0")
                                    {
                                        int n = 0;
                                        n = Int32.Parse(z.Attributes["maxchar"].Value);
                                        if (_Value.Length <= Int32.Parse(z.Attributes["maxchar"].Value))
                                        {
                                            splitndcalculate(_Value, fontname.Split(' ')[0], float.Parse(fonsize), fontname.Split(' ')[1], out getsiz);
                                            XmlAttributeCollection list = z.Attributes;
                                            z.Attributes["width"].Value = getsiz.Width.ToString();
                                            z.Attributes["height"].Value = getsiz.Height.ToString();
                                        }
                                        else
                                        {
                                            string sub = _Value.Substring(0, n);
                                            splitndcalculate(sub, fontname.Split(' ')[0], float.Parse(fonsize), fontname.Split(' ')[1], out getsiz);
                                            XmlAttributeCollection list = z.Attributes;
                                            z.Attributes["width"].Value = getsiz.Width.ToString();
                                            z.Attributes["height"].Value = getsiz.Height.ToString();
                                        }
                                    }
                                    else
                                    {
                                        splitndcalculate(_Value, fontname.Split(' ')[0], float.Parse(fonsize), fontname.Split(' ')[1], out getsiz);
                                        XmlAttributeCollection list = z.Attributes;
                                        z.Attributes["width"].Value = getsiz.Width.ToString();
                                        z.Attributes["height"].Value = getsiz.Height.ToString();
                                    }
                                }
                            }
                            node.InnerText = _Value;
                            if (_Value.ToLower().Contains("/r/n") || _Value.ToLower().Contains("\r\n") || _Value.ToLower().Contains("/"))
                            {
                                if (_Value.Contains("$$/"))
                                {
                                    node.InnerXml = node.InnerXml.Replace("$$/", "$$$$$$$$").Replace("/", "<br/>").Replace("$$$$$$$$", "/");
                                }
                                else
                                {
                                    node.InnerXml = node.InnerXml.Replace("/r/n", "<br/>").Replace("/R/N", "<br/>").Replace("\r\n", "<br/>").Replace("/", "<br/>");
                                }
                            }
                            log = $"info-(templatename={graphics},ID={_Id} ,paramtype= {_Type},value={_Value} )";
                            try { CgXmlLayoutHelper.UpdateOverflowedTextItem(xmlDoc, _Id, _Value); }
                            catch(Exception ex)
                            {
                                objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type},error in pos shifting."+ex.Message, ChannelName);
                            }
                        }
                        else
                        {
                            objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                        }
                        //--------------------For TickerText Running Old Method---------------------
                        switch (_BugType.ToUpper())
                        {
                            case "TICKER-TEXT":
                                //node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content");///content-item");//content-item is added Remove content-item if error occurs                       
                                //bool flag1 = TextTickerUpdate(_Value, node);
                                //log = $"info-(templatename={graphics},ID={_Id} ,paramtype={_Type}, bugtype={_BugType}, value={_Value}, updatestatus={flag1} )";
                                //////////////////////////////////code for running special character in the ticker field/////////////////////////////////////////////////

                                if (!string.IsNullOrEmpty(_Value) && _Value[0] == '"' && _Value[_Value.Length - 1] == '"')
                                {
                                    _Value = "\"" + _Value + "\"";
                                }
                                //else if (!string.IsNullOrEmpty(_Value) && _Value[0] == '"')
                                //{
                                //    _Value = "\"" + _Value;
                                //}
                                //else if (!string.IsNullOrEmpty(_Value) && _Value[_Value.Length - 1] == '"')
                                //{
                                //    _Value = _Value + "\"";
                                //}

                                if (!string.IsNullOrEmpty(_Value) && _Value[0] == '\'' && _Value[_Value.Length - 1] == '\'')
                                {
                                    _Value = "\'" + _Value + "\'";
                                }
                                //else if (!string.IsNullOrEmpty(_Value) && _Value[0] == '\'')
                                //{
                                //    _Value = "\'" + _Value;
                                //}
                                //else if (!string.IsNullOrEmpty(_Value) && _Value[_Value.Length - 1] == '\'')
                                //{
                                //    _Value = _Value + "\'";
                                //}
                                string fpath = TemplatePath + "\\"+graphics+ ".txt";
                                    File.WriteAllText(fpath, _Value);
                                    node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                                    if (node != null)
                                    {
                                        node.Attributes["path"].Value = fpath;
                                    }
                                
                                //////////////////////////////////end ticker field//////////////////////////////////////////////////////////////////////////////
                                break;
                            case "TICKER-FILE":
                                // _Value = ContentInfo(_Value, _BugType,".txt",out IsAvailableOnNas);
                                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                                if (node != null)
                                {
                                    if (IsAvailableOnNas == false)
                                    {
                                        node.Attributes["path"].Value = ContentPath + "\\" + "FILE" + "\\" + _Value;
                                    }
                                    else
                                    {
                                        node.Attributes["path"].Value = SecondaryContentPath + "\\" + _Value;
                                    }

                                    log = $"info-(templatename={graphics},ID=>{_Id} ,paramtype= {_Type},bugtype={_BugType}, Value={ node.Attributes["path"].Value })";
                                }
                                else
                                {
                                    objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                                }
                                break;
                            case "TICKER-RSS":
                                node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString() + "/cg-items/cg-item[@id ='" + _Id + "']/ticker/content/content-item");
                                if (node != null)
                                {
                                    node.Attributes["path"].Value = _Value;
                                    log = $"info-(templatename={graphics},ID={_Id},paramtype= {_Type},bugtype={_BugType},value={ node.Attributes["path"].Value })";
                                }
                                else
                                {
                                    objlog.WriteEvents("UpdateXMLComposition", $"info-(templatename={graphics}, ID={_Id} ,paramtype={_Type}, detail not found for update current ID in template.", ChannelName);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        objlog.WriteEvents("UpdateXMLComposition", $"NO Update Condition matched for Template Name={graphics},ID={_Id},Type={_Type},BugType={_BugType}", ChannelName);
                        break;
                }
                xmlDoc.Save(_TemplatePath);
                objlog.WriteEvents("UpdateXMLComposition", $"update {log} done.", ChannelName);
                XmlNodeList nodelist = xmlDoc.SelectNodes("cg-composition/custom-triggers/setter");
                if (CustumOperation != null && nodelist.Count > 0)
                {
                    CustumOperation.ExecuteProcess(graphics, _Id, _Value, _Type, _BugType);
                }
                return true;
            }
            catch (Exception Ex)
            {
                objlog.WriteError("UpdateXMLComposition", $"Error in updating (template= {graphics}, value={_Value} )errInfo- {Ex.ToString()}", ChannelName);
                FillEventLog("Error in updating value", "E");
                return true;
            }
        }

        private void DurationComplete(double time,string tmpname,string bugtyp,double actualDur)
        {
            int time1 = Convert.ToInt32(time * 1000);
            int time2 = Convert.ToInt32(actualDur * 1000);
            Thread.Sleep(time1);

            //lock (objreceive)
            //{
                string prop = "";
                bool flag = false;
                if (ASRunEnable)
                {
                    try
                    {
                        prop = GetTimeFromFrame((time2 / 40));
                        //  DataRow Dr = objAgent.DtAsRun.Select($"AsRunTemplateName='{templatename.Trim()}'").FirstOrDefault();
                        // Dr["AsRunDuration"] = prop;
                        foreach (DataRow row in objAgent.DtAsRun.Rows)
                        {
                            if (row["AsRunTemplateName"].ToString() == tmpname.Trim())
                            {
                                row["AsRunDuration"] = prop;
                                flag = true;
                                break;
                            }
                        }
                        //   objAgent.PrepareAsRunSetting("STOPKILL", "", "", "", tmpname.Trim());
                    }
                    catch (Exception ex)
                    {
                        objlog.WriteError("DurationComplete", $"template={templatename}during file complete" + ex.Message, ChannelName);
                    }
                    if (flag)
                        objAgent.PrepareAsRunSetting("STOPKILL", "", "", "", tmpname.Trim());
                }
                if (UsedItems.Contains(tmpname))
                {
                    KillCgItem(tmpname.Trim(), bugtyp);
                    objlog.WriteEvents("Duration Complete", $"Killed template={tmpname}on file duration", ChannelName);
                    String che = "GFXClient||" + ChannelName + "||SUCCESS||" + tmpname + "||" + "STOP" + "||" + "" + "||" + prop;
                    if (!String.IsNullOrEmpty(GfxclientIp.Trim()))
                    {
                        SendUdpResponse(GfxclientIp, GfxclientPort, che);
                    objlog.WriteEvents("Duration Complete", $"Sent to client---->>"+che, ChannelName);
                }
                }
            //}
            objlog.WriteEvents("Duration Complete", $"before remove from grid", ChannelName);
            lock (m_objGridLock)
                {
                    RemoveRowInGrid(tmpname);
                }
            objlog.WriteEvents("Duration Complete", $"after remove from grid", ChannelName);
            //  }
        }


        private Size splitndcalculate(String value, String Fnam, float size, String FFname, out Size retval)
        {
            //  String[] arr = value.Split(new String[] {"//"}, StringSplitOptions.None);
            Font arialBold1 = null;
            Size textSize = Size.Empty;
            switch (FFname.ToUpper())
            {
                case "BOLD":
                    Font arialBold = new Font(Fnam, size, FontStyle.Bold);
                    arialBold1 = arialBold;
                    break;
                case "REGULAR":
                    arialBold = new Font(Fnam, size, FontStyle.Regular);
                    arialBold1 = arialBold;
                    break;
                case "ITALIC":
                    arialBold = new Font(Fnam, size, FontStyle.Italic);
                    arialBold1 = arialBold;
                    break;
                case "BOLD ITALIC":
                    arialBold = new Font(Fnam, size, FontStyle.Strikeout);
                    arialBold1 = arialBold;
                    break;
                default:
                    arialBold = new Font(Fnam, size, FontStyle.Regular);
                    arialBold1 = arialBold;
                    break;
            }
            String[] arr = value.Split('/');
            if (arr.Count() > 0)
            {
                String bigger = "";
                int count = 0;
                foreach (String item in arr)
                {
                    if (!(bigger.Length > item.Length))
                        bigger = item;
                    count++;
                }
                textSize = TextRenderer.MeasureText(bigger, arialBold1);
                retval = textSize;
                retval.Height = count * textSize.Height;
                return retval;
            }
            retval = textSize;
            return retval;
        }
        private bool TextTickerUpdate(string _Value, XmlNode node)
        {
            try
            {
                if (node.HasChildNodes)
                {
                    int j = 0;
                    if (node.ChildNodes.Count == 1)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                           node.ChildNodes[i].InnerText = _Value;
                          
                        }
                    }
                    else
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            string[] value = _Value.Split(',');
                            if (value.Length == node.ChildNodes.Count)
                            {
                                node.ChildNodes[i].InnerText = value[i];
                            }
                            else
                            {
                                if (i >= value.Length)
                                {
                                    if (j < value.Length)
                                    {
                                        node.ChildNodes[i].InnerText = value[j];
                                        ++j;
                                    }
                                    else
                                    {
                                        j = 0;
                                        node.ChildNodes[i].InnerText = value[j];
                                        ++j;
                                    }
                                }
                                else
                                {
                                    node.ChildNodes[i].InnerText = value[i];
                                }
                            }
                        }
                    }
                    //for (int i = 0; i < node.ChildNodes.Count; i++)
                    //{
                    //    string[] value = _Value.Split(',');
                    //    if (value.Length == node.ChildNodes.Count)
                    //    {
                    //        node.ChildNodes[i].InnerText = value[i];
                    //    }
                    //    else
                    //    {
                    //        if (i >= value.Length)
                    //        {
                    //            if (j < value.Length)
                    //            {
                    //                node.ChildNodes[i].InnerText = value[j];
                    //                ++j;
                    //            }
                    //            else
                    //            {
                    //                j = 0;
                    //                node.ChildNodes[i].InnerText = value[j];
                    //                ++j;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            node.ChildNodes[i].InnerText = value[i];
                    //        }
                    //    }
                    //}
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal void FillEventLog(string EventMessage, string EventType = "")
        {
            lock (m_objLock)
            {
                try
                {
                    string[] arr = new string[2];

#if UTC_MODE
                      arr[0] = DateTime.UtcNow.ToString("dd-MM-yyyy") + " " + objlog.LTCTime;
#else
                    arr[0] = DateTime.Now.ToString("dd-MM-yyyy") + " " + objlog.LTCTime;
#endif
                    arr[1] = EventMessage;
                    ListViewItem li = new ListViewItem(arr);
                    if (EventType == "E")
                    {
                        li.ForeColor = Color.Red;//Error
                    }
                    else if (EventType == "W")
                    {
                        li.ForeColor = Color.White;//Task not Completed due to issues
                    }
                    else if (EventType == "G")
                    {
                        li.ForeColor = Color.Green;//Task Completed
                    }
                    else if (EventType == "M")
                    {
                        li.ForeColor = Color.Turquoise;//Medialooks Task
                    }
                    lv_eventlog.Items.Insert(0, li);
                    if ((lv_eventlog.Items.Count == 1000))
                        lv_eventlog.Items.RemoveAt(lv_eventlog.Items.Count - 1);
                    lv_eventlog.FullRowSelect = true;
                }
                catch (Exception ex)
                {
                    objlog.WriteError("FillEventLog", ex.ToString(), ChannelName);
                }
            }
        }
        internal void WriteSecondaryFile()
        {
            try
            {
                lock ((objwrite))
                {
                    using (StreamWriter writer = new StreamWriter(Application.StartupPath + "\\Channels\\" + ChannelName + "\\GridText.txt"))
                    {
                        for (int i = 0; i <= dgvsecevent.Rows.Count - 1; i++)
                        {
                            writer.Write("W" + "_^_^_^_");
                            for (int j = 1; j <= dgvsecevent.Columns.Count - 1; j++)
                            {
                                string value = "";
                                if (j == dgvsecevent.Columns.Count - 1)
                                {
                                    value = dgvsecevent.Rows[i].Cells[j].Value + "";
                                }
                                else
                                {
                                    value = dgvsecevent.Rows[i].Cells[j].Value + "_^_^_^_";
                                }
                                writer.Write(value);
                            }

                            writer.WriteLine();
                        }
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("WriteSecondaryFile", "Error in writing data " + ex.ToString(), ChannelName);
            }
        }
        private void ReadSecondaryFile()
        {
            lock (m_objGridLock)
            {
                try
                {
                    if (File.Exists(Application.StartupPath + "\\Channels\\" + ChannelName + "\\GridText.txt"))
                    {

                        string[] allGfxline = System.IO.File.ReadAllLines(Application.StartupPath + "\\Channels\\" + ChannelName + "\\GridText.txt");
                        foreach (string line in allGfxline)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                var Data = line.Split(new string[] { "_^_^_^_" }, StringSplitOptions.None);
                                string _enddate = Data[8];

#if UTC_MODE
                      string _currentdate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
                                string _currentdate = DateTime.Now.ToString("dd-MM-yyyy");
#endif
                                string _endtime = Data[7];
                                bool value = CheckDateTime.Invoke(_endtime, _enddate, objlog.LTCTime);
                                if (!value)
                                {
                                    dgvprievent.Invoke((MethodInvoker)(() =>
                                    {
                                        dgvsecevent.Rows.Add(Data);
                                    }));
                                    objlog.WriteEvents("ReadSecondaryFile", $"Restore previous template detail({line})", ChannelName);
                                }
                                else
                                {
                                    FillEventLog("PACKET NOT ADDED " + line, "W");
                                    objlog.WriteEvents("ReadSecondaryFile", $"template not restore because endtime allready passed({ line})", ChannelName);
                                }
                            }
                        }
                    }
                    else
                    {
                        objlog.WriteEvents("ReadSecondaryFile", "GridText.txt File not found for reloading previous template.", ChannelName);
                    }
                }
                catch (Exception ex)
                {
                    objlog.WriteError("ReadSecondaryFile", "Error in reading  data " + ex.ToString(), ChannelName);
                }
            }
        }
        #endregion


        private void PresetInput()
        {
            try
            {
                if (m_objPreview == null)
                {
                    m_objPreview = new MFPreviewClass();
                }
                    
                m_objPreview.PreviewWindowSet("", panelpreview.Handle.ToInt32());
                m_objPreview.PropsSet("preview.downscale", "18");
                avprops = new M_AV_PROPS();
                avprops.vidProps.fccType = eMFCC.eMFCC_Default;
                if (myLiveSource == null)
                {

                    myLiveSource = new MFLiveClass();
                    Thread.Sleep(100);
                }
                if (ChannelType == "SD")
                {
                    if (VideoFormatType.Trim().ToUpper() != "PAL")
                        avprops.vidProps.eVideoFormat = eMVideoFormat.eMVF_HD1080_5994i;
                    else
                    avprops.vidProps.eVideoFormat = eMVideoFormat.eMVF_PAL;
                    avprops.vidProps.nAspectX = 4;
                    avprops.vidProps.nAspectY = 3;
                    avprops.vidProps.nWidth = 720;
                    avprops.vidProps.nHeight = 576;
                }
                else if (ChannelType == "HD")
                {
                    if (VideoFormatType.Trim().ToUpper() != "PAL")
                        avprops.vidProps.eVideoFormat = eMVideoFormat.eMVF_HD1080_5994i;
                    else
                        avprops.vidProps.eVideoFormat = eMVideoFormat.eMVF_HD1080_50i;
                    avprops.vidProps.nAspectX = 16;
                    avprops.vidProps.nAspectY = 9;
                    avprops.vidProps.nWidth = 1920;
                    avprops.vidProps.nHeight = 1080;
                }
               

                if (KeyingMode == true)
                {
                    avprops.vidProps.fccType = eMFCC.eMFCC_ARGB32;
                    ObjReader = new MFReaderClass();
                    ObjReader.PropsSet("loop", "true");
                    //   ObjReader.ReaderOpen(Application.StartupPath + "\\black.mov", ".nvidia = true decoder.force_gpu_out");
                    ObjReader.ReaderOpen(Application.StartupPath + "\\black.mov", string.Empty);

                }
                //else ///   code added for ddk hydrabad.24-01-24
                //{
                //    if(AudioSettingEnable != null)
                //    {
                //        if (AudioSettingEnable.ToUpper() == "TRUE")
                //        {
                //            avprops.audProps.nChannels = Convert.ToInt32(AudioChannel);
                //            avprops.audProps.nSamplesPerSec = Convert.ToInt32(AudioSamplePerSecond);
                //            avprops.audProps.nBitsPerSample = Convert.ToInt32(AudioBitsPerSample);
                //            avprops.audProps.nTrackSplitBits = Convert.ToInt32(AudioBitrate);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

                objlog.WriteError("PresetInput", "error in Preset Input " + ex.ToString(), ChannelName);
            }
        }
        public string counterKeyer = "start";
        private bool ReaderPlay = false;
        //MFFactoryClass mfc = new MFFactoryClass();

        int framecounter = 0;
        private void DoWork()
        {  //Main Task
          
            try
            {
                MFFrame BGFrame;
                MFFrame SdiSqzFrame = null;
                MFFrame pFrameOut = null;
                PresetInput();
                //   MFFrame pFrameTempOut = null;
                MF_FRAME_INFO mFrameInfo;
                string stimecode = "";
                while (true)
                {
                    try
                    {
                        if (m_bWork)
                        {
                            if (KeyingMode == true)
                            {
                                if (FlgSqzOn)
                                {
                                    avprops.vidProps.fccType = eMFCC.eMFCC_Default;
                                    myLiveSource.SourceFrameConvertedGet(ref avprops, -1, out BGFrame, "");             
                                }
                                else
                                {
                                    ObjReader.SourceFrameConvertedGet(ref avprops, -1, out BGFrame, "");                                
                                }
                            }
                            else
                            {
                               
                                if (sourceMode == InputSourceMode.cardNDI)
                                {                                  
                                    myLiveSource.SourceFrameConvertedGet(ref avprops, -1, out BGFrame, "");
                                    //BGFrame.MFAVPropsGet(out avprops, out samples);                                 
                                }
                                else
                                {
                                    ObjReader.SourceFrameConvertedGet(ref avprops, -1, out BGFrame, "");
                                }
                                if (insertscte)
                                {
                                    try
                                    {
                                        BGFrame.MFStrSet("ANC.Line.10", SCTE104Packet);
                                    }
                                    catch (Exception ex)
                                    {
                                        objlog.WriteError("DoWork()", "error while putting scte packet : ", ex.ToString());

                                    }
                                    finally
                                    {
                                        insertscte = false;
                                    }

                                }
                                //////////////////// Start Video Detection  ////////////
                                CompareCRC(BGFrame);
                                ////////////////////// End Video Detection ////////////////
                            }

                            //    CopyPluggin(m_objCharGenTemp, ref BGFrame, ref pFrameTempOut);
                            if (ReaderPlay == false)
                            {
                                if (gFxsqueeze)
                                {
                                    ShowPluggin(m_objCharGen, ref BGFrame);
                                    ShowPluggin(m_objCharGen_Lband, ref BGFrame);
                                }
                                else
                                {
                                    ShowPluggin(m_objCharGen_Lband, ref BGFrame);
                                    if (Htmlshow)
                                        ShowPluggin(m_objOverlayHTML, ref BGFrame);
                                    ShowPluggin(m_objCharGen, ref BGFrame);                                  
                                }
                                ShowPluggin(m_objCharGen_Logo, ref BGFrame);
                            }
                            else
                            {
                                OverLayFrameLband(m_objCharGen_Lband, ref BGFrame);
                                ShowPluggin(m_objCharGen_Logo, ref BGFrame);
                            }
                            ///////////////////////////////stream///////////////////////////
                            IsNetworkRenderdOn(ref BGFrame);
                            // releaseComObj(oframe);                                 
                            ////////////////////end stream/////////////////////////////////
                            if (IsShow)
                            {
                               m_objPreview.ReceiverFramePut(BGFrame, -1, "");
                                //  if(isPreview)  
                                //m_objPreviewTemp.ReceiverFramePut(pFrameTempOut, -1, ""); 
                              /*  if ((SubtitleEnable == true) &&(ObjSubtitle.bplay == true))
                                {
                                    BGFrame.MFAllGet(out mFrameInfo);
                                    stimecode = mFrameInfo.mTime.tcFrame.nHours.ToString() + ":" + mFrameInfo.mTime.tcFrame.nMinutes.ToString() + ":" + mFrameInfo.mTime.tcFrame.nSeconds.ToString() + ":" + mFrameInfo.mTime.tcFrame.nFrames.ToString();
                                    try
                                    {
                                        objlog.WriteEvents("VITC Timecode = ", stimecode, ChannelName);
                                      //  this.objlog.mFrameTC = stimecode;
                                    }
                                    catch (Exception ex)
                                    {
                                        objlog.WriteError("DoWork", "Error-- " + ex.ToString(), ChannelName);
                                    }
                                }*/
                            }
                          //  releaseComObj(pFrameTempOut);
                           
                            if (ISNDI)
                            {
                                outrender.SourcePreviewOut(BGFrame);
                                if (counterKeyer == "start")
                                {
                                    Thread.Sleep(100);
                                    SendUDPPacket("start");
                                    counterKeyer = "g";
                                }
                            }
                            releaseComObj(BGFrame);
                            releaseComObj(SdiSqzFrame);
                            releaseComObj(pFrameOut);
                            garbagecounter++;
                          // Thread.Sleep(5);
                            if (garbagecounter > 10)
                            {
                                GC.Collect();
                                garbagecounter = 0;
                                Thread.Sleep(1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if(sourceMode != InputSourceMode.cardNDI)
                        {
                            framecounter++;
                            if(framecounter == 2)
                            {
                                framecounter = 0;
                                setrender.GetDirtualSourceName();
                                // setrender.SetDeviceInput("VIRTUAL", setrender.DeviceName);
                                SourceAutoStart();
                            }
                        }
                        Thread.Sleep(1);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public MFSink GetSink()
        {
            return m_objMFSink;
        }
        public void SendUDPPacket(string _Packet)
        {
            UdpClient UdpClient;
            try
            {
                UdpClient = new UdpClient("127.0.0.1", KeyerPort);
                byte[] SendBytes = Encoding.UTF8.GetBytes(_Packet);
                UdpClient.Send(SendBytes, SendBytes.Length);
                //  objlog.WriteEvents("SendUDPPacket", "Sending Information To Udp =" + _Packet, ChannelName);
            }
            catch (Exception ex)
            {
                objlog.WriteError("SendUDPPacket", "Error In Sending Packet" + _Packet + ex.ToString(), ChannelName);
            }
        }

        #region Video Squeeze
        //topmostvideo flg is 0 when animation file play background and live play is up to animation.
        int TopMostVideo = 1;
        private void lbandon(string graphics,bool check = false)
        {
            try
            {
                MLCHARGENLib.tagRECT trSource = new MLCHARGENLib.tagRECT();
                trSource.top = 0;
                trSource.left = 0;
                trSource.bottom = 0;
                trSource.right = 0;
                string param2 = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault()["Param2"].ToString();
                string[] arrparam2 = param2.Split(',');

                // I specify the destination rectangle assuming that video resolution is 1920x1080 (408,0,1920,808)(142/152,0,720,452)//for SD
                MLCHARGENLib.tagRECT trTarget = new MLCHARGENLib.tagRECT();
                trTarget.left = Convert.ToInt32(arrparam2[0]);
                trTarget.top = Convert.ToInt32(arrparam2[1]);
                trTarget.right = Convert.ToInt32(arrparam2[2]);
                trTarget.bottom = Convert.ToInt32(arrparam2[3]);

                string param3 = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault()["Param3"].ToString();
                int durationtochangedimensions = Convert.ToInt32(param3);
                if (ReaderTemplateName.ToUpper() == graphics.ToUpper())
                {
                    ReaderPlay = true;
                    objlog.WriteEvents("LBAND_ON", "Reader Play flg Start for running..", ChannelName);
                    Task.Run(() => animation("START"));
                }
                if (!check)
                // Transform a video from the source to the destination rectangle within "durationtochangedimensions" milisecond.                                      
                {
                    m_objCharGen_Lband.SetVideoOutputRectWithDelay(trSource, trTarget, 0, TopMostVideo, 0, durationtochangedimensions);
                    load.lb_playingtemplatelist.Items.Add(graphics);
                }
                else
                {
                    m_objCharGenTemp.SetVideoOutputRectWithDelay(trSource, trTarget, 0, TopMostVideo, 0, durationtochangedimensions);
                    load.lb_playingtemplatelist.Items.Add(graphics);
                }

              

            }
            catch (Exception ex)
            {
                objlog.WriteError("LBAND_ON", "Error in changing dimensions " + ex.ToString(), ChannelName);
            }
        }
        private void lbandkill(string graphics,bool PorR=false)
        {
            try
            {
                if (load.lb_playingtemplatelist.Items.Contains(graphics))
                {
                    MLCHARGENLib.tagRECT trSource = new MLCHARGENLib.tagRECT();
                    MLCHARGENLib.tagRECT trTarget = new MLCHARGENLib.tagRECT();
                    string param4 = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault()["Param4"].ToString();
                    //  int durationtochangedimensions = Convert.ToInt32(param4);
                    int durationtochangedimensions = 0;
                    if (!int.TryParse(param4, out durationtochangedimensions))
                        durationtochangedimensions = 600;
                    if (!PorR)
                    m_objCharGen_Lband.SetVideoOutputRectWithDelay(trSource, trTarget, 0, TopMostVideo, 0, durationtochangedimensions);
                    else
                    m_objCharGenTemp.SetVideoOutputRectWithDelay(trSource, trTarget, 0, TopMostVideo, 0, durationtochangedimensions);
                    load.lb_playingtemplatelist.Items.Remove(graphics);
                    Thread.Sleep(durationtochangedimensions + 200);
                    objlog.WriteEvents("LBAND_OFF", "Changed to normal dimensions", ChannelName);
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("LBAND_Kill", "Error in changing dimensions " + ex.ToString(), ChannelName);
            }


        }
        #endregion
        public void ShowPluggin(object _obj, ref MFFrame pFrame)
        {
            try
            {
                if (_obj == null) return;
                int nFramesRes;
                MFFrame pFrameOut = null;
                ((MFORMATSLib.IMFProcess)_obj).ProcessFrame(pFrame, out pFrameOut, out nFramesRes, "");
                if (pFrameOut != null)
                {
                    releaseComObj(pFrame);
                    pFrame = pFrameOut;

                }
            }
            catch { }
        }
        public void CopyPluggin(object _obj, ref MFFrame pFrame,ref MFFrame pFrameOut)
        {
            try
            {
                if (_obj == null) return;
                int nFramesRes;      
                ((MFORMATSLib.IMFProcess)_obj).ProcessFrame(pFrame, out pFrameOut, out nFramesRes, "");
            }
            catch
            {
            }
        }
        public bool IsNetworkRenderdOn(ref MFFrame pFrame)
        {
           
            try
            {
                if (NetworkMode.ToUpper() == "TRUE" && ((StreamMode == StreamStatus.ready || StreamMode == StreamStatus.playing) && NetworkStreamWriter != null))
                {
                    NetworkStreamWriter.mWriter.ReceiverFramePut(pFrame,0, "");
                    //  NetworkStreamWriter.mWriter.WriterSet
                    if (StreamMode == StreamStatus.ready)
                    {
                        outrender.Updatecheckbox();
                    }
                    StreamMode = StreamStatus.playing;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                NetworkStreamWriter.mWriter.ReceiverFramePut(pFrame, 0, "");
                //  outrender.NtwrkStrmchbox.CheckState = CheckState.Unchecked;
                //  StreamMode = StreamStatus.stoped;
                objlog.WriteError("IsNetworkRenderdOn","Exception in rendering network stream." + ex.ToString(), ChannelName);
                return false;
            }
        }
        private void TakeCgOutPut(object _obj, ref MFFrame SdiSqzFrame)
        {
            int nFramesRes;
            MFFrame pFrameOut = null;
            try
            {
                avprops.vidProps.fccType = eMFCC.eMFCC_ARGB32;
                mfc.MFFrameCreateFromMem(ref avprops, 0, 0, 0, out SdiSqzFrame, "");
                ((MFORMATSLib.IMFProcess)m_objCharGen).ProcessFrame(SdiSqzFrame, out pFrameOut, out nFramesRes, "");
                if (pFrameOut != null)
                {
                    releaseComObj(SdiSqzFrame);
                    SdiSqzFrame = pFrameOut;

                }
            }
            catch (Exception)
            {
            }
        }

        double rotatenumber = 0;


        private void animation(string eventtype)
        {
            if (eventtype == "STOP")
            {
                AnimationSpeed = EndAnimationSpeed;
            }
            switch (objPmslReader.ANIMATION)
            {
                case "RIGHTTOLEFT":
                    {
#if UTC_MODE
                        var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distence = (objPmslReader.ObjVirRec.dblPosX - objPmslReader.recTarget.dblPosX) / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();



#if UTC_MODE
                          while (DateTime.UtcNow < now.AddMilliseconds(AnimationSpeed))
                        {
      DateTime start = DateTime.UtcNow;
#else
                        while (DateTime.Now < now.AddMilliseconds(AnimationSpeed))
                        {
                            DateTime start = DateTime.Now;
#endif
                            if (objPmslReader.ObjVirRec.dblPosX > objPmslReader.recTarget.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX - distence * tm.TotalMilliseconds;
                                ttol = ttol.Add(tm);
                                if ((objPmslReader.ObjVirRec.dblPosX - objPmslReader.recTarget.dblPosX) > 0)
                                {
                                    objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;

                                }
                                else
                                {
                                    break;
                                }
                                Task.Delay(4);
                                // objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + tm.TotalMilliseconds.ToString(), ChannelName);
                            }
                            //  tm = DateTime.Now - start;
#if UTC_MODE
     tm = DateTime.UtcNow - start;
#else
                            tm = DateTime.Now - start;
#endif
                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objlog.WriteEvents("animation", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                        break;
                    }
                case "RIGHTTOLEFTOUT":
                    {

#if UTC_MODE
      var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distence = (objPmslReader.ObjVirRec.dblPosX - objPmslReader.recTarget.dblPosX) / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();
#if UTC_MODE
      while (DateTime.UtcNow < now.AddMilliseconds(AnimationSpeed))
                        {

                            DateTime start = DateTime.UtcNow;
#else
                        while (DateTime.Now < now.AddMilliseconds(AnimationSpeed))
                        {

                            DateTime start = DateTime.Now;
#endif

                            if (objPmslReader.ObjVirRec.dblPosX > objPmslReader.recTarget.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX - distence * tm.TotalMilliseconds;
                                ttol = ttol.Add(tm);
                                if ((objPmslReader.ObjVirRec.dblPosX - objPmslReader.recTarget.dblPosX) > 0)
                                {
                                    objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;

                                }
                                else
                                {
                                    break;
                                }
                                Task.Delay(4);
                                // objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + tm.TotalMilliseconds.ToString(), ChannelName);
                            }
                         //   tm = DateTime.Now - start;
#if UTC_MODE
       tm = DateTime.UtcNow - start;
#else
                            tm = DateTime.Now - start;
#endif
                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objlog.WriteEvents("animation", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                        break;
                    }
                case "LEFTTORIGHT":
                    {

#if UTC_MODE
       var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distence = (objPmslReader.ObjRec.dblWidth) / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();
#if UTC_MODE
       while (DateTime.UtcNow < now.AddMilliseconds(AnimationSpeed))
                        {
                            DateTime start = DateTime.UtcNow;
#else
                        while (DateTime.Now < now.AddMilliseconds(AnimationSpeed))
                        {
                            DateTime start = DateTime.Now;
#endif

                            if (objPmslReader.recTarget.dblPosX > objPmslReader.ObjVirRec.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX + distence * tm.TotalMilliseconds;
                                ttol = ttol.Add(tm);
                                if (objPmslReader.ObjVirRec.dblPosX < objPmslReader.recTarget.dblPosX)
                                {
                                    objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;
                                    Task.Delay(4);
                                    //  objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                                }
                                else
                                {
                                    break;
                                }
                            }

#if UTC_MODE
       tm = DateTime.UtcNow - start;
#else
        tm = DateTime.Now - start;
#endif
                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objlog.WriteEvents("animation", "final X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);

                        break;
                    }
                case "LEFTTORIGHTIN":
                    {

#if UTC_MODE
        var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distence = (objPmslReader.ObjRec.dblWidth) / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();
#if UTC_MODE
          while (DateTime.UtcNow < now.AddMilliseconds(AnimationSpeed))
#else
                        while (DateTime.Now < now.AddMilliseconds(AnimationSpeed))
#endif

                        {

#if UTC_MODE
         DateTime start = DateTime.UtcNow;
#else
                            DateTime start = DateTime.Now;
#endif
                            if (objPmslReader.recTarget.dblPosX > objPmslReader.ObjVirRec.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX + distence * tm.TotalMilliseconds;
                                ttol = ttol.Add(tm);
                                if (objPmslReader.ObjVirRec.dblPosX < objPmslReader.recTarget.dblPosX)
                                {
                                    objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;
                                    Task.Delay(4);
                                    //  objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                                }
                                else
                                {
                                    break;
                                }
                            }

#if UTC_MODE
         tm = DateTime.UtcNow - start;
#else
                            tm = DateTime.Now - start;
#endif
                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objlog.WriteEvents("animation", "final X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);

                        break;
                    }
                case "DIAGONALIN":
                    {

#if UTC_MODE
           var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distenceX = ((-objPmslReader.ObjVirRec.dblPosX) + objPmslReader.recTarget.dblPosX) / AnimationSpeed;
                        double distenceY = ((-objPmslReader.ObjVirRec.dblPosY) + objPmslReader.recTarget.dblPosY) / AnimationSpeed;
                        double rotate = RotateNumber / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();
                        bool xcomplete = false;
                        bool ycomplete = false;
#if UTC_MODE
           DateTime p1= DateTime.UtcNow;
#else
                        DateTime p1 = DateTime.Now;
#endif
                        while (p1 < now.AddMilliseconds(AnimationSpeed))
                        {

#if UTC_MODE
           DateTime start = DateTime.UtcNow;
#else
                            DateTime start = DateTime.Now;
#endif
                            if (objPmslReader.recTarget.dblPosX > objPmslReader.ObjVirRec.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX + distenceX * tm.TotalMilliseconds;
                                objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;
                            }
                            else
                            {
                                if (xcomplete == false) { objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX; }
                                xcomplete = true;
                            }
                            // objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                            if (objPmslReader.recTarget.dblPosY > objPmslReader.ObjVirRec.dblPosY)
                            {
                                objPmslReader.ObjVirRec.dblPosY = objPmslReader.ObjVirRec.dblPosY + distenceY * tm.TotalMilliseconds;
                                objPmslReader.ObjRec.dblPosY = objPmslReader.ObjVirRec.dblPosY;
                            }
                            else
                            {

                                if (ycomplete == false) { objPmslReader.ObjRec.dblPosY = objPmslReader.recTarget.dblPosY; }
                                ycomplete = true;
                            }
                            if (RotateNumber > rotatenumber)
                            {
                                rotatenumber = rotatenumber + rotate * tm.TotalMilliseconds;
                            }
                            else
                            {
                                rotatenumber = RotateNumber;
                            }
                            //  objlog.WriteEvents("test", "Y-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                            Task.Delay(4);

#if UTC_MODE
           tm = DateTime.UtcNow - start;
#else
                            tm = DateTime.Now - start;
#endif
                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objPmslReader.ObjRec.dblPosY = objPmslReader.recTarget.dblPosY;
                        rotatenumber = RotateNumber;
                        break;
                    }
                case "DIAGONALOUT":
                    {
                     
#if UTC_MODE
           var now = DateTime.UtcNow;
#else
                        var now = DateTime.Now;
#endif
                        double distenceX = ((objPmslReader.ObjRec.dblWidth) + objPmslReader.ObjRec.dblPosX) / AnimationSpeed;
                        double distenceY = ((objPmslReader.ObjRec.dblHeight) + objPmslReader.ObjRec.dblPosY) / AnimationSpeed;
                        double rotate = RotateNumber / AnimationSpeed;
                        TimeSpan tm = new TimeSpan();
                        TimeSpan ttol = new TimeSpan();
                        bool xcomplete = false;
                        bool ycomplete = false;
                        rotatenumber = RotateNumber;
                      
#if UTC_MODE
         DateTime t1 = DateTime.UtcNow;
#else
                        DateTime t1 = DateTime.Now;
#endif
                        while (t1 < now.AddMilliseconds(AnimationSpeed))
                        {

#if UTC_MODE
         DateTime start = DateTime.UtcNow;
#else
                            DateTime start = DateTime.Now;
#endif
                            if (objPmslReader.recTarget.dblPosX < objPmslReader.ObjVirRec.dblPosX)
                            {
                                objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX - distenceX * tm.TotalMilliseconds;
                                objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;
                            }
                            else
                            {
                                if (xcomplete == false) { objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX; }
                                xcomplete = true;
                            }
                            //  objlog.WriteEvents("test", "X-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                            if (objPmslReader.recTarget.dblPosY < objPmslReader.ObjVirRec.dblPosY)
                            {
                                objPmslReader.ObjVirRec.dblPosY = objPmslReader.ObjVirRec.dblPosY - distenceY * tm.TotalMilliseconds;
                                objPmslReader.ObjRec.dblPosY = objPmslReader.ObjVirRec.dblPosY;
                            }
                            else
                            {

                                if (ycomplete == false) { objPmslReader.ObjRec.dblPosY = objPmslReader.recTarget.dblPosY; }
                                ycomplete = true;
                            }
                            //  objlog.WriteEvents("test", "Y-pos=" + objPmslReader.ObjRec.dblPosX.ToString() + " milisecond" + ttol.TotalMilliseconds.ToString(), ChannelName);
                            if (0 < rotatenumber)
                            {
                                rotatenumber = rotatenumber - rotate * tm.TotalMilliseconds;
                            }
                            else
                            {
                                rotatenumber = 0;
                            }
                            Task.Delay(4);
#if UTC_MODE
          tm = DateTime.UtcNow - start;
#else
                            tm = DateTime.Now - start;
#endif

                        }
                        objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                        objPmslReader.ObjRec.dblPosY = objPmslReader.recTarget.dblPosY;
                        rotatenumber = 0;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            if (eventtype.ToUpper().Contains("STOP"))
            {
                ReadTriggerFile("", "AFTERSTOP");
                ReaderPlay = false;
                startanimation = false;

            }


        }




        private void ManageRotateAngle(string AnimationType)
        {
            switch (AnimationType.ToUpper())
            {
                case "RIGHTTOLEFT":
                    if (startanimation == false)
                    {
                        if (objPmslReader.ObjVirRec.dblPosX > objPmslReader.recTarget.dblPosX)
                        {
                            objPmslReader.ObjVirRec.dblPosX = objPmslReader.ObjVirRec.dblPosX - AnimationSpeed;

                            if ((objPmslReader.ObjVirRec.dblPosX - objPmslReader.recTarget.dblPosX) > 0)
                            {
                                objPmslReader.ObjRec.dblPosX = objPmslReader.ObjVirRec.dblPosX;
                            }
                            else
                            {
                                objPmslReader.ObjRec.dblPosX = objPmslReader.recTarget.dblPosX;
                            }

                        }

                    }
                    break;
                case "LEFTTORIGHT":
                    if (startanimation == true)
                    {
                        if (objPmslReader.ObjRec.dblPosX < objPmslReader.ObjVirRec.dblPosX)
                        {
                            objPmslReader.ObjRec.dblPosX = objPmslReader.ObjRec.dblPosX + AnimationSpeed;

                        }
                        else
                        {
                            ReaderPlay = false;
                            startanimation = false;
                            ReadTriggerFile("", "AFTERSTOP");
                        }
                    }
                    break;
                case "Trangle":
                    break;
                default:
                    break;
            }


        }
        MFFactoryClass mfc = new MFFactoryClass();
        private void OverLayFrameLband(object _obj, ref MFFrame pFrame)
        {
            try
            {
                if (_obj == null) return;
                int nFramesRes;
                MFFrame pFrameOut = null;
                MFFrame SdiSqzFrame = null;
                ((MFORMATSLib.IMFProcess)_obj).ProcessFrame(pFrame, out pFrameOut, out nFramesRes, "");
                objPmslReader.OverLayFrame(ref avprops, ref pFrameOut);

                if (pFrameOut != null)
                {
                    if (SqzType == "TYPE2")
                    {
                        mfc.MFFrameCreateFromMem(ref avprops, 0, 0, 0, out SdiSqzFrame, "");
                        ShowPluggin(m_objCharGen, ref SdiSqzFrame);
                        // MFFrame outFrame = null;
                        // pFrameOut.MFClone(out outFrame, eMFrameClone.eMFC_Reference, eMFCC.eMFCC_Default);
                        SdiSqzFrame.MFOverlayRect(pFrameOut, null, ref RotateRec, eMFOverlayFlags.eMFOF_Crop, 1, $"audio_gain=0.0 symmetric_gain=false  rotate.angle={rotatenumber} rotate.scale={ScaleNumber} rotate.crop=false", "");
                        pFrameOut = SdiSqzFrame;
                    }
                    releaseComObj(pFrame);
                    pFrame = pFrameOut;
                }

            }
            catch { }
        }



        private void RotateFrame(ref MFFrame pFrame, ref MFFrame bgFrame)
        {

            try
            {
                if (pFrame != null)
                {
                    // for MNXMOVIES
                    //MF_RECT rec = new MF_RECT();
                    //rec.dblPosX = 0;
                    //rec.dblPosY = -100;
                    //rec.dblWidth = 1920;
                    //rec.dblHeight = 1080;
                    ////1064,450,1854,1018
                    //bgFrame.MFOverlayRect(pFrame, null, ref rec, eMFOverlayFlags.eMFOF_Crop, 1, $"audio_gain=0.0 symmetric_gain=false  rotate.angle={rotatenumber} rotate.scale=1.2 rotate.crop=false", "ch2");
                    //releaseComObj(pFrame);
                    //pFrame = bgFrame;
                    MF_RECT rec = new MF_RECT();
                    rec.dblPosX = 0;
                    rec.dblPosY = 0;
                    rec.dblWidth = 720;
                    rec.dblHeight = 576;
                    //1064,450,1854,1018
                    pFrame.MFOverlayRect(bgFrame, null, ref rec, eMFOverlayFlags.eMFOF_Crop, 1, "", "");
                    releaseComObj(pFrame);
                    pFrame = bgFrame;



                }
            }
            catch { }
        }

#region Action on cg items        
        internal void KillAllCgItem()
        {
            try
            {
                //+++++++++++++++++++++Working On m_objCharGen Object+++++++++++++++++++++
                int numberOfItems = 0;
                m_objCharGen.CompositionsGetCount(out numberOfItems);
                objlog.WriteEvents("KillAllCgItem", "Load Total Template Name = " + numberOfItems, ChannelName);
                for (int i = 0; i < numberOfItems; i++)
                {
                    string CompositionName = "", Comment = "";
                    m_objCharGen.CompositionsGetByIndex(0, out CompositionName, out Comment);
                    objlog.WriteEvents("KillAllCgItem", "Template Name = " + CompositionName, ChannelName);
                    int CountItem = 0;
                    m_objCharGen.CompositionsGetItemsCount(CompositionName, ref CountItem);
                    for (int j = 0; j < CountItem; j++)
                    {
                        string GfxElementId = "";
                        string XmlDesc = "";
                        m_objCharGen.CompositionsGetItem(CompositionName, j, out GfxElementId, out XmlDesc);
                        objlog.WriteEvents("KillAllCgItem", "Template Name = " + CompositionName + " , Element Id=" + GfxElementId, ChannelName);
                        m_objCharGen.RemoveItem(GfxElementId, 300);

                    }
                    m_objCharGen.CompositionsRemove(CompositionName);
                    objlog.WriteEvents("KillAllCgItem", "Remove Template Name = " + CompositionName, ChannelName);
                    load.lb_playingtemplatelist.Items.Remove(CompositionName.ToUpper());
                    UsedItems.Remove(CompositionName.ToUpper());
                }
                //+++++++++++++++++++++Working On m_objCharGen_Lband Object+++++++++++++++++++++
                int ComCount = 0;
                m_objCharGen_Lband.CompositionsGetCount(out ComCount);
                for (int k = 0; k < ComCount; k++)
                {
                    string CompositionName = "", Comment = "";
                    m_objCharGen_Lband.CompositionsGetByIndex(0, out CompositionName, out Comment);
                    lbandkill(CompositionName.ToUpper());
                    int ItemCount = 0;
                    m_objCharGen_Lband.CompositionsGetItemsCount(CompositionName, ref ItemCount);
                    for (int l = 0; l < ItemCount; l++)
                    {
                        string GfxElementId = "";
                        string XmlDesc = "";
                        m_objCharGen_Lband.CompositionsGetItem(CompositionName, l, out GfxElementId, out XmlDesc);
                        m_objCharGen_Lband.RemoveItem(GfxElementId, 300);
                    }

                    m_objCharGen_Lband.CompositionsRemove(CompositionName);
                    load.lb_playingtemplatelist.Items.Remove(CompositionName.ToUpper());
                    UsedItems.Remove(CompositionName.ToUpper());
                }
                objlog.WriteEvents("KillAllCgItem", "All Cg items are removed", ChannelName);
            }
            catch (Exception ex)
            {
                objlog.WriteError("KillAllCgItem", "Error --" + ex.ToString(), ChannelName);
            }
        }
        private double CycleTime(string timelinepath)
        {
            try
            {
                XmlReader reader = XmlReader.Create(timelinepath);
                while (reader.Read())
                {
                    reader.MoveToAttribute("cycle-time");
                    string cycletime = reader.Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(cycletime))
                    {
                        return Convert.ToDouble(cycletime);
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }


        private void CheckTemplateRunningInInternal(string templatename)
        {
            try
            {
                int count = 0;
                string compositionname = "", comment = "";
                m_objCharGen.CompositionsGetCount(out count);
                for (int i = 0; i < count; i++)
                {
                    m_objCharGen.CompositionsGetByIndex(i, out compositionname, out comment);
                    if (compositionname.ToLower() == templatename)
                    {
                        int ComItemCount = 0;
                        string gfxitem = "", xml = "";
                        m_objCharGen.CompositionsGetItemsCount(compositionname, ComItemCount);
                        for (int j = 0; j < ComItemCount; j++)
                        {
                            m_objCharGen.CompositionsGetItem(compositionname, 0, out gfxitem, out xml);
                            // m_objCharGen.RemoveItemWithDelay(itemID, 0, 500);                               
                            m_objCharGen.RemoveItem(gfxitem, 0);
                            m_objCharGen.CompositionsRemoveItem(compositionname, gfxitem);
                        }
                        objlog.WriteEvents("LoadCgitems", $"Template Name=>{templatename} is load found kill by automation for playing same template another", ChannelName);
                        m_objCharGen.CompositionsRemove(templatename);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                objlog.WriteError("LoadCgitems", "Error in find and check template in backent " + ex.ToString(), ChannelName);
            }

        }
        
        public void SaveLogoFileDetail(string TemplateName, string templateValue)
        {
            try
            {
                DataTable Dt = new DataTable("FreezeTemplate");
                Dt.Columns.Add("TemplateName");
                Dt.Columns.Add("param1ID");
                DataRow Dr = Dt.NewRow();
                Dr["TemplateName"] = TemplateName;
                Dr["param1ID"] = templateValue.Trim();
                Dt.Rows.Add(Dr);
                Dt.WriteXml(Application.StartupPath + $"//Channels//{ChannelName}//{ChannelName}_FreezeTemplate.xml");
                if(templateValue!="" && !string.IsNullOrEmpty(templateValue))
                writeHistorydata(templateValue);
            }
            catch (Exception ex)
            {
               objlog.WriteError("SaveLogoFileDetail", "Save Logo File Detail in main server" + ex.Message,ChannelName);
            }
        }
        double _loadcgIntroTime = 0.5;
        double _loadcgExitTime = 0.3;
        string _loadcgBugType = "";
        int _loadcgLayerNO = 5;
        bool _loadcgEnableDuration = false;
        internal bool LoadCgItems(string graphics, string Duration = "00:00:00:00",bool prevon=false)
        {
            string templatepath = TemplatePath + "\\" + graphics + ".ml-cgs";
            string timelinepath = TemplatePath + "\\" + graphics + ".ml-cgst";
            ReadTriggerFile(graphics, "START");
            if (!File.Exists(templatepath))
            {
                if (File.Exists(TemplatePath + "\\" + graphics + ".tgr"))
                {
                    return true;
                }
                objlog.WriteEvents("LoadCgitems", $"Template Name=>{graphics} ,file not found {templatepath}", ChannelName);
                return false;
            }
            lock (objreceive)
            {
                try
                {
                    //Start Retreve data for configration;
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        _loadcgBugType = (dr["BugType"] + "").Trim();
                        _loadcgLayerNO = Convert.ToInt32((dr["LayerNo"] + "").Trim());
                        _loadcgEnableDuration = Convert.ToBoolean((dr["DurationBased"] + "") == string.Empty ? "False" : (dr["DurationBased"] + ""));
                        _loadcgIntroTime = Convert.ToDouble((dr["IntroTime"] + "") == string.Empty ? "0.5" : (dr["IntroTime"] + ""));
                        _loadcgExitTime = Convert.ToDouble((dr["ExitTime"] + "") == string.Empty ? "0.3" : (Convert.ToDouble(dr["ExitTime"]) / 1000).ToString());
                    }
                    if (_loadcgBugType == "VIDEO-SQZ")
                    {
                        if (UsedItems.Contains(graphics.ToUpper()))
                        {
                            var rows = from DataGridViewRow row in dgvsecevent.Rows
                                       where string.Compare(row.Cells["SecStatus"].Value + "", "R", true) == 0 && string.Compare(row.Cells["SecTemplate"].Value + "", graphics, true) == 0
                                       select row;
                            foreach (var item in rows)
                            {
                                item.Cells["SecDeleteCheck"].Value = "A";
                            }
                            KillCgItem(graphics, _loadcgBugType, 0, prevon);
                            Thread.Sleep(Convert.ToInt32(_loadcgExitTime));
                            Thread.Sleep(500);

                        }
                        if (!prevon)
                        {
                           if (m_objCharGen_Lband == null)
                            {
                                m_objCharGen_Lband = new CoMLCharGenClass();
                             //   m_objCharGen_Lband.OnEndOfClip += M_objSqz_OnEndOfClip;
                                Thread.Sleep(300);
                            }
                            m_objCharGen_Lband.CompositionsLoadAll(templatepath, 0);
                            m_objCharGen_Lband.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0);
                            ActivateSquzFlag();
                            lbandon(graphics);
                            UsedItems.Add(graphics);
                        }
                        else
                        {
                            if (m_objCharGenTemp == null)
                            {
                                m_objCharGenTemp = new CoMLCharGenClass();
                            }
                            m_objCharGenTemp.CompositionsLoadAll(templatepath, 0);
                            m_objCharGenTemp.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0);
                            ActivateSquzFlag();
                            lbandon(graphics, prevon);
                            UsedItems.Add(graphics);
                        }
                    }
                    else if (graphics.ToUpper() == "LOGO" || graphics.ToUpper() == "FESTLOGO" || (_loadcgBugType + "").ToUpper() == "LOGO")
                    {
                        if (UsedItems.Contains(graphics.ToUpper()))
                        {
                            var rows = from DataGridViewRow row in dgvsecevent.Rows
                                       where string.Compare(row.Cells["SecStatus"].Value + "", "R", true) == 0 && string.Compare(row.Cells["SecTemplate"].Value + "", graphics, true) == 0
                                       select row;
                            foreach (var item in rows)
                            {
                                item.Cells["SecDeleteCheck"].Value = "A";
                            }
                            KillCgItem(graphics, _loadcgBugType, 0, prevon);
                            Thread.Sleep(Convert.ToInt32(_loadcgExitTime));
                            Thread.Sleep(500);
                        }
                        //  String Templatestarttime = DateTime.Now.ToString("HH:mm:ss") + ":00";
                      //  String Templatestarttime = objlog.LTCTime.Trim();
                      //  LogoTemplatestore.Add(graphics.ToUpper(), Templatestarttime);
                        string logoname = "";
                        string logotype = "";
                        string tmpname = "";
                        objlog.WriteEvents("LoadCgitems", $"Template Name=>{graphics} finding latest logo item", ChannelName);
                        if ((findlatestLogo(out logoname, out logotype, out tmpname)))
                            objAgent.PrepareAsRunSetting("START","", "24:00:00:00", logoname, graphics);
                        if (!prevon)
                        {
                            if (m_objCharGen_Logo == null)
                            {
                                m_objCharGen_Logo = new CoMLCharGenClass();
                            }
                            m_objCharGen_Logo.CompositionsLoadAll(templatepath, 0);
                            m_objCharGen_Logo.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0); //for animation there should be delay//3rd value is for delay //0.5      
                            StartTimeline(m_objCharGen_Logo, templatepath, timelinepath);
                            UsedItems.Add(graphics);
                            load.lb_playingtemplatelist.Items.Add(graphics);
                        }
                        /////////////////////////////////////////DDK/////////////////////////////////
                        else
                        {

                            if (m_objCharGenTemp == null)
                            {
                                m_objCharGenTemp = new CoMLCharGenClass();
                            }
                            m_objCharGenTemp.CompositionsLoadAll(templatepath, 0);
                            m_objCharGenTemp.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0); //for animation there should be delay//3rd value is for delay //0.5      
                            StartTimeline(m_objCharGenTemp, templatepath, timelinepath);
                            UsedItems.Add(graphics);
                            load.lb_playingtemplatelist.Items.Add(graphics);
                        }
                        ////////////////////////////////////end//////////////////////////////////////
                    }
                    else if (_loadcgBugType != "VIDEO-SQZ")
                    {
                        if (UsedItems.Contains(graphics.ToUpper()))
                        {
                            var rows = from DataGridViewRow row in dgvsecevent.Rows
                                       where string.Compare(row.Cells["SecStatus"].Value + "", "R", true) == 0 && string.Compare(row.Cells["SecTemplate"].Value + "", graphics, true) == 0
                                       select row;
                            foreach (var item in rows)
                            {
                                item.Cells["SecDeleteCheck"].Value = "A";
                            }
                            KillCgItem(graphics, _loadcgBugType, 0);
                            Thread.Sleep(Convert.ToInt32(_loadcgExitTime * 1000));
                            Thread.Sleep(500);
                        }
                        if (!prevon)
                        {
                            if (m_objCharGen == null)
                            {
                                m_objCharGen = new CoMLCharGenClass();
                            }
                               m_objCharGen.CompositionsLoadAll(templatepath, 0);                         
                           /* string content = File.ReadAllText(templatepath);
                            string result = content.Replace("&lt;", "<");
                            string path = Path.GetDirectoryName(templatepath)+"\\"+ Path.GetFileNameWithoutExtension(templatepath) + ".txt";
                            File.WriteAllText(path, content);
                            m_objCharGen.CompositionsLoadAll(path, 0);*/
                            if (_loadcgEnableDuration == false)
                            {
                                m_objCharGen.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0); //for animation there should be delay//3rd value is for delay //0.5 
                            }
                            else
                            {
                                double provideDuration = GetDurationInDouble(Duration);
                                m_objCharGen.CompositionsDisplay(graphics, "", _loadcgIntroTime, provideDuration, _loadcgExitTime); //for animation there should be delay//3rd value is for delay //0.5 
                            }
                            ChangeLayer(graphics, _loadcgLayerNO);
                            StartTimeline(m_objCharGen, templatepath, timelinepath);
                            UsedItems.Add(graphics);
                            load.lb_playingtemplatelist.Items.Add(graphics);
                        }
                        /////////////////////////////////////////DDK/////////////////////////////////
                        else
                        {

                            if (m_objCharGenTemp == null)
                            {
                                m_objCharGenTemp = new CoMLCharGenClass();

                            }
                            m_objCharGenTemp.CompositionsLoadAll(templatepath, 0);
                            m_objCharGenTemp.CompositionsDisplay(graphics, "", _loadcgIntroTime, 0, 0); //for animation there should be delay//3rd value is for delay //0.5      
                            StartTimeline(m_objCharGenTemp, templatepath, timelinepath);
                            UsedItems.Add(graphics);
                            load.lb_playingtemplatelist.Items.Add(graphics);
                        }
                        ////////////////////////////////////end//////////////////////////////////////
                    }


#if UTC_MODE
             FillEventLog(graphics + $" played at LTC- { objlog.LTCTime }, SystemTime- { DateTime.UtcNow.ToString("HH:mm:ss")} .", "G");
             objlog.WriteEvents("LoadCgitems", $"Template Name=>{graphics} played at LTC Time=>{objlog.LTCTime}, SystemTime {DateTime.UtcNow.ToString("HH:mm:ss")} ", ChannelName);
#else
                    FillEventLog(graphics + $" played at LTC- { objlog.LTCTime }, SystemTime- { DateTime.Now.ToString("HH:mm:ss")} .", "G");
                    objlog.WriteEvents("LoadCgitems", $"Template Name=>{graphics} played at LTC Time=>{objlog.LTCTime}, SystemTime {DateTime.Now.ToString("HH:mm:ss")} ", ChannelName);
#endif
                    return true;
                }
                catch (Exception ex)
                {
                    objlog.WriteEvents("LoadCGItem", "Error in display of cg item Template Name=>" + graphics + ex.ToString(), ChannelName);
                    objlog.WriteError("LoadCGItem", "Error in display of cg item Template Name=>" + graphics + ex.ToString(), ChannelName);
                    FillEventLog("Error in display of cg item", "E");
                    return false;
                }

            }
        }

        private void StartTimeline(CoMLCharGenClass OBJObject, string templatepath, string timelinepath)
        {
            try
            {
#region timeline
                if (File.Exists(timelinepath))
                {
                    OBJObject.ScheduleLoadFromXML(timelinepath, 0);//1 To remove all timelines                          
                    double cycletime = CycleTime(timelinepath);
                    int count;
                    OBJObject.ScheduleTimelineGetCount(out count);
                    string timelineid;
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(templatepath);
                    XmlNode node = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.Name.ToString());
                    string cgcompostitionname = node.Attributes["name"].Value.ToString();
                    for (int i = 0; i < count; i++)
                    {
                        OBJObject.ScheduleTimelineGetID(i, out timelineid);
                        if (timelineid.ToLower() == cgcompostitionname.ToLower())
                        {
                            OBJObject.ScheduleTimelineStart(timelineid, 0, cycletime, -1);

                        }
                    }
                }
#endregion
            }
            catch (Exception ex)
            {

                objlog.WriteError("StartTimeline", $"Error in Start Time Line File ={timelinepath} ," + ex.ToString(), ChannelName);
            }

        }

        private double GetDurationInDouble(string duration)
        {  //receive time formate "00:00:00"
            try
            {
                string[] arrTime = duration.Split(':');
                int melesecond = 0;
                if (arrTime.Length == 4)
                {
                    melesecond = Convert.ToInt16(arrTime[3]);
                    melesecond = melesecond * 40;
                }
                TimeSpan tm = new TimeSpan(0, Convert.ToInt16(arrTime[0]), Convert.ToInt16(arrTime[1]), Convert.ToInt16(arrTime[2]), melesecond);
                return tm.TotalSeconds;
            }
            catch (Exception ex)
            {
                objlog.WriteError("GetDurationInDouble", $"Error in converting time {duration} Error is -{ ex.ToString()}", ChannelName);
                return 0.5;
            }

        }




#region "Trigger Event"
        DataSet ds = null;
        public void ReadTriggerFile(string _TriggerFileName, string _EventType = "START")
        {
            try
            {
               if (_EventType == "START" || _EventType == "STOP")
                {
                    string TemplatePath = this.TemplatePath + @"\" + _TriggerFileName + ".tgr";
                    if (!File.Exists(TemplatePath))
                    {
                        if (ds != null)
                        {
                            ds = null;
                        }
                        return;
                    }
                    ds = new DataSet();
                    ds.ReadXml(TemplatePath);
                }
                int maxchangetime = 0;
                if (ds != null && ds.Tables.Count > 0)
                {
                    string _Type = "", ItemId = "", PropertyName = "", PropertyValue = "", GfxObject = "", Event = "";
                    foreach (DataRow DrItem in ds.Tables[0].Rows)
                    {
                        _Type = DrItem["Type"] + "";
                        ItemId = DrItem["ItemId"] + "";
                        PropertyName = DrItem["PropertyName"] + "";
                        PropertyValue = DrItem["PropertyValue"] + "";
                        int ChangeTime = Convert.ToInt32(DrItem["ChangeTime"] + "");
                        GfxObject = DrItem["GfxObject"] + "";
                        Event = DrItem["Event"] + "";
                        if (_EventType.ToUpper() != Event.ToUpper())
                        {
                            continue;
                        }
                        if (ChangeTime > maxchangetime)
                        {
                            maxchangetime = ChangeTime;
                        }

                        switch (_Type.ToUpper())
                        {
                            case "STATIC":
                                {
                                    TriggerEvent(ItemId, PropertyName, PropertyValue, ChangeTime, GfxObject);
                                    break;
                                }
                            case "LOADTEMPLATE":
                                {
                                    LoadCgItems(PropertyValue.ToUpper());
                                    break;
                                }
                            case "KILLTEMPLATE":
                                {
                                    KillCgItem(PropertyValue, "", 0);
                                    break;
                                }
                            case "HIDETEMPLATE":
                                {
                                    string[] TemplateVale = PropertyValue.Split(',');
                                    string templatename = TemplateVale.Length >= 1 ? (TemplateVale[0]).ToUpper() : "";
                                    string BugType = TemplateVale.Length >= 2 ? (TemplateVale[1]).ToUpper() : "";
                                    HideCgItem(templatename, BugType);
                                    break;
                                }
                            case "SHOWTEMPLATE":
                                {
                                    string[] TemplateVale = PropertyValue.Split(',');
                                    string templatename = TemplateVale.Length >= 1 ? (TemplateVale[0]).ToUpper() : "";
                                    string BugType = TemplateVale.Length >= 2 ? (TemplateVale[1]).ToUpper() : "";
                                    ShowCgItem(templatename, BugType);
                                    break;

                                }
                            case "SLEEP":
                                {
                                    int result = 0;
                                    int.TryParse(PropertyValue, out result);
                                    Thread.Sleep(result);
                                    break;
                                }
                            default:
                                {
                                    objlog.WriteEvents("ReadTriggerFile", $"  Type Not Matched Type Is-{_Type } but Required [VIDEO,IMAGE,STATIC] ", ChannelName);
                                    break;
                                }
                        }
                        if (_EventType.ToUpper() == "STOP" || _EventType.ToUpper() == "AFTERSTOP")
                        {
                            Thread.Sleep(maxchangetime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("ReadTriggerFile", $"Error In Process Template {_TriggerFileName} trigger File. " + ex.ToString(), ChannelName);
            }
        }


        public void TriggerEvent(string EventId, string Property, string value, int changeTime, string GfxObject)
        {
            try
            {
                dynamic GfxRunningObject = null;
                switch (GfxObject)
                {
                    case "LOGO":
                        {
                            GfxRunningObject = m_objCharGen_Logo;
                            break;
                        }
                    case "DEFAULT":
                        {
                            GfxRunningObject = m_objCharGen;
                            break;
                        }
                    case "SQUEEZEE":
                        {
                            GfxRunningObject = m_objCharGen_Lband;
                            break;
                        }
                    default:
                        {
                            GfxRunningObject = m_objCharGen;
                            break;
                        }
                }
                int ItemCount;
                (GfxRunningObject as CoMLCharGenClass).GetItemsCount(out ItemCount);
                for (int i = 0; i < ItemCount; i++)
                {
                    string strId = "";
                    (GfxRunningObject as CoMLCharGenClass).GetItem(i, out strId);
                    if (strId.ToLower() == EventId.ToString().ToLower())
                    {
                        (GfxRunningObject as CoMLCharGenClass).SetItemProperties(strId, Property, value, "", changeTime);
                        objlog.WriteEvents("TriggerEvent", $" Updation Trigger EventId-{strId} ,Property-{Property} , value -{value} and changetime-{changeTime} ", ChannelName);


                    }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("TriggerEvent", $"Error In Process Trigger File EventId-{EventId} ,Property-{Property} , value -{value}  " + ex.ToString(), ChannelName);
            }
        }

#endregion



        public void ChangeLayer(string _CurrentTemplateName, int CurrentLayerNo)
        {
            try
            {
                string TemplateName, Comment = string.Empty;
                int TemplateCount = 0;
                m_objCharGen.CompositionsGetCount(out TemplateCount);
                int TotalMoveUp = 0;
                for (int iCnt = 0; iCnt < TemplateCount; iCnt++)
                {
                    int LayerNo = 0;
                    int IDCount = 0;
                    m_objCharGen.CompositionsGetByIndex(iCnt, out TemplateName, out Comment);
                    LayerNo = GetLayerNumber(TemplateName);

                    if (CurrentLayerNo < LayerNo)
                    {
                        m_objCharGen.CompositionsGetItemsCount(TemplateName, ref IDCount);
                        TotalMoveUp += IDCount;
                    }
                }
                string objID = null, objXML = null;
                if (TotalMoveUp > 0)
                {
                    m_objCharGen.CompositionsGetItemsCount(_CurrentTemplateName, ref TemplateCount);
                    for (int iCnt = 0; iCnt < TemplateCount; iCnt++)
                    {
                        m_objCharGen.CompositionsGetItem(_CurrentTemplateName, iCnt, out objID, out objXML);
                        for (int jCnt = 0; jCnt < TotalMoveUp; jCnt++)
                        {
                            m_objCharGen.ChangeItemZOrder(objID, -1);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                objlog.WriteError("ChangeLayer", "Error in Updating Layer =>" + _CurrentTemplateName + Ex.ToString(), ChannelName);
            }
        }

        private int GetLayerNumber(string _TemplateName)
        {
            try
            {
                DataRow TemplatePropertyRow;
                TemplatePropertyRow = _dttemplateinfo.Select("TemplateName='" + _TemplateName + "'").FirstOrDefault();
                if (TemplatePropertyRow == null)
                {
                    objlog.WriteError("ChangeLayer", $"{_TemplateName} template defination not found in Graphics Configuration.", ChannelName);
                    return -1;
                }
                return Convert.ToInt16((TemplatePropertyRow["LayerNo"] + "").Trim());
            }
            catch
            {
                return -1;
            }
        }
        public static long calculateFramefromTime(string time)
        {
            String[] ft1 = time.Split(':');
            //   String[] ft2 = tt2.Split(':');
            if (ft1.Count() == 4)
            {
                long l1 = Int64.Parse(ft1[0]) * 3600 * 1000;
                long l2 = Int64.Parse(ft1[1]) * 60 * 1000;
                long l3 = Int64.Parse(ft1[2]) * 1000;
                long l4 = Int64.Parse(ft1[3]);
                return (l1/40 + l2/40 + l3/40 + l4);
            }
            else
                return 0;
        }
        public static string GetTimeDifference(DateTime start, DateTime end)
        {
            TimeSpan diff = end - start;

            // Format: HH:mm:ss.fff (zero-padded)
            string result = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                (int)diff.TotalHours,  // total hours (including from days)
                diff.Minutes,
                diff.Seconds,
                diff.Milliseconds/40
            );

            return result;
        }
        bool DurationBased = false;
        bool startanimation = false;
        public void SetBugtype()
        {
            //  _loadcgBugType = bug;
            Fetchtemplatedata();
            LoadTemplateConfigration();
        }
        internal void KillCgItem(string graphics, string BugType, int KillAnimationtime = 300,bool PorR = false)
        {
            string log = "Template Name " + graphics + " Bug Type=" + BugType + " ";
            lock (objreceive)
            {
                try
                {
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        DurationBased = Convert.ToBoolean(((dr["DurationBased"] + "") == "" ? "False" : dr["DurationBased"]));
                        KillAnimationtime = Convert.ToInt32((dr["ExitTime"] + "") == string.Empty ? "300" : dr["ExitTime"]);
                        BugType = dr["BugType"] + "";
                    }
                    ReadTriggerFile(graphics, "STOP");
                    if (UsedItems.Contains(graphics) && BugType == "VIDEO-SQZ")
                    {
                        string param3 = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault()["Param3"].ToString();
                        int durationtochangedimensions = Convert.ToInt32(param3);
                        int numberOfItems = 0;
                        objPmslReader.ANIMATION = EndAnimationType;
                        Task.Run(() => animation("STOP"));
                        lbandkill(graphics, PorR);
                        Thread.Sleep(durationtochangedimensions);
                        if (!PorR)
                        {
                            m_objCharGen_Lband.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                string itemID;
                                string itemXML;
                                m_objCharGen_Lband.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGen_Lband.RemoveItemWithDelay(itemID, 0, 0);
                            }
                        }
                        else
                        {
                            m_objCharGenTemp.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                string itemID;
                                string itemXML;
                                m_objCharGenTemp.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGenTemp.RemoveItemWithDelay(itemID, 0, 0);
                            }
                        }
                        UsedItems.Remove(graphics);
                        if (ReaderTemplateName == graphics.ToUpper())
                        {
                            Thread.Sleep(EndAnimationSpeed);
                            objPmslReader.ReaderClose();
                            ReaderTemplateName = "";
                        }
                        DEActivateSquzFlag();
                        FillEventLog(graphics + " Removed at " + objlog.LTCTime, "G");
                        objlog.WriteEvents("KillCgitems", $"Templatename={graphics} killed at LTC Time={objlog.LTCTime} info ( enddate={_enddate} ,endtime={_EndTime} ,status={secstatus})", ChannelName);

                    }
                    else if ((BugType == "LOGO" || (graphics.ToUpper() == "LOGO" || graphics.ToUpper() == "FESTLOGO")) && UsedItems.Contains(graphics))
                    {
                        int numberOfItems = 0;
                        string itemID;
                        string itemXML;
                        if (!PorR)
                        {
                            m_objCharGen_Logo.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int iCnt = 0; iCnt < numberOfItems; iCnt++)
                            {
                                m_objCharGen_Logo.CompositionsGetItem(graphics, 0, out itemID, out itemXML);
                                m_objCharGen_Logo.RemoveItem(itemID, KillAnimationtime);
                                m_objCharGen_Logo.CompositionsRemoveItem(graphics, itemID);
                            }
                            m_objCharGen_Logo.CompositionsRemove(graphics.ToLower());
                            log = stoptimefile(m_objCharGen_Logo, graphics, log);
                        }
                        else
                        {
                            m_objCharGenTemp.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int iCnt = 0; iCnt < numberOfItems; iCnt++)
                            {
                                m_objCharGenTemp.CompositionsGetItem(graphics, 0, out itemID, out itemXML);
                                m_objCharGenTemp.RemoveItem(itemID, KillAnimationtime);
                                m_objCharGenTemp.CompositionsRemoveItem(graphics, itemID);
                            }
                            m_objCharGenTemp.CompositionsRemove(graphics.ToLower());
                            log = stoptimefile(m_objCharGenTemp, graphics, log);
                        }
                        FillEventLog(graphics + " Removed at " + objlog.LTCTime, "G");
                        objlog.WriteEvents("KillCgitems", $"Templatename={graphics} killed at LTC Time={objlog.LTCTime} info ( enddate={_enddate} ,endtime={_EndTime} ,status={secstatus})", ChannelName);
                        load.lb_playingtemplatelist.Items.Remove(graphics);
                        UsedItems.Remove(graphics);
                        if ((graphics.ToUpper() == "LOGO") && (ASRunEnable))
                        {
                            String Templateendttime = objlog.LTCTime.Trim();
                            long startframe = calculateFramefromTime(LogoTemplatestore[graphics.ToUpper()]);
                            long endframe = calculateFramefromTime(Templateendttime);
                            long change = 0;
                            if (endframe > startframe)
                            {
                                change = endframe - startframe;
                            }
                            else
                            {
                                change = (calculateFramefromTime("24:00:00:00") - startframe) + endframe;
                            }
                            string diff = GetTimeFromFrame(change);
                            objAgent.PrepareAsRunSetting("STOPKILL", "", diff, "", graphics);
                            LogoTemplatestore.Remove(graphics);
                        }
                        ReadTriggerFile(graphics, "AFTERSTOP");
                    }
                    else if (BugType != "VIDEO-SQZ" && UsedItems.Contains(graphics))
                    {
                        int numberOfItems = 0;
                        string itemID;
                        string itemXML;
                        if (!PorR)
                        {
                            m_objCharGen.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            log = log + " Total ElementCount =" + numberOfItems;
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGen.CompositionsGetItem(graphics, 0, out itemID, out itemXML);
                                try
                                {
                                    m_objCharGen.RemoveItem(itemID, KillAnimationtime);
                                }
                                catch { }
                                m_objCharGen.CompositionsRemoveItem(graphics, itemID);
                                //  log = log + "Remove Element=" + itemID + " ";
                            }
                            m_objCharGen.CompositionsRemove(graphics.ToLower());
                            log = stoptimefile(m_objCharGen, graphics, log);
                        }
                        else
                        {
                            m_objCharGenTemp.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            log = log + " Total ElementCount =" + numberOfItems;
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGenTemp.CompositionsGetItem(graphics, 0, out itemID, out itemXML);
                                try
                                {
                                    m_objCharGenTemp.RemoveItem(itemID, KillAnimationtime);
                                }
                                catch { }
                                m_objCharGenTemp.CompositionsRemoveItem(graphics, itemID);
                                //  log = log + "Remove Element=" + itemID + " ";
                            }
                            m_objCharGenTemp.CompositionsRemove(graphics.ToLower());
                            log = stoptimefile(m_objCharGenTemp, graphics, log);
                        }
                        FillEventLog(graphics + " Removed at " + objlog.LTCTime, "G");
                        objlog.WriteEvents("KillCgitems", $"Templatename={graphics} killed at LTC Time={objlog.LTCTime} info ( enddate={_enddate} ,endtime={_EndTime} ,status={secstatus})", ChannelName);

                        load.lb_playingtemplatelist.Items.Remove(graphics);
                       UsedItems.Remove(graphics);
                        ReadTriggerFile(graphics, "AFTERSTOP");
                    }
                }
                catch (Exception ex)
                {
                    UsedItems.Remove(graphics);
                    load.lb_playingtemplatelist.Items.Remove(graphics);
                    objlog.WriteEvents("KillCgItem", "Error in removing of cg item Template Name=>" + graphics + " " + ex.ToString(), ChannelName);
                    objlog.WriteError("KillCgItem", "Error in removing of cg item Template Name=>" + graphics + " " + ex.ToString(), ChannelName);
                }
                finally
                {
                    objlog.WriteEvents("KillCgitems", $"{log}", ChannelName);
                }

            }
        }
     
        private string stoptimefile(CoMLCharGenClass ObjTmObject, string graphics, string log)
        {
            try
            {
#region "Timeline Stop"
                int TimelineCount = 0;
                string timelineid;
                ObjTmObject.ScheduleTimelineGetCount(out TimelineCount);
                log = log + "Timeline Count= " + TimelineCount + " ";
                for (int iData = 0; iData < TimelineCount; iData++)
                {
                    (ObjTmObject as IMLCharGenSchedule).ScheduleTimelineGetID(iData, out timelineid);
                    log = log + "Timeline name= " + timelineid + " ";
                    int schedulecount;
                    (ObjTmObject as IMLCharGenSchedule).ScheduleGetCount(timelineid, out schedulecount);
                    if (timelineid.ToLower() == graphics.ToLower())
                    {
                        log = log + "ScheduleCount= " + schedulecount + " ";
                        int j = 0;
                        while (j < schedulecount)
                        {
                            (ObjTmObject as IMLCharGenSchedule).ScheduleRemove(timelineid, 0);
                            ++j;
                        }
                        double Starttime, cycletime;
                        eCG_TimelineState Timelinestate;
                        (ObjTmObject as IMLCharGenSchedule).ScheduleTimelineGetTimes(timelineid, out Starttime, out cycletime, out Timelinestate);
                        if (Timelinestate == eCG_TimelineState.eCGTS_Running || Timelinestate == eCG_TimelineState.eCGTS_Paused)
                        {
                            (ObjTmObject as IMLCharGenSchedule).ScheduleTimelineStop(timelineid);
                            log = log + "Status " + timelineid + " Timeline Stop Complete .";
                            break;
                        }
                    }
                }
#endregion
                return log;
            }
            catch (Exception ex)
            {
                return (log + " ERROR -" + ex.ToString());
            }


        }


        internal void STOPANCgItem(string graphics, string BugType)
        {

            //This Function unpause the Template From CGST File 
            lock (objreceive)
            {
                try
                {
                    string itemID, itemXML;
                    int numberOfItems = 0;
                    m_objCharGen.CompositionsGetItemsCount(graphics, ref numberOfItems);
                    // string itemid = "";
                    for (int i = 0; i < numberOfItems; i++)
                    {
                        m_objCharGen.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                        m_objCharGen.SetItemProperties(itemID, "cg-props::pause", "false", "", 0);
                    }
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    objlog.WriteError("STOPANCgItem", "Error in STOPAN of cg items " + graphics + ex.ToString(), ChannelName);
                }
            }

        }

        internal void HideCgItem(string graphics, string BugType)
        {
            lock (objreceive)
            {
                string itemID, itemXML;
                int numberOfItems = 0;
                try
                {
                    if (BugType != "VIDEO-SQZ" && UsedItems.Contains(graphics))
                    {
                        if (graphics.ToUpper() == "LOGO" || graphics.ToUpper() == "FESTLOGO" || BugType.ToUpper() == "LOGO")
                        {
                            m_objCharGen_Logo.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGen_Logo.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGen_Logo.SetItemProperties(itemID, "cg-props::show", "false", "", 0);
                            }
                        }
                        else
                        {
                            m_objCharGen.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGen.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGen.SetItemProperties(itemID, "cg-props::show", "false", "", 0);
                            }
                        }
                        FillEventLog(graphics + " hidden at " + objlog.LTCTime, "G");
#if UTC_MODE
            objlog.WriteEvents("HideCgItem", $"Template Name=>{graphics}  hidden at LTC Time=>{objlog.LTCTime}, SyetemTime= {DateTime.UtcNow.ToString("HH:mm:ss")}", ChannelName);
#else
                        objlog.WriteEvents("HideCgItem", $"Template Name=>{graphics}  hidden at LTC Time=>{objlog.LTCTime}, SyetemTime= {DateTime.Now.ToString("HH:mm:ss")}", ChannelName);
#endif

                    }
                    else if (string.Compare(BugType, "VIDEO-SQZ", true) == 0 && UsedItems.Contains(graphics))
                    {
                        lbandkill(graphics);//Testing
                        m_objCharGen_Lband.CompositionsGetItemsCount(graphics, ref numberOfItems);
                        for (int i = 0; i < numberOfItems; i++)
                        {
                            m_objCharGen_Lband.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                            m_objCharGen_Lband.SetItemProperties(itemID, "cg-props::show", "false", "", 0);
                        }
                        FillEventLog(graphics + " hidden at " + objlog.LTCTime, "G");

#if UTC_MODE
           objlog.WriteEvents("HideCgItem", $"Template Name=>{graphics}  hidden at LTC Time=>{objlog.LTCTime} SyetemTime= {DateTime.UtcNow.ToString("HH:mm:ss")} ", ChannelName);
#else
                        objlog.WriteEvents("HideCgItem", $"Template Name=>{graphics}  hidden at LTC Time=>{objlog.LTCTime} SyetemTime= {DateTime.Now.ToString("HH:mm:ss")} ", ChannelName);
#endif
                    }
                    else
                    {
                        objlog.WriteEvents("HideCgItem", $"Template Name=>{graphics} ,BugType {BugType} is not fullfill condition for hide template .", ChannelName);
                    }

                }
                catch (Exception ex)
                {
                    objlog.WriteError("HideCgItem", "Error in hiding of cg items " + graphics + ex.ToString(), ChannelName);
                }

            }
        }
        internal void ShowCgItem(string graphics, string BugType)
        {
            lock (objreceive)
            {
                string itemID, itemXML;
                int numberOfItems = 0;
                try
                {
                    if (BugType != "VIDEO-SQZ" && UsedItems.Contains(graphics))
                    {
                        if (graphics.ToUpper() == "LOGO" || graphics.ToUpper() == "FESTLOGO" || BugType.ToUpper() == "LOGO")
                        {
                            m_objCharGen_Logo.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGen_Logo.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGen_Logo.SetItemProperties(itemID, "cg-props::show", "true", "", 0);
                            }
                        }
                        else
                        {
                            m_objCharGen.CompositionsGetItemsCount(graphics, ref numberOfItems);
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                m_objCharGen.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                                m_objCharGen.SetItemProperties(itemID, "cg-props::show", "true", "", 0);
                            }
                        }
                        FillEventLog(graphics + " displayed at " + objlog.LTCTime, "G");
#if UTC_MODE
             objlog.WriteEvents("ShowCgItem", $"Template Name=>{graphics} displayed at LTC Time=>{objlog.LTCTime} ,SystemTime={DateTime.UtcNow.ToString("HH:mm:ss")}", ChannelName);
#else
                        objlog.WriteEvents("ShowCgItem", $"Template Name=>{graphics} displayed at LTC Time=>{objlog.LTCTime} ,SystemTime={DateTime.Now.ToString("HH:mm:ss")}", ChannelName);
#endif

                    }
                    else if (string.Compare(BugType, "VIDEO-SQZ", true) == 0 && UsedItems.Contains(graphics))
                    {
                        lbandon(graphics);//Testing
                        m_objCharGen_Lband.CompositionsGetItemsCount(graphics, ref numberOfItems);
                        for (int i = 0; i < numberOfItems; i++)
                        {
                            m_objCharGen_Lband.CompositionsGetItem(graphics, i, out itemID, out itemXML);
                            m_objCharGen_Lband.SetItemProperties(itemID, "cg-props::show", "true", "", 0);
                        }
                        FillEventLog(graphics + " displayed at " + objlog.LTCTime, "G");
#if UTC_MODE
             objlog.WriteEvents("ShowCgItem", $"Template Name=>{graphics} displayed at LTC Time=>{objlog.LTCTime} ,SystemTime={DateTime.UtcNow.ToString("HH:mm:ss")} ", ChannelName);
#else
                        objlog.WriteEvents("ShowCgItem", $"Template Name=>{graphics} displayed at LTC Time=>{objlog.LTCTime} ,SystemTime={DateTime.Now.ToString("HH:mm:ss")} ", ChannelName);
#endif

                    }
                    else
                    {
                        objlog.WriteEvents("ShowCgItem", $"Template Name=>{graphics} ,BugType {BugType} is not fullfill condition for Show template .", ChannelName);
                    }

                }
                catch (Exception ex)
                {
                    objlog.WriteError("ShowCgItem", "Error in display of hidden cg items " + graphics + ex.ToString(), ChannelName);
                }
            }
        }
#endregion
        internal void releaseComObj(object comObj)
        {
            if (comObj != null)
            {
                try
                {
                    Marshal.ReleaseComObject(comObj);
                    comObj = null;
                }
                catch (Exception ex)
                {
                    objlog.WriteError("releaseComObj", "Error in Releasing com object " + ex.ToString(), ChannelName);
                }
            }
        }







#region Form Methods      
        private void panelpreview_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_objPreview != null)
                {
                    m_objPreview.PreviewWindowSet("",panelpreview.Handle.ToInt32());
                }
            }
            catch
            { }
        }
        private void FrmMainServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;



        }
#region Menu Methods
        private void exitTsm_Click(object sender, EventArgs e)
        {
            lock (m_objGridLock)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Do you want to close it", "Graphics Server", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        WriteSecondaryFile();
                        Environment.Exit(0);
                    }
                }
                catch
                {
                    Environment.Exit(0);
                }
            }
        }
        private void refreshConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {   // To refresh config
            try
            {

                ReadConfig(ChannelName);
                Fetchtemplatedata();
                LoadTemplateConfigration();
                CreateTemplate();
                DirectoryInfo(TemplatePath);
                objpacket.StorePrefix();
                FillEventLog("Config Refreshed", "G");
            }
            catch (Exception ex)
            {
                objlog.WriteError("refressConfigToolStripMenuItem_Click", "Error while refreshing configs " + ex.ToString(), ChannelName);
                FillEventLog("Error while refreshing", "E");
            }
        }
        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Aboutus ab = new Aboutus(this);
                ab.ShowDialog();
            }
            catch (Exception)
            { }
        }
        private void ChannelConfigStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = $"{Application.StartupPath}\\Channels\\{ChannelName}\\AppConfig.Xml";
                ChannelConfig ObjChannelConfig = new ChannelConfig(FileName, this);
                ObjChannelConfig.ShowDialog();
            }
            catch (Exception)
            { }
        }
        private void showHideEventLogsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lock (m_objLock)
            {
                try
                {
                    if (lv_eventlog.Visible)
                    {
                        lv_eventlog.Visible = false;
                    }
                    else
                    {
                        lv_eventlog.Visible = true;
                    }

                }
                catch (Exception)
                { }
            }
        }
        private void showHidePreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsShow)
                {
                    IsShow = false;
                    m_objPreview.PreviewEnable("", 0, 0);
                    lblpreviewmessage.Visible = true;
                }
                else
                {
                    m_objPreview.PreviewEnable("", 0, 1);
                    IsShow = true;
                    lblpreviewmessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("showHidePreviewToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
            }

        }
        private void fileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                fileToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\file`.png");
            }
            catch (Exception)
            { }
        }
        private void fileToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                fileToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\file.png");
            }
            catch (Exception)
            { }
        }
        private void toolToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                toolToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\settings`.png");
            }
            catch (Exception)
            { }
        }
        private void toolToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                toolToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\settings.png");
            }
            catch (Exception)
            { }
        }
        private void viewToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                viewToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\view`.png");
            }
            catch (Exception)
            { }
        }
        private void viewToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                viewToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\view.png");
            }
            catch (Exception)
            { }
        }
        private void helpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                helpToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\about`.png");
            }
            catch (Exception)
            { }
        }
        private void helpToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                helpToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\about.png");
            }
            catch (Exception)
            { }
        }

        private void settingsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            settingsToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\channelsettings`.png");
        }

        private void settingsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            settingsToolStripMenuItem.Image = Image.FromFile(Application.StartupPath + "\\Images\\MenuIcons\\channelsettings.png"); ;
        }
#endregion
#region Footer Methods
        private void ll_Eventlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\Channels\\" + ChannelName + "\\Log\\" + "EventLog.txt");
            }
            catch (Exception)
            { }
        }
        private void ll_Errorlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\Channels\\" + ChannelName + "\\Log\\" + "ErrorLog.txt");
            }
            catch (Exception)
            { }

        }
        private void lv_eventlog_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            try
            {
                Color color = Color.FromArgb(42, 42, 42);
                Brush brush = new SolidBrush(color);
                e.Graphics.FillRectangle(brush, e.Bounds);
                e.DrawText();
            }
            catch (Exception)
            { }
        }
#endregion
#endregion
#region UDP Code
        //To Receive UDP Packets
        private void StartUDPReceiver()
        {
            try
            {
                ReceiveLtcPacket();
                ReceiveAutomationPacket();
            }
            catch (Exception ex)
            {
                objlog.WriteError("StartUDPReceiver", ex.ToString(), ChannelName);
                FillEventLog("StartUDPReceiver() " + ex.Message, "E");
            }
        }

        private void ReceiveAutomationPacket()
        {
            try
            {
                ReceUdpClient = new UdpClient(ReceivingPort);
                objpacket = new PacketData(this);
                if (ExternalAgentMode == true)
                {
                    removeTemplateToolStripMenuItem.Enabled = false;
                    removeAllTemplatesToolStripMenuItem.Enabled = false;
                    ReceUdpClient.BeginReceive(new AsyncCallback(ReceiveAgentData), null);
                    objlog.WriteEvents("StartUDPReceiver", "Application Mode Reading Input Packet With External Agent Mode...", ChannelName);
                }
                else
                {
                    ReceUdpClient.BeginReceive(new AsyncCallback(ReceivePmsPacketData), null);
                    objlog.WriteEvents("StartUDPReceiver", "Application Mode Reading Input Packet With Internal Agent Mode...", ChannelName);
                    objpacket.StorePrefix();
                }
                FillEventLog("Start Receiving Automation On Port " + ReceivingPort, "G");
                FillEventLog("Start Receiving LTC Time On Port " + LTCPort, "G");
            }
            catch (Exception ex)
            {
                objlog.WriteError("ReceiveLtcPacket", $"Error In Bind Automation  Packet Port {ReceivingPort} " + ex.ToString(), ChannelName);

            }
        }

        private void ReceiveLtcPacket()
        {
            try
            {
                

                if (MultiCastEnable)
                {
                      IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, LTCPort);               
                    //   ReceiveLTC.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    //  ReceiveLTC.Client.Bind(RemoteIpEndPoint);
                    ReceiveLTC = new UdpClient();
                    ReceiveLTC.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    ReceiveLTC.Client.Bind(RemoteIpEndPoint);
                    ReceiveLTC.JoinMulticastGroup(IPAddress.Parse(MultiCastIP));
                }
                else
                    ReceiveLTC = new UdpClient(LTCPort);

                ReceiveLTC.BeginReceive(new AsyncCallback(LtcTime), null);
            }
            catch (Exception ex)
            {
                objlog.WriteError("ReceiveLtcPacket", $"Error In Bind Ltc Packet Port {LTCPort} " + ex.ToString(), ChannelName);
                FillEventLog("ReceiveLtcPacket() " + ex.Message, "E");
            }
        }

        private void LtcTime(IAsyncResult res)
        {
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, LTCPort);
                  
                byte[] received = ReceiveLTC.EndReceive(res, ref RemoteIpEndPoint);
                string strReturnData = System.Text.Encoding.ASCII.GetString(received);
                objlog.LTCTime = strReturnData;
                if (LTCtimelbl.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        LTCtimelbl.Text = strReturnData;
                    });
                }
            }
            catch (ObjectDisposedException dex)
            {

            }
            catch (Exception ex)
            {
                objlog.WriteError("LtcTime", "Error in UDP received data : " + ex.ToString(), ChannelName);
                //FillEventLog("LtcTime Error in UDP received data : " + ex.Message, "E");
            }
            finally
            {
                try
                {
                    ReceiveLTC.BeginReceive(new AsyncCallback(LtcTime), null);
                }
                catch
                { }
            }
        }
        private void ReceiveAgentData(IAsyncResult res)
        {
            try
            {

                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, ReceivingPort);
                byte[] received = ReceUdpClient.EndReceive(res, ref RemoteIpEndPoint);
                //  string strReturnData = Encoding.UTF8.GetString(received);
                string strReturnData = Encoding.UTF8.GetString(received);
                objlog.WriteEvents("ReceiveAgentData", "==========>>Received AgentPacket from " + RemoteIpEndPoint.Address.ToString() + "  " + strReturnData, ChannelName);
                objAgent.HandelAgentPacket(strReturnData, RemoteIpEndPoint);
            }
            catch (ObjectDisposedException dex)
            {

            }
            catch (Exception ex)
            {
                objlog.WriteError("ReceiveAgentData", $"Error in UDP received data receive Port-{ReceivingPort}: " + ex.ToString(), ChannelName);
                FillEventLog("ReceiveAgentData Error in UDP received data : " + ex.Message, "E");
            }
            finally
            {
                try
                {
                    ReceUdpClient.BeginReceive(new AsyncCallback(ReceiveAgentData), null);
                }
                catch { }
            }
        }
        private bool AutoHideCheck = false;
        private void ReceivePmsPacketData(IAsyncResult res)
        {
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, ReceivingPort);
                byte[] received = ReceUdpClient.EndReceive(res, ref RemoteIpEndPoint);
                string strReturnData = System.Text.Encoding.UTF8.GetString(received);
                objlog.WriteEvents("ReceivePmsPacketData", "Received pms packet from " + RemoteIpEndPoint.Address.ToString() + "  *****  " + strReturnData, ChannelName);
                ReceiveddatafromPMS = strReturnData.Split(';');
                ReceiveddatafromPMS = (from receivedpmsdata in ReceiveddatafromPMS select receivedpmsdata.Trim()).ToArray();
                DifferentEventsOnReceivedPackets();
            }
            catch (Exception ex)
            {
                //objlog.WriteError("ReceivePmsPacketData", "Error in UDP received data : " + ex.ToString(), ChannelName);
                // FillEventLog("ReceivePmsPacketData Error in UDP received data : " + ex.Message, "E");
            }
            finally
            {
                try
                {
                    ReceUdpClient.BeginReceive(new AsyncCallback(ReceivePmsPacketData), null);
                }
                catch
                { }
            }
        }
        private void DifferentEventsOnReceivedPackets()
        {
            if (ReceiveddatafromPMS.Length < 2)
            {
                EventsOnPlayPacket();
            }
            else if (string.Compare(ReceiveddatafromPMS[1], "PRI", true) == 0 && ReceiveddatafromPMS.Length == 7)
            {
                if (!ReceiveddatafromPMS[2].Equals(ChannelName, StringComparison.InvariantCultureIgnoreCase))
                {
                    objlog.WriteEvents("DifferentEventsOnReceivedPackets", "Channel Name Does Not Match Consider bad packet structure.", ChannelName);
                    return;
                }
                EventsOnPrimaryPacket();
            }
            else if (string.Compare(ReceiveddatafromPMS[1], "SEC", true) == 0 && ReceiveddatafromPMS.Length == 14)
            {
                if (!ReceiveddatafromPMS[2].Equals(ChannelName, StringComparison.InvariantCultureIgnoreCase))
                {
                    objlog.WriteEvents("DifferentEventsOnReceivedPackets", "Channel Name Does Not Match Consider bad packet structure.", ChannelName);
                    return;
                }
                EventsOnSecondaryPacket();
            }
            else
            {
                objlog.WriteEvents("DifferentEventsOnReceivedPackets", "BAD PACKET STRUCTURE", ChannelName);
                FillEventLog("BAD PACKET STRUCTURE", "W");
            }
        }
        private void EventsOnSecondaryPacket()
        {
            objpacket.FillSecondaryData(ReceiveddatafromPMS);
        }
        private void EventsOnPrimaryPacket()
        {
            PacketSkippedEvent();
            objpacket.FillPrimaryData(ReceiveddatafromPMS);
        }
        private void PacketSkippedEvent()
        {
            NewEventTime = (ReceiveddatafromPMS[4] + "").ToUpper();
            if (objpacket.CheckInBetween.Invoke("00:" + EventTime, PrimaryEndTime, "00:" + NewEventTime) && !string.IsNullOrEmpty(EventId))
            {
                foreach (DataGridViewRow row in dgvsecevent.Rows)
                {
                    string templatename = (row.Cells["SecTemplate"].Value + "").Trim().ToUpper();
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault();
                    bool KillWithPrimary = Convert.ToBoolean(dr["KillWithPrimary"]);
                    if (KillWithPrimary == true)
                    {
                        row.Cells["SecDeleteCheck"].Value = "Y";
                        objlog.WriteEvents("PacketSkippedEvent", $"Skip Playlist Detected,templatename= {(row.Cells["SecTemplate"].Value + "").Trim().ToUpper()} is KILLWITHPRIMARY=TRUE removed all graphics that previous ID={EventId} ", ChannelName);
                    }
                }
                FillEventLog("Skipped Command Received ", "G");
            }
            //Global Variable OLD PACKET
            EventId = (ReceiveddatafromPMS[3] + "").ToUpper();
            EventTime = (ReceiveddatafromPMS[4] + "").ToUpper();
            Duration = (ReceiveddatafromPMS[5] + "").ToUpper();
            PrimaryEndTime = objpacket.AddTimes(EventTime, Duration, "");
        }
        private void EventsOnPlayPacket()
        {
            if (ReceiveddatafromPMS[0].ToUpper() == "PLAY")
            {
                if (dgvprievent.Rows.Count <= 0) { return; }
                RunningPrimary = (dgvprievent.Rows[dgvprievent.Rows.Count - 1].Cells["priEventID"].Value + "").ToString();
                AutoHideCheck = CheckPrimary(RunningPrimary);
                CheckHideUnHideLogo();
                SecondaryHideCheck(AutoHideCheck);
                if (SubtitleCheck) { PlaySubtitle(RunningPrimary); }
            }
            return;
        }
#endregion
#region Sub Forms
        private void renderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (setrender.Visible == false)
                {
                    setrender.BringToFront();
                    setrender.ShowDialog();
                }
            }
            catch
            { }
        }
        private void renderOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (outrender.Visible == false)
                {
                    outrender.ShowDialog();
                }
            }
            catch
            { }
        }
        private void manualGraphicsLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                BeginInvoke((MethodInvoker)delegate
                {
                    load.ShowDialog();
                });
            }
            catch
            { }
        }
        private void gridFormToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

#endregion
#region Grid Filling and PmsPacketAutomation
        internal void SetGridControl()
        {
            try
            {
                //while (dgvsecevent.ColumnCount > 0)
                //{
                //    dgvsecevent.Columns.RemoveAt(0);
                //}
                DataSet ds = new DataSet();
                ds.ReadXml(XMLGridFormat);
                DataTable dtForGridformate = ds.Tables[0].Copy();
                for (int i = 0; i <= dtForGridformate.Rows.Count - 1; i++)
                {

                    foreach (DataGridViewColumn item in dgvsecevent.Columns)
                    {
                        if (item.Name.ToLower() == dtForGridformate.Rows[i]["ColumnName"].ToString().ToLower())
                        {
                            //col.Name = dtForGridformate.Rows[i]["ColumnName"].ToString();
                            item.HeaderText = dtForGridformate.Rows[i]["DisplayText"].ToString();
                            item.Width = int.Parse(dtForGridformate.Rows[i]["ColumnWidth"].ToString());
                            item.ToolTipText = dtForGridformate.Rows[i]["ToolTipText"].ToString();

                            if (dtForGridformate.Rows[i]["Visible"].ToString().Trim().ToUpper() == "True".ToUpper())
                            {
                                item.Visible = true;
                            }
                            else
                            {
                                item.Visible = false;
                            }

                            if (dtForGridformate.Rows[i]["Resizable"].ToString().Trim().ToUpper() == "true".ToUpper())
                            {
                                item.Resizable = DataGridViewTriState.True;
                            }
                            else
                            {
                                item.Resizable = DataGridViewTriState.False;
                            }
                            if (dtForGridformate.Rows[i]["Frozen"].ToString().Trim().ToUpper() == "true".ToUpper())
                            {
                                item.Frozen = true;
                            }
                            continue;
                        }
                    }

                }
                objlog.WriteEvents("SetGridControl", "Setting Secondary Grid Complete" + Environment.NewLine, ChannelName);
            }
            catch (Exception ex)
            {
                objlog.WriteError("SetGridControl", ex.ToString(), ChannelName);
            }
        }
        internal void SetGridControl(string Value)
        {
            lock (m_objGridLock)
            {
                try
                {
                    while (dgvsecevent.ColumnCount > 0)
                    {
                        dgvsecevent.Columns.RemoveAt(0);
                    }
                    DataSet ds = new DataSet();
                    ds.ReadXml(XMLGridFormat);
                    DataTable dtForGridformate = ds.Tables[0].Copy();
                    for (int i = 0; i <= dtForGridformate.Rows.Count - 1; i++)
                    {
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.Name = dtForGridformate.Rows[i]["ColumnName"].ToString();
                        col.HeaderText = dtForGridformate.Rows[i]["DisplayText"].ToString();
                        col.Width = int.Parse(dtForGridformate.Rows[i]["ColumnWidth"].ToString());
                        col.ToolTipText = dtForGridformate.Rows[i]["ToolTipText"].ToString();

                        if (dtForGridformate.Rows[i]["Visible"].ToString().Trim().ToUpper() == "True".ToUpper())
                        {
                            col.Visible = true;
                        }
                        else
                        {
                            col.Visible = false;
                        }

                        if (dtForGridformate.Rows[i]["Resizable"].ToString().Trim().ToUpper() == "true".ToUpper())
                        {
                            col.Resizable = DataGridViewTriState.True;
                        }
                        else
                        {
                            col.Resizable = DataGridViewTriState.False;
                        }

                        dgvsecevent.Columns.Add(col);
                        if (dtForGridformate.Rows[i]["Frozen"].ToString().Trim().ToUpper() == "true".ToUpper())
                        {
                            col.Frozen = true;
                        }
                    }
                    objlog.WriteEvents("SetGridControl", "Setting Secondary Grid Complete" + Environment.NewLine, ChannelName);
                    if (!ExternalAgentMode)
                    {
                        ReadSecondaryFile();
                    }

                }
                catch (Exception ex)
                {
                    objlog.WriteError("SetGridControl", ex.ToString(), ChannelName);
                }
            }
        }



        private bool SecondarySkipFlag = false;
        private bool ShowKeyPressed = false;
        private bool HideKeyPressed = false;
        private void Secondaryevents()
        {
            while (true)
            {
                try
                {
                    if (dgvsecevent.Rows.Count > 0)
                    {
                        RowsCheckOnFireEvent();
                    }
                    Thread.Sleep(CheckEventInterval);
                }
                catch (Exception ex)
                {
                    objlog.WriteError("Secondaryevents", "Error-- " + ex.ToString(), ChannelName);
                    Thread.Sleep(1);
                }
            }
        }

        private DataGridView SecondaryDataView;
        private DataGridViewRowCollection SecondaryGridData;
        private DataGridViewRow SecondaryData;

        private string deletecheck;
        private string secstatus;
        private string templatename;
        private string _enddate;
        private string _currentdate;
        private string _BugType;
        private string _hidecheck;
        private string _currentltctime;
        private string BugType;
        private string _EndTime;
        private string HideCheck;

        private string[] _LoadTime;
        private string _Loadtime;
        private string _loaddate;
        private string templatevalue;
        private string _timerduration;
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
                objlog.WriteError("GetTimeFromFrame", " Frame Receive : " + iFrames + ", Error is: " + Ex.Message.ToString(), ChannelName);

                return "00:00:00:00";
            }

        }
        private void RowsCheckOnFireEvent()
        {
            lock (m_objGridLock)
            {
                try
                {
                    foreach (DataGridViewRow row in dgvsecevent.Rows)
                    {
                        SecondaryDataView = dgvsecevent;
                        SecondaryGridData = dgvsecevent.Rows;
                        SecondaryData = row;

                        FillingGridDataIntoVariables();
                        if (string.Compare(secstatus.Trim(), "R", true) != 0 && (string.Compare(deletecheck, "N", true) != 0 || CompareDate.Invoke(_currentdate, _enddate)) || string.Compare(deletecheck, "A", true) == 0)
                        {
                            RemoveRowFromGrid();
                            return;
                        }
                        HideTemplatUsingHideAbbreviation();
                        SecondarySkipFlag = false;
                        if (string.Compare(secstatus, "R", true) == 0)
                        {
                            _EndTime = GetTimeFromFrame(GetFrameFromTime(_EndTime) - 1);
                            if (string.Compare(deletecheck, "Y", true) == 0 || CheckDateTime.Invoke(_EndTime, _enddate, _currentltctime) || CompareDate.Invoke(_currentdate, _enddate))
                            {
                                SecondaryAndRowRemove();
                                return;
                            }
                            else if (HideKeyPressed && string.Compare(HideCheck, "N", true) == 0 && index == SecondaryData.Index)
                            {
                                ManuallyGraphicsHide();
                            }
                            else if (ShowKeyPressed && string.Compare(HideCheck, "Y", true) == 0 && index == SecondaryData.Index)
                            {
                                ManuallyGraphicsShow();
                            }
                            SecondarySkipFlag = true;
                        }
                        if (!SecondarySkipFlag)
                        {
                            LoadTemplateOnStatusWaiting();
                            LoadTemplateOnSatusRetry();
                            LoadTemplateOnStatusRunning();
                        }
                    }
                }
                catch (Exception ex)
                {
                    objlog.WriteError("RowsCheckOnFireEvent", "Error in row " + ex.ToString(), ChannelName);
                }
            }
        }
        private void FillingGridDataIntoVariables()
        {
            deletecheck = SecondaryData.Cells["SecDeleteCheck"].Value + "";
            secstatus = (SecondaryData.Cells["SecStatus"].Value + "").Trim().ToUpper();
            templatename = (SecondaryData.Cells["SecTemplate"].Value + "").Trim().ToUpper();
            _enddate = (SecondaryData.Cells["SecEndDate"].Value + "").Trim();
#if UTC_MODE
          _currentdate = DateTime.UtcNow.ToString("dd-MM-yyyy");
#else
            _currentdate = DateTime.Now.ToString("dd-MM-yyyy");
#endif

            BugType = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault()["BugType"] + "";
            _EndTime = SecondaryData.Cells["SecEndTime"].Value + "";
            HideCheck = SecondaryData.Cells["SecIsHide"].Value + "";
            _currentltctime = objlog.LTCTime;
        }
        private void ManuallyGraphicsHide()
        {
            HideCgItem(templatename, BugType);
            HideKeyPressed = false;
            HideCheck = "Y";
            SecondaryData.Cells["SecIsHide"].Value = HideCheck;
        }
        private void ManuallyGraphicsShow()
        {
            ShowCgItem(templatename, BugType);
            ShowKeyPressed = false;
            HideCheck = "N";
            SecondaryData.Cells["SecIsHide"].Value = HideCheck;
        }
        private void SecondaryAndRowRemove()
        {
            RemoveRowFromGrid();
            KillCgItem(templatename, BugType);
            //objlog.WriteEvents("RowsCheckOnFireEvent", $"template killed templatename= { templatename }, enddate={_enddate} ,endtime={_EndTime} ,status={secstatus}", ChannelName);
            ShowHiddenTemplateUsingHideAbbreviation();
        }
        private void RemoveRowFromGrid()
        {
            SecondaryDataView.Invoke((MethodInvoker)(() =>
            {
                {
                    SecondaryGridData.Remove(SecondaryData);
                }
            }));
            objlog.WriteEvents("RowsCheckOnFireEvent", $"templatename={templatename},status={secstatus} removed from grid Where deletecheck={deletecheck} and enddate={_enddate}, endtime={_EndTime}.", ChannelName);
        }
        private void HideTemplatUsingHideAbbreviation()
        {
            var TemplatesToHide = from hiderow in hidetemplatedt.AsEnumerable()
                                  where hiderow.Field<string>("TemplateName") == templatename
                                  select hiderow.Field<string>("HideTemplateName").ToUpper();
            foreach (var TemplateNameToBeHide in TemplatesToHide)
            {
                var HideTemplateRows = from DataGridViewRow templaterows in SecondaryGridData
                                       where (templaterows.Cells["SecTemplate"].Value.ToString() == TemplateNameToBeHide + ""
                                       && templaterows.Cells["SecStatus"].Value.ToString() != "W" && templaterows.Cells["SecIsHide"].Value.ToString() != "Y"
                                       && SecondaryData.Cells["SecStatus"].Value.ToString() == "R")
                                       select templaterows;
                foreach (var HideTemplateRow in HideTemplateRows)
                {
                    _BugType = _dttemplateinfo.Select("TemplateName='" + TemplateNameToBeHide + "'").FirstOrDefault()["BugType"] + "";
                    _hidecheck = HideTemplateRow.Cells["SecIsHide"].Value.ToString();
                    SecondaryHideOnTemplate(HideTemplateRow.Cells["SecTemplate"].Value.ToString(), _BugType, ref _hidecheck);
                    HideTemplateRow.Cells["SecIsHide"].Value = _hidecheck;
                }
            }
        }
        private void LoadTemplateOnStatusRunning()
        {
            string _Starttime = SecondaryData.Cells["SecStartTime"].Value + "";
            string _startdate = SecondaryData.Cells["SecStartDate"].Value + "";
            string _duration = SecondaryData.Cells["SecDuration"].Value + "";
            if (string.Compare(secstatus, "L", true) == 0 && _startdate == _currentdate && CheckDateTime.Invoke(_Starttime, _startdate, _currentltctime))
            {
                BugType = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault()["BugType"] + "";
                int LayerNO = Convert.ToInt16(_dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault()["layerno"] + "");
                objlog.WriteEvents("RowsCheckOnFireEvent", $"start running templatename={ templatename },status={secstatus},starttime={_Starttime} ,startdate={_startdate} ,duration={_duration} .", ChannelName);
                LoadCgItems(templatename);
                secstatus = "R";

                SecondaryData.DefaultCellStyle.ForeColor = System.Drawing.Color.Azure;
                SecondaryData.DefaultCellStyle.BackColor = System.Drawing.Color.DarkGreen;
                SecondaryData.Cells["SecStatus"].Value = secstatus;

                HideCheck = SecondaryData.Cells["SecIsHide"].Value + "";
                SecondaryHideCheckForParticularRow(ref HideCheck, templatename, BugType);
                SecondaryData.Cells["SecIsHide"].Value = HideCheck;
            }
        }
        private void LoadTemplateOnSatusRetry()
        {
            if (secstatus == "RE" && _loaddate == _currentdate && CheckDateTime.Invoke(_Loadtime, _loaddate, _currentltctime))
            {
                templatevalue = SecondaryData.Cells["SecTemplateValue"].Value.ToString().Trim();
                objlog.WriteEvents("RowsCheckOnFireEvent", $"start reloading templatename={ templatename },status={secstatus},loadtime={_Loadtime},templatevalue={templatevalue}.", ChannelName);
                BugType = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault()["BugType"] + "";
                if (BugType == "TIMER")
                {
                    _timerduration = SecondaryData.Cells["SecDuration"].Value + "";
                    templatevalue = _timerduration;
                    SecondaryData.Cells["SecTemplateValue"].Value = templatevalue;
                }
                LoadSecondaryevents(templatevalue, templatename, ref secstatus);
                SecondaryData.Cells["SecStatus"].Value = secstatus;
            }
        }
        private void LoadTemplateOnStatusWaiting()
        {
            _LoadTime = SecondaryData.Cells["SecLoadtime"].Value.ToString().Split(':');
            _Loadtime = _LoadTime[1] + ":" + _LoadTime[2] + ":" + _LoadTime[3] + ":" + _LoadTime[4];
            _loaddate = SecondaryData.Cells["SecLoadDate"].Value + "";
            if (string.Compare(secstatus, "W", true) == 0 && _loaddate == _currentdate && CheckDateTime.Invoke(_Loadtime, _loaddate, _currentltctime))
            {
                templatevalue = SecondaryData.Cells["SecTemplateValue"].Value.ToString().Trim();
                BugType = _dttemplateinfo.Select("TemplateName='" + templatename + "'").FirstOrDefault()["BugType"] + "";
                if (string.Compare(BugType, "TIMER", true) == 0)
                {
                    _timerduration = SecondaryData.Cells["SecDuration"].Value + "";
                    templatevalue = _timerduration;
                    SecondaryData.Cells["SecTemplateValue"].Value = templatevalue;
                }
                objlog.WriteEvents("RowsCheckOnFireEvent", $"Start loading templatename={ templatename },status={secstatus},loadTime={_Loadtime},templatevalue=>{templatevalue}", ChannelName);
                LoadSecondaryevents(templatevalue, templatename, ref secstatus);
                SecondaryData.Cells["SecStatus"].Value = secstatus;
            }
        }
        private void ShowHiddenTemplateUsingHideAbbreviation()
        {
            var TemplatesToShow = from hiderow in hidetemplatedt.AsEnumerable()
                                  where hiderow.Field<string>("TemplateName") == templatename
                                  select hiderow.Field<string>("HideTemplateName").ToUpper();
            foreach (var TemplateNameToBeShown in TemplatesToShow)
            {
                var ShowTemplateRows = from DataGridViewRow templaterows in SecondaryGridData
                                       where (templaterows.Cells["SecTemplate"].Value.ToString() == TemplateNameToBeShown + ""
                                       && templaterows.Cells["SecStatus"].Value.ToString() != "W" && templaterows.Cells["SecIsHide"].Value.ToString() != "N"
                                       )
                                       select templaterows;
                foreach (var ShowTemplateRow in ShowTemplateRows)
                {
                    _BugType = _dttemplateinfo.Select("TemplateName='" + TemplateNameToBeShown + "'").FirstOrDefault()["BugType"] + "";
                    _hidecheck = ShowTemplateRow.Cells["SecIsHide"].Value.ToString();
                    SecondaryHideOnTemplate(ShowTemplateRow.Cells["SecTemplate"].Value.ToString(), _BugType, ref _hidecheck);
                    ShowTemplateRow.Cells["SecIsHide"].Value = _hidecheck;
                }
            }
        }
        private void LoadSecondaryevents(string templatevalue, string templatename, ref string Secstatus)
        {
            try
            {
                bool IsAvailableNas = false;
                bool IsAvaiable = false;
                Secstatus = "L";
                Func<string, string, string, string, string, bool, bool,bool> update;
                update = UpdateXMLComposition;

                DataRow dr = _dttemplateinfo.Select("TemplateName ='" + templatename + "'").FirstOrDefault();
                string BugType = (dr["BugType"] + "").Trim();
                string DefaultExtension = (dr["fileextension"] + "").Trim();
                string[] _receivedvalue = templatevalue.Split(new string[] { "~" }, StringSplitOptions.RemoveEmptyEntries);
                // if (_receivedvalue.Length < 1) { return; }

                string param1 = dr["Param1"].ToString().Trim();
                string[] arrparam1 = param1.Split('~');
                if (arrparam1.Length == 2)
                {

                    string templatevalues = ContentInfo(_receivedvalue.Length >= 1 ? _receivedvalue[0] : "", arrparam1[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                    update(templatename.ToUpper(), arrparam1[0], templatevalues, arrparam1[1], BugType, IsAvailableNas, IsAvaiable);
                }
                if (BugType != "VIDEO-SQZ")
                {
                    string param2 = dr["Param2"].ToString().Trim();
                    string[] arrparam2 = param2.Split('~');
                    if (arrparam2.Length == 2)
                    {
                        string templatevalues = "";
                        if (_receivedvalue.Length >= 2)
                        {
                            templatevalues = _receivedvalue[1];
                        }
                        templatevalues = ContentInfo(templatevalues, arrparam2[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam2[0], templatevalues, arrparam2[1], BugType, IsAvailableNas, IsAvaiable);
                    }

                    string param3 = dr["Param3"].ToString().Trim();
                    string[] arrparam3 = param3.Split('~');
                    if (arrparam3.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 3 ? _receivedvalue[2] : "", arrparam3[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam3[0], templatevalues, arrparam3[1], BugType, IsAvailableNas, IsAvaiable);
                    }
                    string param4 = dr["Param4"].ToString().Trim();
                    string[] arrparam4 = param4.Split('~');
                    if (arrparam4.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 4 ? _receivedvalue[3] : "", arrparam4[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam4[0], templatevalues, arrparam4[1], BugType, IsAvailableNas, IsAvaiable);
                    }
                    string param5 = dr["Param5"].ToString().Trim();
                    string[] arrparam5 = param5.Split('~');
                    if (arrparam5.Length == 2)
                    {

                        string templatevalues = ContentInfo(_receivedvalue.Length >= 5 ? _receivedvalue[4] : "", arrparam5[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam5[0], templatevalues, arrparam5[1], BugType, IsAvailableNas, IsAvaiable);
                    }
                    string param6 = dr["Param6"].ToString().Trim();
                    string[] arrparam6 = param6.Split('~');
                    if (arrparam6.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 6 ? _receivedvalue[5] : "", arrparam6[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam6[0], templatevalues, arrparam6[1], BugType, IsAvailableNas, IsAvaiable);
                    }

                    string param7 = dr["Param7"].ToString().Trim();
                    string[] arrparam7 = param7.Split('~');
                    if (arrparam7.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 7 ? _receivedvalue[6] : "", arrparam7[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam7[0], templatevalues, arrparam7[1], BugType, IsAvailableNas, IsAvaiable);
                    }

                    string param8 = dr["Param8"].ToString().Trim();
                    string[] arrparam8 = param8.Split('~');
                    if (arrparam8.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 8 ? _receivedvalue[7] : "", arrparam8[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam8[0], templatevalues, arrparam8[1], BugType, IsAvailableNas, IsAvaiable);
                    }

                    string param9 = dr["Param9"].ToString().Trim();
                    string[] arrparam9 = param9.Split('~');
                    if (arrparam9.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 9 ? _receivedvalue[8] : "", arrparam9[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam9[0], templatevalues, arrparam9[1], BugType, IsAvailableNas, IsAvaiable);
                    }

                    string param10 = dr["Param10"].ToString().Trim();
                    string[] arrparam10 = param10.Split('~');
                    if (arrparam10.Length == 2)
                    {
                        string templatevalues = ContentInfo(_receivedvalue.Length >= 10 ? _receivedvalue[9] : "", arrparam10[1], DefaultExtension, out IsAvailableNas, out IsAvaiable);
                        update(templatename.ToUpper(), arrparam10[0], templatevalues, arrparam10[1], BugType, IsAvailableNas, IsAvaiable);
                    }
                }
            }
            catch (Exception ex)
            {
                Secstatus = "RE";
                objlog.WriteError("LoadSecondaryevents", "Error-- " + ex.ToString(), ChannelName);
            }
        }

        /// <summary>
        /// To Hide Secondary through Primary Abbreviation
        /// </summary>
        /// <param name="Value">Flag whether this primary will hide secondaries or not</param>
        private void SecondaryHideCheck(bool Value)
        {
            try
            {
                foreach (DataGridViewRow row in dgvsecevent.Rows)
                {
                    string SecStatus = (row.Cells["SecStatus"].Value + "").Trim().ToUpper();
                    string HideCheck = (row.Cells["SecIsHide"].Value + "").Trim().ToUpper();
                    string TemplateName = (row.Cells["SecTemplate"].Value + "").Trim().ToUpper();
                    if (SecStatus == "R")
                    {
                        DataRow dr = _dttemplateinfo.Select("TemplateName='" + TemplateName + "'").FirstOrDefault();
                        if (dr != null)
                        {
                            string BugType = dr["BugType"].ToString();
                            if (Value)
                            {
                                if (string.Compare(HideCheck, "N", true) == 0 && string.Compare(dr["ShowOnBreak"].ToString(), "FALSE", true) == 0)
                                {
                                    HideCgItem(TemplateName, BugType);
                                    HideCheck = "Y";
                                    row.Cells["SecIsHide"].Value = HideCheck;
                                }
                            }
                            else
                            {
                                if (string.Compare(HideCheck, "Y", true) == 0)
                                {
                                    ShowCgItem(TemplateName, BugType);
                                    HideCheck = "N";
                                    row.Cells["SecIsHide"].Value = HideCheck;
                                }
#region  To Hide Template Using _HideAbbreviation
                                DataRow _hidedr = hideabbreviationdt.Select("TemplateName='" + TemplateName + "'").FirstOrDefault();
                                if (_hidedr != null)
                                {
                                    string AbbreviationToHide = (_hidedr["Abbreviation"] + "").Trim().ToUpper();
                                    string[] ArrAbbreviationToHide = AbbreviationToHide.Split(',');

                                    foreach (string abbreviation in ArrAbbreviationToHide)
                                    {
                                        if (string.Compare(HideCheck, "N", true) == 0 && RunningPrimary.StartsWith(abbreviation))
                                        {
                                            SecondaryHideOnTemplate(TemplateName, BugType, ref HideCheck);
                                            row.Cells["SecIsHide"].Value = HideCheck;
                                        }
                                    }
                                }
#endregion
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("SecondaryHideCheck", "Error-- " + ex.ToString(), ChannelName);
            }
        }
        /// <summary>
        /// To Hide Template When Application is Restarted
        /// </summary>
        /// <param name="HideCheck">Hide Flag of Template</param>
        /// <param name="templatename">Template Name</param>
        /// <param name="BugType"></param>
        private void SecondaryHideCheckForParticularRow(ref string HideCheck, string templatename, string BugType)
        {
            try
            {
                if (string.Compare(HideCheck, "Y", true) == 0 && BugType != "VIDEO-SQZ")
                {
                    objlog.WriteEvents("SecondaryHideCheckForParticularRow", $"Template Name=>{templatename} is Hidden due to restart condition", ChannelName);
                    HideCgItem(templatename, BugType);
                }
                else if (string.Compare(BugType, "VIDEO-SQZ", true) == 0)
                {
                    HideCheck = "N";
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("SecondaryHideCheckForParticularRow", $"Template Name=>{templatename}, HideCheck=>{HideCheck} , BugType=>{BugType} {ex.ToString()}", ChannelName);
            }
        }
        /// <summary>
        /// To Hide Template through other Templates
        /// </summary>
        /// <param name="templatename">Template to be hide or show</param>
        /// <param name="bugtype"></param>
        /// <param name="Check">Hide Check</param>
        private void SecondaryHideOnTemplate(string templatename, string bugtype, ref string Check)
        {
            try
            {
                if (string.Compare(Check, "N", true) == 0)
                {
                    HideCgItem(templatename, bugtype);
                    Check = "Y";
                }
                else
                {
                    if (!CheckPrimary(RunningPrimary))
                    {
                        ShowCgItem(templatename, bugtype);
                        Check = "N";
                    }
                }
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// Primary Abbreviation Check
        /// </summary>
        /// <param name="primaryidname">Primary Name</param>
        /// <returns></returns>
        private bool CheckPrimary(string primaryidname)
        {
            try
            {
                foreach (string skipprefix in objpacket.SkipPrefix)
                {
                    if (primaryidname.StartsWith(skipprefix))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                objlog.WriteError("CheckPrimary", "Error-- " + ex.ToString(), ChannelName);
                return false;
            }
        }
        private void PlaySubtitle(string _currentprimaryid)
        {
            try
            {
                DataRow dr = _dttemplateinfo.Select("TemplateName='" + "Subtitle" + "'").FirstOrDefault();
                if (dr == null)
                {
                    FillEventLog("Subtitle Template not available", "W");
                    objlog.WriteEvents("PlaySubtitle", "Subtitle Template not available", ChannelName);
                    return;
                }
                string _receivedvalue = _currentprimaryid + ".srt";
                string bugtype = dr["BugType"].ToString().Trim();
                string param = dr["Param1"].ToString().Trim();
                int layerNO = Convert.ToInt16(dr["layerno"].ToString().Trim());
                string[] arrparam = param.Split('~');
                if (File.Exists(SubtitlePath + "\\" + _receivedvalue))
                {
                    if (arrparam.Length == 2)
                    {
                        UpdateXMLComposition("Subtitle".ToUpper(), arrparam[0], _receivedvalue, arrparam[1], bugtype);
                    }
                    LoadCgItems("Subtitle".ToUpper());
                    return;
                }
                KillCgItem("Subtitle".ToUpper(), bugtype);
                objlog.WriteEvents("PlaySubtitle", $"Subtitle file not exist for this primary id=>{_currentprimaryid}", ChannelName);
                FillEventLog($"Subtitle file not exist for this primary id=>{_currentprimaryid}", "W");
            }
            catch (Exception)
            { }
        }
#endregion
#region Anonymous Function    
#region CheckTime                
        /// <summary>
        /// To Check DateTime
        /// 1st -->EventTime
        /// 2nd -->EventDate
        /// 3rd -->LTCTime
        /// </summary>       
        internal Func<string, string, string, bool> CheckDateTime = (_time, _date, _ltctime) =>
        {
            try
            {
                DateTime StartMidnightTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(0);
                DateTime EndMidnightTime = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                string[] _LTCTime = _ltctime.Split(':');
                if (_LTCTime.Length != 4) { return false; }
                DateTime LTCDateTime = DateTime.Today.AddHours(Convert.ToInt32(_LTCTime[0])).AddMinutes(Convert.ToInt32(_LTCTime[1])).AddSeconds(Convert.ToInt32(_LTCTime[2])).AddMilliseconds(Convert.ToInt32(_LTCTime[3]) * 40);
                if (StartMidnightTime <= LTCDateTime && LTCDateTime <= EndMidnightTime) { return false; }

                DateTime EventDateTime = DateTime.ParseExact(_date, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                string[] _EventTime = _time.Split(':');
                if (_EventTime.Length != 4) { return false; }
                EventDateTime = EventDateTime.AddHours(Convert.ToInt32(_EventTime[0])).AddMinutes(Convert.ToInt32(_EventTime[1])).AddSeconds(Convert.ToInt32(_EventTime[2])).AddMilliseconds(Convert.ToInt32(_EventTime[3]) * 40);
                if (EventDateTime <= LTCDateTime)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        };
#endregion
#region Compare Date
        internal Func<string, string, bool> CompareDate = (CurrentDate, EventDate) =>
        {
            try
            {
                DateTime CurrentDateTime = DateTime.ParseExact(CurrentDate, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                DateTime EventDateTime = DateTime.ParseExact(EventDate, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                if (CurrentDateTime > EventDateTime)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        };
#endregion
#endregion
#region Datarow remove manually
        private int index = -1;
        private void dgvsecevent_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                dgvsecevent.MultiSelect = false;
                if (e.Button != MouseButtons.Right)
                {
                    return;
                }
                if (dgvsecevent.Rows.Count <= 0)
                    return;

                index = dgvsecevent.HitTest(e.X, e.Y).RowIndex;

                if (index >= 0)
                {
                    dgvsecevent.Rows[index].Selected = true;
                    cmsSecOption.Show(dgvsecevent, e.X, e.Y);

                    string status = dgvsecevent.Rows[index].Cells["SecStatus"].Value.ToString();
                    string temp = dgvsecevent.Rows[index].Cells["SecTemplate"].Value.ToString();
                    string hidestatus = dgvsecevent.Rows[index].Cells["SecIsHide"].Value.ToString();
                    MenuONOff(temp, status, hidestatus);
                }
            }
            catch
            { }
        }
        private void MenuONOff(string templatename, string templatestatus, string templatehidestatus)
        {
            try
            {
                if (templatestatus == "W" || templatestatus == "L" || templatestatus == "RE")
                {
                    killTemplateToolStripMenuItem.Enabled = false;
                    removeTemplateToolStripMenuItem.Enabled = true;

                    hideTemplateToolStripMenuItem.Enabled = false;
                    showTemplateToolStripMenuItem.Enabled = false;
                    removeAllTemplatesToolStripMenuItem.Enabled = true;
                }
                else
                {
                    killTemplateToolStripMenuItem.Enabled = true;
                    removeTemplateToolStripMenuItem.Enabled = false;
                    removeAllTemplatesToolStripMenuItem.Enabled = false;

                    if (templatehidestatus == "Y")
                    {
                        hideTemplateToolStripMenuItem.Enabled = false;
                        showTemplateToolStripMenuItem.Enabled = true;
                    }
                    else if (templatehidestatus == "N")
                    {
                        hideTemplateToolStripMenuItem.Enabled = true;
                        showTemplateToolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch
            { }
        }
        private void removeTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvsecevent.Rows[index].Cells["SecDeleteCheck"].Value = "Y";
            }
            catch (Exception ex)
            {
                objlog.WriteError("removeTemplateToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
            }
        }
        private void killTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvsecevent.Rows[index].Cells["SecStatus"].Value.ToString() == "R")
                {
                    string TemplateName = dgvsecevent.Rows[index].Cells["SecTemplate"].Value + "";
                    DialogResult result = CustomMessageBox.ShowMessageBox(Application.ProductName, $"Are you sure you want to delete running {TemplateName} template?", "Yes~No", "Warning");
                    if (result.ToString().ToUpper() == "YES")
                    {
                        dgvsecevent.Rows[index].Cells["SecDeleteCheck"].Value = "Y";
                        if (ExternalAgentMode == true)
                        {
                            try
                            {
                                if ((TemplateName.ToUpper() != "SCTE") && (TemplateName.ToUpper() != "HTMLTICKER"))
                                {
                                    string Bugtype = _dttemplateinfo.Select("TemplateName='" + TemplateName + "'").FirstOrDefault()["BUGTYPE"].ToString();
                                    KillCgItem(TemplateName, Bugtype);

                                    string che = "GFXClient||" + ChannelName + "||SUCCESS||" + TemplateName + "||" + "STOP";

                                    if (GfxclientEnable.ToUpper().Trim().Contains("TRUE"))
                                    {
                                        SendUdpResponse(GfxclientIp, GfxclientPort, che);
                                        objlog.WriteEvents("killTemplateToolStripMenuItem_Click", "Sent to client-->" + che, ChannelName);
                                    }
                                }
                                else if (TemplateName.ToUpper() == "HTMLTICKER")
                                {
                                 //   PlayHTMLGraphics("STOP", "");
                                }
                                else if (TemplateName.ToUpper() == "SCTE")
                                {
                                    TriggerSCTE104Packet("4~~00:00:00:00", "");
                                    if (UsedItems.Contains(TemplateName.ToUpper()))
                                        UsedItems.Remove(TemplateName.ToUpper());
                                }
                                dgvsecevent.Invoke((MethodInvoker)(() =>
                                {
                                    {
                                        dgvsecevent.Rows.RemoveAt(dgvsecevent.Rows[index].Index);
                                        dgvsecevent.ClearSelection();
                                        dgvsecevent.Refresh();
                                    }
                                }));

                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    dgvsecevent.Rows[index].Cells["SecDeleteCheck"].Value = "Y";
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("killTemplateToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
            }
        }
        private void hideTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HideKeyPressed = true;
                if (ExternalAgentMode == true)
                {
                    try
                    {
                        string TemplateName = dgvsecevent.Rows[index].Cells["SecTemplate"].Value + "";
                        string Bugtype = _dttemplateinfo.Select("TemplateName='" + TemplateName + "'").FirstOrDefault()["BUGTYPE"].ToString();
                        HideCgItem(TemplateName, Bugtype);
                        dgvsecevent.Rows[index].Cells["SecIsHide"].Value = "Y";
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("hideTemplateToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
            }
        }
        private void showTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowKeyPressed = true;
                if (ExternalAgentMode == true)
                {
                    try
                    {
                        string TemplateName = dgvsecevent.Rows[index].Cells["SecTemplate"].Value + "";
                        string Bugtype = _dttemplateinfo.Select("TemplateName='" + TemplateName + "'").FirstOrDefault()["BUGTYPE"].ToString();
                        ShowCgItem(TemplateName, Bugtype);
                        dgvsecevent.Rows[index].Cells["SecIsHide"].Value = "N";
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("showTemplateToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
            }
        }
        private void removeAllTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (m_objGridLock)
            {
                try
                {
                    foreach (DataGridViewRow secrow in dgvsecevent.Rows)
                    {
                        if (secrow.Cells["SecStatus"].Value.ToString() != "R")
                        {
                            secrow.Cells["SecDeleteCheck"].Value = "Y";
                            objlog.WriteEvents("removeAllTemplatesToolStripMenuItem_Click", $"templatename={secrow.Cells["SecTemplate"].Value + ""} removed from grid with not running templates condition. ", ChannelName);
                        }
                    }

                    FillEventLog("All non Running Templates removed from grid", "W");
                }
                catch (Exception ex)
                {
                    objlog.WriteError("removeAllTemplatesToolStripMenuItem_Click", "Error-- " + ex.ToString(), ChannelName);
                }
            }
        }
        private void dgvsecevent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    FillEventLog("Commit Error", "E");
                }
                if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
                {
                    FillEventLog("Cell Change Error", "E");
                }
                if (e.Context == DataGridViewDataErrorContexts.Parsing)
                {
                    FillEventLog("Parsing Error", "E");
                }
                if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
                {
                    FillEventLog("LeaveControl Error", "E");
                }
                if (((e.Exception) is ConstraintException))
                {
                    DataGridView view = (DataGridView)sender;
                    view.Rows[e.RowIndex].ErrorText = "an error";
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                    e.ThrowException = false;
                }
                e.Cancel = true;
                objlog.WriteError("DataGrid", "Error in Datagrid Handled", ChannelName);
            }
            catch (Exception)
            { }
        }

        private void secGridConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridForm ObjSecGridForm = new GridForm(this);
            ObjSecGridForm.ShowDialog();
        }
        private void RefreshConfigurationForTemplates()
        {
            try
            {
                //ChannelConfigModelClass _CCForm = new ChannelConfigModelClass(this);
                //ReadConfig(ChannelName);
                Fetchtemplatedata();
                // LoadTemplateConfigration();
                CreateTemplate();
                DirectoryInfo(TemplatePath);
                // objpacket.StorePrefix();
                FillEventLog("Config Refreshed", "G");
            }
            catch (Exception ex)
            {
                objlog.WriteError("refressConfigToolStripMenuItem_Click", "Error while refreshing configs " + ex.ToString(), ChannelName);
                FillEventLog("Error while refreshing", "E");
            }
        }
        private void CheckIfModificationInTemplates()
        {
            try
            {
                if (EnableDb == false) { return; }
                if (DbOnlyForAsrun == true) { return; }
                while (AutoUpdateTemplate == true)
                {


                    DbLayer _db = new DbLayer(this);
                    int getCount = _db.ExecuteSclare("SELECT COUNT(*) from templatemaster where updateflg=1 AND channelid='" + ChannelName + "'");
                    if (getCount > 0)
                    {
                        RefreshConfigurationForTemplates();
                        _db.ExecuteQuery("UPDATE templatemaster SET updateflg=0  WHERE channelid='" + ChannelName + "'");
                    }
                    else
                    {
                        //objlog.WriteEvents("createtemplate", "No updated data found in tempatemaster", ChannelName);
                        // FillEventLog("No updated data found in tempatemaster", "G");
                    }
                    Thread.Sleep(Convert.ToInt32(TemplateRefreshInterval));
                }

            }
            catch (Exception ex)
            {
                objlog.WriteError("Fetchtemplatedata", "Error-- " + ex.ToString(), ChannelName);
                return;
            }

        }

        private void CopyrightToolStrip_Click(object sender, EventArgs e)
        {

        }

        private void killAllGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                KillAllCgItem();
            }
            catch (Exception ex)
            {


            }
        }

        private void templateConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTemplate ObjTemplateConfigForm = new FrmTemplate(this);
                ObjTemplateConfigForm.Text = ChannelName + " " + "Templates";
                ObjTemplateConfigForm.ShowDialog();
            }
            catch (Exception ex)
            {
                objlog.WriteError("Fetchtemplatedata", "Error in Opening Template Setting Form -- " + ex.ToString(), ChannelName);

            }
        }

        private void CreateAppConfigXml(string AppConfigPath)
        {

            //File.Create(AppConfigPath);
            var fileStream = File.Create(AppConfigPath);
            fileStream.Close();

            DataSet dsAppConfig = new DataSet();
            DataTable dt = new DataTable("Applicationmode");
            dt.Columns.Add("ExternalAgentMode");
            dt.Columns.Add("PreviewWindow");
            dt.Rows.Add("True", "False");

            DataTable dt1 = new DataTable("ConfigData");
            dt1.Columns.Add("ChannelName");
            dt1.Columns.Add("ChannelType");
            dt1.Columns.Add("EnableDB");
            dt1.Columns.Add("DbType");
            dt1.Columns.Add("DbIPAddress");
            dt1.Columns.Add("DbPort");
            dt1.Columns.Add("DbUsername");
            dt1.Columns.Add("DbPassword");
            dt1.Columns.Add("DbName");
            dt1.Columns.Add("ReceivingPort");
            dt1.Columns.Add("LTCPort");
            dt1.Columns.Add("KeyingMode");
            dt1.Columns.Add("KeyerPort");
            dt1.Columns.Add("ReconcileKeyCheck");
            dt1.Columns.Add("OverlappingCheck");
            dt1.Columns.Add("SubtitleCheck");
            dt1.Columns.Add("PMSPacketOn");
            dt1.Columns.Add("NdiOutputName");
            dt1.Columns.Add("AutoUpdateTemplate");
            dt1.Columns.Add("TemplateRefreshInterval");
            dt1.Columns.Add("CueOffSet");
            dt1.Columns.Add("PromoPrefix");
            dt1.Columns.Add("ProgramPrefix");
            dt1.Columns.Add("CommercialPrefix");
            dt1.Columns.Add("TemplatePath");
            dt1.Columns.Add("ContentPath");
            dt1.Columns.Add("SubtitlePath");
            dt1.Rows.Add(ChannelName, "SD", "false", "MYSQL", "127.0.0.1", "5555", "", "", "", "8888", "1111", "True", "53000", "false", "false", "false", "True", "MBIV GRAPHICS SERVER OUTPUT", "False", "60000", "75", "", "", "", "", "", "");

            DataTable dt2 = new DataTable("ScteSetting");
            dt2.Columns.Add("ScteEnable");
            dt2.Columns.Add("ScteTemplateName");
            dt2.Columns.Add("ScteIP");
            dt2.Columns.Add("SctePort");
            dt2.Columns.Add("SctePreRoll");
            dt1.Columns.Add("DbOnlyForAsrun");
            dt2.Rows.Add("False", "SCTEON", "127.0.0.1", "6666", "30");

            dsAppConfig.Tables.Add(dt);
            dsAppConfig.Tables.Add(dt1);
            dsAppConfig.Tables.Add(dt2);

            dsAppConfig.WriteXml(AppConfigPath);
        }

        private void menu_strip_main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {



        }

        private void triggerSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmTrigger frm = new frmTrigger(this);
                frm.ShowDialog();
            }
            catch { }
        }

        private bool FlgLogoHide = false;
        public void CheckHideUnHideLogo()
        {
            try
            {
                DataRow _hidedr = hideabbreviationdt.Select("TemplateName='LOGO'").FirstOrDefault();
                if (_hidedr != null)
                {
                    string AbbreviationToHide = (_hidedr["Abbreviation"] + "").Trim().ToUpper();
                    if (string.IsNullOrEmpty(AbbreviationToHide)) return;
                    string[] ArrAbbreviationToHide = AbbreviationToHide.Split(',');
                    bool bolUnmatchId = false;
                    foreach (string abbreviation in ArrAbbreviationToHide)
                    {
                        if (RunningPrimary.ToUpper().StartsWith(abbreviation.ToUpper()))
                        {
                            bolUnmatchId = true;
                            if (FlgLogoHide == false)
                            {
                                HideCgItem("LOGO", "");
                                FlgLogoHide = true;

                                break;
                            }
                        }
                    }
                    if (bolUnmatchId == false)
                    {
                        if (FlgLogoHide == true)
                        {
                            ShowCgItem("LOGO", "");
                            FlgLogoHide = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("CheckHideUnHideLogo", "Error-- " + ex.ToString(), ChannelName);

            }
        }


        private void keryerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string KeyerAppPath = $"{Application.StartupPath}\\Keyer\\DL Live Keyer.exe";
                if (!File.Exists(KeyerAppPath))
                {
                    objlog.WriteEvents("keryerToolStripMenuItem_Click", $"[DL Live Keyer.exe] this exe not found to [{KeyerAppPath}] Location  .", ChannelName);
                    return;
                }
                Process ObjProcess = new Process();
                ObjProcess.StartInfo.FileName = KeyerAppPath;
                Process[] pname = Process.GetProcessesByName("DL Live Keyer");
                if (pname.Length > 0)
                {
                    objlog.WriteEvents("keryerToolStripMenuItem_Click", "Keyer Application Already Running so not going to start Application Again .", ChannelName);
                }
                else
                {
                    ObjProcess.Start();
                    objlog.WriteEvents("keryerToolStripMenuItem_Click", "Keyer Application Run Sucessfully !.", ChannelName);
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("keryerToolStripMenuItem_Click", "Error--Error is Starting Keyer Application . " + ex.ToString(), ChannelName);
            }
        }
#endregion

        //------------------------START For UPDATE RUNNING TEMPLATE ELEMENT ---------------------------------------------------------------------------------------------

        public bool CheckLoadTemplate(string _templatename)
        {
            try
            {
                int CompositionCount = 0;
                m_objCharGen.CompositionsGetCount(out CompositionCount);
                for (int iCnt = 0; iCnt < CompositionCount; iCnt++)
                {
                    string CompositionName = "";
                    string Comment = "";
                    m_objCharGen.CompositionsGetByIndex(iCnt, out CompositionName, out Comment);
                    if (CompositionName.ToLower() == _templatename.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async void RefressElement(string TemplateName, string EventId, string property, string ElementValue, string BugType)
        {
            try
            {
                dynamic GfxObject = null;
                int ItemCount = 0;
                string strId = "";
                string xmldesc = "";
                switch (BugType.ToUpper())
                {
                    case "LOGO":
                        GfxObject = m_objCharGen_Logo;
                        break;
                    case "VIDEO-SQZ":
                        GfxObject = m_objCharGen_Lband;
                        break;
                    default:
                        GfxObject = m_objCharGen;
                        break;
                }
                try
                {
                    (GfxObject as CoMLCharGenClass).CompositionsGetItemsCount(TemplateName.ToLower(), ref ItemCount);
                }
                catch { }

                for (int i = 0; i < ItemCount; i++)
                {
                    strId = "";
                    xmldesc = "";
                    (GfxObject as CoMLCharGenClass).CompositionsGetItem(TemplateName.ToLower(), i, out strId, out xmldesc);
                    if (strId.ToLower() == EventId.ToString().ToLower())
                    {
                        await Task.Run(() =>
                                                m_objCharGen.SetItemProperties(strId, property, ElementValue, "", 10)
                                       );
                        objlog.WriteEvents("RefressElement", $"Refresh Template[{TemplateName}] with EventId-[{strId}] , Property-[{property}], Value -[{ElementValue}].", ChannelName);
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                objlog.WriteError("RefressElement", "Error In Refress Element- " + TemplateName + "---> " + ex.ToString(), ChannelName);
            }

        }




        //-------------------------END  For UPDATE RUNNING TEMPLATE ELEMENT------------------------------------------------------------------
        //-------------------------START  For Keyer Lband Process----------------------------------------------
        private bool FlgSqzOn = false;
        private void ActivateSquzFlag()
        {
            try
            {
                if (FlgSqzOn == false && KeyingMode == true)
                {
                    FlgSqzOn = true;
                    avprops.vidProps.fccType = eMFCC.eMFCC_Default;
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("ActivateSquzFlag", "Error-- in activating sqz flage" + ex.ToString(), ChannelName);

            }
        }
        private void DEActivateSquzFlag()
        {
            try
            {
                if (FlgSqzOn == true && KeyingMode == true)
                {
                    FlgSqzOn = false;
                    avprops.vidProps.fccType = eMFCC.eMFCC_ARGB32;
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("DEActivateSquzFlag", "Error-- in deactivating sqz flage" + ex.ToString(), ChannelName);

            }
        }






        //-------------------------END For Keyer Lband Process-------------------------------------------------

        //----------------------------Load Subtitle File -------------------------------------

        internal bool LoadSubtitle(string graphics)
        {

            string templatepath = TemplatePath + "\\" + graphics + ".ml-cgs";
            string SubtitleBugType = "";
            double SubtitleIntroTime = 0;
            double SubtitleExitTime = 0;
            lock (objreceive)
            {
                try
                {
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        SubtitleBugType = (dr["BugType"] + "").Trim();
                        _loadcgLayerNO = Convert.ToInt32((dr["LayerNo"] + "").Trim());
                        SubtitleIntroTime = Convert.ToDouble((dr["IntroTime"] + "") == string.Empty ? "0" : (dr["IntroTime"] + ""));
                        SubtitleExitTime = Convert.ToDouble((dr["ExitTime"] + "") == string.Empty ? "0" : (Convert.ToDouble(dr["ExitTime"]) / 1000).ToString());
                    }
                    dynamic GfxRunningObject = null;
                    switch (SubtitleBugType.ToUpper())
                    {
                        case "LOGO":
                            GfxRunningObject = m_objCharGen_Logo;
                            break;
                        case "VIDEO-SQZ":
                            GfxRunningObject = m_objCharGen_Lband;
                            break;
                        default:
                            GfxRunningObject = m_objCharGen;
                            break;
                    }
                    GfxRunningObject.CompositionsLoadAll(templatepath, 0);
                    GfxRunningObject.CompositionsDisplay(graphics, "", SubtitleIntroTime, 0, 0);
                 //   ChangeLayer(graphics, _loadcgLayerNO);
                    return true;
                }
                catch (Exception ex)
                {
                    objlog.WriteError("LoadCGItem", "Error in display of cg item Template Name=>" + graphics + ex.ToString(), ChannelName);
                    return false;
                }
            }
        }


        internal void KillSubtitle(string graphics)
        {
            int KillAnimationtime = 300;
            string SubtitleBugType = "";
            try
            {
                DataRow dr = _dttemplateinfo.Select("TemplateName='" + graphics + "'").FirstOrDefault();
                if (dr != null)
                {
                    KillAnimationtime = Convert.ToInt32((dr["ExitTime"] + "") == string.Empty ? "0" : dr["ExitTime"]);
                    SubtitleBugType = (dr["BugType"] + "").Trim();
                }
                dynamic GfxRunningObject = null;
                switch (SubtitleBugType.ToUpper())
                {
                    case "LOGO":
                        GfxRunningObject = m_objCharGen_Logo;
                        break;
                    case "VIDEO-SQZ":
                        GfxRunningObject = m_objCharGen_Lband;
                        break;
                    default:
                        GfxRunningObject = m_objCharGen;
                        break;
                }
                int numberOfItems = 0;
                string itemID;
                string itemXML;
                (GfxRunningObject as CoMLCharGenClass).CompositionsGetItemsCount(graphics, ref numberOfItems);
                for (int iCnt = 0; iCnt < numberOfItems; iCnt++)
                {
                    (GfxRunningObject as CoMLCharGenClass).CompositionsGetItem(graphics, 0, out itemID, out itemXML);
                    (GfxRunningObject as CoMLCharGenClass).RemoveItem(itemID, KillAnimationtime);
                    (GfxRunningObject as CoMLCharGenClass).CompositionsRemoveItem(graphics, itemID);
                }
                (GfxRunningObject as CoMLCharGenClass).CompositionsRemove(graphics.ToLower());
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("The system cannot find the path specified."))
                {
                    objlog.WriteError("KillCgItem", "Error in removing of cg item Template Name=>" + graphics + " " + ex.ToString(), ChannelName);
                }

            }
        }

        private void menu_contentList_Click(object sender, EventArgs e)
        {
            try
            {
                FrmContentList list = new FrmContentList();

                list.Show();
            }
            catch
            {


            }
        }

        //public void Previewwindow()
        //{
        //    if (isPreview == false)
        //    {
        //        if (m_objPreviewTemp == null)
        //            m_objPreviewTemp = new MFPreviewClass();
        //        try
        //        {
        //            m_objPreviewTemp.PreviewWindowSet("", ObjTmpPreviewFrm.panel1.Handle.ToInt32());
                   
        //           // m_objPreviewTemp.PropsSet("preview.downscale", "18");
                  
        //            ObjTmpPreviewFrm.Show();
        //            isPreview = true;
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        ObjTmpPreviewFrm.Hide();
        //        // ObjTmpPreviewFrm.Show();
        //        isPreview = false;
        //    }
        //}

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (isPreview == false)
            //{
            //    if (m_objPreviewTemp == null)
            //        m_objPreviewTemp = new MFPreviewClass();
            //    try
            //    {
            //        m_objPreviewTemp.PreviewWindowSet("", ObjTmpPreviewFrm.panel1.Handle.ToInt32());
                   
           
                
            //        ObjTmpPreviewFrm.Show();
            //        isPreview = true;
            //    }
            //    catch(Exception ex)
            //    {

            //    }
            //}
            //else
            //{
            //    ObjTmpPreviewFrm.Hide() ;
            
            //    isPreview = false;
            //}
        }

     

        private void lblCopyRight_Click(object sender, EventArgs e)
        {

        }

        private void Maincontainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLicenseText_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Added User Manual Document in Help Menu (13/01/2025)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = $"{Application.StartupPath}\\GFX2.X_User_Guide.pdf";
                if (File.Exists(path))
                {
                    UserManual userManual = new UserManual(path, this);
                    userManual.ShowDialog();
                }
                else
                {
                    MessageForm _msg = new MessageForm();
                    string text = "User manual is not available!";

                    _msg.ShowMessageBox(Application.ProductName, text, "OK", "Exclamation");
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("UserManualToolStripMenuItem_Click", "Error - Unable to open the UserManual", ChannelName);
            }
        }
        public void copyfiles(string spath, string dpath)
        {
            string fileName = "";
            string[] files = System.IO.Directory.GetFiles(spath);
            foreach (string s in files)
            {
                fileName = System.IO.Path.GetFileName(s);
                String destFile = System.IO.Path.Combine(dpath, fileName);
                System.IO.File.Copy(s, destFile, true);
            }
        }
        private string ReadLocale(string param)
        {
            param = param.Trim().ToUpper();
            string localepath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            localepath = localepath + @"\BackupRestore.ini";
            if (!System.IO.File.Exists(localepath))
            {
                var fileStream = File.Create(localepath);
                fileStream.Close();
                objlog.WriteEvents("ReadLocale", "Error in reading backup path file,file or path doesnt exist or not correct" + localepath, ChannelName);
                return "";
            }
            try
            {
                using (StreamReader file = new StreamReader(localepath))
                {
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        ln = ln.ToUpper();
                        if (ln.Contains(param.ToUpper()))
                        {
                            ln = ln.Replace(param, "");
                            file.Close();
                            return (ln.Trim());
                        }
                    }
                    file.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("ReadLocale", "Error in reading backup settings file,unreadable file =" + localepath, ChannelName);
                return "";
            }
        }
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != CustomMessageBox.ShowMessageBox(Application.ProductName, $"Sure to Backup ", "Yes~No", "Warning"))
                return;
            string sourcePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Channels\" + ChannelName;
            //  string backupPath = @"D:\"+ ChannelName;
            //    string backupPath = ReadLocale("Location=");
            Bkupoption bkoptionobj = new Bkupoption();
            bkoptionobj.ShowDialog();
            if (bkoptionobj.bBkupoptionflag == true)
            {
                objlog.WriteEvents("Backup_Click", "cancel the bkup", ChannelName);
                return;
            }
            BkupPath = bkoptionobj.fileNamewithpath;
            if (BkupPath.Trim() == "")
            {
                objlog.WriteEvents("Backup_Click", "Backup path doesnt exist in the BackupRestore.ini file", ChannelName);
                return;
            }
            string backupPath = BkupPath + "\\" + ChannelName;
            if (String.IsNullOrEmpty(backupPath)||(backupPath == ""))
            {
                objlog.WriteEvents("Backup_Click", "Backup path doesnt exist in the locale file", ChannelName);
                CustomMessageBox.ShowMessageBox(Application.ProductName, $"Invalid path in locale.dat", "OK", "Warning");
                return;
            }
            if(!(backupPath.Split(new string[] { ":\\" }, StringSplitOptions.None).Length >1))
            {
                objlog.WriteEvents("Backup_Click", "Wrong style backup path in the locale file", ChannelName);
                CustomMessageBox.ShowMessageBox(Application.ProductName, $"Wrong path in the locale.dat,specify like E:\\foldername", "OK", "Warning");
                return;
            }
            //  string []arr = backupPath.Split(new string[] { "\\" }, StringSplitOptions.None);
            if (backupPath.LastIndexOf('\\') == backupPath.Length - 1 ? true : false)

#if UTC_MODE
            backupPath = backupPath + DateTime.UtcNow.ToString("dd-MM-yy-HHmmss");
#else
                backupPath = backupPath + DateTime.Now.ToString("dd-MM-yy-HHmmss");
#endif
            else
#if UTC_MODE
            backupPath = backupPath + "\\" + DateTime.UtcNow.ToString("dd-MM-yy-HHmmss");
#else
                backupPath = backupPath + DateTime.Now.ToString("dd-MM-yy-HHmmss");
#endif
            
            if (!System.IO.Directory.Exists(backupPath))
            {
                try
                {
                    Directory.CreateDirectory(backupPath);
                }
                catch (Exception ex)
                {
                    objlog.WriteError("Backup_Click", $"The directory drive path does not exist {backupPath}, {ex.ToString()}", ChannelName);
                    return;
                }
            }

            Deletefolderndfiles(backupPath);
            copyfolderndfiles(sourcePath, backupPath);
            String destFile;
            string logoname = "";
            string logotype = "";
            string tmpname = "";
            if ((!findlatestLogo(out logoname, out logotype, out tmpname) && (tmpname == "")))
                return;
            string tempPath = TemplatePath + "\\"+tmpname+".ml-cgs";
            if(!File.Exists(tempPath))
            {
                objlog.WriteError("Backup_Click", "Error in finding logo template", ChannelName);
               
                return;
            }
            try
            {
                destFile = System.IO.Path.Combine(backupPath, tmpname +".ml-cgs");
                System.IO.File.Copy(tempPath, destFile, true);
            }
            catch (Exception ex)
            {
                objlog.WriteError("Backup_Click", "Error in finding logo template" + ex.Message, ChannelName);
            }
            if (logotype == "")
            {
                objlog.WriteEvents("Backup_Click", "static logo template is available,so not copying any file", ChannelName);
                CustomMessageBox.ShowMessageBox(Application.ProductName, $"BackUp complete", "OK", "Warning");
                return;
            }
            string logopath = "";
            if (logotype.Trim() == "VIDEO")
            {
                /*  logopath = ContentPath + @"\ANIMATION\" + logoname + ".mov";
                  try
                  {
                      destFile = System.IO.Path.Combine(backupPath, logoname + ".mov");
                      System.IO.File.Copy(logopath, destFile, true);
                  }
                  catch (Exception ex)
                  {
                      objlog.WriteError("Backup_Click", "Error in finding logo file" + ex.Message, ChannelName);
                  }
                  */
                string[] files = Directory.GetFiles(ContentPath + @"\ANIMATION\", logoname + ".*");
                try
                {
                    if (files.Length > 0)
                        logopath = files[0];
                    else
                    {
                        logopath = "";
                    }
                    destFile = System.IO.Path.Combine(backupPath, Path.GetFileName(logopath));
                    System.IO.File.Copy(logopath, destFile, true);
                }
                catch (Exception ex)
                {
                    objlog.WriteError("Backup_Click", "Error in finding logo file" + logopath + ex.Message, ChannelName);
                }
            }
            else
            {
                logopath = ContentPath + @"\IMAGE\" + logoname;
                try
                {
                    string extension = "";
                    string[] filefiles = Directory.GetFiles(ContentPath + @"\IMAGE\", logoname + ".*");
                    foreach (string file in filefiles)
                    {
                        extension = Path.GetExtension(file);
                    }

                    // if (string.IsNullOrEmpty(extension))
                    //    throw;
                    destFile = System.IO.Path.Combine(backupPath, logoname + extension);
                    logopath = logopath + extension;
                    System.IO.File.Copy(logopath, destFile, true);
                }
                catch (Exception ex)
                {
                    objlog.WriteError("Backup_Click", "Error in finding logo file" + ex.Message, ChannelName);
                }
            }
            CustomMessageBox.ShowMessageBox(Application.ProductName, $"BackUp complete", "OK", "Warning");
        }
        public void Deletefolderndfiles(string spath)
        {
            string fileName = "";
            try
            {
                if (System.IO.Directory.Exists(spath))
                {
                    string[] Dir = System.IO.Directory.GetDirectories(spath);
                    if (Dir.Length > 0)
                    {
                        int c = 0;
                        foreach (string s in Dir)
                        {
                            string srcD = s;
                            // string[] subd = s.Split(new string[] { "\\" }, StringSplitOptions.None);
                            //  string destD = dpath + @"\" + subd[subd.Length - 1];
                            Deletefolderndfiles(srcD);
                            // if (c == 0 && System.IO.Directory.GetFiles(spath).Length > 0)
                            //    copyfiles(spath, dpath);
                            //  c = 1;
                            // if(System.IO.Directory.GetFiles(spath).Length > 0)
                            //  {
                            string[] files = System.IO.Directory.GetFiles(spath);
                            foreach (string s1 in files)
                            {

                                // fileName = System.IO.Path.GetFileName(s);
                                // String destFile = System.IO.Path.Combine(dpath, fileName);
                                System.IO.File.Delete(s1);
                            }
                            // }
                        }
                    }
                    else
                    {
                        string[] files = System.IO.Directory.GetFiles(spath);
                        foreach (string s in files)
                        {

                            // fileName = System.IO.Path.GetFileName(s);
                            // String destFile = System.IO.Path.Combine(dpath, fileName);
                            System.IO.File.Delete(s);
                        }
                        System.IO.Directory.Delete(spath);
                    }
                }
                else
                {
                    MessageBox.Show("delete try again" + spath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("delete failed" + ex.Message);
            }
        }
        public void copyfolderndfiles(string spath, string dpath)
        {
            string fileName = "";
            // string fileName1 = "";
            try
            {
                System.IO.Directory.CreateDirectory(dpath);
                if (System.IO.Directory.Exists(spath))
                {
                    string[] Dir = System.IO.Directory.GetDirectories(spath);
                    if (Dir.Length > 0)
                    {
                        int c = 0;
                        foreach (string s in Dir)
                        {
                            string srcD = s;
                            string[] subd = s.Split(new string[] { "\\" }, StringSplitOptions.None);
                            string destD = dpath + @"\" + subd[subd.Length - 1];
                            copyfolderndfiles(srcD, destD);
                            if (c == 0 && System.IO.Directory.GetFiles(spath).Length > 0)
                                copyfiles(spath, dpath);
                            c = 1;
                        }
                    }
                    else
                    {
                        string[] files = System.IO.Directory.GetFiles(spath);
                        foreach (string s in files)
                        {

                            fileName = System.IO.Path.GetFileName(s);
                            String destFile = System.IO.Path.Combine(dpath, fileName);
                            System.IO.File.Copy(s, destFile, true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Backup Path is not correct,check and try again" + dpath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backing up failed" + ex.Message);
            }
        }
        private bool findlatestLogo(out string slogovalue, out string logotyp,out string logofile)
        {
            string _logoConfigPath = Application.StartupPath + $"//Channels//{ChannelName}//{ChannelName}_FreezeTemplate.xml";
            try
            {
                if (File.Exists(_logoConfigPath))
                {
                    DataSet _ds = new DataSet();
                    _ds.ReadXml(_logoConfigPath);
                    string _TemplateName = _ds.Tables["FreezeTemplate"].Rows[0]["TemplateName"] + "";
                    string _TemplateID = _ds.Tables["FreezeTemplate"].Rows[0]["param1ID"] + "";
                  //  int cn=_ds.Tables["FreezeTemplate"].Columns.Count;
                    logofile = _TemplateName.Trim();
                    DataRow dr = _dttemplateinfo.Select("TemplateName='" + _TemplateName.ToUpper() + "'").FirstOrDefault();
                    String param = dr["Param1"] + "";
                    if (param.Split('~').Length> 1)
                    {
                        string _LogoType = param.Split('~')[1] + "";
                        logotyp = _LogoType;
                    }
                    else
                    {
                        slogovalue = _TemplateID;
                        logotyp = "";
                    }
                    _ds.Dispose();
                    slogovalue = _TemplateID;            
                    return true;
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("findlatestLogo", "FreezeTemplate file reading failed" + ex.Message, ChannelName);
                slogovalue = "";
                logotyp = "";
                logofile = "";
                return false;
            }
            slogovalue = "";
            logotyp = "";
            logofile = "";
            return false;
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            string RestorePath;
            if (DialogResult.Yes != CustomMessageBox.ShowMessageBox(Application.ProductName, $"Sure to Restore", "Yes~No", "Warning"))
                return;

            //  string backupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Channels\" + ChannelName;
            string backupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + "BackupRestore.ini";
            //  string sourcePath = ReadLocale("Location=");
            RestorePath = File.ReadAllText(backupPath);
            string sourcePath = RestorePath + "\\" + ChannelName;
            if (String.IsNullOrEmpty(sourcePath) || (sourcePath == ""))
            {
                objlog.WriteEvents("Restore_Click", "Backup path doesnt exist in the locale file", ChannelName);
                return;
            }

            if (!System.IO.Directory.Exists(sourcePath))
            {
                objlog.WriteEvents("Restore_Click", "Backup path or location doesnt exist", ChannelName);
                return;
            }
            string[] resdir = System.IO.Directory.GetDirectories(sourcePath);
            if (resdir.Length <= 0)
            {
                objlog.WriteEvents("Restore_Click", "No Restore point available", ChannelName);
                return;
            }

            RestoreOption _ResObj = new RestoreOption();
            _ResObj.RecievedData += RestoreDataReceived;
            DataTable _customDatatable = new DataTable();
            _customDatatable.Columns.Add(new DataColumn("Available RestorePoint"));

            foreach (var pt in resdir)
            {
                DataRow dr = _customDatatable.NewRow();
                dr["Available RestorePoint"] = Path.GetFileName(pt.ToString());
                _customDatatable.Rows.Add(dr);
            }
            _ResObj.SetData(_customDatatable);
            if (DialogResult.OK != _ResObj.ShowDialog())
                return;
        }
        private void RestoreDataReceived(string obj)
        {
            string backupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Channels\" + ChannelName;
            //    string sourcePath = backupPath + "\\BackupRestore.ini"; //+ReadLocale("Location=");
            string RestorePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + "BackupRestore.ini";
            //  string sourcePath = ReadLocale("Location=");
            RestorePath = File.ReadAllText(RestorePath);
            string sourcePath = RestorePath + "\\" + ChannelName+"\\"+obj; //string backupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + "BackupRestore.ini";
            //  string sourcePath = ReadLocale("Location=");
          


            if (String.IsNullOrEmpty(sourcePath) || (sourcePath == "")|| !Directory.Exists(sourcePath))
            {
                objlog.WriteEvents("RestoreDataReceived", "Backup path in locale file, doesnt exist", ChannelName);
                return;
            }
            //    if (sourcePath.LastIndexOf('\\') == sourcePath.Length - 1 ? true : false)
            //    sourcePath = sourcePath + obj;
            //else
            //    sourcePath = sourcePath + "\\" + obj;
            if (System.IO.Directory.Exists(sourcePath))
            {
                Deletefolderndfiles(backupPath);
                copyfolderndfiles(sourcePath, backupPath);
            }
            else
            {
                objlog.WriteEvents("RestoreDataReceived", "Path to Backup, doesnt exist", ChannelName);
                return;
            }
            string logoname = "";
            string logotype = "";
            string tmpame = "";
            if (!findlatestLogo(out logoname, out logotype, out tmpame))
                return;
            string tempPath = sourcePath + $"\\{tmpame}" + ".ml-cgs";
            String destFile;
            try
            {
                destFile = System.IO.Path.Combine(TemplatePath, $"{tmpame}" + ".ml-cgs");
                if (System.IO.File.Exists(TemplatePath + $"\\{tmpame}" + ".ml-cgs"))
                    System.IO.File.Delete(TemplatePath + $"\\{tmpame}" + ".ml-cgs");
                System.IO.File.Copy(tempPath, destFile, true);
            }
            catch (Exception ex)
            {
                objlog.WriteError("RestoreDataReceived", "Error in finding logo template" + ex.Message, ChannelName);
            }
            if (logotype == "")
                return;
            string logopath = "";
            if (logotype.Trim() == "VIDEO")
                logopath = ContentPath + @"\ANIMATION";
            else
                logopath = ContentPath + @"\IMAGE";
            try
            {
                if (logotype.Trim() == "VIDEO")
                {
                    /*  destFile = System.IO.Path.Combine(logopath, logoname + ".mov");
                      if (System.IO.File.Exists(logopath + @"\" + logoname + ".mov"))
                          System.IO.File.Delete(logopath + @"\" + logoname + ".mov");
                      System.IO.File.Copy(sourcePath + @"\" + logoname + ".mov", destFile, true);*/
                    string extension = "";
                    string[] filefiles = Directory.GetFiles(ContentPath + @"\ANIMATION\", logoname + ".*");
                    foreach (string file in filefiles)
                    {
                        extension = Path.GetExtension(file);
                    }
                    destFile = System.IO.Path.Combine(logopath, logoname + extension);
                    if (System.IO.File.Exists(logopath + @"\" + logoname + extension))
                        System.IO.File.Delete(logopath + @"\" + logoname + extension);
                    System.IO.File.Copy(sourcePath + @"\" + logoname + extension, destFile, true);
                }
                else
                {
                    string extension = "";
                    //FileInfo fi = new FileInfo(logopath);
                    //string ext = fi.Extension;
                    string[] filefiles = Directory.GetFiles(ContentPath + @"\IMAGE\", logoname + ".*");
                    foreach (string file in filefiles)
                    {
                        extension = Path.GetExtension(file);
                    }
                    destFile = System.IO.Path.Combine(logopath, logoname + extension);
                    if (System.IO.File.Exists(logopath + @"\" + logoname + extension))
                        System.IO.File.Delete(logopath + @"\" + logoname + extension);
                    System.IO.File.Copy(sourcePath + @"\" + logoname + extension, destFile, true);
                }
            }
            catch (Exception ex)
            {
                objlog.WriteError("RestoreDataReceived", "Error in deleting/copying logo file from backup" + ex.Message, ChannelName);
            }
            CustomMessageBox.ShowMessageBox(Application.ProductName, $"Restore complete", "OK", "Warning");
        }
        public bool ReadHistoricdata(ref ListBox listBox1)
        {
            string ChannelHistory = Application.StartupPath + $"//Channels//{ChannelName}//ChannelbugHistory.sys";
            try
            {
                listBox1.Items.Clear();
                if (!File.Exists(ChannelHistory))
                {

                    return false;
                }
                StreamReader Textfile = new StreamReader(ChannelHistory);
                string line;
                //  IDictionary<int, string> LogoNames = new Dictionary<int, string>();
                //  int count = 0;
                while ((line = Textfile.ReadLine()) != null)
                {
                    listBox1.Items.Add(line.Trim());
                    //   LogoNames.Add(++count, line);
                }
                Textfile.Close();
                return true;
            }
            catch (Exception ex)
            {
                listBox1.Items.Clear();
                return false;
            }

        }
        public bool writeHistorydata(String Name)
        {
            string ChannelHistory = Application.StartupPath + $"//Channels//{ChannelName}//ChannelbugHistory.sys";
            try
            {
                if (!File.Exists(ChannelHistory))
                {
                    var fileStream = File.Create(ChannelHistory);
                    fileStream.Close();
                }
                StreamReader Textfile = new StreamReader(ChannelHistory);
                string line;
                IDictionary<int, string> LogoNames = new Dictionary<int, string>();
                int count = 0;
                while ((line = Textfile.ReadLine()) != null)
                {
                    LogoNames.Add(++count, line);
                }
                Textfile.Close();
                bool flag = false;
                for (int i = 0; i < LogoNames.Count; i++)
                {

                    if (LogoNames.ElementAt(i).Value == Name)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    LogoNames.Add(++count, Name);
                    using (StreamWriter writer = new StreamWriter(ChannelHistory))
                    {
                        for (int i = 0; i < LogoNames.Count; i++)
                        {

                            writer.WriteLine(LogoNames.ElementAt(i).Value);
                        }
                        writer.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public void GetContentDetail(string dirname, ref ListBox box)
        {
            string FileName = "", Extension = "";
            if (Directory.Exists(dirname))
            {
                string[] files = Directory.GetFiles(dirname);
                if (files.Length > 0)
                    box.Items.Clear();
                else
                    return;
                foreach (string file in files)
                {
                    try
                    {
                        FileName = Path.GetFileNameWithoutExtension(file);
                        Extension = Path.GetExtension(file);
                        box.Items.Add(FileName);
                    }
                    catch (Exception ex)
                    {
                        box.Items.Add(file);
                    }
                }
            }
            else
                objlog.WriteEvents("GetContentDetail", "Directory not found " + dirname, ChannelName);
        }
        public bool GetLatestBugdetails(int seq, out string res)
        {
            try
            {
                string _logoConfigPath = Application.StartupPath + $"//Channels//{ChannelName}//{ChannelName}_FreezeTemplate.xml";
                if (File.Exists(_logoConfigPath))
                {
                    DataSet _ds = new DataSet();
                    _ds.ReadXml(_logoConfigPath);
                    string _TemplateName = _ds.Tables["FreezeTemplate"].Rows[0]["TemplateName"] + "";
                    string _TemplateID = _ds.Tables["FreezeTemplate"].Rows[0]["param1ID"] + "";
                    //  string _TemplateIDtype = _ds.Tables["FreezeTemplate"].Rows[0]["param2ID"] + "";
                    string _TemplateIDtype = "";
                    try
                    {
                        DataRow dr = _dttemplateinfo.Select("TemplateName='" + _TemplateName.ToUpper() + "'").FirstOrDefault();
                        String param = dr["Param1"] + "";
                        if (param.Split('~').Length > 1)
                        {
                            string _LogoType = param.Split('~')[1] + "";
                            _TemplateIDtype = _LogoType;
                        }
                    }
                    catch(Exception ex)
                    {
                        ;
                    }
                    _ds.Dispose();
                    switch (seq)
                    {
                        case 1:
                            res = _TemplateName.Trim();
                            return true;
                        case 2:
                            res = _TemplateID.Trim();
                            return true;
                        case 3:
                            res = _TemplateIDtype.Trim();
                            return true;
                    }
                }
                res = "";
                return false;
            }
            catch (Exception ex)
            {
                res = "";
                objlog.WriteError("GetLatestBugdetails", "Error in reading freeze template" + ex.ToString(), ChannelName);
                return false;
            }
        }
        private void channelBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChannelBug ObjSecGridForm = new ChannelBug(this);
                ObjSecGridForm.ShowDialog();
            }
            catch (Exception ex)
            {
                objlog.WriteError("ChannelbugtoolStrip_Click", "Error in creating channel form", ChannelName);
            }
        }

        private void EASStart_Click(object sender, EventArgs e)
        {
            if (EASStart.Text.ToUpper() == "START")
            {
                if (EASCheck.Checked == true)
                {
                    string tickerItemXML = $@"<cg-item> 
                            <ticker type='crawl' speed='-5' gapless-crawl='yes'/> 
                            <graphics color='ML(180)-Blue(180)' line-height='1.0'/> 
                            <default-text font='{EASFontName}' bold='{EASFontStyle}' color= 'white' size='20' /> 
                        </cg-item>";

                    //                string tickerItemXML = $@"
                    //<cg-item id='ticker - 000' group-type='ticker' intro-delay='0'>
                    //    < graphics color = '0945CF(255)-F71333(255)' />< ticker type = 'crawl' speed = '-2' gapless - crawl = 'no' distance = '1' line - height = '1' >< background color = '0945CF(255)-F71333(255)' />
                    //           < default- text bold = 'true' size = '20' style = 'uppercase' color = '' font = 'Arial' font - size = '12' flip = '' />                               
                    //                               < content >                       
                    //                                     < content - item > TICKER - TEXT </ content - item >                              
                    //                                   </ content >                               
                    //                                 </ ticker >                              
                    //                                 < cg - props pos - x = '0' pos - y = '980' width = '1920' height = '100' show = 'yes' move - type = 'accel-both' alpha = '255' bg - color = 'Black(0)' pixel - ar = '0.0' play - mode = 'loop' interlace = 'auto' scale = 'fit-ar'align = 'top-left' pause = 'no' edges - smooth = '0' >< indent left = '0' right = '0' top = '0' bottom ='0' use -for-bg = 'no' />< group - indent left ='0' right = '0' top = '0' bottom ='0' />
                    //                                                                                      </ cg - props > 
                    //                                                                                    </ cg - item >

                    //                                                                                 ";
                    string tickerName = "EASAlert";
                    double posX = 0.0;
                    //  double posY = 0.9;
                    double sizeX = 0.0;
                    // double sizeY = 0.1;
                    int isRelative = 1;
                    int isDisplay = 1;
                    /*  double posX = 0.0;
                      double posY = 0.8;
                      double sizeX = 0.0;
                      double sizeY = 0.1;
                      int isRelative = 1;
                      int isDisplay =  1;
                      */
                    m_objCharGen.TickerAddNew(tickerItemXML, posX, EASPosY, sizeX, EASSizeY, isRelative, isDisplay, ref tickerName);
                    string tickerID = "EASAlert";
                    string tickerContent = "Sample Text";
                    tickerContent = EASText.Text+"\r\n"+ EASText.Text + "\r\n" + EASText.Text;
                    string tickerAddType = "remove-all";
                    m_objCharGen.TickerAddContent(tickerID, tickerContent, tickerAddType);
                    EASStart.Text = "STOP";
                }
            }
            else
            {
                try
                {
                    string graphics = "EASAlert";
                    m_objCharGen.RemoveItemWithDelay(graphics, 0, 0);
                    EASStart.Text = "START";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TickerFont_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            {
                dlg.ShowColor = false; // Disable color option
                dlg.ShowEffects = false;  // Disable style options like bold, italic, underline
                dlg.AllowScriptChange = false;  // Disable script change (like Arabic, etc.)
                dlg.FixedPitchOnly = false;  // Disables fixed-pitch fonts if needed 
                dlg.MinSize = 12;
                dlg.MaxSize = 12;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                EASFontName = dlg.Font.Name;
                EASFontSize = dlg.Font.Size;
                EASFontStyle = dlg.Font.Bold.ToString();
            }
        }

        private void BandWidthTrack_ValueChanged(object sender, EventArgs e)
        {
            if (EASScrollbar.Value >= 10 && BandWidthTrack.Value!=0)
                return;
            if ((EASCheck.Checked == true) && (EASStart.Text.ToUpper() == "STOP"))
            {
                EASSizeY = BandWidthTrack.Value / 10.0;
                string graphics = "EASAlert";
                m_objCharGen.RemoveItemWithDelay(graphics, 0, 0);
                string tickerItemXML = $@"<cg-item> 
                            <ticker type='crawl' speed='-5' gapless-crawl='yes'/> 
                            <graphics color='ML(180)-Blue(180)'/> 
                            <default-text font='{EASFontName}' bold='{EASFontStyle}' size='20' /> 
                        </cg-item>";
                ////////////////////////////////////////////////
                string tickerName = "EASAlert";
                double posX = 0.0;
                //  double posY = (double)(EASScrollbar.Value) / 10.0;
                EASPosY = (double)(EASScrollbar.Value) / 10.0;
                double sizeX = 0.0;
                //  double sizeY = 0.1;
                int isRelative = 1;
                int isDisplay = 1;
                //////////////////////////////////////////////////////
                if(EASPosY ==1 || EASSizeY ==0)
                {
                    EASSizeY = .1;
                    EASPosY = .9;
                }
                m_objCharGen.TickerAddNew(tickerItemXML, posX, EASPosY, sizeX, EASSizeY, isRelative, isDisplay, ref tickerName);
                string tickerID = "EASAlert";
                string tickerContent = "Sample Text";
                tickerContent = EASText.Text + "\r\n" + EASText.Text + "\r\n" + EASText.Text;
                string tickerAddType = "remove-all";
                m_objCharGen.TickerAddContent(tickerID, tickerContent, tickerAddType);
            }
        }

        private void EASScrollbar_ValueChanged(object sender, EventArgs e)
        {
            if (EASScrollbar.Value == 10)
                return;
            if ((EASCheck.Checked == true) && (EASStart.Text.ToUpper() == "STOP"))
            {
                string graphics = "EASAlert";
                m_objCharGen.RemoveItemWithDelay(graphics, 0, 0);
                string tickerItemXML = $@"<cg-item> 
                            <ticker type='crawl' speed='-5' gapless-crawl='yes'/> 
                            <graphics color='ML(180)-Blue(180)'/> 
                            <default-text font='{EASFontName}' bold='{EASFontStyle}' size='20' /> 
                        </cg-item>";
                ////////////////////////////////////////////////
                string tickerName = "EASAlert";
                double posX = 0.0;
                //  double posY = (double)(EASScrollbar.Value) / 10.0;
                EASPosY = (double)(EASScrollbar.Value) / 10.0;
                double sizeX = 0.0;
                //  double sizeY = 0.1;       
                int isRelative = 1;
                int isDisplay = 1;
                //////////////////////////////////////////////////////
                m_objCharGen.TickerAddNew(tickerItemXML, posX, EASPosY, sizeX, EASSizeY, isRelative, isDisplay, ref tickerName);
                string tickerID = "EASAlert";
                string tickerContent = "Sample Text";
                tickerContent = EASText.Text + "\r\n" + EASText.Text + "\r\n" + EASText.Text;
                string tickerAddType = "remove-all";
                m_objCharGen.TickerAddContent(tickerID, tickerContent, tickerAddType);
            }
        }
        private void CompareCRC(object pMFrame)
        {
            if (pMFrame == null)
                return;

            try
            {
                int size;
                long pointer;

                (pMFrame as IMFFrame).MFDataGet("__crc32_", out size, out pointer);

                if (previos_frame == IntPtr.Zero)
                {
                    previos_frame = new IntPtr(pointer);
                    return;
                }

                current_frame = new IntPtr(pointer);

                // TRUE  -> same CRC (video frozen / missing)
                // FALSE -> CRC changed (video running)
                bool isCrcEqual = current_frame.Equals(previos_frame);

                // ---------- FIRST FRAME INIT ----------
                if (lastCrcEqual == null)
                {
                    lastCrcEqual = isCrcEqual;
                    previos_frame = current_frame;
                    return;
                }

                // ---------- STATE CHANGE DETECTION ----------
                if (isCrcEqual != lastCrcEqual.Value)
                {
                    if (isCrcEqual)
                    {
                        // Video LOST
                        // Debug.WriteLine("VIDEO LOST (CRC stopped changing)");
                        missingFrameCount = 1;
                        loggedNotRecovered = false;
                        objlog.WriteError("CompareCRC", "VIDEO LOST", ChannelName);
                    }
                    else
                    {
                        // Video RECOVERED
                        //  Debug.WriteLine("VIDEO RECOVERED (CRC changing)");
                        missingFrameCount = 0;
                        loggedNotRecovered = false;
                        objlog.WriteEvents("CompareCRC", "VIDEO RECOVERED", ChannelName);
                    }

                    lastCrcEqual = isCrcEqual;
                    previos_frame = current_frame;
                    return; // important: react only once per change
                }

                // ---------- NO CHANGE: CHECK NOT-RECOVERED ----------
                if (isCrcEqual)
                {
                    missingFrameCount++;

                    if (!loggedNotRecovered && missingFrameCount >= NOT_RECOVERED_FRAMES)
                    {
                        // Debug.WriteLine("VIDEO NOT RECOVERED AFTER 20 FRAMES");
                        loggedNotRecovered = true;
                        SourceAutoStart();
                        RenderAutoStart();
                        objlog.WriteError("CompareCRC", "VIDEO NOT RECOVERED AFTER 20 FRAMES THEN REINITIALIZE THE SOURCE", ChannelName);
                    }
                }
                else
                {
                    // video running normally
                    missingFrameCount = 0;
                    loggedNotRecovered = false;
                }

                previos_frame = current_frame;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
                objlog.WriteError("CompareCRC", "error in CompareCRC" + ex.ToString(), ChannelName);
            }
        }



        //-----------Start Dongle Protection
        private bool RockyDongleProtection()
        {

            long iResult;
            try
            {

                ObjMultiAppDongleLib = new MultiAppDongleLib();

                iResult = ObjMultiAppDongleLib.InitializeDongleLib(true);    //false = without datetimecheck and true = check datetime


                string PatchNameWithPath = $"{Application.StartupPath}\\GFX2X_License.dll";    //ProvideDonglePatch
                if (!File.Exists(PatchNameWithPath))
                {
                    ClosedbyDongle = true;
                    iResult = 0;
                    //MessageBox.Show("Patch file not exist");
                    objlog.WriteError("DongleProtection", "Patch file not exist", ChannelName);
                    Environment.Exit(0);
                }


                iResult = ObjMultiAppDongleLib.MultiAppPatchApply("APP_1", PatchNameWithPath); //Provide APP Name and patch file, this App Name exist in patch as well as Dongle  
                if (iResult == 0)
                {
                    // MessageBox.Show("Patch file not apply");
                    ClosedbyDongle = true;
                    objlog.WriteError("DongleProtection", "Patch file not apply", ChannelName);
                    Environment.Exit(0);
                }

                // this function call every 12 hours and Check datetime Whether InitializeDongleLib true

                iResult = ObjMultiAppDongleLib.MultiAppDoDongleProtection("APP_1", "GFX2.X", "20.26.3.1", "SriLanka", out iDongleProtectionRemainingDays);
                if (iResult == 0)
                {
                    // MessageBox.Show("StartDongleProtection Fail");
                    ClosedbyDongle = true;
                    objlog.WriteError("DongleProtection", "StartDongleProtection Fail", ChannelName);
                    Environment.Exit(0);
                }


                //This function Call Every 15 min for checking the Dongle is available on system or not based on hardware ID
                //Also check system datetime is correct or not 
                iResult = ObjMultiAppDongleLib.FindDongleWithHID();
                if (iResult == 0)
                {
                    // MessageBox.Show("Find Dongle Failed");
                    ClosedbyDongle = true;
                    objlog.WriteError("DongleProtection", "Find Dongle Failed", ChannelName);
                    Environment.Exit(0);
                }
                else if (iResult == 10)
                {
                    // MessageBox.Show("System Time Drift");
                    ClosedbyDongle = true;
                    objlog.WriteError("DongleProtection", "System Time Drift", ChannelName);
                    Environment.Exit(0);
                }
                //------------
                string LicenseExpDate = "";
                iResult = ObjMultiAppDongleLib.GetDongleLicenseData(out LicenseExpDate);
                string[] LicArr = LicenseExpDate.Split(',');

                int Licsize = LicArr.Length;
                string startDate = "";
                if (Licsize == 2)
                {
                    //Old License Process
                    startDate = LicArr[0].Replace(@"SD=", "");
                    LicEndDate = LicArr[1].Replace(@"ED=", "");
                }
                else
                {
                    //New License Process
                    startDate = LicArr[1].Replace(@"SD=", "");
                    LicEndDate = LicArr[2].Replace(@"ED=", "");
                }
                if (iDongleProtectionRemainingDays < 8)
                {
                    lblLicenseText.Text = $"Expiry: " + LicEndDate + "[" + iDongleProtectionRemainingDays + " Days Left]";
                    lblLicenseText.ForeColor = Color.Red;
                }
                else if (iDongleProtectionRemainingDays < 30)
                {
                    lblLicenseText.Text = $"Expiry: " + LicEndDate + "[" + iDongleProtectionRemainingDays + " Days Left]";
                    lblLicenseText.ForeColor = Color.DarkOrange;
                }
                else if (iDongleProtectionRemainingDays > 30)
                {
                    lblLicenseText.Text = $"Expiry: " + LicEndDate + "[" + iDongleProtectionRemainingDays + " Days Left]";

                }
                return true;
            }
            catch (Exception ex)
            {
                objlog.WriteError("RockyDongleProtection", "MultiAppDLib error", ChannelName);
                objlog.WriteEvents("RockyDongleProtection", "MultiAppDLib error", ChannelName);
                Environment.Exit(0);
                return false;
            }
        }
        private void DongleTimer_Tick(object sender, EventArgs e)
        {

            long iResult;
            iResult = ObjMultiAppDongleLib.FindDongleWithHID();
            if (iResult == 0)
            {
                // MessageBox.Show("Find Dongle Failed");
                ClosedbyDongle = true;
                objlog.WriteError("Dongle", "Find Dongle Failed", ChannelName);
                Environment.Exit(0);
            }
            else if (iResult == 10)
            {
                // MessageBox.Show("System Time Drift");
                ClosedbyDongle = true;
                objlog.WriteError("Dongle", "System Time Drift", ChannelName);
                Environment.Exit(0);
            }
        }
     

        private void DongleTimer2_Tick(object sender, EventArgs e)
        {
            long lResult;
            lResult = ObjMultiAppDongleLib.MultiAppDoDongleProtection("APP_1", "GFX2.X", "20.26.3.1", "SriLanka", out iDongleProtectionRemainingDays);
            if (lResult == 0)
            {
                //MessageBox.Show("StartDongleProtection Fail");
                ClosedbyDongle = true;
                objlog.WriteError("DongleError", "StartDongleProtection Fail", ChannelName);
                Environment.Exit(0);
            }
            if (iDongleProtectionRemainingDays < 8)
            {
                lblLicenseText.Text = $"Expiry : " + LicEndDate + " [" + iDongleProtectionRemainingDays + " Days Left]";
                lblLicenseText.ForeColor = Color.Red;
            }
            else if (iDongleProtectionRemainingDays < 30)
            {
                lblLicenseText.Text = $"Expiry : " + LicEndDate + " [" + iDongleProtectionRemainingDays + " Days Left]";
                lblLicenseText.ForeColor = Color.DarkOrange;
            }
            else if (iDongleProtectionRemainingDays > 30)
            {
                lblLicenseText.Text = $"Expiry : " + LicEndDate + " [" + iDongleProtectionRemainingDays + " Days Left]";

            }
        }
        // --------End Dongle Protection


        #region  SCTE added by Anurag 22-4-26

        //SCTE  PACKET

        //Immidiate Packet
        //START||LOGO||LINERTEXT||SCTE||||2~~00:00:15:00||||AAAAAAAAA||||LiveGfxClient||06:58:47:02||
        //STOP||LOGO||LINERTEXT||SCTE||||4~~00:00:00:00||||AAAAAAAAA||||LiveGfxClient||06:58:47:02||

        //Schedule Packet
        //START||LOGO||LINERTEXT||SCTE||||1~~00:00:05:00||10:40:00:00||AAAAAAAAA||||LiveGfxClient||06:58:47:02||
        //STOP||LOGO||LINERTEXT||SCTE||||3~~00:00:00:00||14:15:00:00||AAAAAAAAA||||LiveGfxClient||06:58:47:02||


        public static long Trigger104MessageNumber
        {
            get { return _Trigger104MessageNumber; }
            set
            {
                if (value > 254)
                    _Trigger104MessageNumber = 0;
                else
                {
                    _Trigger104MessageNumber = value;
                }
            }
        }


        public static string ScteXmlToHexConvertor(string signal)
        {
            try
            {
                string[] spliter = { " " };
                string finalresuslt = "08FFFF";
                List<string> scte = signal.Split(spliter, StringSplitOptions.None).ToList();
                foreach (string s in scte)
                {
                    if (s.Contains("'"))
                    {
                        int result = -2;
                        int.TryParse(s.Split(new string[] { "'" }, StringSplitOptions.None)[1], out result);
                        //int tempcorrect = chkbyte(s);
                        int temp = CheckBytes(s);

                        if (result != -2)
                        {
                            if (temp != 0)
                            {
                                temp = (temp - result.ToString("x").Count());
                                for (int i = 0; i < temp; i++)
                                {
                                    finalresuslt = finalresuslt + "0";
                                }
                                finalresuslt = finalresuslt + result.ToString("X");
                            }
                        }
                    }
                }
                return finalresuslt;
            }
            catch
            {
                return "";
            }

        }


        private static int CheckBytes(string s)
        {
            if (twodigit.Contains(s.Split('=')[0]))
            {
                return 2;
            }

            else if (furdigit.Contains(s.Split('=')[0]))
            {
                return 4;
            }
            else if (s.Contains("splice_event_id"))
            {
                return 8;
            }
            return 0;

        }

        public long GetSecondsFromTime(string TimeString)
        {

            long TotalSeconds = 0;
            try
            {

                if (TimeString.Contains(".") == true)
                {
                    TimeString = TimeString.Replace('.', ':');
                }
                if (TimeString.Length == 11)
                {
                    string[] ReceivedTime = TimeString.Split(':');

                    //For Hours
                    if (ReceivedTime[0].ToString() != "")
                    {
                        TotalSeconds += Convert.ToInt16(ReceivedTime[0].ToString()) * 3600;
                    }
                    //For Minutes
                    if (ReceivedTime[1].ToString() != "")
                    {
                        TotalSeconds += Convert.ToInt16(ReceivedTime[1].ToString()) * 60;
                    }
                    //For Seconds
                    if (ReceivedTime[2].ToString() != "")
                    {
                        TotalSeconds += Convert.ToInt16(ReceivedTime[2].ToString());
                    }


                }
                else
                {

                    TotalSeconds = 0;
                }

                return TotalSeconds;
            }
            catch
            {

                return TotalSeconds;

            }
        }

        static void SplitString(string input, out string part1, out string part2)
        {
            part1 = "";
            part2 = "";

            if (string.IsNullOrEmpty(input))
                return;

            string[] parts = input.Split(new string[] { "~~" }, StringSplitOptions.None);

            if (parts.Length >= 2)
            {
                part1 = parts[0];
                part2 = parts[1];
            }
        }

        // Call this when you receive the packet
        private void SCTEScheduleExecution(string Scte_Splice_Request_splice_insert_type, string scheduledDuration)
        {
            // Parse the scheduled duration "HH:MM:SS:FF" (hours:minutes:seconds:frames)
            string[] parts = scheduledDuration.Split(':');

            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            int seconds = int.Parse(parts[2]);
            int frames = int.Parse(parts[3]); // if you need frame-level precision

            // Build today's target DateTime
            DateTime targetTime = DateTime.Today.AddHours(hours)
                                                .AddMinutes(minutes)
                                                .AddSeconds(seconds);

            // Calculate how long to wait from now
            TimeSpan delay = targetTime - DateTime.Now;

            if (delay.TotalMilliseconds <= 0)
            {
                // Target time already passed — fire immediately or skip
                objlog.WriteError("SCTEScheduleExecution", "SCTE Target time already passed", ChannelName);
                return;
            }

            // Use a System.Threading.Timer to fire once at the exact scheduled time
            // System.Threading.Timer timer = null;
            _scheduledTimer = new System.Threading.Timer(_ =>
            {
                // Scte_Splice_Request_splice_event_id = DateTime.Now.ToString("ddMMyyHHmmss");
                spliceEventId = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();
                Scte_Splice_Request_break_duration = Convert.ToString(((GetSecondsFromTime(BreakDuration)) * 10));
                string PrerollTime = "4000";

                string TriggerPacket = @"<multiple_operation_message messageSize='30' protocol_version='0' AS_index='12' message_number='" + Trigger104MessageNumber + @"' DPI_PID_index='" + Scte_Splice_Request_DPI_PID_Index + "' SCTE35_protocol_version='0' num_ops='1'><timestamp time_type='0'/><request opID='257' data_length='14'><splice_request_data splice_insert_type='" + Scte_Splice_Request_splice_insert_type + @"' splice_event_id='" + spliceEventId + @"' unique_program_id='4096' pre_roll_time='" + PrerollTime + @"' break_duration='" + Scte_Splice_Request_break_duration + @"' avail_num='" + Scte_Splice_Request_avail_num + "' avails_expected='" + Scte_Splice_Request_avails_expected + "' auto_return_flag='" + Scte_Splice_Request_auto_return_flag + "'/></request></multiple_operation_message>";
                SCTE104Packet = @"<packet did=0x41 sdid=0x07>" + ScteXmlToHexConvertor(TriggerPacket) + @"</packet>";
                insertscte = true;
                // objlog.WriteEvents("TriggerSCTE104Packet()", TriggerPacket, ChannelName);
                objlog.WriteEvents("TriggerSCTE104Packet() in Hex", SCTE104Packet, ChannelName);
                RemoveRowInGrid("SCTE");
                _scheduledTimer?.Dispose();
            }, null, (long)delay.TotalMilliseconds, Timeout.Infinite);
        }
        private void CancelScheduledExecution()
        {
            if (_scheduledTimer != null)
            {
                _scheduledTimer.Dispose();
                _scheduledTimer = null;
            }
            string OprationName = "STOP";
            string templatename = "SCTE";
            string TemplateValue = "3~~00:00:00:00";
            bool PreOrTake = false;
            objAgent.FillSCTEDataInGrid(ref OprationName, ref templatename, TemplateValue, PreOrTake);
            objlog.WriteEvents("CancelScheduledExecution()", "Receive Stop Schedule Packet", ChannelName);
        }

        public void TriggerSCTE104Packet(string ReceiveTextInfo, string ScheduledDuration)
        {
            try
            {
                Trigger104MessageNumber += 1;
                SplitString(ReceiveTextInfo, out Scte_Splice_Request_splice_insert_type, out BreakDuration);

                if (Scte_Splice_Request_splice_insert_type == "1" && ScheduledDuration != "")
                {

                    SCTEScheduleExecution(Scte_Splice_Request_splice_insert_type, ScheduledDuration);
                }
                else if (Scte_Splice_Request_splice_insert_type == "3")
                {
                    CancelScheduledExecution();

                }
                else
                {
                    //Scte_Splice_Request_splice_event_id = DateTime.Now.ToString("ddMMyyHHmmss");
                    spliceEventId = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();

                    Scte_Splice_Request_break_duration = Convert.ToString(((GetSecondsFromTime(BreakDuration)) * 10));
                    string PrerollTime = "4000";

                    string TriggerPacket = @"<multiple_operation_message messageSize='30' protocol_version='0' AS_index='12' message_number='" + Trigger104MessageNumber + @"' DPI_PID_index='" + Scte_Splice_Request_DPI_PID_Index + "' SCTE35_protocol_version='0' num_ops='1'><timestamp time_type='0'/><request opID='257' data_length='14'><splice_request_data splice_insert_type='" + Scte_Splice_Request_splice_insert_type + @"' splice_event_id='" + spliceEventId + @"' unique_program_id='4096' pre_roll_time='" + PrerollTime + @"' break_duration='" + Scte_Splice_Request_break_duration + @"' avail_num='" + Scte_Splice_Request_avail_num + "' avails_expected='" + Scte_Splice_Request_avails_expected + "' auto_return_flag='" + Scte_Splice_Request_auto_return_flag + "'/></request></multiple_operation_message>";
                    SCTE104Packet = @"<packet did=0x41 sdid=0x07>" + ScteXmlToHexConvertor(TriggerPacket) + @"</packet>";




                    // objlog.WriteEvents("TriggerSCTE104Packet()", TriggerPacket, ChannelName);
                    objlog.WriteEvents("TriggerSCTE104Packet() in Hex", SCTE104Packet, ChannelName);
                    insertscte = true;
                   
                }
            }

            catch (Exception ex)
            {
                objlog.WriteError("TriggerSCTE104Packet()", "SCTE Target time already passed", ex.ToString());
            }
        }

        public void overlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Htmlshow == false)
            {
                //     m_objOverlayHTML = new MFOverlayHTMLClass();
                String Urlfile = $"{Application.StartupPath}\\URL.txt";
                if (File.Exists(Urlfile))
                {
                    url = File.ReadAllText(Urlfile);
                }
                m_objOverlayHTML.BrowserPageLoad(url);

                Htmlshow = true;
                overlayToolStripMenuItem.Text = "Stop HTML Overlay";
            }
            else
            {
                Htmlshow = false;
                overlayToolStripMenuItem.Text = "Overlay HTML";
                m_objOverlayHTML.BrowserClose();
                //                m_objOverlayHTML.MFClose();

            }
        }

        #endregion SCTE end by Anurag 22-4-26


        //End of class
    }
}