using ColorControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorSpaceTests {
    [TestClass]
    public class HSVTest {
        [TestMethod]
        public void CreateTest() {
            HSV hsv1 = new HSV(1, 0, 0);
            Assert.AreEqual(1, hsv1.H);
            Assert.AreEqual(0, hsv1.S);
            Assert.AreEqual(0, hsv1.V);

            HSV hsv2 = new HSV(1, -1, -1);
            Assert.AreEqual(1, hsv2.H);
            Assert.AreEqual(0, hsv2.S);
            Assert.AreEqual(0, hsv2.V);

            HSV hsv3 = new HSV(1, 2, 2);
            Assert.AreEqual(1, hsv3.H);
            Assert.AreEqual(1, hsv3.S);
            Assert.AreEqual(1, hsv3.V);

            HSV hsv4 = new HSV(-1, 0.5, 0.5);
            Assert.AreEqual(5, hsv4.H);
            Assert.AreEqual(0.5, hsv4.S);
            Assert.AreEqual(0.5, hsv4.V);

            HSV hsv5 = new HSV(7, 0.5, 0.5);
            Assert.AreEqual(1, hsv5.H);
            Assert.AreEqual(0.5, hsv5.S);
            Assert.AreEqual(0.5, hsv5.V);

            HSV hsv6 = new HSV();
            hsv6.H = 0.1;
            hsv6.S = 0.2;
            hsv6.V = 0.3;

            Assert.AreEqual(0.1, hsv6.H);
            Assert.AreEqual(0.2, hsv6.S);
            Assert.AreEqual(0.3, hsv6.V);
        }

        [TestMethod]
        public void RGBTest() {
            RGB rgb1 = new HSV(0, 1, 1);
            HSV hsv1 = rgb1;

            Assert.AreEqual(1, rgb1.R);
            Assert.AreEqual(0, rgb1.G);
            Assert.AreEqual(0, rgb1.B);

            Assert.AreEqual(0, hsv1.H);
            Assert.AreEqual(1, hsv1.S);
            Assert.AreEqual(1, hsv1.V);

            RGB rgb2 = new HSV(1, 1, 1);
            HSV hsv2 = rgb2;

            Assert.AreEqual(1, rgb2.R);
            Assert.AreEqual(1, rgb2.G);
            Assert.AreEqual(0, rgb2.B);

            Assert.AreEqual(1, hsv2.H);
            Assert.AreEqual(1, hsv2.S);
            Assert.AreEqual(1, hsv2.V);

            RGB rgb3 = new HSV(2, 1, 1);
            HSV hsv3 = rgb3;

            Assert.AreEqual(0, rgb3.R);
            Assert.AreEqual(1, rgb3.G);
            Assert.AreEqual(0, rgb3.B);

            Assert.AreEqual(2, hsv3.H);
            Assert.AreEqual(1, hsv3.S);
            Assert.AreEqual(1, hsv3.V);

            RGB rgb4 = new HSV(3, 1, 1);
            HSV hsv4 = rgb4;

            Assert.AreEqual(0, rgb4.R);
            Assert.AreEqual(1, rgb4.G);
            Assert.AreEqual(1, rgb4.B);

            Assert.AreEqual(3, hsv4.H);
            Assert.AreEqual(1, hsv4.S);
            Assert.AreEqual(1, hsv4.V);

            RGB rgb5 = new HSV(4, 1, 1);
            HSV hsv5 = rgb5;

            Assert.AreEqual(0, rgb5.R);
            Assert.AreEqual(0, rgb5.G);
            Assert.AreEqual(1, rgb5.B);

            Assert.AreEqual(4, hsv5.H);
            Assert.AreEqual(1, hsv5.S);
            Assert.AreEqual(1, hsv5.V);

            RGB rgb6 = new HSV(5, 1, 1);
            HSV hsv6 = rgb6;

            Assert.AreEqual(1, rgb6.R);
            Assert.AreEqual(0, rgb6.G);
            Assert.AreEqual(1, rgb6.B);

            Assert.AreEqual(5, hsv6.H);
            Assert.AreEqual(1, hsv6.S);
            Assert.AreEqual(1, hsv6.V);

            RGB rgb7 = new HSV(1, 0.5, 0.75);
            HSV hsv7 = rgb7;

            Assert.AreEqual(0.75, rgb7.R);
            Assert.AreEqual(0.75, rgb7.G);
            Assert.AreEqual(0.375, rgb7.B);

            Assert.AreEqual(1, hsv7.H);
            Assert.AreEqual(0.5, hsv7.S);
            Assert.AreEqual(0.75, hsv7.V);
        }
    }
}
