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

        public MainWindow()
        {
            InitializeComponent();

            Uri uri = new Uri("/Page1.xaml", UriKind.Relative);
            frame.Source = uri;
        }
    }
}
