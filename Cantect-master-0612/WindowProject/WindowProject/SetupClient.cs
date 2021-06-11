using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WindowProject
{
    /// <summary>
    /// Setup Client cs
    /// </summary>
    public class SetupClient
    {
        /// <summary>
        /// connected event
        /// </summary>
        public static event EventHandler ConnectedEventHandler = null;
        /// <summary>
        /// connect failed event
        /// </summary>
        public static event EventHandler ConnectFailedEventHandler = null;
        static Socket sock;
        /// <summary>
        /// 원격 제어 요청 메서드
        /// </summary>
        public static void Setup(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip); //상대 IP 주소 개체를 생성
            IPEndPoint ep = new IPEndPoint(ipaddr, port); //IP 단말 주소(IP주소 + 포트번호) 개체 생성
           
            sock = new Socket(AddressFamily.InterNetwork, //네트워크 주소 체계
                SocketType.Stream, //전송 방식
                ProtocolType.Tcp);//프로토콜
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            sock.BeginConnect(ep, DoConnect, sock);
        }
        static void DoConnect(IAsyncResult result)
        {
            try
            {
                sock.EndConnect(result);
                if (ConnectedEventHandler != null)
                {
                    ConnectedEventHandler(null, new EventArgs()); //연결 성공
                }
            }
            catch
            {
                if (ConnectFailedEventHandler != null)
                {
                    ConnectFailedEventHandler(null, new EventArgs()); //연결 실패
                }
            }
            sock.Close();
        }
    }
}
