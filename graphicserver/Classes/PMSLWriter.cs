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
    public class PMSLWriter
    {
        private FrmMainServer ObjMainServer = null;
        public MFWriter mWriter = null;
        public string Url;
        public string configuration = "";
        public PMSLWriter(FrmMainServer ObjMain, string PathorUrl)
        {
            Url = PathorUrl;
        }

        public void SetConfiguration(string config)
        {
            configuration = config;
        }
     
        public bool Setwriter()
        {
            try
            {
                mWriter = new MFWriter();
                SetCodec(20, 1, 0);
                mWriter.WriterSet(Url, 1, configuration);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SetCodec(int formatIdx ,int videoCodec,int Audiocodec)
        {
           IMFProps options;
            mWriter.WriterOptionSetByIndex(eMFWriterOption.eMFWO_Format, formatIdx, out options);
            Marshal.ReleaseComObject(options);
            mWriter.WriterOptionSetByIndex(eMFWriterOption.eMFWO_VideoCodec,videoCodec, out options);
            Marshal.ReleaseComObject(options);

            mWriter.WriterOptionSetByIndex(eMFWriterOption.eMFWO_AudioCodec, Audiocodec, out options);
            Marshal.ReleaseComObject(options);
            return true;
        }
    }
}
