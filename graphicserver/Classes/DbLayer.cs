using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MySql.Data.MySqlClient;
using System.Net;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using MySqlConnector;


namespace graphicserver
{
    class DbLayer
    {
        FrmMainServer _objmain = null;

        private const int UNUSED_BUFFER_LENGTH = 4;
        private const int LOGIN_KEY_LENGTH = 20;
        private const int CIPHER_STORE_LENGTH = 4;

        string DatabaseUserName;
        string DatabasePassword;
        string DatabaseIP;
        int DatabaseMySqlPort;
       // string DatabaseName = "mediaxdb_cloud";
       // string ConnectionString;


        public DbLayer(FrmMainServer _objmain)
        {
            this._objmain = _objmain;
        }
        





        MySqlConnection mysqlcon = null;
        MySqlCommand mysqlcmd = null;
        MySqlDataAdapter mysqlda = null;
        private MySqlParameter[] param;
        string MySqlConstring = "";

        Object obj = new Object();





        public bool TestConnection( String Connectionstring ,string DbType)
        {
            try
            {               
                if (DbType == "MYSQL")
                {                    
                    MySqlConnection mysqlcon = new MySqlConnection(Connectionstring);
                    mysqlcon.Open();                    
                    mysqlcon.Close();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CheckFTPConnection(string URL ,string userid ,string password)
        {
            if (URL == string.Empty){MessageBox.Show("Please Enter URl For Checking");return false;}
            if (userid == string.Empty) { MessageBox.Show("Please Enter FTP User Id"); return false; }
            if (password == string.Empty) { MessageBox.Show("Please Enter FTP  Password"); return false; }
            try
            {
                Uri siteUri = new Uri(URL);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(siteUri);
                request.Credentials = new NetworkCredential(userid, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                request.Timeout = 8000;
                request.Proxy = new WebProxy();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return true;
            }            
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false; 
            }
            finally
            {
                
            }
      
        }
        private void CreateConnection()
        {
            try
            {
                lock ((obj))
                {
                    if (_objmain.DbType == "MYSQL")
                    {
                        MySqlConstring = "server=" + _objmain.DbIPAddress + "; Port=" + _objmain.DbPort + ";" + "uid=" + _objmain.DbUsername + ";" + "pwd=" + _objmain.DbPassword + ";" + "database=" + _objmain.DbName + "; ";
                        mysqlcon = new MySqlConnection(MySqlConstring);
                        if (mysqlcon.State == ConnectionState.Open | mysqlcon.State == ConnectionState.Broken)
                        {
                            mysqlcon.Close();
                            mysqlcon.Open();
                        }
                        else if (mysqlcon.State == ConnectionState.Closed)
                        {
                            mysqlcon.Open();
                        }
                    }
                    else if(_objmain.DbType == "MYSQL8")
                    {
                        _objmain.objlog.WriteEvents("CreateConnection", "entred in mysql8: ", _objmain.ChannelName + "");
                        if (AssignCredentials() == true)                     
                        {
                            MySqlConstring = "Database=" + _objmain.DbName.Trim() + "; username=" + DatabaseUserName.Trim() + "; password=" + DatabasePassword.Trim() + ";" +
                             "Pooling=false; default command timeout=" + "5" + "; Server=" + DatabaseIP.Trim() + "; port=" + DatabaseMySqlPort + "; Connect Timeout= " + "5" + ";Allow User Variables=true;AllowLoadLocalInfile=True;AllowPublicKeyRetrieval=true;SslMode=None";
                            _objmain.objlog.WriteEvents("CreateConnection", "going to create connection with string : " + MySqlConstring, _objmain.ChannelName + "");
                            mysqlcon = new MySqlConnection(MySqlConstring);
                            _objmain.objlog.WriteEvents("CreateConnection", "mysqlconnection object created : ", _objmain.ChannelName + "");
                            if (mysqlcon.State == ConnectionState.Open | mysqlcon.State == ConnectionState.Broken)
                            {
                                mysqlcon.Close();
                                mysqlcon.Open();
                            }
                            else if (mysqlcon.State == ConnectionState.Closed)
                            {
                                mysqlcon.Open();
                                _objmain.objlog.WriteEvents("CreateConnection", "connection opened: ", _objmain.ChannelName + "");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Credentials");
                        }
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteEvents("CreateConnection", "Unable to Create Connection : " + ex.Message,_objmain.ChannelName+"");                
            }
        }
        private void CloseConnection()
        {
            try
            {
                lock ((obj))
                {
                    if (mysqlcon != null)
                    {
                        if (_objmain.DbType == "MYSQL" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlcon.Close();
                        }
                        else if (_objmain.DbType == "MYSQL8" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlcon.Close();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteEvents("CloseConnection", "Unable to Close Connection : " + ex.Message, _objmain.ChannelName);
            }
        }
        public int ExecuteQuery(string qry)
        {
            int result = 0;            
            try
            {
                lock ((obj))
                {
                    CreateConnection();
                    if (mysqlcon != null)
                    {
                        if (_objmain.DbType == "MYSQL" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlcmd = new MySqlCommand(qry, mysqlcon);
                            result = mysqlcmd.ExecuteNonQuery();
                        }
                        else if (_objmain.DbType == "MYSQL8" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlcmd = new MySqlCommand(qry, mysqlcon);
                            result = mysqlcmd.ExecuteNonQuery();
                        }
                        else
                        {
                            _objmain.objlog.WriteEvents("ExecuteQuery", "ConnectionState Is=> " + mysqlcon.State.ToString() + " Query Not Executed", _objmain.ChannelName);
                        }
                    }
                    else
                    {
                        _objmain.objlog.WriteEvents("ExecuteQuery", "Database server is not connected.", _objmain.ChannelName);

                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteError("ExecuteQuery", "Query is->" + qry + "Error is" + ex.ToString(), _objmain.ChannelName);
                return result;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable FetchDataFromDB(string qry)
        {
            DataTable dt_fetch = new DataTable();
            try
            {
                lock ((obj))
                {
                    CreateConnection();
                    if (mysqlcon != null)
                    {
                        if (_objmain.DbType == "MYSQL" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlda = new MySqlDataAdapter(qry, mysqlcon);
                            mysqlda.Fill(dt_fetch);                            
                            mysqlda.Dispose();
                        }
                        else if (_objmain.DbType == "MYSQL8" & (mysqlcon.State != ConnectionState.Closed))
                        {
                            mysqlda = new MySqlDataAdapter(qry, mysqlcon);
                            mysqlda.Fill(dt_fetch);
                            mysqlda.Dispose();
                        }
                        if (dt_fetch.Rows.Count == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return dt_fetch;
                        }
                    }
                    else
                    {                       
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteError("FetchDataFromDB", "query "+qry+" Error-- " + ex.ToString(), "");
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int ExecuteSclare(string qry)
        {
            int result = 0;
            try
            {
                CreateConnection();
                if (mysqlcon != null)
                {
                    if (_objmain.DbType == "MYSQL" & (mysqlcon.State != ConnectionState.Closed))
                    {
                        mysqlcmd = new MySqlCommand(qry, mysqlcon);
                        result = Convert.ToInt32(mysqlcmd.ExecuteScalar());

                    }
                    else if (_objmain.DbType == "MYSQL8" & (mysqlcon.State != ConnectionState.Closed))
                    {
                        mysqlcmd = new MySqlCommand(qry, mysqlcon);
                        result = Convert.ToInt32(mysqlcmd.ExecuteScalar());

                    }
                    else
                    {
                        _objmain.objlog.WriteEvents("ExecuteSclare", "ConnectionState Is:> " + mysqlcon.State.ToString() + " Query Not Execute.", _objmain.ChannelName+"");
                    }
                }
                else
                {
                    _objmain.objlog.WriteEvents("ExecuteSclare", "Database server is not connected.", _objmain.ChannelName+"");

                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool ExecuteScript(string scripttext)
        {
         
            try
            {

                CreateConnection();
                //MySqlScript _script = new MySqlScript(mysqlcon);
                //_script.Query = scripttext;
                //int rows = _script.Execute();
                return true;

            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteEvents("FetchDataFromDB", "Query Is =>" + scripttext + "Error is =>" + ex.ToString(), _objmain.ChannelName+"");
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int executeQueryviaParameter(string query)
        {
            int result = 0;
            try
            {
                CreateConnection();
                if (mysqlcon != null)
                {
                    if (_objmain.DbType == "MYSQL" & (mysqlcon.State != ConnectionState.Closed))
                    {
                        mysqlcmd = new MySqlCommand(query, mysqlcon);
                        mysqlcmd.CommandType = CommandType.Text;                        
                        if (param != null)
                        {
                            foreach (MySqlParameter iparam in param)
                            {
                                mysqlcmd.Parameters.Add(iparam);
                            }


                            result = mysqlcmd.ExecuteNonQuery();
                        }

                    }
                    else if (_objmain.DbType == "MYSQL8" & (mysqlcon.State != ConnectionState.Closed))
                    {
                        mysqlcmd = new MySqlCommand(query, mysqlcon);
                        mysqlcmd.CommandType = CommandType.Text;
                        if (param != null)
                        {
                            foreach (MySqlParameter iparam in param)
                            {
                                mysqlcmd.Parameters.Add(iparam);
                            }


                            result = mysqlcmd.ExecuteNonQuery();
                        }

                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                _objmain.objlog.WriteError("executeQueryviaParameter", ex.ToString(),_objmain.ChannelName+"");
                
                return result;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool ClearParam()
        {
            param = null;
            return true;
        }
        public bool AddParam(string parameterName, object Value)
        {
            if (param == null)
            {
                Array.Resize(ref param, 1);
            }
            else
            {
                Array.Resize(ref param, param.Length + 1);

            }
            int arrlength = param.Length - 1;

            param[arrlength] = new MySqlParameter(parameterName, Value);
            _objmain.objlog.WriteEvents("AddParam", " Adding paramete name-" + parameterName + " and value - " + Value, _objmain.ChannelName+"");
            return true;
        }

        //public bool AssignCredentials()
        //{

        //    try
        //    {
        //        string GfxKey = "GFX]";
        //        string FilePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\mylogin.cnf";
        //        if (File.Exists(FilePath) == false)
        //        {
        //            // _objlog.WriteEvents("ChannelConfig", "ChannelMaster.ini File Is Not Found please Configur File !", "");
        //            return false;
        //        }
        //        string DataBaseCredentials = ReadDBFile(FilePath);

        //        string[] Data = DataBaseCredentials.Split('[');
        //        foreach (var item in Data)
        //        {
        //            if (item.Contains(GfxKey))
        //            {
        //                DataBaseCredentials = item;
        //                if (_objmain.DbType == "MYSQL8" && DataBaseCredentials != null)
        //                {
        //                    string[] Credential = DataBaseCredentials.Split('\n');
        //                    DatabaseUserName = Credential[1].Replace("\"", "%").Split('%')[1];
        //                    //DatabasePassword = Credential[2].Replace("\"", "%").Split('%')[1];
        //                    DatabasePassword = Credential[2].Replace("password = ", "");
        //                    DatabasePassword = DatabasePassword.Replace("\"", "");
        //                    DatabaseIP = Credential[3].Replace("\"", "%").Split('%')[1];
        //                    DatabaseMySqlPort = Convert.ToInt32(Credential[4].Replace("= ", "%").Split('%')[1]);
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }
        //        //if (_objmain.DbType == "MYSQL8" && DataBaseCredentials != null)
        //        //{
        //        //    string[] Credential = DataBaseCredentials.Split('\n');
        //        //    DatabaseUserName = Credential[1].Replace("\"", "%").Split('%')[1];
        //        //    DatabasePassword = Credential[2].Replace("\"", "%").Split('%')[1];
        //        //    DatabasePassword = Credential[2].Replace("password = ", "");
        //        //    DatabasePassword = DatabasePassword.Replace("\"", "");
        //        //    DatabaseIP = Credential[3].Replace("\"", "%").Split('%')[1];
        //        //    DatabaseMySqlPort = Convert.ToInt32(Credential[4].Replace("= ", "%").Split('%')[1]);
        //        //}

        //        return false;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public bool AssignCredentials()
        //{
        //    string DatabaseType = "MYSQL";
        //    try
        //    {

        //        string ChannelName = "cloud_user";
        //        string GfxKey = "GFX";
        //        string FilePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\mylogin.cnf";
        //        //  string file_path = @"E:\mylogin.cnf";
        //        string DataBaseCredentials = ReadDBFile(FilePath);//.Replace(']', ' ');
        //        string[] Data = DataBaseCredentials.Split('[');
        //        foreach (var item in Data)
        //        {
        //            if (item.Contains(GfxKey))
        //            {
        //                DataBaseCredentials = item;
        //                //break;
        //                if (DatabaseType == "MYSQL" && DataBaseCredentials != null)
        //                {
        //                    //string[] Credential = DataBaseCredentials.Split('\n');
        //                    //DatabaseUserName = Credential[1].Replace("\"", "%").Split('%')[1];
        //                    ////DatabasePassword = Credential[2].Replace("\"", "%").Split('%')[1];
        //                    //DatabasePassword = Credential[2].Replace("password = ", "");
        //                    //DatabasePassword = DatabasePassword.Replace("\"", "");
        //                    //DatabaseIP = Credential[3].Replace("\"", "%").Split('%')[1];
        //                    //DatabaseMySqlPort = Convert.ToInt32(Credential[4].Replace("= ", "%").Split('%')[1]);

        //                    string[] Credential = DataBaseCredentials.Split('\n');
        //                    DatabaseUserName = Credential[1].Split('=')[1];

        //                    DatabasePassword = Credential[2].Split('=')[1];

        //                    DatabaseIP = Credential[3].Split('=')[1];
        //                    DatabaseMySqlPort = Convert.ToInt32(Credential[4].Split('=')[1]);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public bool AssignCredentials()
        {
            try
            {
                //string file_path = Application.StartupPath + @"\mylogin.cnf";

                //Start of Changes Made by Tushar on 17-06-2025 at 17:14:00

                string DataBaseCredentials;

                string file_path = _objmain.CnfPath.ToString().Trim(); // CNF FIle Path From Configuration
                //End of Changes Made by Tushar on 17-06-2025 at 17:14:00

                //Changes Made for checking if file exist or not by Tushar on 16-06-2025 at 12:27:00
                if (File.Exists(file_path))
                    DataBaseCredentials = ReadDBFile(file_path).Replace(']', ' ');
                else
                {
                    _objmain.objlog.WriteError("AssignCredentials", $"mylogin.cnf does not exists at the path {_objmain.CnfPath}", _objmain.ChannelName + "");
                    return false;
                }
                //Changes Made for checking if file exist or not by Tushar on 16-06-2025 at 12:27:00

                string[] Data = DataBaseCredentials.Split('[');

                foreach (var item in Data)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] s = item.Split('\n');
                        if (s[0].ToUpper().Trim() == "GFX")
                        {
                          //  Log(" [ClassDataBase](AssignCredentials) , Credentials Found for MediaX");
                            foreach (var i in item.Split('\n'))
                            {
                                if (i.Contains("password"))
                                {
                                    string r = i.Replace("password = ", "");
                                    r = r.Replace("\"", "");
                                    DatabasePassword = r;
                                  //  Log(" [ClassDataBase](AssignCredentials) , Database Password Set");
                                }
                                else if (i.Contains("user"))
                                {
                                    string r = i.Replace("user = ", "");
                                    r = r.Replace("\"", "");
                                    DatabaseUserName = r;
                                  //  Log(" [ClassDataBase](AssignCredentials) , Username : " + GeneralConfiguration.DatabaseUserName);
                                }
                                else if (i.Contains("host"))
                                {
                                    string r = i.Replace("host = ", "");
                                    r = r.Replace("\"", "");
                                    DatabaseIP = r;
                                 //   Log(" [ClassDataBase](AssignCredentials) , Database IP : " + GeneralConfiguration.DatabaseIP);
                                }
                                else if (i.Contains("port"))
                                {
                                    string r = i.Replace("port = ", "");
                                    r = r.Replace("\"", "");
                                    DatabaseMySqlPort = Convert.ToInt32(r);
                                 //   Log(" [ClassDataBase](AssignCredentials) , Port : " + GeneralConfiguration.DatabaseMySqlPort.ToString());
                                }
                            }
                            //   break;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log(" [ClassDataBase](AssignCredentials) , Error is: " + ex.ToString());
                return false;
            }
        }

        public string ReadDBFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    // Log(" [ClassDataBase](ReadSQLFile) .mylogin.cnf file not available");
                    return null;
                }
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] bufferC = new byte[UNUSED_BUFFER_LENGTH];
                    fs.Read(bufferC, 0, UNUSED_BUFFER_LENGTH);

                    if (bufferC.Length == 0 || bufferC.Length != UNUSED_BUFFER_LENGTH)
                    {
                        return null;
                    }

                    byte[] keyC = new byte[LOGIN_KEY_LENGTH];
                    fs.Read(keyC, 0, LOGIN_KEY_LENGTH);

                    //return CreateKey(key);
                    byte[] rKey = new byte[16];
                    for (int i = 0; i < keyC.Length; i++)
                    {
                        rKey[i % 16] ^= keyC[i];
                    }

                    byte[] key = rKey;
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider
                    {
                        Key = key,
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.None
                    };
                    AesCryptoServiceProvider cipher = aes;
                    ICryptoTransform decryptor = cipher.CreateDecryptor();
                    byte[] encryptedContent;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] buffer = new byte[CIPHER_STORE_LENGTH];

                        while (true)
                        {
                            int bytesRead = fs.Read(buffer, 0, CIPHER_STORE_LENGTH);
                            if (bytesRead < CIPHER_STORE_LENGTH)
                            {
                                break;
                            }
                            int lineLength = BitConverter.ToInt32(buffer, 0);
                            byte[] line = new byte[lineLength];
                            fs.Read(line, 0, lineLength);
                            byte[] lineC = RemovePad(decryptor.TransformFinalBlock(line, 0, line.Length));
                            ms.Write(lineC, 0, lineC.Length);
                        }
                        encryptedContent = ms.ToArray();
                    }
                    return Encoding.UTF8.GetString(encryptedContent);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] RemovePad(byte[] line_Cipher)
        {
            try
            {
                int padLength = line_Cipher[line_Cipher.Length - 1];
                if (padLength > line_Cipher.Length)
                {
                    return null;
                }
                byte[] result = new byte[line_Cipher.Length - padLength];
                Array.Copy(line_Cipher, result, result.Length);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }

}
