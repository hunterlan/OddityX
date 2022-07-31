using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddityX.Helpers.HistorySpaceX
{
    public class HistoryModel
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        public DateTime DateUtc { get; set; }

        public long DateUnix { get; set; }

        public string Details { get; set; }

        private List<string> Links { get; set; }
    }
}
