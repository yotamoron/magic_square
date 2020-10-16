using Ninject.Modules;

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
        }
    }
}
