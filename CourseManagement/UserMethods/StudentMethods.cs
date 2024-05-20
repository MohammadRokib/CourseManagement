using CourseManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.UserMethods {
    public class StudentMethods {
        private static ApplicationDbContext _context = new ApplicationDbContext();
        private static string giveAttendancePrompt = @"
 ██████  ██ ██    ██ ███████      █████  ████████ ████████ ███████ ███    ██ ██████   █████  ███    ██  ██████ ███████ 
██       ██ ██    ██ ██          ██   ██    ██       ██    ██      ████   ██ ██   ██ ██   ██ ████   ██ ██      ██      
██   ███ ██ ██    ██ █████       ███████    ██       ██    █████   ██ ██  ██ ██   ██ ███████ ██ ██  ██ ██      █████   
██    ██ ██  ██  ██  ██          ██   ██    ██       ██    ██      ██  ██ ██ ██   ██ ██   ██ ██  ██ ██ ██      ██      
 ██████  ██   ████   ███████     ██   ██    ██       ██    ███████ ██   ████ ██████  ██   ██ ██   ████  ██████ ███████ 
                                                                                                                       
                                                                                                                       
";
        private static int ongoingClass = 0;
        private static Attendance? attendance;
        private static Student? student;

        public static void PrintEnrolledCourses(string studentId)
        {
            student = _context.Students
                .Where(s => s.UserId == studentId)
                .Include(sc => sc.EnrolledCourses)
                .ThenInclude(scc => scc.Course)
                .ThenInclude(sct => sct.Instructor)
                .FirstOrDefault();

            if (student != null && student.EnrolledCourses.Count > 0)
            {
                DateTime presentTime = DateTime.Now;
                bool matchWeekday = false;
                var table = new Table();
                table.Border = TableBorder.HeavyHead;

                table.AddColumn(new TableColumn("[green]Course ID[/]").Centered());
                table.AddColumn(new TableColumn("[green]Course Name[/]").Centered());
                table.AddColumn(new TableColumn("[green]Instructor[/]").Centered());
                table.AddColumn(new TableColumn("[green]Schedule[/]").Centered());
                table.AddColumn(new TableColumn("[green]Attendance[/]").Centered());

                foreach(var course in student.EnrolledCourses)
                {
                    DateTime courseStartTime = course.Course.StartTime;
                    DateTime courseEndTime = course.Course.EndTime;

                    foreach(string weekday in course.Course.Weekdays)
                    {
                        if (presentTime.DayOfWeek.ToString() == weekday) { 
                            matchWeekday = true;
                            break;
                        }
                    }

                    if (matchWeekday && (presentTime.TimeOfDay >= courseStartTime.TimeOfDay && presentTime.TimeOfDay <= courseEndTime.TimeOfDay))
                    {
                        string? attendanceString;
                        attendance = _context.Attendances
                            .Where(x => x.StudentId == student.Id
                            && x.CourseId == course.CourseId
                            && x.Date.Date == presentTime.Date)
                            .FirstOrDefault();

                        if (attendance == null) attendanceString = "X";
                        else attendanceString = "✓";

                        table.AddRow(
                            $"{course.Course.CourseId}",
                            $"{course.Course.CourseName}",
                            $"{course.Course.Instructor.Name}",
                            $"{course.Course.Schedule}",
                            $"{attendanceString}"
                        );
                        ongoingClass++;
                    }
                }

                AnsiConsole.Write(table);
                Console.WriteLine();
            }
            else
            {
                AnsiConsole.Markup("[blue underline]Not enrolled in any course yet[/]");
            }
        }

        public static void GiveAttendance(string studentId)
        {
            Utils.PrintPrompt(giveAttendancePrompt);
            PrintEnrolledCourses(studentId);

            if (student.EnrolledCourses.Count < 1 || ongoingClass < 1)
            {
                Utils.WaitForKeyPress();
                return;
            }

            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();
            switch (confirmationKey.Key)
            {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    GiveAttendance(studentId);
                    break;
            }

            bool enrolled = false;
            string courseId = AnsiConsole.Ask<string>("[green]Course ID:[/]");
            foreach(var enrolledCourse in student.EnrolledCourses)
            {
                if (enrolledCourse.Course.CourseId == courseId)
                {
                    enrolled = true;
                    break;
                }
            }

            if (!enrolled) {
                AnsiConsole.Markup("\n[underline red]Invalid Course ID\n[/]");
            }
            else if (enrolled && attendance != null) {
                AnsiConsole.Markup("\n[underline blue]Attendance already updated\n[/]");
            }
            else if (enrolled && attendance == null) {
                Course? course = _context.Courses
                    .Where(x => x.CourseId == courseId)
                    .FirstOrDefault();

                Attendance? newAttendance = new Attendance
                {
                    Student = student,
                    StudentId = student.Id,
                    Course = course,
                    CourseId = course.Id,
                    Date = DateTime.Now
                };

                try
                {
                    _context.Add(newAttendance);
                    _context.SaveChanges();
                    AnsiConsole.Markup("\n[underline green]Attendance updated\n[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.Markup("\n[underline red]Something went wrong\n[/]");
                    Console.WriteLine(ex.ToString());
                }
            }
            else {
                AnsiConsole.Markup("\n[underline red]Something went wrong\n[/]");
            }

            Utils.WaitForKeyPress();
            return;
        }
    }
}
