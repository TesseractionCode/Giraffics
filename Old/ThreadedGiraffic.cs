using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace GirafficsOld
{
    class ThreadedGiraffic
    {
        public Giraffic giraffic;
        private Thread girafficThread;

        public ThreadedGiraffic(string title, Size size)
        {
            giraffic = new Giraffic(title, size);

            //giraffic.OnClose += ThreadStopper;

            girafficThread = new Thread(ThreadRunner);
            girafficThread.Start();

            while (!giraffic.form.IsHandleCreated)
                Thread.Sleep(100);
        }

        private void ThreadStopper(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            girafficThread.Abort();
        }

        private void ThreadRunner()
        {
            giraffic.Start();
        }
    }
}
