using Boards;

namespace MagicSquare
{
    public class BoardSizeReadingBoardFactory
    {
        private readonly BoardSizeReader boardSizeReader;
        private readonly BoardFactory boardFactory;

        public BoardSizeReadingBoardFactory(BoardSizeReader boardSizeReader, BoardFactory boardFactory)
        {
            this.boardSizeReader = boardSizeReader;
            this.boardFactory = boardFactory;
        }

        public Board GetBoard()
        {
            int size = boardSizeReader.Read();

            return boardFactory.Build(size);
        }
    }
}
