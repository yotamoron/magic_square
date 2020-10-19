using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Tiles
{
    public class TilesShuffler
    {
        private readonly Random rand = new Random();

        public virtual void Shuffle(List<Tile> tiles)
        {
            List<Tile> original = new List<Tile>(tiles);

            while (Enumerable.SequenceEqual(tiles, original))
            {
                int n = tiles.Count * 2;

                while (n > 1)
                {
                    n--;
                    int k = rand.Next(tiles.Count);
                    int j = rand.Next(tiles.Count);
                    Tile value = tiles[k];
                    tiles[k] = tiles[j];
                    tiles[j] = value;
                }
            }
        }
    }
}
