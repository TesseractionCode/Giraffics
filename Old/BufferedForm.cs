using System.Windows.Forms;
using System.Drawing;

namespace GirafficsOld
{
    public class BufferedForm : Form
    {
        public BufferedForm(string title, Size size)
        {
            Text = title;
            Size = size;
            DoubleBuffered = true;
        }
    }
}
