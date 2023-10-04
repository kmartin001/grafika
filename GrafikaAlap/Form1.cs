using GrafikaDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        Color[] c = new Color[4] { Color.FromArgb(100,255,255,255) , Color.Red, Color.Blue, Color.Red};
        double[] ratio = new double[3] { 1, 2, 6 };
        Rectangle win;
        PointF point1 = new PointF(50, 50);
        PointF point2 = new PointF(300, 300);

        public Form1()
        {
            InitializeComponent();
            win = new Rectangle(new Point(100, 50), new Size(canvas.Width - 200, canvas.Height - 130));
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawLineDDA(c, ratio, point1, point2);
            //g.DrawLineDDA(Color.Blue, Color.Red, 100, 100, 50, 50);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
