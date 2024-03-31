using CourseManagement.Dashboards;
using CourseManagement.Entities;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    public class App {
        private readonly ApplicationDbContext _context;

        public StudentDashboard studentDashboard;
        public TeacherDashboard teacherDashboard;
        public AdminDashboard adminDashboard;
        private string loginPrompt = @"
██       ██████   ██████      ██ ███    ██ 
██      ██    ██ ██           ██ ████   ██ 
██      ██    ██ ██   ███     ██ ██ ██  ██ 
██      ██    ██ ██    ██     ██ ██  ██ ██ 
███████  ██████   ██████      ██ ██   ████ 
                                           
                                           
";

        public App(ApplicationDbContext context) {
            _context = context;

            adminDashboard = new AdminDashboard(this, _context);
            studentDashboard = new StudentDashboard(this, _context);
            teacherDashboard = new TeacherDashboard(this, _context);
        }
        public void Start() {
            int selectedIndex = Dashboard();
            switch (selectedIndex) {
                case 1:
                    Utils.QuitConsole();
                    break;
            }

            (bool loginSuccess, string userId) = Login();

            if (!loginSuccess) {
                Console.WriteLine("Invalid credentials. Press any key");
                Console.ReadKey(true);
                Start();
            }

            switch (userId[0]) {
                case 'A':
                    adminDashboard.Render();
                    break;
                case 'T':
                    teacherDashboard.Render();
                    break;
                case 'S':
                    studentDashboard.Render();
                    break;
            }
        }

        private int Dashboard() {
            string dashboardPrompt = @"
██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                         
                                                                         
";
            string[] dashboardOptions = {
                "Login",
                "Exit"
            };

            Menu dashboardMenu = new Menu(dashboardOptions, dashboardPrompt);
            return dashboardMenu.Run();
        }

        public (bool, string) Login() {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(loginPrompt);

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
                    Start();
                    break;
                default:
                    Login();
                    break;
            }

            string userid = AnsiConsole.Ask<string>("[green]User Id[/]:");
            string password = (string)AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("red")
                .Secret('-')
            );

            User user = new User();
            if (userid != null && password != null) {
                switch (userid[0]) {
                    case 'A':
                        user = _context.Admins.Where(x => x.UserId == userid)
                            .FirstOrDefault();
                        break;
                    case 'T':
                        user = _context.Teachers.Where(x => x.UserId == userid)
                            .FirstOrDefault();
                        break;
                    case 'S':
                        user = _context.Student.Where(x => x.UserId == userid)
                            .FirstOrDefault();
                        break;
                }

                if (user != null) {
                    if (user is Admin adminUser && adminUser.Password == password) {
                        return (true, adminUser.UserId);
                    } else if (user is Teacher teacherUser && teacherUser.Password == password) {
                        return (true, teacherUser.UserId);
                    } else if (user is Student studentUser && studentUser.Password == password) {
                        return (true, studentUser.UserId);
                    } else {
                        return (false, null);
                    }
                }
            }

            return (false, null);
        }
    }
}
