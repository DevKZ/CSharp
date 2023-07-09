using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SharpDX.MediaFoundation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Interop;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", CallingConvention = CallingConvention.StdCall)]
        private static extern void RtlMoveMemory(IntPtr Destination, IntPtr Source, [MarshalAs(UnmanagedType.U4)] int Length);


        public MainWindow()
        {
            InitializeComponent();

            //MediaFoundation使用前にMediaManager.Startupが必要
            MediaManager.Startup();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //MediaFoundation終了処理
            MediaManager.Shutdown();

        }

        //// System.Drawingの参照追加が必要
        //// 動画フォーマットによっては正しく取得できない
        ///   CBR モードがだめ？

        void Thumnail_ImageSource(string fn)
        {
            System.Drawing.Bitmap bitmap = CreateVideoBitmap(fn, 5);

            var hBitmap = bitmap.GetHbitmap();

            // CreateBitmapSourceFromHBitmap()で System.Windows.Media.Imaging.BitmapSource に変換する
            System.Windows.Media.Imaging.BitmapSource bitmapsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    hBitmap,
                                    IntPtr.Zero,
                                    Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions());

            IMG.Source = bitmapsource;

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "全てのファイル (*.*)|*.*";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {

                Thumnail_ImageSource(dialog.FileName);
            }

        }


        private Bitmap CreateVideoBitmap(string moviePath, double positionOfPercent)
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
                        var bmp = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        RtlMoveMemory(bmpData.Scan0, pBuffer, stride * rect.Height);
                        bmp.UnlockBits(bmpData);
                        buf.Unlock();
                        return bmp;

                    }
                }
            }
            finally
            {
                if (reader != null) reader.Dispose();

                stopwatch.Stop();
                Console.WriteLine($"process time {stopwatch.ElapsedMilliseconds} msec ({moviePath})\r\n");
            }
        }
    }


}
