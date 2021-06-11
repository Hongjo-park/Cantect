using System;
using System.Drawing;
using System.Windows.Forms;
using WindowProject;

namespace Imgclient
{
    /// <summary>
    /// imgClient Form
    /// </summary>
    public partial class Client : Form
    {
        /// <summary>
        /// imgClient
        /// </summary>
        public Client()
        {
            InitializeComponent();
        }

        string ip;
        ImageClient ic;

        private void button1_Click(object sender, EventArgs e)
        {
           
           ip = textBox1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(ic == null)
            {
                return;
            }
            // 화면 전체 캡쳐
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gr = Graphics.FromImage(bitmap);
            Size size = new Size(10, 10);
            gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 
                           0, 0, 
                           bitmap.Size, 
                           CopyPixelOperation.SourceCopy);

            ic.Connect(ip, 10200); // 연결
            ic.SendImage(bitmap); // 이미지 전송 
            ic.Close(); // 종료
            pictureBox1.Image = bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ic = new ImageClient();          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (ip == null) return;
            SendEventClient sec = new SendEventClient(ip, 10300);
            sec.SendMouseUP(e.Button);
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ip == null) return;
            SendEventClient sec = new SendEventClient(ip, 10300);
            sec.SendMouseDown(e.Button);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ip == null) return;
            SendEventClient sec = new SendEventClient(ip, 10300);
            sec.SendKeyDown(e.KeyValue);
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (ip == null) return;
            SendEventClient sec = new SendEventClient(ip, 10300);
            sec.SendKeyUp(e.KeyValue);
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ip == null) return;
            SendEventClient sec = new SendEventClient(ip, 10300);
            sec.SendMouseMove(e.X, e.Y);
        }
    }
}
