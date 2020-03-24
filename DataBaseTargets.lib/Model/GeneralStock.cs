using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class GeneralStock
    {
        public int StockId { get; set; }
        public int TopicId { get; set; }
        public string StockName { get; set; }
        public decimal Price { get; set; }
        public string StockDescription { get; set; }
        public int? OrderStockId { get; set; }
        public int? StoreStockId { get; set; }

        public virtual OrderStock OrderStock { get; set; }
        public virtual StoreStock StoreStock { get; set; }
        public virtual Topics Topic { get; set; }
    }
}
