using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WindowProject
{
    /// <summary>
    /// image Client class
    /// </summary>
    public class ImageClient
    {
        Socket sock;

        /// <summary>
        /// Connect method
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connect(string ip,int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Connect(ep);
        }

        /// <summary>
        /// send image method
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public bool SendImage(Image img)
        {
            if(sock == null)
            {
                return false;
            }
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            byte[] data = ms.GetBuffer();
            try
            {
                int trans = 0;
                byte[] lbuf = BitConverter.GetBytes(data.Length);
                sock.Send(lbuf);    // 전송할 이미지의 용량(길이)를 전송

                while(trans < data.Length)
                {
                    trans += sock.Send(data, trans, data.Length - trans, SocketFlags.None);
                }
                sock.Close();
                sock = null;
                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// delegate
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public delegate bool SendImageDele(Image img);
        
        /// <summary>
        /// 비동기 send image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="callback"></param>
        public void SendImageAsync(Image img, AsyncCallback callback)
        {
            SendImageDele dele = SendImage;
            dele.BeginInvoke(img, callback, this);
        }

        /// <summary>
        /// socket close
        /// </summary>
        public void Close()
        {
            if(sock != null)
            {
                sock.Close();
                sock = null;
            }
        }

    }
}
