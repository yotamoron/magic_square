using Boards;
using Common.Tests;
using Moq;
using System;
using Xunit;

namespace MagicSquare.Test
{
    public class BoardRendererTest
    {
        [Fact]
        public void ThrowWhenCantGetValueFromBoard()
        {
            BoardRenderer boardRenderer = GetBoardRenderer(out Mock<BoardValueFetcher> boardValueFetcher, false);

            Assert.Throws<Exception>(() => boardRenderer.Render(new AccessibleBoard { AccessibleSize = 2 }));
        }

        [Fact]
        public void VarifyThatAllTheValuesAreRendered()
        {
            int? value = null;
            string error = string.Empty;
            BoardRenderer boardRenderer = GetBoardRenderer(out Mock<BoardValueFetcher> boardValueFetcher, true);
            string renderedBoard = boardRenderer.Render(new AccessibleBoard { AccessibleSize = 2 });
            boardValueFetcher.Verify(bvf => bvf.TryGetValueAt(It.IsAny<Board>(), It.IsAny<int>(), It.IsAny<int>(), out value, out error),
                Times.Exactly(4));
            Assert.False(string.IsNullOrEmpty(renderedBoard));
        }

        private BoardRenderer GetBoardRenderer(out Mock<BoardValueFetcher> boardValueFetcher, bool hasValue)
        {
            boardValueFetcher = new Mock<BoardValueFetcher>();
            BoardRenderer boardRenderer = new BoardRenderer(boardValueFetcher.Object);
            int? value = null;
            string error = string.Empty;
            boardValueFetcher.Setup(bvf => bvf.TryGetValueAt(It.IsAny<Board>(), It.IsAny<int>(), It.IsAny<int>(), out value, out error))
                .Returns(hasValue);
            return boardRenderer;
        }
    }
}
