using CPAsgmt1.Interfaces.Menu;

namespace CPAsgmt1.Interfaces.IO
{
    internal interface IIO
    {
        void Init();

        string? Read();
        string? Read(string prompt, bool overwrite = false);
        char ReadKey(bool intercept);
        void Write(string content, bool overwrite = false);
        void WriteLine(string content, bool overwrite = false);

        string? GetAnswer(string title, string prompt, bool overwrite, string? footer = null);

        string ConstructMenu(string title, IEnumerable<string> items, string returnKey);
        void DisplayMenu(string menu, bool overwrite);
        T? GetSelection<T>(string title, string prompt, IEnumerable<string> keys, IEnumerable<T> items, string? returnKey);
        IMenuItem? GetSelection(IMenu menu, string prompt, string? returnKey);

        void ListItems(string title, IEnumerable<string> items, bool overwrite);
    }
}
