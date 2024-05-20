using CourseManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.UserMethods {
    public class TeacherMehtods {
        private static ApplicationDbContext _context = new ApplicationDbContext();
        private static string checkAttendancePrompt = @"
 ██████ ██   ██ ███████  ██████ ██   ██      █████  ████████ ████████ ███████ ███    ██ ██████   █████  ███    ██  ██████ ███████ 
██      ██   ██ ██      ██      ██  ██      ██   ██    ██       ██    ██      ████   ██ ██   ██ ██   ██ ████   ██ ██      ██      
██      ███████ █████   ██      █████       ███████    ██       ██    █████   ██ ██  ██ ██   ██ ███████ ██ ██  ██ ██      █████   
██      ██   ██ ██      ██      ██  ██      ██   ██    ██       ██    ██      ██  ██ ██ ██   ██ ██   ██ ██  ██ ██ ██      ██      
 ██████ ██   ██ ███████  ██████ ██   ██     ██   ██    ██       ██    ███████ ██   ████ ██████  ██   ██ ██   ████  ██████ ███████ 
                                                                                                                                  
                                                                                                                                  
";
        private static Teacher? teacher;
        private static int ongoingClass = 0;

        public static void PrintAssignedCourses(string teacherId) {
            teacher = _context.Teachers
                .Where(t => t.UserId == teacherId)
                .Include(ta => ta.AssignedCourses)
                .FirstOrDefault();

            if (teacher != null && teacher.AssignedCourses.Count > 0)
            {
                var table = new Table();
                table.AddColumn(new TableColumn("[green]Course ID[/]").Centered());
                table.AddColumn(new TableColumn("[green]Course Name[/]").Centered());
                table.AddColumn(new TableColumn("[green]Course Schedule[/]").Centered());

                DateTime presentTime = DateTime.Now;
                foreach(var assignedCourse in teacher.AssignedCourses)
                {
                    bool matchWeekdays = false;
                    foreach(string weekDay in assignedCourse.Weekdays)
                    {
                        if (presentTime.DayOfWeek.ToString() == weekDay)
                        {
                            matchWeekdays = true;
                            break;
                        }
                    }
                    
                    if (matchWeekdays)
                    {
                        table.AddRow(
                            $"{assignedCourse.CourseId}",
                            $"{assignedCourse.CourseName}",
                            $"{assignedCourse.Schedule}"
                        );
                        ongoingClass++;
                    }
                }

                AnsiConsole.Write(table);
                Console.WriteLine();
            }
            else
            {
                AnsiConsole.Markup("\n[underline blue]Not assigned to any course yet\n[/]");
            }
        }
        public static void CheckAttendance(string teacherId) {
            Utils.PrintPrompt(checkAttendancePrompt);
            PrintAssignedCourses(teacherId);

            if (teacher.AssignedCourses.Count < 1 || ongoingClass < 1) {
                AnsiConsole.MarkupLine("[underline blue]Not assigned to any course yet[/]");
                Utils.WaitForKeyPress();
                return;
            }

            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();
            switch(confirmationKey.Key)
            {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    CheckAttendance(teacherId);
                    break;
            }

            bool assignedToCourse = false;
            string courseId = AnsiConsole.Ask<string>("[green]Course ID:[/]");
            foreach(Course assignedCourse in teacher.AssignedCourses) {
                if (assignedCourse.CourseId == courseId) {
                    foreach(string weekDay in assignedCourse.Weekdays) {
                        if (weekDay == DateTime.Now.DayOfWeek.ToString()) {
                            assignedToCourse = true;
                            break;
                        }
                    }
                    if (assignedToCourse) break;
                }
            }

            if (assignedToCourse)
            {
                Course? course = _context.Courses
                    .Where(c => c.CourseId == courseId)
                    .Include(ci => ci.Instructor)
                    .Include(cr => cr.RegisteredStudents)
                    .ThenInclude(crs => crs.Student)
                    .FirstOrDefault();

                if (course == null)
                {
                    AnsiConsole.MarkupLine("[underline red]Invalid Course ID[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("\n[underline blue]Course Details[/]");

                    var table = new Table();
                    table.Border = TableBorder.HeavyHead;
                    table.AddColumn(new TableColumn("[green]Option[/]"));
                    table.AddColumn(new TableColumn("[green]Details[/]"));

                    table.AddRow("Course Name", $"{course.CourseName}");
                    table.AddRow("Schedule", $"{course.Schedule}");
                    AnsiConsole.Write(table);

                    AnsiConsole.MarkupLine("\n[underline blue]Attendence List[/]");
                    table = new Table();
                    table.Border = TableBorder.HeavyHead;
                    table.AddColumn(new TableColumn("[green]Student ID[/]").Centered());
                    table.AddColumn(new TableColumn("[green]Student Name[/]").Centered());
                    table.AddColumn(new TableColumn("[green]Attendence[/]").Centered());

                    DateTime presentTime = DateTime.Now;
                    if (course.RegisteredStudents.Count > 0)
                    {
                        foreach (var enrolledStudent in course.RegisteredStudents)
                        {
                            string? attendanceStatus = "X";
                            Attendance? attendance = _context.Attendances
                                .Where(
                                    a => a.CourseId == course.Id &&
                                    a.StudentId == enrolledStudent.StudentId &&
                                    a.Date.Date == presentTime.Date
                                )
                                .FirstOrDefault();

                            if (attendance != null) attendanceStatus = "✓";
                            table.AddRow(
                                $"{enrolledStudent.Student.UserId}",
                                $"{enrolledStudent.Student.Name}",
                                $"{attendanceStatus}"
                            );
                        }
                    }
                    AnsiConsole.Write(table);
                }
            }
            else AnsiConsole.MarkupLine("[underline red]Invalid Course ID[/]");

            Utils.WaitForKeyPress();
            return;
        }
    }
}
