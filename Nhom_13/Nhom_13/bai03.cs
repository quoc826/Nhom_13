using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai03
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();

        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai3.csv",
            "bai3#csv",
            DataAccessMethod.Sequential)]
        [DeploymentItem("bai3.csv")]
        [TestMethod]
        public void TestBinToDec_DataDriven()
        {
            string input = TestContext.DataRow[0]?.ToString();
            string expected = TestContext.DataRow[1]?.ToString()?.Trim();

            if (string.Equals(expected, "FormatException", StringComparison.OrdinalIgnoreCase))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    m.BinToDec(input);
                });
            }
            else
            {
                long expectedVal = long.Parse(expected);
                long actualVal = m.BinToDec(input);

                Assert.AreEqual(
                    expectedVal,
                    actualVal,
                    $"Test case thất bại tại input = {input}"
                );
            }
        }
    }
}