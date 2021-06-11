using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
#pragma warning disable CS1591

namespace WindowProject
{
    /// <summary>
    /// 메세지 타임 열거형
    /// </summary>
    public enum MsgType
    {
        MT_NONE,
        MT_KEYDOWN,
        MT_KEYUP,
        MT_MLEFTDOWN,
        MT_MLEFTUP,
        MT_MRIGHTDOWN,
        MT_MRIGHTUP,
        MT_MMIDDLEDOWN,
        MT_MMIDDLEUP,
        M_MMOVE
    }

    public class SendEventClient
    {
        IPEndPoint ep;
        byte[] data = new byte[9];

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public SendEventClient(String ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        /// <summary>
        /// 키를 눌렀을 때 데이터 보내기
        /// </summary>
        /// <param name="Key"></param>
        public void SendKeyDown(int Key)
        {
            data[0] = (byte)MsgType.MT_KEYDOWN;
            // byte Converting for data 1~4
            Array.Copy(BitConverter.GetBytes(Key), 0, data, 1, 4);
            SendData(data);

        }
        /// <summary>
        /// 키를 땠을 때 데이터 보내기
        /// </summary>
        /// <param name="Key"></param>
        public void SendKeyUp(int Key)
        {
            data[0] = (byte)MsgType.MT_KEYUP;
            // byte Converting for data 1~4
            Array.Copy(BitConverter.GetBytes(Key), 0, data, 1, 4);
            SendData(data);
        }
        /// <summary>
        /// 데이터를 보내는 메소드
        /// </summary>
        /// <param name="data"></param>
        private void SendData(byte[] data)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(ep);
            sock.Send(data);
            sock.Close();

        }
        public void SendMouseDown(MouseButtons mouseButtons)
        {
            switch(mouseButtons)
            {
                case MouseButtons.Left: data[0] = (byte)MsgType.MT_MLEFTDOWN;
                    break;
                case MouseButtons.Right: data[0] = (byte)MsgType.MT_MRIGHTDOWN;
                    break;
                case MouseButtons.Middle: data[0] = (byte)MsgType.MT_MMIDDLEDOWN;
                    break;
            }
            SendData(data);
        }

        public void SendMouseUP(MouseButtons mouseButtons)
        {
            switch (mouseButtons)
            {
                case MouseButtons.Left:
                    data[0] = (byte)MsgType.MT_MLEFTUP;
                    break;
                case MouseButtons.Right:
                    data[0] = (byte)MsgType.MT_MRIGHTUP;
                    break;
                case MouseButtons.Middle:
                    data[0] = (byte)MsgType.MT_MMIDDLEUP;
                    break;
            }
            SendData(data);
        }    

        public void SendMouseMove(int x, int y)
        {
            data[0] = (byte)MsgType.M_MMOVE;
            Array.Copy(BitConverter.GetBytes(x), 0, data, 1, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, data, 5, 4);
            SendData(data);
        }
    }
}
