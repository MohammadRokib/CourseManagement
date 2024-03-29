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
        public override void Render() {
            string[] studentOptions = {
                "Give attendence",
                "Check attendence",
                "Logout"
            };

            Menu studentMenu = new Menu(studentOptions, studentPrompt);
            int selectedIndex = studentMenu.Run();

            switch (selectedIndex) {
                case 0:
                    GiveAttendence();
                    break;
                case 1:
                    CheckAttendence();
                    break;
                case 2:
                    MyApp.Start();
                    break;
            }
        }

        private void GiveAttendence() {
            Console.Clear();
            Console.WriteLine(studentPrompt);
            Console.WriteLine("Give Attendence");
            Console.ReadKey(true);
            Render();
        }
        private void CheckAttendence() {
            Console.Clear();
            Console.WriteLine(studentPrompt);
            Console.WriteLine("Check Attendence");
            Console.ReadKey(true);
            Render();
        }
    }
}
