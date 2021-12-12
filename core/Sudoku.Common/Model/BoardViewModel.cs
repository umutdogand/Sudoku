namespace Sudoku.Common
{
    public class BoardViewModel
    {
        public int[,] Board { get; set; }
        public int Diffuculty { get; set; }

        public BoardViewModel(int[,] board, int diffuculty)
        {
            Board = board;
            Diffuculty = diffuculty;
        }
    }
}