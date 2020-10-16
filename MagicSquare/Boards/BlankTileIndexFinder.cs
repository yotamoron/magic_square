using System.Collections.Generic;

namespace Boards
{
    public class BlankTileIndexFinder
    {
        public int Find(List<Tile> tiles)
        {
            int blankIndex = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                if (!tiles[index].Value.HasValue)
                {
                    blankIndex = index;
                    break;
                }
            }
            return blankIndex;
        }
    }
}
