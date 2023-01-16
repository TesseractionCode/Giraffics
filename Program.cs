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

namespace Giraffics
{
    class Planet
    {
        public Vector2 pos;
        public Vector2 vel = Vector2.Zero;
        public Vector2 accel = Vector2.Zero;
        public double r;
        public Color color = Color.Yellow;
        public double mass;

        private Vector2 forces = Vector2.Zero;

        private static List<Planet> planets = new List<Planet>();
        private static object lockObj = new object();

        public static double G = 100;

        public Planet(Vector2 pos, double r)
        {
            this.pos = pos;
            this.r = r;
            mass = r * r;

            planets.Add(this);
        }

        public static void UpdateAll(double dT)
        {
            foreach (Planet planet in planets)
            {
                planet.Update(dT);
            }
        }

        public static void RenderAll(Giraffic giraffic)
        {
            foreach (Planet planet in planets)
            {
                planet.Render(giraffic);
            }
        }

        public void Update(double dT)
        {
            foreach (Planet planet in planets)
            {
                if (this == planet)
                    continue;

                double d_squared = Vector2.DistanceSquared(pos, planet.pos);
                double F = G * mass * planet.mass / d_squared;
                
                ApplyForce((float)F * Vector2.Normalize(planet.pos - pos));
            }

            accel = forces / (float)mass;
            vel += accel * (float)dT;
            pos += vel * (float)dT;

            forces *= 0;
        }

        public void Render(Giraffic giraffic)
        {
            giraffic.FillCircle(Color.FromArgb(100, 255, 0, 200), (int)pos.X - giraffic.X, (int)pos.Y - giraffic.Y, (int)r);
        }

        public void ApplyForce(Vector2 force)
        {
            forces += force;
        }
    }

    class Program
    {
        static Giraffic giraffic;

        static Point[] starPositions;
        static int[] starRadii;
        static double[] starBrightnesses;

        static DateTime startTime;
        static Random random;

        static bool mouseUp = false;
        static Point mousePos;

        static void Main(string[] args)
        {
            giraffic = new Giraffic("Spacey Wacey", new Size(800, 600));
            random = new Random();

            // Immediate-mode GUI
            giraffic.Icon = new Icon("F:\\Icons\\giraffeIcon.ico");
            giraffic.BackColor = Color.FromArgb(5, 5, 5);

            giraffic.WindowState = FormWindowState.Maximized;
            GenStarSeed(0);
            giraffic.WindowState = FormWindowState.Normal;

            giraffic.MouseUp += Giraffic_MouseUp;


            new Thread((ThreadStart)delegate
            {
                DateTime loopTime = DateTime.Now;
                Font myFont = new Font(FontFamily.GenericSansSerif, 20);
                double dT = 0;
                double fps = 1;
                while (giraffic.IsRunning)
                {
                    DateTime startTime = DateTime.Now;

                    if (mouseUp)
                    {
                        MakeRandomPlanet(mousePos);
                        mouseUp = false;
                    }

                    giraffic.Clear();
                    DrawStars();
                    Planet.UpdateAll(dT);
                    Planet.RenderAll(giraffic);

                    if ((DateTime.Now - loopTime).TotalMilliseconds >= 500)
                    {
                        fps = (int)(1 / dT);
                        loopTime = DateTime.Now;
                    }
                    giraffic.DrawText(Color.Red, fps + "FPS", 10, 10, myFont);

                    giraffic.Refresh();

                    dT = (DateTime.Now - startTime).TotalSeconds;
                }
            }).Start();

            startTime = DateTime.Now;
        }

        private static void Giraffic_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp = true;
            mousePos = e.Location;
        }

        static void MakeRandomPlanet(Point location)
        {
            float mag = 10 + 90 * (float)random.NextDouble();
            double r = 10 + 80 * random.NextDouble();
            Vector2 vel = mag * Vector2.Normalize(new Vector2((float)(2 * random.NextDouble() - 1), (float)(2 * random.NextDouble() - 1)));
            Planet planet = new Planet(new Vector2(location.X + giraffic.X, location.Y + giraffic.Y), r);
            planet.vel = vel;
        }

        static void GenStarSeed(int numStars)
        {
            starPositions = new Point[numStars];
            starRadii = new int[numStars];
            starBrightnesses = new double[numStars];
            for (int i = 0; i < numStars; i++)
            {
                starPositions[i] = new Point(random.Next(-2000, giraffic.Size.Width), random.Next(giraffic.Size.Height));
                starRadii[i] = random.Next(1, 3);
                starBrightnesses[i] = random.NextDouble();
            }
        }

        static void DrawStars()
        {
            double t = (double)(DateTime.Now - startTime).TotalMilliseconds / 1000;
            double b = (2 + Math.Sin(t)) / 3;
            for (int i = 0; i < starPositions.Length; i++)
            {
                Point pos = starPositions[i];
                int r = starRadii[i];
                int brightness = (int)(b * starBrightnesses[i] * 255);
                giraffic.FillCircle(Color.FromArgb(brightness, brightness, brightness), pos.X - giraffic.X, pos.Y - giraffic.Y, r);
            }
        }
    }
}
