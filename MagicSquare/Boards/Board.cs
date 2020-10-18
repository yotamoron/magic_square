using Boards.Tiles;
using System.Collections.Generic;

namespace Boards
{
    public class Board
    {
        protected internal List<Tile> Tiles { get; set; }
        public int Size { get; protected internal set; }
        protected internal int BlankIndex { get; set; }
        public int TotalMisplacedTiles { get; protected internal set; }
        public bool IsSolved { get; protected internal set; }
    }
}
