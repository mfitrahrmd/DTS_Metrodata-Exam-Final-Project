using System.Data;
using System.Data.SqlClient;
using Exam_Final_Project.config.database;
using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.repositories;

public class UserRepository : IUserRepository
{
    private SqlServerDb _sqlServerDb;

    public UserRepository(SqlServerDb sqlServerDb)
    {
        _sqlServerDb = sqlServerDb;
    }

    public void InsertOne(User data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText =
                "INSERT INTO users (username, password, fullname) VALUES (@username,@password,@fullname);";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@username",
                    SqlValue = data.Username,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@password",
                    SqlValue = data.Password,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@fullname",
                    SqlValue = data.Fullname,
                    SqlDbType = SqlDbType.VarChar
                }
            });

            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to insert user");
        }
    }

    public User FindOneById(int id)
    {
        User user = null;
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {

                command.CommandText = "SELECT * FROM users WHERE id = @id;";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@id",
                    SqlValue = id,
                    SqlDbType = SqlDbType.Int
                });
            
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("User not found");
            }

            user = new User
            {
                Id = dataTable.Rows[0].Field<int>("id"),
                Username = dataTable.Rows[0].Field<string>("username"),
                Password = dataTable.Rows[0].Field<string>("password"),
                Fullname = dataTable.Rows[0].Field<string>("fullname")
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return user;
    }

    public List<User> ListAll()
    {
        List<User> users = new List<User>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM users;";
                
                return command;
            });
            
            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Users is empty");
            }

            foreach (DataRow row in dataTable.Rows)
            {
                users.Add(new User
                {
                    Id = row.Field<int>("id"),
                    Username = row.Field<string>("username"),
                    Password = row.Field<string>("password"),
                    Fullname = row.Field<string>("fullname")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return users;
    }

    public void UpdateOneById(int id, User data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "UPDATE users SET fullname = @fullname WHERE id = @id;";
            command.Parameters.AddRange(new []
            {
                new SqlParameter
                {
                    ParameterName = "@fullname",
                    SqlValue = data.Fullname,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@id",
                    SqlValue = id,
                    SqlDbType = SqlDbType.Int
                }
            });
                
            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to update user");
        }
    }

    public void DeleteOneById(int id)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "DELETE FROM users WHERE id = @id;";
            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                SqlValue = id,
                SqlDbType = SqlDbType.Int
            });
            
            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to delete user");
        }
    }

    public User FindOneByUsername(string username)
    {
        User user = null;
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM users WHERE username = @username;";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@username",
                    SqlValue = username,
                    SqlDbType = SqlDbType.VarChar
                });
                
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("User not found");
            }
            
            user = new User
            {
                Id = dataTable.Rows[0].Field<int>("id"),
                Username = dataTable.Rows[0].Field<string>("username"),
                Password = dataTable.Rows[0].Field<string>("password"),
                Fullname = dataTable.Rows[0].Field<string>("fullname")
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return user;
    }
}