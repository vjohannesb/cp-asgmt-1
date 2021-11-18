using CPAsgmt1.Interfaces.IO;

namespace CPAsgmt1.Models.IO
{
    internal class ConsoleOutput : IOutput
    {
        private static void Clear() => Console.Clear();

        public void Write(string content, bool overwrite)
        {
            if (overwrite) Clear();
            Console.Write(content);
        }

        public void WriteLine(string content, bool overwrite)
        {
            if (overwrite) Clear();
            Console.WriteLine(content);
        }
    }
}
