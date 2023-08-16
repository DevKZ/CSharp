using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Page2.xaml の相互作用ロジック
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int b = (int)MyMessageBoxButton.OK; b <= (int)MyMessageBoxButton.YesNo; b++)
            {
                var result = await MyMessageBox.Show("ダイアログの説明文です。よろしいですか？", "確認", (MyMessageBoxButton)b);

                if (((string)result == "OK") ||
                    ((string)result == "Cancel") ||
                    ((string)result == "Yes") ||
                    ((string)result == "No") ||
                    ((string)result == "Abort") ||
                    ((string)result == "Retry") ||
                    ((string)result == "Ignore")
                    )
                {
                    Debug.Print((string)result + "が押されました。");

                }
                else
                {
                    Debug.Print("ダイアログの外が押されました。");
                }

            }

        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }
    }
}
