using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1
{
    /// <summary>
    /// Class declared as creator
    /// </summary>
   abstract class Creator
    {
        /// <summary>
        /// used to pass shape of an object
        /// </summary>
        /// <param name="ShapeType"></param>
        /// <returns></returns>
        public abstract IBasicShapes getShape(string ShapeType);
    }
}
