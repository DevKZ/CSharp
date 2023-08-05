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
        private Matrix default_matrix;
        private MediaState m_stateCurrent;

        double SC = Constants.InitialScale;
        int TC = 0;

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
            mediaElement.RenderTransform = new ScaleTransform(SC, SC);
            default_matrix = mediaElement.RenderTransform.Value;
            SubFocusRedraw(mediaElement.RenderTransform.Value);

            Play();
        }

        private void SubFocusRedraw(Matrix matrix)
        {
            var tfg = new TransformGroup();

            double scale = (Constants.SizeScale / matrix.M11);
            tfg.Children.Add(new ScaleTransform(scale - Constants.ScaleStep, scale - Constants.ScaleStep));
            // ずれるため調整
            tfg.Children.Add(new TranslateTransform(-(double)((matrix.OffsetX * scale * 1.2) / (Constants.DivWLevel)), -(double)((matrix.OffsetY * scale * 1.1) / Constants.DivHLevel)));

            SubFocus.RenderTransform = tfg;
        }

        private int DivW()
        {
            return (int)(mediaElement.NaturalVideoWidth / Constants.DivWLevel);
        }
        private int DivH()
        {
            return (int)(mediaElement.NaturalVideoHeight / Constants.DivHLevel);
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            Matrix matrix;
            TransformGroup tfg;
            int t1, t2, t3, t4;
            int s1;

            switch (e.Key)
            {
                case Key.O:
                    s1 = (int)(SC * 10);    //整数化
                    if (s1 <Constants.ScaleMax)
                    {
                        SC += Constants.ScaleStep;

                        tfg = new TransformGroup();

                        tfg.Children.Add(new ScaleTransform(SC, SC));
                        tfg.Children.Add(new TranslateTransform(TC, 0));

                        mediaElement.RenderTransform = tfg;

                        SubFocusRedraw(mediaElement.RenderTransform.Value);
                    }

                    break;
                case Key.P:
                    s1 = (int)(SC * 10);    //整数化
                    if (s1 >= Constants.ScaleMin)
                    {
                        t1 = System.Math.Abs(TC);
                        if (((t1 == DivW() * 3) && (s1 <= 18)) || ((t1 == DivW()*2) && (s1 <= 15)) || ((t1 == DivW()) && (s1 <= 13)))
                        {
                            // nop
                        }
                        else
                        {
                            SC += -Constants.ScaleStep;

                            tfg = new TransformGroup();

                            tfg.Children.Add(new ScaleTransform(SC, SC));
                            tfg.Children.Add(new TranslateTransform(TC, 0));

                            mediaElement.RenderTransform = tfg;

                            SubFocusRedraw(mediaElement.RenderTransform.Value);
                        }
                    }
                    break;

                case Key.Left:
                    s1 = (int)(SC * 10);    //整数化

                    t1 = (-(((TC / DivW())) -3)) +(20 - s1) * (Constants.DivWLevel-1);
                    t2 = (t1 - 22);
                    t3 = (t1 - 44);
                    t4 = (t1 - 59);

                    if (((t1 % (Constants.DivWLevel - 1)) == 0)
                        || ((t2 >= 0) && ((t2 % (Constants.DivWLevel - 1)) == 0))
                        || ((t3 >= 0) && ((t3 % (Constants.DivWLevel - 1)) == 0))
                        || ((t4 >= 0) && ((t4 % (Constants.DivWLevel - 1)) == 0)))
                    {
                        //nop
                    }
                    else
                    {
                        TC += DivW();
                        tfg = new TransformGroup();

                        tfg.Children.Add(new ScaleTransform(SC, SC));
                        tfg.Children.Add(new TranslateTransform(TC, 0));

                        mediaElement.RenderTransform = tfg;

                        SubFocusRedraw(mediaElement.RenderTransform.Value);
                    }

                    break;
                case Key.Right:
                    s1 = (int)(SC * 10);    //整数化

                    t1 = (((TC / DivW()) +4)-1) + (20 - s1) * (Constants.DivWLevel - 1);
                    t2 = (t1 - 22);
                    t3 = (t1 - 44);
                    t4 = (t1 - 59);

                    if (((t1 % (Constants.DivWLevel - 1)) == 0)
                        || ((t2 >= 0) && ((t2 % (Constants.DivWLevel - 1)) == 0))
                        || ((t3 >= 0) && ((t3 % (Constants.DivWLevel - 1)) == 0))
                        || ((t4 >= 0) && ((t4 % (Constants.DivWLevel - 1)) == 0)))
                    {
                        //nop
                    }
                    else
                    {
                        TC -= DivW();
                        tfg = new TransformGroup();

                        tfg.Children.Add(new ScaleTransform(SC, SC));
                        tfg.Children.Add(new TranslateTransform(TC, 0));

                        mediaElement.RenderTransform = tfg;

                        SubFocusRedraw(mediaElement.RenderTransform.Value);
                    }
                    break;
                case Key.Down:
                    s1 = (int)(SC * 10);    //整数化
                    matrix = mediaElement.RenderTransform.Value;
                    t1 = (int)matrix.OffsetY / DivH();
                    if ((t1 == -3) || ((s1 <= 19) && (t1 == -2)) || ((s1 <= 14) && (t1 == -1)) || ((s1 <= 13) && (t1 == 0)))
                    {
                        // nop
                    }
                    else
                    {
                        matrix.Translate(0, -DivH());

                        mediaElement.RenderTransform = new MatrixTransform(matrix);

                        SubFocusRedraw(matrix);
                    }
                    break;
                case Key.Up:
                    s1 = (int)(SC * 10);    //整数化
                    matrix = mediaElement.RenderTransform.Value;
                    t1 =(int)matrix.OffsetY / DivH();
                    if ((t1 == 3) || ((s1 <= 19) && (t1 == 2)) || ((s1 <= 14) && (t1 == 1)) || ((s1 <=13) && (t1 == 0)))
                    {
                        // nop
                    }
                    else
                    {
                        matrix.Translate(0, DivH());

                        mediaElement.RenderTransform = new MatrixTransform(matrix);

                        SubFocusRedraw(matrix);
                    }
                    break;
                case Key.Escape:

                    mediaElement.RenderTransform = new MatrixTransform(default_matrix);

                    TC = 0;
                    SC = Constants.InitialScale;

                    SubFocusRedraw(mediaElement.RenderTransform.Value);
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
