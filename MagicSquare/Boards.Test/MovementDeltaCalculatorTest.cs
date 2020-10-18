using Boards.Movements;
using Moq;
using Xunit;

namespace Boards.Test
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
            Mock<Board> board = new Mock<Board>();

            board.SetupGet(b => b.Size).Returns(SIZE);
            int delta = movementDeltaCalculator.GetMovementDelta(board.Object, movement);

            Assert.Equal(expected, delta);
        }    
    }
}
