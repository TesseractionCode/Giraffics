using System.Windows.Forms;
using System.Drawing;
using System;

namespace Giraffics
{
    /// <summary>
    /// An interface between a windows form instance and some of its
    /// window-modifying properties. Properties must be accessed from 
    /// a different thread than the one the form is running on.
    /// </summary>
    public class FormPropertiesWrapper
    {
        private Form form;

        public FormPropertiesWrapper(Form form)
        {
            this.form = form;
        }
        
        /// <summary>Execute some delegate on the same thread as the form.
        /// Meant for set operations on form properties. Returns true if success.</summary>
        private bool CrossThreadFormOp(MethodInvoker operation)
        {
            try
            {
                form.Invoke(operation);
                return true;
            }
            catch (ObjectDisposedException) // Abort if form is disposed (window closed).
            {
                return false;
            }
            
        }

        public Color BackColor
        {
            get => form.BackColor;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.BackColor = value;
                });
            }
        }

        public double Opacity
        {
            get => form.Opacity;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.Opacity = value;
                });
            }
        }

        public bool ShowIcon
        {
            get => form.ShowIcon;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.ShowIcon = value;
                });
            }
        }

        public Icon Icon
        {
            get => form.Icon;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.Icon = value;
                });
            }
        }

        public bool HasMaximizeButton
        {
            get => form.MaximizeBox;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.MaximizeBox = value;
                });
            }
        }

        public bool HasMinimizeButton
        {
            get => form.MinimizeBox;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.MinimizeBox = value;
                });
            }
        }

        public FormBorderStyle BorderStyle
        {
            get => form.FormBorderStyle;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.FormBorderStyle = value;
                });
            }
        }


        public Rectangle DesktopBounds
        {
            get => form.DesktopBounds;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.DesktopBounds = value;
                });
            }
        }

        public Size MaxSize
        {
            get => form.MaximumSize;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.MaximumSize = value;
                });
            }
        }

        public Size MinSize
        {
            get => form.MinimumSize;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.MinimumSize = value;
                });
            }
        }

        public Size ClientSize
        {
            get => form.ClientSize;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.ClientSize = value;
                });
            }
        }

        public Size Size
        {
            get => form.Size;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.Size = value;
                });
            }
        }


        public FormWindowState WindowState
        {
            get => form.WindowState;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.WindowState = value;
                });
            }
        }

        public bool ShowInTaskbar
        {
            get => form.ShowInTaskbar;
            set
            {
                CrossThreadFormOp(delegate
                {
                    form.ShowInTaskbar = value;
                });
            }
        }
    }
}
