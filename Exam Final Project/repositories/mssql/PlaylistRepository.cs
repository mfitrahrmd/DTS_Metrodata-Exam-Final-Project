using System.Data;
using System.Data.SqlClient;
using Exam_Final_Project.config.database;
using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private SqlServerDb _sqlServerDb;

    public PlaylistRepository(SqlServerDb sqlServerDb)
    {
        _sqlServerDb = sqlServerDb;
    }

    public void InsertOne(Playlist data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "INSERT INTO playlists (name, owner) VALUES (@name, @owner);";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@name",
                    SqlValue = data.Name,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@owner",
                    SqlValue = data.Owner,
                    SqlDbType = SqlDbType.Int
                }
            });

            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to insert playlist");
        }
    }

    public Playlist FindOneById(int id)
    {
        Playlist playlist = null;
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM playlists WHERE id = @id;";
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
                throw new Exception("Playlist not found");
            }
            
            playlist = new Playlist
            {
                Id = dataTable.Rows[0].Field<int>("id"),
                Name = dataTable.Rows[0].Field<string>("name"),
                Owner = dataTable.Rows[0].Field<int>("owner")
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlist;
    }

    public List<Playlist> ListAll()
    {
        List<Playlist> playlists = new List<Playlist>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM playlists;";
                
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Playlists is empty");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                playlists.Add(new Playlist
                {
                    Id = row.Field<int>("id"),
                    Name = row.Field<string>("name"),
                    Owner = row.Field<int>("owner")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlists;
    }

    public List<Playlist> ListByOwner(int owner)
    {
        List<Playlist> playlists = new List<Playlist>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM playlists WHERE owner = @owner;";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@owner",
                    Value = owner,
                    SqlDbType = SqlDbType.Int
                });
                
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Playlists is empty");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                playlists.Add(new Playlist
                {
                    Id = row.Field<int>("id"),
                    Name = row.Field<string>("name"),
                    Owner = row.Field<int>("owner")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlists;
    }

    
    public void UpdateOneById(int id, Playlist data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "UPDATE playlists SET name = @name WHERE id = @id;";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@name",
                    SqlValue = data.Name,
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
            throw new Exception("Failed to update playlist");
        }
    }

    public void DeleteOneById(int id)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "DELETE FROM playlists WHERE id = @id;";
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
            throw new Exception("Failed to delete playlist");
        }
    }
}