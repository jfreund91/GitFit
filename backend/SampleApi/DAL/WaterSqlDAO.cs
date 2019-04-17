using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace SampleApi.DAL
{
    public class WaterSqlDAO
    {
        private string connectionString;

        public WaterSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Add an entry for an 8 oz. cup of water.
        /// </summary>
        /// <param name="currentUserId"></param>
        public void AddWaterEntry(int currentUserId)
        {
            string sql = "INSERT INTO water_entries VALUES (@userId, @entryDate);";

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                cmd.Parameters.AddWithValue("@entryDate", DateTime.Today);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets the number of 8oz cups of water a user consumed on a given date.
        /// </summary>
        /// <returns></returns>
        public int GetWaterCountByDate(int currentUserId, DateTime queryDate)
        {
            string sql = "SELECT COUNT(*) FROM water_entries WHERE userId = @userId AND entry_date = @queryDate;";
            int output = 0;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", currentUserId);
                cmd.Parameters.AddWithValue("@queryDate", queryDate);
                output = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return output;
        }

        //public IList<int> GetWaterEntriesInRange(int currentUser, DateTime start, DateTime finish)
        //{
        //    string sql = "SELECT COUNT(*) FROM water_entries WHERE userId = @userId AND entry_date BETWEEN @start AND @finish GROUP BY entry_date;";
        //}
    }
}
