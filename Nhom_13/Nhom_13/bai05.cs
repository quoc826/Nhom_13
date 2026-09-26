using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai05
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai5.csv",
            "bai5#csv",
            DataAccessMethod.Sequential)]
        [DeploymentItem("bai5.csv")]
        [TestMethod]
        public void TestSolveQuadratic()
        {
            // 1. Đọc dữ liệu từ file CSV
            string inputA = TestContext.DataRow["a"]?.ToString()?.Trim();
            string inputB = TestContext.DataRow["b"]?.ToString()?.Trim();
            string inputC = TestContext.DataRow["c"]?.ToString()?.Trim();
            string expectedMsg = TestContext.DataRow["ExpectedMsg"]?.ToString()?.Trim();
            string expectedX1Str = TestContext.DataRow["ExpectedX1"]?.ToString()?.Trim();
            string expectedX2Str = TestContext.DataRow["ExpectedX2"]?.ToString()?.Trim();

            // 2. Chuyển đổi dữ liệu input
            bool isIntA = int.TryParse(inputA, out int a);
            bool isIntB = int.TryParse(inputB, out int b);
            bool isIntC = int.TryParse(inputC, out int c);

            // 3. Trường hợp Exception (Đầu vào không hợp lệ)
            if (string.Equals(expectedMsg, "Exception", StringComparison.OrdinalIgnoreCase))
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    if (!isIntA || !isIntB || !isIntC)
                    {
                        throw new Exception("Đầu vào không phải số nguyên.");
                    }
                    m.SolveQuadratic(a, b, c, out float x1, out float x2);
                });
            }
            // 4. Trường hợp hợp lệ
            else
            {
                Assert.IsTrue(isIntA && isIntB && isIntC, $"Đầu vào ({inputA}, {inputB}, {inputC}) phải là số nguyên.");

                // Gọi hàm thực tế
                string actualMsg = m.SolveQuadratic(a, b, c, out float actualX1, out float actualX2);

                // a) Kiểm tra thông báo
                if (expectedMsg.Equals("Vo so nghiem", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(actualMsg.Contains("Vô số nghiệm") || actualMsg.Contains("Vo so nghiem"));
                }
                else if (expectedMsg.Equals("Vo nghiem", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(actualMsg.Contains("Vô nghiệm") || actualMsg.Contains("Vo nghiem"));
                }
                else if (expectedMsg.Equals("Co 1 nghiem", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(actualMsg.Contains("1 nghiệm") || actualMsg.Contains("1 nghiem"));
                }
                else if (expectedMsg.Equals("Co nghiem kep", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(actualMsg.Contains("nghiệm kép") || actualMsg.Contains("nghiem kep"));
                }
                else if (expectedMsg.Equals("Co 2 nghiem phan biet", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(actualMsg.Contains("2 nghiệm") || actualMsg.Contains("2 nghiem"));
                }

                // b) So sánh nghiệm x1 (SỬA LỖI NaN TẠI ĐÂY)
                if (string.Equals(expectedX1Str, "NaN", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(float.IsNaN(actualX1), $"Sai x1 tại TC({a}, {b}, {c}): Kỳ vọng NaN nhưng thực tế là {actualX1}");
                }
                else if (float.TryParse(expectedX1Str, out float expX1))
                {
                    Assert.AreEqual(expX1, actualX1, 0.001f, $"Sai x1 tại TC({a}, {b}, {c})");
                }

                // c) So sánh nghiệm x2 (SỬA LỖI NaN TẠI ĐÂY)
                if (string.Equals(expectedX2Str, "NaN", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(float.IsNaN(actualX2), $"Sai x2 tại TC({a}, {b}, {c}): Kỳ vọng NaN nhưng thực tế là {actualX2}");
                }
                else if (float.TryParse(expectedX2Str, out float expX2))
                {
                    Assert.AreEqual(expX2, actualX2, 0.001f, $"Sai x2 tại TC({a}, {b}, {c})");
                }
            }
        }
    }
}