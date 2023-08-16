using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// InputDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class InputDlg : UserControl
    {

        public InputDlg()
        {
            InitializeComponent();
        }

        public static Task<object> Show(string text, string caption)
        {
            var dialog = new InputDlg();
            dialog.DataContext = new { DialogTitle = caption, DialogText = text };
            return DialogHost.Show(dialog);
        }

    }
}
