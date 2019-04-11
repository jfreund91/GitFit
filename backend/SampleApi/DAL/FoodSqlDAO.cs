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
        /// <param name="food"></param>
        public void AddFoodItem(Food food)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();


                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
