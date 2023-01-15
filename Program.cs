using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.ComponentModel;

namespace Giraffics
{
    class Program
    {
        static Giraffic giraffic;
        static void Main(string[] args)
        {
            giraffic = new Giraffic("Threaded Giraffic", new Size(800, 600));

            giraffic.winEvents.WindowMoved += WinEvents_WindowMoved;

            int r = (int)(255 * (double)Clamp(giraffic.winProperties.DesktopBounds.X, 0, 2800) / 2800);
            int g = (int)(255 * (double)Clamp(giraffic.winProperties.DesktopBounds.Y, 0, 1000) / 1500);
            int b = (int)(255 * (0.5 + Math.Sin((double)giraffic.winProperties.DesktopBounds.X / 200) / 2));

            giraffic.winProperties.BackColor = Color.FromArgb(r, g, b);

            giraffic.winProperties.Icon = SystemIcons.Information;




            /*double dT = 0;
            double t = 0;
            double amp = 255;
            double freq = 1;
            while (giraffic.isRunning)
            {
                DateTime startTime = DateTime.Now;
                double val = amp * (Math.Sin(freq * 2 * Math.PI * t) + 1) / 2;

                giraffic.winProps.BackColor = Color.FromArgb((int)val, (int)val, (int)val);
                
                t += dT;
                dT = (double)(DateTime.Now - startTime).Milliseconds / 1000;
            }*/

            //Console.ReadKey();
        }

        private static int Clamp(int val, int min, int max)
        {
            return Math.Min(max, Math.Max(min, val));
        }

        private static void WinEvents_WindowMoved(object sender, MoveArgs e)
        {
            
            int r = (int)(255 * (double)Clamp(e.newPosition.X, 0, 2800) / 2800);
            int g = (int)(255 * (double)Clamp(e.newPosition.Y, 0, 1000) / 1500);
            int b = (int)(255 * (0.5 + Math.Sin((double)e.newPosition.X / 200)/2));

            giraffic.winProperties.BackColor = Color.FromArgb(r, g, b);
        }
    }
}
