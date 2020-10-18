using Boards.Movements;
using Boards.Tiles;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Boards.Test.Tiles
{
    public class TileMoverTest
    {
        [Fact]
        public void FailOnIllegalMovement()
        {
            TileMover tileMover = GetTileMover(out TileMoverContext tileMoverContext);

            tileMoverContext.LegalMovesCalculator.Setup(lmc => lmc.GetLegalMoves(It.IsAny<Board>())).Returns(new List<Movement>());
            bool moved = tileMover.TryMove(new Board(), Movement.DOWN, out string error);

            Assert.False(moved);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void SuccessMoveDown()
        {
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleTiles = ToTiles(new List<int?> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, null }),
                AccessibleBlankIndex = 15,
                AccessibleSize = 4,
                AccessibleTotalMisplacedTiles = 0,
                AccessibleIsSolved = true
            };

            SuccessMove(board, Movement.DOWN, 11, false);
        }

        [Fact]
        public void SuccessMoveUp()
        {
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleTiles = ToTiles(new List<int?> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, null, 12, 13, 14, 11 }),
                AccessibleBlankIndex = 11,
                AccessibleSize = 4,
                AccessibleTotalMisplacedTiles = 1,
                AccessibleIsSolved = false
            };

            SuccessMove(board, Movement.UP, 15, true);
        }

        [Fact]
        public void SuccessMoveLeft()
        {
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleTiles = ToTiles(new List<int?> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, null, 14 }),
                AccessibleBlankIndex = 14,
                AccessibleSize = 4,
                AccessibleTotalMisplacedTiles = 1,
                AccessibleIsSolved = false
            };

            SuccessMove(board, Movement.LEFT, 15, true);
        }

        [Fact]
        public void SuccessMoveRight()
        {
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleTiles = ToTiles(new List<int?> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, null }),
                AccessibleBlankIndex = 15,
                AccessibleSize = 4,
                AccessibleTotalMisplacedTiles = 0,
                AccessibleIsSolved = true
            };

            SuccessMove(board, Movement.RIGHT, 14, false);
        }


        private void SuccessMove(AccessibeBoard board, Movement movement, int expectedBlankIndex, bool expectedIsSolved)
        {
            TileMover tileMover = GetTileMover(out TileMoverContext tileMoverContext);

            tileMoverContext.LegalMovesCalculator.Setup(lmc => lmc.GetLegalMoves(It.IsAny<Board>())).Returns(new List<Movement>() { movement });
            bool moved = tileMover.TryMove(board, movement, out string error);

            Assert.True(moved);
            Assert.Equal(expectedBlankIndex, board.AccessibleBlankIndex);
            Assert.Equal(expectedIsSolved, board.AccessibleIsSolved);
        }

        private List<Tile> ToTiles(List<int?> values)
        {
            return values.Select(value => new Tile(value)).ToList();
        }

        private TileMover GetTileMover(out TileMoverContext tileMoverContext)
        {
            tileMoverContext = GetMockedTileMoverContext();
            return new TileMover(tileMoverContext.LegalMovesCalculator.Object, tileMoverContext.MovementDeltaCalculator.Object,
                tileMoverContext.MisplacedTilesCounter.Object);
        }

        private TileMoverContext GetMockedTileMoverContext()
        {
            return new TileMoverContext
            {
                LegalMovesCalculator = new Mock<LegalMovesCalculator>(),
                MovementDeltaCalculator = new Mock<MovementDeltaCalculator>(),
                MisplacedTilesCounter = new Mock<MisplacedTilesCounter>()
            };
        }
    }

    internal class TileMoverContext
    {
        public Mock<LegalMovesCalculator> LegalMovesCalculator { get; set; }
        public Mock<MovementDeltaCalculator> MovementDeltaCalculator { get; set; }
        public Mock<MisplacedTilesCounter> MisplacedTilesCounter { get; set; }
    }
}
