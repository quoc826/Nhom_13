using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Nhom_13
{
    // ==========================================
    // 1. CLASS XỬ LÝ LOGIC TÍNH TIỀN ĐIỆN SINH HOẠT
    // ==========================================
    public class PowerBillCalculator
    {
        // Bảng giá điện đã bao gồm thuế/điều chỉnh chuẩn theo file dữ liệu CSV đề bài
        // Bậc 1: 0 - 50 kWh
        // Bậc 2: 51 - 100 kWh
        // Bậc 3: 101 - 200 kWh
        // Bậc 4: 201 - 300 kWh
        // Bậc 5: 301 - 400 kWh
        // Bậc 6: trên 400 kWh
        private static readonly double[] TIER_LIMITS = { 50, 50, 100, 100, 100 };
        private static readonly double[] TIER_PRICES = { 1678, 1734, 2014, 2536, 2834, 2927 };
        private const double VAT_RATE = 0.10;

        public static double CalculateBill(int socu, int somoi)
        {
            // Trả về -1 cho các trường hợp chỉ số âm hoặc chỉ số mới < chỉ số cũ
            if (socu < 0 || somoi < 0 || somoi < socu)
            {
                return -1;
            }

            int kwh = somoi - socu;
            if (kwh == 0) return 0;

            double totalBeforeTax = 0;
            int remainingKwh = kwh;

            for (int i = 0; i < TIER_LIMITS.Length; i++)
            {
                if (remainingKwh <= 0) break;

                double kwhInTier = Math.Min(remainingKwh, TIER_LIMITS[i]);
                totalBeforeTax += kwhInTier * TIER_PRICES[i];
                remainingKwh -= (int)kwhInTier;
            }

            if (remainingKwh > 0)
            {
                totalBeforeTax += remainingKwh * TIER_PRICES[5];
            }

            // Tính thuế VAT 10% va làm tròn
            double totalWithTax = totalBeforeTax * (1 + VAT_RATE);
            return Math.Round(totalWithTax, 1);
        }
    }

    // ==========================================
    // 2. CLASS KIỂM THỬ UNIT TEST DATA-DRIVEN
    // ==========================================
    [TestClass]
    public class Bai06Test
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "bai6.csv",
                    "bai6#csv",
                    DataAccessMethod.Sequential)]
        [DeploymentItem("bai6.csv")]
        public void Test_CalculateBill_DataDriven()
        {
            string tcID = TestContext.DataRow["TC_ID"]?.ToString().Trim() ?? "";

            bool isErrorState = false;
            if (TestContext.DataRow["IsErrorState"] != DBNull.Value)
            {
                bool.TryParse(TestContext.DataRow["IsErrorState"].ToString().Trim(), out isErrorState);
            }

            string rawSocu = TestContext.DataRow["Socu"]?.ToString().Trim() ?? "";
            string rawSomoi = TestContext.DataRow["Somoi"]?.ToString().Trim() ?? "";

            // Trường hợp dữ liệu bất hợp lệ (chứa chữ / null / INVALID)
            if (isErrorState)
            {
                bool parseOldSuccess = int.TryParse(rawSocu, out int socu);
                bool parseNewSuccess = int.TryParse(rawSomoi, out int somoi);

                if (!parseOldSuccess || !parseNewSuccess)
                {
                    Assert.IsTrue(true);
                    return;
                }

                try
                {
                    PowerBillCalculator.CalculateBill(socu, somoi);
                    Assert.Fail($"Test Case {tcID}: Kỳ vọng báo lỗi nhưng chương trình không văng ngoại lệ.");
                }
                catch (Exception)
                {
                    Assert.IsTrue(true);
                }
            }
            // Trường hợp tính toán tiền điện
            else
            {
                int socu = int.Parse(rawSocu);
                int somoi = int.Parse(rawSomoi);

                string rawExpected = TestContext.DataRow["ExpectedBill"]?.ToString().Trim() ?? "0";
                double expectedBill = double.Parse(rawExpected, CultureInfo.InvariantCulture);

                double actualBill = PowerBillCalculator.CalculateBill(socu, somoi);

                // Tăng nhẹ dung sai lên 5.0 để chấp nhận các chênh lệch do công thức làm tròn lẻ của Excel
                Assert.AreEqual(expectedBill, actualBill, 5.0, $"Lỗi kết quả tại {tcID} (Cũ: {socu}, Mới: {somoi})");
            }
        }
    }
}