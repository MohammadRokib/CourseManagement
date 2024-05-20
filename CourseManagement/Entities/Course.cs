using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Entities
{
    public class Course {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public double CourseFee { get; set; }
        public Teacher Instructor { get; set; }
        public int? InstructorId { get; set; }
        public List<CourseRegistration> RegisteredStudents { get; set; }

        // new Schedule type
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<String>? Weekdays { get; set; }
        public string? Schedule { get; set; }
    }
}
