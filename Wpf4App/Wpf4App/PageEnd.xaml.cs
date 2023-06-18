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
    /// PageEnd.xaml の相互作用ロジック
    /// </summary>
    public partial class PageEnd : Page
    {
        public PageEnd()
        {
            InitializeComponent();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            var page = new PageHistory();
            Application.Current.Properties["backpage"] = "PageEnd";
            NavigationService.Navigate(page);
        }
    }
}
