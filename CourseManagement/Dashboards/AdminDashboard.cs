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
        public override void Render(string adminId) {
            string[] adminOptions = {
                "Create Admin",
                "Create Teacher",
                "Create Student",
                "Create Course",
                "Schedule Class",
                "Assign Teacher to course",
                "Enroll Student to course",
                "Course Details",
                "Logout"
            };

            Menu adminMenu = new Menu(adminOptions, adminPrompt);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex) {
                case 0:
                    AdminMethods.CreateAdmin();
                    Render(adminId);
                    break;
                case 1:
                    AdminMethods.CreateTeacher();
                    Render(adminId);
                    break;
                case 2:
                    AdminMethods.CreateStudent();
                    Render(adminId);
                    break;
                case 3:
                    AdminMethods.CreateCourse();
                    Render(adminId);
                    break;
                case 4:
                    AdminMethods.ScheduleClass();
                    Render(adminId);
                    break;
                case 5:
                    AdminMethods.AssignTeacher();
                    Render(adminId);
                    break;
                case 6:
                    AdminMethods.EnrollStudent();
                    Render(adminId);
                    break;
                case 7:
                    AdminMethods.CourseDetails();
                    Render(adminId);
                    break;
                case 8:
                    MyApp.Start();
                    Render(adminId);
                    break;

            }
        }
    }
}
