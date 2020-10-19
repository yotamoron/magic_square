using Boards;
using Boards.Movements;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MagicSquare.Test
{
    public class ActionReaderTest
    {
        private static readonly Random random = new Random();

        [Fact]
        public void Sanity()
        {
            Mock<MovementDisplayNamesResolver> movementDisplayNamesResolver = new Mock<MovementDisplayNamesResolver>(MockBehavior.Strict, new object[] { null });
            Mock<LegalMovesCalculator> legalMovesCalculator = new Mock<LegalMovesCalculator>();
            Mock<IO.IO> io = new Mock<IO.IO>();
            string endGameSymbol = RandomString(100);
            string newGameSymbol = RandomString(100);
            string movements = RandomString(100);
            string expectedResponse = RandomString(100);
            ActionReader actionReader = new ActionReader(newGameSymbol, endGameSymbol, movementDisplayNamesResolver.Object,
                legalMovesCalculator.Object, io.Object);
            legalMovesCalculator.Setup(lmc => lmc.GetLegalMoves(It.IsAny<Board>())).Returns(new List<Movement>());
            movementDisplayNamesResolver.Setup(mdnr => mdnr.Render(It.IsAny<List<Movement>>())).Returns(movements);
            io.Setup(o =>
                o.Read(Match.Create<string>(msg =>
                    msg.Contains(endGameSymbol) &&
                    msg.Contains(newGameSymbol) &&
                    msg.Contains(movements))))
                    .Returns(expectedResponse);

            string response = actionReader.ReadAction(new Board());
            Assert.Equal(expectedResponse, response);
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
