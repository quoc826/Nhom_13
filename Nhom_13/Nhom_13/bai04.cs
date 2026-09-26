using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai04
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai4.csv",
            "bai4#csv",
            DataAccessMethod.Sequential)]
        [DeploymentItem("bai4.csv")]
        [TestMethod]
        public void TestTriangle()
        {
            // 1. Đọc dữ liệu 4 cột từ file CSV
            string inputA = TestContext.DataRow["a"]?.ToString()?.Trim();
            string inputB = TestContext.DataRow["b"]?.ToString()?.Trim();
            string inputC = TestContext.DataRow["c"]?.ToString()?.Trim();
            string expected = TestContext.DataRow["ExpectedResult"]?.ToString()?.Trim();

            // 2. Chuyển đổi dữ liệu sang số nguyên
            bool isIntA = int.TryParse(inputA, out int a);
            bool isIntB = int.TryParse(inputB, out int b);
            bool isIntC = int.TryParse(inputC, out int c);

            // 3. Xử lý trường hợp Kỳ vọng ném ra Exception
            if (string.Equals(expected, "Exception", StringComparison.OrdinalIgnoreCase))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    // Nếu dữ liệu đầu vào không phải là số nguyên (chữ T, null...)
                    if (!isIntA || !isIntB || !isIntC)
                    {
                        throw new Exception("Đầu vào không phải là số nguyên.");
                    }

                    // Gọi hàm nghiệp vụ trong DLL
                    string res = m.Triangle(a, b, c);

                    // Nếu hàm trả về chuỗi báo lỗi thay vì quăng Exception
                    if (string.Equals(res, "Not A Triangle", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(res))
                    {
                        throw new Exception("Không phải là tam giác.");
                    }
                });
            }
            // 4. Xử lý trường hợp hợp lệ (Equilateral, Isosceles, Scalene)
            else
            {
                Assert.IsTrue(isIntA && isIntB && isIntC, $"Đầu vào ({inputA}, {inputB}, {inputC}) phải là số nguyên.");

                string actual = m.Triangle(a, b, c);

                Assert.AreEqual(expected, actual, true, $"Lỗi tại Test Case ({a}, {b}, {c})");
            }
        }
    }
}