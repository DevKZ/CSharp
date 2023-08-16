using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    public enum MyMessageBoxButton
    {
        OK = 0,
        OKCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
    }    /// <summary>
         /// MyMessageBox.xaml の相互作用ロジック
         /// </summary>
    public partial class MyMessageBox : UserControl
    {

        public MyMessageBox(MyMessageBoxButton button)
        {
            InitializeComponent();

            PanelOK.Visibility = Visibility.Collapsed;
            PanelOKCancel.Visibility = Visibility.Collapsed;
            PanelAbortRetryIgnore.Visibility = Visibility.Collapsed;
            PanelYesNoCancel.Visibility = Visibility.Collapsed;
            PanelYesNo.Visibility = Visibility.Collapsed;

            switch (button)
            {
                case MyMessageBoxButton.OK:
                    PanelOK.Visibility = Visibility.Visible;
                    break;
                case MyMessageBoxButton.OKCancel:
                    PanelOKCancel.Visibility = Visibility.Visible;
                    break;
                case MyMessageBoxButton.AbortRetryIgnore:
                    PanelAbortRetryIgnore.Visibility = Visibility.Visible;
                    break;
                case MyMessageBoxButton.YesNoCancel:
                    PanelYesNoCancel.Visibility = Visibility.Visible;
                    break;
                case MyMessageBoxButton.YesNo:
                    PanelYesNo.Visibility = Visibility.Visible;
                    break;
                default:
                    break;

            }
        }

        public static Task<object> Show(string text, string caption, MyMessageBoxButton button)
        {
            var dialog = new MyMessageBox(button);
            dialog.DataContext = new { DialogTitle = caption, DialogText = text };
            return DialogHost.Show(dialog);
        }
    }
}
