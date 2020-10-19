using Boards.Tiles;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test
{
    public class BoardFactoryTest
    {
        [Fact]
        public void FailOnIllegalSize()
        {
            BoardFactory boardFactory = GetBoardFactory(out BoardFactoryContext boardFactoryContext);

            Assert.Throws<ArgumentException>(() => boardFactory.Build(0));
        }

        [Fact]
        public void FailOnIllegalNumberOfBlankTiles()
        {
            BoardFactory boardFactory = GetBoardFactory(out BoardFactoryContext boardFactoryContext);
            int dontcare;
            boardFactoryContext.BlankTileIndexFinder.Setup(btif => btif.TryFind(It.IsAny<List<Tile>>(), out dontcare)).Returns(false);
            boardFactoryContext.LegalBoardValidator.Setup(lbv => lbv.Validate(It.IsAny<List<Tile>>(), 4)).Returns(true);

            Assert.Throws<Exception>(() => boardFactory.Build(4));
        }

        [Fact]
        public void AssertBoardIsValidated()
        {
            BoardFactory boardFactory = GetBoardFactory(out BoardFactoryContext boardFactoryContext);
            boardFactoryContext.LegalBoardValidator.Setup(lbv => lbv.Validate(It.IsAny<List<Tile>>(), 4)).Returns(true);

            Board board = boardFactory.Build(4);

            boardFactoryContext.LegalBoardValidator.Verify(lbv => lbv.Validate(It.IsAny<List<Tile>>(), 4), Times.AtLeastOnce);            
        }

        [Fact]
        public void AssertBoardIsShuffledAsLongAsItIsNotValid()
        {
            BoardFactory boardFactory = GetBoardFactory(out BoardFactoryContext boardFactoryContext);
            boardFactoryContext.LegalBoardValidator.SetupSequence(lbv => lbv.Validate(It.IsAny<List<Tile>>(), 4))
                .Returns(false)
                .Returns(false)
                .Returns(true);
            boardFactoryContext.TilesShuffler.Setup(ts => ts.Shuffle(It.IsAny<List<Tile>>())).Verifiable();

            Board board = boardFactory.Build(4);

            boardFactoryContext.LegalBoardValidator.Verify(lbv => lbv.Validate(It.IsAny<List<Tile>>(), 4), Times.Exactly(3));
            boardFactoryContext.TilesShuffler.Verify(ts => ts.Shuffle(It.IsAny<List<Tile>>()), Times.Exactly(3));
        }

        private BoardFactory GetBoardFactory(out BoardFactoryContext boardFactoryContext)
        {
            boardFactoryContext = new BoardFactoryContext
            {
                LegalBoardValidator = new Mock<LegalBoardValidator>(MockBehavior.Strict, new object[] { null, null }),
                BlankTileIndexFinder = new Mock<BlankTileIndexFinder>(),
                MisplacedTilesCounter = new Mock<MisplacedTilesCounter>(),
                TilesShuffler = new Mock<TilesShuffler>()
            };
            int dontcare;
            boardFactoryContext.BlankTileIndexFinder.Setup(btif => btif.TryFind(It.IsAny<List<Tile>>(), out dontcare)).Returns(true);
            return new BoardFactory(boardFactoryContext.LegalBoardValidator.Object,
                boardFactoryContext.BlankTileIndexFinder.Object, boardFactoryContext.MisplacedTilesCounter.Object,
                boardFactoryContext.TilesShuffler.Object);
        }
    }

    class BoardFactoryContext
    {
        public Mock<LegalBoardValidator> LegalBoardValidator { get; set; }
        public Mock<BlankTileIndexFinder> BlankTileIndexFinder { get; set; }
        public Mock<MisplacedTilesCounter> MisplacedTilesCounter { get; set; }
        public Mock<TilesShuffler> TilesShuffler { get; set; }
    }
}
