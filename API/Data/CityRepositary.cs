using System.Data;
using API1;
using Microsoft.Data.SqlClient;
namespace API.Data;

public class CityRepositary
{
    
    private readonly string _connectionString;
    
    public CityRepositary(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }
    
    #region SelectAllCity

    public IEnumerable<CityModel> SelectAll()
    {
        var city = new List<CityModel>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "PR_CITY_SELECTALL";
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        
            city.Add(new CityModel()
            {
                CityID=Convert.ToInt32(reader["CityID"]),
                CityName = Convert.ToString(reader["CityName"]),
                PinCode = Convert.ToString(reader["PinCode"]),
                CountryID = Convert.ToInt32(reader["CountryID"]),
                StateID = Convert.ToInt32(reader["StateID"]),
            });
        return city;
    }

    #endregion

    #region SelectByPK

    public CityModel SelectByPK(int cityID)
    {
        CityModel city = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_CITY_SELECTBYPK", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CityID", cityID);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                city = new CityModel()
                {
                    CityID=Convert.ToInt32(reader["CityID"]),
                    CityName = Convert.ToString(reader["CityName"]),
                    PinCode = Convert.ToString(reader["PinCode"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                };
            }
        }
        return city;
    }

    #endregion
    
    #region CityInsert

    public bool CityInsert(CityModel city)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_CITY_INSERT", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@PinCode", city.PinCode);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.AddWithValue("@StateID", city.StateID);

    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region UpdateCity

    public bool CityUpdate(CityModel city)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_CITY_UPDATE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CityID", city.CityID);
            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@PinCode", city.PinCode);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.AddWithValue("@StateID", city.StateID);
    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region CityDelete

    public bool CityDelete(int cityID)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_CITY_DELETE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CityID", cityID);

            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }

    }

    #endregion

    #region CountryDropDown

    public IEnumerable<CountryDropDown> GetCountries()
    {
        var countries = new List<CountryDropDown>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_STATE_DROPDOWN", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                countries.Add(new CountryDropDown()
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString()
                });
            }
         }
         return countries;
    }

    #endregion

    #region GetStatesByCountryID

    public IEnumerable<StateDropDownModel> GetStatesByCountryID(int countryID)
    {
        var states = new List<StateDropDownModel>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_SELECTSTATEFROMCOUNTRY_DROPDOWN", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CountryID", countryID);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                states.Add(new StateDropDownModel()
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = reader["StateName"].ToString()
                });
            }
        }

        return states;
    }

    #endregion
    
    
    
}