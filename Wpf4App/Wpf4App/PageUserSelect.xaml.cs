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
    /// PageUserSelect.xaml の相互作用ロジック
    /// </summary>
    public partial class PageUserSelect : Page
    {
        public PageUserSelect()
        {
            InitializeComponent();
        }

        private void btnUserConfirm_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageContentsSelect();
            NavigationService.Navigate(page);
        }

        private void btnUserEdit_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageUserEdit();
            NavigationService.Navigate(page);
        }
    }
}
