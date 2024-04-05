using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.UserMethods;

namespace CourseManagement.Dashboards {
    public class AdminDashboard : Dashboard {
        private readonly ApplicationDbContext _context;
        private string adminPrompt = @"
 █████  ██████  ███    ███ ██ ███    ██     ██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
██   ██ ██   ██ ████  ████ ██ ████   ██     ██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
███████ ██   ██ ██ ████ ██ ██ ██ ██  ██     ██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
██   ██ ██   ██ ██  ██  ██ ██ ██  ██ ██     ██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
██   ██ ██████  ██      ██ ██ ██   ████     ██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                                                                     
                                                                                                                     
";

        public AdminDashboard(App myapp, ApplicationDbContext context) : base(myapp, context) {
            _context = context;
        }
        public override void Render() {
            string[] adminOptions = {
                "Create Admin",
                "Create Teacher",
                "Create Student",
                "Create Course",
                "Schedule Class",
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
                    AdminMethods.CreateCourse(_context);
                    Render();
                    break;
                case 4:
                    AdminMethods.ScheduleClass();
                    Render();
                    break;
                case 5:
                    AdminMethods.AssignTeacher(_context);
                    Render();
                    break;
                case 6:
                    AdminMethods.EnrollStudent(_context);
                    Render();
                    break;
                case 7:
                    MyApp.Start();
                    Render();
                    break;

            }
        }
    }
}
