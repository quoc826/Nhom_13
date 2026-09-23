using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MethodLibrary;

namespace Nhom_13
{
    [TestClass]
    [DeploymentItem(@"bai8.csv")]
    public class Bai08
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai8.csv",
            "bai8#csv",
            DataAccessMethod.Sequential)]
        public void Test_Bai08_HuyChuoi()
        {
            // 1. Đọc dữ liệu đầu vào từ file CSV
            string s = TestContext.DataRow["s"].ToString().Trim();
            int n = Convert.ToInt32(TestContext.DataRow["n"]);
            int p = Convert.ToInt32(TestContext.DataRow["p"]);
            string expected = TestContext.DataRow["expected"].ToString().Trim();

            MethodLibrary.MethodLibrary lib = new MethodLibrary.MethodLibrary();
            string actual;

            // 2. Gọi hàm và xử lý ngoại lệ một cách trung thực
            try
            {
                actual = lib.HuyChuoi(s, n, p);
            }
            catch (Exception ex)
            {
                // Nếu hàm của bạn văng lỗi (ví dụ do truyền số âm vào Substring),
                // ghi nhận thực tế là hàm đã phát sinh Exception.
                actual = "Lỗi Exception: " + ex.GetType().Name;
            }

            // 3. So sánh kết quả thực tế với kết quả mong đợi
            Assert.AreEqual(
                expected,
                actual,
                $"Test Case ID {TestContext.DataRow["id"]} thất bại!"
            );
        }
    }
}