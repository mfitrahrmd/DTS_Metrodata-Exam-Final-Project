using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Exam_Final_Project.config.database;

public class SqlServerDb
{
    private readonly SqlConnectionStringBuilder _connectionStringBuilder;
    private readonly string _dbName;
    private readonly string _dbPassword;
    private readonly string _dbUser;

    private readonly SqlConnection _connection;

    public SqlServerDb(string dbUser, string dbPassword, string dbName)
    {
        
        _dbUser = dbUser;
        _dbPassword = dbPassword;
        _dbName = dbName;


        _connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "localhost,1433",
            UserID = _dbUser,
            Password = _dbPassword,
            InitialCatalog = _dbName,
            ConnectTimeout = 30
        };

        try
        {
            _connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            _connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public SqlConnection CreateConnection()
    {
        return _connection;
    }

    public int WithTx(Func<SqlCommand, SqlCommand> command)
    {
        SqlConnection sqlConnection = CreateConnection();
        
        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

        SqlCommand sqlCommand = new();

        int rowAffected = 0;
        try
        {
            SqlCommand cbCommand = command(sqlCommand);
            cbCommand.Connection = _connection;
            cbCommand.Transaction = sqlTransaction;

            rowAffected = cbCommand.ExecuteNonQuery();

            sqlTransaction.Commit();

            return rowAffected;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            try
            {
                sqlTransaction.Rollback();
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
            }
        }

        return rowAffected;
    }

    public DataTable WithDataTable(Func<SqlCommand, SqlCommand> command)
    {
        SqlConnection sqlConnection = CreateConnection();
        
        DataTable dataTable = new();
        SqlCommand sqlCommand = new();

        SqlCommand cbCommand = command(sqlCommand);
        cbCommand.Connection = sqlConnection;

        SqlDataAdapter sqlDataAdapter = new(cbCommand);

        sqlDataAdapter.Fill(dataTable);

        return dataTable;
    }
}