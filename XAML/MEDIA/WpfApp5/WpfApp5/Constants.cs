using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Constants
    {
        public const int DivWLevel = 8;
        public const int DivHLevel = 6;

        public const int ScaleMax = 20; // x2.0倍
        public const int ScaleMin = 10; // x1.0倍

        public const double InitialScale = ScaleMax * 0.09;  // 初期サイズ 90%
        public const double ScaleStep = .1; // 拡大縮小ステップ

        public const double SubViewScale = .6;  // メイン画面に対して 60%

        public const double SizeScale = 1.1; 

    }
}
