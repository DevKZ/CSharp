using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;


namespace Wpf4App
{
    /// <summary>
    /// PlayWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PlayWindow : Window
    {

        public string mediaURL;

        public PlayWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mediaURL != "")
            {
                mediaElement.Source = new Uri(mediaURL);

                mediaElement.Play();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            this.Close();

        }
    }
}
