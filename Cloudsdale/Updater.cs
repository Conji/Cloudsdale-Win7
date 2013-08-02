using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Cloudsdale_Win7.Cloudsdale
{
    class Updater : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            if (ModelSettings.Dispatcher != null && !ModelSettings.Dispatcher.Thread.IsAlive)
            {
                ModelSettings.Dispatcher.InvokeAsync(() => handler(this, new PropertyChangedEventArgs(propertyname)), DispatcherPriority.Normal);
            }
            else
            {
                handler(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
