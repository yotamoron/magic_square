using Boards;
using Ninject;
using System.Reflection;
using System.Threading;

namespace MagicSquare
{
    public class Program
    {
        static void Main(string[] args)
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IO.IO io = kernel.Get<IO.IO>();
            BoardSizeReader boardSizeReader = kernel.Get<BoardSizeReader>();
            BoardFactory boardFactory = kernel.Get<BoardFactory>();

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
