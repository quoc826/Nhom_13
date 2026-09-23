using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MethodLibrary;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem(@"bai9.csv")]
    public class Bai09
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai9.csv",
            "bai9#csv",
            DataAccessMethod.Sequential)]
        public void Test_Bai09_ThayThe()
        {
            string s1 = Convert.ToString(TestContext.DataRow["s1"]);
            string s2 = Convert.ToString(TestContext.DataRow["s2"]);
            string s3 = Convert.ToString(TestContext.DataRow["s3"]);
            string expected_result = Convert.ToString(TestContext.DataRow["expected_result"]);

            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();

            string actual_result = lib.ThayThe(s1, s2, s3);

            Assert.AreEqual(
                expected_result,
                actual_result,
                $"Test Case ID {TestContext.DataRow["ID"]} thất bại: Sai kết quả chuỗi đầu ra!"
            );
        }
    }
}