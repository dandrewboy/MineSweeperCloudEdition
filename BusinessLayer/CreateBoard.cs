using MIlestone1Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    // Holds the peramiters for the game board the player selected during the setup process
    public class CreateBoard
    {
        List<Cell> bombSet = new List<Cell>();
        public void setLiveNeighbors(Cell[,] theGrid, int Size, int difficulty)
        {
            double bombNum;
            List<Cell> bombSet = new List<Cell>();

            var rnd = new Random();

            switch (difficulty)
            {
                case 1:
                    bombNum = ((Size * Size) / 10);
                    for (int i = 0; i < bombNum;)
                    {
                        int row = rnd.Next(Size);
                        int col = rnd.Next(Size);
                        Cell c = new Cell(row, col);
                        Boolean newBomb = true;
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
    }
}
