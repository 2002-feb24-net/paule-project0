using System;
using System.Collections.Generic;

namespace DataBaseTargets.lib.Model
{
    public partial class Topics
    {
        public Topics()
        {
            GeneralStock = new HashSet<GeneralStock>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public virtual ICollection<GeneralStock> GeneralStock { get; set; }
    }
}
