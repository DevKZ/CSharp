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

namespace Wpf4App
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri uri = new Uri("/PageStartup.xaml", UriKind.Relative);
            frame.Source = uri;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void CheckBoxVisible_Checked(object sender, RoutedEventArgs e)
        {
            frame.NavigationUIVisibility = NavigationUIVisibility.Visible;

        }

        private void CheckBoxAuto_Checked(object sender, RoutedEventArgs e)
        {
            frame.NavigationUIVisibility = NavigationUIVisibility.Automatic;

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

        }
    }
}
