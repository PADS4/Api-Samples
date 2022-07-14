using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleApiExample.RequestModels.Schedule
{
    class ScheduleFilter
    {
        public filter Filter { get; set; }
        public sorting Sorting { get; set; }
        public paging Paging { get; set; }
    }
    class filter
    {
        public string SearchString { get; set; }
        public IList<Guid> Destinations { get; set; }
        public IList<Guid> ScheduleIds { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<int> ScheduleTypeFilter { get; set; }

    }
    class sorting
    {
        public int SortBy { get; set; }
        public bool Descending { get; set; }
    }
}
