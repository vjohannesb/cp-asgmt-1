namespace cp_asgmt_1.Interfaces.IO
{
    internal interface IInput
    {
        public string? Read();
        public ConsoleKeyInfo ReadKey(bool intercept);
    }
}
