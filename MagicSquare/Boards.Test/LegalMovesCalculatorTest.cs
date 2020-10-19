using Boards.Movements;
using Common.Tests;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Boards.Test
{
    public class LegalMovesCalculatorTest
    {
        [Fact]
        public void UpperLeftCorner()
        {
            Validate(0, new List<Movement> { Movement.LEFT, Movement.UP });
        }

        [Fact]
        public void UpperRightCorner()
        {
            Validate(2, new List<Movement> { Movement.RIGHT, Movement.UP });
        }

        [Fact]
        public void LowerRightCorner()
        {
            Validate(8, new List<Movement> { Movement.RIGHT, Movement.DOWN });
        }

        [Fact]
        public void LowerLeftCorner()
        {
            Validate(6, new List<Movement> { Movement.LEFT, Movement.DOWN });
        }

        [Fact]
        public void MiddleLeftSide()
        {
            Validate(3, new List<Movement> { Movement.LEFT, Movement.DOWN, Movement.UP });
        }

        [Fact]
        public void MiddleUpSide()
        {
            Validate(1, new List<Movement> { Movement.LEFT, Movement.RIGHT, Movement.UP });
        }

        [Fact]
        public void MiddleRightSide()
        {
            Validate(5, new List<Movement> { Movement.DOWN, Movement.RIGHT, Movement.UP });
        }

        [Fact]
        public void MiddleBottomSide()
        {
            Validate(7, new List<Movement> { Movement.DOWN, Movement.RIGHT, Movement.LEFT });
        }

        [Fact]
        public void MiddleMiddle()
        {
            Validate(4, new List<Movement> { Movement.DOWN, Movement.RIGHT, Movement.LEFT, Movement.UP });
        }

        private void Validate(int blankIndex, List<Movement> expectedLegalMovements)
        {
            AccessibeBoard board = new AccessibeBoard
            {
                AccessibleSize = 3,
                AccessibleBlankIndex = blankIndex
            };
            LegalMovesCalculator legalMovesCalculator = new LegalMovesCalculator();
            List<Movement> legalMovements = legalMovesCalculator.GetLegalMoves(board);
            bool areEquivalent = (expectedLegalMovements.Count == legalMovements.Count) && !expectedLegalMovements.Except(legalMovements).Any();

            Assert.True(areEquivalent);
        }
    }
}
