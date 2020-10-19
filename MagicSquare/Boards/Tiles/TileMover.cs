using Boards.Movements;

namespace Boards.Tiles
{
    public class TileMover
    {
        private readonly LegalMovesCalculator legalMovesCalculator;
        private readonly MovementDeltaCalculator movementDeltaCalculator;
        private readonly MisplacedTilesCounter misplacedTilesCounter;

        public TileMover(LegalMovesCalculator legalMovesCalculator, MovementDeltaCalculator movementDeltaCalculator,
            MisplacedTilesCounter misplacedTilesCounter)
        {
            this.legalMovesCalculator = legalMovesCalculator;
            this.movementDeltaCalculator = movementDeltaCalculator;
            this.misplacedTilesCounter = misplacedTilesCounter;
        }

        public virtual bool TryMove(Board board, Movement movement, out string error)
        {            
            bool isLegalMove = legalMovesCalculator.GetLegalMoves(board).Contains(movement);
            
            if (isLegalMove)
            {
                Move(board, movement);
            }
            error = isLegalMove ? string.Empty : $"{movement} is an illegal movement!";

            return isLegalMove;
        }

        private void UpdateTotalMisplacedTiles(Board board)
        {            
            board.TotalMisplacedTiles = misplacedTilesCounter.Count(board.Tiles);            
            board.IsSolved = board.TotalMisplacedTiles == 0;
        }

        private void Move(Board board, int sourceIndex)
        {
            int blankIndex = board.BlankIndex;
            Tile tileToMove = board.Tiles[sourceIndex];            
            Tile blankTile = board.Tiles[blankIndex];

            board.Tiles[blankIndex] = tileToMove;
            board.Tiles[sourceIndex] = blankTile;

            board.BlankIndex = sourceIndex;
        }

        private void Move(Board board, Movement movement)
        {
            int sourceIndex = board.BlankIndex + movementDeltaCalculator.GetMovementDelta(board, movement);
                        
            Move(board, sourceIndex);
            UpdateTotalMisplacedTiles(board);
        }
    }
}
