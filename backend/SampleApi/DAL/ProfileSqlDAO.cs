using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class ProfileSqlDAO : IProfileDAO
    {
        private string connectionString;

        public ProfileSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public void CreateProfile(Profile profile)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Open the connection
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_profiles VALUES (@name, " +
                        "@currentWeight, @goalWeight, @age, @height, @activityLevel, @gender);", conn);
                    cmd.Parameters.AddWithValue("@name", profile.Name);
                    cmd.Parameters.AddWithValue("@currentWeight", profile.CurrentWeight);
                    cmd.Parameters.AddWithValue("goalWeight", profile.GoalWeight);
                    cmd.Parameters.AddWithValue("@age", profile.Age);
                    cmd.Parameters.AddWithValue("@height", profile.Height);
                    cmd.Parameters.AddWithValue("@activityLevel", profile.ActivityLevel);
                    cmd.Parameters.AddWithValue("@gender", profile.Gender);

                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
