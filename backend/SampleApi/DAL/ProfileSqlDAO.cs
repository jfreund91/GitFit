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
        /// <summary>
        /// Creates a user profile and saves it to the database.
        /// </summary>
        /// <param name="profile"></param>
        public Profile CreateProfile(Profile profile)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    // Open the connection
                    conn.Open();
                    int userExists = 0;

                    SqlCommand cmd = new SqlCommand("SELECT * from user_profiles where userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", profile.UserId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userExists = 1;
                        
                    }

                    reader.Close();

                    if (userExists > 0) // Then the user exists and run an update statement
                    {
                        cmd = new SqlCommand("UPDATE user_profiles SET name=@name, currentWeight=@currentWeight, goalWeight = @goalWeight, " +
                            "birthDate = @birthDate, feet = @feet, inches = @inches, activityLevel = @activityLevel, gender = @gender, " +
                            "timeline = @timeline, [image] = @userImage WHERE userId = @userId", conn);
                    }

                    else
                    {
                        cmd = new SqlCommand("INSERT INTO user_profiles VALUES (@userId, @name, " +
                        "@currentWeight, @goalWeight, @birthDate, @feet, @inches, @activityLevel, @gender, @timeline, @userImage); SELECT @@IDENTITY;", conn);
                        
                    }

                    cmd.Parameters.AddWithValue("@userId", profile.UserId);
                    cmd.Parameters.AddWithValue("@name", profile.Name);
                    cmd.Parameters.AddWithValue("@currentWeight", profile.CurrentWeight);
                    cmd.Parameters.AddWithValue("@goalWeight", profile.GoalWeight);
                    cmd.Parameters.AddWithValue("@birthDate", profile.BirthDate);
                    cmd.Parameters.AddWithValue("@feet", profile.Feet);
                    cmd.Parameters.AddWithValue("@inches", profile.Inches);
                    cmd.Parameters.AddWithValue("@activityLevel", profile.ActivityLevel);
                    cmd.Parameters.AddWithValue("@gender", profile.Gender);
                    cmd.Parameters.AddWithValue("@timeline", profile.Timeline);
                    cmd.Parameters.AddWithValue("@userImage", profile.UserImage);

                    cmd.ExecuteScalar();

                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a user's profile from the user's id number.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public Profile GetProfile(int currentUserId)
        {
            Profile profile = new Profile();
            string sql = "SELECT * FROM user_profiles WHERE userId = @userId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        profile.UserId = Convert.ToInt32(reader["userId"]);
                        profile.Name = Convert.ToString(reader["name"]);
                        profile.CurrentWeight = Convert.ToInt32(reader["currentWeight"]);
                        profile.GoalWeight = Convert.ToInt32(reader["goalWeight"]);
                        profile.BirthDate = Convert.ToDateTime(reader["birthDate"]).Date;
                        profile.Feet = Convert.ToInt32(reader["feet"]);
                        profile.Inches = Convert.ToInt32(reader["inches"]);
                        profile.ActivityLevel = Convert.ToString(reader["activityLevel"]);
                        profile.Gender = Convert.ToChar(reader["gender"]);
                        profile.Timeline = Convert.ToString(reader["timeline"]);
                        profile.UserImage = Convert.ToString(reader["image"]);
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return profile;
        }
    }
}
