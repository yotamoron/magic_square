using System.Collections.Generic;

namespace Boards
{
    public class InversionsCounter
    {
        public int CountInversions(List<Tile> tiles)
        {
            int numberOfInversions = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                Tile currentTile = tiles[index];

                if (currentTile.Value.HasValue)
                {
                    int additionalInversions = CountInversions(tiles, index);

                    numberOfInversions += additionalInversions;
                }
            }

            return numberOfInversions;
        }

        private int CountInversions(List<Tile> tiles, int index)
        {
            int numberOfInversions = 0;
            Tile currentTile = tiles[index];

            if (index < tiles.Count)
            {
                int restOfListInitialIndex = index + 1;
                int restOfListSize = tiles.Count - restOfListInitialIndex;
                tiles.GetRange(restOfListInitialIndex, restOfListSize).ForEach(tile =>
                {
                    bool inverted = tile.Value.HasValue && currentTile.Value > tile.Value;

                    if (inverted)
                    {
                        numberOfInversions++;
                    }
                });
            }

            return numberOfInversions;
        }
    }
}
