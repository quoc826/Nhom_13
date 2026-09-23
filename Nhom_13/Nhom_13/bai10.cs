using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem("bai10.csv")]
    public class Bai10
    {
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai10.csv",
            "bai10#csv",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestLargest()
        {
            string arrString = TestContext.DataRow["arr"].ToString();
            int expected = Convert.ToInt32(TestContext.DataRow["expected"].ToString().Trim());
            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();
            int[] arr;
            try
            {
                arr = ParseArray(arrString);
            }
            catch (FormatException)
            {
                Assert.AreEqual(0, expected, "Dữ liệu không hợp lệ"
                );
                return;
            }
            int actual = lib.Largest(arr);
            Assert.AreEqual(
                expected,
                actual,
                "Giá trị lớn nhất không đúng."
            );
        }

        private int[] ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Array.Empty<int>();
            }
            string[] parts = input.Split(',');
            int[] arr = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                arr[i] = Convert.ToInt32(parts[i].Trim());
            }
            return arr;
        }
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    int[] arr = { 3, 6, 1, 8, 10, 23 };
        //    int exp = 23;
        //    MethodLibrary.MethodLibrary obj = new MethodLibrary.MethodLibrary();
        //    int result = obj.Largest(arr);
        //    Assert.AreEqual(exp, result);
        //}
        //[TestMethod]
        //public void TestMethod2()
        //{
        //    int[] arr = {3};
        //    int exp = 3;
        //    MethodLibrary.MethodLibrary obj = new MethodLibrary.MethodLibrary();
        //    int result = obj.Largest(arr);
        //    Assert.AreEqual(exp, result);
        //}
    }
}
