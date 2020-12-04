using Component1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
   public class UnitTestTriangle
    {
        [TestMethod]
        public void UniTestTriangle()
        {
            var triangle = new Triangle();
            Color c = Color.Black;
            int x = 70, y = 80, height = 65, width = 55;
            triangle.set(c, x, y, width, height);
            Assert.AreEqual(80, triangle.y);

        }
    }
}
