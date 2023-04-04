using System.Data;
using System.Data.SqlClient;
using Exam_Final_Project.config.database;
using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.repositories;

public class SongRepository : ISongRepository
{
    private SqlServerDb _sqlServerDb;

    public SongRepository(SqlServerDb sqlServerDb)
    {
        _sqlServerDb = sqlServerDb;
    }

    public void InsertOne(Song data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText =
                "INSERT INTO songs (title, year, genre, performer, duration, is_explicit, album_id) VALUES (@title, @year, @genre, @performer, @duration, @is_explicit, @album_id);";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@title",
                    SqlValue = data.Title,
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
                    ParameterName = "@genre",
                    SqlValue = data.Genre,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@performer",
                    SqlValue = data.Performer,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@duration",
                    SqlValue = data.Duration,
                    SqlDbType = SqlDbType.SmallInt
                },
                new SqlParameter
                {
                    ParameterName = "@is_explicit",
                    SqlValue = data.IsExplicit,
                    SqlDbType = SqlDbType.Bit
                },
                new SqlParameter
                {
                    ParameterName = "@album_id",
                    SqlValue = data.AlbumId,
                    SqlDbType = SqlDbType.Int
                }
            });

            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to insert song");
        }
    }

    public Song FindOneById(int id)
    {
        Song song = null;
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM songs WHERE id = @id;";
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
                throw new Exception("Song not found");
            }

            song = new Song
            {
                Id = dataTable.Rows[0].Field<int>("id"),
                Title = dataTable.Rows[0].Field<string>("title"),
                Duration = dataTable.Rows[0].Field<Int16>("duration"),
                Genre = dataTable.Rows[0].Field<string>("genre"),
                Performer = dataTable.Rows[0].Field<string>("performer"),
                Year = dataTable.Rows[0].Field<Int16>("year"),
                IsExplicit = dataTable.Rows[0].Field<bool>("is_explicit"),
                AlbumId = dataTable.Rows[0].Field<int>("album_id"),
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return song;
    }

    public List<Song> ListAll()
    {
        List<Song> songs = new List<Song>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM songs;";

                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Song is empty");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                songs.Add(new Song
                {
                    Id = row.Field<int>("id"),
                    Title = row.Field<string>("title"),
                    Genre = row.Field<string>("genre"),
                    Performer = row.Field<string>("performer"),
                    Year = row.Field<Int16>("year"),
                    Duration = row.Field<Int16>("duration"),
                    IsExplicit = row.Field<bool>("is_explicit"),
                    AlbumId = row.Field<int>("album_id")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return songs;
    }
    
    public List<Song> FindSongsContainingTitle(string title)
    {
        List<Song> songs = new List<Song>();
        try
        {
            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM songs WHERE title LIKE '%' + @title +'%';";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@title",
                    SqlValue = title,
                    SqlDbType = SqlDbType.VarChar
                });
            
                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Song not found");
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                songs.Add(new Song
                {
                    Id = row.Field<int>("id"),
                    Title = row.Field<string>("title"),
                    Genre = row.Field<string>("genre"),
                    Performer = row.Field<string>("performer"),
                    Year = row.Field<Int16>("year"),
                    Duration = row.Field<Int16>("duration"),
                    IsExplicit = row.Field<bool>("is_explicit"),
                    AlbumId = row.Field<int>("album_id")
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return songs;
    }


    public void UpdateOneById(int id, Song data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText =
                "UPDATE songs SET title = @title, year = @year, genre = @genre, performer = @performer, duration = @duration, is_explicit = @is_explicit WHERE id = @id;";

            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@title",
                    SqlValue = data.Title,
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
                    ParameterName = "@genre",
                    SqlValue = data.Genre,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@performer",
                    SqlValue = data.Performer,
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter
                {
                    ParameterName = "@duration",
                    SqlValue = data.Duration,
                    SqlDbType = SqlDbType.SmallInt
                },
                new SqlParameter
                {
                    ParameterName = "@is_explicit",
                    SqlValue = data.IsExplicit,
                    SqlDbType = SqlDbType.Bit
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
            throw new Exception("Failed to update song");
        }
    }

    public void DeleteOneById(int id)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "DELETE FROM songs WHERE id = @id;";
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
            throw new Exception("Failed to delete song");
        }
    }
}