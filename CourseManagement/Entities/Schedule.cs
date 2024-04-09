using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Entities {
    public class Schedule {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ScheduleString { get; set; }
    }
}
