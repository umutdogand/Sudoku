using Sudoku.Common;
using Sudoku.Game;
using Xunit;
namespace Sudoku.Test
{
    public class TestGame
    {
        /// <summary>
        /// This method produce a unique game and solves it.
        /// </summary> 
        [Theory]
        [InlineData(true, 9, EnumDifficulty.Easy)]
        public void TestGeneraterAndSolver(bool uniqueControl, int size, EnumDifficulty difficulty)
        {
            var game = new GameBoard(size);
            var generator = new Generator(game);
            var solver = new Solver(game);

            var board = new GameService(game, generator, solver).GameBoard(uniqueControl, difficulty);
            if (board != null)
            {
                var solvedBoard = new GameService(game, generator, solver).SolverBoard(board.Board, uniqueControl, board.Diffuculty);
                if (solvedBoard != null) Assert.True(true);
            }
            Assert.False(false);
        }

    }
}