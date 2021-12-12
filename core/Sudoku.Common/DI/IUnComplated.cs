using System.Collections.Generic;

namespace Sudoku.Common
{
    public interface IUnComplated
    {
        List<BoardCell> UnComplatedLog { get; }
    }
}