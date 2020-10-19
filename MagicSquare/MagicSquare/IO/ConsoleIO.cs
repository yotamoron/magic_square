using System;
using System.Threading;

namespace MagicSquare.IO
{
    class ConsoleIO : IO
    {
        public void WriteLine(string msg, int displayTimeMillis = 0)
        {
            Write(msg + Environment.NewLine, displayTimeMillis);
        }

        public void Write(string msg, int displayTimeMillis = 0)
        {
            Console.Write(msg);
            Thread.Sleep(displayTimeMillis);
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
