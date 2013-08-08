using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Cloudsdale_Win7.Cloudsdale_Lib
{
    public static class ModelSettings
    {
        public static DateTime AppLastSuspended = DateTime.Now;
        public static Dispatcher Dispatcher;
    }
}
