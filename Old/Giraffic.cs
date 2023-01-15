using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace GirafficsOld
{
    public partial class Giraffic
    {
        /// <summary>Use this to draw onto the Giraffic graphics.</summary>
        public delegate void Painter(Graphics g);

        public SmoothingMode smoothingMode;
        /// <summary>The minimum amount of time in milliseconds between each paint. This shouldn't be too small.</summary>
        public int minRefreshTime = 15;

        public BufferedForm form;
        private string title;
        private Size size;
        private bool resizable;
        private bool isStopped = false;
        private Bitmap bitmap;
        private DateTime lastRenderTime;

        // Input Events
        public event EventHandler<KeyPressEventArgs> OnKeyHeld;
        public event EventHandler<KeyEventArgs> OnKeyReleased;
        public event EventHandler<MouseEventArgs> OnMouseUp;
        public event EventHandler<MouseEventArgs> OnMouseDown;
        public event EventHandler<MouseEventArgs> OnMouseDoubleClick;
        public event EventHandler<MouseEventArgs> OnMouseScroll;
        public event EventHandler<MouseEventArgs> OnMouseMove;

        // Other events
        /// <summary>Invoked when the Giraffic window size is changed. Passes the change in size.</summary>
        public event EventHandler<Size> OnResize;
        public event EventHandler<EventArgs> OnStart;
        public event EventHandler<FormClosingEventArgs> OnClose;
        public event EventHandler<EventArgs> OnPaint;
        public event EventHandler<EventArgs> OnFocus;
        public event EventHandler<EventArgs> OnLostFocus;

        public Giraffic(string title, Size size)
        {
            this.title = title;
            this.size = size;

            

            form = new BufferedForm(title, size);
            form.Paint += new PaintEventHandler(Renderer);

            // Setup some important events
            form.FormClosing += Form_Closing;

            // Input events
            form.KeyPress += Form_OnKeyHeld;
            form.KeyUp += Form_OnKeyReleased;
            form.MouseDoubleClick += Form_OnMouseDoubleClick;
            form.MouseWheel += Form_OnMouseScroll;
            form.MouseUp += Form_OnMouseUp;
            form.MouseDown += Form_MouseDown;
            form.MouseMove += Form_MouseMove;
            form.Resize += Form_SizeChanged;
            form.GotFocus += Form_GotFocus;
            form.LostFocus += Form_LostFocus;

            smoothingMode = SmoothingMode.AntiAlias;
            
            // Make resizable
            form.MinimumSize = Size.Empty;
            form.MaximumSize = new Size(int.MaxValue, int.MaxValue);

            
        }

        /// <summary>Start the Giraffic.</summary>
        public void Start()
        {
            lastRenderTime = DateTime.Now;

            OnStart?.Invoke(this, EventArgs.Empty);

            try
            {
                Application.Run(form);
            }
            catch (ThreadAbortException)
            {
                Console.Error.WriteLine("Giraffic stopped before creation finished.");
            }

        }

        /// <summary>Stop the Giraffic and all its threads.</summary>
        public void Stop()
        {

            form.Dispose();
            isStopped = true;
        }

        /// <summary>Paints onto the Giraffic with a delegate. Clears the previous paint. This function should
        /// ideally be called as minimally as possible.</summary>
        /// <param name="painter">The delegate responsible for painting onto the Giraffic before the refresh.</param>
        public void Paint(Painter painter)
        {
            // Make sure the Paint method isn't executed too quicky.
            double dT = (float)(DateTime.Now - lastRenderTime).TotalMilliseconds;
            if (dT < minRefreshTime)
                Thread.Sleep((int)(minRefreshTime - dT));

            if (isStopped)
            {
                global::System.Console.WriteLine("Cannot paint to the Giraffic after the Giraffic is closed.");
                return;
            }

            // Modify the bitmap with the painter
            bitmap = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bitmap);
            painter(g);
            g.Dispose();

            // Tell the renderer to do its thing
            form.Refresh();

            OnPaint?.Invoke(this, EventArgs.Empty);

            lastRenderTime = DateTime.Now;
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = smoothingMode;
            
            // Draw the bitmap created by the painter
            if (bitmap != null)
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }
    }
}
