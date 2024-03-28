using CourseManagement.Dashboards;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    public class App {
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
        private string Username;
        private string UserId;

        public App() {
            adminDashboard = new AdminDashboard(this);
            studentDashboard = new StudentDashboard(this);
            teacherDashboard = new TeacherDashboard(this);
        }
        public void Start() {
            int selectedIndex = Dashboard();
            switch (selectedIndex) {
                case 1:
                    Utils.QuitConsole();
                    break;
            }

            bool loginSuccess = Login();

            if (!loginSuccess) {
                Console.WriteLine("Invalid credentials");
                Start();
            }

            if (Username == "admin")
                adminDashboard.Render();
            else if (Username == "student")
                studentDashboard.Render();
            else if (Username == "teacher")
                teacherDashboard.Render();

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

        public bool Login() {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(loginPrompt);

            Username = AnsiConsole.Ask<string>("[green]Username[/]:");
            string Password = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("red")
                .Secret()
            );

            if (Username != null && Password != null) {
                UserId = "user-001";
                return true;
            }
            return false;
        }
        private void RunMainMenu() {
            string prefix = "\u001b[33m";
            string postfix = "\u001b[0m";

            string prompt = @$"{prefix}
____ ____ _  _ ____ ____ ____    _  _ ____ _  _ ____ ____ ____ _  _ ____ _  _ ___    ____ _   _ ____ ___ ____ _  _ 
|    |  | |  | |__/ [__  |___    |\/| |__| |\ | |__| | __ |___ |\/| |___ |\ |  |     [__   \_/  [__   |  |___ |\/| 
|___ |__| |__| |  \ ___] |___    |  | |  | | \| |  | |__] |___ |  | |___ | \|  |     ___]   |   ___]  |  |___ |  | 
                                                                                                                   
{postfix}";
            string[] options = {
                "Login",
                "Exit"
            };

            Menu mainMenu = new Menu(options, prompt);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex) {
                case 0:
                    LoginMenu();
                    break;
                case 1:
                    ExitApp();
                    break;

            }
        }
        private void LoginMenu() {
            string prefix = "\u001b[33m";
            string postfix = "\u001b[0m";
            string prompt = @$"{prefix}
  _                     ___        
 | |     ___   __ _    |_ _|  _ _  
 | |__  / _ \ / _` |    | |  | ' \ 
 |____| \___/ \__, |   |___| |_||_|
              |___/                
{postfix}";

            Menu loginMenu = new Menu(new[] {"empty"}, prompt);
            loginMenu.Login();
            ExitApp();
        }
        private void ExitApp() {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
