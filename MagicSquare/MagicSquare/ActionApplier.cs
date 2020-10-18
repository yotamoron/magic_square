using Boards;
using Boards.Movements;
using Boards.Tiles;

namespace MagicSquare
{
    public class ActionApplier
    {
        private readonly string newGameSymbol;
        private readonly TileMover tileMover;
        private readonly MovementDisplayNamesResolver movementDisplayNamesResolver;
        private readonly IO.IO io;

        public ActionApplier(string newGameSymbol, TileMover tileMover, MovementDisplayNamesResolver movementDisplayNamesResolver,
            IO.IO io)
        {
            this.newGameSymbol = newGameSymbol;
            this.tileMover = tileMover;
            this.movementDisplayNamesResolver = movementDisplayNamesResolver;
            this.io = io;
        }

        public bool ApplyAction(Board board, string action)
        {
            bool keepPlaying = true;

            if (movementDisplayNamesResolver.TryResolve(action, out Movement movement))
            {
                if (!tileMover.TryMove(board, movement, out string error))
                {
                    io.WriteLine($"Failed moving a tile: {error}", 3000);
                }
            }
            else if (action == newGameSymbol)
            {
                keepPlaying = false;
            }
            else
            {
                io.WriteLine($"'{action}' is not a legal input", 3000);
            }

            return keepPlaying;
        }
    }
}
