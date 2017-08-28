using System;
using System.Collections.Generic;
using System.Numerics;

namespace Spike_Floodfill
{
    class Program
    {
        public void FloodFill(Vector2[,] arrayToBeFilled, Vector2 startPoint)
        {
            Stack<Vector2> points = new Stack<Vector2>();
            points.Push(arrayToBeFilled[(int)startPoint.X, (int)startPoint.Y]);


            while (points.Count > 0)
            {
                Vector2 a =  points.Pop();

                if (a.X < arrayToBeFilled.Length && a.X > 0 && a.Y < arrayToBeFilled.Length && a.Y > 0)
                {
                    if (arrayToBeFilled[a.X, a.Y].Bool)
                    {
                        arrayToBeFilled[a.X, a.Y].reveal;

                        points.Push(new Vector2(a.X - 1, a.Y));
                        points.Push(new Vector2(a.X, a.Y - 1));
                        points.Push(new Vector2(a.X + 1, a.Y));
                        points.Push(new Vector2(a.X, a.Y + 1));
                    }
                }
            }
        }

        static void Main(string[] args)
        {

        }
    }
}