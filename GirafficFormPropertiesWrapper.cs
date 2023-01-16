using System.Windows.Forms;
using System.Drawing;
using System;

namespace Giraffics
{
    // A wrapper for relevant non-windowsforms specific methods in the Giraffic BufferedWindow instance. 
    public partial class Giraffic
    {
        public Color BackColor
        {
            get => window.BackColor;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.BackColor = value;
                });
            }
        }

        public double Opacity
        {
            get => window.Opacity;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.Opacity = value;
                });
            }
        }

        public Color TransparencyKey
        {
            get => window.TransparencyKey;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.TransparencyKey = value;
                });
            }
        }

        public bool ShowIcon
        {
            get => window.ShowIcon;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.ShowIcon = value;
                });
            }
        }

        public Icon Icon
        {
            get => window.Icon;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.Icon = value;
                });
            }
        }

        public bool HasMaximizeButton
        {
            get => window.MaximizeBox;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.MaximizeBox = value;
                });
            }
        }

        public bool HasMinimizeButton
        {
            get => window.MinimizeBox;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.MinimizeBox = value;
                });
            }
        }

        public FormBorderStyle BorderStyle
        {
            get => window.FormBorderStyle;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.FormBorderStyle = value;
                });
            }
        }


        public Rectangle DesktopBounds
        {
            get => window.DesktopBounds;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.DesktopBounds = value;
                });
            }
        }

        public Point Position
        {
            get => DesktopBounds.Location;
            set => DesktopBounds = new Rectangle(value, DesktopBounds.Size);
        }

        public int X
        {
            get => Position.X;
            set => Position = new Point(value, Y);
        }

        public int Y
        {
            get => Position.Y;
            set => Position = new Point(X, value);
        }

        public Size MaxSize
        {
            get => window.MaximumSize;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.MaximumSize = value;
                });
            }
        }

        public Size MinSize
        {
            get => window.MinimumSize;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.MinimumSize = value;
                });
            }
        }

        /// <summary>Size of the Giraffic's drawable area.</summary>
        public Size ClientSize
        {
            get => window.ClientSize;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.ClientSize = value;
                });
            }
        }

        /// <summary>Width of the Giraffic's drawable area.</summary>
        public int Width
        {
            get => ClientSize.Width;
            set => ClientSize = new Size(value, Height);
        }

        /// <summary>Height of the Giraffic's drawable area.</summary>
        public int Height
        {
            get => ClientSize.Height;
            set => ClientSize = new Size(Width, value);
        }

        /// <summary>Size of the whole Giraffic window (including border).</summary>
        public Size Size
        {
            get => window.Size;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.Size = value;
                });
            }
        }

        public FormWindowState WindowState
        {
            get => window.WindowState;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.WindowState = value;
                });
            }
        }

        public bool ShowInTaskbar
        {
            get => window.ShowInTaskbar;
            set
            {
                CrossThreadWindowOp(delegate
                {
                    window.ShowInTaskbar = value;
                });
            }
        }
    }
}
