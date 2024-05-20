using CourseManagement.Entities;
using CourseManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement
{
    public class Test
    {
        public void Solution()
        {
            DateTime specificTime = DateTime.Parse("4/26/2024 10:00 AM");
            Console.WriteLine(specificTime);
            Console.WriteLine(specificTime.Date);
            Console.WriteLine(specificTime.Day);
            Console.WriteLine(specificTime.DayOfWeek);
            Console.WriteLine(specificTime.DayOfYear);

            Console.Write("Class starting time: ");
            string time = Console.ReadLine();
            DateTime startTime = DateTime.Parse(time);

            Console.Write("Class ending time: ");
            time = Console.ReadLine();
            DateTime endTime = DateTime.Parse(time);

            Console.WriteLine(startTime);
            Console.WriteLine(endTime);

            if (startTime.TimeOfDay <= specificTime.TimeOfDay && endTime.TimeOfDay >= specificTime.TimeOfDay)
            {
                Console.WriteLine("Class time");
            }
            else
            {
                Console.WriteLine("Not class time");
            }
        }

        public void GetSchedule()
        {
            string startingTime, endingTime;
            string[] dayOfWeeks;

            Console.Write("Class days in the week: ");
            dayOfWeeks = Console.ReadLine().Split(' ');

            Console.Write("Class starting time: ");
            startingTime = Console.ReadLine();

            Console.Write("Class ending time: ");
            endingTime = Console.ReadLine();

            foreach (string weekDay in dayOfWeeks)
                Console.WriteLine(weekDay);

            Console.WriteLine(startingTime);
            Console.WriteLine(endingTime);
        }

        public bool StudentAvailabel(Student student, Course course)
        {
            DateTime crStartTime = course.StartTime;
            DateTime crEndTime = course.EndTime;
            bool available = true;
            foreach(var studentCourse in student.EnrolledCourses)
            {
                DateTime stStartTime = studentCourse.Course.StartTime;
                DateTime stEndtime = studentCourse.Course.EndTime;

                if (studentCourse.Course.CourseId == course.CourseId) { return false; }
                foreach(string wdStudent in studentCourse.Course.Weekdays)
                {
                    foreach(string wdCourse in course.Weekdays) {
                        if (wdStudent == wdCourse) available = false;
                    }
                }
                if (available)
                {
                    if ((stStartTime.TimeOfDay >= crStartTime.TimeOfDay && stStartTime.TimeOfDay <= crEndTime.TimeOfDay) ||
                        (stEndtime.TimeOfDay >= crStartTime.TimeOfDay && stEndtime.TimeOfDay <= crEndTime.TimeOfDay))
                    { return false; }
                }
            }
            return true;
        }

        public void BasicDashBoarda()
        {
            /*Utils.PrintPrompt(assignTeacherPrompt);
            PrintUser(UserType.Teacher);
            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch (confirmationKey.Key)
            {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    AssignTeacher();
                    break;
            }
            Console.WriteLine("Assign Teacher to Course");

            Utils.WaitForKeyPress();*/
        }
    }
}



/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Entities {
    public class Schedule {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
*/