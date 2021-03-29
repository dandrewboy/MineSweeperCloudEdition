using BusinessLayer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIlestone1Library;
using MineSweeperCloudEdition.Models;
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
        string endTime;
        int clickCount;
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
            int k = 0;
           for(int row = 0; row<size; row++)
            {
                for(int col = 0; col<size; col++)
                {
                    cells[row, col] = new Cell(row,col);
                    cells[row, col].Id = k;
                    k++;
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
            cb.startTimer();
            return View("Board", cellList);
        }

        //public IActionResult RightClick(int index)
        //{
        //    // Change flagged property of selected cell
        //    cellList.ElementAt(index).flagged = !cellList.ElementAt(index).flagged;
        //    //return to view
        //    return View("Board", cellList);
        //}

        public IActionResult OneButton(int index)
        {
            // Change flagged property of selected cell
            cellList.ElementAt(index).flagged = !cellList.ElementAt(index).flagged;
            //return to view
            return PartialView(cellList.ElementAt(index));
        }

        public IActionResult HandleButtonClick(int index)
        {
            // set the size of the board
            int size = (int)Math.Sqrt(cells.Length);
            // selects the cell out of the cell list
            //Cell cell = cellList.ElementAt(index);
            MouseEventArgs me = new MouseEventArgs();
            if (me.Button.Equals(0))
            {
                clickCount++;
                // Left click
                // check if the cell is a bomb
                if (cellList.ElementAt(index).live != true && cellList.ElementAt(index).liveNeighbors <= 1)
                {
                    // Call the Flood Fill method
                    cb.floodFill(cells, cellList.ElementAt(index).row, cellList.ElementAt(index).col, size);
                }
                else if (cellList.ElementAt(index).liveNeighbors > 1)
                {
                    cellList.ElementAt(index).visited = true;
                }
                else if (cellList.ElementAt(index).live == true)
                {
                    // set visited to true to reveal the bomb
                    cellList.ElementAt(index).visited = true;
                    lose = true;
                    endTime = cb.stopTimer();
                    int UserID = Convert.ToInt32(HttpContext.Session.GetString("_Id"));
                    ResultData resultData = new ResultData();
                    ResultsDTO rDTO = new ResultsDTO(UserID, 1, endTime, clickCount);
                    resultData.addResult(rDTO);
                }
            }
            // checks for win condition
            if(cb.boardCheck(cells, size) == true)
            {
                win = true;
                endTime = cb.stopTimer();
                int UserID = Convert.ToInt32(HttpContext.Session.GetString("_Id"));
                ResultData resultData = new ResultData();
                ResultsDTO rDTO = new ResultsDTO(UserID, 1, endTime, clickCount);
                resultData.addResult(rDTO);
            }
            if(lose == true)
            {
                // reveals the board after a loss
                cb.revealBoard(cells, size);
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
