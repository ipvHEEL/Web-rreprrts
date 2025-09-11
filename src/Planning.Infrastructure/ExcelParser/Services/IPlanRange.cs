using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClosedXML.Excel;

using Planning.Infrastructure.ExcelParser.Models;

namespace Planning.Infrastructure.ExcelParser.Services
{
    public interface IPlanRange
    {
        List<PlanOrder> Parse(string filePath);
    }
}
