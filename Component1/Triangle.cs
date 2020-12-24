using System;
using System.Drawing;

namespace Component1
{
    /// <summary>
    /// class declared as triangle and implements interface 
    /// </summary>
    public class Triangle : IBasicShapes
    {
        /// <summary>
        /// sets the values for sides of triangle 
        /// </summary>
        public int xcordinate1, ycordinate1, xcordinate2, ycordinate2,
            xcordinate3, ycordinate3, xcordinate4, ycordinate4,
            xcordinate5, ycordinate5, xcordinate6, ycordinate6;
        /// <summary>
        /// draws the triangle 
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Pen pen = new Pen(Color.RosyBrown, 2);
                g.DrawLine(pen, xcordinate1, ycordinate1, xcordinate2, ycordinate2);
                g.DrawLine(pen, xcordinate3, ycordinate3, xcordinate4, ycordinate4);
                g.DrawLine(pen, xcordinate5, ycordinate5, xcordinate6, ycordinate6);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// used to set color and values of different sides of trianle
        /// </summary>
        /// <param name="c"> Color </param>
        /// <param name="list"> List of parameters </param>

        public void set(Color c, params int[] list)
        {
            this.xcordinate1 = list[0];
            this.ycordinate1 = list[1];
            this.xcordinate2 = list[2];
            this.ycordinate2 = list[3];

            this.xcordinate3 = list[4];
            this.ycordinate3 = list[5];
            this.xcordinate4 = list[6];
            this.ycordinate4 = list[7];

            this.xcordinate5 = list[8];
            this.ycordinate5 = list[9];
            this.xcordinate6 = list[10];
            this.ycordinate6 = list[11];
        }
    }
}