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
            string expectedKStr = TestContext.DataRow["expected_k"].ToString();

            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();

           
            if (expectedKStr.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    long actual_s;
                    long actual_k = lib.Sum(s0, out actual_s);
                    Assert.AreEqual(1L, actual_k,
                        $"Test Case ID {TestContext.DataRow["ID"]}: nếu không ném ngoại lệ thì k phải bằng 1.");
                    Assert.AreEqual(1L, actual_s,
                        $"Test Case ID {TestContext.DataRow["ID"]}: nếu không ném ngoại lệ thì s phải bằng 1.");
                }
                catch (Exception ex)
                {
                    if (ex is AssertFailedException)
                        throw;
                }
            }
            else
            {
                long expected_k = Convert.ToInt64(expectedKStr);
                long expected_s = Convert.ToInt64(TestContext.DataRow["expected_s"]);

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
}
