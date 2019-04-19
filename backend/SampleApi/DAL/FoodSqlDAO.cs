using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class FoodSqlDAO : IFoodDAO
    {
        private readonly string connectionString;

        public FoodSqlDAO (string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        /// <summary>
        /// Gets all the foods that a user has eaten.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public IList<Food> GetLifetimeFoodEntries(int currentUserId)
        {
            List<Food> foods = new List<Food>();
            string sql = "SELECT * FROM food_entries WHERE userId = @currentUserId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Food food = null;
                    while(reader.Read())
                    {
                        food = ConvertReaderToFood(reader);
                        foods.Add(food);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return foods;
        }

        /// <summary>
        /// Returns a list of foods between a date range. 
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public IList<Food> GetFoodEntriesInRange(int currentUserId, DateTime start, DateTime finish)
        {
            IList<Food> results = new List<Food>();
            string sql = "SELECT * FROM food_entries WHERE userId = @userId AND meal_date BETWEEN @start AND @finish;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.AddWithValue("@userId", currentUserId);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@finish", finish);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Food food = null;
                    while (reader.Read())
                    {
                        food = ConvertReaderToFood(reader);
                        results.Add(food);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return results;
        }


        private Food ConvertReaderToFood(SqlDataReader reader)
        {
            Food food = new Food();
            food.EntryId = Convert.ToInt32(reader["id"]);
            food.Name = Convert.ToString(reader["name"]);
            food.Calories = Convert.ToDecimal(reader["calories"]);
            food.Fat = Convert.ToDecimal(reader["fat"]);
            food.Protein = Convert.ToDecimal(reader["protein"]);
            food.Carbs = Convert.ToDecimal(reader["carbohydrates"]);
            food.MealType = Convert.ToString(reader["meal_type"]);
            food.Servings = Convert.ToDecimal(reader["servings"]);
            food.Date = Convert.ToDateTime(reader["meal_date"]);
            food.ndbno = Convert.ToInt32(reader["ndbno"]);

            return food;

        }

        /// <summary>
        /// This method provides the ability to add a food item to the db.
        /// </summary>
        /// <param name="currentUserId">Current User's Id</param>
        /// <param name="food">Food to add</param>
        public Food AddFoodItem(int currentUserId, Food food)
        {
            string sql = "INSERT INTO food_entries VALUES(@userId, @name, @calories, @fat, @protein, @carbs, @meal_type, @meal_date, @servings, @ndbno); SELECT @@IDENTITY;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId",currentUserId);
                    cmd.Parameters.AddWithValue("@name", food.Name);
                    cmd.Parameters.AddWithValue("@calories", food.Calories);
                    cmd.Parameters.AddWithValue("@fat", food.Fat);
                    cmd.Parameters.AddWithValue("@protein", food.Protein);
                    cmd.Parameters.AddWithValue("@carbs", food.Carbs);
                    cmd.Parameters.AddWithValue("@meal_type", food.MealType);
                    cmd.Parameters.AddWithValue("@meal_date", DateTime.Today);
                    cmd.Parameters.AddWithValue("@servings", food.Servings);
                    cmd.Parameters.AddWithValue("@ndbno", food.ndbno);

                   
                    food.EntryId = Convert.ToInt32(cmd.ExecuteScalar());

                    return food;
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }
        /// <summary>
        /// Removes an entry from the food_entries table.
        /// </summary>
        /// <param name="entryId"></param>
        public void RemoveFoodItem(int entryId)
        {
            string sql = "DELETE FROM food_entries WHERE id = @entryId";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@entryId", entryId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException x)
            {
                throw;
            }
        }

        /// <summary>
        /// Edit a perviously entered food entry.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public void EditEntry(Food food, int userId)
        {
            string sql = @"UPDATE food_entries
                           SET servings = @newServings, meal_Type = @newMealType
                           WHERE name = @entryId AND userId = @userId AND meal_date = @date;";

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@newServings", food.Servings);
                    cmd.Parameters.AddWithValue("@newMealType", food.MealType);
                    cmd.Parameters.AddWithValue("@entryId", food.Name);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@date", food.Date);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }

        }
        public List<decimal> GetTimelineCaloriesByDay(int currentUser, int timeline)
        {
            List<decimal> results = new List<decimal>();
            DateTime query = DateTime.Today.AddDays(-timeline);
            for (int i = timeline; i > 0; i--)
            {
               
                string sql = "SELECT SUM(food_entries.calories) FROM food_entries WHERE meal_date = @meal_date AND userId = @userId;";
                try
                {
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userId", currentUser);
                        cmd.Parameters.AddWithValue("meal_date", query.ToString("yyyy-MM-dd"));
                        Object o = cmd.ExecuteScalar();
                        decimal totalCal = 0;
                        if (o is DBNull)
                        {

                        }
                        else
                        {
                            totalCal = (decimal)o;
                        }
                         
                        results.Add(totalCal);
                        query = query.AddDays(1);
                    }
                }
                catch (SqlException ex)
                {

                    throw;
                }

            }
            return results;
        }
    }
}
