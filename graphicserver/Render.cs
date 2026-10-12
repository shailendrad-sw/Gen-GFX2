using MFORMATSLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
//using AJASettingLibLib;

namespace graphicserver
{
    public partial class Render : Form
    {
        FrmMainServer ObjMainServer;
        Loging ObjLog;
        public int NDIIndex = -1;
        List<InputDeviceDetail> ListInputDevice = new List<InputDeviceDetail>();
        public List<NdiVideoLineDetail> ListNDIDevice = new List<NdiVideoLineDetail>();
        
        // Anurag Added AJASettingLib  
      //  AJA_AV_Settings objAjaSetting;

        public Render(FrmMainServer obj)
        {
            InitializeComponent();
            this.ObjMainServer = obj;
            this.ObjLog = obj.objlog;
            // Anurag Added AJASettingLib  
         //  objAjaSetting = new AJA_AV_Settings();
        }

        #region Form Control and Method
        private void Render_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
                if (ObjMainServer.myLiveSource==null)
                {
                    ObjMainServer.myLiveSource = new MFLiveClass();
                }
            }
            catch (Exception)
            {
                ObjLog.WriteError("Render_Load","Error In Loading Render Form .",ObjMainServer.ChannelName);
            }
        }
        private void outpreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjMainServer.outrender != null)
                {
                    ObjMainServer.outrender.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("outpreview_Click", "Error In Open Render Form .", ObjMainServer.ChannelName);
            }
        }

        private void GetInputDeviceName()
        {
            int nDevices = 0;
            ListInputDevice.Clear();
            try
            {
                string Log = "";
                ObjMainServer.myLiveSource.DeviceGetCount(eMFDeviceType.eMFDT_Video, out nDevices);
                for (int i = 0; i < nDevices; i++)
                {
                    string strName;
                    int nBusy;
                    ObjMainServer.myLiveSource.DeviceGetByIndex(eMFDeviceType.eMFDT_Video, i, out strName, out nBusy);
                    Log = Log=="" ? strName: (Log+","+strName) ;
                    if (strName.ToUpper().Contains("NDI")||strName.ToUpper().Contains("DECKLINK") || strName.ToUpper().Contains("AJA") || strName.ToUpper().Contains("EPOCH") || strName.ToUpper().Contains("DELTA"))
                    {
                        ListInputDevice.Add( new InputDeviceDetail { DeviceName= strName ,DeviceIndex=i,DeviceBussyStatus= nBusy } );
                    }
                }
                ObjLog.WriteEvents("GetInputDeviceName", $"Total Input Device Detail [ { Log} ]", ObjMainServer.ChannelName);
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("GetInputDeviceName", "Error In Getting Input Device.", ObjMainServer.ChannelName);
            }
        }
       public void GetDirtualSourceName()
        {
            if (ObjMainServer.ObjReader == null)
                ObjMainServer.ObjReader = new MFReaderClass();
            else
            {
                ObjMainServer.ObjReader.ReaderClose();
                ObjMainServer.releaseComObj(ObjMainServer.ObjReader);
                ObjMainServer.ObjReader = new MFReaderClass();
            }
            int nCount = 0;
            ObjMainServer.ObjReader.SendersGetCount(out nCount);
            string strName;
            M_VID_PROPS vidProps;
            M_AUD_PROPS audProps;
            // if (nCount > 0)

            for (int k = 0; k < nCount; k++)
            {
                ObjMainServer.ObjReader.SendersGetByIndex(k, out strName, out vidProps, out audProps);
                if (!VidLineComboBox.Items.Contains(strName))
                    VidLineComboBox.Items.Add(strName);
            }
        }
        private void cmbSourcetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                txtNdiHelp.Text = "";
                VidLineComboBox.Items.Clear();
                VidLineComboBox.Text = "";
                GetInputDeviceName();
                switch ((cmbSourcetype.SelectedItem+""))
                {
                    case "SDI":
                        foreach (InputDeviceDetail obj in ListInputDevice)
                        {
                            if (obj.DeviceName.ToUpper().Contains("DECKLINK") || obj.DeviceName.ToUpper().Contains("AJA") || obj.DeviceName.ToUpper().Contains("EPOCH") || obj.DeviceName.ToUpper().Contains("DELTA"))
                            {
                                VidLineComboBox.Items.Add(obj.DeviceName);
                            }
                        }
                        break;
                    case "NDI":
                        int NdiDeviceIndex = -1;
                        foreach (InputDeviceDetail obj in ListInputDevice)
                        {
                            if (obj.DeviceName.ToUpper().Contains("NDI") )
                            {
                                ObjMainServer.m_bWork = false;
                                NdiDeviceIndex = obj.DeviceIndex;
                                ObjMainServer.myLiveSource.DeviceSet(MFORMATSLib.eMFDeviceType.eMFDT_Video, obj.DeviceIndex, "");

                                ObjLog.WriteEvents("cmbSourcetype_SelectedIndexChanged", $"Ndi Device Set For Taking Input, going to search Video Line...", ObjMainServer.ChannelName);
                                break;
                            }
                        }
                        SearchNdiDeviceLineName(NdiDeviceIndex);
                        break;
                    case "VIRTUAL":
                        if (ObjMainServer.KeyingMode == false)
                        {
                            GetDirtualSourceName();
                        }
                        break;
                    default:
                        ObjLog.WriteEvents("cmbSourcetype_SelectedIndexChanged", $"Input Source Not Matched Ndi/SDI ...", ObjMainServer.ChannelName);
                        break;
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("cmbSourcetype_SelectedIndexChanged", "Error In Setting Input Source Type ." +ex.ToString(), ObjMainServer.ChannelName);
            }
        }
        private void VidLineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                if (string.Compare(cmbSourcetype.Text, "NDI", true) == 0)
                {
                    string  SelectedNdiSource =  VidLineComboBox.SelectedItem+"";
                    if(!string.IsNullOrEmpty(SelectedNdiSource))
                    {
                       var Obj=  ListNDIDevice.AsEnumerable().Where(er => er.NdiDeviceOptionsName == SelectedNdiSource).FirstOrDefault();
                        if(Obj!=null && Obj is NdiVideoLineDetail)
                        {
                            ObjMainServer.myLiveSource.PropsOptionSetByIndex("line-in", Obj.NdiVideoLineIndex);
                            ObjMainServer.m_bWork = true;
                            txtNdiHelp.Text ="INPUT SET AS - "+ Obj.NdiDeviceHelp.ToUpper();
                            SourceFileCreate("NDI");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("cmbSourcetype_SelectedIndexChanged", "Error In Setting Input Device Name" + ex.ToString(), ObjMainServer.ChannelName);
            }
        }
        private void refreshsource_Click(object sender, EventArgs e)
        {
            refreshsource.Text = "Searching..";
            refreshsource.Enabled = false;
            try
            {
                switch ((cmbSourcetype.Text+""))
                {
                    case "NDI":
                        SearchNdiDeviceLineName(-1);
                        break;
                    case "SDI":
                        SearchSDIDeviceLineName();
                        break;
                    case "VIRTUAL":
                        GetDirtualSourceName();
                        break;
                    default:
                        ObjLog.WriteEvents("refreshsource_Click", $" Source Type Parameter Not Match For Search NDI/SDI", ObjMainServer.ChannelName);
                        break;
                }

            }
            catch(Exception ex)
            {
                ObjLog.WriteError("refreshsource_Click", "Error In Refress device Name" + ex.ToString(), ObjMainServer.ChannelName);
            }finally
            {
                refreshsource.Enabled = true;
                refreshsource.Text = "Refresh";
            }
        }

        private void InitDevice_Click(object sender, EventArgs e)
        {
            try
            {
                string DeviceName = VidLineComboBox.Text + "";
                ObjLog.WriteEvents("SetDeviceInput",$"Before setdevice in initclick {DeviceName}", ObjMainServer.ChannelName);
                if (!string.IsNullOrEmpty(DeviceName))
                {
                    ObjLog.WriteEvents("SetDeviceInput", $"Before setdevice invalue combo {cmbSourcetype.Text}", ObjMainServer.ChannelName);
                    switch ((cmbSourcetype.Text + ""))
                    {
                        case "SDI":
                            SetDeviceInput("SDI", DeviceName);
                            break;
                        case "NDI":
                            SetDeviceInput("NDI", DeviceName);
                            break;
                        case "VIRTUAL":
                            SetDeviceInput("VIRTUAL", DeviceName);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("InitDevice_Click", "Error In Init Device Set for Taking Input Source NDI/SDI --" + ex.ToString(), ObjMainServer.ChannelName);
                
            }
        }
        /// <summary>
        /// Setting Input Device for capture NDI/SDI Source
        /// </summary>
        /// <param name="InputSourceType">Source Type(NDI/SDI)</param>
        /// <param name="DeviceName"> Provide Device Name To Set (Like NDI Renderer,Decklink)</param>
        public void SetDeviceInput(string InputSourceType, string DeviceName)
        {
         //   long lCheck = 0;
            try
            {
                ObjLog.WriteEvents("SetDeviceInput", $"input DeviceName= {InputSourceType} ,Device {DeviceName}", ObjMainServer.ChannelName);
                switch (InputSourceType.ToUpper())
                {
                    case "SDI":
                        foreach (InputDeviceDetail obj in ListInputDevice)
                        {
                            ObjLog.WriteEvents("SetDeviceInput", $"Going to set Device for Taking Input Source ,DeviceName= {obj.DeviceName} ,DeviceIndex {obj.DeviceIndex}", ObjMainServer.ChannelName);
                            ObjLog.WriteEvents("SetDeviceInput", $"file name to set ,DeviceName= {DeviceName}", ObjMainServer.ChannelName);
                            if (obj.DeviceName == DeviceName)
                            {
                                ObjLog.WriteEvents("SetDeviceInput", $"Going to set Device for Taking Input Source ,DeviceName= {DeviceName} ,DeviceIndex {obj.DeviceIndex}", ObjMainServer.ChannelName);
                                ObjMainServer.myLiveSource.DeviceSet(MFORMATSLib.eMFDeviceType.eMFDT_Video, obj.DeviceIndex, "");
                                //Code Added By Anurag 3-12-25  
                                if (DeviceName.ToUpper().Contains("AJA"))
                                {
                                    ObjMainServer.myLiveSource.PropsOptionSetByIndex("line-in", 0);
                                }


                                // Anurag Added AJASettingLib  
                                //if (DeviceName.ToUpper().Contains("AJA"))
                                //{
                                //    if (ObjMainServer.AJASettingsEnable.ToUpper() == "TRUE")
                                //    {
                                //        int iAjaReference = Convert.ToInt32(ObjMainServer.AJACardReference);
                                //        // ObjMainServer.myLiveSource.PropsOptionSetByIndex("line-in", 0);
                                //        if (DeviceName.ToUpper().Contains("CH 1"))
                                //        {
                                //            if (ObjMainServer.ChannelType.ToUpper() == "SD")
                                //            {
                                //                // First Parameter Aja Card Number like 1 or 2 
                                //                // Second Parameter Aja Video Format for SD 0 and HD 1
                                //                // Third Parameter  Aja Card Reference like 
                                //                // 0 For External Reference 
                                //                //  1 for SDI-1
                                //                //  2 for SDI-2 
                                //                //  3 for Free Run

                                //                lCheck = objAjaSetting.InializeAjaCard(1, 0, iAjaReference);
                                //            }
                                //            else
                                //            {
                                //                lCheck = objAjaSetting.InializeAjaCard(1, 1, iAjaReference);
                                //            }

                                //        }
                                //        if (DeviceName.ToUpper().Contains("CH 2"))
                                //        {
                                //            if (ObjMainServer.ChannelType.ToUpper() == "SD")
                                //            {
                                //                lCheck = objAjaSetting.InializeAjaCard(2, 0, iAjaReference);
                                //            }
                                //            else
                                //            {
                                //                lCheck = objAjaSetting.InializeAjaCard(2, 1, iAjaReference);

                                //            }

                                //        }

                                //    }

                                //}
                                //--------------------------End----------------------------------
                                ///
                                //if (DeviceName.ToUpper().Contains("DECKLINK"))
                                //{
                                //    // ObjMainServer.myLiveSource.PropsSet("bmd.vanc.capture", "true");
                                //    // ObjMainServer.myLiveSource.PropsSet("bmd.vanc.lines", "10");
                                //    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Medialooks\MFormats\MFLive\BMD", true);
                                //    key.SetValue("vanc.capture", "true");
                                //    key.SetValue("vanc.lines", "a");
                                //    key.Close();
                                //}
                                ObjMainServer.sourceMode = InputSourceMode.cardNDI;
                                ObjMainServer.m_bWork = true;
                                SourceFileCreate("SDI");
                                ObjLog.WriteEvents("SetDeviceInput", $"SDI Device Set for Taking Input Source ,DeviceName= {DeviceName},DeviceBusyStatus= {obj.DeviceBussyStatus} ,DeviceIndex {obj.DeviceIndex}  ", ObjMainServer.ChannelName);
                                break;
                            }
                        }
                        break;
                    case "NDI":
                        GetInputDeviceName();
                        foreach (InputDeviceDetail obj in ListInputDevice)
                        {
                            if (obj.DeviceName.ToUpper().Contains("NDI"))
                            {
                                ObjMainServer.myLiveSource.DeviceSet(eMFDeviceType.eMFDT_Video, obj.DeviceIndex, "");
                                ObjLog.WriteEvents("cmbSourcetype_SelectedIndexChanged", $"Ndi Device Set For Taking Input, going to search Video Line...", ObjMainServer.ChannelName);
                                var Objndidata = ListNDIDevice.AsEnumerable().Where(er => er.NdiDeviceOptionsName == DeviceName).FirstOrDefault();
                                if (Objndidata != null && Objndidata is NdiVideoLineDetail)
                                {
                                    ObjMainServer.myLiveSource.PropsOptionSetByIndex("line-in", Objndidata.NdiVideoLineIndex);
                                    ObjMainServer.sourceMode = InputSourceMode.cardNDI;
                                    ObjMainServer.m_bWork = true;
                                    txtNdiHelp.Text = "INPUT SET AS - " + Objndidata.NdiDeviceHelp.ToUpper();
                                    SourceFileCreate("NDI");
                                }
                                break;
                            }
                        }
                        break;
                    case "VIRTUAL":
                        string VirtualURL = "mp://" + DeviceName;
                        try
                        {
                            
                            ObjMainServer.ObjReader.ReaderOpen(VirtualURL, "");
                            ObjMainServer.sourceMode = InputSourceMode.virt;
                            SourceFileCreate("VIRTUAL");
                            ObjMainServer.m_bWork = true;
                            ObjLog.WriteEvents("SetDeviceInput", "Source Type Virtual properly set set."+VirtualURL, ObjMainServer.ChannelName);
                        }
                        catch(Exception ex)
                        {
                            ObjLog.WriteEvents("SetDeviceInput", "Source Type VirtualURL not set."+VirtualURL, ObjMainServer.ChannelName);
                        }
                        break;
                    default:
                        ObjLog.WriteEvents("SetDeviceInput", $"Source Type NDI/SDI not Matched for Device Set.", ObjMainServer.ChannelName);
                        break;
                }

            }
            catch (Exception ex)
            {
                ObjLog.WriteError("SetDeviceInput", "Error In Set Device  Input Source NDI/SDI Capture --" + ex.ToString(), ObjMainServer.ChannelName);

            }
        }

        private void CloseDevice_Click(object sender, EventArgs e)
        {
            try
            {
                lock (ObjMainServer.m_objLock)
                {
                    ObjMainServer.m_bWork = false;

                    VidLineComboBox.Items.Clear();
                    VidLineComboBox.Text = "";
                    ObjMainServer.myLiveSource.DeviceClose();
                    ObjMainServer.ObjReader.ReaderClose();
                }
            }
            catch(Exception ex)
            {
                ObjLog.WriteError("CloseDevice_Click", "Error In Closing  Device --" + ex.ToString(), ObjMainServer.ChannelName);
            }
        }
        private void Closeformbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("Closeformbutton_Click", "Error In Closing  Form --" + ex.ToString(), ObjMainServer.ChannelName);

            }
    
        }

        #endregion

        #region "Method and Function"
        //private void ClearRenderers()
        //{
        //    try
        //    {
        //        for (int i = 0; i < ObjMainServer.listViewRenderers.Count; i++)
        //        {
        //            string strName;
        //            int nBusy;
        //            ObjMainServer.myLiveSource.DeviceGetByIndex(MFORMATSLib.eMFDeviceType.eMFDT_Video, i, out strName, out nBusy);
        //            ObjMainServer.myLiveSource.DeviceClose();
        //        }
        //        ObjMainServer.listViewRenderers.Clear();
        //    }
        //    catch (Exception ex )
        //    {
        //        ObjLog.WriteError("Render_Load", "Error In ClearRenderers .", ObjMainServer.ChannelName);
        //    }
        //}
        //private void CheckLineIn()
        //{
        //    try
        //    {
        //        VidLineComboBox.Items.Clear();
        //        string strCurrent;
        //        string _pOptions;
        //        string _pHelp;
        //        int nSel = -1;
        //        int lCount;

        //        ObjMainServer.myLiveSource.PropsGet("line-in", out strCurrent);
        //        ObjMainServer.myLiveSource.PropsOptionGetCount("line-in", out lCount);
        //        if (lCount > 0 && !string.IsNullOrEmpty(cmbSourcetype.Text))
        //        {
        //            for (int i = 0; i < lCount; i++)
        //            {
        //                ObjMainServer.myLiveSource.PropsOptionGetByIndex("line-in", i, out _pOptions, out _pHelp);
        //                VidLineComboBox.Items.Add(_pHelp);

        //                if (strCurrent == _pOptions)
        //                    nSel = i;
        //            }
        //            if (nSel >= 0)
        //            {
        //                VidLineComboBox.SelectedIndex = nSel;
        //            }
        //            else
        //            {
        //                VidLineComboBox.SelectedIndex = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjLog.WriteError("CheckLineIn", ex.ToString(), ObjMainServer.ChannelName);
        //    }
        //}

        public void SearchNdiDeviceLineName(int NdiDeviceIndex)
        {
            VidLineComboBox.Items.Clear();
            VidLineComboBox.Text = "";
            try
            {
                if(NdiDeviceIndex == -1)
                {                    
                    foreach (InputDeviceDetail obj in ListInputDevice)
                    {
                        if (obj.DeviceName.ToUpper().Contains("NDI"))
                        {
                            NdiDeviceIndex = obj.DeviceIndex;
                            ObjMainServer.myLiveSource.DeviceSet(MFORMATSLib.eMFDeviceType.eMFDT_Video, obj.DeviceIndex, "");
                            ObjLog.WriteEvents("cmbSourcetype_SelectedIndexChanged", $"Ndi Device Set For Taking Input, going to search Video Line...", ObjMainServer.ChannelName);
                            break;
                        }
                    }
                }
                if(NdiDeviceIndex>-1)
                {
                    int SearchCounter = 5;
                    while (SearchCounter > 0)
                    {
                        try
                        {
                            SearchCounter--;
                            ListNDIDevice.Clear();
                            string strCurrent, _pOptions, _pHelp;
                            int nSel = -1, lCount = 0; 
                            ObjMainServer.myLiveSource.PropsGet("line-in", out strCurrent);
                            ObjMainServer.myLiveSource.PropsOptionGetCount("line-in", out lCount);
                            if (lCount > 0 && (cmbSourcetype.SelectedItem + "") == "NDI")
                            {
                                for (int i = 0; i < lCount; i++)
                                {
                                    ObjMainServer.myLiveSource.PropsOptionGetByIndex("line-in", i, out _pOptions, out _pHelp);
                                    ListNDIDevice.Add(new NdiVideoLineDetail { NdiDeviceOptionsName = _pOptions, NdiDeviceHelp = _pHelp, NdiVideoLineIndex = i });
                                }
                            }
                            Thread.Sleep(50);
                        }
                        catch { }
                    }
                    foreach (NdiVideoLineDetail item in ListNDIDevice)
                    {
                        VidLineComboBox.Items.Add(item.NdiDeviceOptionsName);
                    }
                }else
                {
                    ObjLog.WriteEvents("cmbSourcetype_SelectedIndexChanged", $"Ndi Device Not Set For Taking Input", ObjMainServer.ChannelName);
                }
            }
            catch (Exception ex)
            {
                ObjLog.WriteError("SearchNdiDeviceLineName", "Error In Geting Ndi Device Line Name...."+ex.ToString(), ObjMainServer.ChannelName);
            }
        }
        private void SearchSDIDeviceLineName()
        {
            try
            {
                VidLineComboBox.Items.Clear();
                VidLineComboBox.Text = "";
                GetInputDeviceName();
                foreach (InputDeviceDetail obj in ListInputDevice)
                {
                    if (obj.DeviceName.ToUpper().Contains("DECKLINK") || obj.DeviceName.ToUpper().Contains("AJA") || obj.DeviceName.ToUpper().Contains("EPOCH") || obj.DeviceName.ToUpper().Contains("DELTA"))
                    {
                        VidLineComboBox.Items.Add(obj.DeviceName);
                    }
                }

            }
            catch (Exception ex)
            {
                ObjLog.WriteError("SearchNdiDeviceLineName", "Error In Searching SDI Device Name...." + ex.ToString(), ObjMainServer.ChannelName);
            }
        }
        private void SourceFileCreate(string sourcetype)
        {
            try
            {
                using (FileStream fs = new FileStream($"{Application.StartupPath}\\Channels\\{ObjMainServer.ChannelName}\\SourceName.txt", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (StreamWriter sr = new StreamWriter(fs))
                    {
                        sr.WriteLine("Source Type=" + cmbSourcetype.Text);
                        sr.WriteLine("Source Name=" + VidLineComboBox.SelectedItem);
                    }
                }
            }
            catch(Exception ex)
            {
                ObjLog.WriteError("SourceFileCreate", "Error in Creating Txt File for track input Source..."+ex.ToString(), ObjMainServer.ChannelName);
            }
        }

        #endregion




        #region "Code for Moving Form"
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Headpanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

    }
    public class InputDeviceDetail
    {
        public String DeviceName { get; set; }
        public int DeviceIndex { get; set; }
        public int DeviceBussyStatus { get; set; }
    }

    public class NdiVideoLineDetail
    {
        public string NdiDeviceOptionsName { get; set; }
        public string NdiDeviceHelp { get; set; }
        public int NdiVideoLineIndex { get; set; }
    }

}
