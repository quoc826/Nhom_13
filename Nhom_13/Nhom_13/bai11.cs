using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem("bai11.csv")]
    public class Bai11
    {
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai11.csv",
            "bai11#csv",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestIsSymmetry()
        {
            string arrString = TestContext.DataRow["arr"].ToString();
            string expectedString = TestContext.DataRow["expected"].ToString().Trim();
            bool err = expectedString.Equals("throw exception", StringComparison.OrdinalIgnoreCase);
            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();
            try
            {
                int[] arr = ParseArray(arrString);
                int n = Convert.ToInt32(TestContext.DataRow["n"].ToString().Trim());
                bool result = lib.IsSymmetry(arr, n);
                if (err)
                    Assert.Fail("Lỗi");
                bool expected = Convert.ToBoolean(expectedString);
                Assert.AreEqual(expected, result, "Kết quả kiểm tra đối xứng không đúng.");
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (!err) Assert.Fail($"Không mong đợi exception nhưng bị lỗi " + $"{ex.GetType().Name}: {ex.Message}");
            }
        }
        private int[] ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<int>();
            string[] parts = input.Split(',');
            int[] arr = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                arr[i] = Convert.ToInt32(parts[i].Trim());
            }
            return arr;
        }
    }
}
