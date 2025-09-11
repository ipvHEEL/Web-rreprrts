using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Infrastructure.ExcelParser.Models
{
    public class PlanOrder
    {
        public long Id { get; set; }
        public string Factory { get; set; } = "";
        public string TempMode { get; set; } = "";
        public string Segment { get; set; } = "";
        public int SG { get; set; }
        public long ItemId { get; set; }
        public string ProductName { get; set; } = "";
        public List<PlanOrderQnt> Quantities { get; set; } = new();
    }
}
