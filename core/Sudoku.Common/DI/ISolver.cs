namespace Sudoku.Common
{
    public interface ISolver : IUnComplated
    {
        IGame Game { get; }
        int[,]? Solve();
    }
}
