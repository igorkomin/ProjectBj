using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj
{
    public static class ConsoleView
    {
        public static string Data { get; set; }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static string ReadData()
        {
            return Console.ReadLine();
        }
    }
}
