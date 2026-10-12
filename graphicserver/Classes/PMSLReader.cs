using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFORMATSLib;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Threading;

namespace graphicserver.Classes
{
    public class PMSLReader
    {
        FrmMainServer ObjMain = null;
        public PMSLReader(FrmMainServer ObjMain)
        {
            this.ObjMain = ObjMain;
        }
        private MFReaderClass objReaderFirst = null;
        private MFReaderClass objReaderSecond = null;
        public MF_RECT ObjRec = new MF_RECT();
        public MF_RECT ObjVirRec = new MF_RECT();
        public MF_RECT recTarget = new MF_RECT();
        private string _filePath = "";
        public string animate = "yes";
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                if (objReaderFirst != null)
                {
                    ObjWorking = LoadedReader.None;
                    objReaderFirst.PropsSet("loop", "true");
                    objReaderFirst.ReaderOpen(value, String.Empty);
                    _filePath = value;
                }
            }
        }
        public double Pos_X
        {
            get
            {
                return ObjRec.dblPosX;
            }
            set
            {
                ObjRec.dblPosX = value;
                recTarget.dblPosX = value;
            }
        }
        public double Pos_Y
        {
            get
            {
                return ObjRec.dblPosY;
            }
            set
            {
                ObjRec.dblPosY = value;
                recTarget.dblPosY = value;
            }


        }
        public double Pos_Width
        {
            get
            {
                return ObjRec.dblWidth;
            }
            set
            {
                ObjRec.dblWidth = value;
                recTarget.dblWidth = value;
            }

        }
        public double Pos_Hight
        {
            get
            {
                return ObjRec.dblHeight;
            }
            set
            {
                ObjRec.dblHeight = value;
                recTarget.dblHeight = value;
            }

        }
   
        private string _play_mode = "loop";

        private string _scale = "default";


        public void SetInputFile(string Id, String filepath)
        {
            if (objReaderFirst == null)
            {
                objReaderFirst = new MFReaderClass();
            }
            FilePath = filepath;

        }
        string _AnimationType = "";
        public string ANIMATION
        {
            get
            {
                return _AnimationType;
            }
            set
            {
                if (value == "RIGHTTOLEFT")
                {

                    ObjVirRec.dblPosX = ObjRec.dblPosX + ObjRec.dblWidth;
                    _AnimationType = value;
                }
                else if (value == "LEFTTORIGHT")
                {
                    ObjVirRec.dblPosX = ObjRec.dblPosX ;
                    recTarget.dblPosX= ObjRec.dblPosX + ObjRec.dblWidth;
                    _AnimationType = value;
                }
                else if(value == "LEFTTORIGHTIN")
                {
                    ObjVirRec.dblPosX = ObjRec.dblPosX- ObjRec.dblWidth;
                    recTarget.dblPosX = ObjRec.dblPosX ;
                    _AnimationType = value;
                }
                else if(value == "RIGHTTOLEFTOUT")
                {
                    ObjVirRec.dblPosX = ObjRec.dblPosX ;
                    recTarget.dblPosX = ObjRec.dblPosX- ObjRec.dblWidth;
                    _AnimationType = value;

                }
                else if(value == "DIAGONALIN")
                {
                    ObjVirRec.dblPosX =  - ObjRec.dblWidth;
                    ObjVirRec.dblPosY = -ObjRec.dblHeight;
                    recTarget.dblPosX = ObjRec.dblPosX;
                    recTarget.dblPosY = ObjRec.dblPosY;
                    _AnimationType = value;

                }
                else if (value == "DIAGONALOUT")
                {
                    recTarget.dblPosX = -ObjRec.dblWidth;
                    recTarget.dblPosY = -ObjRec.dblHeight;
                    ObjVirRec.dblPosX = ObjRec.dblPosX;
                    ObjVirRec.dblPosY = ObjRec.dblPosY;
                    _AnimationType = value;
                }
            }
        }
        MFAudioBufferClass m_objAudBuff = new MFAudioBufferClass();
      
        public void OverLayFrame(ref M_AV_PROPS avprops, ref MFFrame OutFrame)
        {

            try
            {

                if (objReaderFirst == null)
                {
                    return;
                }
                M_AV_PROPS avPropsSrc;
                int lAudioSamples = 0;
                OutFrame.MFAVPropsGet(out avPropsSrc, out lAudioSamples);

                // For ONLY rate conversion to source video standard
                M_AV_PROPS avPropsPIP = new M_AV_PROPS();
                avPropsPIP.vidProps.dblRate = avPropsSrc.vidProps.dblRate;

                MFFrame BGFrame = null, pFrame2scaled = null;
              
                if (ObjWorking == LoadedReader.FirstReader)
                {
                   objReaderFirst.SourceFrameConvertedGet(ref avPropsPIP, -1, out BGFrame, "");
                }else if(ObjWorking == LoadedReader.SecondReader)
                {
                    objReaderSecond.SourceFrameConvertedGet(ref avPropsPIP, -1, out BGFrame, "");
                }
                else
                {
                    objReaderFirst.SourceFrameConvertedGet(ref avPropsPIP, -1, out BGFrame, "");
                }
                if(ObjMain.SqzType=="TYPE2")
                {
                    avprops.vidProps.fccType = eMFCC.eMFCC_ARGB32;
    
                }
                else
                {
                    ObjMain.ShowPluggin(ObjMain.m_objCharGen, ref BGFrame);
                }
                     
                M_TIME mTime;
                BGFrame.MFTimeGet(out mTime);
                if ((mTime.eFFlags & eMFrameFlags.eMFF_Last) != 0)
                {
                    if(ObjWorking==LoadedReader.FirstReader)
                    {
                        ObjWorking = LoadedReader.SecondReader;
                        ObjMain.objlog.WriteEvents("OverLayFrame", "Switch Reader1=====>READER2", ObjMain.ChannelName);
                    }
                    else if(ObjWorking == LoadedReader.SecondReader)
                    {
                        ObjWorking = LoadedReader.FirstReader;
                        ObjMain.objlog.WriteEvents("OverLayFrame", "Switch Reader2=====>READER1", ObjMain.ChannelName);
                    }
                    else
                    {
                        ObjWorking = LoadedReader.None;
                    }
                    switchReader = true;
                    // indicate to switch to the next reader
                }
                M_AUD_PROPS m_PropsAudio = new M_AUD_PROPS();
                int audioSamples = 0;
                m_objAudBuff.BufferFramePut("", BGFrame, "");
                m_objAudBuff.BufferPropsGet("", out m_PropsAudio, out audioSamples);
                if (animate.ToLower() == "yes")
                {
                    BGFrame.MFResize(eMFCC.eMFCC_Default, Convert.ToInt32(ObjRec.dblWidth), Convert.ToInt16(ObjRec.dblHeight), -1, out pFrame2scaled, "", "");
                    OutFrame.MFOverlayRect(pFrame2scaled, null, ObjRec, eMFOverlayFlags.eMFOF_Crop, 1, "", "");
                }
                int samples = 0;
                // fill a frame with audio data from the buffer
                m_objAudBuff.BufferFrameFill("", OutFrame, ref samples, "");
                //remove used samples from the buffer
                m_objAudBuff.BufferRemove("", ref samples);
               
                releaseComObj(pFrame2scaled);
                releaseComObj(BGFrame);
            }
            catch
            {


            }

        }
        public void ReaderClose()
        {
            try
            {
                perFormTask = false;
                ObjWorking =LoadedReader.None;
                
                try
                {
                    if (objReaderFirst != null)
                    {
                        objReaderFirst.ReaderClose();
                        releaseComObj(objReaderFirst);
                        objReaderFirst = null;

                    }
                }
                catch { }
                try
                {
                    if (objReaderSecond != null)
                    {

                        objReaderSecond.ReaderClose();
                        releaseComObj(objReaderSecond);
                        objReaderSecond = null;
                    }
                }
                catch { }

            }
            catch
            {
            }
        }


        internal void releaseComObj(object comObj)
        {
            if (comObj != null)
            {
                try
                {
                    Marshal.ReleaseComObject(comObj);
                    comObj = null;
                }
                catch { }
            }
        }
        DataTable DtPlaylist = null;
        private void CreatePlayListStr()
        {
            try
            {
                if (DtPlaylist == null)
                {
                    DtPlaylist = new DataTable();
                    DtPlaylist.Columns.Add("Srno");
                    DtPlaylist.Columns.Add("FilePath");
                    DtPlaylist.Columns.Add("Status");
                    DtPlaylist.Columns.Add("ReaderName");
                }
                
            }
            catch { }
        }
        private void LoadPlaylst()
        {
            string log = "Provide File Name In Playlist -"+Environment.NewLine  ;
            try
            {
                if (File.Exists(ObjMain.ChannelNamepath + "\\Playlist.txt"))
                {
                    DtPlaylist.Clear();
                    string[] StrArr = File.ReadAllLines(ObjMain.ChannelNamepath + "\\Playlist.txt");
                    for (int i = 0; i < StrArr.Length; i++)
                    {
                        DtPlaylist.Rows.Add(i, StrArr[i], "W", "");
                        log = log + StrArr[i] + Environment.NewLine;
                    }

                }else
                {
                    log = "";
                    log = "Playlist File Not Found in Location ";
                }
                ObjMain.objlog.WriteEvents("LoadPlaylst", log,ObjMain.ChannelName);
            }
            catch(Exception Ex)
            {
                ObjMain.objlog.WriteEvents("LoadPlaylst", "Error In Loading Playlist File "+Ex.ToString(), ObjMain.ChannelName);
            }
        }
        LoadedReader ObjWorking = LoadedReader.None;





#if UTC_MODE
   DateTime LastLoadingTime = DateTime.UtcNow;
#else
        DateTime LastLoadingTime = DateTime.Now;
#endif
        bool switchReader = true;
        bool perFormTask = true;
        public void PerFormTask()
        {
            string log = "";
            string Status = "";
            string LoadTime = "";
            string FilePath = "";
            switchReader = true;
            perFormTask = true;
            int CurrentIndex = 0;
            int nextIndex = 0;
            bool LoadErrorFlg = false;
            CreatePlayListStr();
            LoadPlaylst();
            try
            {
                while (perFormTask)
                {
                    for (int i = 0; i < DtPlaylist.Rows.Count; i++)
                    {
                        Status = DtPlaylist.Rows[i]["Status"] + "";
                        if (Status == "W" && nextIndex == i && switchReader==true && (ObjWorking == LoadedReader.SecondReader|| ObjWorking == LoadedReader.None))
                        {
                            LoadErrorFlg = false;
                            FilePath = DtPlaylist.Rows[i]["FilePath"] + "";
                            if (!File.Exists(FilePath))
                            {
                                ObjMain.objlog.WriteEvents("PerFormTask", $"File Not Exist {FilePath}   .", ObjMain.ChannelName);
                            }

#if UTC_MODE
log = $"Try To Load Video File= {FilePath}, Reader=READER1 , Time {DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff")} ";
#else
                            log = $"Try To Load Video File= {FilePath}, Reader=READER1 , Time {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff")} ";
#endif

                            try
                            {
                                if (objReaderFirst == null) { objReaderFirst = new MFReaderClass(); }
                                objReaderFirst.ReaderOpen(FilePath, String.Empty);
                                objReaderFirst.PropsSet("loop", "true");
                            }
                            catch (Exception ex)
                            {
                                LoadErrorFlg = true;
                                log = log + ", Error In Loading Video try To Next Video Loading.. ";
                               
                            }
                            DtPlaylist.Rows[i]["Status"] = "L";

#if UTC_MODE
 DtPlaylist.Rows[i]["ReaderName"] = "objReaderFirst"+DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"); ;
                              LastLoadingTime = DateTime.UtcNow;
#else
                            DtPlaylist.Rows[i]["ReaderName"] = "objReaderFirst" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); ;
                            LastLoadingTime = DateTime.Now;
#endif
                            CurrentIndex = i;
                            nextIndex = i + 1;
                          
                            if(ObjWorking==LoadedReader.None)
                            {
                                ObjWorking = LoadedReader.FirstReader;
                            } else
                            {
                                if (LoadErrorFlg == false) { switchReader = false; }
                                
                            }        
                            
                            if(i==DtPlaylist.Rows.Count-1)
                            {
                               // DtPlaylist.AsEnumerable().Where(e=>e.Field<string>("srno")!= CurrentIndex.ToString()).ToList().ForEach(r => r["Status"] = "W");
                                DtPlaylist.Rows.OfType<DataRow>().ToList().ForEach(r => r["Status"] = "W");
                                CurrentIndex = i;
                                nextIndex = 0;
                            }

#if UTC_MODE
 log = log + $" Load Process Complete by Time {DateTime.UtcNow.ToString("dd- MM-yyyy HH:mm:ss:fff")} ";
#else
                            log = log + $" Load Process Complete by Time {DateTime.Now.ToString("dd- MM-yyyy HH:mm:ss:fff")} ";
#endif
                            ObjMain.objlog.WriteEvents("PerFormTask", log, ObjMain.ChannelName);
                            if (DtPlaylist.Rows.Count == 1)
                            {
                                perFormTask = false;
                                ObjWorking = LoadedReader.None;
                                break;
                            }
                        }
#if UTC_MODE
   if (nextIndex == i && Status == "W" && LastLoadingTime.AddSeconds(5) < DateTime.UtcNow && switchReader == true && ObjWorking == LoadedReader.FirstReader)
#else
                        if (nextIndex == i && Status == "W" && LastLoadingTime.AddSeconds(5) < DateTime.Now && switchReader == true && ObjWorking == LoadedReader.FirstReader)
#endif
                        {
                            LoadErrorFlg = false;
                            FilePath = DtPlaylist.Rows[i]["FilePath"] + "";
                            if (!File.Exists(FilePath))
                            {
                                ObjMain.objlog.WriteEvents("PerFormTask", $"File Not Exist {FilePath}   .", ObjMain.ChannelName);
                            }

#if UTC_MODE
  log = $"Try To Load Video File= {FilePath}, Reader=READER2 , Time {DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:fff")} ";
#else
                            log = $"Try To Load Video File= {FilePath}, Reader=READER2 , Time {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:fff")} ";
#endif
                            try
                                {
                                if (objReaderSecond == null) { objReaderSecond = new MFReaderClass(); }
                                
                                objReaderSecond.ReaderOpen(FilePath, String.Empty);
                                objReaderSecond.PropsSet("loop", "true");
                            }
                            catch (Exception ex)
                            {
                                LoadErrorFlg = true;
                                log = log + ", Error In Loading Video try To Next Video Loading.. ";
                               
                            }
                            DtPlaylist.Rows[i]["Status"] = "L";
#if UTC_MODE
  DtPlaylist.Rows[i]["ReaderName"] = "objReaderSecond"+ DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss");
                            LastLoadingTime = DateTime.UtcNow;
#else
                            DtPlaylist.Rows[i]["ReaderName"] = "objReaderSecond" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                            LastLoadingTime = DateTime.Now;
#endif

                            CurrentIndex = i;
                            nextIndex = i + 1;
                            if (LoadErrorFlg == false) { switchReader = false; }                            
                            if (i == DtPlaylist.Rows.Count - 1)
                            {
                               // DtPlaylist.AsEnumerable().Where(e => e.Field<string>("srno") != CurrentIndex.ToString()).ToList().ForEach(r => r["Status"] = "W");
                                DtPlaylist.Rows.OfType<DataRow>().ToList().ForEach(r => r["Status"] = "W");
                                CurrentIndex = i;
                                nextIndex = 0;
                            }
#if UTC_MODE
                            log = log + $"  Load Process Complete by Time {DateTime.UtcNow.ToString("dd- MM-yyyy HH:mm:ss:fff")} ";
#else
                            log = log + $"  Load Process Complete by Time {DateTime.Now.ToString("dd- MM-yyyy HH:mm:ss:fff")} ";
#endif

                            ObjMain.objlog.WriteEvents("PerFormTask", log, ObjMain.ChannelName);

                        }

                    }
                    Thread.Sleep(500);
                }
                

            }
            catch { }
        }







        //end of class
    }

    public enum LoadedReader
    {
        None,
        FirstReader,
        SecondReader
    }
}
