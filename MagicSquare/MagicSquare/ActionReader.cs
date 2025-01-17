﻿using Boards;
using Boards.Movements;
using System.Collections.Generic;

namespace MagicSquare
{
    public class ActionReader
    {
        private readonly string newGameSymbol;
        private readonly string endGameSymbol;
        private readonly MovementDisplayNamesResolver movementDisplayNamesResolver;
        private readonly LegalMovesCalculator legalMovesCalculator;
        private readonly IO.IO io;

        public ActionReader(string newGameSymbol, string endGameSymbol, MovementDisplayNamesResolver movementDisplayNamesResolver,
            LegalMovesCalculator legalMovesCalculator, IO.IO io)
        {
            this.newGameSymbol = newGameSymbol;
            this.endGameSymbol = endGameSymbol;
            this.movementDisplayNamesResolver = movementDisplayNamesResolver;
            this.legalMovesCalculator = legalMovesCalculator;
            this.io = io;
        }

        public virtual string ReadAction(Board board)
        {
            List<Movement> legalMoves = legalMovesCalculator.GetLegalMoves(board);
            string movements = movementDisplayNamesResolver.Render(legalMoves);

            return io.Read($"Please select movement: {movements} - Or New Game ({newGameSymbol}) / End Game ({endGameSymbol})");
        }
    }
}
