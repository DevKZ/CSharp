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
    /// UserPage2.xaml の相互作用ロジック
    /// </summary>
    public partial class UserPage2 : Page
    {
        public UserPage2()
        {
            InitializeComponent();
        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageStartup();
            NavigationService.Navigate(page);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
