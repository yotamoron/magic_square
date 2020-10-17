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
        private readonly BoardValueFetcher boardValueFetcher;
        private readonly MovementDisplayNamesResolver movementDisplayNamesResolver;
        private readonly TileMover tileMover;
        private readonly LegalMovesCalculator legalMovesCalculator; 

        public MagicSquare(IO.IO io, BoardSizeReader boardSizeReader, BoardFactory boardFactory,
            BoardRenderer boardRenderer, BoardValueFetcher boardValueFetcher,MovementDisplayNamesResolver movementDisplayNamesResolver,
            TileMover tileMover, LegalMovesCalculator legalMovesCalculator)
        {
            this.io = io;
            this.boardSizeReader = boardSizeReader;
            this.boardFactory = boardFactory;
            this.boardRenderer = boardRenderer;
            this.boardValueFetcher = boardValueFetcher;
            this.movementDisplayNamesResolver = movementDisplayNamesResolver;
            this.tileMover = tileMover;
            this.legalMovesCalculator = legalMovesCalculator;
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
                    List<Movement> legalMoves = legalMovesCalculator.GetLegalMoves(board);
                    string movements = movementDisplayNamesResolver.Render(legalMoves);

                    io.Clear();
                    io.WriteLine(renderedBoard);
                                        
                    string nextAction = io.Read($"Please select movement: {movements} - Or New Game ({NEW_GAME})");

                    if (movementDisplayNamesResolver.TryResolve(nextAction, out Movement nextMovement))
                    {
                        if (!tileMover.TryMove(board, nextMovement, out string error))
                        {
                            io.WriteLine($"Failed moving a tile: {error}", 3000);
                        }
                        else if(board.IsSolved)
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
