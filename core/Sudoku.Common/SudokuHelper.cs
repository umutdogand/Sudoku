using Sudoku.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SudokuHelper
    {
        /// <summary>
        /// This just works for 9X9 matrix
        /// According to filled cell count this method decides difficulty of board.(Except samurai level)
        /// </summary> 
        public static EnumDifficulty GetLevelOfDifficulty(int[,] board)
        {
            int fillCellCount = GetFilledCellCount(board);

            if (IsDifficultySamurai(board))
                return EnumDifficulty.Samurai;
            else if (fillCellCount <= (int)EnumDifficulty.Hard)
                return EnumDifficulty.Hard;
            else if (fillCellCount > (int)EnumDifficulty.Hard && fillCellCount < (int)EnumDifficulty.Easy)
                return EnumDifficulty.Medium;
            else
                return EnumDifficulty.Easy;

        }

        /// <summary>
        /// If any row, column have more than Sqrt of Board size filled cell than difficulty cannot be samurai
        /// But for example I checking here just row
        /// </summary> 
        public static bool IsDifficultySamurai(int[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                int filledRowCount = 0/*, filledColumnCount = 0*/;
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] != 0)
                        if (filledRowCount++ >= Math.Sqrt(board.GetLength(0))) return false;

                    //if (board[column, row] != 0)
                    //    if (filledColumnCount++ >= 3) return false;
                }
            }
            return true;
        }

        public static int GetFilledCellCount(int[,] board)
        {
            int fillCellCount = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] != 0) fillCellCount++;
                }
            }
            return fillCellCount;
        }

        public static bool UniqueSolution(int[,] board, int[,] solution, ISolver solver)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] == 0)
                    {
                        List<int> possibleValues = Enumerable.Range(1, board.GetLength(0)).ToList();
                        possibleValues.Remove(solution[row, column]);

                        foreach (var value in possibleValues)
                        {
                            board[row, column] = value;

                            solver.Game.Reset(board);
                            if (solver.Solve() != null) return false;
                        }
                    }
                }
            }
            return true;
        }

        public static int[,] GetGameBoard(int[,] solution, EnumDifficulty difficulty)
        {
            int[,] unSolvedBoard = new int[solution.GetLength(0), solution.GetLength(1)];

            int filledCellCount = 0;
            while (filledCellCount <= (int)difficulty)
            {
                var row = new Random().Next(0, solution.GetLength(0));
                var column = new Random().Next(0, solution.GetLength(0));

                if (unSolvedBoard[row, column] == 0)
                {
                    unSolvedBoard[row, column] = solution[row, column];
                    filledCellCount++;
                }
            } 
            return unSolvedBoard;
        }

        public static void PreviousColumnControl(List<BoardCell> UnComplatedLog, IGame game, int row, int column)
        {
            var previousColumnState = UnComplatedLog.FirstOrDefault(x => x.RowNumber == row && x.ColumnNumber == column - 1);

            if (previousColumnState != null)
                previousColumnState.UnComplatedValues.Add(game.GetCellValue(row, column - 1));
            else
                UnComplatedLog.Add(new BoardCell() { RowNumber = row, ColumnNumber = column - 1, UnComplatedValues = new List<int>() { game.GetCellValue(row, column - 1) } });

            UnComplatedLog.RemoveAll(x => x.RowNumber > row || (x.RowNumber == row && x.ColumnNumber > column - 1));
        }

        public static void PreviousRowControl(List<BoardCell> UnComplatedLog, IGame game, int row)
        {
            var previousRowState = UnComplatedLog.FirstOrDefault(x => x.RowNumber == row - 1 && x.ColumnNumber == game.BoardSize - 1);

            if (previousRowState != null)
                previousRowState.UnComplatedValues.Add(game.GetCellValue(row - 1, game.BoardSize - 1));
            else
                UnComplatedLog.Add(new BoardCell() { RowNumber = row - 1, ColumnNumber = game.BoardSize - 1, UnComplatedValues = new List<int>() { game.GetCellValue(row - 1, game.BoardSize - 1) } });

            UnComplatedLog.RemoveAll(x => x.RowNumber > row - 1);
        }
    }
}
