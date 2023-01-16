using System.Drawing;
using System.Windows.Forms;

namespace Giraffics
{
    public delegate void PaintDelegate(PaintEventArgs e);

    /// <summary>
    /// The usual windows form, but with double buffering.
    /// </summary>
    class BufferedWindow : Form
    {
        public BufferedWindow(string name, Size size)
        {
            Text = name;
            Size = size;
            DoubleBuffered = true;
        }
    }
}
