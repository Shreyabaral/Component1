using System;
using System.Drawing;

namespace Component1
{
    public class Circle : IBasicShapes
    {
        public int x,y,radius;

        public Circle() : base()
        {

        }
        public Circle(int x,int y,int radius)
        {
            this.radius = radius;
        }
        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Black, 2);
                g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.radius = list[2];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}