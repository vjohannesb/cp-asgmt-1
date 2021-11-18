using CPAsgmt1.Interfaces.IO;

namespace CPAsgmt1.Models.IO
{
    internal class ConsoleInput : IInput
    {
        public string? Read()
        {
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }
}
