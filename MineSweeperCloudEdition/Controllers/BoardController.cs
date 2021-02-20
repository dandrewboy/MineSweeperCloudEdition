using Microsoft.AspNetCore.Mvc;
using MIlestone1Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Controllers
{
    public class BoardController : Controller
    {
        // List of buttons called cells wich be set for the number of cells in the game board.
        public Cell[,] cells;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Board(int size, int difficulty)
        {
            cells = new Cell[size, size];
           for(int row = 0; row<size; row++)
            {
                for(int col = 0; col<size; col++)
                {
                    cells[row, col] = new Cell(row,col);
                }
            }
            List<Cell> cellList = new List<Cell>();
           for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellList.Add(cells[i, j]);
                }
            }
            return View("Board", cellList);
        }
    }
}
