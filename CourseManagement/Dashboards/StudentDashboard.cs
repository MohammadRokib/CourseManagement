using CourseManagement.UserMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class StudentDashboard : Dashboard {
        private readonly ApplicationDbContext _context;
        private string studentPrompt = @"
███████ ████████ ██    ██ ██████  ███████ ███    ██ ████████     ██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
██         ██    ██    ██ ██   ██ ██      ████   ██    ██        ██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
███████    ██    ██    ██ ██   ██ █████   ██ ██  ██    ██        ██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
     ██    ██    ██    ██ ██   ██ ██      ██  ██ ██    ██        ██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
███████    ██     ██████  ██████  ███████ ██   ████    ██        ██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                                                                                          
                                                                                                                                          
";
        
        public StudentDashboard(App myApp, ApplicationDbContext context) : base(myApp, context) {
            _context = context;
        }
        public override void Render(string studentId) {
            string[] studentOptions = {
                "Give attendence",
                "Logout"
            };

            Menu studentMenu = new Menu(studentOptions, studentPrompt);
            int selectedIndex = studentMenu.Run();

            switch (selectedIndex) {
                case 0:
                    StudentMethods.GiveAttendance(studentId);
                    Render(studentId);
                    break;
                case 1:
                    MyApp.Start();
                    break;
            }
        }
    }
}
