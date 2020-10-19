using Moq;
using Xunit;

namespace MagicSquare.Test
{
    public class BoardSizeReaderTest
    {
        [Fact]
        public void KeepTryingWhileSizeIsIllegal()
        {
            Mock<IO.IO> io = new Mock<IO.IO>();
            BoardSizeReader boardSizeReader = new BoardSizeReader(io.Object);
            io.Setup(o => o.Clear()).Verifiable();
            io.SetupSequence(o => o.Read(It.IsAny<string>()))
                .Returns((string)null)
                .Returns("ENTER")
                .Returns(string.Empty)
                .Returns("ENTER")
                .Returns("this is not a number")
                .Returns("ENTER")
                .Returns("-1")
                .Returns("ENTER")
                .Returns("4");
            int size = boardSizeReader.Read();
            io.Verify(o => o.Clear(), Times.Exactly(5));
            io.Verify(o => o.Read(It.IsAny<string>()), Times.Exactly(9));
            Assert.Equal(4, size);
        }
    }
}
