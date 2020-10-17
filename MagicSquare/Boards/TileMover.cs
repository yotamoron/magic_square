namespace Boards
{
    public class TileMover
    {
        private readonly LegalMovesCalculator legalMovesCalculator;

        public TileMover(LegalMovesCalculator legalMovesCalculator)
        {
            this.legalMovesCalculator = legalMovesCalculator;
        }

        public bool TryMove(Board board, Movement movement, out string error)
        {
            error = string.Empty;

            if (!legalMovesCalculator.GetLegalMoves(board).Contains(movement))
            {
                error = $"{movement} is an illegal movement!";
                return false;
            }
            int sourceIndex = board.BlankIndex + GetMovementDelta(board, movement);
            Tile tileToMove = board.Tiles[sourceIndex];
            Tile blankTile = board.Tiles[board.BlankIndex];
            bool tileWasOnRightIndex = tileToMove.Value == sourceIndex;
            bool tileIsOnRightIndex = tileToMove.Value == board.BlankIndex;

            board.Tiles[board.BlankIndex] = tileToMove;
            board.Tiles[sourceIndex] = blankTile;

            board.BlankIndex = sourceIndex;

            if (tileWasOnRightIndex && !tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles++;
            }
            else if (!tileWasOnRightIndex && tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles--;
            }

            board.IsSolved = board.NumberOfMisplacedTiles == 0;
            return true;
        }

        private int GetMovementDelta(Board board, Movement movement)
        {
            int delta = 0;

            switch (movement)
            {
                case Movement.DOWN:
                    delta = -board.Size;
                    break;
                case Movement.UP:
                    delta = board.Size;
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
    }
}
