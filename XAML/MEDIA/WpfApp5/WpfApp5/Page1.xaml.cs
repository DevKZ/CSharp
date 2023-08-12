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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp5
{

    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        private MediaState m_stateCurrent;

        int ScaleFactor = Constants.InitialScale;
        int OffsetX = 0;
        int OffsetY = 0;


        public Page1()
        {
            InitializeComponent();


        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Stop();

        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            InitMediaTransform();

            Play();
        }

        private void SubFocusRedraw(Matrix matrix)
        {
            matrix.Invert();
            matrix.OffsetX = (double)(matrix.OffsetX / (mediaElement.NaturalVideoWidth / Constants.SubViewWidth));
            matrix.OffsetY = (double)(matrix.OffsetY / (mediaElement.NaturalVideoHeight / Constants.SubViewHeight));
            SubFocus.RenderTransform = new MatrixTransform(matrix);
        }

        private int DivW()
        {
            return (int)(mediaElement.NaturalVideoWidth / Constants.DivWLevel);
        }
        private int DivH()
        {
            return (int)(mediaElement.NaturalVideoHeight / Constants.DivHLevel);
        }

        private void MediaTransform()
        {
            TransformGroup tfg;

            tfg = new TransformGroup();

            tfg.Children.Add(new ScaleTransform((double)ScaleFactor / 10, (double)ScaleFactor / 10));
            tfg.Children.Add(new TranslateTransform(OffsetX, OffsetY));

            mediaElement.RenderTransform = tfg;

            SubFocusRedraw(mediaElement.RenderTransform.Value);
        }

        private void InitMediaTransform()
        {
            ScaleFactor = Constants.InitialScale;
            OffsetX = 0;
            OffsetY = 0;

            MediaTransform();
        }

        private bool IsRangeWidth()
        {
            bool ret = true;
            int t1;

            t1 = System.Math.Abs(OffsetX);
            if (((t1 == DivW() * 3) && (ScaleFactor <= 18)) || ((t1 == DivW() * 2) && (ScaleFactor <= 15)) || ((t1 == DivW()) && (ScaleFactor <= 13)))
            {
                ret = false;
            }
            return ret;

        }

        private bool IsRangeHeight()
        {
            bool ret = true;
            int t1;

            t1 = System.Math.Abs(OffsetY);
            if ((t1 == DivH() * 3) || ((t1 == DivH()*2) && (ScaleFactor <= 17)) || ((t1 == DivH()) && (ScaleFactor <= 14)))
            {
                ret = false;
            }
            return ret;

        }
        private bool IsRangeMove(Key k)
        {
            bool ret = true;
            int t1,t2,t3,t4;

            if (k == Key.Up)
            {
                t1 = (int)(OffsetY / DivH());
                if ((t1 == 3) || ((ScaleFactor <= 19) && (t1 == 2)) || ((ScaleFactor <= 16) && (t1 == 1)) || (ScaleFactor <= 13) && (t1 == 0))
                {
                    ret = false;
                }

            }
            else if (k == Key.Down)
            {
                t1 = (int)(OffsetY / DivH());
                if ((t1 == -3) || ((ScaleFactor <= 19) && (t1 == -2)) || ((ScaleFactor <= 16) && (t1 == -1)) || (ScaleFactor <= 13) && (t1 == 0))
                {
                    ret = false;
                }

            }
            else if (k == Key.Left)
            {
                t1 = (-(((OffsetX / DivW())) - 3)) + (20 - ScaleFactor) * (Constants.DivWLevel - 1);
                t2 = (t1 - 22);
                t3 = (t1 - 44);
                t4 = (t1 - 59);

                if (((t1 % (Constants.DivWLevel - 1)) == 0)
                    || ((t2 >= 0) && ((t2 % (Constants.DivWLevel - 1)) == 0))
                    || ((t3 >= 0) && ((t3 % (Constants.DivWLevel - 1)) == 0))
                    || ((t4 >= 0) && ((t4 % (Constants.DivWLevel - 1)) == 0)))
                {
                    ret = false;
                }
            }
            else if (k == Key.Right)
            {
                t1 = (((OffsetX / DivW()) + 4) - 1) + (20 - ScaleFactor) * (Constants.DivWLevel - 1);
                t2 = (t1 - 22);
                t3 = (t1 - 44);
                t4 = (t1 - 59);

                if (((t1 % (Constants.DivWLevel - 1)) == 0)
                    || ((t2 >= 0) && ((t2 % (Constants.DivWLevel - 1)) == 0))
                    || ((t3 >= 0) && ((t3 % (Constants.DivWLevel - 1)) == 0))
                    || ((t4 >= 0) && ((t4 % (Constants.DivWLevel - 1)) == 0)))
                {
                    ret = false;
                }
            }

            return ret;

        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Z:
                    break;

                case Key.O:
                    if (ScaleFactor < Constants.ScaleMax)
                    {
                        ScaleFactor += Constants.ScaleStep;
                        MediaTransform();

                    }

                    break;
                case Key.P:
                    // Scaleチェック
                    if (ScaleFactor > Constants.ScaleMin)
                    {
                        // Offsetチェック
                        if (IsRangeWidth() && IsRangeHeight())
                        {
                            ScaleFactor += -Constants.ScaleStep;

                            MediaTransform();
                        }
                    }
                    break;

                case Key.Left:
                    if (IsRangeMove(e.Key))
                    {
                        OffsetX += DivW();

                        MediaTransform();
                    }
                    break;
                case Key.Right:
                    if (IsRangeMove(e.Key))
                    {
                        OffsetX -= DivW();

                        MediaTransform();
                    }

                    break;
                case Key.Down:
                    if (IsRangeMove(e.Key))
                    {
                        OffsetY -= DivH();
                        MediaTransform();

                    }
                    break;
                case Key.Up:
                    if (IsRangeMove(e.Key))
                    {
                        OffsetY += DivH();
                        MediaTransform();

                    }
                    break;
                case Key.Escape:

                    InitMediaTransform();

                    break;

                case Key.Space:
                    if (m_stateCurrent == MediaState.Pause)
                        Play();
                    else
                        Pause();
                    break;

                case Key.W:
                    if (Application.Current.MainWindow.WindowStyle == WindowStyle.None)
                    {
                        Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                        Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                        Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    }
                    break;

                case Key.Q:

                    Stop();
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }

        private void Page_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void Page_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var name in fileNames)
                {
                    PlayOne(name);
                }
            }

        }

        private void PlayOne(string name)
        {
            Console.WriteLine(name);
            Stop();

            mediaElement.Source = new Uri(name);
            // MediaElementをロードさせる。
            Play();
            Stop();
        }

        /// <summary>
        /// 再生
        /// </summary>
        private void Play()
        {
            mediaElement.Play();
            m_stateCurrent = MediaState.Play;
        }
        /// <summary>
        /// 一時停止
        /// </summary>
        private void Pause()
        {
            mediaElement.Pause();
            m_stateCurrent = MediaState.Pause;
        }
        /// <summary>
        /// 停止
        /// </summary>
        private void Stop()
        {
            mediaElement.Stop();
            m_stateCurrent = MediaState.Stop;
        }
    }
}
