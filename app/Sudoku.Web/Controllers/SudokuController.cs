using Microsoft.AspNetCore.Mvc;
using Sudoku.Common;

namespace Sudoku.Web.Controllers
{
    public class SudokuController : Controller
    {
        private readonly IGameService gameService;
        public SudokuController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult Game()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GameBoard(bool uniqueControl, EnumDifficulty diffuculty = EnumDifficulty.Easy, int boardSize = (int)EnumBoardSize.Medium)
        {
            var model =gameService.GameBoard(uniqueControl, diffuculty, boardSize);
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Solver()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SolverBoard(IFormFile fileSudoku,bool uniqueControl, int boardSize = (int)EnumBoardSize.Medium)
        {
            var matrix = fileSudoku.ReadAsList(boardSize);
            var model = gameService.SolverBoard(matrix, uniqueControl, boardSize);   
            return PartialView(model);
        }
    }
}