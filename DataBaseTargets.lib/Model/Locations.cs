using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class Locations
    {
        public Locations()
        {
            StoreStock = new HashSet<StoreStock>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<StoreStock> StoreStock { get; set; }
    }
}
