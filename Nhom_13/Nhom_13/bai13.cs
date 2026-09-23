using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MethodLibrary;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem(@"bai13.csv")]
    public class Bai13
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai13.csv",
            "bai13#csv",
            DataAccessMethod.Sequential)]
        public void Test_Bai13_Max()
        {
            int A = Convert.ToInt32(TestContext.DataRow["A"]);
            int B = Convert.ToInt32(TestContext.DataRow["B"]);
            int C = Convert.ToInt32(TestContext.DataRow["C"]);

            string expected =
                TestContext.DataRow["ExpectedResult"].ToString();

            MethodLibrary.MethodLibrary lib =
                new MethodLibrary.MethodLibrary();

            try
            {
                int actual = lib.Max(A, B, C);

                if (expected == "IndexOutOfRangeException")
                {
                    Assert.Fail(
                        $"Test Case ID {TestContext.DataRow["ID"]} thất bại: " +
                        "Expected IndexOutOfRangeException nhưng hàm không throw exception!"
                    );
                }

                int expectedValue = Convert.ToInt32(expected);

                Assert.AreEqual(
                    expectedValue,
                    actual,
                    $"Test Case ID {TestContext.DataRow["ID"]} thất bại!"
                );
            }
            catch (IndexOutOfRangeException)
            {
                Assert.AreEqual(
                    "IndexOutOfRangeException",
                    expected,
                    $"Test Case ID {TestContext.DataRow["ID"]} thất bại: Exception không đúng!"
                );
            }
        }
    }
}