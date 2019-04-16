using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApi.Models;
using System.Data.SqlClient;

namespace SampleApi.DAL
{
    public class MealSqlDAO: IMealDAO
    {
        private readonly string connectionString;

        public MealSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        /// <summary>
        /// Creates a meal and associates it with a user's id.
        /// </summary>
        /// <param name="meal"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public Meal CreateMeal(Meal meal, int currentUser)
        {
            string sql = "INSERT INTO quick_meals VALUES(@name, @userId); SELECT @@IDENTITY";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", meal.Name);
                    cmd.Parameters.AddWithValue("@userId", currentUser);
                    meal.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return meal;
        }

        public void DeleteMeal(Meal meal, int currentUser)
        {
            string sql2 = "DELETE FROM quick_meals WHERE id = @mealId;";
            string sql = "DELETE FROM quick_meal_food_items WHERE meal_id = @mealId;";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mealId", meal.Id);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@mealId", meal.Id);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        //public Food AddFoodToMeal(Food food, Meal meal, int currentUser)
        //{
        //    string sql = "INSERT INTO quick_meal_food_items VALUES(@mealId, @foodName, @calories, @fat, @protein, @carbs, @servings);";
        //    try
        //    {
        //        using(SqlCon)
        //    }
        //    catch(SqlException ex)
        //    {
        //        throw;
        //    }
        //    return food;
        //}
    }
}
