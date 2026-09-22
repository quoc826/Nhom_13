using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MethodLibrary;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem(@"bai7.csv")]
    public class Bai07
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai7.csv",
            "bai7#csv",
            DataAccessMethod.Sequential)]
        public void Test_Bai07_Sum()
        {
            long s0 = Convert.ToInt64(TestContext.DataRow["s0"]);
            long expected_k = Convert.ToInt64(TestContext.DataRow["expected_k"]);
            long expected_s = Convert.ToInt64(TestContext.DataRow["expected_s"]);

            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();

            long actual_s;
            long actual_k = lib.Sum(s0, out actual_s);

            Assert.AreEqual(
                expected_k,
                actual_k,
                $"Test Case ID {TestContext.DataRow["ID"]} thất bại: Sai k!"
            );

            Assert.AreEqual(
                expected_s,
                actual_s,
                $"Test Case ID {TestContext.DataRow["ID"]} thất bại: Sai s!"
            );
        }
    }
}