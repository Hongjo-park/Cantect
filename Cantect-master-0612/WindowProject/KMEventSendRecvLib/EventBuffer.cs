using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowProject
{
    /// <summary>
    /// 이벤트 수신 데이터 변환
    /// </summary>
    public class EventBuffer
    {
        /// <summary>
        /// Message type
        /// </summary>
        public MsgType MT
        {
            get;
            private set;
        }

        /// <summary>
        /// get Key Down or Up 
        /// </summary>
        public int Key
        {
            get;
            private set;
        }

        /// <summary>
        /// get Mouse point
        /// </summary>
        public Point MP
        {
            get;
            private set;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="data"></param>
        public EventBuffer(byte[] data)
        {
            MT = (MsgType)data[0]; // Message type

            switch(MT)
            {
                case MsgType.MT_KEYDOWN:
                case MsgType.MT_KEYUP:
                    ConvertKey(data); break;
                case MsgType.M_MMOVE:
                    ConvertPoint(data); break;
            }
        }

        private void ConvertPoint(byte[] data)
        {
            // data -> Point
            Point mp = new Point(0, 0);
            mp.X = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + (data[1]);
            mp.Y = (data[8] << 24) + (data[7] << 16) + (data[6] << 8) + (data[5]);
            MP = mp;
        }

        private void ConvertKey(byte[] data)
        {
            // data -> Key(int)
            // 24Bit, 16, 8 shift => 하나의 정수 형태
            Key = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + (data[1]);
         }
    }
}
