using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace GirafficsOld
{
    public partial class Giraffic
    {
        public int Width
        {
            get => size.Width;
            set
            {
                Size oldSize = new Size(Width, Height);
                size.Width = value;
                form.Width = value;
                OnResize?.Invoke(this, size - oldSize);
            }
        }

        public int Height
        {
            get => size.Height;
            set
            {
                Size oldSize = new Size(Width, Height);
                size.Height = value;
                form.Height = value;
                OnResize?.Invoke(this, size - oldSize);
            }
        }

        public bool Visible
        {
            get => form.Visible;
            set => form.Visible = value;
        }

        public bool Resizable
        {
            
            get => resizable;
            set
            {
                form.Invoke((MethodInvoker)delegate
                {
                    if (value)
                    {
                        form.MinimumSize = Size.Empty;
                        form.MaximumSize = new Size(int.MaxValue, int.MaxValue);
                    }
                    else
                    {
                        form.MinimumSize = size;
                        form.MaximumSize = size;
                    }
                    form.MaximizeBox = value;
                    resizable = value;
                });
            }
        }

        public Icon Icon
        {
            get => form.Icon;
            set => form.Icon = value;
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                form.Text = value;
            }
        }
    }
}
