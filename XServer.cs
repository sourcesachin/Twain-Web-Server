using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace Avs.Server
{
    public partial class XServer : Form
    {
        Server server = new Server();
        public XServer()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            if (server.start(ipAddress, Convert.ToInt32(8005), 100, ""))
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            server.stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
        private void XServer_Load(object sender, EventArgs e)
        {

        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                trayIcon.Visible = true;
                this.Hide();
            }
        }
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            trayIcon.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

      

      
    }
}
