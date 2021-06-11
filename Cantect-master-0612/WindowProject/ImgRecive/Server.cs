using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowProject;



namespace ImgRecive
{
    /// <summary>
    /// image recive form
    /// </summary>
    public partial class Server : Form
    {

        
        /// <summary>
        /// imamge server
        /// </summary>
        public Server()
        {
            InitializeComponent();
        }

        ImageServer ims;
        int img = 1;
        RecvEventServer RecvServer;
        private void Form1_Load(object sender, EventArgs e)
        {
            ims = new ImageServer(DefaultIP, 10200); // Test를 위한 hard coding
            ims.RecvedImage += Ims_RecvedImage;
            RecvServer = new RecvEventServer(DefaultIP, 10300);
            RecvServer.RecvedKMEvent += RecvServer_RecvedKMEvent;
        }

        private void RecvServer_RecvedKMEvent(object sender, RecvKMEventArgs e)
        {
            String str = e.MT.ToString();
            switch(e.MT)
            {
                case MsgType.MT_KEYDOWN:
                case MsgType.MT_KEYUP:
                    str += " " + e.Key.ToString(); 
                    break;
                case MsgType.MT_MMOVE:
                    str += " " + e.MP.X.ToString() + "," + e.MP.Y.ToString();
                    break;
            }
            lbox_KM.Items.Add(str);
            lbox_KM.SelectedIndex = lbox_KM.Items.Count - 1;
        }

        private void Ims_RecvedImage(object sender, RecvImageEventArgs e)
        {
            e.Image.Save(string.Format("{0}.bmp", img));
            MessageBox.Show("이미지 수신 완료 !", "알립니다");
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = string.Format("{0}.bmp", img);
        }

        static string DefaultIP
        {
            get { return "127.0.0.1"; }
        }

        private void lbox_KM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
