using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MFORMATSLib;

namespace graphicserver
{
    public partial class Renderout : Form
    {
        //private String NDIName = string.Empty;
        FrmMainServer objMainServer;
        Loging Objlog;
        private List<string> listViewRenderersout = new List<string>();
        private MFRendererClass NDIRendererMOut;
        private MFRendererClass SDIRendererOut;
        public string RendertypeName;
        public bool OutputResponse = false;
        public Renderout(FrmMainServer obj)
        {
            InitializeComponent();
            this.objMainServer = obj;
            this.Objlog = obj.objlog;
        }


        #region "Form Control Event"
        private void Renderout_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            catch (Exception ex)
            {
                Objlog.WriteError("SetRender", "Error in SetRender" + ex.ToString(), objMainServer.ChannelName);
            }

        }
        private delegate void OnUpdateStatus();
        private void updatestatus()
        {
            NtwrkStrmchbox.Checked = true;
        }
        public void Updatecheckbox()
        {
            OnUpdateStatus onstatus = new OnUpdateStatus(updatestatus);
            if (InvokeRequired)
            {
                this.Invoke(onstatus);
            }
            else
                updatestatus();
        }
        private void renderoutsrccmbbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                renderoutcombobox.Items.Clear();
                renderoutcombobox.Text = "";
                FillRendererDevice();
                for (int iCnt = 0; iCnt <= listViewRenderersout.Count - 1; iCnt++)
                {
                    if (renderoutsrccmbbox.SelectedItem.ToString().Contains("NDI") && listViewRenderersout[iCnt].ToUpper().Contains("NDI") == true)
                    {
                        renderoutcombobox.Items.Add(listViewRenderersout[iCnt]);
                        if (objMainServer.GetSink() != null)
                            objMainServer.GetSink().SinkClose();
                    }
                    else if (renderoutsrccmbbox.SelectedItem.ToString().Contains("SDI") && (listViewRenderersout[iCnt].ToUpper().Contains("DECKLINK") || listViewRenderersout[iCnt].ToUpper().Contains("AJA") || listViewRenderersout[iCnt].ToUpper().Contains("EPOCH") || listViewRenderersout[iCnt].ToUpper().Contains("DELTA")))
                    {
                        renderoutcombobox.Items.Add(listViewRenderersout[iCnt]);
                        if (objMainServer.GetSink() != null)
                            objMainServer.GetSink().SinkClose();
                    }
                    else if(renderoutsrccmbbox.SelectedItem.ToString().Contains("VIRTUAL"))
                    {
                        if(objMainServer.InitialiseVirtualDevice(""))
                            renderoutcombobox.Items.Add(objMainServer.ChannelName+"Gfx2Out");
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Objlog.WriteError("renderoutsrccmbbox_SelectedIndexChanged", "Error Is Setting output type ...." + ex.ToString(), objMainServer.ChannelName);
            }
        }
        private void renderoutcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RenderType = "", RenderDeviceName="";
            try
            {
                RenderType = renderoutsrccmbbox.SelectedItem.ToString();
                RenderDeviceName = renderoutcombobox.SelectedItem.ToString();
               
                SetDeviceForOutPut(RenderType,RenderDeviceName, out OutputResponse);
            }
            catch (Exception ex)
            {
                Objlog.WriteError("renderoutcombobox_SelectedIndexChanged", "Error Is Setting output Device ...." + ex.ToString(), objMainServer.ChannelName);
            }

        }
        private void srcoutchkbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!srcoutchkbox.Checked)
                {
                    objMainServer.ISNDI = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(renderoutcombobox.Text))
                    {
                        objMainServer.ISNDI = true;
                    }
                    else
                    {
                        srcoutchkbox.Checked = false;
                        objMainServer.CustomMessageBox.ShowMessageBox(Application.ProductName, "Please Select Source First", "OK", "Exclamation");
                    }
                }
           
            }
            catch (Exception ex)
            {
                Objlog.WriteError("srcoutchkbox_CheckedChanged", "Error Is Checking For send output Frame ...." + ex.ToString(), objMainServer.ChannelName);
            }
        }
        private void renderoutclosebut_Click(object sender, EventArgs e)
        {
            try
            {
                if (srcoutchkbox.Checked)
                {
                    srcoutchkbox.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Objlog.WriteError("renderoutclosebut_Click", "Error in Close Device OutPut ...." + ex.ToString(), objMainServer.ChannelName);
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
                Objlog.WriteError("renderoutclosebut_Click", "Error in Closing RenderOut Form ...." + ex.ToString(), objMainServer.ChannelName);
            }
        }


        #endregion




        #region "Method and Function"
        /// <summary>
        /// This Method Create Text File For Tracking RenderOut Device and Output Type. 
        /// </summary>
        private void RenderFileCreate()
        {
            try
            {
                using (FileStream fs = new FileStream($"{Application.StartupPath}\\Channels\\{objMainServer.ChannelName}\\RenderOut.txt", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (StreamWriter sr = new StreamWriter(fs))
                    {

                        sr.WriteLine("Source Type=" + renderoutsrccmbbox.Text);
                        sr.WriteLine("Source Name=" + renderoutcombobox.SelectedItem);
                    }
                }

            }
            catch (Exception ex)
            {
                Objlog.WriteError("RenderFileCreate", "Error in Creating RenderOut.txt file" + ex.ToString(), objMainServer.ChannelName);
            }
        }
        /// <summary>
        /// This Method Getting All Device and Fill in List<String> . 
        /// </summary>
        public void FillRendererDevice()
        {
            listViewRenderersout.Clear();
            try
            {
                MFRendererClass pRender = new MFRendererClass();
                int nDevices = 0;
                pRender.DeviceGetCount(eMFDeviceType.eMFDT_Video, out nDevices);
                for (int i = 0; i < nDevices; i++)
                {
                    string strName;
                    int nBusy;
                    pRender.DeviceGetByIndex(eMFDeviceType.eMFDT_Video, i, out strName, out nBusy);
                    listViewRenderersout.Add(strName);
                }
                objMainServer.releaseComObj(pRender);
            }
            catch (Exception Ex)
            {
                Objlog.WriteError("FillRendererDevice", "Error In Filling Renderout Device." + Ex.ToString(), objMainServer.ChannelName);
            }
        }
        /// <summary>
        /// This Method Send Frame To Output Rander Device (NDI and SDI)
        /// </summary>
        /// <param name="Frame">Provide Frame to send Render Device </param>
        public void SourcePreviewOut(MFFrame Frame)
        {
           
            

            try
            {
                if (renderoutsrccmbbox.Text.ToUpper() == "NDI")
                {
                    NDIRendererMOut.ReceiverFramePut(Frame, -1, "");
                }
                else if (renderoutsrccmbbox.Text.ToUpper() == "SDI" && (renderoutcombobox.Text.ToUpper().Contains("DECKLINK") || renderoutcombobox.Text.ToUpper().Contains("AJA") || renderoutcombobox.Text.ToUpper().Contains("EPOCH") || renderoutcombobox.Text.ToUpper().Contains("DELTA")))
                {
                    SDIRendererOut.ReceiverFramePut(Frame, -1, "");
                }
                else if(renderoutsrccmbbox.Text.ToUpper() == "VIRTUAL")
                {
                    objMainServer.m_objMFSink.ReceiverFramePut(Frame, -1, "");
                }
                objMainServer.releaseComObj(Frame);
            }
            catch(Exception ex)
            {
                int x = 0;
            }
        }
        internal void DisposedObjects()
        {
            try
            {
                if (NDIRendererMOut != null)
                {
                    NDIRendererMOut.DeviceClose();
                }
                //if (SDIRendererOut != null)
                //{
                //    SDIRendererOut.DeviceClose();
                //}
                objMainServer.releaseComObj(NDIRendererMOut);
                objMainServer.releaseComObj(SDIRendererOut);
            }
            catch (Exception ex)
            {


            }

        }
        /// <summary>
        /// This Metod Set Rendrer Device for produced NDI/SDI Output
        /// </summary>
        /// <param name="RenderType"> Detail for Output Type Like NDI/SDI</param>
        /// <param name="RenderDevice"> DeviceName for set output</param>
        /// <param name="boolResponse">Provide Status for device set at bool </param>
        public void SetDeviceForOutPut(string RenderType, string RenderDevice, out bool boolResponse)
        {
            boolResponse = false;
            int DeviceIndex = -1;

            try
            {
                switch (RenderType)
                {
                    case "SDI":
                        for (int iCnt = 0; iCnt <= listViewRenderersout.Count - 1; iCnt++)
                        {
                            if (listViewRenderersout[iCnt].ToUpper() == RenderDevice.ToUpper())
                            {
                                DeviceIndex = iCnt;
                                Objlog.WriteEvents("SetDeviceForOutPut", $"Device- {RenderDevice} is Found going to Set Device. ", objMainServer.ChannelName);
                                break;
                            }
                        }
                        if (DeviceIndex > -1)
                        {
                            if (SDIRendererOut == null)
                            {
                                SDIRendererOut = new MFRendererClass();
                               Thread.Sleep(200);
                            }
                            SDIRendererOut.DeviceSet(eMFDeviceType.eMFDT_Video, DeviceIndex, "");

                            //code commented by Anurag for AJA Audio 12-4-24

                            //if (objMainServer.counterKeyer == "g") objMainServer.counterKeyer = "start";
                            //SDIRendererOut.PropsSet("line-out", Application.ProductName + "[" + objMainServer.ChannelName + "]");
                            //SDIRendererOut.PropsSet("scaling_quality", "18");
                            //----------------------------------------------------
                          //  SDIRendererOut.PropsSet("experimental.direct_audio_thread", "true");
                            RenderFileCreate();
                            Objlog.WriteEvents("SetDeviceForOutPut", "RenderOut Device is Set complete. ", objMainServer.ChannelName);
                            if (objMainServer.KeyingMode)
                            {
                                string strNameKeying, strXML;
                                int nCount;
                                SDIRendererOut.PropsOptionGetCount("keying", out nCount);
                                Objlog.WriteEvents("SetRender", "Total Number Internal Keying Device- " + nCount.ToString(), objMainServer.ChannelName);
                                for (int i = 0; i < nCount; i++)
                                {
                                    strXML = string.Empty;
                                    try
                                    {
                                        SDIRendererOut.PropsOptionGetByIndex("keying", i, out strNameKeying, out strXML);

                                        if (strNameKeying == objMainServer.KeyerType)
                                        {
                                            SDIRendererOut.PropsOptionSetByIndex("keying", i);
                                            Objlog.WriteEvents("SetDeviceForOutPut", $"{objMainServer.KeyerType} Keying Method Set! ", objMainServer.ChannelName);

                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                            boolResponse = true;
                        }
                        else
                        {
                            Objlog.WriteEvents("SetDeviceForOutPut", $"Device- {RenderDevice} is not Found so output not Set.", objMainServer.ChannelName);
                            boolResponse = false;
                        }
                        break;
                    case "NDI":
                        for (int iCnt = 0; iCnt <= listViewRenderersout.Count - 1; iCnt++)
                        {
                            if (listViewRenderersout[iCnt].ToString().Contains("NDI") == true)
                            {
                                DeviceIndex = iCnt;
                                break;
                            }
                        }
                        if(DeviceIndex>-1)
                        {
                            if (NDIRendererMOut == null)
                            {
                                NDIRendererMOut = new MFRendererClass();
                                Thread.Sleep(200);
                            }
                            NDIRendererMOut.DeviceSet(MFORMATSLib.eMFDeviceType.eMFDT_Video, DeviceIndex, "");
                            RenderFileCreate();
                            if (objMainServer.NdiOutputName == string.Empty)
                            {
                                NDIRendererMOut.PropsSet("line-out", Application.ProductName);
                            }
                            else
                            {
                                NDIRendererMOut.PropsSet("line-out", objMainServer.NdiOutputName);
                            }
                            NDIRendererMOut.PropsSet("scaling_quality", "18");
                            boolResponse = true;
                        }else
                        {
                            Objlog.WriteEvents("SetDeviceForOutPut", $"Device- {RenderDevice} is not Found so output not Set.", objMainServer.ChannelName);
                            boolResponse = false;
                        }
                        break;
                    case "VIRTUAL":
                        RenderFileCreate();
                        boolResponse = true;
                        break;
                    default:
                        Objlog.WriteEvents("SetDeviceForOutPut", $"Output Type SDI/NDI are not Matched So any device Not Set", objMainServer.ChannelName);
                        boolResponse = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                Objlog.WriteError("SetDeviceForOutPut", "Error in Setting Output Render Device ." + ex.ToString(), objMainServer.ChannelName);
                boolResponse = false;
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
        private void Headerpanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        private void NtwrkStrmchbox_Click(object sender, EventArgs e)
        {
            if (NtwrkStrmchbox.Checked)
            {
                if ((objMainServer.NetworkMode.ToUpper() == "TRUE") && (objMainServer.closeNetwrkWriter() && (objMainServer.StreamMode == StreamStatus.stoped)))
                {
                    if (objMainServer.initialiseNetworkWriter() == true)
                    {
                        objMainServer.StreamMode = StreamStatus.playing;
                        NtwrkStrmchbox.Checked = true;
                    }
                }
            }
            else
            {

                NtwrkStrmchbox.Checked = false;
                objMainServer.StreamMode = StreamStatus.stoped;
            }
        }

        private void NtwrkStrmchbox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
