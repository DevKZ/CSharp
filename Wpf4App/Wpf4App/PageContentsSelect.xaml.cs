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
    /// PageContentsSelect.xaml の相互作用ロジック
    /// </summary>
    public partial class PageContentsSelect : Page
    {
        public PageContentsSelect()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageUserSelect();
            NavigationService.Navigate(page);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow f = new PlayWindow();
            f.mediaURL = "";
            f.ShowDialog();

            var page = new PageEnd();
            NavigationService.Navigate(page);
        }
    }
}
