using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace spizlUT
{
    [TestClass]
    public class UnitTest1
    {

        private string FS(string s)
        {
            return s.Replace("\r", "").Replace("\n", "");
        }

        [TestMethod]
        public void Bubble()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/bubble.spiz");
            spiz.Run();

            string expected = "0123451098";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

        [TestMethod]
        public void String()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/string.spiz");
            spiz.Run();
            string expected = "spz123assspiz";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }
        //2368912161761886911219608
        [TestMethod]
        public void QuickSort()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/QuickSort.spiz");
            spiz.Run();
            string expected = "22368912161761886911219608";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }
        [TestMethod]

        public void Factorial()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/factorial.spiz");
            spiz.Run();
            string expected = "3628800";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

        [TestMethod]

        public void Foreach()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/foreach.spiz");
            spiz.Run();
            string expected = "56788";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

        [TestMethod]

        public void spizout()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/spizout.spiz");
            spiz.Run();
            string expected = "161820101";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

        [TestMethod]
        public void Infinite()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/infinite.spiz");
            spiz.Run();
            string expected = "1000001";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

        [TestMethod]
        public void Spif()
        {
            spizL.spizL spiz = new spizL.spizL("Tests/spif.spiz");
            spiz.Run();
            string expected = "0spz";
            Assert.AreEqual(expected, FS(spiz.Result), "Fail.");
        }

    }
}
