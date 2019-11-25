using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class TimelineViewModel
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
