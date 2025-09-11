using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

using Planning.Infrastructure.ExcelParser.Models;

namespace Planning.Infrastructure.ExcelParser.Services
{
    public class PlanQuarterRange : IPlanRange
    {
        public List<PlanOrder> Parse(string filePath)
        {
            var orders = new List<PlanOrder>();
            var dateColumns = new Dictionary<int, DateTime>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet(1);

                // Парсим заголовки для определения дат
                var headerRow = ws.FirstRow();
                int col = 7; // Первая дата в столбце G (18 ноя)

                while (col <= ws.LastCellUsed().Address.ColumnNumber)
                {
                    var cell = headerRow.Cell(col);
                    if (cell.IsEmpty()) break;

                    var headerText = cell.GetString().Trim();
                    if (headerText.StartsWith("итого"))
                    {
                        col++;
                        continue;
                    }

                    if (DateTime.TryParseExact(headerText, "dd MMM",
                         CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out var date))
                    {
                        dateColumns[col] = date;
                    }
                    col++;
                }

                // Обрабатываем строки с данными
                foreach (var row in ws.RowsUsed().Skip(1)) // Пропускаем заголовок
                {
                    var order = new PlanOrder
                    {
                        Factory = row.Cell(1).GetString(),
                        TempMode = row.Cell(2).GetString(),
                        Segment = row.Cell(3).GetString(),
                        SG = row.Cell(4).GetValue<int>(),
                        ItemId = row.Cell(5).GetValue<long>(),
                        ProductName = row.Cell(6).GetString(),
                        Quantities = new List<PlanOrderQnt>()
                    };

                    // Парсим количества по датам
                    foreach (var (colIndex, date) in dateColumns)
                    {
                        var qntCell = row.Cell(colIndex);
                        if (qntCell.IsEmpty()) continue;

                        var qntValue = ParseQuantity(qntCell.GetString());
                        if (qntValue == 0) continue;

                        order.Quantities.Add(new PlanOrderQnt
                        {
                            Date = date,
                            Quantity = qntValue
                        });
                    }

                    orders.Add(order);
                }
            }

            return orders;
        }

        private decimal ParseQuantity(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Trim() == "-")
                return 0;

            // Убираем пробелы между цифрами
            var cleanInput = input.Replace(" ", "");
            return decimal.TryParse(cleanInput, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? result
                : 0;
        }
    }
}
