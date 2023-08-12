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

        // Scale 整数化のため10倍
        public const int ScaleMax = 20; // x2.0倍
        public const int ScaleMin = 10; // x1.0倍

        public const int InitialScale = 18;  // 初期サイズ 90%(=20*0.9)
        public const int ScaleStep = 1; // 拡大縮小ステップ

        public const int SubViewWidth = 320;
        public const int SubViewHeight = 180;
    }
}
