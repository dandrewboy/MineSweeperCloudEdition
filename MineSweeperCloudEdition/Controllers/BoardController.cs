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
        static CreateBoard cb = new CreateBoard();
        string endTime;
        static int clickCount;
        bool win;
        bool lose;
        static int UserID;

        public IActionResult Index()
        {
            //checks if user session is set or not, and if not, we determine its an unauthorized user.
            var User = HttpContext.Session.GetInt32("_Id");
            if (User.HasValue)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Player");
        }
        public IActionResult Board(int size, int difficulty, string btnVal)
        {
            //Login Validation to see if user is logged in and setting the session id
            var User = HttpContext.Session.GetInt32("_Id");
            if (User.HasValue)
            {
                UserID = (int)User;
            }
            else
            {
                return RedirectToAction("Index", "Player");
            }

            //from button index, get btnval.
            if (btnVal.Equals("Load Game"))
            {
                GameData gd = new GameData();
                if (gd.CheckSave(UserID, "") == true) //if user has a game stored in the database that we should resume.
                {
                    //Code to load in game.
                    List<GameDTO> game = gd.LoadGame(UserID);
                    int boardSize = (int)Math.Sqrt(game.Count);
                    cells = new Cell[boardSize, boardSize];
                    cellList = new List<Cell>();
                    bool gameover = false;
                    // Theres a bug with game sql inserting not in order before this so we use linq to fix it :)
                    List<GameDTO> fixIdOrder = new List<GameDTO>();
                    int counter = 0;
                    while (counter < game.Count)
                    {
                        //while we re-add board count starting from 0 (going in order)
                        foreach (GameDTO gdto in game)
                        { //find id of counter so we can add that gamedto and cell data in order.
                            if(gdto.Id == counter)
                            {
                                fixIdOrder.Add(gdto);
                                counter++;
                            }
                        }
                    }
                    //need to set cells otherwise a nullable object error comes up.
                    for (int row = 0; row < boardSize; row++)
                    {
                        for (int col = 0; col < boardSize; col++)
                        {
                            foreach (GameDTO gdto in game)
                            {
                                if (gdto.Id == row+col)
                                {
                                    Cell imp = new Cell(gdto.row, gdto.col);
                                    imp.Id = gdto.Id;
                                    imp.visited = gdto.visited;
                                    imp.live = gdto.live;
                                    imp.flagged = gdto.flagged;
                                    imp.liveNeighbors = gdto.liveNeighbors;
                                    cells[row, col] = imp;
                                }
                            }
                        }
                    }
                    //take out the cell of our gamedto and add it to cell list.
                    foreach (GameDTO gdto in fixIdOrder)
                    {
                        Cell imp = new Cell(gdto.row, gdto.col);
                        imp.Id = gdto.Id;
                        imp.visited = gdto.visited;
                        imp.live = gdto.live;
                        imp.flagged = gdto.flagged;
                        imp.liveNeighbors = gdto.liveNeighbors;
                        cellList.Add(imp);
                        cb.addElapsedTime(TimeSpan.Parse(gdto.Time));
                        clickCount = gdto.Clicks;
                        //a quick calculation if game is already over. may change logic later.
                        if (imp.visited == true && imp.live)
                        {
                            gameover = true;
                        }
                    }
                    if (gameover == true)
                    {
                        cb.revealBoard(cells, size);
                        ViewBag.win = "You loaded a game that is completed.";
                    }
                }
                else
                {
                    //if there is no game saved, we create a new game. 
                    btnVal = "New Game";
                }
            }
            if (btnVal.Equals("New Game"))
            {
                // Creates a 2d array of cells
                cells = new Cell[size, size];
                cellList = new List<Cell>();
                int k = 0;
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        cells[row, col] = new Cell(row, col);
                        cells[row, col].Id = k;
                        k++;
                    }
                }
                // Sets the bomb cells to active
                cb.setLiveNeighbors(cells, size, difficulty);
                // Calculates how many neighboring cells are bombs
                cb.calculateLiveNeighbors(cells, size);
                // Adds cells to cellList
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        cellList.Add(cells[i, j]);
                    }
                }
                clickCount = 0;
                cb.resetTimer();
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

        /// <summary>
        /// takes all the variables needed to resume the game, puts them into a dto and uploads to database.
        /// </summary>
        /// <returns>view</returns>
        public IActionResult SaveGame()
        {
            GameData gd = new GameData();
            List<GameDTO> game = new List<GameDTO>();
            foreach (Cell cell in cellList)
            {
                GameDTO gdto = new GameDTO();
                gdto.Id = cell.Id;
                gdto.row = cell.row;
                gdto.col = cell.col;
                gdto.visited = cell.visited;
                gdto.live = cell.live;
                gdto.flagged = cell.flagged;
                gdto.liveNeighbors = cell.liveNeighbors;
                gdto.PlayerId = UserID;
                gdto.Time = cb.stopTimer();
                gdto.Clicks = clickCount;
                game.Add(gdto);
            }
            gd.SaveGame(game, UserID);
            return RedirectToAction("Index","Board");
        }

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
            var User = HttpContext.Session.GetInt32("_Id");
            UserID = (int)User;
            //if (cellList.ElementAt(index).visited == false)
            //{
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
                        ResultData resultData = new ResultData();
                        ResultsDTO rDTO = new ResultsDTO(UserID, 0, endTime, clickCount);
                        resultData.addResult(rDTO);
                    }
                }
                // checks for win condition
                if (cb.boardCheck(cells, size) == true)
                {
                    win = true;
                    endTime = cb.stopTimer();
                    ResultData resultData = new ResultData();
                    ResultsDTO rDTO = new ResultsDTO(UserID, 1, endTime, clickCount);
                    resultData.addResult(rDTO);
                }
                if (lose == true)
                {
                    // reveals the board after a loss
                    cb.revealBoard(cells, size);
                    // Lose statement
                    ViewBag.win = "BOOM! You set off a bomb! You Lose!";
                }
                else if (win == true)
                {
                    cb.revealBoard(cells, size);
                    // win statement
                    ViewBag.win = "All Bomb Locations Identified! You Win!";
                }
            //}
            return View("Board", cellList);
        }
    }
}