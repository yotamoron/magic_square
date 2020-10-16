using System.Collections.Generic;

namespace Boards
{
    class BoardPlayer
    {
        public bool TryMove(Board board, Movement movement, out string error)
        {
            error = string.Empty;

            if (!GetLegalMoves(board).Contains(movement))
            {
                error = $"{movement} is an illegal movement!";
                return false;
            }
            int sourceIndex = board.BlankIndex + GetMovementDelta(board, movement);
            Tile tileToMove = board.Tiles[sourceIndex];
            bool tileWasOnRightIndex = tileToMove.Value == sourceIndex;
            bool tileIsOnRightIndex = tileToMove.Value == board.BlankIndex;

            board.Tiles[board.BlankIndex] = tileToMove;
            board.Tiles[sourceIndex] = null;

            board.BlankIndex = sourceIndex;

            if (tileWasOnRightIndex && !tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles++;
            }
            else if (!tileWasOnRightIndex && tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles--;
            }

            return true;
        }

        private int GetMovementDelta(Board board, Movement movement)
        {
            int delta = 0;

            switch (movement)
            {
                case Movement.DOWN:
                    delta = -board.PuzzleN;
                    break;
                case Movement.UP:
                    delta = board.PuzzleN;
                    break;
                case Movement.LEFT:
                    delta = 1;
                    break;
                case Movement.RIGHT:
                    delta = -1;
                    break;
            }

            return delta;
        }

        public List<Movement> GetLegalMoves(Board board)
        {
            List<Movement> legalMovements = new List<Movement>();
            Coordinates destinationCoordinates = GetBlankIndexCoordinates(board);
            int edge = board.PuzzleN - 1;

            if (destinationCoordinates.Column != edge)
            {
                legalMovements.Add(Movement.LEFT);
            }
            if (destinationCoordinates.Column != 0)
            {
                legalMovements.Add(Movement.RIGHT);
            }
            if (destinationCoordinates.Row != edge)
            {
                legalMovements.Add(Movement.UP);
            }
            if (destinationCoordinates.Row != 0)
            {
                legalMovements.Add(Movement.DOWN);
            }

            return legalMovements;
        }

        public bool TryGetValueAt(Board board, int row, int column, out string value, out string error)
        {
            value = string.Empty;
            error = string.Empty;
            int edge = board.PuzzleN - 1;

            if (row < 1 || row > edge)
            {
                error = $"Illegal row number: {row}";
                return false;
            }
            if (column < 1 || column > edge)
            {
                error = $"Illegal column number: {column}";
                return false;
            }
            int index = row * board.PuzzleN + column;
            Tile tile = board.Tiles[index];

            value = tile.Value.HasValue ? $"{tile.Value}" : string.Empty;
            return true;
        }

        private Coordinates GetBlankIndexCoordinates(Board board)
        {
            return new Coordinates
            {
                Column = board.BlankIndex % board.PuzzleN,
                Row = board.BlankIndex / board.PuzzleN
            };
        }
    }

    class Coordinates
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
