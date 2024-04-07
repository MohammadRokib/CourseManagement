using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    static class Utils {
        public static void WaitForKeyPress() {
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey(true);
        }
        public static void QuitConsole() {
            Console.WriteLine("\n\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void PrintPrompt(string prompt) {
            Console.Clear();
            Console.WriteLine(prompt);
        }
        public static (ConsoleKeyInfo, int, int) GetConfirmationKey() {
            (int left, int top) = Console.GetCursorPosition();
            Console.WriteLine("Press Enter to continue or Esc to go back");
            ConsoleKeyInfo confirmationKey = Console.ReadKey(true);

            return (confirmationKey, left, top);
        }
        public static void SetCursorPosition(int left, int top) {
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(left, top);
        }
    }
}
