using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace DatabaseeHelper.Providers
{
    public class SqliteHelper
    {
        /// <summary>
        ///  Connection string for Database. Readonly so we can avoid any ConString errors (providing the given string is correct, of course)
        /// </summary>
        private readonly string _connectionString;

        // Default instance contructor
        public SqliteHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Return a new SqliteConnection object using the connection string provided in the constructor. The connection is not opened,
        /// so the caller is responsible for opening and closing the connection as needed.
        /// </summary>
        /// <returns></returns>
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        /// <summary>
        /// Executes a non-query SQL command using the specified query and parameters.
        /// </summary>
        /// <remarks>Use this method to execute SQL statements that do not return result sets, such as
        /// INSERT, UPDATE, or DELETE commands. The method opens and closes the database connection
        /// automatically.</remarks>
        /// <param name="query">The SQL statement to execute. This should be a valid non-query command such as INSERT, UPDATE, or DELETE.</param>
        /// <param name="parameters">A list of parameters to be applied to the SQL statement. Each parameter in the list is added to the command
        /// before execution. Cannot be null.</param>
        /// <returns>The number of rows affected by the executed command.</returns>
        public int Create(string query, List<SqliteParameter> parameters)
        {
            // Create a new Sqlite connection using the Sqlite connection string provided in the constructor. The connection is disposed of automatically
            // because it is wrapped in a using statement. This ensures that the connection is properly closed and resources are released,#
            // even if an exception occurs.
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }

        /// <summary>
        /// Retrieve data from database where the retrieved data matches the provided query filters
        /// </summary>
        /// <param name="query">Sqlite query to be executed</param>
        /// <param name="parameters">Sqlite query parameters</param>
        /// <returns>A DataTable containing the retrieved data</returns>
        public DataTable Read(string query, List<SqliteParameter>? parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using var reader = command.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader); // Load the data from the reader into the DataTable
                    return table;
                }
            }
        }

        // Update an already existing database entry with the provided data
        public int Update(string query, List<SqliteParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }

        public int Delete(string query, List<SqliteParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return command.ExecuteNonQuery(); // Returns the number of affected rows
                }
            }
        }
    }
}
