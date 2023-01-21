using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Numerics;

using Giraffics;
using Giraffics.Utilities.Game;

namespace TestSpace
{
    class Program
    {
        static float x = 0;
        static float y = 0;
        static float speedX = 100;
        static float speedY = 100;

        static void Main(string[] args)
        {
            Giraffic giraffic = new Giraffic("My Giraffic", new Size(800, 600), FormStartPosition.CenterScreen);

            // Configure the Giraffic
            giraffic.MinSize = new Size(250, 100);
            giraffic.BackColor = Color.FromArgb(240, 240, 240);
            giraffic.BackColor = Color.Black;

            EventListener events = new EventListener(giraffic);
            GameLoop gameLoop = new GameLoop(giraffic, events, Update, Render);
            gameLoop.Start();
        }

        private static void Update(EventListener events, float dT, float elapsedTime)
        {
            x = elapsedTime * 5 * (float)Math.Cos(100 * elapsedTime) + 400;
            y = elapsedTime * 5 * (float)Math.Sin(100 * elapsedTime) + 300;
        }

        private static void Render(Giraffic giraffic)
        {
            //giraffic.Clear();
            giraffic.FillCircle(Color.Teal, (int)x + 100, (int)y, 2);
            giraffic.FillCircle(Color.IndianRed, (int)x + 200, (int)y + 100, 2);
            giraffic.FillCircle(Color.AliceBlue, (int)x, (int)y + 100, 2);
        }
    }
}
