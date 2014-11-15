using System;
using System.Windows.Forms;
using SmsSender.Properties;

namespace SmsSender
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            tbSid.Text = Settings.Default.Sid;
            tbToken.Text = Settings.Default.Token;
            tbNumber.Text = Settings.Default.Number;
            tbHost.Text = Settings.Default.ProxyHost;
            nudPort.Value = Settings.Default.ProxyPort;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Settings.Default.Sid = tbSid.Text;
            Settings.Default.Token = tbToken.Text;
            Settings.Default.Number = tbNumber.Text;
            Settings.Default.ProxyHost = tbHost.Text;
            Settings.Default.ProxyPort = (int)nudPort.Value;
            Settings.Default.Save();
            Close();
        }
    }
}
