using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Common
{
    class Constants
    {
#if APP_1
        public const int SERVER_MAX = 5;
        public const int SELF_PORT = 1024;

#else
        public const int SERVER_MAX = 1;
        public const int SELF_PORT = 51011;
#endif
    }
}
