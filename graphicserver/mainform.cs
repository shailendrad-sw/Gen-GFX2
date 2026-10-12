using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
//using DongleProtectionLib;

namespace graphicserver
{
    public partial class mainform : Form
    {
        List<FrmMainServer> _objMainserver = null;
        List<Panel> TabPanel;
        List<Label> TabPanelLabel;
        private int ChannelNumber = 0;
        private Panel AddTabPanel;
        private int count;
        List<Panel> TabChannelPanel;
        List<Label> TabChannelPanelLabel;
        Point StartingPoint = new Point(0, 0);
        private static Mutex mutex = null;
        public Loging objlog = null;
        public bool AutoRestart = false;
        //public string userChannelType_MF = null;
        public mainform()
        {
            InitializeComponent();
            int bResult;
            objlog = new Loging();
         //   if (!Appinstancepermitble())
          //      System.Environment.Exit(0);
            //StartDongletProtection objProtection = new StartDongletProtection();
            //bResult = objProtection.StartProtection();
            //if (bResult == 0)
            //    System.Environment.Exit(0);

        }
        private bool Appinstancepermitble()
        {
            Process[] processes = Process.GetProcesses();
            int count = 0;
            // Loop through each process
            foreach (var process in processes)
            {
                // Check if the process name starts with "GFX2."
                if (process.ProcessName.StartsWith("GFX2.", StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            if ((count > 0) && (count < 3))
                return true;
            MessageBox.Show("Exceeding pemissible max instances");
            return false;
        }
        private void mainform_Load(object sender, EventArgs e)
        {
            try
            {
                StaticGlobal.ChannelConfig();
                if (File.Exists(Application.StartupPath + "//RunningChannel.ini"))
                {
                    StaticGlobal.ReadRunningChannelConfig();
                    if (StaticGlobal.Runningchannels == null)
                    {
                        SelectChannels _Select = new SelectChannels();
                        _Select.ShowDialog();
                        if (SelectChannels.SelectedChannelsList.Count == 0)
                        {
                            Environment.Exit(0);
                        }
                        count = SelectChannels.SelectedChannelsList.Count;
                    }
                }
                else
                {
                    SelectChannels _Select = new SelectChannels();
                    _Select.ShowDialog();
                    if (SelectChannels.SelectedChannelsList.Count == 0)
                    {
                        Environment.Exit(0);
                    }
                    count = SelectChannels.SelectedChannelsList.Count;
                }
                TabChannelPanel = new List<Panel>();
                TabChannelPanelLabel = new List<Label>();
                if (StaticGlobal.Runningchannels != null)
                {
                    SelectChannels.SelectedChannelsList = StaticGlobal.Runningchannels.ToList<string>();
                }
                count = SelectChannels.SelectedChannelsList.Count;
                //////////////////////////////////////////////MultiInstance running prevention start/////////////////////////
                if (count != 1)
                    System.Environment.Exit(0);
                else
                {
                    bool createdNew = false;

                    mutex = new Mutex(true, SelectChannels.SelectedChannelsList[0].ToString(), out createdNew);

                    if (!createdNew)
                    {
                        //app is already running! Exiting the application
                       objlog.WriteEvents("MainFormLoad", "Same instance already running,exiting...", "");
                        System.Environment.Exit(0);
                    }
                }
                /////////////////////////////////MultiInstance running prevention end////////////////////////////////////////////////
                TabPanel = new List<Panel>();
                TabPanelLabel = new List<Label>();
                Point TabPanelLocation = new Point(0, 0);
                _objMainserver = new List<FrmMainServer>();
                for (ChannelNumber = 0; ChannelNumber < SelectChannels.SelectedChannelsList.Count; ChannelNumber++)
                {
                   
                    TabPanelLocation = ChannelTabCreation(TabPanel, TabPanelLocation);
                    mainpanel.Controls.Add(_objMainserver[ChannelNumber]);
                    _objMainserver[ChannelNumber].Show();
                    TabPanelLabel[ChannelNumber].BackColor = Color.FromArgb(42, 42, 42);
                    TabPanelLabel[ChannelNumber].ForeColor = Color.Azure;
                    mainpanel.Controls.RemoveAt(mainpanel.Controls.Count - 1);
                    this.Text = $"{ this.Text}-{ _objMainserver[ChannelNumber].Text}-{_objMainserver[ChannelNumber].ChannelType}";
                    Thread.Sleep(500);
                    
                }
                mainpanel.Controls.Add(_objMainserver[0]);
                TabPanelLabel[0].BackColor = Color.Azure;
                TabPanelLabel[0].ForeColor = Color.FromArgb(42, 42, 42);
                _objMainserver[0].Show();
                AddTabButtonCreate(TabPanelLocation);
            }
            catch (Exception ex)
            {
                _objMainserver[ChannelNumber].objlog.WriteError("MainFormLoad", "Error Object not created " + ex.ToString(), "");
            }
        }
        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (AutoRestart) // Added the below block to when user aggreed to restart the application after adding channels
                {
                    for (int i = 0; i < SelectChannels.SelectedChannelsList.Count; i++)
                    {
                        if (_objMainserver[i] != null)
                        {
                            _objMainserver[i].WriteSecondaryFile();
                            if (_objMainserver[i].GetSink() != null)
                                _objMainserver[i].GetSink().SinkClose();
                        }
                        try
                        {
                            _objMainserver[i].DisposedObjects();
                        }
                        catch { }
                        AutoRestart = false;
                        e.Cancel = true;

                    }
                }
                else
                {
                    if (!_objMainserver[0].ClosedbyDongle)
                    {
                        DialogResult result = _objMainserver[0].CustomMessageBox.ShowMessageBox(Application.ProductName, "Do you want to close this application", "Yes~No", "Warning");
                        if (result == DialogResult.Yes)
                        {
                            for (int i = 0; i < SelectChannels.SelectedChannelsList.Count; i++)
                            {
                                if (_objMainserver[i] != null)
                                {
                                    _objMainserver[i].WriteSecondaryFile();
                                    if (_objMainserver[i].GetSink() != null)
                                        _objMainserver[i].GetSink().SinkClose();
                                }
                                try
                                {
                                    _objMainserver[i].DisposedObjects();
                                }
                                catch { }

                            }
                            Environment.Exit(0);
                        }
                        e.Cancel = true;
                    }
                    else
                    {
                        for (int i = 0; i < SelectChannels.SelectedChannelsList.Count; i++)
                        {
                            if (_objMainserver[i] != null)
                            {
                                _objMainserver[i].WriteSecondaryFile();
                                if (_objMainserver[i].GetSink() != null)
                                    _objMainserver[i].GetSink().SinkClose();
                            }
                            try
                            {
                                _objMainserver[i].DisposedObjects();
                            }
                            catch { }

                        }
                        Environment.Exit(0);
                    }
                }
            }
            catch
            { }
        }
        private Point ChannelTabCreation(List<Panel> TabPanel, Point TabPanelLocation)
        {
            try
            {
                AddTabPanelOnMainPanel(TabPanel, TabPanelLocation);
                LabelOnTabPanel(TabPanel);
                _objMainserver.Add(new FrmMainServer(SelectChannels.SelectedChannelsList[ChannelNumber]));
                _objMainserver[ChannelNumber].TopLevel = false;
                _objMainserver[ChannelNumber].Text = SelectChannels.SelectedChannelsList[ChannelNumber];
                _objMainserver[ChannelNumber].Dock = DockStyle.Fill;
                TabPanelLabel[ChannelNumber].BackColor = Color.FromArgb(42, 42, 42);
                TabPanelLabel[ChannelNumber].Click += new System.EventHandler(ShowForm);
              //  TabPanelLabel[ChannelNumber].DoubleClick += new System.EventHandler(RemoveTab);
                TabPanelLocation.X += 105;
                return TabPanelLocation;
            }
            catch (Exception ex)
            {
                _objMainserver[0].objlog.WriteError("ChannelTabCreation", "Error Object not created " + ex.ToString(), "");
                return TabPanelLocation;
            }
        }
        private void AddTabPanelOnMainPanel(List<Panel> TabPanel, Point TabPanelLocation)
        {
            TabPanel.Add(new Panel());
            TabPanel[ChannelNumber].Name = SelectChannels.SelectedChannelsList[ChannelNumber];
            TabPanel[ChannelNumber].Location = TabPanelLocation;
            TabPanel[ChannelNumber].Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
            TabPanel[ChannelNumber].Size = new System.Drawing.Size(100, 35);
            HeadTabPanel.Controls.Add(TabPanel[ChannelNumber]);
            TabPanel[ChannelNumber].BringToFront();
        }
        private void LabelOnTabPanel(List<Panel> TabPanel)
        {
            TabPanelLabel.Add(new Label());
            TabPanelLabel[ChannelNumber].Font = new Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TabPanelLabel[ChannelNumber].Name = SelectChannels.SelectedChannelsList[ChannelNumber];
            TabPanelLabel[ChannelNumber].Dock = DockStyle.Fill;
            TabPanelLabel[ChannelNumber].Text = SelectChannels.SelectedChannelsList[ChannelNumber];
            TabPanelLabel[ChannelNumber].TextAlign = ContentAlignment.MiddleCenter;
            TabPanelLabel[ChannelNumber].Location = new Point(0, 0);
            TabPanelLabel[ChannelNumber].Margin = new Padding(0);
            TabPanelLabel[ChannelNumber].Size = new Size(100, 35);
            TabPanel[ChannelNumber].Controls.Add(TabPanelLabel[ChannelNumber]);
        }
        private void ShowForm(object sender, EventArgs e)
        {
            int index = _objMainserver.FindIndex(s => s.Text == ((Label)sender).Text);
            try
            {
                ChangeTabColor(sender);
                mainpanel.Controls.RemoveAt(mainpanel.Controls.Count - 1);
                mainpanel.Controls.Add(_objMainserver[index]);
                _objMainserver[index].Show();
            }
            catch (Exception ex)
            {
                _objMainserver[index].objlog.WriteError("ShowForm", "Error in Displaying Form " + ex.ToString(), _objMainserver[index].ChannelName);
            }
        }
        private void ShowForm(object sender)
        {
            int index = _objMainserver.FindIndex(s => s.Text == ((Label)sender).Text);
            try
            {
                ChangeTabColor(sender);
                mainpanel.Controls.RemoveAt(mainpanel.Controls.Count - 1);
                mainpanel.Controls.Add(_objMainserver[index]);
                _objMainserver[index].Show();
            }
            catch (Exception ex)
            {
                _objMainserver[index].objlog.WriteError("ShowForm", "Error in Displaying Form " + ex.ToString(), _objMainserver[index].ChannelName);
            }
        }
        private void ChangeTabColor(object Sender)
        {
            try
            {
                for (int i = 0; i < SelectChannels.SelectedChannelsList.Count; i++)
                {
                    if (((Label)Sender).Name == SelectChannels.SelectedChannelsList[i])
                    {
                        ((Label)Sender).BackColor = Color.Azure;
                        ((Label)Sender).ForeColor = Color.FromArgb(42, 42, 42);
                    }
                    else
                    {
                        if (TabPanelLabel[i] != null)
                        {
                            TabPanelLabel[i].BackColor = Color.FromArgb(42, 42, 42);
                            TabPanelLabel[i].ForeColor = Color.Azure;
                        }
                    }
                }
            }
            catch
            { }
        }
        private void RemoveTab(object sender, EventArgs e)
        {
            int index = _objMainserver.FindIndex(s => string.Compare(s.Text, ((Label)sender).Text, true) == 0);
            try
            {
                if (HeadTabPanel.Controls.Count == 2)
                {
                    _objMainserver[index].CustomMessageBox.ShowMessageBox(Application.ProductName, "Last Channel can not be closed", "OK", "Exclamation");
                    return;
                }
                DialogResult result = _objMainserver[index].CustomMessageBox.ShowMessageBox(Application.ProductName, "Do you want to close this Channel", "Yes~No", "Warning");
                if (result == DialogResult.Yes)
                {
                    _objMainserver[index].WriteSecondaryFile();
                    Point Newlocation = new Point(1000000, 1000000);//Fixed Location

                    for (int TabPanelNumber = 0; TabPanelNumber < HeadTabPanel.Controls.Count; TabPanelNumber++)
                    {
                        if (HeadTabPanel.Controls[TabPanelNumber].Name == ((Label)sender).Text)
                        {
                            for (int AffectedTabPanelNumber = TabPanelNumber - 1; AffectedTabPanelNumber >= 0; AffectedTabPanelNumber--)
                            {
                                if (Newlocation.X == 1000000)
                                {
                                    Newlocation = ChangeTabDimensions(AffectedTabPanelNumber, HeadTabPanel.Controls[TabPanelNumber].Location);
                                }
                                else
                                {
                                    Newlocation = ChangeTabDimensions(AffectedTabPanelNumber, Newlocation);
                                }
                            }
                            HeadTabPanel.Controls.RemoveAt(TabPanelNumber);
                            TabPanel[index].Controls.RemoveAt(0);
                            TabPanel.RemoveAt(index);
                            TabPanelLabel.RemoveAt(index);
                        }
                    }
                    mainpanel.Controls.RemoveAt(mainpanel.Controls.Count - 1);//This is Fixed
                    SelectChannels.SelectedChannelsList.RemoveAt(index);
                    SelectChannels.WriteRunningChannelIni(SelectChannels.SelectedChannelsList);
                    _objMainserver[index].Close();
                    _objMainserver[index].DisposedObjects();
                    _objMainserver[index] = null;
                    _objMainserver.RemoveAt(index);
                    SelectedTab(index);
                }
            }
            catch (Exception ex)
            {
                _objMainserver[index].objlog.WriteError("RemoveTab", "Error in removing tab" + ex.ToString(), _objMainserver[index].ChannelName);
            }
        }
        private Point ChangeTabDimensions(int value, Point Newlocation)
        {
            try
            {
                //When Channel is Deleted
                HeadTabPanel.Controls[value].Location = Newlocation;
                Newlocation.X += 105;
                return Newlocation;
            }
            catch (Exception ex)
            {
                _objMainserver[0].objlog.WriteError("ChangeTabDimensions", "Error in removing Channel Tab " + ex.ToString(), "");
                return Newlocation;
            }
        }
        private void SelectedTab(int index)
        {
            try
            {
                if (TabPanelLabel.Count == index)
                {
                    TabPanelLabel[index - 1].ForeColor = Color.FromArgb(42, 42, 42);
                    TabPanelLabel[index - 1].BackColor = Color.Azure;
                    mainpanel.Controls.Add(_objMainserver[index - 1]);
                    _objMainserver[index - 1].Show();
                }
                else if (TabPanelLabel.Count < index)
                {
                    TabPanelLabel[0].ForeColor = Color.FromArgb(42, 42, 42);
                    TabPanelLabel[0].BackColor = Color.Azure;
                    mainpanel.Controls.Add(_objMainserver[0]);
                    _objMainserver[0].Show();
                }
                else if (TabPanelLabel.Count > index)
                {
                    TabPanelLabel[index].ForeColor = Color.FromArgb(42, 42, 42);
                    TabPanelLabel[index].BackColor = Color.Azure;
                    mainpanel.Controls.Add(_objMainserver[index]);
                    _objMainserver[index].Show();
                }
            }
            catch (Exception)
            { }
        }
        private void AddTabButtonCreate(Point TabPanelLocation)
        {
            try
            {
                #region Adding TabPanel 
                AddTabPanel = new Panel();
                AddTabPanel.Name = "AddButton";
                AddTabPanel.Location = TabPanelLocation;
                AddTabPanel.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left)));
                AddTabPanel.Size = new System.Drawing.Size(35, 35);
                HeadTabPanel.Controls.Add(AddTabPanel);
                AddTabPanel.BringToFront();
                #endregion
                #region Adding Label in TabPanel
                Label AddTabPanelLabel = new Label();
                AddTabPanelLabel.Font = new Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AddTabPanelLabel.Name = "AddButtonLabel";
                AddTabPanelLabel.AutoSize = false;
                AddTabPanelLabel.Dock = DockStyle.Fill;
                AddTabPanelLabel.Text = "+";
                AddTabPanelLabel.Margin = new Padding(0);
                AddTabPanelLabel.Padding = Padding.Empty;
                AddTabPanelLabel.TextAlign = ContentAlignment.MiddleCenter;
                AddTabPanelLabel.ForeColor = Color.Azure;
                AddTabPanelLabel.BackColor = Color.FromArgb(42, 42, 42);
                AddTabPanel.Controls.Add(AddTabPanelLabel);
                AddTabPanelLabel.Click += CreateChannels;
                AddTabPanelLabel.MouseEnter += AddButtonColorChange;
                AddTabPanelLabel.MouseLeave += AddButtonNormalColor;
                #endregion
            }
            catch (Exception)
            { }
        }
        private void AddButtonColorChange(object sender, EventArgs e)
        {
            try
            {
                ((Label)sender).BackColor = Color.Azure;
                ((Label)sender).ForeColor = Color.FromArgb(42, 42, 42);
            }
            catch (Exception)
            { }
        }
        private void AddButtonNormalColor(object sender, EventArgs e)
        {
            try
            {
                ((Label)sender).ForeColor = Color.Azure;
                ((Label)sender).BackColor = Color.FromArgb(42, 42, 42);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Resolved the issue in Adding a New Channel and Removing the previous channel automatically. (13/01/2025)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateChannels(object sender, EventArgs e)
        {
            try
            {
                SelectChannels _Select = new SelectChannels();
                _Select.ShowDialog();
                if (_Select.saveChanges)
                {
                    if (count != SelectChannels.SelectedChannelsList.Count)
                    {
                        count = SelectChannels.SelectedChannelsList.Count;
                    }
                    Point ModifiedLocation = new Point(0, 0);
                    for (int i = TabPanelLabel.Count - 1; i >= 0; i--)
                    {
                        if (TabPanelLabel[i] != null)
                        {
                            if (ModifiedLocation.X < TabPanel[i].Location.X)
                            {
                                ModifiedLocation = TabPanel[i].Location;
                            }
                        }
                    }
                    ModifiedLocation.X += 105;
                    for (int i = 0; i <= SelectChannels.SelectedChannelsList.Count - 1; i++)
                    {
                        if (TabPanelLabel.Count == i && SelectChannels.SelectedChannelsList[i] != null)
                        {
                            ChannelNumber = i;
                            ModifiedLocation = ChannelTabCreation(TabPanel, ModifiedLocation);
                            ShowForm(TabPanelLabel[TabPanelLabel.Count - 1]);
                        }
                    }
                    AddTabPanel.Controls.RemoveAt(0);
                    HeadTabPanel.Controls.Remove(AddTabPanel);
                    AddTabButtonCreate(ModifiedLocation);

                    //Added the below block to give information of thge reloading the application and removing the previous channel
                    DialogResult result = _objMainserver[0].CustomMessageBox.ShowMessageBox(Application.ProductName, "Please Restart the Appplication!", "Yes~No", "Warning");
                    if (result == DialogResult.Yes)
                    {
                        AutoRestart = true;

                        RemoveFirstTab(sender); 

                        Environment.Exit(0);

                    }
                    else
                    {
                        _objMainserver[0].CustomMessageBox.ShowMessageBox(Application.ProductName, "Error in channel configuration, \n You need to restart the application!", "OK", "Exclamation");

                        SelectChannels.SelectedChannelsList.RemoveAt(0);
                        SelectChannels.WriteRunningChannelIni(SelectChannels.SelectedChannelsList);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                _objMainserver[0].objlog.WriteError("CreateChannels", "Error in creating new channels " + ex.ToString(), "");
            }
        }


        /// <summary>
        /// Resolved the issue in Adding a New Channel and Removing the previous channel automatically. (13/01/2025)
        /// Method to remove the previous channel automatically when user clicks yes for restarting the application
        /// </summary>
        /// <param name="sender"></param>
        private void RemoveFirstTab(object sender)
        {
            int index = 0;
            try
            {
                _objMainserver[index].WriteSecondaryFile();
                Point Newlocation = new Point(1000000, 1000000);//Fixed Location

                HeadTabPanel.Controls.RemoveAt(0);
                TabPanel[index].Controls.RemoveAt(0);
                TabPanel.RemoveAt(index);
                TabPanelLabel.RemoveAt(index);
                mainpanel.Controls.RemoveAt(mainpanel.Controls.Count - 1);//This is Fixed
                SelectChannels.SelectedChannelsList.RemoveAt(index);
                SelectChannels.WriteRunningChannelIni(SelectChannels.SelectedChannelsList);
                _objMainserver[index].Close();
                _objMainserver[index].DisposedObjects();
                _objMainserver[index] = null;
                _objMainserver.RemoveAt(index);
                SelectedTab(index);
            }
            catch (Exception ex)
            {
                _objMainserver[index].objlog.WriteError("RemoveTab", "Error in removing tab" + ex.ToString(), _objMainserver[index].ChannelName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //end of class
    }
}
