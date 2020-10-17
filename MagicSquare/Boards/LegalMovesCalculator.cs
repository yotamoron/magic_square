using Boards.Movements;
using System.Collections.Generic;

namespace Boards
{
    public class LegalMovesCalculator
    {
        public List<Movement> GetLegalMoves(Board board)
        {
            List<Movement> legalMovements = new List<Movement>();
            Coordinates blankCoordinates = GetBlankIndexCoordinates(board);
            int edge = board.Size - 1;

            if (blankCoordinates.Column != edge)
            {
                legalMovements.Add(Movement.LEFT);
            }
            if (blankCoordinates.Column != 0)
            {
                legalMovements.Add(Movement.RIGHT);
            }
            if (blankCoordinates.Row != edge)
            {
                legalMovements.Add(Movement.UP);
            }
            if (blankCoordinates.Row != 0)
            {
                legalMovements.Add(Movement.DOWN);
            }

            return legalMovements;
        }

        private Coordinates GetBlankIndexCoordinates(Board board)
        {
            return new Coordinates
            {
                Column = board.BlankIndex % board.Size,
                Row = board.BlankIndex / board.Size
            };
        }
    }

    class Coordinates
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }

}
