﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class MineSweeperGame
    {
        private PositionInfo[,] gameBoard;

        public MineSweeperGame(int sizeX, int sizeY, int nrOfMines, IServiceBus bus)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = nrOfMines;
            _bus = bus;
            PosX = 0;
            PosY = 0;
            gameBoard = new PositionInfo[sizeX, sizeY];
            ResetBoard();
            Console.SetCursorPosition(PosX,PosY);
            
        }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        private IServiceBus _bus { get; }
        public GameState State { get; private set; }
        public bool isStarting = true;

        void PlaceMines(int numberOfMines, int sizeX, int sizeY)
        {
            int minesLeft = numberOfMines;

            while (minesLeft > 0)
            {
                int indexX = _bus.Next(sizeX);
                int indexY = _bus.Next(sizeY);
                if (!gameBoard[indexX, indexY].HasMine)
                {
                    gameBoard[indexX, indexY].HasMine = true;
                    minesLeft--;
                }
            }
        }

        public PositionInfo GetCoordinate(int x, int y)
        {
            
            if (x >= SizeX)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (y >= SizeY)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (x < 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            if (y < 0)
            {
                throw new System.IndexOutOfRangeException();
            }
            return gameBoard[x, y];
        }

        public void FlagCoordinate()
        {
        }

        public void FloodReveal(PositionInfo[,] arrayToBeFilled, int startPointX, int startPointY)
        {
            Stack<PositionInfo> points = new Stack<PositionInfo>();
            points.Push(arrayToBeFilled[startPointX, startPointY]);


            while (points.Count > 0)
            {
                PositionInfo a = points.Pop();

                if (a.X < arrayToBeFilled.Length && a.X >= 0 && a.Y < arrayToBeFilled.Length && a.Y >= 0)
                {
                    if (arrayToBeFilled[a.X, a.Y].NrOfNeighbours == 0 && arrayToBeFilled[a.X, a.Y].IsOpen == false)
                    {
                        arrayToBeFilled[a.X, a.Y].IsOpen = true;

                        if(a.X > 0)
                            points.Push(arrayToBeFilled[a.X - 1, a.Y]);
                        if (a.Y > 0)
                            points.Push(arrayToBeFilled[a.X, a.Y - 1]);
                        if (a.X < SizeX-1)
                            points.Push(arrayToBeFilled[a.X + 1, a.Y]);
                        if(a.Y < SizeY-1)
                            points.Push(arrayToBeFilled[a.X, a.Y + 1]);
                    }

                    else
                    {
                        arrayToBeFilled[a.X, a.Y].IsOpen = true;
                    }
                }
            }
        }

        public void ClickCoordinate()
        {
            FloodReveal(gameBoard, PosX, PosY);
        }

        public void ResetBoard()
        {

            
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    gameBoard[i, j] = new PositionInfo(i, j, false);
                }
            }

            PlaceMines(NumberOfMines, SizeX, SizeY);

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (i <= 0 && j <= 0)
                    {
                        if (gameBoard[i + 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i + 1, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;

                        if (i <= 0 && j > 0)
                        {
                            if (gameBoard[i, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                            if (gameBoard[i + 1, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        }

                        if (i > 0 && j <= 0)
                        {
                            if (gameBoard[i - 1, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                            if (gameBoard[i - 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        }
                    }
                    else if (i == SizeX && j == SizeY)
                    {
                        if (gameBoard[i, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i - 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i - 1, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;

                        if (i == SizeX && j < SizeY)
                        {
                            if (gameBoard[i, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                            if (gameBoard[i - 1, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        }

                        if (i < SizeX && j == SizeY)
                        {
                            if (gameBoard[i + 1, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                            if (gameBoard[i + 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;

                        }
                    }

                    else if (i < SizeX-1 && i > 0 && j < SizeY-1 && j > 0)
                    {
                        if (gameBoard[i + 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i + 1, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i - 1, j + 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i - 1, j].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i - 1, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                        if (gameBoard[i + 1, j - 1].HasMine) gameBoard[i, j].NrOfNeighbours++;
                    }

                }
            }

        }

        public void DrawBoard()
        {
            isStarting = true;
            while (isStarting)
            {
                for (int i = 0; i < SizeY; i++)
                {
                    for (int k = 0; k < SizeX; k++)
                    {
                        if (gameBoard[k, i].IsOpen)
                        {
                            _bus.Write("* ");
                        }

                        if (i == PosY && k == PosX)
                        {
                            _bus.Write("? ", ConsoleColor.DarkCyan); 
                        }
                        else
                        {
                            _bus.Write("? ");
                        }
                        
                    }
                    _bus.WriteLine();
                }
                isStarting = false;
            }
            
            

        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
            if (PosY != 0)
            {
                PosY -= 1;
                Console.SetCursorPosition(PosX, PosY);
            }
            DrawBoard();
        }

        public void MoveCursorDown()
        {
            if (PosY != SizeY - 1)
            {
                PosY += 1;
                Console.SetCursorPosition(PosX, PosY);
            }
            DrawBoard();
        }

        public void MoveCursorLeft()
        {
            if (PosX != 0)
            {
                PosX -= 1;
                Console.SetCursorPosition(PosX, PosY);
            }
            DrawBoard();
        }

        public void MoveCursorRight()
        {
            if (PosX != SizeX - 1)
            {
                PosX += 1;
                Console.SetCursorPosition(PosX, PosY);
            }
            
            DrawBoard();
        }

        #endregion

    }
}
