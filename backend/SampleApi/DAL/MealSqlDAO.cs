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

        public Food AddFoodToMeal(Food food, Meal meal, int currentUser)
        {
            string sql = "INSERT INTO quick_meal_food_items VALUES(@mealId, @foodName, @calories, @fat, @protein, @carbs, @servings);";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mealId", meal.Id);
                    cmd.Parameters.AddWithValue("@foodName", food.Name);
                    cmd.Parameters.AddWithValue("@calories", food.Calories);
                    cmd.Parameters.AddWithValue("@fat", food.Fat);
                    cmd.Parameters.AddWithValue("@protein", food.Protein);
                    cmd.Parameters.AddWithValue("@carbs", food.Carbs);
                    cmd.Parameters.AddWithValue("@servings", food.Servings);
                    food.EntryId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return food;
        }

        /// <summary>
        /// Removes a food from a quick-meal.
        /// </summary>
        /// <param name="food"></param>
        /// <param name="meal"></param>
        public void RemoveFoodFromMeal(Food food)
        {
            string sql = "DELETE FROM quick_meal_food_items WHERE id = @entryId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@entryId", food.EntryId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }


       public IList<Food> GetMeal(int mealId)
        {
            IList<Food> output = new List<Food>();
            string sql = "SELECT * FROM quick_meal_food_items WHERE meal_id = @mealId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mealId", mealId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(ConvertReaderToFood(reader));
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }

        public IList<Meal> GetAllMeals(int mealId)
        {
            IList<Meal> output = new List<Meal>();
            string sql = "SELECT * FROM quick_meals WHERE userId = @userId;";
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", mealId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        output.Add(ConvertReaderToMeal(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        private Meal ConvertReaderToMeal(SqlDataReader reader)
        {
            Meal meal = new Meal();
            meal.Id = Convert.ToInt32(reader["userId"]);
            meal.Name = Convert.ToString(reader["name"]);

            return meal;
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
    }
}
