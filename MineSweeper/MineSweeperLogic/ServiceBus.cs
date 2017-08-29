using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class ServiceBus : IServiceBus
    {
        readonly Random rnd = new Random();


        public ServiceBus()
        {

        }

        public void Write(string text)
        {
            Console.Write(text);
        }


        public void Write(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Write(string text, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            
        }

        public int Next(int maxValue)
        {
            return rnd.Next(maxValue);
        }
    }
}
