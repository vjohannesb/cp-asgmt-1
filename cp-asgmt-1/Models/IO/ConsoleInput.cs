using cp_asgmt_1.Interfaces.IO;

namespace cp_asgmt_1.Models.IO
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
