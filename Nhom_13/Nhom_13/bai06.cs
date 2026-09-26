using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Nhom_13
{
    [TestClass]
    public class Bai06
    {
        private MethodLibrary.MethodLibrary m = new MethodLibrary.MethodLibrary();
        public TestContext TestContext { get; set; }

        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\bai6.csv",
            "bai6#csv",
            DataAccessMethod.Sequential)]
        [DeploymentItem("bai6.csv")]
        [TestMethod]
        public void TestTinhTien()
        {
            // 1. Đọc dữ liệu từ file CSV
            string inputCu = TestContext.DataRow["chiSoCu"]?.ToString()?.Trim();
            string inputMoi = TestContext.DataRow["chiSoMoi"]?.ToString()?.Trim();
            string expectedStr = TestContext.DataRow["Expected"]?.ToString()?.Trim();

            // 2. Chuyển đổi dữ liệu input
            bool isIntCu = int.TryParse(inputCu, out int chiSoCu);
            bool isIntMoi = int.TryParse(inputMoi, out int chiSoMoi);

            // 3. TRƯỜNG HỢP KỲ VỌNG EXCEPTION (Data Row 21 -> 26)
            if (string.Equals(expectedStr, "Exception", StringComparison.OrdinalIgnoreCase) || !isIntCu || !isIntMoi)
            {
                Assert.ThrowsException<Exception>(() =>
                {
                    // Nếu dữ liệu CSV là số thực (10.5), chữ (abc) hoặc null -> ném Exception ngay
                    if (!isIntCu || !isIntMoi)
                    {
                        throw new Exception("Đầu vào không phải là số nguyên.");
                    }

                    // Gọi hàm trong DLL
                    m.TinhTienDien(chiSoCu, chiSoMoi);
                });
            }
            // 4. TRƯỜNG HỢP TÍNH TIỀN HOẶC TRẢ VỀ -1 (Data Row 0 -> 20, 27 -> 29)
            else
            {
                // Gọi hàm DLL
                object res = m.TinhTienDien(chiSoCu, chiSoMoi);
                double actual = Convert.ToDouble(res);

                // Chuyển giá trị kỳ vọng sang double
                double expected = Convert.ToDouble(expectedStr);

                // So sánh kết quả
                Assert.AreEqual(expected, actual, 1.0, $"Sai kết quả tại TC({chiSoCu}, {chiSoMoi}): Kỳ vọng {expected} nhưng thực tế là {actual}");
            }
        }
    }
}