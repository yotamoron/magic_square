using Boards;
using Boards.Tiles;
using System.Collections.Generic;

namespace Common.Tests
{
    public class AccessibeBoard : Board
    {
        public List<Tile> AccessibleTiles
        {
            get { return Tiles; }
            set { Tiles = value; }
        }

        public int AccessibleBlankIndex
        {
            get { return BlankIndex; }
            set { BlankIndex = value; }
        }

        public int AccessibleSize {
            get { return Size; }
            set { Size = value; }
        }

        public int AccessibleTotalMisplacedTiles {
            get { return TotalMisplacedTiles; }
            set { TotalMisplacedTiles = value; }
        }        

        public bool AccessibleIsSolved
        {
            get { return IsSolved; }
            set { IsSolved = value; }
        }
    }
}
