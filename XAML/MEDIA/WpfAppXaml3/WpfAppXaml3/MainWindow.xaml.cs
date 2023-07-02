using Microsoft.Win32;
using System;
using System.Windows;


namespace WpfAppXaml3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private int cnt = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnt++;
            BTNTEST.Content = cnt;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "全てのファイル (*.*)|*.*";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                mediaElement.Source = new Uri(dialog.FileName);
                mediaElement.Play();
                mediaElement.Stop();
            }
        }
    }
}
