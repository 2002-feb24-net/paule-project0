using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class StoreStock
    {
        public StoreStock()
        {
            GeneralStock = new HashSet<GeneralStock>();
        }

        public int StoreStockId { get; set; }
        public int LocationId { get; set; }

        public virtual Locations Location { get; set; }
        public virtual ICollection<GeneralStock> GeneralStock { get; set; }
    }
}
