using System.Data;
using System.Data.Common;
using API1;
using Microsoft.Data.SqlClient;
namespace API.Data;

public class StateRepositary
{
    private readonly string _connectionString;
    
    public StateRepositary(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }
    
    #region SelectAllState

    public IEnumerable<StateModel> SelectAll()
    {
        var state = new List<StateModel>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "PR_STATE_SELECTALL";
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        
            state.Add(new StateModel()
            {
                StateID = Convert.ToInt32(reader["StateID"]),
                StateName = Convert.ToString(reader["StateName"]),
                StateCode = Convert.ToString(reader["StateCode"]),
                CountryID = Convert.ToInt32(reader["CountryID"])
            });
        return state;
    }

    #endregion

    #region SelectByPK

    public StateModel SelectByPK(int stateID)
    {
        StateModel state = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_STATE_SELECTBYPK", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StateID", stateID);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                state = new StateModel()
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = reader["StateName"].ToString(),
                    StateCode = reader["StateCode"].ToString(),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                };
            }
        }
        return state;
    }

    #endregion
    
    #region StateInsert

    public bool StateInsert(StateModel state)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_STATE_INSERT", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StateName", state.StateName);
            cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
            cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region UpdateState

    public bool StateUpdate(StateModel state)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_STATE_UPDATE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StateID", state.StateID);
            cmd.Parameters.AddWithValue("@StateName", state.StateName);
            cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
            cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
    
            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }
    }

    #endregion

    #region StateDelete

    public bool StateDelete(int stateID)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_STATE_DELETE", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StateID", stateID);

            connection.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            return rowAffected > 0;
        }

    }

    #endregion

    #region StateDropDown

    public IEnumerable<CountryDropDownModel> StateDropDown()
    {
        var state = new List<CountryDropDownModel>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "PR_STATE_DROPDOWN";
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            state.Add(new CountryDropDownModel()
            {
                CountryID = Convert.ToInt32(reader["CountryID"]),
                CountryName = Convert.ToString(reader["CountryName"])
            });
        }
        return state;
    }

    #endregion
}