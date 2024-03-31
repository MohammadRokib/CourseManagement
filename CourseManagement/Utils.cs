using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    static class Utils {
        public static void WaitForKeyPress() {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(true);
        }
        public static void QuitConsole() {
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
