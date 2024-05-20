using System.Globalization;

namespace CourseManagement {
    internal class Program {
        static void Main(string[] args) {
            ApplicationDbContext context = new ApplicationDbContext();

            App myApp = new App(context);
            myApp.Start();

            /*Spectre s = new Spectre();
            s.SpectreHello();*/

            /*Test test = new Test();
            test.Solution();*/
        }
    }
}
