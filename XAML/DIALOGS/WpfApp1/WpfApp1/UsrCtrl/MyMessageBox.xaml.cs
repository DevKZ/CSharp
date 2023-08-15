using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// MyMessageBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MyMessageBox : UserControl
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }

        public static Task<object> Show(string text, string caption)
        {
            var dialog = new MyMessageBox();
            dialog.DataContext = new { DialogTitle = caption, DialogText = text };
            return DialogHost.Show(dialog);
        }
    }
}
