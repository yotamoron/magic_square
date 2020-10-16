using System;
using System.Threading;

namespace MagicSquare.IO
{
    class ConsoleIO : IO
    {
        public void WriteLine(string msg, int sleepMillis = 0)
        {
            Write(msg + Environment.NewLine, sleepMillis);
        }

        public void Write(string msg, int sleepMillis = 0)
        {
            Console.Write(msg);
            Thread.Sleep(sleepMillis);
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
