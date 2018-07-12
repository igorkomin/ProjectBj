using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProjectBj.Logger
{
    public static class Log
    {
        public static void ToDebug(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
