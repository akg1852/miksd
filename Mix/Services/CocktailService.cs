using Dapper;
using Mix.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Mix.Services
{
    public class CocktailService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MixDB"].ConnectionString;

        private string setupSql = @"
DELETE FROM Cocktail

INSERT INTO Cocktail (Name) VALUES
('Daiquiri'),
('Whisky Sour'),
('Old Fashioned'),
('Martini')
        ";

        public CocktailService()
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute(setupSql);
            }
        }

        public IEnumerable<Cocktail> Cocktails()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query<Cocktail>("SELECT * FROM Cocktail");
            }
        }
    }
}