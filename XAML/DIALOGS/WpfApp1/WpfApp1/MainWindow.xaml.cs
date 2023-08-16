using System;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri uri = new Uri("/Page2.xaml", UriKind.Relative);
            frame.Source = uri;

        }
    }
}
