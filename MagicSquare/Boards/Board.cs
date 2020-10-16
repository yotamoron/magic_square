using System.Collections.Generic;

namespace Boards
{
    public class Board
    {
        internal List<Tile> Tiles { get; set; }
        public int PuzzleN { get; internal set; }
        internal int BlankIndex { get; set; }
        internal int NumberOfMisplacedTiles { get; set; }
    }
}
