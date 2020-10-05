using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using FrameDecoderCore;
using FrameDecoderCore.DecodedFrames;
using RtspClientSharpCore;
using RtspClientSharpCore.RawFrames.Video;
using RtspClientSharpCore.Rtsp;
using FrameDecoderCore.FFmpeg;
using RtspClientSharpCore.RawFrames;
using PixelFormat = FrameDecoderCore.PixelFormat;

namespace Lingkail.VMS.RtspClient
{
    class Program
    {
        public static Microsoft.AspNetCore.Hosting.IWebHostEnvironment WebHostEnvironment { get; set; }
        private const int STREAM_WIDTH = 240;
        private const int STREAM_HEIGHT = 160;
        private static readonly Dictionary<FFmpegVideoCodecId, FFmpegVideoDecoder> _videoDecodersMap =
            new Dictionary<FFmpegVideoCodecId, FFmpegVideoDecoder>();
        //public static event EventHandler<IDecodedVideoFrame> FrameReceived;
        private static bool isWindows;
        private static bool isLinux;
        public Program(Microsoft.AspNetCore.Hosting.IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;//look at explanation above constructor 

            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);


            Console.WriteLine($"Platform {RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture}");
            var serverUri = new Uri("rtsp://admin:cctvsim14@175.139.231.133:554/Streaming/Channels/1");
            //var credentials = new NetworkCredential("admin", "cctvsim14");

            var connectionParameters = new ConnectionParameters(serverUri/*, credentials*/);
            var cancellationTokenSource = new CancellationTokenSource();

            Task connectTask = ConnectAsync(connectionParameters, cancellationTokenSource.Token);

            Console.WriteLine("Press any key to cancel");
            Console.ReadLine();

            cancellationTokenSource.Cancel();

            Console.WriteLine("Canceling");
            connectTask.Wait(CancellationToken.None);
        }


        private static async Task ConnectAsync(ConnectionParameters connectionParameters, CancellationToken token)
        {
            try
            {
                TimeSpan delay = TimeSpan.FromSeconds(5);

                using (var rtspClient = new RtspClientSharpCore.RtspClient(connectionParameters))
                {
                    rtspClient.FrameReceived += RtspClient_FrameReceived;

                    while (true)
                    {
                        Console.WriteLine("Connecting...");

                        try
                        {
                            await rtspClient.ConnectAsync(token);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        catch (RtspClientException e)
                        {
                            Console.WriteLine(e.ToString());
                            await Task.Delay(delay, token);
                            continue;
                        }

                        Console.WriteLine("Connected.");

                        try
                        {
                            await rtspClient.ReceiveAsync(token);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        catch (RtspClientException e)
                        {
                            Console.WriteLine(e.ToString());
                            await Task.Delay(delay, token);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private static void RtspClient_FrameReceived(object sender, RawFrame rawFrame)
        {

            if (!(rawFrame is RawVideoFrame rawVideoFrame))
                return;

            FFmpegVideoDecoder decoder = GetDecoderForFrame(rawVideoFrame);
            IDecodedVideoFrame decodedFrame = decoder.TryDecode(rawVideoFrame);

            if (decodedFrame != null)
            {
                var _FrameType = rawFrame is RawH264IFrame ? "IFrame" : "PFrame";
                TransformParameters _transformParameters = new TransformParameters(RectangleF.Empty,
                    new Size(STREAM_WIDTH, STREAM_HEIGHT),
                    ScalingPolicy.Stretch, PixelFormat.Bgra32, ScalingQuality.FastBilinear);

                var pictureSize = STREAM_WIDTH * STREAM_HEIGHT;
                IntPtr unmanagedPointer = Marshal.AllocHGlobal(pictureSize * 4);

                decodedFrame.TransformTo(unmanagedPointer, STREAM_WIDTH * 4, _transformParameters);
                byte[] managedArray = new byte[pictureSize * 4];
                Marshal.Copy(unmanagedPointer, managedArray, 0, pictureSize * 4);
                Marshal.FreeHGlobal(unmanagedPointer);
                Console.WriteLine($"Frame was successfully decoded! {_FrameType } Trying to save to JPG file...");
                try
                {
                    string mypath = "empty";//initialize

                    
                    //request.AddFile("f1", "http://localhost:44321/uploads/1/12638.jpg");                    
                    var im = CopyDataToBitmap(managedArray);
                    if (isWindows)
                    {
                        // Change to your path
                        mypath = Path.Combine(WebHostEnvironment.WebRootPath, "uploads\\", "1", "image21.jpg");//need escpae character double slash
                        //im.Save(@"E:\learning\testphoto\image21.jpg", ImageFormat.Jpeg);
                        im.Save(@mypath, ImageFormat.Jpeg);
                        return;
                    }
                    if (isLinux)
                    {
                        // Change to your path
                        mypath = Path.Combine(WebHostEnvironment.WebRootPath, "uploads/", "1", "image21.jpg");//linux path
                        im.Save(@mypath, ImageFormat.Jpeg);
                        return;
                    }
                    throw new PlatformNotSupportedException("Not supported OS platform!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error saving to file: {e.Message}");
                    Debug.WriteLine($"Error saving to file: {e.Message}");
                    Debug.WriteLine($"Stack trace: {e.StackTrace}");
                }
            }

        }


        private static Bitmap CopyDataToBitmap(byte[] data)
        {
            //Here create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(STREAM_WIDTH, STREAM_HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Create a BitmapData and Lock all pixels to be written 
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly, bmp.PixelFormat);

            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            //Unlock the pixels
            bmp.UnlockBits(bmpData);
            //Return the bitmap 
            return bmp;
        }

        private static FFmpegVideoDecoder GetDecoderForFrame(RawVideoFrame videoFrame)
        {
            FFmpegVideoCodecId codecId = DetectCodecId(videoFrame);
            if (!_videoDecodersMap.TryGetValue(codecId, out FFmpegVideoDecoder decoder))
            {
                decoder = FFmpegVideoDecoder.CreateDecoder(codecId);
                _videoDecodersMap.Add(codecId, decoder);
            }

            return decoder;
        }

        private static FFmpegVideoCodecId DetectCodecId(RawVideoFrame videoFrame)
        {
            if (videoFrame is RawJpegFrame)
                return FFmpegVideoCodecId.MJPEG;
            if (videoFrame is RawH264Frame)
                return FFmpegVideoCodecId.H264;

            throw new ArgumentOutOfRangeException(nameof(videoFrame));
        }
    }
}
