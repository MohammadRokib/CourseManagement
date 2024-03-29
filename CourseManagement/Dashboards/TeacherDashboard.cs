﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class TeacherDashboard : Dashboard {
        private readonly ApplicationDbContext _context;
        private string teacherPrompt = @"
████████ ███████  █████   ██████ ██   ██ ███████ ██████      ██████   █████  ███████ ██   ██ ██████   ██████   █████  ██████  ██████  
   ██    ██      ██   ██ ██      ██   ██ ██      ██   ██     ██   ██ ██   ██ ██      ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
   ██    █████   ███████ ██      ███████ █████   ██████      ██   ██ ███████ ███████ ███████ ██████  ██    ██ ███████ ██████  ██   ██ 
   ██    ██      ██   ██ ██      ██   ██ ██      ██   ██     ██   ██ ██   ██      ██ ██   ██ ██   ██ ██    ██ ██   ██ ██   ██ ██   ██ 
   ██    ███████ ██   ██  ██████ ██   ██ ███████ ██   ██     ██████  ██   ██ ███████ ██   ██ ██████   ██████  ██   ██ ██   ██ ██████  
                                                                                                                                      
                                                                                                                                      
";

        public TeacherDashboard(App myApp, ApplicationDbContext context) : base(myApp, context) {
            _context = context;
        }
        public override void Render() {
            string[] teacherOptions = {
                "Check Attendence report",
                "Logout"
            };

            Menu teacherMenu = new Menu(teacherOptions, teacherPrompt);
            int selectedIndex = teacherMenu.Run();

            switch (selectedIndex) {
                case 0:
                    CheckAttendence();
                    break;
                case 1:
                    MyApp.Start();
                    break;
            }
        }
        private void CheckAttendence() {
            Console.Clear();
            Console.WriteLine(teacherPrompt);
            Console.WriteLine("Check Attendence Report");
            Console.ReadKey(true);
            Render();
        }
    }
}
