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
        public string connectionString;

        public FoodSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        /// <summary>
        /// This method provides the ability to add a food item to the db.
        /// </summary>
        /// <param name="currentUserId">Current User's Id</param>
        /// <param name="food">Food to add</param>
        public void AddFoodItem(int currentUserId, Food food)
        {
            string sql = "INSERT INTO food_entries VALUES(@userId, @name, @calories, @fat, @protein, @carbs, @meal_type, @meal_date, @servings, @ndbno)";
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
                    cmd.Parameters.AddWithValue("@carbs", food.Carbohydrates);
                    cmd.Parameters.AddWithValue("@meal_type", food.MealType);
                    cmd.Parameters.AddWithValue("@meal_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@servings", food.Servings);
                    cmd.Parameters.AddWithValue("@ndbno", food.ndbno);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }
        
        public void RemoveFoodItem(int entryId)
        {

        }
    }
}
