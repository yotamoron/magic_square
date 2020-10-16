using Boards;
using MagicSquare.IO;
using System.Threading;

namespace MagicSquare
{
    public class Program
    {
        static void Main(string[] args)
        {
            IO.IO io = new ConsoleIO();
            BoardSizeReader boardSizeReader = new BoardSizeReader(io);
            BoardFactory boardFactory = new BoardFactory();

            io.WriteLine("Welcome to the MagicSquare!");
            Thread.Sleep(1000);

            while (true)
            {
                int size = boardSizeReader.Read();
                Board board = boardFactory.Build(size);
                io.WriteLine($"{size}");
                Thread.Sleep(1000);
            }
        }
    }
}
