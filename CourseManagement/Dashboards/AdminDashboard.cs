using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.UserMethods;

namespace CourseManagement.Dashboards {
    public class AdminDashboard : Dashboard {
        private string adminPrompt = @"
 █████  ██████  ███    ███ ██ ███    ██     ██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
██   ██ ██   ██ ████  ████ ██ ████   ██     ██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
███████ ██   ██ ██ ████ ██ ██ ██ ██  ██     ██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
██   ██ ██   ██ ██  ██  ██ ██ ██  ██ ██     ██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
██   ██ ██████  ██      ██ ██ ██   ████     ██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                                                                     
                                                                                                                     
";

        public AdminDashboard(App myapp) : base(myapp) { }
        public override void Render() {
            string[] adminOptions = {
                "Create Admin",
                "Create Teacher",
                "Create Student",
                "Create Course",
                "Assign Teacher to course",
                "Enroll Student to course",
                "Logout"
            };

            Menu adminMenu = new Menu(adminOptions, adminPrompt);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex) {
                case 0:
                    AdminMethods.CreateAdmin();
                    Render();
                    break;
                case 1:
                    AdminMethods.CreateTeacher();
                    Render();
                    break;
                case 2:
                    AdminMethods.CreateStudent();
                    Render();
                    break;
                case 3:
                    AdminMethods.CreateCourse();
                    Render();
                    break;
                case 4:
                    AdminMethods.AssignTeacher();
                    Render();
                    break;
                case 5:
                    AdminMethods.EnrollStudent();
                    Render();
                    break;
                case 6:
                    MyApp.Start();
                    Render();
                    break;

            }
        }
    }
}
