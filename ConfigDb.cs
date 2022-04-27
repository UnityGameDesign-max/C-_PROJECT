using System.Data.SqlClient;
using DatabaseDetails;

namespace ConnetionSQLSERVER{
    public class DataBaseConfig{
        public string connection
        {
            get
            {
                string ConnectionString = $"server={ConfigDetails.server};database={ConfigDetails.databaseName};trusted_connection=true";
                SqlConnection connetion = new SqlConnection(ConnectionString);
                try
                {
                    connetion.Open();
                    return "Open Connection Successful.....";
                }
                catch (Exception error)
                {
                    return "Error" + error.Message;
                }
            }
        }
    }
}
