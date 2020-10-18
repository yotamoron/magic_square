using Boards.Tiles;
using System.Collections.Generic;

namespace Boards
{
    public class InversionsCounter
    {
        public int Count(List<Tile> tiles)
        {
            int numberOfInversions = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                Tile currentTile = tiles[index];

                if (currentTile.Value.HasValue)
                {
                    int additionalInversions = Count(tiles, index);

                    numberOfInversions += additionalInversions;
                }
            }

            return numberOfInversions;
        }

        private int Count(List<Tile> tiles, int index)
        {
            int inversionsCount = 0;
            Tile currentTile = tiles[index];

            if (index < tiles.Count)
            {
                int restOfListInitialIndex = index + 1;
                int restOfListSize = tiles.Count - restOfListInitialIndex;

                tiles.GetRange(restOfListInitialIndex, restOfListSize).ForEach(tile =>
                {
                    bool isInverted = tile.Value.HasValue && currentTile.Value > tile.Value;

                    if (isInverted)
                    {
                        inversionsCount++;
                    }
                });
            }

            return inversionsCount;
        }
    }
}
