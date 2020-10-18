using System;
using System.Collections.Generic;

namespace Boards.Tiles
{
    public class TilesShuffler
    {
        private readonly Random rand = new Random();

        public void Shuffle(List<Tile> tiles)
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
