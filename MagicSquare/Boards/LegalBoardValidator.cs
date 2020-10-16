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
            int numberOfInversions = inversionsCounter.CountInversions(tiles);
            int blankIndex = blankTileIndexFinder.Find(tiles);
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

    }
}
