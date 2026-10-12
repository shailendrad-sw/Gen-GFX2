using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicserver
{
    public partial class MessageForm : Form
    {
        FrmMainServer objmainserver;
        public MessageForm()
        {
            InitializeComponent();
        }
        public MessageForm(FrmMainServer obj)//Currently No Use
        {
            InitializeComponent();
            objmainserver = obj;
        }
        /// <summary>
        /// Custom Message Box
        /// </summary>
        /// <param name="HeaderText">Application Name</param>
        /// <param name="Maintext">Message which will be shown</param>
        /// <param name="ButtonsToShow">Yes~No or OK</param>
        /// <param name="IconName">Exclamation or Warning</param>
        /// <returns>Dialog Result</returns>
        internal DialogResult ShowMessageBox(string HeaderText, string Maintext, string ButtonsToShow, string IconName)
        {
            try
            {
                HeadingLabel.Text = HeaderText;
                DisplayMessagelbl.Text = Maintext;
                string[] arrbuttons = ButtonsToShow.Split('~');

                IconPictureBox.Image = Image.FromFile(Application.StartupPath + "\\Images\\CustomMessageBox\\" + IconName + ".png");
                if (arrbuttons.Length == 1)
                {
                    YesButton.Visible = false;
                    Button2OKNo.Text = "OK";
                }
                else
                {
                    YesButton.Visible = true;
                    Button2OKNo.Text = "No";
                }
                this.ShowDialog();
                return DialogResult;
            }
            catch (Exception)
            { return DialogResult; }
        }
        private void YesButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Yes;
            }
            catch (Exception)
            { }
        }
        private void Button2OKNo_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.No;
            }
            catch (Exception)
            { }
        }

        private void HeadingLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
