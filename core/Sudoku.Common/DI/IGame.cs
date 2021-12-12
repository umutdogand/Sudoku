using System.Collections.Generic;

namespace Sudoku.Common
{
    public interface IGame
    {
        int BoardSize { get; set; }

        /// <summary>
        /// This method finds a unique  value for current row and column.
        /// </summary> 
        int GetUniqueValue(int row, int column, List<int> unComplatedValues);

        /// <summary>
        /// Check if the input is incorrect format 
        /// </summary> 
        void CheckSize();

        /// <summary>
        /// This method gets filled sudoku.
        /// </summary> 
        int[,] GetBoard();

        /// <summary>
        /// New game is created with param size
        /// </summary>
        void Reset(int size = (int)EnumBoardSize.Medium);

        /// <summary>
        /// New game is created with param matrix
        /// </summary>
        void Reset(int[,] matrix);

        void SetCellValue(int row, int column, int value);

        int GetCellValue(int row, int column);

      
    }
}