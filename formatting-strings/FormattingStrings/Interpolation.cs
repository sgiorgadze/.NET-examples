using System.Globalization;

namespace FormattingStrings
{
    public static class Interpolation
    {
        public static string GetDepositCsv(int id, string name, string iban, decimal deposit, decimal balance, double interestRate)
        {
            FormattableString csv = $@"{id},""{name}"",{balance:F2},""{interestRate:P2}"",""{deposit:C4}"",{iban}";
            return FormattableString.Invariant(csv);
        }

        public static string GetProductCsv(int id, string name, int supplierId, int categoryId, string quantityPerUnit, double unitPrice, int unitInStock, int unitsOnOrder, int reorderLevel, bool discontinued)
        {
            var a = unitPrice.ToString("F2", CultureInfo.InvariantCulture) + "," + unitInStock.ToString(CultureInfo.InvariantCulture) + "," + unitsOnOrder.ToString(CultureInfo.InvariantCulture) + "," + reorderLevel.ToString(CultureInfo.InvariantCulture);
            var b = discontinued ? 0 : 1;
            FormattableString csv = $@"{id},""{name}"",{supplierId.ToString(CultureInfo.InvariantCulture)},{categoryId.ToString(CultureInfo.InvariantCulture)},""{quantityPerUnit}"",{a},{b}";

            return FormattableString.Invariant(csv);
        }

        public static string GetDepositJson(int id, string name, string iban, decimal deposit, decimal balance, double interestRate)
        {
            FormattableString csv = $@"{{ ""id"": {id}, ""name"": ""{name}"", ""balance"": {balance:F2}, ""rate"": {interestRate:F2}, ""deposit"": {deposit:F4}, ""account"": ""{iban}"" }}";
            return FormattableString.Invariant(csv);
        }

        public static string GetProductXml(int id, string name, int supplierId, int categoryId, string quantityPerUnit, double unitPrice, int unitInStock, int unitsOnOrder, int reorderLevel, bool discontinued)
        {
            string available = discontinued ? "Yes" : "No";
            return $"<product id=\"{id}\" name=\"{name}\" category=\"{categoryId}\" available=\"{available}\">" +
          $"<supplier>{supplierId}</supplier>" +
          $"<quantity>{quantityPerUnit}</quantity>" +
         $"<price>{unitPrice.ToString("F4", CultureInfo.GetCultureInfo("en-US"))}</price>" +
          $"<stock>{unitInStock}</stock>" +
          $"<ordered>{unitsOnOrder}</ordered>" +
          $"<reorder>{reorderLevel}</reorder>" +
          "</product>";
        }

        public static string GetDepositTable(int id, string name, string iban, decimal deposit, decimal balance, double interestRate)
        {
            return $"| {id.ToString(CultureInfo.InvariantCulture),5} | {name,-21}| {balance.ToString("C4", CultureInfo.InvariantCulture),-25}| {interestRate * 100,2:F0} % | {deposit.ToString("N2", CultureInfo.GetCultureInfo("en-US")),-15} | {iban,-24}  |";
        }

        public static string GetProductList(int id, string name, int supplierId, int categoryId, string quantityPerUnit, double unitPrice, int unitInStock, int unitsOnOrder, int reorderLevel, bool discontinued)
        {
            var d = supplierId;
            var f = unitsOnOrder;
            var g = reorderLevel;
            var y = discontinued;
            var x = $"{d}{f}{g}{y}";
            string a = unitPrice.ToString("F2", CultureInfo.InvariantCulture);
            return $"{id,-5}{name,-27}{categoryId} ¤{a,-9}{unitInStock,2} {quantityPerUnit,-20}";
        }
    }
}
