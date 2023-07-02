using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppXaml7
{
    /// MainWindow.xaml の相互作用ロジック
    /// https://blog.xin9le.net/entry/2013/10/27/195614
    /// </summary>
    public static class MenuItemExtensions
    {
        public static void PerformClick(this MenuItem menuitem)
        {
            if (menuitem == null)
                throw new ArgumentNullException("menuitem");

            var provider = new MenuItemAutomationPeer(menuitem) as IInvokeProvider;
            provider.Invoke();
        }
    }

    public static class MenuExtensions
    {
        public static void PerformClick(this Menu menu)
        {
            if (menu == null)
                throw new ArgumentNullException("menu");

            var provider = new MenuAutomationPeer(menu) as IInvokeProvider;
            provider.Invoke();
        }
    }
    public static class ButtonExtensions
    {
        public static void PerformClick(this Button button)
        {
            if (button == null)
                throw new ArgumentNullException("button");

            var provider = new ButtonAutomationPeer(button) as IInvokeProvider;
            provider.Invoke();
        }
    }

    public partial class MainWindow : Window
    {
        /// <summary>


        public MainWindow()
        {
            InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItemExtensions.PerformClick(HBGMNUITEM);

        }

        private void BTNTEST_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
