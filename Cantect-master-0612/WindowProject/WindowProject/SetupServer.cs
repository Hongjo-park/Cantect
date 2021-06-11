using System;
using System.Net;
using System.Net.Sockets;
using WindowProjectLib;

namespace WindowProject
{
    /// <summary>
    /// server class
    /// </summary>
    public static class SetupServer
    {
        static Socket lis_sock;
        /// <summary>
        /// event handler
        /// </summary>
        static public event RecvRCInfoEventHandler RecvRCInfo = null;
        static string ip;
        static int port;

        /// <summary>
        /// server Start method
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void Start(string ip, int port)
        {
            SetupServer.ip = ip;
            SetupServer.port = port;
            SocketBooting();
        }

        /// <summary>
        /// soket booting method
        /// </summary>
        private static void SocketBooting()
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            lis_sock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            lis_sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            lis_sock.Bind(ep);
            lis_sock.Listen(1);
            lis_sock.BeginAccept(DoAccept, null);
        }
        /// <summary>
        /// socket accept method
        /// </summary>
        /// <param name="result"></param>
        static void DoAccept(IAsyncResult result)
        {
            if (lis_sock == null)
            {
                return;
            }
            try
            {
                Socket sock = lis_sock.EndAccept(result);
                DoIt(sock);
                lis_sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close();
            }
        }
        /// <summary>
        /// socket
        /// </summary>
        /// <param name="dosock"></param>
        static void DoIt(Socket dosock)
        {
            if(RecvRCInfo != null)
            {
                RecvRCInfoEventArgs e = new RecvRCInfoEventArgs(dosock.RemoteEndPoint);
                RecvRCInfo(null, e); // sender = null
            }
            dosock.Close();
        }
        /// <summary>
        /// socket close method
        /// </summary>
        public static void Close()
        {
            if (lis_sock != null)
            {
                lis_sock.Close();
                lis_sock = null;
            }
        }
    }
}
