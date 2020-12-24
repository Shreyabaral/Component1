using System;
using System.Drawing;

namespace Component1
{
    /// <summary>
    /// has commands of the class circle 
    /// </summary>
    public class Circle : IBasicShapes
    {
        public int x,y,radius;

        public Circle() : base()
        {

        }
        /// <summary>
        /// gets the parameter for the  circle 
        /// </summary>
        /// <param name="x"> sets the x-coordinate</param>
        /// <param name="y"> sets the y-coordinate</param>
        /// <param name="radius"> sets the radius of the circle </param>
        public Circle(int x,int y,int radius)
        {
            this.radius = radius;
        }
        /// <summary>
        /// draws the circle in the display panel 
        /// </summary>
        /// <param name="g"></param>
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
        /// <summary>
        /// accepts the color and sets value for x,y and radius
        /// </summary>
        /// <param name="c"> gives the color of the circle </param>
        
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