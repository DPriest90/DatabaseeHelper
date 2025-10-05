using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;


namespace DatabaserHelper
{
    /// <summary>
    /// A class to speed up development of Database transaction heavy applications.This is the NON-ASYNC version.
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        ///  Connection string for Database. Readonly so we can avoid any ConString errors (providing the given string is correct, of course)
        /// </summary>
        private readonly string _connectionString;

        // Default instance contructor
        public DatabaseHelper(string connectionString)
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
                    return command.ExecuteNonQuery();  // Returns the number of affected rows
                }
            }            
        }

        // Retrieve data from database where the retrieved data matches the provided query filters
        public DataTable Read(string query, List<SqlParameter> ?parameters)
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
                    return command.ExecuteNonQuery();  // Returns the number of affected rows
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
                    return command.ExecuteNonQuery();  // Returns the number of affected rows
                }
            }
        }
    }

    /// <summary>
    /// A class to speed up development of Database transaction heavy applications. This class uses Asyncronous methods.
    /// </summary>
    public class DatabaseHelperAsync
    {
        /// <summary>
        ///  Connectionstring for Database. Readonly so we can avoid any ConString errors (providing the given string is correct, of course)
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DatabaseHelperAsync(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Create a new Database connection using the given connection string
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Insert a new record to the database from a SQL "INSERT INTO" command
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return await command.ExecuteNonQueryAsync();  // Returns the number of affected rows
                }
            }
        }

        /// <summary>
        /// Retrieve data from database where the retrieved data matches the provided query filters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DataTable> ReadAsync(string query, List<SqlParameter> parameters = null)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    using (var dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => dataAdapter.Fill(dataTable));  // Execute the data adapter in a non-blocking manner
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Update an already existing database entry with the provided data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return await command.ExecuteNonQueryAsync();  // Returns the number of affected rows
                }
            }
        }

        /// <summary>
        /// Delete a record from the database that matches the provided query filter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string query, List<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return await command.ExecuteNonQueryAsync();  // Returns the number of affected rows
                }
            }
        }
    }
}
