using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards
{
    public class BoardFactory
    {
        private readonly Random rand = new Random();

        public Board Build(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException($"Board size must be bigger then 0, got {size}");
            }
            List<Tile> tiles = GetLegalBoardTiles(size);
            int blankIndex = GetBlankIndex(tiles);
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

            } while (!IsLegalBoard(tiles, size));

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

        private bool IsLegalBoard(List<Tile> tiles, int size)
        {
            int numberOfInversions = CountInversions(tiles);
            int blankIndex = GetBlankIndex(tiles);
            bool isNumberOfInversionsEven = numberOfInversions % 2 == 0;
            bool isSizeEven = size % 2 == 0;

            if (isSizeEven)
            {
                int blankIndexRow = blankIndex / size;
                bool isBlankOnAnEvenRow = blankIndexRow % 2 == 0;

                return isBlankOnAnEvenRow ^ isNumberOfInversionsEven;
            }
            else
            {
                return isNumberOfInversionsEven;
            }
        }

        private int GetBlankIndex(List<Tile> tiles)
        {
            int blankIndex = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                if (!tiles[index].Value.HasValue)
                {
                    blankIndex = index;
                    break;
                }
            }
            return blankIndex;
        }

        private int CountInversions(List<Tile> tiles)
        {
            int numberOfInversions = 0;

            for (int index = 0; index < tiles.Count; index++)
            {
                Tile currentTile = tiles[index];

                if (currentTile.Value.HasValue)
                {
                    int additionalInversions = CountInversions(tiles, index);

                    numberOfInversions += additionalInversions;
                }
            }

            return numberOfInversions;
        }

        private int CountInversions(List<Tile> tiles, int index)
        {
            int numberOfInversions = 0;
            Tile currentTile = tiles[index];

            if (index < tiles.Count)
            {
                int restOfListInitialIndex = index + 1;
                int restOfListSize = tiles.Count - restOfListInitialIndex;
                tiles.GetRange(restOfListInitialIndex, restOfListSize).ForEach(tile =>
                {
                    bool inverted = tile.Value.HasValue && currentTile.Value > tile.Value;

                    if (inverted)
                    {
                        numberOfInversions++;
                    }
                });
            }

            return numberOfInversions;
        }
    }
}
