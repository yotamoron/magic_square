using System;
using System.Collections.Generic;

namespace Boards.Tiles
{
    public class Tile
    {
        public Tile(int? value)
        {
            Value = value;
        }

        public int? Value { get; }

        public override bool Equals(object obj)
        {
            var tile = obj as Tile;
            return tile != null &&
                   EqualityComparer<int?>.Default.Equals(Value, tile.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
