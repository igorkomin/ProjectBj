using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Configuration
{
    public static class DbConfig
    {
        public static readonly string Server = "localhost";
        public static readonly string Database = "blackjack";
        public static readonly string ConnectionString = $"Server={Server};Database={Database};Trusted_Connection=True;";
    }
}
