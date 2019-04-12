﻿using SampleApi.Models;
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

        public FoodSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }
        /// <summary>
        /// Gets all the foods that a user has eaten.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public IList<Food> GetAllFoods(int currentUserId)
        {
            List<Food> foods = new List<Food>();
            string sql = "SELECT * FROM food_entries WHERE userId = @currentUserId";
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

        private Food ConvertReaderToFood(SqlDataReader reader)
        {
            Food food = new Food();
            food.FoodId = Convert.ToInt32(reader["id"]);
            food.Name = Convert.ToString(reader["name"]);
            food.Calories = Convert.ToDecimal(reader["calories"]);
            food.Fat = Convert.ToDecimal(reader["fat"]);
            food.Protein = Convert.ToDecimal(reader["protein"]);
            food.Carbohydrates = Convert.ToDecimal(reader["carbs"]);
            food.MealType = Convert.ToString(reader["meal_price"]);
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
                }
            }
            catch(SqlException x)
            {
                throw;
            }
        }
    }
}