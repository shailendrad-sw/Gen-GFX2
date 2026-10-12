using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace graphicserver
{
    public partial class ScteUserControl : UserControl
    {
        public string ChannelID { get; set; }
        public string ScteIP { get; set; }
        public string SctePort { get; set; }
        public string SctePreRoll { get; set; }
        public Loging objlog = null;
        public ScteUserControl(string _ip,string _Port,string _SctePreRoll,string ChannelID, Loging objlog)
        {
            InitializeComponent();
            this.ScteIP = _ip;
            this.SctePort = _Port;
            this.SctePreRoll = _SctePreRoll;
            this.objlog = objlog;
            this.ChannelID = ChannelID;
            objlog.WriteEvents("ScteUserControl", "Initialize SCTE Control.", ChannelID);
       }

        private void ScteUserControl_Load(object sender, EventArgs e)
        {

        }

        public void UpdateSctePacket(string _Eventid,string _EventTime,string _Duration, string reconkey)
        {
            lblEventId.Text = "xxxxx";
            lblduration.Text = "00:00:00:00 (0)";
            lblEventTime.Text = "00:00:00:00";
            lblprerolltime.Text = "xxxxx";
            lblreconkey.Text = "xxxxx";
            try
            {
                lblEventId.Text = _Eventid;
                lblduration.Text = $"{_Duration } ({GetDurationInSecond(_Duration)})";
                lblEventTime.Text = _EventTime;
                lblprerolltime.Text = SctePreRoll;
                lblreconkey.Text = reconkey;
                string PacketStructure = $"splice start normal {_EventTime.Split(':')[0]} {_EventTime.Split(':')[1]} {_EventTime.Split(':')[2]} {_EventTime.Split(':')[3]} {GetDurationInSecond(_Duration)} event_id {_Eventid} pre_roll {SctePreRoll} ";
                SendUDPPacket(PacketStructure);
            }
            catch (Exception ex)
            {
                objlog.WriteError("UpdateSctePacket", "Error In PreSet SCTE Information ."+ ex.ToString(), ChannelID);
            }

        }


        private string GetDurationInSecond(string duration)
        {
            try
            {
                string[] arr = duration.Split(':');
                TimeSpan tm = new TimeSpan(0, Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[2]), Convert.ToInt32(arr[2]) * 40);
                return tm.TotalSeconds.ToString("00000");
            }
            catch (Exception ex)
            {
                objlog.WriteError("GetDurationInSecond", "Error In Converting Duration In Second Packet" + duration + ex.ToString(), ChannelID);
                return "0";                
            }         

        }


        public void SendUDPPacket(string _Packet)
        {
            UdpClient UdpClient;
            try
            {
                UdpClient = new UdpClient(ScteIP,Convert.ToInt32(SctePort));
                byte[] SendBytes = Encoding.UTF8.GetBytes(_Packet);
                UdpClient.Send(SendBytes, SendBytes.Length);
                objlog.WriteEvents("SendUDPPacket", "Sending Infotmation To Udp "+ _Packet, ChannelID);
            }
            catch(Exception ex)
            {
                objlog.WriteError("SendUDPPacket", "Error In Sending Packet" + _Packet+ ex.ToString(),ChannelID);
            }
        }

    }
}
