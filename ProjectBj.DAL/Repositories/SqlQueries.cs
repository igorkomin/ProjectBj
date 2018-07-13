using System;

namespace ProjectBj.DAL.Repositories
{
    public static class SqlQueries
    {
        public static class Players
        {
            public static readonly string insert = "INSERT INTO Players (Name, Balance, IsHuman, InGame) VALUES(@Name, @Balance, @IsHuman, @InGame); SELECT CAST(SCOPE_IDENTITY() as int)";
            public static readonly string delete = "DELETE FROM Players WHERE Id = @id";
            public static readonly string select = "SELECT * FROM Players WHERE Id = @id";
            public static readonly string get = "SELECT * FROM Players WHERE Id = @Id";
            public static readonly string getAll = "SELECT * FROM Players";
            public static readonly string find = "SELECT * FROM Players WHERE Name = @name";
            public static readonly string update = "UPDATE Players SET Name = @Name, IsHuman = @IsHuman, Balance = @Balance, InGame = @Ingame";
            public static readonly string addCard = "INSERT INTO PlayerHands (PlayerId, CardId) VALUES(@PlayerId, @CardId)";
            public static readonly string getCards = "SELECT c.* FROM playerhands ph JOIN Cards c ON ( ph.CardId = c.Id ) JOIN Players p ON ( ph.PlayerId = p.Id ) WHERE ph.PlayerId = @Id";
        }

        public static class Cards
        {
            public static readonly string insert = "INSERT INTO Cards (Suit, Rank, Value) VALUES(@Suit, @Rank, @Value); SELECT CAST(SCOPE_IDENTITY() as int)";
            public static readonly string delete = "DELETE FROM Cards WHERE Id = @id";
            public static readonly string select = "SELECT * FROM Cards WHERE Id = @id";
            public static readonly string getAll = "SELECT * FROM Cards";
            public static readonly string update = "UPDATE Cards SET Suit = @Suit, Rank = @Rank, Value = @Value WHERE Id = @Id";
        }
    }
}
