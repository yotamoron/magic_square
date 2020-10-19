using Boards;
using Boards.Movements;
using Boards.Tiles;

namespace MagicSquare
{
    public class ActionApplier
    {
        private readonly string newGameSymbol;
        private readonly string endGameSymbol;
        private readonly TileMover tileMover;
        private readonly MovementDisplayNamesResolver movementDisplayNamesResolver;
        private readonly IO.IO io;

        public ActionApplier(string newGameSymbol, string endGameSymbol,
            TileMover tileMover, MovementDisplayNamesResolver movementDisplayNamesResolver,
            IO.IO io)
        {
            this.newGameSymbol = newGameSymbol;
            this.endGameSymbol = endGameSymbol;
            this.tileMover = tileMover;
            this.movementDisplayNamesResolver = movementDisplayNamesResolver;
            this.io = io;
        }

        public virtual GameFlow ApplyAction(Board board, string action)
        {
            GameFlow flow = GameFlow.KEEP_PLAYING;

            if (movementDisplayNamesResolver.TryResolve(action, out Movement movement))
            {
                if (!tileMover.TryMove(board, movement, out string error))
                {
                    io.WriteLine($"Failed moving a tile: {error}", 3000);
                }
            }
            else if (action == newGameSymbol)
            {
                flow = GameFlow.NEW_GAME;
            }
            else if (action == endGameSymbol)
            {
                flow = GameFlow.END_GAME;
            }
            else
            {
                io.WriteLine($"'{action}' is not a legal input", 3000);
            }

            return flow;
        }
    }
}
