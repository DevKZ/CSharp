using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = vm;

            this.MyTabControl.SelectedIndex = 0;

        }


        protected virtual void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxResult.Yes != MessageBox.Show("画面を閉じます。よろしいですか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Information))
            {
                e.Cancel = true;
                return;
            }
            vm.OnClosing();

        }

    }
}
