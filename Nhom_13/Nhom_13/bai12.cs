using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem("bai12.csv")]
    public class Bai12
    {
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai12.csv",
            "bai12#csv",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestQuickSort()
        {
            string arrString = TestContext.DataRow[0]?.ToString().Trim() ?? "";
            int left = Convert.ToInt32(TestContext.DataRow[1]?.ToString().Trim() ?? "");
            int right = Convert.ToInt32(TestContext.DataRow[2]?.ToString().Trim() ?? "");
            string expectedString = TestContext.DataRow[3]?.ToString().Trim() ?? "";
            if (expectedString.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Inconclusive("Quay lại đặc tả.");
                return;
            }
            if (!TryParseArray(arrString, out int[] arr))
            {
                Assert.Inconclusive("Quay lại đặc tả: arr không hợp lệ.");
                return;
            }
            if (!TryParseArray(expectedString, out int[] expected))
            {
                Assert.Inconclusive("Quay lại đặc tả: expected không hợp lệ.");
                return;
            }
            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();
            lib.QuickSort(arr, left, right);
            CollectionAssert.AreEqual(expected, arr, "Kết quả sau khi QuickSort không đúng."
            );
        }
        private bool TryParseArray(string input, out int[] arr)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                arr = Array.Empty<int>();
                return true;
            }
            string[] parts = input.Split(',');
            arr = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i].Trim(), out arr[i]))
                {
                    arr = null;
                    return false;
                }
            }
            return true;
        }
    }
}
