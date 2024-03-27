using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    static class Utils {
        public static void WaitForKeyPress() {
            Console.WriteLine("Press any ke to continue");
            Console.ReadKey(true);
        }
        public static void QuitConsole() {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
