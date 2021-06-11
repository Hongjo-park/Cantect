using System;
using System.Collections.Generic;
using System.Text;

namespace WindowProject
{
    /// <summary>
    /// Controller class
    /// </summary>
    public class Controller
    {
        static Controller single;
        /// <summary>
        /// 단일체
        /// </summary>
        public static Controller Single
        {
            get { return single; }
        }
        
        static Controller()
        {
            single = new Controller();
        }
        private Controller()
        {
        }
        ImageServer img_server = null;
        SendEventClient se_client = null;
        /// <summary>
        /// 이미지 수신 이벤트 핸들러
        /// </summary>
        public event RecvImageEventHandler RecvedImage = null;
        string host_ip;
        /// <summary>
        /// 이미지 전송 클라이언트
        /// </summary>
        public SendEventClient SendEventClient
        {
            get { return se_client; }
        }
        /// <summary>
        /// get my ip
        /// </summary>
        public string My_IP
        {
            get { return NetworkInfomation.DefaultIP; }
        }
        /// <summary>
        /// Start setup image server
        /// </summary>
        /// <param name="host_ip"></param>
        public void Start(string host_ip)
        {
            this.host_ip = host_ip;
            img_server = new ImageServer(My_IP, NetworkInfomation.ImgPort);
            img_server.RecvedImage += Img_server_RecvImage;
            SetupClient.Setup(host_ip, NetworkInfomation.SetupPort);

        }

        private void Img_server_RecvImage(object sender, RecvImageEventArgs e)
        {
            if(RecvedImage != null)
            {
                RecvedImage(this, e);
            }
        }
        /// <summary>
        /// Stop to image server
        /// </summary>
        public void Stop()
        {
            if(img_server != null)
            {
                img_server.Close();
                img_server = null;
            }
        }
    }
}
