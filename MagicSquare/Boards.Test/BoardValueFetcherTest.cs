using Boards.Tiles;
using Common.Tests;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test
{
    public class BoardValueFetcherTest
    {
        [Fact]
        public void FailOnTooSmallRow()
        {
            bool found = TryGetValueAt(-1, 0, out int? dontcare, out string error);

            Assert.False(found);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void FailOnTooBigRow()
        {
            bool found = TryGetValueAt(100, 0, out int? dontcare, out string error);

            Assert.False(found);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void FailOnTooSmallCol()
        {
            bool found = TryGetValueAt(0, -1, out int? dontcare, out string error);

            Assert.False(found);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void FailOnTooBigCol()
        {
            bool found = TryGetValueAt(0, 100, out int? dontcare, out string error);

            Assert.False(found);
            Assert.False(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void SuccessOnGettingBlankValue()
        {
            bool found = TryGetValueAt(1, 1, out int? value, out string error);

            Assert.True(found);
            Assert.False(value.HasValue);
        }

        [Fact]
        public void SuccessOnGettingNonBlankValue()
        {
            bool found = TryGetValueAt(0, 0, out int? value, out string error);

            Assert.True(found);
            Assert.True(value.HasValue);
            Assert.Equal(1, value);
        }


        private bool TryGetValueAt(int row, int column, out int? value, out string error)
        {
            AccessibleBoard board = new AccessibleBoard
            {
                AccessibleTiles = new List<Tile>() { new Tile(0), new Tile(1), new Tile(2), new Tile(null) },
                AccessibleBlankIndex = 3,
                AccessibleSize = 2,
                AccessibleIsSolved = true,
                AccessibleTotalMisplacedTiles = 0
            };
            BoardValueFetcher boardValueFetcher = new BoardValueFetcher();
            bool found = boardValueFetcher.TryGetValueAt(board, row, column, out value, out error);

            return found;
        }
    }
}
