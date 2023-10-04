using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GrafikaDLL
{
    public static class ExtensionGraphics
    {
        #region Pixel
        public static void Pixel(this Graphics g,Pen pen, int x, int y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        #endregion

        #region DrawLine
        public static void Pixel(this Graphics g, Color c, int x, int y)
        {
            g.DrawRectangle(new Pen(c), x, y, 0.5f, 0.5f);
        }
        public static void DrawLineDDA(this Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            int dx, dy, len;

            dx = x2 - x1 ;
            dy = y2 - y1 ;

            len = Math.Abs(dx);

            if (len < Math.Abs(dy)) { len = Math.Abs(dy); }

            double incX = (double)dx / (double)len;
            double incY = (double)dy / (double)len;

            double x = x1, y = y1;

            g.Pixel(pen, (int)x1, (int)y1);

            for (int i = 0; i < len; i++)
            {
                x += incX;
                y += incY;
                g.Pixel(pen, (int)x, (int)y);
            }

        }

        public static void DrawLineDDA(this Graphics g, Color c1, Color c2, int x1, int y1, int x2, int y2)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(new Point(x1, y1), new Point(x2, y2), c1, c2);

            Pen p = new Pen(lgb);

            int dx, dy, len;

            dx = x2 - x1;
            dy = y2 - y1;

            int dG = c2.G - c1.G;
            int dR = c2.R - c1.R;
            int dB = c2.B - c1.B;


            len = Math.Abs(dx);

            if (len < Math.Abs(dy)) { len = Math.Abs(dy); }

            double incX = (double)dx / (double)len;
            double incY = (double)dy / (double)len;

            double incR = (double)dR / len;
            double incG = (double)dG / len;
            double incB = (double)dB / len;


            double x = x1, y = y1;
            double R = c1.R, B = c1.B, G = c1.G;

            g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x1, (int)y1);

            for (int i = 0; i < len; i++)
            {
                x += incX;
                y += incY;
                R += incR;
                B += incB; G += incG;
                g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x, (int)y);
            }

        }

        public static void DrawLineDDA(this Graphics g, Color[] colors, double[] rations, PointF p0, PointF p1)
        {
            float x1 = p0.X;
            float x2 = p1.X;
            float y1 = p0.Y;
            float y2 = p1.Y;

            float dx = x2 - x1;
            float dy = y2 - y1;

            float length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);


            double rationsSum = 0;
            for (int i = 0; i < rations.Length; i++)
            {
                rationsSum += rations[i];
            }
            double lepeskoz = length / rationsSum;

            for (int i = 0; i < rations.Length; i++)
            {
                x2 = (float)(x1 + (lepeskoz * rations[i]));
                y2 = (float)(y1 + (lepeskoz * rations[i]));
                dx = x2 - x1;
                dy = y2 - y1;

                int dR = colors[i + 1].R - colors[i].R;
                int dG = colors[i + 1].G - colors[i].G;
                int dB = colors[i + 1].B - colors[i].B;

                length = Math.Abs(dx);
                if (length < Math.Abs(dy))
                    length = Math.Abs(dy);
                double incX = (double)dx / length;
                double incY = (double)dy / length;
                double incR = (double)dR / length;
                double incG = (double)dG / length;
                double incB = (double)dB / length;

                double x = x1, y = y1;
                double R = colors[i].R, G = colors[i].G, B = colors[i].B;

                g.Pixel(Color.FromArgb(255, 255, 255), (int)x, (int)y);
                g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x, (int)y);

                for (int j = 0; j < length - 1; j++)
                {
                    x += incX; y += incY;
                    R += incR; G += incG; B += incB;
                    
                    g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x, (int)y);
                    g.Pixel(Color.FromArgb(255, 255, 255), (int)x, (int)y);
                }


                x1 = (float)(x1 + (lepeskoz * rations[i]));
                y1 = (float)(y1 + (lepeskoz * rations[i]));
            }


        }

        public static void DrawLineMidPoint2(this Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            int dx = x2- x1 ;
            int dy = y2- y1 ;

            int d = 2 * dy - dx;
            int x = x1, y = y1;
            for (int i = 0; i < dx; i++)
            {
                g.Pixel(pen, x, y);
                if (d > 0)
                {
                    y++;
                    d += dy - dx;
                }
                else
                {
                    d = d +  dy;
                }
                x++;
            }
        }
        #endregion

        #region DrawCircle
        private static void CirclePoints(this Graphics g, Pen pen, int x, int y)
        {
            g.Pixel(pen, x, y);
            g.Pixel(pen, x, -y);
            g.Pixel(pen, -x, y);
            g.Pixel(pen, -x, -y);
            g.Pixel(pen, y, x);
            g.Pixel(pen, y, -x);
            g.Pixel(pen, -y, x);
            g.Pixel(pen, -y, -x);
        }

        public static void DrawCirlce(this Graphics g, Pen pen, int r)
        {
            int x = 0;
            int y = r;
            int h = 1 - r;
            g.CirclePoints(pen, x, y);
            while (x > y)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                g.CirclePoints(pen, x, y);
            }
        }


        #endregion
    }
}
