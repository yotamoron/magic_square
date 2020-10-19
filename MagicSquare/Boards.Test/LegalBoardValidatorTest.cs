using Boards.Tiles;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Boards.Test
{
    public class LegalBoardValidatorTest
    {
        private delegate void BlankIndexSetter(List<Tile> tiles, out int index);

        [Fact]
        public void FailWhenCantFindBlankTile()
        {
            LegalBoardValidator legalBoardValidator = GetLegalBoardValidator(out LegalBoardValidatorContext legalBoardValidatorContext);
            int dontcare = 0;

            legalBoardValidatorContext.BlankTileIndexFinder.Setup(btif => btif.TryFind(It.IsAny<List<Tile>>(), out dontcare)).Returns(false);
            bool found = legalBoardValidator.Validate(new List<Tile>(), dontcare);

            Assert.False(found);
        }

        [Fact]
        public void Fail_SizeEven_InversionsEven_BlankEven()
        {
            bool valid = Validate(true, true, true);

            Assert.False(valid);
        }

        [Fact]
        public void Success_SizeEven_InversionsEven_BlankOdd()
        {
            bool valid = Validate(true, true, false);

            Assert.True(valid);
        }

        [Fact]
        public void Success_SizeEven_InversionsOdd_BlankEven()
        {
            bool valid = Validate(true, false, true);

            Assert.True(valid);
        }

        [Fact]
        public void Fail_SizeEven_InversionsOdd_BlankOdd()
        {
            bool valid = Validate(true, false, false);

            Assert.False(valid);
        }

        [Fact]
        public void Success_SizeOdd_InversionsEven_BlankEven()
        {
            bool valid = Validate(false, true, true);

            Assert.True(valid);
        }

        [Fact]
        public void Success_SizeOdd_InversionsEven_BlankOdd()
        {
            bool valid = Validate(false, true, false);

            Assert.True(valid);
        }

        [Fact]
        public void Fail_SizeOdd_InversionsOdd_BlankEven()
        {
            bool valid = Validate(false, false, true);

            Assert.False(valid);
        }

        [Fact]
        public void Fail_SizeOdd_InversionsOdd_BlankOdd()
        {
            bool valid = Validate(false, false, false);

            Assert.False(valid);
        }

        private bool Validate(bool isSizeEven, bool isInversionsEven, bool isBlankEven)
        {
            LegalBoardValidator legalBoardValidator = GetLegalBoardValidator(out LegalBoardValidatorContext legalBoardValidatorContext);
            int size = isSizeEven ? 2 : 3;
            int inversions = isInversionsEven ? 2 : 3;
            int blankTileIndex = 0;

            legalBoardValidatorContext.BlankTileIndexFinder.Setup(btif => btif.TryFind(It.IsAny<List<Tile>>(), out blankTileIndex))
                .Returns(true)
                .Callback(new BlankIndexSetter((List<Tile> tiles, out int index) =>
                {
                    index = isBlankEven ? 1 : 2;
                }));
            legalBoardValidatorContext.InversionsCounter.Setup(ic => ic.Count(It.IsAny<List<Tile>>()))
                .Returns(inversions);
            bool valid = legalBoardValidator.Validate(new List<Tile>(), size);

            return valid;
        }

        private LegalBoardValidator GetLegalBoardValidator(out LegalBoardValidatorContext legalBoardValidatorContext)
        {
            legalBoardValidatorContext = new LegalBoardValidatorContext
            {
                InversionsCounter = new Mock<InversionsCounter>(),
                BlankTileIndexFinder = new Mock<BlankTileIndexFinder>()
            };

            return new LegalBoardValidator(legalBoardValidatorContext.InversionsCounter.Object,
                legalBoardValidatorContext.BlankTileIndexFinder.Object);
        }
    }

    class LegalBoardValidatorContext
    {
        public Mock<InversionsCounter> InversionsCounter { get; set; }
        public Mock<BlankTileIndexFinder> BlankTileIndexFinder { get; set; }
    }
}
