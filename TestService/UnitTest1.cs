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
            var LocalfileService = new LocalFileService();
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
    }
}
