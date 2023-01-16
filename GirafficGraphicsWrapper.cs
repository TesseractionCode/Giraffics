using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giraffics
{
    // A wrapper for the Giraffic's graphics instance. Exposes drawing-related methods.
    public partial class Giraffic
    {
        /// <summary>Clears the screen and fills it with the given color.</summary>
        public void Clear(Color color)
        {
            graphics.Clear(color);
        }

        /// <summary>Clears the screen clean with transparency.</summary>
        public void Clear()
        {
            graphics.Clear(Color.Transparent);
        }

        /// <summary>Draws the outline of a rectangle.</summary>
        public void DrawRect(Color color, int x, int y, int width, int height, int thickness)
        {
            graphics.DrawRectangle(new Pen(color, thickness), x, y, width, height);
        }

        /// <summary>Draws the outline of a rectangle.</summary>
        public void DrawRect(Color color, Rectangle rect, int thickness)
        {
            graphics.DrawRectangle(new Pen(color, thickness), rect);
        }

        /// <summary>Draws the outline of an ellipse.</summary>
        public void DrawEllipse(Color color, int x, int y, int width, int height, int thickness)
        {
            graphics.DrawEllipse(new Pen(color, thickness), x, y, width, height);
        }

        /// <summary>Draws the outline of an ellipse.</summary>
        public void DrawEllipse(Color color, Rectangle rect, int thickness)
        {
            graphics.DrawEllipse(new Pen(color, thickness), rect);
        }

        /// <summary>Draws the outline of a circle with its origin at the given position.</summary>
        public void DrawCircle(Color color, int x, int y, int radius, int thickness)
        {
            DrawEllipse(color, x - radius, y - radius, 2 * radius, 2 * radius, thickness);
        }

        /// <summary>Draws the outline of a circle with its origin at the given position.</summary>
        public void DrawCircle(Color color, Point position, int radius, int thickness)
        {
            DrawEllipse(color, position.X - radius, position.Y - radius, 2 * radius, 2 * radius, thickness);
        }

        /// <summary>Draws the outline of a polygon made up of the given points.</summary>
        public void DrawPolygon(Color color, Point[] points, int thickness)
        {
            graphics.DrawPolygon(new Pen(color, thickness), points);
        }

        /// <summary>Draws a line between two points.</summary>
        public void DrawLine(Color color, int x1, int y1, int x2, int y2, int thickness)
        {
            graphics.DrawLine(new Pen(color, thickness), x1, y1, x2, y2);
        }

        /// <summary>Draws a line between two points.</summary>
        public void DrawLine(Color color, Point point1, Point point2, int thickness)
        {
            graphics.DrawLine(new Pen(color, thickness), point1, point2);
        }

        /// <summary>Draws lines between all of the given points in order from start to finish.</summary>
        public void DrawLines(Color color, Point[] points, int thickness)
        {
            graphics.DrawLines(new Pen(color, thickness), points);
        }

        /// <summary>Draws a curved line spanning between the given points.</summary>
        public void DrawCurve(Color color, Point[] points, float tension, int thickness)
        {
            graphics.DrawCurve(new Pen(color, thickness), points, tension);
        }

        /// <summary>Draws a looped curvy line spanning between the given points.</summary>
        public void DrawClosedCurve(Color color, Point[] points, int thickness)
        {
            graphics.DrawClosedCurve(new Pen(color, thickness), points);
        }

        /// <summary>Draws a bezier line between four points.</summary>
        public void DrawBezier(Color color, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int thickness)
        {
            graphics.DrawBezier(new Pen(color, thickness), x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>Draws a bezier line between four points.</summary>
        public void DrawBezier(Color color, Point point1, Point point2, Point point3, Point point4, int thickness)
        {
            graphics.DrawBezier(new Pen(color, thickness), point1, point2, point3, point4);
        }

        /// <summary>Draws the portion of an ellipse's outline starting at startAngle and with a segment length defined by sweepAngle.</summary>
        public void DrawArc(Color color, int x1, int y1, int width, int height, float startAngle, float sweepAngle, int thickness)
        {
            graphics.DrawArc(new Pen(color, thickness), x1, y1, width, height, startAngle, sweepAngle);
        }

        /// <summary>Draws the portion of an ellipse's outline starting at startAngle and with a segment length defined by sweepAngle.</summary>
        public void DrawArc(Color color, Rectangle rect, float startAngle, float sweepAngle, int thickness)
        {
            graphics.DrawArc(new Pen(color, thickness), rect, startAngle, sweepAngle);
        }

        /// <summary>Draws an image at the specified location.</summary>
        public void DrawImage(Image image, int x, int y)
        {
            graphics.DrawImage(image, x, y);
        }

        /// <summary>Draws an image with the given width and height at the specified location.</summary>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        /// <summary>Draws an image whose dimensions and location are defined by the Rectangle argument.</summary>
        public void DrawImage(Image image, Rectangle rect)
        {
            graphics.DrawImage(image, rect);
        }

        /// <summary>Draws text at the given location. Size decided by font size.</summary>
        public void DrawText(Color color, string text, int x, int y, Font font)
        {
            graphics.DrawString(text, font, new SolidBrush(color), x, y);
        }

        /// <summary>Draws text at the given location. Size decided by font size.</summary>
        public void DrawText(Color color, string text, Point position, Font font)
        {
            graphics.DrawString(text, font, new SolidBrush(color), position);
        }

        /// <summary>Draws text inside of the given bounds.</summary>
        public void DrawText(Color color, string text, RectangleF bounds, Font font)
        {
            graphics.DrawString(text, font, new SolidBrush(color), bounds);
        }


        /// <summary>Draws a filled rectangle.</summary>
        public void FillRect(Color color, int x, int y, int width, int height)
        {
            graphics.FillRectangle(new SolidBrush(color), x, y, width, height);
        }

        /// <summary>Draws a filled rectangle.</summary>
        public void FillRect(Color color, Rectangle rect)
        {
            graphics.FillRectangle(new SolidBrush(color), rect);
        }

        /// <summary>Draws a filled ellipse.</summary>
        public void FillEllipse(Color color, int x, int y, int width, int height)
        {
            graphics.FillEllipse(new SolidBrush(color), x, y, width, height);
        }

        /// <summary>Draws a filled ellipse.</summary>
        public void FillEllipse(Color color, Rectangle rect)
        {
            graphics.FillEllipse(new SolidBrush(color), rect);
        }

        /// <summary>Draws a filled circle with its origin at the given position.</summary>
        public void FillCircle(Color color, int x, int y, int radius)
        {
            FillEllipse(color, x - radius, y - radius, 2 * radius, 2 * radius);
        }

        /// <summary>Draws a filled circle with its origin at the given position.</summary>
        public void FillCircle(Color color, Point position, int radius)
        {
            FillEllipse(color, position.X - radius, position.Y - radius, 2 * radius, 2 * radius);
        }

        /// <summary>Draws a filled polygon.</summary>
        public void FillPolygon(Color color, Point[] points)
        {
            graphics.FillPolygon(new SolidBrush(color), points);
        }

        /// <summary>Draws a filled looped curvy line spanning between the given points.</summary>
        public void FillClosedCurve(Color color, Point[] points)
        {
            graphics.FillClosedCurve(new SolidBrush(color), points);
        }
    }
}
