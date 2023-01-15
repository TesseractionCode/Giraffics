using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System;

namespace Giraffics
{
    /// <summary>
    /// A fancy modular window that runs in another thread. A Giraffic is 
    /// functionally a dynamically paintable canvas derived from Windows Forms.
    /// </summary>
    class Giraffic
    {
        // Private Giraffic instance variables
        private Thread windowThread;
        private BufferedWindow window;

        // Public instance variables
        public FormEventsWrapper winEvents;
        public FormPropertiesWrapper winProperties;
        public bool isRunning = false;


        public Giraffic(string name, Size size, FormStartPosition startPos = FormStartPosition.WindowsDefaultLocation)
        {
            // Setup window in another thread
            windowThread = new Thread(() => InitializeWindow(name, size, startPos));
            windowThread.Start();
            
            // Wait until the window is completely constructed before return.
            while (window == null || !window.IsHandleCreated)
                Thread.Sleep(10);
            
        }

        /// <summary>Stop the Giraffic and destroy its window. Cannot be started again.</summary>
        public void Close()
        {
            isRunning = false;
            CrossThreadWindowOp(delegate
            {
                window.Close();
            });
        }

        /// <summary>Show the Giraffic window.</summary>
        public void Show()
        {
            CrossThreadWindowOp(delegate
            {
                window.Show();
            });
        }

        /// <summary>Hide the Giraffic window.</summary>
        public void Hide()
        {
            CrossThreadWindowOp(delegate
            {
                window.Hide();
            });
        }

        /// <summary>Create the Giraffic window and all of its necessary cross-thread
        /// interfaces, then run the application.</summary>
        private void InitializeWindow(string name, Size size, FormStartPosition startPos)
        {
            window = new BufferedWindow(name, size);
            window.StartPosition = startPos;

            // Establish all window-related and user input events
            winEvents = new FormEventsWrapper(window);
            window.Paint += WindowPainter;

            // Create a cross-thread accessible wrapper of the window properties
            winProperties = new FormPropertiesWrapper(window);

            // Make sure the window closes properly
            winEvents.WindowClosing += delegate { if (isRunning) Close(); }; // Properly dispose window and end thread.

            isRunning = true;
            Application.Run(window);
        }

        /// <summary>Draws the actual pixels onto the window.</summary>
        private void WindowPainter(object sender, PaintEventArgs e)
        {
            /*Canvas canvas = new Canvas(e.Graphics);
            canvas.DrawRect(Color.Black, 50, 50, 400, 150, 3);*/
        }

        /// <summary>Execute some delegate on the same thread as the window.
        /// Meant for set operations on window properties. Returns true if success.</summary>
        private bool CrossThreadWindowOp(MethodInvoker operation)
        {
            try
            {
                window.Invoke(operation);
                return true;
            }
            catch (ObjectDisposedException) // Abort if window is disposed (closed).
            {
                return false;
            }

        }
    }
}
