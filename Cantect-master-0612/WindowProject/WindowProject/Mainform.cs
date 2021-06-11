using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowProjectLib;

namespace WindowProject
{
    /// <summary>
    /// Mainform cs
    /// </summary>
    public partial class Mainform : Form
    {
        /// <summary>
        /// Mainform
        /// </summary>
        public Mainform()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // form Load and server Start
            SetupServer.RecvRCInfo += SetupServer_RecvedRCInfoEventHandler;
            SetupServer.Start("127.0.0.1", 10200);
        }

        private void SetupServer_RecvedRCInfoEventHandler(object sender, RecvRCInfoEventArgs e)
        {
            
            tbox_controller_ip.Text = e.IPAddressStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = tbox_ip.Text;
            SetupClient.Setup(ip, 10200);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

        }

        private void tbox_ip_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbox_controller_ip_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
