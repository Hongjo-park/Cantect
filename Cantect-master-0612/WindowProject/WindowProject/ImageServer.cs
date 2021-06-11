using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WindowProject
{   
    /// <summary>
    /// 이미지 수신 서버
    /// </summary>
    public class ImageServer
    {
        Socket sock;
        /// <summary>
        /// 
        /// </summary>
        public event RecvImageEventHandler RecvedImage = null;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip">IP주소</param>
        /// <param name="port">포트</param>
        public ImageServer(string ip, int port)
        {   
            //소켓 생성
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            //생성한 소켓과 EndPoint(IP주소 + 포트) 결합
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Bind(ep);

            //Back 로그 큐 크기 5로 설정
            sock.Listen(5);
            sock.BeginAccept(DoAccept, null);
        }
        void DoAccept(IAsyncResult result)
        {
            if(sock == null)
            {
                return;
            }
            try
            {
                Socket dosock = sock.EndAccept(result);
                Recevice(dosock);
                sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close();
            }
        }

        /// <summary>
        /// 이미지 서버 닫음
        /// </summary>
        public void Close()
        {
            if(sock != null)
            {
                sock.Close();
                sock = null;
            }
        }

        /// <summary>
        /// 이미지를 수신
        /// </summary>
        /// <param name="dosock">소켓</param>
        private void Recevice(Socket dosock)
        {
            byte[] lbuf = new byte[4];  //lbuf에 이미지 길이를 수신
            dosock.Receive(lbuf);   //수신

            int len = BitConverter.ToInt32(lbuf, 0);    //수신된 이미지 길이를 정수형으로 변환
            byte[] buffer = new byte[len];  // len(이미지 길이) 만큼의 버퍼 선언
            int trans = 0;
            while(trans < len)  //이미지 길이가 수신된 데이터 길이보다 길 경우(수신이 완료되지 않았을 경우) 계속 수신함
            {
                trans += dosock.Receive(buffer, trans, len - trans, SocketFlags.None);
            }

            if(RecvedImage != null) // 이미지 수신 이벤트가 존재함
            {
                //이미지 수신 이벤트 발생
                IPEndPoint ep = dosock.RemoteEndPoint as IPEndPoint;
                RecvImageEventArgs e = new RecvImageEventArgs(ep, ConvertBitmap(buffer));
                RecvedImage(this, e);
            }
        }

        /// <summary>
        /// 수신된 버퍼를 비트맵 이미지로 변환
        /// </summary>
        /// <param name="data">버퍼</param>
        /// <returns>비트맵 이미지</returns>
        public Bitmap ConvertBitmap(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, (int)data.Length); // memory stream에 buffer 기록
            Bitmap bitmap = new Bitmap(ms); // ms로 bitmap 생성
            return bitmap;
        }
    }
}
