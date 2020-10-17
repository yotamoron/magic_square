namespace Boards.Movements
{
    public class MovementDeltaCalculator
    {
        public int GetMovementDelta(Board board, Movement movement)
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
