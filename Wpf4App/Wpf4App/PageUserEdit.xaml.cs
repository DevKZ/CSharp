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
    /// PageUserEdit.xaml の相互作用ロジック
    /// </summary>
    public partial class PageUserEdit : Page
    {
        public PageUserEdit()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageUserSelect();
            NavigationService.Navigate(page);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegist_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
