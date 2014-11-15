using System;
using System.Net;
using System.Windows.Forms;
using SmsSender.Properties;
using Twilio;

namespace SmsSender
{
    public partial class SmsSenderForm : Form
    {
        public SmsSenderForm()
        {
            InitializeComponent();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                string accountSid = Settings.Default.Sid;
                string authToken = Settings.Default.Token;
                string numberFrom = Settings.Default.Number;
                string proxyHost = Settings.Default.ProxyHost;
                int proxyPort = Settings.Default.ProxyPort;
                string numbersString = tbNumbers.Text;
                numbersString = numbersString
                    .Replace(" ", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty);
                var numbersForSending = numbersString.Split('.', ',', ';', ':');
                string messageText = tbMessage.Text;
                var twilio = new TwilioRestClient(accountSid, authToken);
                if(!string.IsNullOrEmpty(proxyHost))
                    twilio.Proxy = new WebProxy(proxyHost, proxyPort);
                foreach (var n in numbersForSending)
                {
                    string numberTo = n;
                    if (!string.IsNullOrEmpty(numberTo))
                    {
                        if (numberTo.StartsWith("8"))
                        {
                            numberTo = numberTo.Remove(0, 1);
                            numberTo = numberTo.Insert(0, "+7");
                        }
                        var message = twilio.SendMessage(numberFrom, numberTo, messageText);
                        if (message.RestException != null)
                        {
                            var error = message.RestException.Message;
                            MessageBox.Show(string.Format("Произошла ошибка \r\n{0}", error), "Ошибка");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Произошла ошибка \r\n{0}", ex.Message), "Ошибка");
            }
        }

        private void lbSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            (new SettingsForm()).ShowDialog();
        }
    }
}
