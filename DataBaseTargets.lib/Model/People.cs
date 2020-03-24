using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class People
    {
        public People()
        {
            Orders = new HashSet<Orders>();
        }

        public int PersonId { get; set; }
        public string Username { get; set; }
        public int LocationId { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
