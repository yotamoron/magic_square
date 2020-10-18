using Boards.Tiles;
using System.Collections.Generic;

namespace Boards
{
    public class LegalBoardValidator
    {
        private readonly InversionsCounter inversionsCounter;
        private readonly BlankTileIndexFinder blankTileIndexFinder;

        public LegalBoardValidator(InversionsCounter inversionsCounter, BlankTileIndexFinder blankTileIndexFinder)
        {
            this.inversionsCounter = inversionsCounter;
            this.blankTileIndexFinder = blankTileIndexFinder;
        }

        public bool Validate(List<Tile> tiles, int size)
        {
            bool isValid = blankTileIndexFinder.TryFind(tiles, out int blankIndex);
            
            if (isValid)
            {
                int numberOfInversions = inversionsCounter.Count(tiles);
                bool isNumberOfInversionsEven = numberOfInversions % 2 == 0;
                bool isSizeEven = size % 2 == 0;

                if (isSizeEven)
                {
                    int blankIndexRow = blankIndex / size;
                    bool isBlankOnAnEvenRow = blankIndexRow % 2 == 0;

                    isValid = isBlankOnAnEvenRow ^ isNumberOfInversionsEven;
                }
                else
                {
                    isValid = isNumberOfInversionsEven;
                }
            }
            return isValid;
        }
    }
}
