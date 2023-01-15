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


    /// <summary>
    /// An interface between a windows form instance and its input related events.
    /// Some events include more info in their arguments than the windows form class.
    /// </summary>
    public class FormEventsWrapper
    {
        private Form form;

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

        // Tracking form properties for extra event info
        private Point formPosition;
        private Size formSize;

        /// <summary>Create InputEvents instance with events subscribed to form.</summary>
        public FormEventsWrapper(Form form)
        {
            this.form = form;

            // Initially track some of the form's properties
            formPosition = form.Location;
            formSize = form.Size;

            // Window Events
            form.FormClosing += Form_FormClosing; // WindowClosing
            form.FormClosed += Form_FormClosed; // WindowClosed
            form.GotFocus += Form_GotFocus; // WindowFocused
            form.LostFocus += Form_LostFocus; // WindowUnfocused
            form.Move += Form_Move; // WindowMoved
            form.Resize += Form_Resize; // WindowResized

            // Keyboard Events
            form.KeyDown += Form_KeyDown; // KeyDown
            form.KeyUp += Form_KeyUp; // KeyUp

            // Mouse Events
            form.MouseDoubleClick += Form_MouseDoubleClick; // MouseDoubleClick
            form.MouseDown += Form_MouseDown; // MouseDown
            form.MouseUp += Form_MouseUp; // MouseUp
            form.MouseEnter += Form_MouseEnter; // MouseEnter
            form.MouseLeave += Form_MouseLeave; // MouseLeave
            form.MouseMove += Form_MouseMove; // MouseMove
            form.MouseWheel += Form_MouseWheel; // MouseWheel
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
            Point oldPos = new Point(formPosition.X, formPosition.Y);
            formPosition = form.Location;

            MoveArgs args = new MoveArgs() { oldPosition = oldPos, newPosition = formPosition };
            WindowMoved?.Invoke(sender, args);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            Size oldSize = new Size(formSize.Width, formSize.Height);
            formSize = form.Size;

            ResizeArgs args = new ResizeArgs() { oldSize = oldSize, newSize = formSize };
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
            MouseDoubleClick?.Invoke(sender, e);
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