using Boards;

namespace MagicSquare
{
    public class MagicSquare
    {
        private readonly IO.IO io;
        private readonly BoardSizeReadingBoardFactory boardSizeReadingBoardFactory;
        private readonly GameDriver gameDriver;

        public MagicSquare(IO.IO io, BoardSizeReadingBoardFactory boardSizeReadingBoardFactory, GameDriver gameDriver)
        {
            this.io = io;
            this.boardSizeReadingBoardFactory = boardSizeReadingBoardFactory;
            this.gameDriver = gameDriver;
        }

        private void Welcome()
        {
            io.Clear();
            io.WriteLine("Welcome to the MagicSquare!", 1000);
        }

        public void Play()
        {
            Welcome();

            while (true)
            {
                Board board = boardSizeReadingBoardFactory.GetBoard();

                gameDriver.Play(board);
            }
        }
    }
}
