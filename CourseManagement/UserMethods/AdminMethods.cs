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

        public static void CreateAdmin() {
            Console.Clear();
            Console.WriteLine(createAdminPrompt);

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
                    return;
                default:
                    CreateAdmin();
                    break;
            }

            string userId = GenerateId();
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
                Admin newAdmin = new Admin {
                    UserId = userId,
                    Name = name,
                    Password = password1
                };

                _context.Add(newAdmin);
                _context.SaveChanges();

                AnsiConsole.Markup("\n[underline green]Admin Created Successfully[/]");
                Utils.WaitForKeyPress();
            } else {
                AnsiConsole.Markup("\n\n[underline red]Something went wrong. Try again[/]");
                Console.ReadKey(true);
                CreateAdmin();
            }

            Console.ReadKey(true);
        }
        public static void CreateTeacher(ApplicationDbContext context) {
            Console.Clear();
            Console.WriteLine(createTeacherPrompt);
            Console.WriteLine("Create Teacher");
            Console.ReadKey(true);
        }
        public static void CreateStudent(ApplicationDbContext context) {
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

        private static string GenerateId() {
            Admin lastAdmin = _context.Admins
                .OrderByDescending(u => u.UserId)
                .FirstOrDefault();

            int newNumericPart = 1;
            if (lastAdmin != null) {
                string numericPart = lastAdmin.UserId.Substring(lastAdmin.UserId.IndexOf('-') + 1);
                newNumericPart = int.Parse(numericPart) + 1;
            }

            string newAdminId = "A-" + newNumericPart.ToString("D3");
            return newAdminId;
        }
    }
}
