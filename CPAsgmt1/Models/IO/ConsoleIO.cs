using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Customers;
using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Kennel;
using CPAsgmt1.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAsgmt1.Models.IO
{
    internal class ConsoleIO : IIO
    {
        public void Init() => Console.OutputEncoding = Encoding.UTF8;

        public string? Read() => Console.ReadLine();

        public string? Read(string prompt, bool overwrite = false)
        {
            Write(prompt, overwrite);
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
            => Console.ReadKey(intercept);

        private static void Clear()
            => Console.Clear();

        public void Write(string content, bool overwrite = false)
        {
            if (overwrite) Clear();
            Console.Write(content);
        }

        public void WriteLine(string content, bool overwrite = false)
        {
            if (overwrite) Clear();
            Console.WriteLine(content);
        }

        public string? GetAnswer(string title, string prompt, bool overwrite, string? footer = null)
        {
            if (overwrite) Clear();

            WriteLine(ConsoleBox.Box(title));
            if (!string.IsNullOrEmpty(footer))
                WriteLine(footer);

            string? input;
            while (string.IsNullOrEmpty(input = Read(prompt)?.Trim()))
            {
                Write(input ?? "");
                if (input == "0") return null;
            }

            return input;
        }

        public string ConstructMenu(string title,
            IEnumerable<string> items, string? returnKey)
        {
            var builder = new StringBuilder();
            builder.Append(ConsoleBox.Box(title));

            for (var i = 0; i < items.Count(); i++)
                builder.AppendLine(ConsoleBox.Line(content: $" [{i + 1}] {items.ElementAt(i)}"));

            if (!string.IsNullOrEmpty(returnKey))
                builder.AppendLine(ConsoleBox.Line(content: $" [0] {returnKey}"));

            builder.AppendLine(ConsoleBox.Line(LineType.Bottom));
            return builder.ToString();
        }

        public void DisplayMenu(string menu, bool overwrite)
            => WriteLine(menu, overwrite);

        public T? GetSelection<T>(string title, string prompt,
            IEnumerable<string> keys, IEnumerable<T> items, 
            string? returnKey = "Cancel")
        {
            var menu = ConstructMenu(title, keys, returnKey);
            DisplayMenu(menu, true);

            int choice;
            while (!IsInRange(Read(prompt), 0, items.Count(), out choice)) { }

            return choice == 0 ? default : items.ElementAtOrDefault(choice - 1);
        }

        public IMenuItem? GetSelection(IMenu menu, string prompt)
        {
            var builder = new StringBuilder();
            builder.Append(ConsoleBox.Box(menu.Title));

            foreach (var item in menu.MenuItems)
                builder.AppendLine(ConsoleBox.Line(content: $" [{item.Index}] {item.Name}"));

            builder.AppendLine(ConsoleBox.Line(LineType.Bottom));
            DisplayMenu(builder.ToString(), true);

            int choice;
            while (!IsInRange(Read(prompt), 0, menu.MenuItems.Count(), out choice)) { }

            return choice == 0 
                ? default 
                : menu.MenuItems.ElementAtOrDefault(choice - 1);
        }

        private static bool IsInRange(string? value, int min, int max, out int result)
            => int.TryParse(value, out result) && result >= min && (result < max || result == 1);

        public void ListItems(string title, IEnumerable<string> items, bool overwrite)
        {
            var builder = new StringBuilder();
            builder.AppendLine(ConsoleBox.Line(LineType.Top));
            builder.AppendLine(ConsoleBox.Line(LineType.Middle, title, true));
            builder.AppendLine(ConsoleBox.Line(LineType.Bottom));

            for (var i = 0; i < items.Count(); i++)
                builder.AppendLine(ConsoleBox.Line(content: $" {i + 1}. {items.ElementAt(i)}"));

            builder.AppendLine(ConsoleBox.Line(LineType.Bottom));
            DisplayMenu(builder.ToString(), overwrite);
        }
    }
}
