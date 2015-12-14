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
           int result= UserInfoServer.CheckLogin("admin","admin","belongs","a");
           int expected = 0;
           Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void TestMethod0()
        {
            PersonalProjectServer server = new PersonalProjectServer();
            string result = server.SelectEachFile(3,"report_file");

            string[] actual = result.Split('/');
            string expected = "adsa";

            Assert.AreEqual(expected, actual[4]);
        }
    }
}
