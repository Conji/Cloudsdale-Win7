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
        public delegate void INotifyPropertyChanged 

        protected internal virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            if (ModelSettings.Dispatcher != null && !ModelSettings.Dispatcher.Thread.IsAlive)
            {
                ModelSettings.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                                     handler(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
