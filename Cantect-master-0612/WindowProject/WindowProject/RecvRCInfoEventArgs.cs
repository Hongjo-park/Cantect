using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowProjectLib
{
    /// <summary>
    /// RecvRCInfoEventArgs
    /// </summary>
    public class RecvRCInfoEventArgs
    {
        /// <summary>
        /// 원격 제어 요청 수신 이벤트 인자SeoEunBin
        /// </summary>

        /// <summary>
        /// IP 단말 정보 - 가져오기
        /// </summary>
        public IPEndPoint IPEndPoint
        {
            get;
            private set;
        }
        /// <summary>
        /// IP 주소 문자열 - 가져오기
        /// </summary>
        public string IPAddressStr
        {
            get
            {
                return IPEndPoint.Address.ToString();
            }
        }
        /// <summary>
        /// 포트 - 가져오기
        /// </summary>
        public int Port
        {
            get
            {
                return IPEndPoint.Port;
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="RemoteEndPoint">상대측 단말 정보</param>
        public RecvRCInfoEventArgs(EndPoint RemoteEndPoint)
        {
            IPEndPoint = RemoteEndPoint as IPEndPoint;
        }

       

    }
    /// <summary>
    /// delegate for event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RecvRCInfoEventHandler(object sender, RecvRCInfoEventArgs e);
}
