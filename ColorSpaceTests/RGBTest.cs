using ColorControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorSpaceTests {
    [TestClass]
    public class RGBTest {
        [TestMethod]
        public void CreateTest() {
            RGB rgb1 = new RGB(-1.5, 2, 3);
            Assert.AreEqual(-1.5, rgb1.R);
            Assert.AreEqual(2, rgb1.G);
            Assert.AreEqual(3, rgb1.B);

            RGB rgb2 = rgb1.Normalize;
            Assert.AreEqual(0, rgb2.R);
            Assert.AreEqual(1, rgb2.G);
            Assert.AreEqual(1, rgb2.B);

            RGB rgb3 = new RGB();
            rgb3.R = 0.1;
            rgb3.G = 0.2;
            rgb3.B = 0.3;

            Assert.AreEqual(0.1, rgb3.R);
            Assert.AreEqual(0.2, rgb3.G);
            Assert.AreEqual(0.3, rgb3.B);
        }
    }
}
