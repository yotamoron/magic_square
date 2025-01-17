﻿using Boards.Tiles;

namespace Boards
{
    public class BoardValueFetcher
    {
        public virtual bool TryGetValueAt(Board board, int row, int column, out int? value, out string error)
        {
            bool isRequestedCoordinatesWithinBoard = IsWithinBoard(board, row, "row", out error) && IsWithinBoard(board, column, "column", out error);

            if (isRequestedCoordinatesWithinBoard)
            {
                int index = row * board.Size + column;
                Tile tile = board.Tiles[index];

                value = tile.Value.HasValue ? tile.Value + 1 : tile.Value;
            }
            else
            {
                value = null;
            }
            return isRequestedCoordinatesWithinBoard;
        }

        private bool IsWithinBoard(Board board, int coordinate, string coordinateName, out string error)
        {
            bool isWithinBoard = coordinate > -1 && coordinate < board.Size;

            error = isWithinBoard ? string.Empty : $"Illegal {coordinateName} number: {coordinate}";
            
            return isWithinBoard;
        }
    }
}
