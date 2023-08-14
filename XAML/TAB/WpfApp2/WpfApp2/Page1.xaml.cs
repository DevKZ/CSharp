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
        public class TabItem
        {
            public string Header { get; set; }
            public ObservableCollection<Person> Content { get; set; }
        }
        public class Person
        {
            public bool Selected { get; set; }
            public string name { get; set; }
            public int age { get; set; }
        }
        
        public ObservableCollection<TabItem> Tabs { get; set; }

        public Page1()
        {
            InitializeComponent();


            Tabs = new ObservableCollection<TabItem>() {
                new TabItem {
                    Header = "Header1",
                    Content = new ObservableCollection<Person>()
                    {
                        new Person{Selected = false, name = "hoge1" , age = 20},
                        new Person{Selected = false, name = "hoge2" , age = 21},
                        new Person{Selected = false, name = "hoge3" , age = 22},
                        new Person{Selected = false, name = "hoge4" , age = 23},
                        new Person{Selected = false, name = "hoge5" , age = 24},
                        new Person{Selected = false, name = "hoge6" , age = 25},
                        new Person{Selected = false, name = "hoge7" , age = 26},
                        new Person{Selected = false, name = "hoge8" , age = 27},
                    }
                },
                new TabItem {
                    Header = "Header2",
                    Content = new ObservableCollection<Person>()
                    {
                        new Person{Selected = false, name = "fuga1" , age = 30},
                        new Person{Selected = false, name = "fuga2" , age = 31},
                        new Person{Selected = false, name = "fuga3" , age = 32},
                    }
                }
            };
            tabcontrol.ItemsSource = Tabs;
        }



        private void BtnItemAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTabRen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnItemDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }

}
