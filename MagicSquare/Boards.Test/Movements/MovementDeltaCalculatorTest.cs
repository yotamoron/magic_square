﻿using Boards.Movements;
using Boards.Test.Tiles;
using Common.Tests;
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
            AccessibleBoard board = new AccessibleBoard
            {
                AccessibleSize = SIZE
            };

            int delta = movementDeltaCalculator.GetMovementDelta(board, movement);

            Assert.Equal(expected, delta);
        }
    }
}
