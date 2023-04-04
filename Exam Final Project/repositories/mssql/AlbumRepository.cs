using System.Data;
using System.Data.SqlClient;
using Exam_Final_Project.config.database;
using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.repositories;

public class AlbumRepository : IAlbumRepository
{
    private SqlServerDb _sqlServerDb;
    
    public AlbumRepository(SqlServerDb sqlServerDb)
    {
        _sqlServerDb = sqlServerDb;
    }
    
    public void InsertOne(Album data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "INSERT INTO albums (name, year) VALUES (@name, @year);";
            command.Parameters.AddRange(new []
            {
                new SqlParameter
                {
                    ParameterName = "@name",
                    SqlValue = data.Name,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@year",
                    SqlValue = data.Year,
                    SqlDbType = SqlDbType.SmallInt
                }
            });
            
            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to insert album");
        }
    }

    public Album FindOneById(int id)
    {
        Album album = null;
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM albums WHERE id = @id;";
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
                throw new Exception("Album not found");
            }

            album = new Album
            {
                Id = dataTable.Rows[0].Field<int>("id"),
                Name = dataTable.Rows[0].Field<string>("name"),
                Year = dataTable.Rows[0].Field<Int16>("year"),
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return album;
    }

    public List<Album> FindAlbumsContainingName(string name)
    {
        List<Album> albums = new List<Album>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM albums WHERE name LIKE '%' + @name +'%';";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@name",
                    SqlValue = name,
                    SqlDbType = SqlDbType.VarChar
                });
            
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Album not found");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                albums.Add(new Album
                {
                    Id = row.Field<int>("id"),
                    Name = row.Field<string>("name"),
                    Year = row.Field<Int16>("year")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return albums;
    }

    public List<Album> ListAll()
    {
        List<Album> albums = new List<Album>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM albums;";
                
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Albums is empty");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                albums.Add(new Album
                {
                    Id = row.Field<int>("id"),
                    Name = row.Field<string>("name"),
                    Year = row.Field<Int16>("year")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return albums;
    }

    public void UpdateOneById(int id, Album data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "UPDATE albums SET name = @name, year = @year WHERE id = @id;";
            command.Parameters.AddRange(new []
            {
                new SqlParameter
                {
                    ParameterName = "@name",
                    SqlValue = data.Name,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@year",
                    SqlValue = data.Year,
                    SqlDbType = SqlDbType.SmallInt
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
            throw new Exception("Failed to update album");
        }
    }

    public void DeleteOneById(int id)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "DELETE FROM albums WHERE id = @id;";
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
            throw new Exception("Failed to delete album");
        }
    }
}