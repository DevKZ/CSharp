using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp2
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();


            Tabs = new ObservableCollection<TabItem>() {
                new TabItem {
                    Header = "Header1",
                    Content = new ObservableCollection<abc>()
                    {
                        new abc{ name = "content1" , nickname = "nick1"},
                        new abc { name = "content2", nickname = "nick2" },
                    }
                },
                new TabItem {
                    Header = "Header2",
                    Content = new ObservableCollection<abc>()
                    {
                        new abc{ name = "content3", nickname = "nick3" },
                        new abc{ name = "content4", nickname = "nick4" },
                    }
                }
            };
            tabcontrol.ItemsSource = Tabs;
        }

        public ObservableCollection<TabItem> Tabs { get; set; }
        public class TabItem
        {
            public string Header { get; set; }
            public ObservableCollection<abc> Content { get; set; }
        }
        public class abc
        {
            public string name { get; set; }
            public string nickname { get; set; }
        }
    }

}
