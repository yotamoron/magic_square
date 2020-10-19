using Boards;
using Common.Tests;
using Moq;
using Xunit;

namespace MagicSquare.Test
{
    public class GameDriverTest
    {
        private readonly string RENDERED_BOARD = "RENDERED_BOARD";

        [Fact]
        public void PlayOnceForSolvedBoard()
        {
            GameDriver gameDriver = GetGameDriver(out GameDriverContext gameDriverContext);
            AccessibleBoard board = new AccessibleBoard
            {
                AccessibleIsSolved = true
            };
            GameFlow flow = gameDriver.Play(board);
            
            gameDriverContext.BoardRenderer.Verify(br => br.Render(It.IsAny<Board>()), Times.Once);
            gameDriverContext.IO.Verify(io => io.Clear(), Times.Once);
            gameDriverContext.IO.Verify(io => io.WriteLine(Match.Create<string>(b => b == RENDERED_BOARD), It.IsAny<int>()), Times.Once);
            gameDriverContext.IO.Verify(io => io.Read(It.IsAny<string>()), Times.Once);
            Assert.Equal(GameFlow.NEW_GAME, flow);
        }

        [Fact]
        public void StopPlayingWhenFlowIsNotKeepPlaying()
        {
            GameDriver gameDriver = GetGameDriver(out GameDriverContext gameDriverContext);
            AccessibleBoard board = new AccessibleBoard
            {
                AccessibleIsSolved = false
            };

            gameDriverContext.ActionReader.Setup(ar => ar.ReadAction(It.IsAny<Board>())).Returns("some action");
            gameDriverContext.ActionApplier.Setup(aa => aa.ApplyAction(It.IsAny<Board>(), It.IsAny<string>())).Returns(GameFlow.END_GAME);

            GameFlow flow = gameDriver.Play(board);

            gameDriverContext.BoardRenderer.Verify(br => br.Render(It.IsAny<Board>()), Times.Once);
            gameDriverContext.IO.Verify(io => io.Clear(), Times.Once);
            gameDriverContext.IO.Verify(io => io.WriteLine(Match.Create<string>(b => b == RENDERED_BOARD), It.IsAny<int>()), Times.Once);
            gameDriverContext.ActionReader.Verify(ar => ar.ReadAction(It.IsAny<Board>()), Times.Once);
            gameDriverContext.ActionApplier.Verify(aa => aa.ApplyAction(It.IsAny<Board>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(GameFlow.END_GAME, flow);
        }

        private GameDriver GetGameDriver(out GameDriverContext gameDriverContext)
        {
            gameDriverContext = new GameDriverContext
            {
                IO = new Mock<IO.IO>(),
                BoardRenderer = new Mock<BoardRenderer>(MockBehavior.Strict, new object[] { null }),
                ActionReader = new Mock<ActionReader>(MockBehavior.Strict, new object[] { null, null, null, null, null }),
                ActionApplier = new Mock<ActionApplier>(MockBehavior.Strict, new object[] { null, null, null, null, null })
            };

            gameDriverContext.IO.Setup(io => io.Clear()).Verifiable();
            gameDriverContext.IO.Setup(io => io.WriteLine(Match.Create<string>(board => board == RENDERED_BOARD), It.IsAny<int>())).Verifiable();
            gameDriverContext.IO.Setup(io => io.Read(It.IsAny<string>())).Returns("some string");
            gameDriverContext.BoardRenderer.Setup(br => br.Render(It.IsAny<Board>())).Returns(RENDERED_BOARD);

            return new GameDriver(gameDriverContext.IO.Object, gameDriverContext.BoardRenderer.Object,
                gameDriverContext.ActionReader.Object, gameDriverContext.ActionApplier.Object);
        }
    }

    class GameDriverContext
    {
        public Mock<IO.IO> IO { get; set; }
        public Mock<BoardRenderer> BoardRenderer { get; set; }
        public Mock<ActionReader> ActionReader { get; set; }
        public Mock<ActionApplier> ActionApplier { get; set; }
    }
}
