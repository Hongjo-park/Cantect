using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Automation;
using WindowProjectLib;

namespace WindowProject
{
    public class Remote
    {
        static Remote single;
        public static Remote Single
        {
            get { return single; }
        }
        static Remote()
        {
            single = new Remote();
        }
        private Remote()
        {
            AutomationElement Auto_Element = AutomationElement.RootElement;
            // 윈도우의 사각 영역을 얻어온다
            System.Windows.Rect rt = Auto_Element.Current.BoundingRectangle;
            Rect = new Rectangle((int)rt.Left, (int)rt.Top, (int)rt.Width, (int)rt.Height);

            SetupServer.RecvRCInfo += SetupServer_RecvedInfoEventHandler;
            SetupServer.Start(My_IP, NetworkInfomation.SetupPort);

        }

        public string My_IP
        {
            get { return NetworkInfomation.DefaultIP; }
        }

        private void SetupServer_RecvedInfoEventHandler(object sender, RecvRCInfoEventArgs e)
        {
            RecvedRCInfo(this, e);
        }

        /// <summary>
        /// 원격제어 요청을 받기 위한 핸들러
        /// </summary>
        public event RecvRCInfoEventHandler RecvedRCInfo = null;
        public event RecvKMEventHandler RecvedKMEvent = null;
        RecvEventServer RE_server = null;
 
       
        public Rectangle Rect
        {
            get;
            private set;
        }

        public void RecvEventStart()
        {
            RE_server = new RecvEventServer(My_IP, NetworkInfomation.EventPort);
            RE_server.RecvedKMEvent += RE_server_RecvedKMEvent;
        }

        private void RE_server_RecvedKMEvent(object sender, RecvKMEventArgs e)
        {
            //bypass
            if (RecvedKMEvent != null)
            {
                RecvedKMEvent(this, e);
            }
            switch (e.MT)
            {
                case MsgType.MT_KEYDOWN: Wrap.KeyDown(e.Key); break;
                case MsgType.MT_KEYUP: Wrap.KeyUp(e.Key); break;
                case MsgType.MT_MLEFTDOWN: Wrap.LeftDown(); break;
                case MsgType.MT_MLEFTUP: Wrap.LeftUp(); break;
                case MsgType.M_MMOVE: Wrap.Move(e.MP); break;
            }
            
        }
        public void Stop()
        {
            SetupServer.Close();
            if (RE_server != null)
            {
                RE_server.Close();
                RE_server = null;
            }
        }
    }
}
