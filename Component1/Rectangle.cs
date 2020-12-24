using System;
using System.Drawing;

namespace Component1
{
    /// <summary>
    /// class declared as rectangle 
    /// </summary>
    public class Rectangle : IBasicShapes
    {
        public int x, y, width, height;
        /// <summary>
        /// sets the width and height of the rectangle
        /// </summary>
        public Rectangle() : base()
        {
            width = 0;
            height = 0;
        }
        /// <summary>
        /// sets  the value of x-axis, y-axis, width nad height of rectangle
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate </param>
        /// <param name="width"> width of rectangele</param>
        /// <param name="height"> height of rectangle</param>
        public Rectangle(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// used to draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Black, 2);
             
                g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// provides the color and parameters
        /// </summary>
        /// <param name="c"> color</param>
        /// <param name="list"> List of parameters</param>

        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.width = list[2];
                this.height = list[3];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}