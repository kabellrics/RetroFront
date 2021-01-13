using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetroFront.Services.Implementation;

namespace TestService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var JsonReader = new FileJSONService();
            var syslist = JsonReader.GetAllSysFromJSON();
            Assert.AreNotEqual(string.Empty, syslist);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mainservice = new MainService();
            var dbservice = new DatabaseService();
            Assert.AreEqual(43,dbservice.GetSystemes());
        }
        [TestMethod]
        public void TestMethod3()
        {
            var RetroarchService = new RetroarchService();
            var listcore = RetroarchService.GetInstalledCore();
            Assert.AreNotEqual(null, listcore);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var ThemeService = new ThemeService();
            var listcore = ThemeService.GetLogoForTheme("gba","simple");
            Assert.AreNotEqual(null, listcore);
        }
    }
}
