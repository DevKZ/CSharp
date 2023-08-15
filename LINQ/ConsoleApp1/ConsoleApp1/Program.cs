using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SampleData
    {
        public int Data1 { get; set; }
        public int Data2 { get; set; }
    }
    public class CsvMember
    {
        public DateTime date1 { get; set; }
        public DateTime date2 { get; set; }
        public string item1 { get; set; }
        public string item2 { get; set; }
        public string item3 { get; set; }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // SampleDataクラスのリストの宣言と初期化
            List<SampleData> list = new List<SampleData> {
                new SampleData { Data1 = 11, Data2 = 20, },
                new SampleData { Data1 = 12, Data2 = 20, },
                new SampleData { Data1 = 10, Data2 = 22, },
                new SampleData { Data1 = 10, Data2 = 21, },
                new SampleData { Data1 = 10, Data2 = 20, },
            };

            // 昇順ソート
            var order_list1 = list.OrderBy(d => d.Data1);
            Console.WriteLine("OrderByを使って昇順ソート(キーはData1）");
            foreach (var data in order_list1) Console.WriteLine($"data1={data.Data1}, data2={data.Data2}");

            // 降順ソート
            var order_list2 = list.OrderByDescending(d => d.Data1);
            Console.WriteLine("OrderByDescendingを使って降順ソート(キーはData1）");
            foreach (var data in order_list2) Console.WriteLine($"data1={data.Data1}, data2={data.Data2}");


            // 複数キーでの並べ替え（Data1昇順、Data2昇順）
            var order_list3 = list.OrderBy(d => d.Data1).ThenBy(d => d.Data2);
            Console.WriteLine("OrderByでData1を昇順に並べた後でThenByでData2の昇順に並べる");
            foreach (var data in order_list3) Console.WriteLine($"data1={data.Data1}, data2={data.Data2}");

            // 複数キーでの並べ替え（Data1昇順、Data2降順）
            var order_list4 = list.OrderBy(d => d.Data1).ThenByDescending(d => d.Data2);
            Console.WriteLine("OrderByでData1を昇順に並べた後でThenByDescendingでData2の降順に並べる");
            foreach (var data in order_list4) Console.WriteLine($"data1={data.Data1}, data2={data.Data2}");


            CsvReadSort();  


        }


        static private void CsvReadSort()
        {
            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new StreamReader(@"sample.csv", System.Text.Encoding.GetEncoding("UTF-8"));
            {
                // 配列からリストに格納する
                List<CsvMember> Group = new List<CsvMember>();

                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split(',');


                    CsvMember m = new CsvMember();

                    m.date1 = DateTime.Parse(values[0]);
                    m.date2 = DateTime.Parse(values[1]);
                    m.item1 = values[2];
                    m.item2 = values[3];
                    m.item3 = values[4];

                    Group.RemoveAll(match => (match.item1.Equals(m.item1)) && (match.date2 < m.date2));

                    // 日時降順にするため先頭に追加する
                    Group.Insert(0, m);



                }
                // コンソールに出力する
                foreach (CsvMember list in Group)
                {
                    System.Console.WriteLine("{0} {1} {2} {3} {4} ", list.date1, list.date2, list.item1, list.item2, list.item3);
                }
                System.Console.WriteLine();
                System.Console.ReadKey();

            }
        }
    }
}
