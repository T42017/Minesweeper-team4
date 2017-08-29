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
            ResetBoard();
            gameBoard = new PositionInfo[sizeX, sizeY];
            _bus = bus;
        }
        IServiceBus _bus;
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        

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
            
        }

        public void DrawBoard()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int k = 0; k < SizeX; k++)
                {
                    _bus.Write("? ");
                }
                _bus.WriteLine();
            }
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
