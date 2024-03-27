using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    public class Menu {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string[] options, string prompt) {
            Options = options;
            Prompt = prompt;
            SelectedIndex = 0;
        }

        private void DisplayOptions() {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(Prompt);

            for (int i=0; i<Options.Length; i++) {
                string prefix;

                if (i == SelectedIndex) {
                    prefix = "\u001b[32m\udb80\udc56 ";
                } else {
                    prefix = "  ";
                }

                Console.WriteLine($"{prefix} {Options[i]}\u001b[0m");
                Console.ResetColor();
            }
        }
        public int Run() {
            ConsoleKey keyPressed;
            (int left, int top) = Console.GetCursorPosition();

            do {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                switch (keyPressed) {
                    case ConsoleKey.UpArrow:
                        SelectedIndex = (SelectedIndex == 0 ? Options.Length-1 : SelectedIndex-1);
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedIndex = (SelectedIndex == Options.Length - 1 ? 0 : SelectedIndex + 1);
                        break;
                }
                // Console.SetCursorPosition(left, top);

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
        public bool Login() {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(Prompt);

            string name = AnsiConsole.Ask<string>("[green]Name[/]:");
            string username = AnsiConsole.Ask<string>("[green]Username[/]:");
            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Password[/]:")
                .PromptStyle("red")
                .Secret()
            );
            string userType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]User type[/]:")
                .PageSize(10)
                .MoreChoicesText("[](Move Up and Down with the arrow keys")
                .AddChoices(new[] {
                    "Admin",
                    "Teacher",
                    "Student"
                })
            );

            var table = new Table();
            table.Border = TableBorder.Rounded;

            table.AddColumn(new TableColumn("Name").Centered());
            table.AddColumn(new TableColumn("Username").Centered());
            table.AddColumn(new TableColumn("User type").Centered());

            // Add some rows
            table.AddRow(name, username, userType);

            // Render the table to the console
            AnsiConsole.Write(table);

            return true;
        }

        #region
/*
        public void SelectMenu() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("*** *** Course Management System *** ***");
            Console.ResetColor();

            Console.WriteLine("" +
                    "\nUse \udb81\udec3 and \udb81\udec0 to"+
                    " navigate and press the \u001b[32mEnter\u001b[0m key to select"+
                    "and \u001b[32mEsc\u001b[0m to exit"
                );

            ConsoleKeyInfo key;
            int option = 0;
            bool isSelected = false;
            string[] options = new string[] {
                "Option 1\u001b[0m",
                "Option 2\u001b[0m",
                "Option 3\u001b[0m",
                "Option 4\u001b[0m",
                "Option 5\u001b[0m"
            };
            int length = options.Length;
            string color = "\u001b[32m\udb80\udc56 ";
            (int left, int top) = Console.GetCursorPosition();

            while (!isSelected) {
                Console.SetCursorPosition(left, top);

                for (int i = 0; i < length; i++) {
                    if (option == i) {
                        Console.WriteLine($"{color}  {options[i]}");
                    }
                    else {
                        Console.WriteLine($"    {options[i]}");
                    }
                }

                key = Console.ReadKey(true);

                switch (key.Key) {
                    case ConsoleKey.DownArrow:
                        option = (option == length - 1 ? 0 : option + 1);
                        break;

                    case ConsoleKey.UpArrow:
                        option = (option == 0 ? length - 1 : option - 1);
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                    case ConsoleKey.Escape:
                        return;
                }

            }
        }
 */
        #endregion
    }
}
