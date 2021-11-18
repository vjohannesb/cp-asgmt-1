using CPAsgmt1.Interfaces.Menu;
using System.Text;

namespace CPAsgmt1.Models.Menu
{
    internal class ConsoleMenu : IMenu
    {
        private enum LineType { Top, Middle, Bottom }
        private readonly char _topLeft = '╭';
        private readonly char _topRight = '╮';
        private readonly char _bottomLeft = '╰';
        private readonly char _bottomRight = '╯';
        private readonly char _middle = '─';
        private readonly char _side = '│';
        private readonly int _width = 40;
        private readonly int _padding = 16;

        public ConsoleMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public string Title { get; set; } = string.Empty;
        public IEnumerable<IMenuItem> MenuItems { get; set; } = new List<IMenuItem>();


        private string PadCenter(string toPad, char padChar = ' ')
        {
            int padLeft = (_width - toPad.Length) / 2 + toPad.Length;
            return toPad.PadLeft(padLeft, padChar).PadRight(_width, padChar);
        }

        private string BoxLine(string content, LineType lineType, bool center = false, char padChar = ' ')
        {
            char left, right;
            switch (lineType)
            {
                case LineType.Top:
                    left = _topLeft;
                    right = _topRight;
                    break;
                case LineType.Middle:
                    left = _side;
                    right = _side;
                    break;
                case LineType.Bottom:
                    left = _bottomLeft;
                    right = _bottomRight;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (center) content = PadCenter(content, padChar);
            else content = content.PadRight(_width, padChar);
            return left + content + right;
        }

        private string BoxLine(char content, LineType lineType, bool center = false, char padChar = ' ')
            => BoxLine(content.ToString(), lineType, center, padChar);

        public string Box(string content)
        {
            var builder = new StringBuilder();
            builder.AppendLine(BoxLine(_side, LineType.Top, padChar: _side));
            builder.AppendLine(BoxLine(content, LineType.Middle));
            builder.AppendLine(BoxLine(_side, LineType.Bottom, padChar: _side));
            return builder.ToString();
        }

        public string SelectionMenu()
        {
            var builder = new StringBuilder();
            builder.AppendLine(BoxLine(_middle, LineType.Top, padChar: _middle));
            builder.AppendLine(BoxLine(Title, LineType.Middle, true));
            builder.AppendLine(BoxLine(_middle, LineType.Bottom, padChar: _middle));

            foreach (var item in MenuItems)
                builder.AppendLine($" [{item.Index}] {item.Name}");

            return builder.ToString();
        }

        public string ListMenu()
        {
            var builder = new StringBuilder();
            builder.AppendLine(BoxLine(_middle, LineType.Top, padChar: _middle));
            builder.AppendLine(BoxLine(Title, LineType.Middle, true));
            builder.AppendLine(BoxLine(_middle, LineType.Middle, padChar: _middle));

            foreach (var item in MenuItems.Where(i => i.Index != 0))
                builder.AppendLine(BoxLine($" {item.Index}. {item.Name}", LineType.Middle));

            builder.AppendLine(BoxLine(_middle, LineType.Bottom, padChar: _middle));
            return builder.ToString();
        }
    }
}
