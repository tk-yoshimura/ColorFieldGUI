using ColorControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorSpaceTests {
    [TestClass]
    public class YCbCrTest {
        [TestMethod]
        public void CreateTest() {
            YCbCr ycbcr1 = new YCbCr(1, 0, 0);
            Assert.AreEqual(1, ycbcr1.Y);
            Assert.AreEqual(0, ycbcr1.Cb);
            Assert.AreEqual(0, ycbcr1.Cr);

            YCbCr ycbcr2 = new YCbCr(1, -1, -1);
            Assert.AreEqual(1, ycbcr2.Y);
            Assert.AreEqual(-0.5, ycbcr2.Cb);
            Assert.AreEqual(-0.5, ycbcr2.Cr);

            YCbCr ycbcr3 = new YCbCr(1, 2, 2);
            Assert.AreEqual(1, ycbcr3.Y);
            Assert.AreEqual(+0.5, ycbcr3.Cb);
            Assert.AreEqual(+0.5, ycbcr3.Cr);

            YCbCr ycbcr4 = new YCbCr(-1, 0.5, 0.5);
            Assert.AreEqual(0, ycbcr4.Y);
            Assert.AreEqual(0.5, ycbcr4.Cb);
            Assert.AreEqual(0.5, ycbcr4.Cr);

            YCbCr ycbcr5 = new YCbCr();
            ycbcr5.Y = 0.1;
            ycbcr5.Cb = 0.2;
            ycbcr5.Cr = 0.3;

            Assert.AreEqual(0.1, ycbcr5.Y);
            Assert.AreEqual(0.2, ycbcr5.Cb);
            Assert.AreEqual(0.3, ycbcr5.Cr);
        }

        [TestMethod]
        public void RGBTest() {
            RGB rgb1 = new YCbCr(1, 0, 0);
            YCbCr ycbcr1 = rgb1;

            Assert.AreEqual(1, rgb1.R);
            Assert.AreEqual(1, rgb1.G);
            Assert.AreEqual(1, rgb1.B);

            Assert.AreEqual(1, ycbcr1.Y, 1e-15);
            Assert.AreEqual(0, ycbcr1.Cb, 1e-15);
            Assert.AreEqual(0, ycbcr1.Cr, 1e-15);

            RGB rgb2 = new YCbCr(0, 0, 0);
            YCbCr ycbcr2 = rgb2;

            Assert.AreEqual(0, rgb2.R);
            Assert.AreEqual(0, rgb2.G);
            Assert.AreEqual(0, rgb2.B);

            Assert.AreEqual(0, ycbcr2.Y, 1e-15);
            Assert.AreEqual(0, ycbcr2.Cb, 1e-15);
            Assert.AreEqual(0, ycbcr2.Cr, 1e-15);

            RGB rgb3 = new YCbCr(0, +0.5, 0);
            YCbCr ycbcr3 = rgb3;

            Assert.AreEqual(0, rgb3.R);
            Assert.AreEqual(-0.17206814310051111, rgb3.G);
            Assert.AreEqual(+0.886, rgb3.B);

            Assert.AreEqual(0, ycbcr3.Y, 1e-15);
            Assert.AreEqual(+0.5, ycbcr3.Cb, 1e-15);
            Assert.AreEqual(0, ycbcr3.Cr, 1e-15);

            RGB rgb4 = new YCbCr(0, -0.5, 0);
            YCbCr ycbcr4 = rgb4;

            Assert.AreEqual(0, rgb4.R);
            Assert.AreEqual(+0.17206814310051111, rgb4.G);
            Assert.AreEqual(-0.886, rgb4.B);

            Assert.AreEqual(0, ycbcr4.Y, 1e-15);
            Assert.AreEqual(-0.5, ycbcr4.Cb, 1e-15);
            Assert.AreEqual(0, ycbcr4.Cr, 1e-15);

            RGB rgb5 = new YCbCr(0, 0, +0.5);
            YCbCr ycbcr5 = rgb5;

            Assert.AreEqual(+0.701, rgb5.R);
            Assert.AreEqual(-0.35706814310051105, rgb5.G);
            Assert.AreEqual(0, rgb5.B);

            Assert.AreEqual(0, ycbcr5.Y, 1e-15);
            Assert.AreEqual(0, ycbcr5.Cb, 1e-15);
            Assert.AreEqual(+0.5, ycbcr5.Cr, 1e-15);

            RGB rgb6 = new YCbCr(0, 0, -0.5);
            YCbCr ycbcr6 = rgb6;

            Assert.AreEqual(-0.701, rgb6.R);
            Assert.AreEqual(+0.35706814310051105, rgb6.G);
            Assert.AreEqual(0, rgb6.B);

            Assert.AreEqual(0, ycbcr6.Y, 1e-15);
            Assert.AreEqual(0, ycbcr6.Cb, 1e-15);
            Assert.AreEqual(-0.5, ycbcr6.Cr, 1e-15);
        }
    }
}
