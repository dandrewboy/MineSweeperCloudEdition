using BusinessLayer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using MIlestone1Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisioForge.Shared.MediaFoundation.OPM;

namespace MineSweeperCloudEdition.Controllers
{
    public class BoardController : Controller
    {
        // List of buttons called cells wich be set for the number of cells in the game board.
        public static Cell[,] cells;
        public static List<Cell> cellList;
        CreateBoard cb = new CreateBoard();
        bool win;
        bool lose;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Board(int size, int difficulty)
        {
            // Creates a 2d array of cells
            cells = new Cell[size, size];
            cellList = new List<Cell>();
           for(int row = 0; row<size; row++)
            {
                for(int col = 0; col<size; col++)
                {
                    cells[row, col] = new Cell(row,col);
                }
            }
           // Sets the bomb cells to active
            cb.setLiveNeighbors(cells, size, difficulty);
            // Calculates how many neighboring cells are bombs
            cb.calculateLiveNeighbors(cells, size);
            // Adds cells to cellList
           for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cellList.Add(cells[i, j]);
                }
            }
            return View("Board", cellList);
        }
        public IActionResult HandleButtonClick(int index)
        {
            // set the size of the board
            int size = (int)Math.Sqrt(cells.Length);
            // selects the cell out of the cell list
            Cell cell = cellList.ElementAt(index);
            MouseEventArgs me = new MouseEventArgs();
            if (me.Button.Equals(0))
            {
                // Left click
                // check if the cell is a bomb
                if (cell.live != true && cell.liveNeighbors <= 1)
                {
                    // Call the Flood Fill method
                    cb.floodFill(cells, cell.row, cell.col, size);
                }
                else if (cell.liveNeighbors > 1)
                {
                    cell.visited = true;
                }
                else if (cell.live == true)
                {
                    // set visited to true to reveal the bomb
                    cell.visited = true;
                    lose = true;
                }
                // Check for any stand alone bombs to be flagged
                cb.flagBomb(cells, size);
                // Create an updated list of cells
                cellList = new List<Cell>();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        cellList.Add(cells[i, j]);
                    }
                }
            }
            // checks for win condition
            if(cb.boardCheck(cells, size) == true)
            {
                win = true;
            }
            if(lose == true)
            {
                // reveals the board after a loss
                cb.revealBoard(cells, size);
                cellList = new List<Cell>();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        cellList.Add(cells[i, j]);
                    }
                }
                // Lose statement
                ViewBag.win = "BOOM! You set off a bomb! You Lose!";
            }
            else if(win == true)
            {
                // win ststement
                ViewBag.win = "All Bomb Locations Identified! You Win!";
            }
            return View("Board", cellList);
        }
    }
}
