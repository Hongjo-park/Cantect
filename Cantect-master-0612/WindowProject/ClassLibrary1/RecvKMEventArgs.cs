using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WindowProject
{
    /// <summary>
    /// Key Mouse Event 인자 클래스
    /// </summary>
    public class RecvKMEventArgs : EventArgs
    {
        /// <summary>
        /// get Event Buffer
        /// </summary>
        public EventBuffer EventBuffer
        {
            get;
            private set;
        }

        /// <summary>
        /// get Key Event
        /// </summary>
        public int Key
        {
            get { return EventBuffer.Key; }
        }

        /// <summary>
        /// get Mouse Event
        /// </summary>
        public Point MP
        {
            get { return EventBuffer.MP; }
        }

        /// <summary>
        /// get Message type
        /// </summary>
        public MsgType MT
        {
            get { return EventBuffer.MT; }
        }
        
        internal RecvKMEventArgs(EventBuffer eventBuffer)
        {
            EventBuffer = eventBuffer;
        }
    }
    /// <summary>
    /// 대리자
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RecvKMEventHandler(object sender, RecvKMEventArgs e);
}
