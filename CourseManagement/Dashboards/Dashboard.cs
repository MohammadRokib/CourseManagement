using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Dashboards {
    public class Dashboard {
        protected App MyApp;
        public Dashboard(App myapp) {
            MyApp = myapp;
        }
        virtual public void Render() {}
    }
}
