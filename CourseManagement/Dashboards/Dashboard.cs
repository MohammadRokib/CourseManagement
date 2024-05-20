using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class Dashboard {
        protected App MyApp;
        private readonly ApplicationDbContext _context;

        public Dashboard(App myapp, ApplicationDbContext context) {
            MyApp = myapp;
            _context = context;
        }
        virtual public void Render(string userId) {}
    }
}
