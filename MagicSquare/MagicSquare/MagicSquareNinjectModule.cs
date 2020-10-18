using Boards;
using Boards.Movements;
using Boards.Tiles;
using MagicSquare.IO;
using Ninject;
using Ninject.Modules;

namespace MagicSquare
{
    public class MagicSquareNinjectModule : NinjectModule
    {
        private static readonly string newGameSymbol = "N";

        public override void Load()
        {
            Bind<IO.IO>().To<ConsoleIO>().InSingletonScope();
            Bind<BoardRenderer>().ToSelf().InSingletonScope();
            Bind<MagicSquare>().ToSelf().InSingletonScope();
            Bind<BoardSizeReadingBoardFactory>().ToSelf().InSingletonScope();
            Bind<ActionReader>().ToMethod(ctx =>
            {
                MovementDisplayNamesResolver movementDisplayNamesResolver = ctx.Kernel.Get<MovementDisplayNamesResolver>();
                LegalMovesCalculator legalMovesCalculator = ctx.Kernel.Get< LegalMovesCalculator>();
                IO.IO io = ctx.Kernel.Get<IO.IO>();

                return new ActionReader(newGameSymbol, movementDisplayNamesResolver, legalMovesCalculator, io);
            }).InSingletonScope();
            Bind<ActionApplier>().ToMethod(ctx =>
            {
                TileMover tileMover = ctx.Kernel.Get<TileMover>();
                MovementDisplayNamesResolver movementDisplayNamesResolver = ctx.Kernel.Get<MovementDisplayNamesResolver>();
                IO.IO io = ctx.Kernel.Get<IO.IO>();

                return new ActionApplier(newGameSymbol, tileMover, movementDisplayNamesResolver, io);
            }).InSingletonScope();
        }
    }
}
