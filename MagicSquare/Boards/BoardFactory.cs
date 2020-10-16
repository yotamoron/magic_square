using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards
{
    public class BoardFactory
    {
        private readonly Random rand = new Random();

        private readonly LegalBoardValidator legalBoardValidator ;
        private readonly BlankTileIndexFinder blankTileIndexFinder;

        public BoardFactory(LegalBoardValidator legalBoardValidator, BlankTileIndexFinder blankTileIndexFinder)
        {
            this.legalBoardValidator = legalBoardValidator;
            this.blankTileIndexFinder = blankTileIndexFinder;
        }

        public Board Build(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException($"Board size must be bigger then 0, got {size}");
            }
            List<Tile> tiles = GetLegalBoardTiles(size);
            int blankIndex = blankTileIndexFinder.Find(tiles);
            int numberOfMisplacedTiles = CountNumberOfMisplacedTiles(tiles);
            Board board = new Board
            {
                Tiles = tiles,
                BlankIndex = blankIndex,
                PuzzleN = size,
                NumberOfMisplacedTiles = numberOfMisplacedTiles
            };

            return board;
        }

        private int CountNumberOfMisplacedTiles(List<Tile> tiles)
        {
            int numberOfMisplacedTiles = 0;
            for (int index = 0; index < tiles.Count; index++)
            {
                Tile tile = tiles[index];
                bool isMisplacedTile = tile.Value.HasValue && tile.Value != index;

                if (isMisplacedTile)
                {
                    numberOfMisplacedTiles++;
                }
            }

            return numberOfMisplacedTiles;
        }

        private List<Tile> GetLegalBoardTiles(int size)
        {
            int numberOfTiles = size * size - 1;
            List<Tile> tiles;

            do
            {
                tiles = Enumerable.Range(0, numberOfTiles).Select(i => new Tile(i)).ToList();
                tiles.Add(new Tile(null));
                Shuffle(tiles);

            } while (!legalBoardValidator.Validate(tiles, size));

            return tiles;
        }

        private void Shuffle(List<Tile> tiles)
        {
            int n = tiles.Count * 2;

            while (n > 1)
            {
                n--;
                int k = rand.Next(tiles.Count);
                int j = rand.Next(tiles.Count);
                Tile value = tiles[k];
                tiles[k] = tiles[j];
                tiles[j] = value;
            }
        }       
    }
}
