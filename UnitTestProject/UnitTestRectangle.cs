using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Rectangle = Component1.Rectangle;

namespace UnitTestProject
{
    /// <summary>
    /// class declared as UnitTestRectangle 
    /// </summary>
    [TestClass]
    public class UnitTestRectangle
    {
        [TestMethod]
        /// <summary>
        ///Checks the expected output with actual output
        /// </summary>
        public void UniTestRectangle()
        {
            var rectangle = new Rectangle();
            Color c = Color.Black;
            int x = 50, y = 60, width = 70, height = 90;
            rectangle.set(c, x, y, width, height);
            Assert.AreEqual(90, rectangle.height);

        }
    }
}
