using MagicSquare.IO;
using Ninject.Modules;

namespace MagicSquare
{
    public class MagicSquareNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IO.IO>().To<ConsoleIO>().InSingletonScope();
        }
    }
}
