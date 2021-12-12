namespace Sudoku.Common
{
    public static class ArrayHelper
    {
        public static void CopyMatrix(this int[,] soruce, int[,] destination)
        {
            FillInto(soruce, destination);
        }

        public static int[,] CloneMatrix(this int[,] soruce)
        {
            int[,] destination = new int[soruce.GetLength(0), soruce.GetLength(1)];
            
            FillInto(soruce, destination);

            return destination;
        }

        private static void FillInto(int[,] soruce, int[,] destination)
        {
            for (int row = 0; row < soruce.GetLength(0); row++)
            {
                for (int column = 0; column < soruce.GetLength(1); column++)
                {
                    destination[row, column] = soruce[row, column];
                }
            }
        }
    }
}
