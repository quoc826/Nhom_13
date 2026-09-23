using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai01
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();
        public TestContext TestContext { get; set; }
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai1.csv",
            "bai1#csv",
            DataAccessMethod.Sequential)]
        [DeploymentItem("bai1.csv")]
        [TestMethod]
        public void TestPrimeCheck()
        {
            string input = TestContext.DataRow[0]?.ToString();
            string expected = TestContext.DataRow[1]?.ToString()?.Trim();
            bool isNumber = int.TryParse(input, out int num);
            if (string.Equals(expected, "Exception", StringComparison.OrdinalIgnoreCase))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    if (!isNumber)
                    {
                        throw new Exception("Input không phải số nguyên hợp lệ.");
                    }
                    // Gọi hàm nghiệp vụ (quăng Exception nếu num < 0 hoặc num > 1000)
                    m.primeCheck(num);
                });
            }
            else
            {
                Assert.IsTrue(isNumber, $"Input '{input}' không phải là số nguyên.");
                bool expectedBool = bool.Parse(expected);
                bool actualBool = m.primeCheck(num);
                Assert.AreEqual(expectedBool, actualBool);
            }
        }
    }
}
