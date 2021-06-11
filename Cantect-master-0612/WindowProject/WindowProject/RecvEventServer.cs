using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowProject
{
    /// <summary>
    /// 이벤트 수신 서버
    /// </summary>
    public class RecvEventServer
    {
        Socket sock;
        /// <summary>
        /// 
        /// </summary>
        public event RecvKMEventHandler RecvedKMEvent = null;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public RecvEventServer(String ip, int port)
        {
            // Socket 생성
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Bind(ep); // 결합
            sock.Listen(5); // 
            sock.BeginAccept(DoAccept, null); // 비동기


        }

        void DoAccept(IAsyncResult result)
        {
            if(sock != null)
            {
                // EndAccept -> Client 소통을 위한 소켓
                Socket dosock = sock.EndAccept(result);
                sock.BeginAccept(DoAccept, null);
                Receive(dosock);
            }
        }

        private void Receive(Socket dosock)
        {
            byte[] buffer = new byte[9];
            int n = dosock.Receive(buffer);
            if (RecvedKMEvent != null)
            {
                //Event 인자 생성
                RecvKMEventArgs e = new RecvKMEventArgs(new EventBuffer(buffer));
                // 수신 이벤트 선언
                RecvedKMEvent(this, e);
            }
            dosock.Close();
        }

        /// <summary>
        /// 서버 닫기
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
