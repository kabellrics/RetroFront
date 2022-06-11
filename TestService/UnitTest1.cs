using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetroFront.Services.Implementation;
using System.Threading.Tasks;

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

        //[TestMethod]
        //public void TestMethod2()
        //{
        //    var dbservice = new DatabaseService();
        //    Assert.AreEqual(43,dbservice.GetSystemes());
        //}
        //[TestMethod]
        //public void TestMethod3()
        //{
        //    var RetroarchService = new RetroarchService();
        //    var listcore = RetroarchService.GetInstalledCore();
        //    Assert.AreNotEqual(null, listcore);
        //}
        [TestMethod]
        public void TestMethod4()
        {
            var ThemeService = new ThemeService();
            var listcore = ThemeService.GetLogoForTheme("gba");
            Assert.AreNotEqual(null, listcore);
        }
        //[TestMethod]
        //public void TestMethod5()
        //{
        //    var SteamService = new SteamService();
        //    //SteamService.GetSteamGame(@"C:\Program Files (x86)\Steam\steam.exe");
        //    Assert.AreNotEqual(null, null);
        //}
        [TestMethod]
        public void TestMethod6()
        {
            var IGDBService = new IGDBService();
            var result = IGDBService.GetPlatformByName("Nintendo");
            Assert.AreNotEqual(null, result);
        } 
        [TestMethod]
        public void TestMethod7()
        {
            var IGDBService = new IGDBService();
            var result = IGDBService.GetGameByName("Lego");
            Assert.AreNotEqual(null, result);
        }
        [TestMethod]
        public void TestMethod8()
        {
            var IGDBService = new IGDBService();
            var result = IGDBService.GetArtworksByGameId(19560);// .GetGameByName("Lego");
            Assert.AreNotEqual(null, result);
        }
        [TestMethod]
        public void TestMethod9()
        {
            var IGDBService = new IGDBService();
            var result = IGDBService.GetVideosByGameId(19560);// .GetGameByName("Lego");
            Assert.AreNotEqual(null, result);
        }
        [TestMethod]
        public void TestMethod10()
        {
            var JsonReader = new FileJSONService();
            var syslist = JsonReader.GetStandaloneEmulators();
            Assert.AreNotEqual(string.Empty, syslist);
        }
        [TestMethod]
        public void TestMethod11()
        {
            var themeService = new ThemeService();
            themeService.LoadThemesForDefaultCollection();
            Assert.Fail();
        }
    }
}
