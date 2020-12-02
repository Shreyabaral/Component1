using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component1
{
   abstract class Creator
    {
        public abstract IBasicShapes getShape(string ShapeType);
    }
}
