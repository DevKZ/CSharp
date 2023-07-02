using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

using WpfApp1.Common;

namespace WpfApp1.Models
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Text;

    public class UDP
    {

        public struct stConnectInf
        {
            public int self_port;
            public int dest_port;
            public string dest_ip;
        }

        DataStore datastore;
        UdpClient client;
        stConnectInf connectinf;


        public UDP()
        {
            datastore = new DataStore();

        }

        public void Init(int self_port, string dest_ip, int dest_port)
        { 
            connectinf.self_port = self_port;
            connectinf.dest_port = dest_port;
            connectinf.dest_ip = dest_ip;

        }

        public void Process(int prcsidx)
        {
            switch (prcsidx)
            {
                case 0:
                    //seqid=xx;
                    send();
                    break;
                case 1:
                    //seqid=xx;
                    send();
                    break;


                case 9000:  //rcv
                    receive();
                    break;
                default:
                    break;

            }
        }

        private void send()
        {
            client = new UdpClient(connectinf.self_port);   //送信用ポート

            datastore.sendBytes = Encoding.ASCII.GetBytes("1");

            client.BeginSend(
                datastore.sendBytes,
                datastore.sendBytes.Length,
                connectinf.dest_ip, connectinf.dest_port,
                SendCallback, client);
        }
        private void SendCallback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp =
                (System.Net.Sockets.UdpClient)ar.AsyncState;

            //非同期送信を終了する
            try
            {
                udp.EndSend(ar);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("送信エラー({0}/{1})",
                    ex.Message, ex.ErrorCode);
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                Console.WriteLine("Socketは閉じられています。");
            }

        }

        private void receive()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, connectinf.self_port);

            client = new System.Net.Sockets.UdpClient(ep);
            //非同期的なデータ受信を開始する
            client.BeginReceive(OnReceive, client);
        }

        void OnReceive(IAsyncResult ar)
        {

            System.Net.Sockets.UdpClient udp =
                (System.Net.Sockets.UdpClient)ar.AsyncState;

            //非同期受信を終了する
            System.Net.IPEndPoint remoteEP = null;
            byte[] rcvBytes;
            try
            {
                rcvBytes = udp.EndReceive(ar, ref remoteEP);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("受信エラー({0}/{1})",
                    ex.Message, ex.ErrorCode);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                //すでに閉じている時は終了
                Console.WriteLine("Socketは閉じられています。");
                return;
            }
        }


        public void end() //送受信用ポートを閉じつつ受信用スレッドも廃止
        {
            try
            {
            }
            catch { }
        }
    }


}
