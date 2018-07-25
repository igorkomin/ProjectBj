namespace ProjectBj.DAL
{
    internal class DatabaseConfiguration
    {
        public static readonly string Server = "localhost";
        public static readonly string Database = "blackjack";
        public static readonly string ConnectionString = $"Server={Server};Database={Database};Trusted_Connection=True;";
    }
}
