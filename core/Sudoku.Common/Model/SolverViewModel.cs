namespace Sudoku.Common
{
    public class SolverViewModel
    {
        public int[,] LoadedBoard { get; set; }
        public int[,] SolvedBoard { get; set; }
        public EnumDifficulty Diffuculty { get; set; }

        public SolverViewModel(int[,] loadedBoard, int[,] solvedBoard, EnumDifficulty diffuculty)
        {
            LoadedBoard = loadedBoard;
            SolvedBoard = solvedBoard;
            Diffuculty = diffuculty;
        }
    }
}