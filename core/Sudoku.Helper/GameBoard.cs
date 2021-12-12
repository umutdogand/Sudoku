using Sudoku.Common;

namespace Sudoku.Game
{
    public class GameBoard : IGame
    {
        public int BoardSize { get; set; }

        private int[,] Sudoku;

        public GameBoard(int size = (int)EnumBoardSize.Medium)
        {
            BoardSize = size;
            Sudoku = new int[BoardSize, BoardSize];
        }

        /// <summary>
        /// Get room number of current row and column
        /// </summary> 
        public string GetRoomValue(int row, int column)
        {
            return Math.Floor(row / Math.Sqrt(BoardSize)) + "" + Math.Floor(column / Math.Sqrt(BoardSize));
        }

        /// <summary>
        /// Randomizes a value from left possible values
        /// </summary> 
        public int GetValueInPossibleValues(List<int> possibleValues)
        {
            if (possibleValues.Count == 0) return 0;
            var index = new Random().Next(possibleValues.Count);
            return possibleValues[index];
        }

        /// <summary>
        /// Removes the value from possible values.
        /// </summary> 
        public void RemoveValueIfExists(int row, int column, List<int> possibleValues)
        {
            var cellValue = Sudoku[row, column];
            if (cellValue != 0 && possibleValues.Contains(cellValue))
            {
                possibleValues.Remove(cellValue);
            }
        }
         
        public int GetUniqueValue(int row, int column, List<int> unComplatedValues)
        {
            List<int> possibleValues = Enumerable.Range(1, BoardSize).ToList();
            possibleValues.RemoveAll(x => unComplatedValues.Contains(x));

            var roomValue = GetRoomValue(row, column);
            for (var i = 0; i < BoardSize; i++)
            {
                //Removing unavailable  values in possibleValues according to same row
                RemoveValueIfExists(row, i, possibleValues);

                //Removing unavailable  values in possibleValues according to same column
                RemoveValueIfExists(i, column, possibleValues);

                //Removing unavailable  values in possibleValues according to same room
                for (var j = 0; j < BoardSize; j++)
                {
                    var currentRoom = GetRoomValue(i, j);
                    if (currentRoom.Equals(roomValue))
                    {
                        RemoveValueIfExists(i, j, possibleValues);
                    }
                }
            }
            //Get available value in possibleValues for the current cell.
            return GetValueInPossibleValues(possibleValues);
        }

        public void CheckSize()
        {
            if (BoardSize <= 0) throw new ArgumentException("The Size must be greater than 0.");
            else if (Math.Sqrt(BoardSize) % 1 != 0) throw new ArgumentException("The Size must be square of the any number.");
        }

        public void SetCellValue(int row, int column, int newValue)
        {
            Sudoku[row, column] = newValue;
        }

        public int[,] GetBoard()
        {
            var result = new int[BoardSize, BoardSize];
            Sudoku.CopyMatrix(result);
            return result;
        }

        public int GetCellValue(int row, int column)
        {
            return Sudoku[row, column];
        }

        public void Reset(int size = (int)EnumBoardSize.Medium)
        {
            BoardSize = size;
            Sudoku = new int[BoardSize, BoardSize];
        }

        public void Reset(int[,] matrix)
        {
            BoardSize = matrix.GetLength(0);
            matrix.CopyMatrix(Sudoku);
        }
    }
}
