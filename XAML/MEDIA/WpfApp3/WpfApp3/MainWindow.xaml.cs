using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using SharpDX.MediaFoundation;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //MediaFoundation使用前にMediaManager.Startupが必要
            MediaManager.Startup();

        }

        private WriteableBitmap test2()
        {
            WriteableBitmap lwb = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgr32, null);
            lwb.Lock();
            unsafe
            {
                byte* ptr = (byte*)lwb.BackBuffer;
                for (int y = 0; y < lwb.PixelHeight; y++)
                {
                    for (int x = 0; x < lwb.PixelWidth; x++)
                    {
                        byte* p = ptr + (y * lwb.BackBufferStride) + (x * 4);
                        p[0] = 255;
                        p[1] = 64;
                        p[2] = 0;
                        //p[3] = 0;
                    }
                }
            }
            lwb.AddDirtyRect(new Int32Rect(0, 0, lwb.PixelWidth - 200, lwb.PixelHeight));
            lwb.Unlock();
            return lwb;

        }


        private void Window_Closed(object sender, EventArgs e)
        {
            //MediaFoundation終了処理
            MediaManager.Shutdown();

        }
#if false
        public MF.Sample CreateSampleFromFrame(byte[] data)
        {
            MF.MediaBuffer mediaBuffer = MF.MediaFactory.CreateMemoryBuffer(data.Length);

            // Write all contents to the MediaBuffer for media foundation
            int cbMaxLength = 0;
            int cbCurrentLength = 0;
            IntPtr mediaBufferPointer = mediaBuffer.Lock(out cbMaxLength, out cbCurrentLength);
            try
            {

                Marshal.Copy(data, 0, mediaBufferPointer, data.Length);
            }
            finally
            {
                mediaBuffer.Unlock();
                mediaBuffer.CurrentLength = data.Length;
            }

            // Create the sample (includes image and timing information)
            MF.Sample sample = MF.MediaFactory.CreateSample();
            sample.AddBuffer(mediaBuffer);

            return sample;
        }
#endif
        /// <summary>
        /// 動画の指定位置から画像を取得し、WriteableBitmapオブジェクトを作成する。
        /// </summary>
        /// <param name="moviePath">動画ファイルパス</param>
        /// <param name="positionOfPercent">取得する再生位置(0～100%)</param>
        /// <returns>作成したWriteableBitmapオブジェクト</returns>
        private WriteableBitmap CreateVideoWriteableBitmap(string moviePath, double positionOfPercent)
        {
            var stopwatch = Stopwatch.StartNew();
            SourceReader reader = null;
            try
            {
                using (var attr = new MediaAttributes(1))
                using (var newMediaType = new MediaType())
                {
                    //SourceReaderに動画のパスを設定
                    attr.Set(SourceReaderAttributeKeys.EnableVideoProcessing.Guid, true);
                    reader = new SourceReader(moviePath, attr);

                    //出力メディアタイプをRGB32bitに設定
                    newMediaType.Set(MediaTypeAttributeKeys.MajorType, MediaTypeGuids.Video);
                    newMediaType.Set(MediaTypeAttributeKeys.Subtype, VideoFormatGuids.Rgb32);
                    reader.SetCurrentMediaType(SourceReaderIndex.FirstVideoStream, newMediaType);

                    //元のメディアタイプから動画情報を取得する
                    // duration:ビデオの総フレーム数
                    // frameSize:フレーム画像サイズ（上位32bit:幅 下位32bit:高さ）
                    // stride:フレーム画像一ライン辺りのバイト数
                    var mediaType = reader.GetCurrentMediaType(SourceReaderIndex.FirstVideoStream);
                    var duration = reader.GetPresentationAttribute(SourceReaderIndex.MediaSource, PresentationDescriptionAttributeKeys.Duration);
                    var frameSize = mediaType.Get(MediaTypeAttributeKeys.FrameSize);
                    var stride = mediaType.Get(MediaTypeAttributeKeys.DefaultStride);
                    var rect = new Rectangle()
                    {
                        Width = (int)(frameSize >> 32),
                        Height = (int)(frameSize & 0xffffffff)
                    };

                    //取得する動画の位置を設定
                    var mulPositionOfPercent = Math.Min(Math.Max(positionOfPercent, 0), 100.0) / 100.0;
                    reader.SetCurrentPosition((long)(duration * mulPositionOfPercent));

                    //動画から1フレーム取得し、Bitmapオブジェクトを作成してメモリコピー
                    int actualStreamIndex;
                    SourceReaderFlags readerFlags;
                    long timeStampRef;
                    using (var sample = reader.ReadSample(SourceReaderIndex.FirstVideoStream, SourceReaderControlFlags.None, out actualStreamIndex, out readerFlags, out timeStampRef))
                    using (var buf = sample.ConvertToContiguousBuffer())
                    {
                        int maxLength;
                        int currentLength;
                        var pBuffer = buf.Lock(out maxLength, out currentLength);
                        double dpix = double.Parse(TXT_DPIX.Text);
                        double dpiy = double.Parse(TXT_DPIY.Text);

                        var width = (int)rect.Width;
                        var height = (int)rect.Height;

                        var bmp = new WriteableBitmap(width, height, dpix, dpiy, PixelFormats.Bgr32, null);
// 以下は描画されない
//                        var bmp = new WriteableBitmap(width, height, dpix, dpiy, PixelFormats.Pbgra32, null);

                        var size = width * height * 4;

                        bmp.Lock();
                        unsafe
                        {
                            byte* ptr = (byte*)bmp.BackBuffer;
                            byte* srcptr = (byte*)pBuffer;

                            Buffer.MemoryCopy(srcptr, ptr, size, size);
                            // Marshal.Copy(pBuffer, bmp.BackBuffer, 0, size);
                        }

                        bmp.AddDirtyRect(new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight));
                        bmp.Unlock();

                        buf.Unlock();

                        return bmp;
                    }
                }
            }
            finally
            {
                if (reader != null) reader.Dispose();

                stopwatch.Stop();
                string ptime = $"process time {stopwatch.ElapsedMilliseconds} msec ({moviePath})\r\n";
                LBL_PROCESS.Content = ptime;
                Console.WriteLine(ptime);
            }
        }

        private void BTN_Test_Click(object sender, RoutedEventArgs e)
        {
            IMG.Source = test2();
        }

        private void BTN_MakeThumnail_Click(object sender, RoutedEventArgs e)
        {
            double sec;
            sec = double.Parse(TXT_SEC.Text);
            IMG.Source = CreateVideoWriteableBitmap(TXT_FILEPATH.Text, sec);

        }

        private void BTN_FILE_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "全てのファイル (*.*)|*.*";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                TXT_FILEPATH.Text = dialog.FileName;
            }

        }
    }
}
