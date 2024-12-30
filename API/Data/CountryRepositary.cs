using System.Data;
using Microsoft.Data.SqlClient;

namespace API.Data;

public class CountryRepositary
{

    private readonly string _connectionString;
    
    public CountryRepositary(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }

    #region SelectAllCountry

    public IEnumerable<CountryModel> SelectAll()
    {
        var country = new List<CountryModel>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "PR_COUNTRY_SELECTALL";
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            country.Add(new CountryModel()
            {
                CountryID = Convert.ToInt32(reader["CountryID"]),
                CountryName = Convert.ToString(reader["CountryName"])
            });
            
        }
        return country;
    }

    #endregion

    #region SelectByPK

    public CountryModel SelectByPK(int countryID)
    {
        CountryModel country = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_COUNTRY_SELECTBYPK", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CountryID", countryID);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                country = new CountryModel
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString(),
                };
            }
        }
        return country;
    }

    #endregion
    
    #region CountryInsert

    public bool CountryInsert(CountryModel country)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_COUNTRY_INSERT", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region UpdateCountry

    public bool CountryUpdate(CountryModel country)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_COUNTRY_UPDATE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CountryID", country.CountryID);
            cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region CountryDelete

    public bool CountryDelete(int countryID)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_COUNTRY_DELETE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CountryID", countryID);

            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }

    }

    #endregion
}