namespace MagicSquare.IO
{
    public interface IO
    {
        void Write(string msg);

        void WriteLine(string msg);

        string Read(string msg);

        void Clear();
    }
}
