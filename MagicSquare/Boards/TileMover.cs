namespace Boards
{
    public class TileMover
    {
        private readonly LegalMovesCalculator legalMovesCalculator;
        private readonly MovementDeltaCalculator movementDeltaCalculator;

        public TileMover(LegalMovesCalculator legalMovesCalculator, MovementDeltaCalculator movementDeltaCalculator)
        {
            this.legalMovesCalculator = legalMovesCalculator;
            this.movementDeltaCalculator = movementDeltaCalculator;
        }

        public bool TryMove(Board board, Movement movement, out string error)
        {            
            bool isLegalMove = legalMovesCalculator.GetLegalMoves(board).Contains(movement);
            
            if (isLegalMove)
            {
                Move(board, movement);
            }
            error = isLegalMove ? string.Empty : $"{movement} is an illegal movement!";

            return isLegalMove;
        }

        private void UpdateNumberOfMisplacedTiles(Board board, int sourceIndex)
        {
            Tile tileToMove = board.Tiles[sourceIndex];
            bool tileWasOnRightIndex = tileToMove.Value == sourceIndex;
            bool tileIsOnRightIndex = tileToMove.Value == board.BlankIndex;

            if (tileWasOnRightIndex && !tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles++;
            }
            else if (!tileWasOnRightIndex && tileIsOnRightIndex)
            {
                board.NumberOfMisplacedTiles--;
            }
            board.IsSolved = board.NumberOfMisplacedTiles == 0;
        }

        private void Move(Board board, int sourceIndex)
        {
            Tile tileToMove = board.Tiles[sourceIndex];
            Tile blankTile = board.Tiles[board.BlankIndex];

            board.Tiles[board.BlankIndex] = tileToMove;
            board.Tiles[sourceIndex] = blankTile;

            board.BlankIndex = sourceIndex;
        }

        private void Move(Board board, Movement movement)
        {
            int sourceIndex = board.BlankIndex + movementDeltaCalculator.GetMovementDelta(board, movement);
            
            UpdateNumberOfMisplacedTiles(board, sourceIndex);
            Move(board, sourceIndex);
        }
    }
}
