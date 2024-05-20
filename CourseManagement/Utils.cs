using CourseManagement.Entities;
using CourseManagement.Enums;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    static class Utils {
        public static void WaitForKeyPress() {
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey(true);
        }

        public static void QuitConsole() {
            Console.WriteLine("\n\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        public static void PrintPrompt(string prompt) {
            Console.Clear();
            Console.WriteLine(prompt);
        }

        public static (ConsoleKeyInfo, int, int) GetConfirmationKey() {
            (int left, int top) = Console.GetCursorPosition();
            Console.WriteLine("Press Enter to continue or Esc to go back");
            ConsoleKeyInfo confirmationKey = Console.ReadKey(true);

            return (confirmationKey, left, top);
        }

        public static void SetCursorPosition(int left, int top) {
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(left, top);
        }

        public static bool TeacherAvailable(Teacher teacher, Course course)
        {
            DateTime courseStartTime = course.StartTime;
            DateTime courseEndTime = course.EndTime;
            bool available = true;

            if (teacher.AssignedCourses != null)
            {
                foreach(var teacherCourse in teacher.AssignedCourses)
                {
                    DateTime teacherStartTime = teacherCourse.StartTime;
                    DateTime teacherEndTime = teacherCourse.EndTime;

                    if (teacherCourse.CourseId == course.CourseId) { return false; }
                    foreach(string wdTeacher in teacherCourse.Weekdays)
                    {
                        foreach (string wdCourse in course.Weekdays)
                        {
                            if (wdTeacher == wdCourse)
                            {
                                available = false;
                                break;
                            }
                        }
                        if (!available) break;
                    }

                    if (!available)
                    {
                        if ((teacherStartTime >= courseStartTime && teacherStartTime <= courseEndTime) ||
                            (teacherEndTime >= courseStartTime && teacherEndTime <= courseEndTime))
                        { return false; }
                    }
                }
            }

            return true;
        }

        public static bool StudentAvailable(Student student, Course course)
        {
            DateTime courseStartTime = course.StartTime;
            DateTime courseEndTime = course.EndTime;
            bool available = true;

            if (student.EnrolledCourses != null)
            {
                foreach(var enrolledCourse in student.EnrolledCourses)
                {
                    if (enrolledCourse.CourseId == course.Id) { return false; }

                    DateTime studentStartTime = enrolledCourse.Course.StartTime;
                    DateTime studentEndTime = enrolledCourse.Course.EndTime;
                    foreach(string wdStudent in enrolledCourse.Course.Weekdays)
                    {
                        foreach(string wdCourse in course.Weekdays)
                        {
                            if (wdCourse == wdStudent)
                            {
                                available = false;
                                break;
                            }
                        }
                        if (!available) break;
                    }

                    if (!available)
                    {
                        if ((studentStartTime >= courseStartTime && studentStartTime <= courseEndTime) ||
                            (studentEndTime >= courseStartTime && studentEndTime <= courseEndTime))
                        { return false; }
                    }
                }
            }
            return true;
        }

        public static void PrintUsers(UserType userType, ApplicationDbContext _context)
        {
            List<Admin> admins = new List<Admin>();
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();

            switch (userType)
            {
                case UserType.Admin:
                    admins = _context.Admins.ToList();
                    break;
                case UserType.Teacher:
                    teachers = _context.Teachers.ToList();
                    break;
                case UserType.Student:
                    students = _context.Students.ToList();
                    break;
            }

            var table = new Table();
            table.Border = TableBorder.HeavyHead;

            table.AddColumn(new TableColumn("[green]ID[/]").Centered());
            table.AddColumn(new TableColumn("[green]Name[/]").Centered());

            if (students != null)
            {
                foreach (Student student in students)
                {
                    table.AddRow($"{student.UserId}", $"{student.Name}");
                }
            }
            if (teachers != null)
            {
                foreach (Teacher teacher in teachers)
                {
                    table.AddRow($"{teacher.UserId}", $"{teacher.Name}");
                }
            }
            if (admins != null)
            {
                foreach (Admin admin in admins)
                {
                    table.AddRow($"{admin.UserId}", $"{admin.Name}");
                }
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
        }

        public static void PrintCourses(ApplicationDbContext _context)
        {
            List<Course> courses = _context.Courses
                .Include(c => c.Instructor)
                .ToList();

            var table = new Table();
            table.Border = TableBorder.HeavyHead;

            table.AddColumn(new TableColumn("[green]Course ID[/]").Centered());
            table.AddColumn(new TableColumn("[green]Course Name[/]").Centered());
            table.AddColumn(new TableColumn("[green]Course Instructor[/]").Centered());
            table.AddColumn(new TableColumn("[green]Schedule[/]").Centered());
            table.AddColumn(new TableColumn("[green]Course Fee[/]").Centered());

            if (courses != null)
            {
                foreach (Course course in courses)
                {
                    string? instructorName = " ";
                    if (course.Instructor != null)
                        instructorName = course.Instructor.Name;

                    table.AddRow(
                        $"{course.CourseId}",
                        $"{course.CourseName}",
                        $"{instructorName}",
                        $"{course.Schedule}",
                        $"৳ {course.CourseFee}"
                    );
                }
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
        }
    }
}
