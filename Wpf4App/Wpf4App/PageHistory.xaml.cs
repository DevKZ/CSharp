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
    /// PageHistory.xaml の相互作用ロジック
    /// </summary>
    public partial class PageHistory : Page
    {
        public PageHistory()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var a = Application.Current.Properties["backpage"];//), "PageStartup"))
            string b = Application.Current.Properties["backpage"] as string;//), "PageStartup"))
            
            if (b.Equals("PageStartup"))
            {
                //if (frame.CanGoBack) contentFrame.GoBack();
                var page = new PageStartup();
                NavigationService.Navigate(page);
            } else if (b.Equals("PageEnd"))
            {
                //if (frame.CanGoBack) contentFrame.GoBack();
                var page = new PageEnd();
                NavigationService.Navigate(page);
            }
        }
    }
}
