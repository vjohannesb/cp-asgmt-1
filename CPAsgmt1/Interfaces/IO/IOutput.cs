namespace CPAsgmt1.Interfaces.IO
{
    internal interface IOutput
    {
        public void Write(string content, bool overwrite);
        public void WriteLine(string content, bool overwrite);
    }
}
