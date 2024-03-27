using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class LoginMenu : Dashboard {
        private string loginPrompt = @"
██       ██████   ██████      ██ ███    ██ 
██      ██    ██ ██           ██ ████   ██ 
██      ██    ██ ██   ███     ██ ██ ██  ██ 
██      ██    ██ ██    ██     ██ ██  ██ ██ 
███████  ██████   ██████      ██ ██   ████ 
                                           
                                           
";
        public LoginMenu(App myApp) : base(myApp) { }
        public override void Render() {
            string[] loginOptions = {

            };
        }
    }
}
