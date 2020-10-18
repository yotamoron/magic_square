using System.Collections.Generic;

namespace Boards.Tiles
{
    public class BlankTileIndexFinder
    {
        public bool TryFind(List<Tile> tiles, out int tileIndex)
        {
            tileIndex = -1;

            for (int index = 0; index < tiles.Count; index++)
            {
                if (!tiles[index].Value.HasValue)
                {
                    bool alreadyFound = tileIndex != -1;

                    if (alreadyFound)
                    {
                        tileIndex = -1;
                        break;
                    }
                    else
                    {
                        tileIndex = index;
                    }
                }
            }
            return tileIndex != -1;
        }
    }
}
