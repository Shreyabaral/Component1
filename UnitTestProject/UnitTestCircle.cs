using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using Circle = Component1.Circle;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    /// <summary>
    /// class declared as UnitTestCircle
    /// </summary>

    public class UnitTestCircle
    {
        [TestMethod]
        /// <summary>
        /// checks the expected value with output value 
        /// </summary>
        public void UniTestCircle()
        {
            var circle = new Circle();
            Color c = Color.Black;
            int x = 40, y = 50, radius = 30;
            circle.set(c, x, y, radius);
            Assert.AreEqual(40, circle.x);

        }
    }
}
