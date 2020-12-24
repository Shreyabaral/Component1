using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1
{
    /// <summary>
    /// Gives the shapes of the object
    /// </summary>
    abstract class BasicShapes : IBasicShapes
    {
        protected Color colour;
        protected int x, y;
        public BasicShapes()
        {
            colour = Color.Black;
            x = y = 100;
        }
        /// <summary>
        /// holds the color , x- coordinate and y-coordinate
        /// </summary>
        /// <param name="colour"> sets the color</param>
        /// <param name="x"> sets the c-ordinate</param>
        /// <param name="y"> sets the y-coordinate</param>
        public BasicShapes(Color colour,int x, int y)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// draws the shape of object
        /// </summary>
        /// <param name="g"></param>
        public abstract void draw(Graphics g);
       

        public virtual void set(Color colour, params int[] list)
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
        }
        /// <summary>
        /// used to overwite the values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
