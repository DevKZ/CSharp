using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
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
                        new abc{ text = "content1" },
                        new abc { text = "content2" },
                    }
                },
                new TabItem
                {
                    Header = "Header2",
                    Content = new ObservableCollection<abc>()
                    {
                        new abc{ text = "content3" },
                        new abc{ text = "content4" },
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
            public string text { get; set; }
        }
    }

}
