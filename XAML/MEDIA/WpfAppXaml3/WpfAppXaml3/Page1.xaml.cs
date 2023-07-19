using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfAppXaml3
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        /// <summary>
        /// スライダー更新用タイマー
        /// </summary>
        private DispatcherTimer m_timer;
        /// <summary>
        /// ユーザーの操作により設定中のステータス
        /// </summary>
        private MediaState m_stateCurrent;
        /// <summary>
        /// スライダー前回値
        /// </summary>
        private double m_dbLastSliderValue;

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
            // タイマースタート
            m_timer.Start();


        }

        //--------------------------------------------------------------------
        // コントロールイベント
        //--------------------------------------------------------------------
        /// <summary>
        /// 再生ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickPlay(object sender, RoutedEventArgs e)
        {
            Play();
        }
        /// <summary>
        /// 一時停止ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickPause(object sender, RoutedEventArgs e)
        {
            Pause();
        }
        /// <summary>
        /// 停止ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickStop(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SyncSliderAndSeek();
        }
        //----------------------------------------------------------------------------
        // MediaElement 関連処理
        //----------------------------------------------------------------------------
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
            slider.Value = 0;
        }
        /// <summary>
        /// スライダー値とMediaElement再生位置を同期する
        /// </summary>
        private void SyncSliderAndSeek()
        {
            if (m_stateCurrent == MediaState.Play || m_stateCurrent == MediaState.Pause)
            {
                if (m_dbLastSliderValue == slider.Value)
                {
                    // 動画経過時間に合わせてスライダーを動かす
                    double dbPrg = GetMovieProgress();
                    slider.Value = dbPrg * slider.Maximum;
                    m_dbLastSliderValue = slider.Value;
                    if (GetMediaState(mediaElement) == MediaState.Pause && m_stateCurrent == MediaState.Play)
                    {
                        mediaElement.Play();
                    }
                }
                else
                {
                    // Sliderを操作したとき
                    if (m_stateCurrent == MediaState.Play)
                    {
                        mediaElement.Pause();
                    }
                    // スライダーを動かした位置に合わせて動画の再生箇所を更新する
                    double dbSliderValue = slider.Value;
                    double dbDurationMS = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
                    int nSetMS = (int)(dbSliderValue * dbDurationMS / slider.Maximum);
                    mediaElement.Position = TimeSpan.FromMilliseconds(nSetMS);
                    m_dbLastSliderValue = slider.Value;
                }
            }
        }
        /// <summary>
        /// MediaElement 再生位置取得
        /// </summary>
        /// <returns></returns>
        private double GetMovieProgress()
        {
            TimeSpan tsCrnt = mediaElement.Position;
            TimeSpan tsDuration = mediaElement.NaturalDuration.TimeSpan;
            double dbPrg = tsCrnt.TotalMilliseconds / tsDuration.TotalMilliseconds;
            return dbPrg;
        }
        /// <summary>
        /// MediaElementステータス取得
        /// </summary>
        /// <param name="mediaElement"></param>
        /// <returns></returns>
        private MediaState GetMediaState(MediaElement mediaElement)
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(mediaElement);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);
            return state;
        }

        private void ButtonClickDlgOpen(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "全てのファイル (*.*)|*.*";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                // タイマー設定
                m_timer = new DispatcherTimer();
                m_timer.Interval = TimeSpan.FromMilliseconds(100);
                m_timer.Tick += dispatcherTimer_Tick;

                mediaElement.Source = new Uri(dialog.FileName);
                // MediaElementをロードさせる。
                mediaElement.Play();
                mediaElement.Stop();
            }
        }

        private void RectButton_Click(object sender, RoutedEventArgs e)
        {
            /* 動画の切り抜き(四角形) */
            RectangleGeometry rGeometry = new RectangleGeometry();

            rGeometry.Rect = new Rect(50, 50, 128, 128);
            this.mediaElement.Clip = rGeometry;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            /* 動画の切り抜き(円形) */
            EllipseGeometry eGeometry = new EllipseGeometry();

            eGeometry.RadiusX = 64;
            eGeometry.RadiusY = 64;
            eGeometry.Center = new Point(100, 100);
            this.mediaElement.Clip = eGeometry;
        }

        private void Origin_Click(object sender, RoutedEventArgs e)
        {
            /* 通常 */
            this.mediaElement.Clip = null;
            this.mediaElement.BitmapEffect = null;
        }

        private void TransParencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /* 透明度設定 */
            Slider s = new Slider();

            s = (Slider)sender;

            this.mediaElement.Opacity = (1.0 - s.Value);
        }

        private void Blur_Click(object sender, RoutedEventArgs e)
        {
            /* ぼかし */
            BlurBitmapEffect blur = new BlurBitmapEffect();

            blur.Radius = 5.0;
            blur.KernelType = KernelType.Gaussian;

            this.mediaElement.BitmapEffect = blur;
        }

        private void Shadow_Click(object sender, RoutedEventArgs e)
        {
            /* 影 */
            DropShadowBitmapEffect shadow = new DropShadowBitmapEffect();

            shadow.ShadowDepth = 10;

            this.mediaElement.BitmapEffect = shadow;
        }

        private void Glow_Click(object sender, RoutedEventArgs e)
        {
            /* グロウ */
            OuterGlowBitmapEffect glow = new OuterGlowBitmapEffect();

            glow.GlowSize = 20;

            this.mediaElement.BitmapEffect = glow;
        }

        private void Emboss_Click(object sender, RoutedEventArgs e)
        {
            /* Emboss */
            EmbossBitmapEffect emboss = new EmbossBitmapEffect();

            emboss.LightAngle = 100;
            emboss.Relief = 0.9;

            this.mediaElement.BitmapEffect = emboss;
        }

        private void TiltSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /* 傾き */
            RotateTransform rotate = new RotateTransform();

            Slider s = new Slider();

            s = (Slider)sender;

            rotate.Angle = s.Value;
            rotate.CenterX = this.mediaElement.Width / 2;
            rotate.CenterY = this.mediaElement.Height / 2;

            this.mediaElement.RenderTransform = rotate;
        }


        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /* 拡大 */
            ScaleTransform scal = new ScaleTransform();

            Slider s = new Slider();

            s = (Slider)sender;

            scal.CenterX = this.mediaElement.Width / 2;
            scal.CenterY = this.mediaElement.Height / 2;

            scal.ScaleX = s.Value;
            scal.ScaleY = s.Value;

            this.mediaElement.RenderTransform = scal;
        }

        private void TransParent_Click(object sender, RoutedEventArgs e)
        {
            if (BtnClickMe.Visibility == Visibility.Visible) BtnClickMe.Visibility = Visibility.Collapsed;
            else BtnClickMe.Visibility = Visibility.Visible;
        }

        private void Button_ClickMe(object sender, RoutedEventArgs e)
        {

        }
    }
}
