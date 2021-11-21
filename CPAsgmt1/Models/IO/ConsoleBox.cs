using System.Text;

namespace CPAsgmt1.Models.IO
{
    // Simply for prettifying output, no real use hence the static class without interface
    public enum LineType { Top, Middle, Bottom }
    public static class ConsoleBox
    {
        private static readonly char _topLeft = '╭';
        private static readonly char _topRight = '╮';
        private static readonly char _bottomLeft = '╰';
        private static readonly char _bottomRight = '╯';
        private static readonly char _middle = '─';
        private static readonly char _side = '│';
        private static readonly int _width = 40;

        private static string PadCenter(string toPad, char padChar = ' ')
        {
            int padLeft = (_width - toPad.Length) / 2 + toPad.Length;
            return toPad.PadLeft(padLeft, padChar).PadRight(_width, padChar);
        }

        public static string Line(LineType lineType = LineType.Middle,
            string? content = null, bool center = false, char padChar = ' ')
        {
            char left, right;
            switch (lineType)
            {
                case LineType.Top:
                    left = _topLeft;
                    right = _topRight;
                    break;
                case LineType.Middle:
                    left = right = _side;
                    break;
                case LineType.Bottom:
                    left = _bottomLeft;
                    right = _bottomRight;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (!string.IsNullOrEmpty(content))
                if (center) content = PadCenter(content, padChar);
                else content = content.PadRight(_width, padChar);
            else content = _middle.ToString().PadRight(_width, _middle);
            return left + content + right;
        }

        public static string Line(LineType lineType, char content, bool center = false, char padChar = ' ')
            => Line(lineType, content.ToString(), center, padChar);

        public static string Box(string content)
        {
            var builder = new StringBuilder();
            builder.AppendLine(Line(LineType.Top, _middle, padChar: _middle));
            builder.AppendLine(Line(LineType.Middle, content, true));
            builder.AppendLine(Line(LineType.Bottom, _middle, padChar: _middle));
            return builder.ToString();
        }
    }
}
