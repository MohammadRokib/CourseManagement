using CourseManagement.Entities;
using CourseManagement.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CourseManagement.UserMethods {
    public class AdminMethods {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        private static string createAdminPrompt = @"
 ██████ ██████  ███████  █████  ████████ ███████      █████  ██████  ███    ███ ██ ███    ██ 
██      ██   ██ ██      ██   ██    ██    ██          ██   ██ ██   ██ ████  ████ ██ ████   ██ 
██      ██████  █████   ███████    ██    █████       ███████ ██   ██ ██ ████ ██ ██ ██ ██  ██ 
██      ██   ██ ██      ██   ██    ██    ██          ██   ██ ██   ██ ██  ██  ██ ██ ██  ██ ██ 
 ██████ ██   ██ ███████ ██   ██    ██    ███████     ██   ██ ██████  ██      ██ ██ ██   ████ 
                                                                                             
                                                                                             
";
        private static string createTeacherPrompt = @"
 ██████ ██████  ███████  █████  ████████ ███████     ████████ ███████  █████   ██████ ██   ██ ███████ ██████  
██      ██   ██ ██      ██   ██    ██    ██             ██    ██      ██   ██ ██      ██   ██ ██      ██   ██ 
██      ██████  █████   ███████    ██    █████          ██    █████   ███████ ██      ███████ █████   ██████  
██      ██   ██ ██      ██   ██    ██    ██             ██    ██      ██   ██ ██      ██   ██ ██      ██   ██ 
 ██████ ██   ██ ███████ ██   ██    ██    ███████        ██    ███████ ██   ██  ██████ ██   ██ ███████ ██   ██ 
                                                                                                              
                                                                                                              
";
        private static string createStudentPrompt = @"
 ██████ ██████  ███████  █████  ████████ ███████     ███████ ████████ ██    ██ ██████  ███████ ███    ██ ████████ 
██      ██   ██ ██      ██   ██    ██    ██          ██         ██    ██    ██ ██   ██ ██      ████   ██    ██    
██      ██████  █████   ███████    ██    █████       ███████    ██    ██    ██ ██   ██ █████   ██ ██  ██    ██    
██      ██   ██ ██      ██   ██    ██    ██               ██    ██    ██    ██ ██   ██ ██      ██  ██ ██    ██    
 ██████ ██   ██ ███████ ██   ██    ██    ███████     ███████    ██     ██████  ██████  ███████ ██   ████    ██    
                                                                                                                  
                                                                                                                  
";
        private static string createCoursePrompt = @"
 ██████ ██████  ███████  █████  ████████ ███████      ██████  ██████  ██    ██ ██████  ███████ ███████ 
██      ██   ██ ██      ██   ██    ██    ██          ██      ██    ██ ██    ██ ██   ██ ██      ██      
██      ██████  █████   ███████    ██    █████       ██      ██    ██ ██    ██ ██████  ███████ █████   
██      ██   ██ ██      ██   ██    ██    ██          ██      ██    ██ ██    ██ ██   ██      ██ ██      
 ██████ ██   ██ ███████ ██   ██    ██    ███████      ██████  ██████   ██████  ██   ██ ███████ ███████ 
                                                                                                       
                                                                                                       
";
        private static string scheduleClassPrompt = @"
███████  ██████ ██   ██ ███████ ██████  ██    ██ ██      ███████      ██████ ██       █████  ███████ ███████ 
██      ██      ██   ██ ██      ██   ██ ██    ██ ██      ██          ██      ██      ██   ██ ██      ██      
███████ ██      ███████ █████   ██   ██ ██    ██ ██      █████       ██      ██      ███████ ███████ ███████ 
     ██ ██      ██   ██ ██      ██   ██ ██    ██ ██      ██          ██      ██      ██   ██      ██      ██ 
███████  ██████ ██   ██ ███████ ██████   ██████  ███████ ███████      ██████ ███████ ██   ██ ███████ ███████ 
                                                                                                             
                                                                                                             
";
        private static string assignTeacherPrompt = @"
 █████  ███████ ███████ ██  ██████  ███    ██     ████████ ███████  █████   ██████ ██   ██ ███████ ██████  
██   ██ ██      ██      ██ ██       ████   ██        ██    ██      ██   ██ ██      ██   ██ ██      ██   ██ 
███████ ███████ ███████ ██ ██   ███ ██ ██  ██        ██    █████   ███████ ██      ███████ █████   ██████  
██   ██      ██      ██ ██ ██    ██ ██  ██ ██        ██    ██      ██   ██ ██      ██   ██ ██      ██   ██ 
██   ██ ███████ ███████ ██  ██████  ██   ████        ██    ███████ ██   ██  ██████ ██   ██ ███████ ██   ██ 
                                                                                                           
                                                                                                           
";
        private static string enrollStudentPrompt = @"
███████ ███    ██ ██████   ██████  ██      ██          ███████ ████████ ██    ██ ██████  ███████ ███    ██ ████████ 
██      ████   ██ ██   ██ ██    ██ ██      ██          ██         ██    ██    ██ ██   ██ ██      ████   ██    ██    
█████   ██ ██  ██ ██████  ██    ██ ██      ██          ███████    ██    ██    ██ ██   ██ █████   ██ ██  ██    ██    
██      ██  ██ ██ ██   ██ ██    ██ ██      ██               ██    ██    ██    ██ ██   ██ ██      ██  ██ ██    ██    
███████ ██   ████ ██   ██  ██████  ███████ ███████     ███████    ██     ██████  ██████  ███████ ██   ████    ██    
                                                                                                                    
                                                                                                                    
";
        private static string courseDetailsPrompt = @"
 ██████  ██████  ██    ██ ██████  ███████ ███████     ██████  ███████ ████████  █████  ██ ██      ███████ 
██      ██    ██ ██    ██ ██   ██ ██      ██          ██   ██ ██         ██    ██   ██ ██ ██      ██      
██      ██    ██ ██    ██ ██████  ███████ █████       ██   ██ █████      ██    ███████ ██ ██      ███████ 
██      ██    ██ ██    ██ ██   ██      ██ ██          ██   ██ ██         ██    ██   ██ ██ ██           ██ 
 ██████  ██████   ██████  ██   ██ ███████ ███████     ██████  ███████    ██    ██   ██ ██ ███████ ███████ 
                                                                                                          
                                                                                                          
";

        public static (string?, string?, string?) CreateUser(string userPrompt, string userId) {
            Utils.PrintPrompt(userPrompt);
            switch (userId[0]) {
                case 'A':
                    Utils.PrintUsers(UserType.Admin, _context);
                    break;
                case 'S':
                    Utils.PrintUsers(UserType.Student, _context);
                    break;
                case 'T':
                    Utils.PrintUsers(UserType.Teacher, _context);
                    break;
            }

            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch (confirmationKey.Key) {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return (null, null, null);
                default:
                    CreateUser(userPrompt, userId);
                    break;
            }

            string _userId = userId;
            AnsiConsole.Markup($"[green]UserId[/]: [grey]{userId}[/]\n");
            string name = AnsiConsole.Ask<string>("[green]Name[/]:");
            string password1 = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Enter Password[/]:")
                .PromptStyle("red")
                .Secret()
            );
            string password2 = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Enter Password again[/]:")
                .PromptStyle("red")
                .Secret()
            );

            if (password1 == password2 && name != null && password1 != null) {
                return (_userId, name, password1);
            }

            AnsiConsole.Markup("\n\n[underline red]Something went wrong. Try again[/]");
            return (null, null, null);
        }

        
        public static void CreateAdmin() {
            Admin? lastAdmin = _context.Admins
                .OrderByDescending(u => u.UserId)
                .FirstOrDefault();

            int newNumericPart = 1;
            if (lastAdmin != null) {
                string numericPart = lastAdmin.UserId.Substring(lastAdmin.UserId.IndexOf('-') + 1);
                newNumericPart = int.Parse(numericPart) + 1;
            }

            string userId = "A-" + newNumericPart.ToString("D3");
            (userId, string name, string password) = CreateUser(createAdminPrompt, userId);

            if (userId != null && name != null && password != null) {
                Admin newAdmin = new Admin {
                    UserId = userId,
                    Name = name,
                    Password = password
                };

                _context.Add(newAdmin);
                _context.SaveChanges();

                AnsiConsole.Markup("\n[underline green]Admin Created Successfully[/]");
            }
            Utils.WaitForKeyPress();
        }

        public static void CreateTeacher() {
            Teacher? lastTeacher = _context.Teachers
                .OrderByDescending(u => u.UserId)
                .FirstOrDefault();

            int newNumericPart = 1;
            if (lastTeacher != null) {
                string? numericPart = lastTeacher.UserId.Substring(lastTeacher.UserId.IndexOf('-') + 1);
                newNumericPart = int.Parse(numericPart) + 1;
            }

            string userId = "T-" + newNumericPart.ToString("D3");
            (userId, string name, string password) = CreateUser(createTeacherPrompt, userId);

            if (userId != null && name != null && password != null) {
                Teacher newTeacher = new Teacher {
                    UserId = userId,
                    Name = name,
                    Password = password
                };

                _context.Add(newTeacher);
                _context.SaveChanges();

                AnsiConsole.Markup("\n[underline green]Teacher Created Successfully[/]");
            }
            Utils.WaitForKeyPress();
        }

        public static void CreateStudent() {
            Student? lastStudent = _context.Students
                .OrderByDescending(u => u.UserId)
                .FirstOrDefault();

            int newNumericPart = 1;
            if (lastStudent != null) {
                string numericPart = lastStudent.UserId.Substring(lastStudent.UserId.IndexOf('-') + 1);
                newNumericPart = int.Parse(numericPart) + 1;
            }

            string userId = "S-" + newNumericPart.ToString("D3");
            (userId, string name, string password) = CreateUser(createStudentPrompt, userId);

            if (userId != null && name != null && password != null) {
                Student newStudent = new Student {
                    UserId = userId,
                    Name = name,
                    Password = password
                };

                _context.Add(newStudent);
                _context.SaveChanges();

                AnsiConsole.Markup("\n[underline green]Student Created Successfully[/]");
            }
            Utils.WaitForKeyPress();
        }

        public static void CreateCourse() {
            Utils.PrintPrompt(createCoursePrompt);
            Utils.PrintCourses(_context);

            Course? lastCourse = _context.Courses
                .OrderByDescending(c => c.CourseId)
                .FirstOrDefault();

            int newNumericPart = 1;
            if (lastCourse != null) {
                string numericPart = lastCourse.CourseId.Substring(lastCourse.CourseId.IndexOf('-') + 1);
                newNumericPart = int.Parse(numericPart) + 1;
            }
            string courseId = "C-" + newNumericPart.ToString("D3");

            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();
            switch (confirmationKey.Key) {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    CreateCourse();
                    break;
            }

            AnsiConsole.Markup($"[green]CourseId[/]: [grey]{courseId}[/]\n");
            string courseName = AnsiConsole.Ask<string>("[green]Course Name[/]:");
            double courseFees = AnsiConsole.Ask<double>("[green]Course Fee[/]: ৳");

            if (courseId != null && courseName != null && courseFees > 0) {
                Course newCourse = new Course {
                    CourseId = courseId,
                    CourseName = courseName,
                    CourseFee = courseFees,
                };

                try
                {
                    _context.Add(newCourse);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    Console.WriteLine(ex.ToString());
                }

                AnsiConsole.Markup("\n[underline green]Course Created Successfully[/]");
            }
            Utils.WaitForKeyPress();
        }

        public static void ScheduleClass() {
            Utils.PrintPrompt(scheduleClassPrompt);
            Utils.PrintCourses(_context);
            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch (confirmationKey.Key) {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    ScheduleClass();
                    break;
            }

            string courseId = AnsiConsole.Ask<string>("[green]Course ID[/]:");

            Course? savedCourse = _context.Courses
                .Where(c => c.CourseId == courseId)
                .Include(i => i.Instructor)
                .Include(rs => rs.RegisteredStudents)
                .ThenInclude(rss => rss.Student)
                .FirstOrDefault();
            
            if (savedCourse == null)
            {
                AnsiConsole.Markup("\n[underline red]Invalid Course Id. Try again[/]");
            }
            else
            {
                int classes = AnsiConsole.Ask<int>("[green]Classes in a week[/]:");
                List<string> weekDays = new List<string>();
                for (int i=1; i<=classes; i++)
                {
                    string weekDay = AnsiConsole.Ask<string>($"[green]  Day {i}[/]:");
                    weekDays.Add(weekDay);
                }
                savedCourse.Weekdays = weekDays;

                string startTimeString = AnsiConsole.Ask<string>("[green]  Class start time:[/]");
                DateTime startTime = DateTime.Parse(startTimeString);
                savedCourse.StartTime = startTime;

                string endTimeString = AnsiConsole.Ask<string>("[green]  Class end   time:[/]");
                DateTime endTime = DateTime.Parse(endTimeString);
                savedCourse.EndTime = endTime;

                string? schedule = null;
                foreach (string weekDay in weekDays) { schedule += (weekDay + " "); }
                schedule += (startTimeString + " - " + endTimeString);

                savedCourse.Schedule = schedule;

                try {
                    _context.Update(savedCourse);
                    _context.SaveChanges();
                } catch (Exception ex) { 
                    AnsiConsole.Markup("\n[underline red]Something went wrong. Try again[/]");
                    Console.WriteLine(ex.ToString());
                }
            }

            Utils.WaitForKeyPress();
        }

        public static void AssignTeacher() {
            Utils.PrintPrompt(assignTeacherPrompt);
            Utils.PrintCourses(_context);
            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch(confirmationKey.Key)
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

            string courseId = AnsiConsole.Ask<string>("[green]Course ID:[/]");
            Course? course = _context.Courses
                .Where(c => c.CourseId == courseId)
                .FirstOrDefault();

            if (course == null)
            {
                AnsiConsole.Markup("\n[underline red]Invalid Course ID[/]");
                Utils.WaitForKeyPress();
                AssignTeacher();
            } else if (course.Schedule == null)
            {
                AnsiConsole.Markup("\n[underline red]Course is not scheduled yet[/]");
                Utils.WaitForKeyPress();
                AssignTeacher();
            }
            else
            {
                AnsiConsole.Markup("\n[blue]Available teachers\n[/]");
                Utils.PrintUsers(UserType.Teacher, _context);

                string teacherId = AnsiConsole.Ask<string>("[green]Teacher ID:[/]");
                Teacher? teacher = _context.Teachers
                    .Where(t => t.UserId == teacherId)
                    //.Include(x => x.AssignedCourses)
                    .FirstOrDefault();

                if (teacher == null)
                {
                    AnsiConsole.Markup("\n[underline red]Invalid Teacher ID\n[/]");
                    Utils.WaitForKeyPress();
                    AssignTeacher();
                }
                if (!Utils.TeacherAvailable(teacher, course))
                {
                    AnsiConsole.Markup("\n[underline red]Teacher not available for this course[/]");
                    Utils.WaitForKeyPress();
                    AssignTeacher();
                } else
                {
                    course.InstructorId = teacher.Id;
                    course.Instructor = teacher;

                    _context.Update(teacher);
                    _context.Update(course);
                    _context.SaveChanges();

                    AnsiConsole.Markup("\n[underline green]Teacher successfully assigned to course.[/]");
                }
            }

            Utils.WaitForKeyPress();
            return;
        }

        public static void EnrollStudent() {
            Utils.PrintPrompt(enrollStudentPrompt);
            Utils.PrintCourses(_context);
            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch(confirmationKey.Key)
            {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    EnrollStudent();
                    break;
            }

            string courseId = AnsiConsole.Ask<string>("[green]Course ID:[/]");
            Course? course = _context.Courses
                .Where(c => c.CourseId == courseId)
                .Include(x => x.RegisteredStudents)
                .ThenInclude(y => y.Student)
                .FirstOrDefault();

            if (course == null)
            {
                AnsiConsole.Markup("\n[underline red]Invalid Course ID[/]");
                Utils.WaitForKeyPress();
                EnrollStudent();
            }
            else if (course.Schedule == null) {
                AnsiConsole.Markup("\n[underline red]Course is not scheduled yet[/]");
                Utils.WaitForKeyPress();
                EnrollStudent();
            }
            else
            {
                AnsiConsole.Markup("\n[blue]Select student to enroll\n[/]");
                Utils.PrintUsers(UserType.Student, _context);

                string studentId = AnsiConsole.Ask<string>("[green]Student ID:[/]");
                Student? student = _context.Students
                    .Where(s => s.UserId == studentId)
                    .Include(x => x.EnrolledCourses)
                    .ThenInclude(y => y.Course)
                    .FirstOrDefault();

                if (student == null)
                {
                    AnsiConsole.Markup("\n[underline red]Invalid Student ID[/]");
                    Utils.WaitForKeyPress();
                    EnrollStudent();
                }
                else if (!Utils.StudentAvailable(student, course))
                {
                    AnsiConsole.Markup("\n[underline red]Student has another course at this time[/]");
                    Utils.WaitForKeyPress();
                    EnrollStudent();
                }
                else
                {
                    CourseRegistration newCourseRegistration = new CourseRegistration
                    {
                        CourseId = course.Id,
                        Course = course,
                        StudentId = student.Id,
                        Student = student
                    };

                    course.RegisteredStudents.Add(newCourseRegistration);
                    student.EnrolledCourses.Add(newCourseRegistration);

                    try {
                        _context.Update(course);
                        _context.Update(student);
                        _context.SaveChanges();

                        AnsiConsole.Markup("\n[underline green]Student enrolled to course successfully[/]");
                    } catch (Exception ex) {
                        Console.WriteLine("Something went wrong");
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            Utils.WaitForKeyPress();
        }

        public static void CourseDetails()
        {
            Utils.PrintPrompt(courseDetailsPrompt);
            Utils.PrintCourses(_context);
            (ConsoleKeyInfo confirmationKey, int left, int top) = Utils.GetConfirmationKey();

            switch (confirmationKey.Key)
            {
                case ConsoleKey.Enter:
                    Utils.SetCursorPosition(left, top);
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    CourseDetails();
                    break;
            }

            string courseId = AnsiConsole.Ask<string>("[green]Course ID:[/]");
            Course? course = _context.Courses
                .Where(c => c.CourseId == courseId)
                .Include(x => x.RegisteredStudents)
                .ThenInclude(y => y.Student)
                .FirstOrDefault();

            if (course == null)
            {
                AnsiConsole.Markup("\n[underline red]Invalid Course ID[/]");
                Utils.WaitForKeyPress();
                CourseDetails();
            }
            else
            {
                Utils.PrintPrompt(courseDetailsPrompt);
                AnsiConsole.Markup("\n[underline blue]Course Details\n[/]");

                var table = new Table();
                table.Border = TableBorder.HeavyHead;
                table.AddColumn(new TableColumn("[green]Option[/]"));
                table.AddColumn(new TableColumn("[green]Details[/]"));

                table.AddRow("Course Name", $"{course.CourseName}");
                table.AddRow("Instructor ID", $"{course.Instructor.UserId}");
                table.AddRow("Instructor", $"{course.Instructor.Name}");
                table.AddRow("Course Fee", $"৳ {course.CourseFee}");
                table.AddRow("Schedule", $"{course.Schedule}");
                AnsiConsole.Write(table);

                AnsiConsole.Markup("\n[underline blue]Enrolled Students\n[/]");
                table = new Table();
                table.Border = TableBorder.HeavyHead;
                table.AddColumn(new TableColumn("[green]Student ID[/]").Centered());
                table.AddColumn(new TableColumn("[green]Student Name[/]").Centered());

                foreach(var registeredStudent in course.RegisteredStudents)
                {
                    table.AddRow($"{registeredStudent.Student.UserId}", $"{registeredStudent.Student.Name}");
                }
                AnsiConsole.Write(table);
            }

            Utils.WaitForKeyPress();
        }
    }
}
