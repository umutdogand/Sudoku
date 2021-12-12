namespace Sudoku.Common
{
    public interface IGenerator : IUnComplated
    {
        IGame Game { get; }
        int[,] Generate();
    }
}