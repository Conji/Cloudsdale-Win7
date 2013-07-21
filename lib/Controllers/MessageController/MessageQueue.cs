using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudsdale.lib.MessageController
{
    public class MessageQueue
    {
        public static readonly Queue<string> Queue = new Queue<string>();
        public static readonly Timer Timer = new Timer(o =>
        {
            if (Queue.Count > 0)
            {
                var txt = Queue.Dequeue();
            }
        }, null, 1000, 100);
    }
}
