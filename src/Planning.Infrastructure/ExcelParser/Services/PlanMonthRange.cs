using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Planning.Infrastructure.ExcelParser.Extension;
using Planning.Infrastructure.ExcelParser.Models;

namespace Planning.Infrastructure.ExcelParser.Services
{
    public class PlanMonthRange : IPlanRange
    {
        private readonly ExcelConfig _config;
        private readonly Dictionary<string, int> _columnMappings = new();

        public PlanMonthRange(ExcelConfig config)
        {
            _config = config;
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            foreach (var column in _config.Columns)
            {
                var index = column.Index > 0 ?
                    column.Index :
                    XLHelper.GetColumnNumberFromLetter(column.A1Notation);

                _columnMappings[column.Name] = index;
            }
        }

        public List<PlanOrder> Parse(string filePath)
        {
            var orders = new List<PlanOrder>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var targetSheet = workbook.Worksheet(_config.SheetName);
                if (targetSheet == null) throw new Exception("Sheet not found");

                var firstDateCol = _config.FirstDateColumn.Index > 0 ?
                    _config.FirstDateColumn.Index :
                    XLHelper.GetColumnNumberFromLetter(_config.FirstDateColumn.A1Notation);

                var dateColumns = ParseDateColumns(targetSheet, firstDateCol);
                ParseDataRows(targetSheet, dateColumns, orders);
            }

            return orders;
        }

        private Dictionary<int, DateTime> ParseDateColumns(IXLWorksheet ws, int firstDateCol)
        {
            var dateColumns = new Dictionary<int, DateTime>();
            var headerRow = ws.FirstRow();

            for (int col = firstDateCol; col <= ws.LastCellUsed().Address.ColumnNumber; col++)
            {
                var cell = headerRow.Cell(col);
                if (cell.IsEmpty()) continue;

                var headerText = cell.GetString().Trim();
                if (headerText.StartsWith("итого")) continue;

                if (DateTime.TryParseExact(headerText,
                    new[] { "dd MMM", "dd.MM.yy", "d MMM" },
                    CultureInfo.GetCultureInfo("ru-RU"),
                    DateTimeStyles.None,
                    out var date))
                {
                    dateColumns[col] = date;
                }
            }
            return dateColumns;
        }

        private void ParseDataRows(IXLWorksheet ws, Dictionary<int, DateTime> dateColumns, List<PlanOrder> orders)
        {
            foreach (var row in ws.RowsUsed().Skip(1))
            {
                var factory = GetCellValue(row, _columnMappings["Factory"]);
                if (!_config.AllowedFactories.Contains(factory)) continue;

                var order = new PlanOrder
                {
                    Factory = factory,
                    TempMode = GetCellValue(row, _columnMappings["TempMode"]),
                    Segment = GetCellValue(row, _columnMappings["Segment"]),
                    SG = GetCellValueInt(row, _columnMappings["SG"]),
                    ItemId = GetCellValueLong(row, _columnMappings["ItemId"]),
                    ProductName = GetCellValue(row, _columnMappings["ProductName"])
                };

                foreach (var (col, date) in dateColumns)
                {
                    var quantity = ParseQuantity(GetCellValue(row, col));
                    if (quantity > 0)
                    {
                        order.Quantities.Add(new PlanOrderQnt
                        {
                            Date = date,
                            Quantity = quantity
                        });
                    }
                }
                orders.Add(order);
            }
        }

        private string GetCellValue(IXLRow row, int column) =>
            row.Cell(column).GetString().Trim();

        private int GetCellValueInt(IXLRow row, int column) =>
            row.Cell(column).TryGetValue<int>(out var value) ? value : 0;

        private long GetCellValueLong(IXLRow row, int column) =>
            row.Cell(column).TryGetValue<long>(out var value) ? value : 0;

        private decimal ParseQuantity(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            var cleanInput = input.Replace(" ", "").Replace("-", "");
            return decimal.TryParse(cleanInput, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? result
                : 0;
        }
    }
}
