using Microsoft.AspNetCore.Http;

namespace Sudoku.Common
{
    public interface IGameService
    {
        /// <summary>
        /// This methos generated a game
        /// </summary>
        BoardViewModel GameBoard(bool uniqueControl, EnumDifficulty diffuculty = EnumDifficulty.Easy, int boardSize = (int)EnumBoardSize.Medium);

        /// <summary>
        ///  This method solves the matrix 
        /// </summary>
        SolverViewModel SolverBoard(int[,] matrix, bool uniqueControl, int boardSize = (int)EnumBoardSize.Medium);
    }
}