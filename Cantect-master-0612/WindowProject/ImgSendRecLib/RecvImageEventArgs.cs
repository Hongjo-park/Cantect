using System;
using System.Drawing;
using System.Net;

namespace WindowProject
{
    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RecvImageEventHandler(object sender, RecvImageEventArgs e);
    /// <summary>
    /// 이미지 수신 이벤트
    /// </summary>
    public class RecvImageEventArgs:EventArgs
    {
        /// <summary>
        /// IP단말(IP주소 + port) 가져오는 함수
        /// </summary>
        public IPEndPoint IPEndPoint
        {
            get;
            private set;
        }
        /// <summary>
        /// IP주소 가져오는 함수
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return IPEndPoint.Address;
            }
        }
        /// <summary>
        /// IP주소를 문자열로 가져오는 함수
        /// </summary>
        public string IPAddressStr
        {
            get
            {
                return IPAddress.ToString();
            }
        }
        /// <summary>
        /// Port번호를 가져오는 함수
        /// </summary>
        public int Port
        {
            get
            {
                return IPEndPoint.Port;
            }
        }
        /// <summary>
        /// 이미지를 가져옴
        /// </summary>
        public Image Image
        {
            get;
            private set;
        }
        /// <summary>
        /// size를 가져옴
        /// </summary>
        public Size Size
        {
            get
            {
                return Image.Size;
            }
        }
        /// <summary>
        /// 이미지 폭을 가져옴
        /// </summary>
        public int Width
        {
            get
            {
                return Image.Width;
            }
        }
        /// <summary>
        /// 이미지 높이를 가져옴
        /// </summary>
        public int Height
        {
            get
            {
                return Image.Height;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="image"></param>
        public RecvImageEventArgs(IPEndPoint ep, Image image)
        {
            IPEndPoint = ep;
            Image = image;
        }
        /// <summary>
        /// </summary>
        /// <returns>IP주소와 폭, 높이를 문자열로 반환</returns>
        public override string ToString()
        {
            return string.Format("IP:{0} width:{1} Height{2}", IPAddressStr, Width, Height);
        }
    }
}
