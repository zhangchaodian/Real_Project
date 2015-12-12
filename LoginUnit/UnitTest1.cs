using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.BLL;

namespace LoginUnit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           int result= UserInfoServer.CheckLogin("admin","admin");
           int expected = 0;
           Assert.AreEqual(result, expected);
        }
    }
}
