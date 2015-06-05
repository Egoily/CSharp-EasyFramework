using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PostSharp.Patterns.Diagnostics;

namespace WinForm
{
    public partial class Form1 : Form
    {

        string[] strings = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        //string[] strings = new string[] { "1"};
        public Form1()
        {
            InitializeComponent();

        }

        private string[] FindData(string currentText)
        {
            List<string> all = new List<string>();
            foreach (var item in strings)
            {
                all.Add(item);
            }
            if (this.comboBox1.Text != currentText)
            {
                all.Remove(this.comboBox1.Text);
            }
            if (this.comboBox2.Text != currentText)
            {
                all.Remove(this.comboBox2.Text);
            }
            if (this.comboBox3.Text != currentText)
            {
                all.Remove(this.comboBox3.Text);
            }


            return all.ToArray();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            int? i = null;

            int? j = i ?? 0;



            //SendEmail();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            string curentText=(sender as ComboBox).Text;
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).Items.AddRange(FindData(curentText));
        }

        private string _emailReceiverAddress = "76416712@qq.com";
        //private string _emailReceiverAddress = "egoily@hotmail.com";
        //private string _emailReceiverAddress = "ego.huang@elephanttalk.com";

        private string _emailSender_smptHost = "mail.elephanttalk.com";
        //private string _emailSender_smptHost = "smtp.qq.com";

        private string _emailSender_userAddress = "pcrf-alerts@elephanttalk.com";
        private string _emailSender_userName = "pcrf-alerts";
        private string _emailSender_userPassword = "Chang3N0w!";

        //private string _emailSender_userAddress = "ego.huang@elephanttalk.com";
        //private string _emailSender_userName = "ego.huang";
        //private string _emailSender_userPassword = "";



        private string _emailSubject="test";
        [PostSharp.Patterns.Diagnostics.LogAttribute()]
        private void SendEmail()
        {
            string body = string.Format("This a test email from {0}/{1}.", _emailSender_userName, _emailSender_userAddress);
            ETalkMailMessage mailMessage = new ETalkMailMessage(_emailSender_userAddress, _emailReceiverAddress, _emailSubject, body);

            bool sendEmailResult = SendEmail(mailMessage, _emailSender_smptHost, _emailSender_userName, _emailSender_userPassword);

        }

        private bool SendEmail(ETalkMailMessage mail, string smptHost, string userName, string userPassword)
        {
            try
            {
                SendEmail sender = new SendEmail();
                return sender.Send(mail, smptHost, userName, userPassword);
            }
            catch (Exception ex)
            {           
                return false;
            }
        }



    }
}
