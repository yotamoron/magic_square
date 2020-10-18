using Boards.Tiles;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Boards.Test.Tiles
{
    public class MisplacedTilesCounterTest
    {
        [Fact]
        public void NoMisplacedTiles()
        {
            AssertTotalMisplaced(new List<int?> { 0, 1, 2, null }, 0);
        }

        [Fact]
        public void AllTilesAreMisplaced()
        {
            AssertTotalMisplaced(new List<int?> { null, 0, 1, 2 }, 3);
        }

        [Fact]
        public void SomeTilesAreMisplaced()
        {
            AssertTotalMisplaced(new List<int?> { 0, 1, null, 2 }, 1);
        }

        private void AssertTotalMisplaced(List<int?> values, int expected)
        {
            int totalMisplaced = CountMisplaced(values);

            Assert.Equal(expected, totalMisplaced);
        }

        private int CountMisplaced(List<int?> values)
        {
            List<Tile> tiles = values.Select(value => new Tile(value)).ToList();
            MisplacedTilesCounter misplacedTilesCounter = new MisplacedTilesCounter();
            int totalMisplaced = misplacedTilesCounter.Count(tiles);

            return totalMisplaced;
        }
    }
}
