using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Domain.ProductionOrder
{
    public class ProductionOrderDto
    {
        public int PlanDatum { get; set; }
        public int CostCenter { get; set; }
        public long Bom { get; set; }
        public short Variant { get; set; }
        public long ProductionOrder { get; set; }
        public short ProductionOrderPosition { get; set; }
        public long BatchNumber { get; set; }
        public int StartDateExpected { get; set; }
    }
}
