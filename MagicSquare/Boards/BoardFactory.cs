using Boards.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards
{
    public class BoardFactory
    {
        private readonly LegalBoardValidator legalBoardValidator ;
        private readonly BlankTileIndexFinder blankTileIndexFinder;
        private readonly MisplacedTilesCounter misplacedTilesCounter;
        private readonly TilesShuffler tilesShuffler;

        public BoardFactory(LegalBoardValidator legalBoardValidator, BlankTileIndexFinder blankTileIndexFinder,
            MisplacedTilesCounter misplacedTilesCounter, TilesShuffler tilesShuffler)
        {
            this.legalBoardValidator = legalBoardValidator;
            this.blankTileIndexFinder = blankTileIndexFinder;
            this.misplacedTilesCounter = misplacedTilesCounter;
            this.tilesShuffler = tilesShuffler;
        }

        public Board Build(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException($"Board size must be bigger then 0, got {size}");
            }
            List<Tile> tiles = GetLegalBoardTiles(size);
            int blankIndex = blankTileIndexFinder.TryFind(tiles, out int index) ? index: throw new Exception("Illegal number of tiles!");
            int numberOfMisplacedTiles = misplacedTilesCounter.Count(tiles);
            Board board = new Board
            {
                Tiles = tiles,
                BlankIndex = blankIndex,
                Size = size,
                TotalMisplacedTiles = numberOfMisplacedTiles,
                IsSolved = numberOfMisplacedTiles == 0
            };

            return board;
        }

        private List<Tile> GetLegalBoardTiles(int size)
        {
            int numberOfTiles = size * size - 1;
            List<Tile> tiles = Enumerable.Range(0, numberOfTiles).Select(i => new Tile(i)).ToList(); ;

            tiles.Add(new Tile(null));
            do
            {
                tilesShuffler.Shuffle(tiles);
            } while (!legalBoardValidator.Validate(tiles, size));

            return tiles;
        }
    }
}
