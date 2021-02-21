using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIlestone1Library
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] theGrid;
        

        public Board(int s)
        {
            Size = s;
            theGrid = new Cell[Size, Size];
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    theGrid[i, j] = new Cell(i, j);
                }
            }
        }

        public void setLiveNeighbors(string difficulty)
        {
            int bombNum;
            List<Cell> bombSet = new List<Cell>();

            var rnd = new Random();

            switch (difficulty)
            {
                case "Easy":
                  bombNum = ((Size * Size) / 10);
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
                case "Medium":
                    bombNum = (((Size * Size) * 2) /10);
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

                case "Hard":
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
        public void calculateLiveNeighbors()
        {
            Cell currentCell;
            for(int r = 0; r < Size; r++)
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
    }
}
