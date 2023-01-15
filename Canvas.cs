using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Giraffics
{
    /// <summary>
    /// A simple wrapper for the System.Drawing.Graphics class which
    /// exposes only drawing-related methods.
    /// </summary>
    public class Canvas
    {
        public Graphics g;

        public Canvas(Graphics graphics)
        {
            g = graphics;
        }

        /// <summary>Clears the canvas with the given color.</summary>
        public void Clear(Color color)
        {
            g.Clear(color);
        }

        /// <summary>Draws the outline of a rectangle.</summary>
        public void DrawRect(Color color, int x, int y, int width, int height, int thickness)
        {
            g.DrawRectangle(new Pen(color, thickness), x, y, width, height);
        }

        /// <summary>Draws the outline of a rectangle.</summary>
        public void DrawRect(Color color, Rectangle rect, int thickness)
        {
            g.DrawRectangle(new Pen(color, thickness), rect);
        }

        /// <summary>Draws the outline of an ellipse.</summary>
        public void DrawEllipse(Color color, int x, int y, int width, int height, int thickness)
        {
            g.DrawEllipse(new Pen(color, thickness), x, y, width, height);
        }

        /// <summary>Draws the outline of an ellipse.</summary>
        public void DrawEllipse(Color color, Rectangle rect, int thickness)
        {
            g.DrawEllipse(new Pen(color, thickness), rect);
        }

        /// <summary>Draws the outline of a polygon made up of the given points.</summary>
        public void DrawPolygon(Color color, Point[] points, int thickness)
        {
            g.DrawPolygon(new Pen(color, thickness), points);
        }

        /// <summary>Draws a line between two points.</summary>
        public void DrawLine(Color color, int x1, int y1, int x2, int y2, int thickness)
        {
            g.DrawLine(new Pen(color, thickness), x1, y1, x2, y2);
        }

        /// <summary>Draws a line between two points.</summary>
        public void DrawLine(Color color, Point point1, Point point2, int thickness)
        {
            g.DrawLine(new Pen(color, thickness), point1, point2);
        }

        /// <summary>Draws lines between all of the given points in order from start to finish.</summary>
        public void DrawLines(Color color, Point[] points, int thickness)
        {
            g.DrawLines(new Pen(color, thickness), points);
        }

        /// <summary>Draws a curved line spanning between the given points.</summary>
        public void DrawCurve(Color color, Point[] points, float tension, int thickness)
        {
            g.DrawCurve(new Pen(color, thickness), points, tension);
        }

        /// <summary>Draws a looped curvy line spanning between the given points.</summary>
        public void DrawClosedCurve(Color color, Point[] points, int thickness)
        {
            g.DrawClosedCurve(new Pen(color, thickness), points);
        }

        /// <summary>Draws a bezier line between four points.</summary>
        public void DrawBezier(Color color, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int thickness)
        {
            g.DrawBezier(new Pen(color, thickness), x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>Draws a bezier line between four points.</summary>
        public void DrawBezier(Color color, Point point1, Point point2, Point point3, Point point4, int thickness)
        {
            g.DrawBezier(new Pen(color, thickness), point1, point2, point3, point4);
        }

        /// <summary>Draws the portion of an ellipse's outline starting at startAngle and with a segment length defined by sweepAngle.</summary>
        public void DrawArc(Color color, int x1, int y1, int width, int height, float startAngle, float sweepAngle, int thickness)
        {
            g.DrawArc(new Pen(color, thickness), x1, y1, width, height, startAngle, sweepAngle);
        }

        /// <summary>Draws the portion of an ellipse's outline starting at startAngle and with a segment length defined by sweepAngle.</summary>
        public void DrawArc(Color color, Rectangle rect, float startAngle, float sweepAngle, int thickness)
        {
            g.DrawArc(new Pen(color, thickness), rect, startAngle, sweepAngle);
        }

        /// <summary>Draws an image at the specified location.</summary>
        public void DrawImage(Image image, int x, int y)
        {
            g.DrawImage(image, x, y);
        }

        /// <summary>Draws an image with the given width and height at the specified location.</summary>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            g.DrawImage(image, x, y, width, height);
        }

        /// <summary>Draws an image whose dimensions and location are defined by the Rectangle argument.</summary>
        public void DrawImage(Image image, Rectangle rect)
        {
            g.DrawImage(image, rect);
        }

        /// <summary>Draws text at the given location. Size decided by font size.</summary>
        public void DrawText(Color color, string text, int x, int y, Font font)
        {
            g.DrawString(text, font, new SolidBrush(color), x, y);
        }

        /// <summary>Draws text at the given location. Size decided by font size.</summary>
        public void DrawText(Color color, string text, Point position, Font font)
        {
            g.DrawString(text, font, new SolidBrush(color), position);
        }

        /// <summary>Draws text inside of the given bounds.</summary>
        public void DrawText(Color color, string text, RectangleF bounds, Font font)
        {
            g.DrawString(text, font, new SolidBrush(color), bounds);
        }


        /// <summary>Draws a filled rectangle.</summary>
        public void FillRect(Color color, int x, int y, int width, int height)
        {
            g.FillRectangle(new SolidBrush(color), x, y, width, height);
        }

        /// <summary>Draws a filled rectangle.</summary>
        public void FillRect(Color color, Rectangle rect)
        {
            g.FillRectangle(new SolidBrush(color), rect);
        }

        /// <summary>Draws a filled ellipse.</summary>
        public void FillEllipse(Color color, int x, int y, int width, int height)
        {
            g.FillRectangle(new SolidBrush(color), x, y, width, height);
        }

        /// <summary>Draws a filled ellipse.</summary>
        public void FillEllipse(Color color, Rectangle rect)
        {
            g.FillRectangle(new SolidBrush(color), rect);
        }

        /// <summary>Draws a filled polygon.</summary>
        public void FillPolygon(Color color, Point[] points)
        {
            g.FillPolygon(new SolidBrush(color), points);
        }

        /// <summary>Draws a filled looped curvy line spanning between the given points.</summary>
        public void FillClosedCurve(Color color, Point[] points)
        {
            g.FillClosedCurve(new SolidBrush(color), points);
        }
    }
}
