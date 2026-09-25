using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai02
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();
        public TestContext TestContext { get; set; }

        [DataSource(
        "Microsoft.VisualStudio.TestTools.DataSource.CSV",
        "|DataDirectory|\\bai2.csv",
        "bai2#csv",
        DataAccessMethod.Sequential)]
        [DeploymentItem("bai2.csv")]
        [TestMethod]
        public void TestIsLeapYear()
        {
            string input = TestContext.DataRow[0]?.ToString();
            string expected = TestContext.DataRow[1]?.ToString()?.Trim();

            bool isNumber = int.TryParse(input, out int year);

            if (string.Equals(expected, "Exception", StringComparison.OrdinalIgnoreCase))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    if (!isNumber)
                    {
                        throw new Exception("Input không phải số nguyên.");
                    }
                    m.IsLeapYear(year);
                });
            }
            else
            {
                Assert.IsTrue(isNumber, $"Input '{input}' không phải là số nguyên.");
                bool actualBool = m.IsLeapYear(year);
                bool expectedBool = bool.Parse(expected);
                Assert.AreEqual(expectedBool, actualBool, $"Test case thất bại tại n = {year}");
            }
        }
    }
}
