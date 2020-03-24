using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrderStock = new HashSet<OrderStock>();
        }

        public int OrderId { get; set; }
        public int? PersonId { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual People Person { get; set; }
        public virtual ICollection<OrderStock> OrderStock { get; set; }
    }
}
