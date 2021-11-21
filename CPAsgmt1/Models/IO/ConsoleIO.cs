using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Menu;
using System.Text;

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

        public char ReadKey(bool intercept)
            => Console.ReadKey(intercept).KeyChar;

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

            string? input = null;
            while (string.IsNullOrEmpty(input))
            {
                input = Read(prompt)?.Trim();
                if (input == "0") return null;
            }
            return input;
        }

        public string ConstructMenu(string title,
            IEnumerable<string> items, string? returnKey)
        {
            StringBuilder builder = new();
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
            string? returnKey)
        {
            var menu = ConstructMenu(title, keys, returnKey);
            DisplayMenu(menu, true);

            int choice;
            string? input = null;
            while (!IsInRange(input, 0, items.Count(), out choice))
                input = Read(prompt);

            return choice == 0 ? default : items.ElementAtOrDefault(choice - 1);
        }

        public IMenuItem? GetSelection(IMenu menu, string prompt, string? returnKey)
        {
            IEnumerable<string>? keys = menu.MenuItems.Select(mi => mi.Name);
            return GetSelection(menu.Title, prompt, keys, menu.MenuItems, returnKey);
        }

        public void ListItems(string title, IEnumerable<string> items, bool overwrite)
        {
            StringBuilder builder = new();
            builder.AppendLine(ConsoleBox.Line(LineType.Top));
            builder.AppendLine(ConsoleBox.Line(LineType.Middle, title, true));
            builder.AppendLine(ConsoleBox.Line(LineType.Bottom));

            for (var i = 0; i < items.Count(); i++)
                builder.AppendLine(ConsoleBox.Line(content: $" {i + 1}. {items.ElementAt(i)}"));

            builder.Append(ConsoleBox.Line(LineType.Bottom));
            DisplayMenu(builder.ToString(), overwrite);
        }

        private static void Clear()
            => Console.Clear();

        private static bool IsInRange(string? input, int min, int max, out int output)
            => int.TryParse(input, out output) && output >= min && output <= max;
    }
}
