using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class CreateChannel : Form
    {
        public CreateChannel()
        {
            InitializeComponent();
        }
        string var;
        string[] channelarry;
        List<string> channellist = new List<string>();

        public CreateChannel(string[] list)
        {
            channelarry = list;
            foreach (string item in list)
                channellist.Add(item);


            InitializeComponent();

        }


        private void btn_CreateChannel_Click(object sender, EventArgs e)
        {
            String var = txtbox_CreateChannel.Text;
            
            bool flag = false;
            for (int no = 0; no < channelarry.Count(); no++)
            {
                if (var.Equals(channelarry[no],StringComparison.InvariantCultureIgnoreCase) == true)
                {
                    //messagebox already exist
                    MessageForm _msg = new MessageForm();
                    string text = "Channel ";
                    text = text + var + " is already exist";
                    _msg.ShowMessageBox(Application.ProductName, text, "OK", "Exclamation");
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                if (String.IsNullOrEmpty(var))
                {
                    MessageForm _msg = new MessageForm();
                    _msg.ShowMessageBox(Application.ProductName, "Channel Name is Empty", "OK", "Exclamation");
                    return;
                }
                else
                {
                    channellist.Add(var);
                    string contents = "ChannelName=";
                    foreach (string text in channellist)
                        contents = contents + text + ',';
                    contents = contents.Remove(contents.Length - 1, 1);
                    string FileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\ChannelMaster.ini";
                    try
                    {
                        System.IO.File.WriteAllText(FileName, contents);
                    }
                    catch
                    {
                        this.Close();
                    }
                }
            }
            this.Close();

        }
        public void RemoveChannel(List<string> ChannelNames)
        {
            MessageForm _msg = new MessageForm();

            foreach (string item in ChannelNames)
            {
                channellist.Remove(item);
            }

            string contents = "ChannelName=";
            foreach (string text in channellist)
                contents = contents + text + ',';
            contents = contents.Remove(contents.Length - 1, 1);
            string FileName = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\ChannelMaster.ini";
            try
            {
                System.IO.File.WriteAllText(FileName, contents);

                foreach (string item in ChannelNames)
                {
                    string dirPath = $"{Application.StartupPath}\\Channels\\{item}";

                    if (Directory.Exists(dirPath))
                    {
                        Directory.Delete(dirPath, true);
                    }
                }

                
                string text = "Selected Channels Deleted Successfully";

                _msg.ShowMessageBox(Application.ProductName, text, "OK", "Exclamation");
            }
            catch
            {
                string text = "Unable to delete the selected channels!";

                _msg.ShowMessageBox(Application.ProductName, text, "OK", "Exclamation");
                this.Close();
            }
        }

        private void CreateChannel_Load(object sender, EventArgs e)
        {

        }
    }
}
