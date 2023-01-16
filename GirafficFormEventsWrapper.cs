using System;
using System.Drawing;
using System.Windows.Forms;

namespace Giraffics
{
    // Custom Event Args
    public class MoveArgs : EventArgs
    {
        public Point oldPosition;
        public Point newPosition;
    }

    public class ResizeArgs : EventArgs
    {
        public Size oldSize;
        public Size newSize;
    }


    // A digestible wrapper for the BufferedWindow's input-related events.
    public partial class Giraffic
    {
        // Window Events
        public event FormClosingEventHandler WindowClosing;
        public event FormClosedEventHandler WindowClosed;
        public event EventHandler WindowFocused;
        public event EventHandler WindowUnfocused;
        public event EventHandler<MoveArgs> WindowMoved;
        public event EventHandler<ResizeArgs> WindowResized;

        // Keyboard Events
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        // Mouse Events
        public event MouseEventHandler MouseDoubleClick;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;

        // Tracking window properties for extra event info
        private Point windowPosition;
        private Size windowSize;

        private void ListenToWindowEvents()
        {
            // Initially track some of the window's properties
            windowPosition = window.Location;
            windowSize = window.Size;

            // Window Events
            window.FormClosing += Form_FormClosing; // WindowClosing
            window.FormClosed += Form_FormClosed; // WindowClosed
            window.GotFocus += Form_GotFocus; // WindowFocused
            window.LostFocus += Form_LostFocus; // WindowUnfocused
            window.Move += Form_Move; // WindowMoved
            window.Resize += Form_Resize; // WindowResized

            // Keyboard Events
            window.KeyDown += Form_KeyDown; // KeyDown
            window.KeyUp += Form_KeyUp; // KeyUp

            // Mouse Events
            window.MouseDoubleClick += Form_MouseDoubleClick; // MouseDoubleClick
            window.MouseDown += Form_MouseDown; // MouseDown
            window.MouseUp += Form_MouseUp; // MouseUp
            window.MouseEnter += Form_MouseEnter; // MouseEnter
            window.MouseLeave += Form_MouseLeave; // MouseLeave
            window.MouseMove += Form_MouseMove; // MouseMove
            window.MouseWheel += Form_MouseWheel; // MouseWheel
        }
        
        #region Window Events

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowClosing?.Invoke(sender, e);
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowClosed?.Invoke(sender, e);
        }

        private void Form_GotFocus(object sender, EventArgs e)
        {
            WindowFocused?.Invoke(sender, e);
        }

        private void Form_LostFocus(object sender, EventArgs e)
        {
            WindowUnfocused?.Invoke(sender, e);
        }

        private void Form_Move(object sender, EventArgs e)
        {
            Point oldPos = new Point(windowPosition.X, windowPosition.Y);
            windowPosition = window.Location;

            MoveArgs args = new MoveArgs() { oldPosition = oldPos, newPosition = windowPosition };
            WindowMoved?.Invoke(sender, args);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Size oldSize = new Size(windowSize.Width, windowSize.Height);
            windowSize = window.Size;

            ResizeArgs args = new ResizeArgs() { oldSize = oldSize, newSize = windowSize };
            WindowResized?.Invoke(sender, args);
        }
        #endregion

        
        #region Keyboard Events

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }
        #endregion


        #region Mouse Events

        private void Form_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClick?.Invoke(sender, e);
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(sender, e);
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(sender, e);
        }

        private void Form_MouseEnter(object sender, System.EventArgs e)
        {
            MouseEnter?.Invoke(sender, e);
        }

        private void Form_MouseLeave(object sender, System.EventArgs e)
        {
            MouseLeave?.Invoke(sender, e);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheel?.Invoke(sender, e);
        }
        #endregion
    }
}