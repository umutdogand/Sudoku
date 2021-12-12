using Microsoft.AspNetCore.Http;
using Sudoku.Common;

namespace Sudoku.Service
{
    public class GameService : IGameService
    {
        private readonly IGenerator generator;
        private readonly ISolver solver;
        private readonly IGame game;

        public GameService(IGame game, IGenerator generator, ISolver solver)
        {
            this.generator = generator;
            this.solver = solver;
            this.game = game;
        }

        public BoardViewModel GameBoard(bool uniqueControl, EnumDifficulty diffuculty = EnumDifficulty.Easy, int boardSize = 9)
        {
            game.Reset(boardSize);

            generator.Generate();

            var unSolvedBoard = SudokuHelper.GetGameBoard(game.GetBoard().CloneMatrix(), diffuculty);

            while (uniqueControl && SudokuHelper.UniqueSolution(unSolvedBoard.CloneMatrix(), game.GetBoard(), solver))
            {
                unSolvedBoard = SudokuHelper.GetGameBoard(game.GetBoard().CloneMatrix(), diffuculty);
            }

            BoardViewModel model = new BoardViewModel(unSolvedBoard, (int)diffuculty);
            return model;
        }

        public SolverViewModel SolverBoard(IFormFile fileSudoku, bool uniqueControl, int boardSize = 9)
        {
            var matrix = fileSudoku.ReadAsList(boardSize);
            game.Reset(matrix);

            var solvedBoard = solver.Solve();
            if (solvedBoard == null)
                throw new Exception("For this input couldn't find any solution");

            if (uniqueControl && SudokuHelper.UniqueSolution(matrix.CloneMatrix(), solvedBoard, solver))
                throw new Exception("For this input has founded multiple solution");

            SolverViewModel model = new SolverViewModel(matrix, solvedBoard, SudokuHelper.GetLevelOfDifficulty(matrix));
            return model;
        }
    }
}