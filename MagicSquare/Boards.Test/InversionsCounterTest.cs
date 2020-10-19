using Boards.Tiles;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test
{
    public class InversionsCounterTest
    {
        [Fact]
        public void NoInversionsForSolvedBoard()
        {
            List<Tile> tiles = new List<Tile> { new Tile(0), new Tile(1), new Tile(2), new Tile(null) };
            int totalInversions = Count(tiles);

            Assert.Equal(0, totalInversions);
        }

        [Fact]
        public void NoInversionsWhenBlankTileIsNotInTheBottomRightCorner()
        {
            List<Tile> tiles = new List<Tile> { new Tile(0), new Tile(1), new Tile(null), new Tile(2) };
            int totalInversions = Count(tiles);

            Assert.Equal(0, totalInversions);
        }

        [Fact]
        public void OneInversion()
        {
            List<Tile> tiles = new List<Tile> { new Tile(1), new Tile(0), new Tile(null), new Tile(2) };
            int totalInversions = Count(tiles);

            Assert.Equal(1, totalInversions);
        }

        [Fact]
        public void AllInverted()
        {
            List<Tile> tiles = new List<Tile> { new Tile(2), new Tile(1), new Tile(null), new Tile(0) };
            int totalInversions = Count(tiles);

            Assert.Equal(3, totalInversions);
        }

        private int Count(List<Tile> tiles)
        {
            InversionsCounter inversionsCounter = new InversionsCounter();
            int totalInversions = inversionsCounter.Count(tiles);

            return totalInversions;
        }
    }
}
