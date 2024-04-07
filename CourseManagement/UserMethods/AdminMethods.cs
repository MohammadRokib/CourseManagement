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

        public static (string?, string?, string?) CreateUser(string userPrompt, string userId) {
            Utils.PrintPrompt(userPrompt);
            switch (userId[0]) {
                case 'A':
                    PrintUser(UserType.Admin);
                    break;
                case 'S':
                    PrintUser(UserType.Student);
                    break;
                case 'T':
                    PrintUser(UserType.Teacher);
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
            Console.ReadKey(true);
            return (null, null, null);
        }
        public static void PrintUser(UserType userType) {
            List<Admin> admins = new List<Admin>();
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();

            switch (userType) {
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

            if (students != null) {
                foreach(Student student in students) {
                    table.AddRow($"{student.UserId}", $"{student.Name}");
                }
            }
            if (teachers != null) {
                foreach (Teacher teacher in teachers) {
                    table.AddRow($"{teacher.UserId}", $"{teacher.Name}");
                }
            }
            if (admins != null) {
                foreach (Admin admin in admins) {
                    table.AddRow($"{admin.UserId}", $"{admin.Name}");
                }
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
        }

        public static void PrintCourses() {
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

            if (courses != null) {
                foreach(Course course in courses) {
                    string? instructorName = " ";
                    if (course.Instructor != null)
                        instructorName = course.Instructor.Name;

                    table.AddRow(
                        $"{course.CourseId}",
                        $"{course.CourseName}",
                        $"{instructorName}",
                        $"{course.Schedule}",
                        $"{course.CourseFee}"
                    );
                }
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
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
            PrintCourses();

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
            double courseFees = AnsiConsole.Ask<double>("[green]Course Fee[/]:");

            if (courseId != null && courseName != null && courseFees > 0) {
                Course newCourse = new Course {
                    CourseId = courseId,
                    CourseName = courseName,
                    CourseFee = courseFees
                };

                _context.Add(newCourse);
                _context.SaveChanges();

                AnsiConsole.Markup("\n[underline green]Course Created Successfully[/]");
            }
            Utils.WaitForKeyPress();
        }
        public static void ScheduleClass() {
            Console.Clear();
            Console.WriteLine(scheduleClassPrompt);
            PrintCourses();

            Console.WriteLine("Schedule Class");
            Console.ReadKey(true);
        }
        public static void AssignTeacher(ApplicationDbContext context) {
            Console.Clear();
            Console.WriteLine(assignTeacherPrompt);
            Console.WriteLine("Assign Teacher to Course");
            Console.ReadKey(true);
        }
        public static void EnrollStudent(ApplicationDbContext context) {
            Console.Clear();
            Console.WriteLine(enrollStudentPrompt);
            Console.WriteLine("Enroll Student to Course");
            Console.ReadKey(true);
        }
    }
}
