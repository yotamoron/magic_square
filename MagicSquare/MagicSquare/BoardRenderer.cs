using Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicSquare
{
    public class BoardRenderer
    {
        private readonly BoardPlayer boardPlayer;

        public BoardRenderer(BoardPlayer boardPlayer)
        {
            this.boardPlayer = boardPlayer;
        }

        public string Render(Board board)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<int> indices = Enumerable.Range(0, board.PuzzleN);

            indices.ToList().ForEach(row =>
            {
                IEnumerable<string> rowValues = indices.Select(col =>
                {
                    if (!boardPlayer.TryGetValueAt(board, row, col, out string value, out string error))
                    {
                        throw new System.Exception($"Something is wrong - can't get value at {row}/{col}: {error}");
                    }
                    else
                    {
                        return value;
                    }
                });
                sb.AppendJoin("\t", rowValues);
                sb.Append(Environment.NewLine);
            });
            return sb.ToString();
        }
    }
}
