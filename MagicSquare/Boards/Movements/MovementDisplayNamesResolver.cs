using System.Collections.Generic;
using System.Linq;

namespace Boards.Movements
{
    public class MovementDisplayNamesResolver
    {
        private readonly Dictionary<Movement, string> movementDisplayNames;

        public MovementDisplayNamesResolver(Dictionary<Movement, string> movementDisplayNames)
        {
            this.movementDisplayNames = movementDisplayNames;
        }

        public string Render(List<Movement> legalMoves)
        {
            return string.Join(", ", legalMoves.Select(movement => $"{movement} ({movementDisplayNames[movement]})"));
        }

        public bool TryResolve(string nextAction, out Movement nextMovement)
        {
            nextMovement = default(Movement);

            foreach (KeyValuePair<Movement, string> entry in movementDisplayNames)
            {
                if (entry.Value == nextAction)
                {
                    nextMovement = entry.Key;
                    return true;
                }
            }
            return false;
        }
    }
}
