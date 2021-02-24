using MIlestone1Library;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    // Holds the peramiters for the game board the player selected during the setup process
    public class CreateBoard
    {
        List<Cell> bombSet = new List<Cell>();
        // sets cells to be bombs 
        public void setLiveNeighbors(Cell[,] theGrid, int Size, int difficulty)
        {
            // Total number of bombs needing to be made
            double bombNum;
            // A list of bombs currently made
            List<Cell> bombSet = new List<Cell>();

            // radom number genreator
            var rnd = new Random();

            // Switch case create bombs based on difficulty ranging from one to five, one being the easiest and five being the hardest
            switch (difficulty)
            {
                case 1:
                    // set number of total bombs
                    bombNum = ((Size * Size) / 10);
                    // randomly selects cells in the array to become bombs
                    for (int i = 0; i < bombNum;)
                    {
                        int row = rnd.Next(Size);
                        int col = rnd.Next(Size);
                        Cell c = new Cell(row, col);
                        Boolean newBomb = true;
                        // Checks if the first bomb has been made
                        if (bombSet.Count == 0)
                        {
                            theGrid[c.row, c.col].live = true;
                            bombSet.Add(c);
                            i++;
                        }
                        else
                        {
                            // Ensures that another bomb cell is not seleceted to be made into a bomb
                            for (int j = 0; j < bombSet.Count; j++)
                            {

                                if (bombSet[j].row == c.row && bombSet[j].col == c.col)
                                {
                                    newBomb = false;
                                }
                            }
                            // adds a new bomb to the list
                            if (newBomb == true)
                            {
                                theGrid[c.row, c.col].live = true;
                                bombSet.Add(c);
                                i++;
                            }
                        }

                    }
                    break;
                case 2:
                    bombNum = (((Size * Size) * 1.5) / 10);
                    for (int i = 0; i < bombNum;)
                    {
                        Boolean newBomb = true;
                        Cell c = new Cell(rnd.Next(Size), rnd.Next(Size));
                        if (bombSet.Count == 0)
                        {
                            theGrid[c.row, c.col].live = true;
                            bombSet.Add(c);
                            i++;
                        }
                        else
                        {
                            for (int j = 0; j < bombSet.Count; j++)
                            {

                                if (bombSet[j].row == c.row && bombSet[j].col == c.col)
                                {
                                    newBomb = false;
                                }
                            }
                            if (newBomb == true)
                            {
                                theGrid[c.row, c.col].live = true;
                                bombSet.Add(c);
                                i++;
                            }
                        }

                    }
                    break;
                case 3:
                    bombNum = (((Size * Size) * 2) / 10);
                    for (int i = 0; i < bombNum;)
                    {
                        Boolean newBomb = true;
                        Cell c = new Cell(rnd.Next(Size), rnd.Next(Size));
                        if (bombSet.Count == 0)
                        {
                            theGrid[c.row, c.col].live = true;
                            bombSet.Add(c);
                            i++;
                        }
                        else
                        {
                            for (int j = 0; j < bombSet.Count; j++)
                            {

                                if (bombSet[j].row == c.row && bombSet[j].col == c.col)
                                {
                                    newBomb = false;
                                }
                            }
                            if (newBomb == true)
                            {
                                theGrid[c.row, c.col].live = true;
                                bombSet.Add(c);
                                i++;
                            }
                        }

                    }
                    break;

                case 4:
                    bombNum = (((Size * Size) * 2.5) / 10);
                    for (int i = 0; i < bombNum;)
                    {
                        Boolean newBomb = true;
                        Cell c = new Cell(rnd.Next(Size), rnd.Next(Size));
                        if (bombSet.Count == 0)
                        {
                            theGrid[c.row, c.col].live = true;
                            bombSet.Add(c);
                            i++;
                        }
                        else
                        {
                            for (int j = 0; j < bombSet.Count; j++)
                            {

                                if (bombSet[j].row == c.row && bombSet[j].col == c.col)
                                {
                                    newBomb = false;
                                }
                            }
                            if (newBomb == true)
                            {
                                theGrid[c.row, c.col].live = true;
                                bombSet.Add(c);
                                i++;
                            }
                        }

                    }
                    break;

                case 5:
                    bombNum = (((Size * Size) * 3) / 10);
                    for (int i = 0; i < bombNum;)
                    {
                        Boolean newBomb = true;
                        Cell c = new Cell(rnd.Next(Size), rnd.Next(Size));
                        if (bombSet.Count == 0)
                        {
                            theGrid[c.row, c.col].live = true;
                            bombSet.Add(c);
                            i++;
                        }
                        else
                        {
                            for (int j = 0; j < bombSet.Count; j++)
                            {

                                if (bombSet[j].row == c.row && bombSet[j].col == c.col)
                                {
                                    newBomb = false;
                                }
                            }
                            if (newBomb == true)
                            {
                                theGrid[c.row, c.col].live = true;
                                bombSet.Add(c);
                                i++;
                            }
                        }

                    }
                    break;
            }
        }
        public void calculateLiveNeighbors(Cell[,] theGrid, int Size)
        {
            // Variable for the current cell
            Cell currentCell;
            // Iterates through the rows and columns and counts the surrounding bombs at the current cell and sets the liveNeighbor value to it
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    int bombCount = 0;
                    currentCell = theGrid[r, c];
                    if (theGrid[r, c].live == true)
                    {
                        theGrid[r, c].liveNeighbors = 9;
                    }
                    else
                    {
                        if (currentCell.row + 1 < Size)
                        {
                            if (theGrid[r + 1, c].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.row - 1 >= 0)
                        {
                            if (theGrid[r - 1, c].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.col + 1 < Size)
                        {
                            if (theGrid[r, c + 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.col - 1 >= 0)
                        {
                            if (theGrid[r, c - 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.row + 1 < Size && currentCell.col + 1 < Size)
                        {
                            if (theGrid[r + 1, c + 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.row + 1 < Size && currentCell.col - 1 >= 0)
                        {
                            if (theGrid[r + 1, c - 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.row - 1 >= 0 && currentCell.col + 1 < Size)
                        {
                            if (theGrid[r - 1, c + 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                        if (currentCell.row - 1 >= 0 && currentCell.col - 1 >= 0)
                        {
                            if (theGrid[r + -1, c - 1].live == true)
                            {
                                bombCount++;
                            }
                        }
                    }
                    theGrid[r, c].liveNeighbors = bombCount;
                }
            }
        }

        public void flagBomb(Cell[,] theGrid, int size)
        {
            // Four checks for above, below, left, and right of a bomb
            bool check1 = false;
            bool check2 = false;
            bool check3 = false;
            bool check4 = false;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (theGrid[row, col].live == true)
                    {
                        // Chcecks if the area above is valid
                        if (isValid(row + 1, col, size) == true) 
                        {
                            // checks live and visited status of cell above
                            if(theGrid[row + 1, col].visited == true || theGrid[row + 1, col].live == true)
                            {
                                check1 = true;
                            }
                        } 
                        // chekcs if cell above is not valid
                        else if(isValid(row + 1, col, size) == false)
                        {
                            check1 = true;
                        }
                        if (isValid(row - 1, col, size) == true)
                        {
                            if(theGrid[row - 1, col].visited == true || theGrid[row - 1, col].live == true)
                            {
                                check2 = true;
                            }
                            
                        }
                        else if (isValid(row - 1, col, size) == false)
                        {
                            check2 = true;
                        }
                        if (isValid(row, col + 1, size) == true)
                        {
                            if (theGrid[row, col + 1].visited == true || theGrid[row, col + 1].live == true)
                            {
                                check3 = true;
                            }
                            
                        }
                        else if (isValid(row, col + 1, size) == false)
                        {
                            check3 = true;
                        }
                        if (isValid(row, col - 1, size) == true)
                        {
                            if (theGrid[row, col - 1].visited == true || theGrid[row, col - 1].live == true)
                            {
                                check4 = true;
                            }
                            
                        }
                        else if (isValid(row, col - 1, size) == false)
                        {
                            check4 = true;
                        }
                        // if all checks are set to true flagg the bomb
                        if(check1 == true && check2 == true && check3 == true && check4 ==true)
                        {
                            theGrid[row, col].flagged = true;
                        }
                        // reset checks to be false after confirming flagged status
                        check1 = false;
                        check2 = false;
                        check3 = false;
                        check4 = false;
                    }
                }
            }

        }
        public static bool isValid(int r, int c, int size)
        {
            // Checcks to see if the target cell is inside the bounds of the board
            if (r >= 0 && r < size && c >= 0 && c < size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  void floodFill(Cell[,] cells, int r, int c, int size)
        {
            // Calls the is valid method to check if the target cell is inside the board
            if (isValid(r, c, size) && cells[r, c].live == false && cells[r, c].visited == false && cells[r, c].liveNeighbors <= 1)
            {
                // sets the curretns cells visited property to true
                cells[r, c].visited = true;
                // Calls floodFill again at one up, doen, left, and right of the target cell.
                floodFill(cells, r + 1, c, size);
                floodFill(cells, r - 1, c, size);
                floodFill(cells, r, c + 1, size);
                floodFill(cells, r, c - 1, size);
            }
        }
        public bool boardCheck(Cell[,] cell, int size)
        {
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    // checks to see if any non-bomb cells are not vivited yet
                    if (cell[r, c].live == false && cell[r, c].visited == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void revealBoard(Cell[,] cell, int size)
        {
            // revelas all the cells
            for (int row = 0; row < size; row++) {
                for(int col = 0; col < size; col++)
                {
                    cell[row, col].visited = true;
                    cell[row, col].flagged = false;
                }
            }
        }
    }
}
