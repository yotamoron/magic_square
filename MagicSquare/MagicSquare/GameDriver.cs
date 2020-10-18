using Boards;

namespace MagicSquare
{
    public class GameDriver
    {
        private readonly IO.IO io;
        private readonly BoardRenderer boardRenderer;
        private readonly ActionReader actionReader;
        private readonly ActionApplier actionApplier;

        public GameDriver(IO.IO io, BoardRenderer boardRenderer,
            ActionReader actionReader, ActionApplier actionApplier)
        {
            this.io = io;
            this.boardRenderer = boardRenderer;
            this.actionReader = actionReader;
            this.actionApplier = actionApplier;
        }

        public GameFlow Play(Board board)
        {
            GameFlow flow = GameFlow.KEEP_PLAYING;

            while (flow == GameFlow.KEEP_PLAYING)
            {
                PrintBoard(board);
                if (board.IsSolved)
                {
                    io.Read("Congrats! you solved the game! Press Enter to continue");
                    flow = GameFlow.NEW_GAME;
                }
                else
                {
                    string action = actionReader.ReadAction(board);

                    flow = actionApplier.ApplyAction(board, action);
                }
            }
            return flow;
        }

        private void PrintBoard(Board board)
        {
            string renderedBoard = boardRenderer.Render(board);

            io.Clear();
            io.WriteLine(renderedBoard);
        }
    }
}
