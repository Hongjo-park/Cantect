using System;
using System.Drawing;
using System.Net;

namespace WindowProject
{
    /// <summary>
    /// image delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RecvImageEventHandler(object sender, RecvImageEventArgs e);
    

    /// <summary>
    /// 이미지 이벤트 인자
    /// </summary>
    public class RecvImageEventArgs:EventArgs
    {
        /// <summary>
        /// IP 가져오기
        /// </summary>
        public IPEndPoint IPEndPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// IP주소 가져오기
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return IPEndPoint.Address;
            }
        }

        /// <summary>
        /// IP 문자열
        /// </summary>
        public string IPAddressStr
        {
            get
            {
                return IPAddress.ToString();
            }
        }

        /// <summary>
        /// Port 가져오기
        /// </summary>
        public int Port
        {
            get
            {
                return IPEndPoint.Port;
            }
        }

        /// <summary>
        /// 이미지 가져오기
        /// </summary>
        public Image Image
        {
            get;
            private set;
        }

        /// <summary>
        /// 이미지 SIZE 가져오기
        /// </summary>
        public Size Size
        {
            get
            {
                return Image.Size;
            }
        }

        /// <summary>
        /// 이미지 폭
        /// </summary>
        public int Width
        {
            get
            {
                return Image.Width;
            }
        }

        /// <summary>
        /// 이미지 높이
        /// </summary>
        public int Height
        {
            get
            {
                return Image.Height;
            }
        }
        /// <summary>
        /// IP, image
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="image"></param>
        public RecvImageEventArgs(IPEndPoint ep, Image image)
        {
            IPEndPoint = ep;
            Image = image;
        }

        /// <summary>
        /// Tostring method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("IP:{0} width:{1} Height{2}", IPAddressStr, Width, Height);
        }
    }
}
