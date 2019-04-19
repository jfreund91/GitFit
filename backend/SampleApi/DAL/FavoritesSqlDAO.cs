using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;
using System.Data.SqlClient;

namespace SampleApi.DAL
{
    public class FavoritesSqlDAO: IFavoritesDAO
    {
        private readonly string connectionString;

        public FavoritesSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        /// <summary>
        /// Adds an entry into the DB for a favorited food.
        /// </summary>
        /// <param name="food"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public Food AddFavorite(Food food, int currentUser)
        {
            string sql = "INSERT INTO favorites VALUES (@userId, @name, @calories, @fat, @protein, @carbs, @ndbno); SELECT @@IDENTITY;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUser);
                    cmd.Parameters.AddWithValue("@name", food.Name);
                    cmd.Parameters.AddWithValue("@calories", food.Calories);
                    cmd.Parameters.AddWithValue("@fat", food.Fat);
                    cmd.Parameters.AddWithValue("@protein", food.Protein);
                    cmd.Parameters.AddWithValue("@carbs", food.Carbs);
                    cmd.Parameters.AddWithValue("@ndbno", food.ndbno);
                    food.EntryId=Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return food;
        }

        public void RemoveFavorite(int favoriteId)
        {
            string sql = "DELETE FROM favorites WHERE id = @entryId;";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@entryId", favoriteId);
                }
            }
            catch (SqlException x)
            {

                throw;
            }
        }

        public IList<Food> GetFavorites(int currentUser)
        {
            IList<Food> favorites = new List<Food>();
            string sql = "SELECT TOP 5 * FROM favorites WHERE userId = @userId ORDER BY id desc;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUser);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Food food = ConvertReaderToFood(reader);
                        favorites.Add(food);
                    }
                }
            }
            catch (SqlException x)
            {

                throw;
            }
            return favorites;
        }

        private Food ConvertReaderToFood(SqlDataReader reader)
        {
            Food food = new Food();
            food.Name = Convert.ToString(reader["name"]);
            food.EntryId = Convert.ToInt32(reader["id"]);
            food.Calories = Convert.ToDecimal(reader["calories"]);
            food.Fat = Convert.ToDecimal(reader["fat"]);
            food.Carbs = Convert.ToDecimal(reader["carbs"]);
            food.Protein = Convert.ToDecimal(reader["protein"]);
            food.ndbno = Convert.ToInt32(reader["ndbno"]);
            return food;
        }
    }
}
