using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Page3.xaml の相互作用ロジック
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await InputDlg.Show("ダイアログの説明文です。よろしいですか？", "確認");
        }

        private void DialogHost_OnDialogClosed(object sender, MaterialDesignThemes.Wpf.DialogClosedEventArgs eventArgs)
        {

        }

        private void DialogHost_OnDialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }
    }
}
