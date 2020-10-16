namespace MagicSquare.IO
{
    public interface IO
    {
        void Write(string msg, int sleepMillis = 0);

        void WriteLine(string msg, int sleepMillis = 0);

        string Read(string msg);

        void Clear();
    }
}
