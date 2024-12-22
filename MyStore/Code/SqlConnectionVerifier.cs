using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MyStore.Code
{
    public class SqlConnectionVerifier
    {
        private string _connectionString;

        public SqlConnectionVerifier(string connectionStringName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName]?.ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Connection string not found.");
            }
        }

        public bool VerifyConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return true; // Connection successful
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                return false;
            }
        }
    }
}