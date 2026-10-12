using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphicserver
{
    public class Loging
    {
        object objrtf = new object();
        public string LTCTime ="00:00:00:00";
        public string mFrameTC = "00:00:00:00";   
        public void WriteEvents(string Methodname, string StrWrite,string Channelname="")
        {
            try
            {
                lock ((objrtf))
                {
                    string LogDir = "";
                    string FileAlongWithPath = null;
                    FileStream fs = null;
                    StreamWriter objWriter = null;
                    if (string.IsNullOrEmpty(Channelname))
                    {
                        LogDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Log";
                    }
                    else
                    {
                        LogDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Channels\" + Channelname + @"\Log";
                    }
                    string DebugLogFile = "EventLog.txt";
                    if (System.IO.Directory.Exists(LogDir) == false)
                    {
                        System.IO.Directory.CreateDirectory(LogDir);
                    }

                    FileAlongWithPath = LogDir + "\\" + DebugLogFile;
                    if (System.IO.File.Exists(FileAlongWithPath) == true)
                    {
                        FileInfo fio = new FileInfo(FileAlongWithPath);
                        if (fio.Length > 1048000)
                        {
                            string moveFolder = LogDir + "\\Log\\";
                            if (System.IO.Directory.Exists(moveFolder) == false)
                                System.IO.Directory.CreateDirectory(moveFolder);
#if UTC_MODE
                    string MovefilealongwithPath = moveFolder + "EventLog" + DateTime.UtcNow.ToString("ddMMMyyHHmmmssFFF") + ".txt";
#else
                            string MovefilealongwithPath = moveFolder + "EventLog" + DateTime.Now.ToString("ddMMMyyHHmmmssFFF") + ".txt";
#endif

                            fio.MoveTo(MovefilealongwithPath);
                        }
                    }

                    if (System.IO.File.Exists(FileAlongWithPath) == true)
                    {
                        fs = new FileStream(FileAlongWithPath, FileMode.Append, FileAccess.Write);
                        objWriter = new StreamWriter(fs);
                        objWriter.BaseStream.Seek(0, SeekOrigin.End);
                    }
                    else
                    {
                        fs = new FileStream(FileAlongWithPath, FileMode.Create, FileAccess.Write);
                        objWriter = new StreamWriter(fs);
                        objWriter.BaseStream.Seek(0, SeekOrigin.End);
                    }

#if UTC_MODE
                   objWriter.WriteLine("[" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + "]" + "[" + LTCTime + "] -- " + "[" + Methodname + "()], " + StrWrite);
#else
                    objWriter.WriteLine("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]" + "[" + LTCTime + "] -- " + "[" + Methodname + "()], " + StrWrite);
#endif
                    objWriter.Close();
                    fs.Close();
                }
            }
            catch (Exception)
            { }
        }
        ///Error Log
        public void WriteError(string Methodname, string StrWrite, string Channelname="")
        {
            try
            {
                lock ((objrtf))
                {
                    string LogDir = "";
                    string FileAlongWithPath = null;
                    FileStream fs = null;
                    StreamWriter objWriter = null;
                    if(string.IsNullOrEmpty(Channelname))
                    {
                        LogDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)  + @"\Log";
                    }
                    else
                    {
                        LogDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Channels\" + Channelname + @"\Log";
                    }
 
                    string DebugLogFile = "ErrorLog.txt";
                    if (System.IO.Directory.Exists(LogDir) == false)
                    {
                        System.IO.Directory.CreateDirectory(LogDir);
                    }

                    FileAlongWithPath = LogDir + "\\" + DebugLogFile;
                    if (System.IO.File.Exists(FileAlongWithPath) == true)
                    {
                        FileInfo fio = new FileInfo(FileAlongWithPath);
                        if (fio.Length > 1048000)
                        {
                            string moveFolder = LogDir + "\\Log\\";
                            if (System.IO.Directory.Exists(moveFolder) == false)
                                System.IO.Directory.CreateDirectory(moveFolder);
#if UTC_MODE
            string MovefilealongwithPath = moveFolder + "ErrorLog" + DateTime.UtcNow.ToString("ddMMMyyHHmmmssFFF") + ".txt";
#else
                            string MovefilealongwithPath = moveFolder + "ErrorLog" + DateTime.Now.ToString("ddMMMyyHHmmmssFFF") + ".txt";
#endif

                            fio.MoveTo(MovefilealongwithPath);
                        }
                    }

                    if (System.IO.File.Exists(FileAlongWithPath) == true)
                    {
                        fs = new FileStream(FileAlongWithPath, FileMode.Append, FileAccess.Write);
                        objWriter = new StreamWriter(fs);
                        objWriter.BaseStream.Seek(0, SeekOrigin.End);
                    }
                    else
                    {
                        fs = new FileStream(FileAlongWithPath, FileMode.Create, FileAccess.Write);
                        objWriter = new StreamWriter(fs);
                        objWriter.BaseStream.Seek(0, SeekOrigin.End);
                    }
#if UTC_MODE
            objWriter.WriteLine("[" + DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss") + "]" + "[" + LTCTime + "] -- " + "[" + Methodname + "()], " + StrWrite);
#else
                    objWriter.WriteLine("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]" + "[" + LTCTime + "] -- " + "[" + Methodname + "()], " + StrWrite);
#endif

                    objWriter.Close();
                    fs.Close();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
