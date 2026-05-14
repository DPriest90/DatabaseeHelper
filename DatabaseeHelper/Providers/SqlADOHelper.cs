using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DatabaseeHelper.Providers
{
    public class SqlADOHelper
    {
        /// <summary>
        ///  Connection string for Database. Readonly so we can avoid any ConString errors (providing the given string is correct, of course)
        /// </summary>
        private readonly string _connectionString;

        // Default instance contructor
        public SqlADOHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Create a new Database connection using the given connection string
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Insert a new record to the database from a SQL "INSERT INTO" command
        public int Create(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }

        // Retrieve data from database where the retrieved data matches the provided query filters
        public DataTable Read(string query, List<SqlParameter>? parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    using (var dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        // Update an already existing database entry with the provided data
        public int Update(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }

        // Delete a record from the database that matches the provided query filter
        public int Delete(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }
    }
}
