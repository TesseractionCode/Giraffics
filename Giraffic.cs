using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Giraffics
{
    /// <summary>
    /// A fancy modular window that runs in another thread. A Giraffic is 
    /// functionally a dynamically paintable canvas which acts as a wrapper for
    /// the Windows Forms class and Graphics class for simplicity of drawing.
    /// </summary>
    public partial class Giraffic : Canvas
    {
        // Private Giraffic instance variables
        private Thread windowThread;
        private BufferedWindow window;
        //private Bitmap bitmap;
        //private Graphics graphics; // This is set to a new instance after every paint.
        private SizeF oldGraphicsSize; // Keep track of 

        // Public instance variables
        public bool IsRunning { get; protected set; }
        public bool isAntiAlias;

        /// <summary>
        /// A fancy modular window that runs in another thread. A Giraffic is 
        /// functionally a dynamically paintable canvas which acts as a wrapper for
        /// the Windows Forms class and Graphics class for simplicity of drawing.
        /// </summary>
        public Giraffic(string name, Size size, FormStartPosition startPos = FormStartPosition.WindowsDefaultLocation): base(size)
        {
            // Setup window in another thread
            windowThread = new Thread(() => InitializeWindow(name, size, startPos));
            windowThread.Start();
            
            // Enable anti-aliasing by default.
            isAntiAlias = true;

            // Create a bitmap and a graphics object to help draw on it.
            /*bitmap = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;*/
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            oldGraphicsSize = graphics.VisibleClipBounds.Size;

            IsRunning = true;

            // Wait until the window is completely constructed before return.
            while (window == null || !window.IsHandleCreated)
                Thread.Sleep(10);
        }

        /// <summary>Stop the Giraffic and destroy its window. Cannot be started again.</summary>
        public void Close()
        {
            IsRunning = false;
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

        /// <summary>Force the Giraffic window to display what's on the canvas.</summary>
        public void Refresh()
        {
            CrossThreadWindowOp(delegate
            {
                window.Refresh();
            });
        }

        /// <summary>Create the Giraffic window and all of its necessary cross-thread
        /// interfaces, then run the application.</summary>
        private void InitializeWindow(string name, Size size, FormStartPosition startPos)
        {
            window = new BufferedWindow(name, size);
            window.StartPosition = startPos;
            
            // Add some pazzaz
            window.BackColor = Color.FromArgb(240, 240, 240);
            window.Icon = new Icon(typeof(Giraffic), "giraffe.ico"); // Made by Rfourtytwo MUST CREDIT
            
            // Establish all window-related and user input events
            ListenToWindowEvents();
            window.Paint += WindowPainter;

            // Make sure the window closes properly
            WindowClosing += delegate { if (IsRunning) Close(); }; // Properly dispose window and end thread.
            
            IsRunning = true;
            Application.Run(window);
        }

        /// <summary>Draws the bitmap onto the window.</summary>
        private void WindowPainter(object sender, PaintEventArgs e)
        {
            // Reset the bitmap's size to fit the screen if the screen's size has changed.
            if (oldGraphicsSize != e.Graphics.VisibleClipBounds.Size)
                ChangeSize(window.Size);
            oldGraphicsSize = e.Graphics.VisibleClipBounds.Size;
            
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        /// <summary>Execute some delegate on the same thread as the window.
        /// Meant for set operations on window properties. Returns true if success.</summary>
        private bool CrossThreadWindowOp(MethodInvoker operation)
        {
            if (!window.IsHandleCreated || window.IsDisposed)
                return false;
            try
            {
                window.Invoke(operation);
            } catch (ObjectDisposedException) { return false; }
            
            return true;
        }
    }
}
