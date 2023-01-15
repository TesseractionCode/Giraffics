using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GirafficsOld
{
    public partial class Giraffic
    {
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown?.Invoke(this, e);
        }

        private void Form_LostFocus(object sender, EventArgs e)
        {
            OnLostFocus?.Invoke(this, e);
        }

        private void Form_GotFocus(object sender, EventArgs e)
        {
            OnFocus?.Invoke(this, e);
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            Size oldSize = new Size(Width, Height);
            size = new Size(form.Width, form.Height);
            OnResize?.Invoke(this, size - oldSize);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove?.Invoke(this, e);
        }

        private void Form_OnMouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp?.Invoke(this, e);
        }

        private void Form_OnMouseScroll(object sender, MouseEventArgs e)
        {
            OnMouseScroll?.Invoke(this, e);
        }

        private void Form_OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick?.Invoke(this, e);
        }

        private void Form_OnKeyHeld(object sender, KeyPressEventArgs e)
        {
            OnKeyHeld?.Invoke(this, e);
        }

        private void Form_OnKeyReleased(object sender, KeyEventArgs e)
        {
            OnKeyReleased?.Invoke(this, e);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            OnClose?.Invoke(this, e);
            Stop();
        }
    }
}
