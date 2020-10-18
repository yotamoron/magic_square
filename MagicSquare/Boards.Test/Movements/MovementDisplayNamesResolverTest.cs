using Boards.Movements;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test.Movements
{
    public class MovementDisplayNamesResolverTest
    {
        [Fact]
        public void FailResolveUnknownAction()
        {
            bool resolved = TryResolve("ThisIsAnIllegelActionName", out Movement movement);

            Assert.False(resolved);
        }

        [Fact]
        public void SuccessfullyResolveKnownAction()
        {
            bool resolved = TryResolve("D", out Movement movement);

            Assert.True(resolved);
            Assert.Equal(Movement.DOWN, movement);
        }

        private bool TryResolve(string action, out Movement movement)
        {
            Dictionary<Movement, string> movementDisplayNames = new Dictionary<Movement, string>()
            {
                {Movement.DOWN, "D" }
            };
            MovementDisplayNamesResolver movementDisplayNamesResolver = new MovementDisplayNamesResolver(movementDisplayNames);

            return movementDisplayNamesResolver.TryResolve(action, out movement);

        }
    }
}
