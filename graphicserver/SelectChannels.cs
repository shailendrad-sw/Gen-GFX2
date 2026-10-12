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
    public partial class SelectChannels : Form
    {
        public static string[] channelArr;
        public bool saveChanges = false;
        public SelectChannels()
        {
            InitializeComponent();
        }
        List<Panel> TabChannelPanel;
        List<Label> TabChannelPanelLabel;
        Point StartingPoint = new Point(0, 0);
        public int ChannelNumber = -1;
        internal static List<string> SelectedChannelsList = new List<string>();
        private void SelectChannels_Load(object sender, EventArgs e)
        {
            try
            {
                StaticGlobal.ChannelConfig();
                int TotalAvailableChannels = StaticGlobal.channels.Length;
                TabChannelPanel = new List<Panel>();
                TabChannelPanelLabel = new List<Label>();
                for (ChannelNumber = 0; ChannelNumber < TotalAvailableChannels; ChannelNumber++)
                {
                    CreatingChannelPanel();
                    CreatingChannelLabel();
                }
                channelArr = StaticGlobal.channels;
                CurrentRunningChannels();
            }
            catch (Exception)
            { }
        }
        private void CreatingChannelPanel()
        {
            if (StartingPoint.X > this.Size.Width - 110)
            {
                StartingPoint.X = 0;
                StartingPoint.Y += 35;
                if (StartingPoint.Y >= TabsPanel.Height - 35)
                {
                    Size NewFormSize = new Size(this.Size.Width, this.Size.Height + 50);
                    this.MaximumSize = NewFormSize;
                    this.Size = NewFormSize;
                }
            }
            TabChannelPanel.Add(new Panel());
            TabChannelPanel[ChannelNumber].Location = StartingPoint;
            TabChannelPanel[ChannelNumber].Size = new Size(105, 30);
            TabChannelPanel[ChannelNumber].BackColor = Color.FromArgb(42, 42, 42);
            TabsPanel.Controls.Add(TabChannelPanel[ChannelNumber]);
        }
        private void CreatingChannelLabel()
        {
            TabChannelPanelLabel.Add(new Label());
            TabChannelPanelLabel[ChannelNumber].Dock = DockStyle.Fill;
            TabChannelPanelLabel[ChannelNumber] = new Label();
            TabChannelPanelLabel[ChannelNumber].Location = new Point(0, 0);
            TabChannelPanelLabel[ChannelNumber].Dock = DockStyle.Fill;
            TabChannelPanelLabel[ChannelNumber].Text = StaticGlobal.channels[ChannelNumber];
            TabChannelPanelLabel[ChannelNumber].Font = new Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TabChannelPanelLabel[ChannelNumber].TextAlign = ContentAlignment.MiddleCenter;
            TabChannelPanelLabel[ChannelNumber].ForeColor = Color.White;
            TabChannelPanel[ChannelNumber].Controls.Add(TabChannelPanelLabel[ChannelNumber]);
            TabChannelPanel[ChannelNumber].BringToFront();
            TabChannelPanelLabel[ChannelNumber].Click += SelectedChannels;
            TabChannelPanelLabel[ChannelNumber].DoubleClick += UnSelectedChannels;
            StartingPoint.X += 110;
        }
        private void CurrentRunningChannels()
        {
            if (SelectedChannelsList.Count > 0)
            {
                for (int i = 0; i < TabChannelPanelLabel.Count; i++)
                {
                    for (int j = 0; j < SelectedChannelsList.Count; j++)
                    {
                        if (TabChannelPanelLabel[i].Text.ToUpper() == SelectedChannelsList[j].ToUpper())
                        {
                            TabChannelPanelLabel[i].BackColor = Color.DarkGreen;
                            TabChannelPanelLabel[i].ForeColor = Color.Azure;
                        }
                        else if (TabChannelPanelLabel[i].BackColor != Color.DarkGreen)
                        {
                            TabChannelPanelLabel[i].BackColor = Color.FromArgb(42, 42, 42);
                            TabChannelPanelLabel[i].ForeColor = Color.Azure;
                        }
                    }
                }
            }
        }
        private void UnSelectedChannels(object sender, EventArgs e)
        {
            try
            {
                if (((Label)sender).BackColor == Color.DarkGreen) { return; }
                ((Label)sender).BackColor = Color.FromArgb(42, 42, 42);
                ((Label)sender).ForeColor = Color.White;
            }
            catch (Exception)
            { }
        }
        private void SelectedChannels(object sender, EventArgs e)
        {
            try
            {
                if (((Label)sender).BackColor == Color.DarkGreen) { return; }
                ((Label)sender).BackColor = Color.DarkOrange;
                ((Label)sender).ForeColor = Color.Azure;
            }
            catch (Exception)
            { }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool selectedChannel = false;
                int count = 0;
                for (int i = 0; i < TabChannelPanel.Count; i++)
                {
                    if (TabChannelPanelLabel[i].BackColor == Color.DarkOrange)
                    {
                        if (!SelectedChannelsList.Contains(TabChannelPanelLabel[i].Text))
                        {                            
                            SelectedChannelsList.Add(TabChannelPanelLabel[i].Text);
                            WriteRunningChannelIni(SelectedChannelsList);
                            selectedChannel = true;
                            saveChanges = true;
                            count++;
                        }
                        
                    }
                }
                if(count == 0)
                {
                    MessageForm _msg = new MessageForm();
                    _msg.ShowMessageBox(Application.ProductName, "Channel is already running", "OK", "Exclamation");
                    return;
                }
                if (SelectedChannelsList.Count == 0)
                {
                    MessageForm _msg = new MessageForm();
                    _msg.ShowMessageBox(Application.ProductName, "Select atleast One Channel", "OK", "Exclamation");
                    return;
                }
                if (!selectedChannel) //Added the below block to check if no channel is selected
                {
                    MessageForm _msg = new MessageForm();
                    _msg.ShowMessageBox(Application.ProductName, "Select atleast One Channel", "OK", "Exclamation");
                    return;
                }
                this.Close();
            }
            catch (Exception)
            { }
        }
        public static void WriteRunningChannelIni(List<string> RunningChannelsList)
        {
            try {
                FileStream fs = new FileStream($"{Application.StartupPath}\\RunningChannel.ini", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sr = new StreamWriter(fs);
                using (sr)
                {

                    sr.Write("ChannelName=");
                    for (int i = 0; i < RunningChannelsList.Count; i++)
                    {
                        if (i == 0)
                        {
                            sr.Write(RunningChannelsList[i]);
                        }
                        else
                        {
                            sr.Write("," + RunningChannelsList[i]);
                        }
                    }
                   
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            saveChanges = false;
            this.Close();
        }

        private void Btn_Create_Click(object sender, EventArgs e)
        {

            Point FrmLoc = new Point(this.Left + 20, this.Top + 20);
            StaticGlobal.ChannelConfig();
            channelArr = StaticGlobal.channels;
            CreateChannel obj = new CreateChannel(channelArr);
            obj.StartPosition = FormStartPosition.Manual;
            obj.Location = FrmLoc;
            obj.ShowDialog();
            CreateDynamicTab();
            return;
        }

        public void CreateDynamicTab()
        {
            try
            {
                StaticGlobal.ChannelConfig();
                int TotalAvailableChannels = StaticGlobal.channels.Length;
                TabChannelPanel = new List<Panel>();
                TabChannelPanelLabel = new List<Label>();
                TabsPanel.Controls.Clear();

                StartingPoint.X = 0;
                StartingPoint.Y = 0;
                for (ChannelNumber = 0; ChannelNumber < TotalAvailableChannels; ChannelNumber++)
                {
                    CreatingChannelPanel();
                    CreatingChannelLabel();
                }
                CurrentRunningChannels();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Added Delete functionality for channels from ChannelMaster along with respective directory. (13/01/2025)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            MessageForm _msg = new MessageForm();
            try
            {
                List<string> channelList = new List<string>();
                bool selectedChannelToRemove = false;
                for (int i = 0; i < TabChannelPanel.Count; i++)
                {
                    if (TabChannelPanelLabel[i].BackColor == Color.DarkOrange)
                    {
                        channelList.Add(TabChannelPanelLabel[i].Text);

                        selectedChannelToRemove = true;
                    }
                }

                if (channelList.Count == 0)
                {
                    _msg.ShowMessageBox(Application.ProductName, "Running Channel Can Not be Deleted,Create New first", "OK", "Exclamation");
                    return;
                }

                DialogResult result = _msg.ShowMessageBox(Application.ProductName, "Are you sure? You want to delete the selected channels", "Yes~No", "Warning"); 
                if(result == DialogResult.Yes)
                {
                    
                    if (SelectedChannelsList.Count == 0)
                    {
                        _msg.ShowMessageBox(Application.ProductName, "Select atleast One Channel", "OK", "Exclamation");
                        return;
                    }
                    if (!selectedChannelToRemove)
                    {

                        _msg.ShowMessageBox(Application.ProductName, "Select atleast One Channel to Delete", "OK", "Exclamation");
                        return;
                    }

                    channelArr = StaticGlobal.channels;
                    CreateChannel obj = new CreateChannel(channelArr);

                    obj.RemoveChannel(channelList);


                    this.Close();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        //// Endof class//////

    }
}
