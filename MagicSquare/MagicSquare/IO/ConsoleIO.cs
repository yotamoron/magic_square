using System;

namespace MagicSquare.IO
{
    class ConsoleIO : IO
    {
        public void WriteLine(string msg)
        {
            Write(msg + Environment.NewLine);
        }

        public void Write(string msg)
        {
            Console.Write(msg);
        }

        public string Read(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
