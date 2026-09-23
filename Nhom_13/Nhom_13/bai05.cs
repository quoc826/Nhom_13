using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Nhom_13
{
    // ==========================================
    // 1. CLASS XỬ LÝ LOGIC GIẢI PHƯƠNG TRÌNH BẬC 2
    // ==========================================
    public class QuadraticEquationSolver
    {
        public static string Solve(string aStr, string bStr, string cStr)
        {
            // Bắt ngoại lệ nếu dữ liệu không phải là số nguyên hợp lệ (chữ, rỗng, số thập phân)
            if (!double.TryParse(aStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out double a) ||
                !double.TryParse(bStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out double b) ||
                !double.TryParse(cStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out double c))
            {
                throw new ArgumentException("Dữ liệu đầu vào không hợp lệ.");
            }

            if (a == 0)
            {
                if (b == 0)
                {
                    if (c == 0)
                        return "Vô số nghiệm";
                    else
                        return "Vô nghiệm";
                }
                else
                {
                    return "Có 1 nghiệm";
                }
            }

            double delta = b * b - 4 * a * c;

            if (delta < 0)
            {
                return "Vô nghiệm";
            }
            else if (delta == 0)
            {
                return "Có nghiệm kép";
            }
            else
            {
                return "Có 2 nghiệm phân biệt";
            }
        }
    }

    // ==========================================
    // 2. CLASS KIỂM THỬ UNIT TEST DATA-DRIVEN
    // ==========================================
    [TestClass]
    public class Bai05Test
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "bai5.csv",
                    "bai5#csv",
                    DataAccessMethod.Sequential)]
        [DeploymentItem("bai5.csv")]
        public void Test_SolveQuadratic_DataDriven()
        {
            // Đọc trạng thái kỳ vọng văng lỗi từ cột IsExceptionThrown
            bool isExceptionExpected = false;
            if (TestContext.DataRow["IsExceptionThrown"] != DBNull.Value)
            {
                bool.TryParse(TestContext.DataRow["IsExceptionThrown"].ToString().Trim(), out isExceptionExpected);
            }

            // Đọc các giá trị từ file CSV theo đúng tên cột exResult
            string rawA = TestContext.DataRow["a"]?.ToString().Trim() ?? "";
            string rawB = TestContext.DataRow["b"]?.ToString().Trim() ?? "";
            string rawC = TestContext.DataRow["c"]?.ToString().Trim() ?? "";
            string expectedResult = TestContext.DataRow["exResult"]?.ToString().Trim() ?? "";

            // Xử lý kiểm thử
            if (isExceptionExpected)
            {
                try
                {
                    QuadraticEquationSolver.Solve(rawA, rawB, rawC);
                    Assert.Fail("Kỳ vọng văng Exception nhưng chương trình không văng.");
                }
                catch (ArgumentException)
                {
                    // Văng lỗi đúng như kỳ vọng trong CSV -> Test Pass
                    Assert.IsTrue(true);
                }
            }
            else
            {
                string actualResult = QuadraticEquationSolver.Solve(rawA, rawB, rawC);
                Assert.AreEqual(expectedResult, actualResult, $"Lỗi kết quả tại case (a={rawA}, b={rawB}, c={rawC})");
            }
        }
    }
}