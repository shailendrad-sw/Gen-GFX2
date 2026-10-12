using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace graphicserver
{
    class StaticGlobal
    {      
        public static string[] channels;
        public static string[] Runningchannels;
        public static void ChannelConfig()
        {
            Loging _objlog = new Loging();   
            try
            {
                StreamReader objReader;
                string strline = "";
                string FileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\ChannelMaster.ini";
                if (File.Exists(FileName) == false)
                {
                    _objlog.WriteEvents("ChannelConfig", "ChannelMaster.ini File Is Not Found please Configur File !", "");
                    return;
                }
                objReader = new System.IO.StreamReader(FileName);
                string[] lineSplit;
                while (objReader.Peek() != -1)
                {
                    strline = objReader.ReadLine();
                    lineSplit = strline.Split('=');
                    channels = lineSplit[1].Split(',');
                }
                objReader.Dispose();             
            }
            catch(Exception ex)
            {
               _objlog.WriteError("ChannelConfig", "Error in reading ChannelMaster.ini  main channel config " + ex.ToString(), "");
            }
        }



        public static void ReadRunningChannelConfig()
        {
            Loging _objlog = new Loging();
            try
            {
                StreamReader objReader;
                string strline = "";
                string FileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\RunningChannel.ini";
                if (File.Exists(FileName) == false)
                {
                    return;
                }
                objReader = new System.IO.StreamReader(FileName);
                string[] lineSplit;
                while (objReader.Peek() != -1)
                {
                    strline = objReader.ReadLine();
                    lineSplit = strline.Split('=');
                    if(lineSplit[1]!=string.Empty)
                    {
                        Runningchannels = lineSplit[1].Split(',');
                    }                  
                }
                objReader.Dispose();
            }
            catch (Exception ex)
            {
                _objlog.WriteError("ChannelConfig", "Error in reading main channel config " + ex.ToString(), "");
            }
        }

    }
}
