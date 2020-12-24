using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Component1
{
    /// <summary>
    /// used to provide shape of objects
    /// </summary>
    interface IBasicShapes
    {
        /// <summary>
        /// sets the color and  value of the shapes 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="list"></param>
        void set(Color c, params int[] list);
        /// <summary>
        /// draws the shape of an object
        /// </summary>
        /// <param name="g"></param>
        void draw(Graphics g);
    }
}
