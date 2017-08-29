using System;
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
            Bus = bus;
            
            gameBoard = new PositionInfo[sizeX, sizeY];
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    gameBoard[i, j] = new PositionInfo(i, j, false);
                }
            }
            PlaceMines(NumberOfMines, SizeX, SizeY, Bus);

            ResetBoard();
        }
        
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        private IServiceBus Bus { get; }
        public GameState State { get; private set; }

        void PlaceMines(int numberOfMines, int sizeX, int sizeY, IServiceBus bus)
        {
            int minesLeft = numberOfMines;

            while (minesLeft > 0)
            {
                int indexX = bus.Next(sizeX);
                int indexY = bus.Next(sizeY);
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

                if (a.X < arrayToBeFilled.Length && a.X > 0 && a.Y < arrayToBeFilled.Length && a.Y > 0)
                {
                    if (!arrayToBeFilled[a.X, a.Y].IsRevealed)
                    {
                        arrayToBeFilled[a.X, a.Y].IsRevealed = true;

                        points.Push(arrayToBeFilled[a.X - 1, a.Y]);
                        points.Push(arrayToBeFilled[a.X, a.Y - 1]);
                        points.Push(arrayToBeFilled[a.X + 1, a.Y]);
                        points.Push(arrayToBeFilled[a.X, a.Y + 1]);
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

            State = GameState.Playing;
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    gameBoard[i, j] = new PositionInfo(i, j, false);
                }
            }

            PlaceMines(NumberOfMines, SizeX, SizeY, Bus);

            
        }

        public void DrawBoard()
        {
        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
        }

        public void MoveCursorDown()
        {
        }

        public void MoveCursorLeft()
        {
        }

        public void MoveCursorRight()
        {
        }

        #endregion

    }
}
