namespace MagicSquare
{
    public class BoardSizeReader
    {
        private readonly IO.IO io;

        public BoardSizeReader(IO.IO io)
        {
            this.io = io;
        }

        public virtual int Read()
        {

            while (true)
            {
                io.Clear();
                string candidateSize = io.Read("Please enter board size. The size must be a natural number bigger then 0");

                if (!string.IsNullOrEmpty(candidateSize) && int.TryParse(candidateSize, out int size) && size > 0)
                {
                    return size;
                }
                else
                {
                    io.Read($"{candidateSize} is not a valid board size! Press Enter to continue.");
                }
            }
        }
    }
}
