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
            _bus = bus;
            PosX = 2;
            PosY = 3;
            gameBoard = new PositionInfo[sizeX, sizeY];
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    gameBoard[i, j] = new PositionInfo(i, j, false);
                }
            }
            PlaceMines(NumberOfMines, SizeX, SizeY, _bus);

            Console.SetCursorPosition(PosX,PosY);
            ResetBoard();
        }
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        private IServiceBus _bus { get; }
        public GameState State { get; private set; }
        public bool isStarting = true;

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

        public void ClickCoordinate()
        {

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

            PlaceMines(NumberOfMines, SizeX, SizeY, _bus);

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
                Console.SetCursorPosition(PosX, PosY - 1);
            }
            DrawBoard();
        }

        public void MoveCursorDown()
        {
            if (PosY != SizeY - 1)
            {
                Console.SetCursorPosition(PosX, PosY + 1);
            }
            DrawBoard();
        }

        public void MoveCursorLeft()
        {
            if (PosX != 0)
            {
                Console.SetCursorPosition(PosX - 1, PosY);
            }
            DrawBoard();
        }

        public void MoveCursorRight()
        {
            if (PosX != SizeX - 1)
            {
                Console.SetCursorPosition(PosX + 1, PosY);
            }
            
            DrawBoard();
        }

        #endregion

    }
}
