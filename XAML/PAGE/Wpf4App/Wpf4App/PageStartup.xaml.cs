﻿using System;
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
    /// PageStartup.xaml の相互作用ロジック
    /// </summary>
    public partial class PageStartup : Page
    {
        public PageStartup()
        {
            InitializeComponent();
        }


        private void btnPage0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            var page = new UserPage2();
            NavigationService.Navigate(page);

        }

    }
}
