using Boards.Tiles;
using System.Collections.Generic;

namespace Boards
{
    public class Board
    {
        internal List<Tile> Tiles { get; set; }
        public virtual int Size { get; internal set; }
        internal int BlankIndex { get; set; }
        public int TotalMisplacedTiles { get; internal set; }
        public bool IsSolved { get; internal set; }
    }
}
