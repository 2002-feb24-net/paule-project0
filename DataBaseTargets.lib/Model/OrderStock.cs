using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class OrderStock
    {
        public OrderStock()
        {
            GeneralStock = new HashSet<GeneralStock>();
        }

        public int OrderStockId { get; set; }
        public int OrderId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual ICollection<GeneralStock> GeneralStock { get; set; }
    }
}
