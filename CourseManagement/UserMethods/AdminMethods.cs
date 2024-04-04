using CourseManagement.Entities;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static (string, string, string) CreateUser(string userPrompt, string userId) {
            Console.Clear();
            Console.WriteLine(userPrompt);

            (int left, int top) = Console.GetCursorPosition();
            Console.WriteLine("Press Enter to continue or Esc to go back");
            ConsoleKeyInfo confirmationKey = Console.ReadKey(true);

            switch (confirmationKey.Key) {
                case ConsoleKey.Enter:
                    Console.SetCursorPosition(left, top);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(left, top);
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

        public static void CreateAdmin() {
            Admin lastAdmin = _context.Admins
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
            Console.Clear();
            Console.WriteLine(createTeacherPrompt);
            Console.WriteLine("Create Teacher");
            Console.ReadKey(true);
        }
        public static void CreateStudent() {
            Console.Clear();
            Console.WriteLine(createStudentPrompt);
            Console.WriteLine("Create Student");
            Console.ReadKey(true);
        }
        public static void CreateCourse(ApplicationDbContext context) {
            Console.Clear();
            Console.WriteLine(createCoursePrompt);
            Console.WriteLine("Create Course");
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
