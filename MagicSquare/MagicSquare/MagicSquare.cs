using Boards;
using System.Collections.Generic;

namespace MagicSquare
{
    public class MagicSquare
    {
        private static readonly string NEW_GAME = "N";
        private readonly IO.IO io;
        private readonly BoardSizeReader boardSizeReader;
        private readonly BoardFactory boardFactory;
        private readonly BoardRenderer boardRenderer;
        private readonly BoardPlayer boardPlayer;
        private readonly MovementDisplayNamesResolver movementDisplayNamesResolver;

        public MagicSquare(IO.IO io, BoardSizeReader boardSizeReader, BoardFactory boardFactory,
            BoardRenderer boardRenderer, BoardPlayer boardPlayer,MovementDisplayNamesResolver movementDisplayNamesResolver)
        {
            this.io = io;
            this.boardSizeReader = boardSizeReader;
            this.boardFactory = boardFactory;
            this.boardRenderer = boardRenderer;
            this.boardPlayer = boardPlayer;
            this.movementDisplayNamesResolver = movementDisplayNamesResolver;
        }

        public void Play()
        {
            io.WriteLine("Welcome to the MagicSquare!", 1000);

            while (true)
            {
                int size = boardSizeReader.Read();
                Board board = boardFactory.Build(size);
                bool playCurrentGame = true;

                while (playCurrentGame)
                {
                    string renderedBoard = boardRenderer.Render(board);
                    List<Movement> legalMoves = boardPlayer.GetLegalMoves(board);
                    string movements = movementDisplayNamesResolver.Render(legalMoves);

                    io.Clear();
                    io.WriteLine(renderedBoard);
                                        
                    string nextAction = io.Read($"Please select movement: {movements} - Or New Game ({NEW_GAME})");

                    if (movementDisplayNamesResolver.TryResolve(nextAction, out Movement nextMovement))
                    {
                        if (!boardPlayer.TryMove(board, nextMovement, out string error))
                        {
                            io.WriteLine($"Failed moving a tile: {error}", 3000);
                        }
                        else if(boardPlayer.IsSolved(board))
                        {
                            io.Read("Congrats! you solved the game! Press Enter to continue");
                            playCurrentGame = false;
                        } 
                    }
                    else if (nextAction == NEW_GAME)
                    {
                        playCurrentGame = false;
                    }
                    else
                    {
                        io.WriteLine($"'{nextAction}' is not a legal input", 3000);
                    }
                }
            }
        }
    }
}
