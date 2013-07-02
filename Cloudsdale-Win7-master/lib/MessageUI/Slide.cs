using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloudsdale.lib.MessageUI
{
    class Slide
    {
        public void SlideLeft(Control ControlToSlide, int SizeOfSlide, int SizeOfObjectX)
        {
            var SlideCount = new Timer();
            SlideCount.Interval = 10;
            int BeginSlide = ControlToSlide.Location.X - SizeOfObjectX;
        }
    }
}
