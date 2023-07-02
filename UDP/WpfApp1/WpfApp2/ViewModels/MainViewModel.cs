using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Net.Sockets;

using WpfApp2.Common;
using WpfApp2.Models;

namespace WpfApp2.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
#if APP_1
        //コマンド
        public ICommand Init_Pushed { get; set; }
        public ICommand Connect_Pushed { get; set; }
#else
        public ICommand Init_Pushed { get; set; }
#endif

        UDP[] udp;

        public MainViewModel()
        {
#if APP_1
            //コマンド
            Init_Pushed = new RelayCommand(Init_Command);
            Connect_Pushed = new RelayCommand(Connect_Command);
#else
            Init_Pushed = new RelayCommand(Init_Command);
#endif
#if APP_1
            udp = new UDP[Constants.SERVER_MAX];
            for (int i = 0; i < Constants.SERVER_MAX; i++)
            {
                udp[i] = new UDP();

            }
            udp[0].Init(1024, "127.0.0.1", 5000);
            udp[1].Init(1025, "127.0.0.1", 5001);
            udp[2].Init(1026, "127.0.0.1", 5002);
            udp[3].Init(1027, "127.0.0.1", 5003);
            udp[4].Init(1028, "127.0.0.1", 5004);
#else
            udp = new UDP[Constants.SERVER_MAX];
            for (int i = 0; i < Constants.SERVER_MAX; i++)
            {
                udp[i] = new UDP();

            }
            udp[0].Init(5000, "127.0.0.1", 1024);

#endif

        }

        public void OnClosing()
        {
            for (int i = 0; i < Constants.SERVER_MAX; i++)
            {
                udp[i].end();
            }
        }


#if APP_1
        //コマンドの処理内容
        private void Connect_Command()
        {
            for (int i = 0; i < Constants.SERVER_MAX; i++)
            {
                udp[i].Process(0);
            }
        }

#endif
        private void Init_Command()
        {
            for (int i = 0; i < Constants.SERVER_MAX; i++)
            {
                udp[i].Process(9000);
            }
        }




        //変数の更新通知用
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
