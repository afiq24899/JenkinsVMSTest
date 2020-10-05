using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Text;
using System.Linq;

namespace LibVLCSharp.WinForms.Sample
{
    public partial class Form1 : Form
    {
        public LibVLC _libVLC;
        public MediaPlayer _mp;

        public Form1()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }

            InitializeComponent();
            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mp;
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var mess_form = ""; //define a variable message winform
            string[] passedInArgs = Environment.GetCommandLineArgs();
            if (passedInArgs.Contains("/0"))//this will load rtsp
            {
                var stream_CCTV_SENA_OFFICE = "rtsp://admin:cctvsim14@175.139.231.133:554/Streaming/Channels/1"; //public IP for testing
                //var stream_CCTV = "rtsp://admin:ITIS12345@10.20.5.23:554/Streaming/Channels/1";//need VPN
                                                                                               //var stream_file = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
                var mmm = new Media(_libVLC, stream_CCTV_SENA_OFFICE, FromType.FromLocation);
                //mmm.AddOption($":rtsp-tcp");
                //mmm.AddOption($":rtsp-tcp");
                mmm.AddOption($":network-caching=1200");
                _mp.Play(mmm);
            }

            if (passedInArgs.Contains("/1"))//this won't load rtsp
            {
                mess_form = "This VMS doesn't have CCTV. You can safely close this window when it appears";
                MessageBox.Show(mess_form);
            }
            
        }

        private void videoView1_Click(object sender, EventArgs e)
        {

        }
    }
}
