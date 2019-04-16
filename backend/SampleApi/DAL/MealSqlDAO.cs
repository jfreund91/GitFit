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

        //public void DeleteMeal(Meal meal, int currentUser)
        //{
        //    string sql = "DELETE FROM quick_meals WHERE id = @mealId;";
        //    try
        //    {
        //        using(SqlConnection conn = new SqlConnection(this.connectionString))
        //        {
        //            conn.Open();
                    
        //        }
        //    }
        //    catch(SqlException ex)
        //    {
        //        throw;
        //    }
        //}
        
    }
}
