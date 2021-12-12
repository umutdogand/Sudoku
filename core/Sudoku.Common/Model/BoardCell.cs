using System.Collections.Generic;

namespace Sudoku.Common
{
    public class BoardCell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }  
        public List<int> UnComplatedValues { get; set; } 
    }
}
