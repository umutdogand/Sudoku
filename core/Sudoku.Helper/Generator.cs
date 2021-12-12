using Sudoku.Common;

namespace Sudoku.Game
{
    public class Generator : IGenerator
    {
        public List<BoardCell> UnComplatedLog { get; } = new List<BoardCell>();

        public IGame Game { get; }

        public Generator(IGame game) => Game = game;

        /// <summary>
        /// Genarates a board randomly according to given size.
        /// </summary> 
        public int[,] Generate()
        {
            UnComplatedLog.Clear();
            Game.CheckSize();

            for (int row = 0; row < Game.BoardSize; row++)
            {
                for (int column = 0; column < Game.BoardSize; column++)
                {
                    var currentCellState = UnComplatedLog.FirstOrDefault(x => x.RowNumber == row && x.ColumnNumber == column);

                    List<int> unComplatedValues = currentCellState != null ? currentCellState.UnComplatedValues : new List<int>();
                    int newValue = Game.GetUniqueValue(row, column, unComplatedValues);

                    if (newValue == 0)
                    {
                        PreviousCellControl(ref row, ref column);
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
        ///  If algorithm can't find any value for current row and column than the code goes to previous cell and produce new value.
        /// </summary> 
        private void PreviousCellControl(ref int row, ref int column)
        {
            if (column == 0)
            {
                SudokuHelper.PreviousRowControl(UnComplatedLog, Game, row);

                row = row - 1;
                column = Game.BoardSize - 2;
                Game.SetCellValue(row, Game.BoardSize - 1, 0);
            }
            else
            {
                SudokuHelper.PreviousColumnControl(UnComplatedLog, Game, row, column);

                SetEmptyToOngoingCells(row, column);

                column = column - 2;
            }
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
                    if (((t == row && z == column - 1) || t > row) && Game.GetCellValue(t,z)!= 0)
                    {
                        Game.SetCellValue(t, z, 0);
                    }
                }
            }
        }
    }
}