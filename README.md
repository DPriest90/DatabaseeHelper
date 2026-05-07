This class library is meant to help speed up the development of database transaction heavy application.

Basic usage is:
• Create a new instance of the DatabaseHelper class, providing the connection string to the database you want to work with. I usually have this in an App.Config file for security.
DatabaseHelper _dbHelper = new DatabaseHelper("conString");
  // _dbHelper then contains all CRUD operations you will need for SQL operations.

_dbHelper.Read(string query, List<SqlParameter> parameters = null) - Reads data into a .Net DataTable object
_dbHelper.Create(string query, List<SqlParameter> parameters); - Inserts new record
_dbHelper.Update(string query, List<SqlParameter> parameters); - Update an existing record in the database
_dbHelper.Delete(string query, List<SqlParameter> parameters); - Permanant delete an existing record in the database

There are actually two classes in this library.
• DatabaseHelper - This is the normal version
• DatabaseHelperAsync - This is the more modern asyncronous version which allows you to keep your application responsive when the functions are processing. I highly recommend using this if the machine your application is deployed to allows for it.
