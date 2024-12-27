This class library is meant to help speed up the development of database transaction heavy application by wrapping CRUD operation up into an easy to use DLL wrapper. Simlply compile and add the DLL as a resource to your application.

The reason this exists is it was actually the first program I was told to create at the first proper full time development job I ever had. It has certainly come in handy over the years! I think the idea was "if he can't create a simple library to handle DB transactions, then he won't be mucgh use to us!".

Basic usage is:
• Create a new instance of the DatabaseHelper class, providing the connection string to the database you want to work with. I usually have this in an App.Config file for security.
DatabaseHelper _dbHelper = new DatabaseHelper("conString");
  // _dbHelper then constains all CRUD operations you will need for SQL development.

_dbHelper.Read(string query, List<SqlParameter> parameters = null) - Reads data into a .Net DataTable object
_dbHelper.Create(string query, List<SqlParameter> parameters); - Inserts new record
_dbHelper.Update(string query, List<SqlParameter> parameters); - Update an existing record in the database
_dbHelper.Delete(string query, List<SqlParameter> parameters); - Permanant delete an existing record in the database

There are actually two classes in this library.
• DatabaseHelper - This is the normal version
• DatabaseHelperAsync - This is the more modern asyncronous version which allows you to keep your application responsive when the functions are processing. I highly recommend using this if the machine your application is deployed to allows for it.

That really is all there is to it. I hope someone at least finds it useful. Feel free to use for your own needs!
