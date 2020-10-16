using Ninject.Modules;
using System.Collections.Generic;

namespace Boards
{
    public class BoardsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<BoardPlayer>().ToSelf().InSingletonScope();
            Bind<BoardFactory>().ToSelf().InSingletonScope();
            Bind<InversionsCounter>().ToSelf().InSingletonScope();
            Bind<LegalBoardValidator>().ToSelf().InSingletonScope();
            Bind<BlankTileIndexFinder>().ToSelf().InSingletonScope();
            Bind<MovementDisplayNamesResolver>().ToMethod(ctx =>
            {
                Dictionary<Movement, string> movementDisplayNames = new Dictionary<Movement, string>
                {
                    { Movement.DOWN, "D" },
                    { Movement.UP, "U" },
                    { Movement.RIGHT, "R" },
                    { Movement.LEFT, "L" }
                };

                return new MovementDisplayNamesResolver(movementDisplayNames);
            }).InSingletonScope();
        }
    }
}
