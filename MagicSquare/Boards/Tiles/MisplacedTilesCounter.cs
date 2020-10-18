using System.Collections.Generic;

namespace Boards.Tiles
{
    public class MisplacedTilesCounter
    {
        public int Count(List<Tile> tiles)
        {
            int numberOfMisplacedTiles = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                Tile tile = tiles[index];
                bool isMisplacedTile = tile.Value.HasValue && tile.Value != index;

                if (isMisplacedTile)
                {
                    numberOfMisplacedTiles++;
                }
            }

            return numberOfMisplacedTiles;
        }
    }
}
