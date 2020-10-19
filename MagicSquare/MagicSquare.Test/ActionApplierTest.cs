using Boards;
using Boards.Movements;
using Boards.Tiles;
using Moq;
using Xunit;

namespace MagicSquare.Test
{
    public class ActionApplierTest
    {
        private static readonly string ACTION_NAME = "SOME_ACTION";
        private static readonly string ERROR = "XXX__ERROR__XXX";

        private delegate void MovementDisplayNamesResolverCallback(string action, out Movement movement);
        private delegate void TileMoverCallback(Board board, Movement movement, out string error);

        Movement movement;
        string err;

        [Fact]
        public void ActionResolved_MovementDone()
        {            
            ActionApplier actionApplier = GetActionApplier(out ActionApplierContext actionApplierContext, true, true, ACTION_NAME);
            GameFlow flow = actionApplier.ApplyAction(new Board(), ACTION_NAME);

            Assert.Equal(GameFlow.KEEP_PLAYING, flow);
            actionApplierContext.IO.Verify(io => io.WriteLine(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
            actionApplierContext.MovementDisplayNamesResolver
                .Verify(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == ACTION_NAME), out movement), Times.Once);
            actionApplierContext.TileMover.Verify(tm => tm.TryMove(It.IsAny<Board>(), It.IsAny<Movement>(), out err), Times.Once);
        }

        [Fact]
        public void ActionResolved_MovementFailed()
        {
            ActionApplier actionApplier = GetActionApplier(out ActionApplierContext actionApplierContext, true, false, ACTION_NAME);
            GameFlow flow = actionApplier.ApplyAction(new Board(), ACTION_NAME);

            Assert.Equal(GameFlow.KEEP_PLAYING, flow);
            actionApplierContext.IO.Verify(io => io.WriteLine(
                Match.Create<string>(err => err.Contains(ERROR)), 
                It.IsAny<int>()), Times.Once);
            actionApplierContext.MovementDisplayNamesResolver
                .Verify(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == ACTION_NAME), out movement), Times.Once);
            actionApplierContext.TileMover.Verify(tm => tm.TryMove(It.IsAny<Board>(), It.IsAny<Movement>(), out err), Times.Once);
        }

        [Fact]
        public void NewGame()
        {
            ActionApplier actionApplier = GetActionApplier(out ActionApplierContext actionApplierContext, false, false,
                ActionApplierContext.NewGameSymbol);
            GameFlow flow = actionApplier.ApplyAction(new Board(), ActionApplierContext.NewGameSymbol);

            Assert.Equal(GameFlow.NEW_GAME, flow);
            actionApplierContext.MovementDisplayNamesResolver
                .Verify(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == ActionApplierContext.NewGameSymbol), out movement), Times.Once);
            actionApplierContext.TileMover.Verify(tm => tm.TryMove(It.IsAny<Board>(), It.IsAny<Movement>(), out err), Times.Never);
        }

        [Fact]
        public void EndGame()
        {
            ActionApplier actionApplier = GetActionApplier(out ActionApplierContext actionApplierContext, false, false, 
                ActionApplierContext.EndGameSymbol);
            GameFlow flow = actionApplier.ApplyAction(new Board(), ActionApplierContext.EndGameSymbol);

            Assert.Equal(GameFlow.END_GAME, flow);
            actionApplierContext.MovementDisplayNamesResolver
                .Verify(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == ActionApplierContext.EndGameSymbol), out movement), Times.Once);
            actionApplierContext.TileMover.Verify(tm => tm.TryMove(It.IsAny<Board>(), It.IsAny<Movement>(), out err), Times.Never);
        }

        [Fact]
        public void UnknownAction()
        {
            string unknwonAction = "SOME_UNKNOWN_ACTION";
            ActionApplier actionApplier = GetActionApplier(out ActionApplierContext actionApplierContext, false, false, unknwonAction);            
            GameFlow flow = actionApplier.ApplyAction(new Board(), unknwonAction);

            Assert.Equal(GameFlow.KEEP_PLAYING, flow);
            actionApplierContext.IO.Verify(io => io.WriteLine(
                Match.Create<string>(err => err.Contains(unknwonAction)),
                It.IsAny<int>()), Times.Once);
            actionApplierContext.MovementDisplayNamesResolver
                .Verify(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == unknwonAction), out movement), Times.Once);
            actionApplierContext.TileMover.Verify(tm => tm.TryMove(It.IsAny<Board>(), It.IsAny<Movement>(), out err), Times.Never);
        }

        private ActionApplier GetActionApplier(out ActionApplierContext actionApplierContext, bool resolveDisplayNameSuccessfully,
            bool moveSuccessfully, string targetAction)
        {
            actionApplierContext = new ActionApplierContext
            {
                TileMover = new Mock<TileMover>(MockBehavior.Strict, new object[] { null, null, null }),
                MovementDisplayNamesResolver = new Mock<MovementDisplayNamesResolver>(MockBehavior.Strict, new object[] { null }),
                IO = new Mock<IO.IO>()
            };
            Movement movement = default(Movement);
            string error = string.Empty;

            actionApplierContext.MovementDisplayNamesResolver
                .Setup(mdnr => mdnr.TryResolve(Match.Create<string>(action => action == targetAction), out movement))
                .Returns(resolveDisplayNameSuccessfully)
                .Callback(new MovementDisplayNamesResolverCallback((string action, out Movement outMovement) =>
                {
                    outMovement = default(Movement);
                }));
            actionApplierContext.TileMover.Setup(tm => tm.TryMove(It.IsAny<Board>(), Match.Create<Movement>(m => m == default(Movement)), out error))
                .Returns(moveSuccessfully)
                .Callback(new TileMoverCallback((Board b, Movement m, out string err) =>
                {
                    err = ERROR;
                }));
            actionApplierContext.IO.Setup(io => io.Write(It.IsAny<string>(), It.IsAny<int>())).Verifiable();

            return new ActionApplier(ActionApplierContext.NewGameSymbol, ActionApplierContext.EndGameSymbol,
                actionApplierContext.TileMover.Object, actionApplierContext.MovementDisplayNamesResolver.Object,
                actionApplierContext.IO.Object);
        }
    }

    class ActionApplierContext
    {
        public static string NewGameSymbol { get { return "N"; } }
        public static string EndGameSymbol { get { return "E"; } }
        public Mock<TileMover> TileMover { get; set; }
        public Mock<MovementDisplayNamesResolver> MovementDisplayNamesResolver { get; set; }
        public Mock<IO.IO> IO { get; set; }
    }
}
