using Boards;
using Moq;
using Xunit;

namespace MagicSquare.Test
{
    public class MagicSquareTest
    {
        [Fact]
        public void PlayWhileNotEndGame()
        {
            MagicSquare magicSquare = GetMagicSquare(out MagicSquareContext magicSquareContext);

            magicSquareContext.GameDriver.SetupSequence(gd => gd.Play(It.IsAny<Board>()))
                .Returns(GameFlow.NEW_GAME)
                .Returns(GameFlow.KEEP_PLAYING)
                .Returns(GameFlow.END_GAME);
            magicSquare.Play();

            magicSquareContext.GameDriver.Verify(gd => gd.Play(It.IsAny<Board>()), Times.Exactly(3));
            magicSquareContext.BoardSizeReadingBoardFactory.Verify(bsrbf => bsrbf.GetBoard(), Times.Exactly(3));
            magicSquareContext.IO.Verify(io => io.Clear(), Times.Once);
            magicSquareContext.IO.Verify(io => io.WriteLine(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        private MagicSquare GetMagicSquare(out MagicSquareContext magicSquareContext)
        {
            magicSquareContext = new MagicSquareContext
            {
                IO = new Mock<IO.IO>(),
                BoardSizeReadingBoardFactory = new Mock<BoardSizeReadingBoardFactory>(MockBehavior.Strict, new object[] { null, null }),
                GameDriver = new Mock<GameDriver>(MockBehavior.Strict, new object[] { null, null, null, null })
            };
            magicSquareContext.IO.Setup(io => io.Clear()).Verifiable();
            magicSquareContext.IO.Setup(io => io.WriteLine(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            magicSquareContext.BoardSizeReadingBoardFactory.Setup(bsrbf => bsrbf.GetBoard()).Returns(new Board());

            return new MagicSquare(magicSquareContext.IO.Object, magicSquareContext.BoardSizeReadingBoardFactory.Object,
                magicSquareContext.GameDriver.Object);
        }
    }

    class MagicSquareContext
    {
        public Mock<IO.IO> IO { get; set; }
        public Mock<BoardSizeReadingBoardFactory> BoardSizeReadingBoardFactory { get; set; }
        public Mock<GameDriver> GameDriver { get; set; }

    }
}
