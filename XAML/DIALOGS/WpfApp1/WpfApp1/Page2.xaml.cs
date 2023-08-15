using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
            var result = await MyMessageBox.Show("ダイアログの説明文です。よろしいですか？", "確認");

            if ((string)result == "OK")
            {
                Debug.Print("OKが押されました。");
            }
            else if ((string)result == "キャンセル")
            {
                Debug.Print("キャンセルが押されました。");
            }
            else
            {
                Debug.Print("ダイアログの外が押されました。");
            }
        }

    }
}
