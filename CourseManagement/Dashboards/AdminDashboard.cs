using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class AdminDashboard : Dashboard {
        private string prompt = @"
 █████  ██████  ███    ███ ██ ███    ██     ██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
██   ██ ██   ██ ████  ████ ██ ████   ██     ██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
███████ ██   ██ ██ ████ ██ ██ ██ ██  ██     ██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
██   ██ ██   ██ ██  ██  ██ ██ ██  ██ ██     ██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
██   ██ ██████  ██      ██ ██ ██   ████     ██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                                                                     
                                                                                                                     
";

        public AdminDashboard(App myapp) : base(myapp) { }
        public override void Render() {
            string[] adminOptions = {
                "Create User",
                "Create Course",
                "Assign Teacher to course",
                "Enroll Student to course",
                "Logout"
            };

            Menu adminMenu = new Menu(adminOptions, prompt);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex) {
                case 0:
                    CreateUser();
                    break;
                case 1:
                    CreateCourse();
                    break;
                case 2:
                    AssignTeacher();
                    break;
                case 3:
                    EnrollStudent();
                    break;
                case 4:
                    MyApp.Start();
                    break;

            }
        }

        private void CreateUser() {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Create User");
            Console.ReadKey(true);
            Render();
        }
        private void CreateCourse() {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Create Course");
            Console.ReadKey(true);
            Render();
        }
        private void AssignTeacher() {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Assign Teacher to Course");
            Console.ReadKey(true);
            Render();
        }
        private void EnrollStudent() {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Enroll Student to Course");
            Console.ReadKey(true);
            Render();
        }
    }
}
