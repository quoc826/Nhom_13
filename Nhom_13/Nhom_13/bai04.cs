using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Nhom_13
{
    // ==========================================
    // 1. CLASS XỬ LÝ LOGIC KIỂM TRA TAM GIÁC
    // ==========================================
    public class GeometryChecker
    {
        public static string CheckTriangleType(string aStr, string bStr, string cStr)
        {
            // Bắt ngoại lệ nếu cạnh chứa chữ ("T"), null, rỗng hoặc không ép kiểu thành số được
            if (!double.TryParse(aStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double a) ||
                !double.TryParse(bStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double b) ||
                !double.TryParse(cStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double c))
            {
                throw new ArgumentException("Độ dài cạnh không hợp lệ.");
            }

            // Ràng buộc điều kiện tồn tại tam giác: các cạnh phải > 0 và thỏa mãn bất đẳng thức tam giác
            if (a <= 0 || b <= 0 || c <= 0 || (a + b <= c) || (a + c <= b) || (b + c <= a))
            {
                return "Not a Triangle"; // Hoặc trả về chuỗi rỗng "" tùy theo quy ước đề bài
            }

            // Phân loại loại tam giác
            if (a == b && b == c)
            {
                return "Equilateral"; // Tam giác đều
            }
            else if (a == b || b == c || a == c)
            {
                return "Isosceles";   // Tam giác cân
            }
            else
            {
                return "Scalene";     // Tam giác thường
            }
        }
    }

    // ==========================================
    // 2. CLASS KIỂM THỬ UNIT TEST DATA-DRIVEN
    // ==========================================
    [TestClass]
    public class Bai04Test
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "bai4.csv",
                    "bai4#csv",
                    DataAccessMethod.Sequential)]
        [DeploymentItem("bai4.csv")]
        public void Test_CheckTriangleType_DataDriven()
        {
            // Đọc trạng thái kỳ vọng văng lỗi từ cột shouldThrowException
            bool shouldThrow = false;
            if (TestContext.DataRow.Table.Columns.Contains("shouldThrowException") &&
                TestContext.DataRow["shouldThrowException"] != DBNull.Value)
            {
                bool.TryParse(TestContext.DataRow["shouldThrowException"].ToString().Trim(), out shouldThrow);
            }

            // Đọc dữ liệu các cột a, b, c và expected
            string rawA = TestContext.DataRow["a"]?.ToString().Trim() ?? "";
            string rawB = TestContext.DataRow["b"]?.ToString().Trim() ?? "";
            string rawC = TestContext.DataRow["c"]?.ToString().Trim() ?? "";
            string expected = TestContext.DataRow["expected"]?.ToString().Trim() ?? "";

            // Xử lý kiểm thử các case kỳ vọng văng ngoại lệ (Exception)
            if (shouldThrow)
            {
                try
                {
                    GeometryChecker.CheckTriangleType(rawA, rawB, rawC);
                    Assert.Fail("Kỳ vọng văng Exception nhưng chương trình không văng.");
                }
                catch (ArgumentException)
                {
                    // Văng lỗi đúng kỳ vọng -> Pass
                    Assert.IsTrue(true);
                }
            }
            // Xử lý kiểm thử các case bình thường
            else
            {
                string actual = GeometryChecker.CheckTriangleType(rawA, rawB, rawC);
                Assert.AreEqual(expected, actual, $"Lỗi kết quả tại case (a={rawA}, b={rawB}, c={rawC})");
            }
        }
    }
}