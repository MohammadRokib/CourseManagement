using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement {
    public class Spectre {
        public void SpectreHello() {
            AnsiConsole.Markup("[underline red]Hello[/] World!");

            // New Code
            Console.WriteLine("\n");

            // Create a table
            var table = new Table();
            table.Border = TableBorder.Square;

            // Add some columns
            table.AddColumn("Foo");
            table.AddColumn(new TableColumn("Bar").Centered());

            // Add some rows
            table.AddRow("Baz", "[green]Qux[/]");
            table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));

            // Render the table to the console
            AnsiConsole.Write(table);
        }
    }
}
