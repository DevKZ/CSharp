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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DrawImage();
        }

        private void DrawImage()
        {
            // 描画先になるImageを生成する
            var image = new Image();
            // WritableBitmapを生成する
            var width = (int)Width;
            var height = (int)Height;
            var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Pbgra32, null);
            // 描画データを格納するバイト列を生成する
            var size = width * height * 4;
            var pixels = new byte[size];
            // バイト列に描画データを格納する
            for (int i = 0; i < size; i += 4)
            {
                pixels[i] = 255;        // Blue
                pixels[i + 1] = 0;      // Green
                pixels[i + 2] = 0;      // Red
                pixels[i + 3] = 255;    // Alpha
            }
            // バイト列 -> BitmapImage
            var stride = width * 4;    // 1行あたりのバイト数
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0, 0);
            // Image.Sourceに作成したWriteableBitmapを指定する
            image.Source = bitmap;
            // Canvasに登録する
            xCanvas.Children.Add(image);
        }
    }
}
