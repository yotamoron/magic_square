using Boards.Movements;
using Boards.Test.Tiles;
using Xunit;

namespace Boards.Test.Movements
{
    public class MovementDeltaCalculatorTest
    {
        private readonly int SIZE = 4;
        [Fact]
        public void ValidateDownDelta()
        {
            TestMovement(Movement.DOWN, -SIZE);
        }

        [Fact]
        public void ValidateUpDelta()
        {
            TestMovement(Movement.UP, SIZE);
        }

        [Fact]
        public void ValidateLeftDelta()
        {
            TestMovement(Movement.LEFT, 1);
        }

        [Fact]
        public void ValidateRightDelta()
        {
            TestMovement(Movement.RIGHT, -1);
        }

        private void TestMovement(Movement movement, int expected)
        {
            MovementDeltaCalculator movementDeltaCalculator = new MovementDeltaCalculator();
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleSize = SIZE
            };

            int delta = movementDeltaCalculator.GetMovementDelta(board, movement);

            Assert.Equal(expected, delta);
        }
    }
}
