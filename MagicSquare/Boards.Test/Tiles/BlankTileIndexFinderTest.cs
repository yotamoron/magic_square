using Boards.Tiles;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test.Tiles
{
    public class BlankTileIndexFinderTest
    {
        [Fact]
        public void FailWhenNoBlankTile()
        {
            List<Tile> tiles = new List<Tile> { new Tile(0) };
            bool found = TryFind(tiles, out int dontcare);

            Assert.False(found);
        }

        [Fact]
        public void FailWhenMoreThenOneBlankTile()
        {
            List<Tile> tiles = new List<Tile> { new Tile(null), new Tile(null) };
            bool found = TryFind(tiles, out int dontcare);

            Assert.False(found);
        }

        [Fact]
        public void SuccessWhenBlankIsFirst()
        {
            List<Tile> tiles = new List<Tile> { new Tile(null), new Tile(0) };
            bool found = TryFind(tiles, out int index);

            Assert.True(found);
            Assert.Equal(0, index);
        }

        [Fact]
        public void SuccessWhenBlankIsLast()
        {
            List<Tile> tiles = new List<Tile> { new Tile(0), new Tile(null) };
            bool found = TryFind(tiles, out int index);

            Assert.True(found);
            Assert.Equal(1, index);
        }

        [Fact]
        public void SuccessWhenBlankNotInExtremities()
        {
            List<Tile> tiles = new List<Tile> { new Tile(0), new Tile(null), new Tile(2) };
            bool found = TryFind(tiles, out int index);

            Assert.True(found);
            Assert.Equal(1, index);
        }

        private bool TryFind(List<Tile> tiles, out int index)
        {
            BlankTileIndexFinder blankTileIndexFinder = new BlankTileIndexFinder();
            bool found = blankTileIndexFinder.TryFind(tiles, out index);

            return found;
        }
    }
}
