using Boards;
using Moq;
using Xunit;

namespace MagicSquare.Test
{
    public class BoardSizeReadingBoardFactoryTest
    {
        [Fact]
        public void Sanity()
        {
            BoardSizeReadingBoardFactory boardSizeReadingBoardFactory = GetBoardSizeReadingBoardFactory(out BoardSizeReadingBoardFactoryContext boardSizeReadingBoardFactoryContext);

            boardSizeReadingBoardFactoryContext.BoardSizeReader.Setup(bsr => bsr.Read()).Returns(4);
            boardSizeReadingBoardFactoryContext.BoardFactory.Setup(bf => bf.Build(Match.Create<int>(i => i == 4))).Returns(new Board());

            Board board = boardSizeReadingBoardFactory.GetBoard();
            boardSizeReadingBoardFactoryContext.BoardSizeReader.Verify(bsr => bsr.Read(), Times.Once);
            boardSizeReadingBoardFactoryContext.BoardFactory.Verify(bf => bf.Build(Match.Create<int>(i => i == 4)), Times.Once);
        }

        private BoardSizeReadingBoardFactory GetBoardSizeReadingBoardFactory(out BoardSizeReadingBoardFactoryContext boardSizeReadingBoardFactoryContext)
        {
            boardSizeReadingBoardFactoryContext = new BoardSizeReadingBoardFactoryContext
            {
                BoardSizeReader = new Mock<BoardSizeReader>(MockBehavior.Strict, new object[] { null }),
                BoardFactory = new Mock<BoardFactory>(MockBehavior.Strict, new object[] { null, null, null, null })
            };

            return new BoardSizeReadingBoardFactory(boardSizeReadingBoardFactoryContext.BoardSizeReader.Object,
                boardSizeReadingBoardFactoryContext.BoardFactory.Object);
        }
    }

    class BoardSizeReadingBoardFactoryContext
    {
        public Mock<BoardSizeReader> BoardSizeReader { get; set; }
        public Mock<BoardFactory> BoardFactory { get; set; }
    }
}
