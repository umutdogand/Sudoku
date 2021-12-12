using Sudoku.Common;

namespace Sudoku.Game
{
    public class Solver : ISolver
    {
        public List<BoardCell> UnComplatedLog { get; } = new List<BoardCell>();

        public IGame Game { get; }

        private int[,] LoadedSudoku { get; set; }

        public Solver(IGame game)
        {
            Game = game;
            LoadedSudoku = new int[game.BoardSize, game.BoardSize];
            game.GetBoard().CopyMatrix(LoadedSudoku);
        }

        /// <summary>
        /// This method solves LoadedSudoku
        /// </summary> 
        public int[,]? Solve()
        {
            Game.CheckSize();
            Reset();

            for (int row = 0; row < Game.BoardSize; row++)
            {
                for (int column = 0; column < Game.BoardSize; column++)
                {
                    if (LoadedSudoku[row, column] != 0) continue;

                    var currentCellState = UnComplatedLog.FirstOrDefault(x => x.RowNumber == row && x.ColumnNumber == column);

                    List<int> unComplatedValues = currentCellState != null ? currentCellState.UnComplatedValues : new List<int>();

                    int newValue = Game.GetUniqueValue(row, column, unComplatedValues);

                    if (newValue == 0)
                    {
                        if (!PreviousCellControl(ref row, ref column)) return null;
                    }
                    else
                    {
                        Game.SetCellValue(row, column, newValue);
                    }
                }
            }
            return Game.GetBoard();
        }

        /// <summary>
        /// If algorithm can't find any value for current row and column than the code goes to previous cell and produce new value.
        /// </summary> 
        private bool PreviousCellControl(ref int row, ref int column)
        {
            if (row == 0 && column == 0) return false;
            return  column == 0 ?  FirstColumnControl(ref row, ref column) : ColumnControl(ref row, ref column);
        }

        /// <summary>
        ///  If the algorithm goes back to find new value for previous cell, values of ongoing cells should set to empty.
        /// </summary> 
        private void SetEmptyToOngoingCells(int row, int column)
        {
            for (int t = row; t < Game.BoardSize; t++)
            {
                for (int z = 0; z < Game.BoardSize; z++)
                {
                    if (((t == row && z == column - 1) || t > row) && Game.GetCellValue(t, z) != 0 && LoadedSudoku[t, z] == 0)
                    {
                        Game.SetCellValue(t, z, 0);
                    }
                }
            }
        }

        /// <summary>
        /// When the code goes to back for new value, if current cell is first cell than algortim goes to previous row last column
        /// </summary> 
        private bool FirstColumnControl(ref int row, ref int column)
        {
            if (LoadedSudoku[row - 1, Game.BoardSize - 1] != 0)
            {
                row = row - 1;
                column = Game.BoardSize - 1;

                return PreviousCellControl(ref row, ref column);
            }
            else
            {
                SudokuHelper.PreviousRowControl(UnComplatedLog, Game, row);
                row = row - 1;
                column = Game.BoardSize - 2;
                Game.SetCellValue(row, Game.BoardSize - 1, 0);
            }
            return true;
        }

        private bool ColumnControl(ref int row, ref int column)
        {
            if (LoadedSudoku[row, column - 1] != 0)
            {
                column = column - 1;
                return PreviousCellControl(ref row, ref column);
            }
            else
            {
                SudokuHelper.PreviousColumnControl(UnComplatedLog, Game, row, column);

                SetEmptyToOngoingCells(row, column);

                column = column - 2;
            }
            return true;
        }
      
        private void Reset()
        {
            UnComplatedLog.Clear();

            LoadedSudoku = new int[Game.BoardSize, Game.BoardSize];
            Game.GetBoard().CopyMatrix(LoadedSudoku);
        }
    }
}