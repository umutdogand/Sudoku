using System.Net;

namespace Sudoku.Common
{
    public class AppResponse
    {
        public object? Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }

        public AppResponse(object data, string message, int status = (int)HttpStatusCode.OK)
        {
            Data = data;
            Message = message;
            Status = status;
        }
    }
}