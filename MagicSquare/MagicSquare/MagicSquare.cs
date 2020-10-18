using Boards;
using Boards.Movements;
using Boards.Tiles;
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

        private void Welcome()
        {
            io.Clear();
            io.WriteLine("Welcome to the MagicSquare!", 1000);
        }

        private Board GetBoard()
        {
            int size = boardSizeReader.Read();

            return boardFactory.Build(size);
        }

        private void RenderBoard(Board board)
        {
            string renderedBoard = boardRenderer.Render(board);

            io.Clear();
            io.WriteLine(renderedBoard);
        }

        private string ReadNextAction(Board board)
        {
            List<Movement> legalMoves = legalMovesCalculator.GetLegalMoves(board);
            string movements = movementDisplayNamesResolver.Render(legalMoves);

             return io.Read($"Please select movement: {movements} - Or New Game ({NEW_GAME})");
        }

        private bool ApplyNextAction(Board board, string nextAction)
        {
            bool keepPlaying = true;

            if (movementDisplayNamesResolver.TryResolve(nextAction, out Movement nextMovement))
            {
                if (!tileMover.TryMove(board, nextMovement, out string error))
                {
                    io.WriteLine($"Failed moving a tile: {error}", 3000);
                }
            }
            else if (nextAction == NEW_GAME)
            {
                keepPlaying = false;
            }
            else
            {
                io.WriteLine($"'{nextAction}' is not a legal input", 3000);
            }

            return keepPlaying;
        }

        private void Play(Board board)
        {
            bool keepPlaying = true;

            while (keepPlaying)
            {
                RenderBoard(board);
                if (board.IsSolved)
                {
                    io.Read("Congrats! you solved the game! Press Enter to continue");
                    keepPlaying = false;
                }
                else
                {
                    string nextAction = ReadNextAction(board);

                    keepPlaying = ApplyNextAction(board, nextAction);
                }                
            }
        }

        public void Play()
        {
            Welcome();

            while (true)
            {
                Board board = GetBoard();

                Play(board);
            }
        }
    }
}
