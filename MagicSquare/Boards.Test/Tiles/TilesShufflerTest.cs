using Boards.Tiles;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Boards.Test.Tiles
{
    public class TilesShufflerTest
    {
        [Fact]
        public void ValidateShuffle()
        {
            List<Tile> original = Enumerable.Range(0, 24).Select(value => new Tile(value)).ToList();
            List<Tile> shuffled = new List<Tile>(original);
            TilesShuffler shuffler = new TilesShuffler();

            shuffler.Shuffle(shuffled);
            Assert.NotEqual(original, shuffled);
        }
    }
}
