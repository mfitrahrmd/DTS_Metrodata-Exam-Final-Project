using System.Data;
using System.Data.SqlClient;
using Exam_Final_Project.config.database;
using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.repositories;

public class PlaylistSongRepository : IPlaylistSongRepository
{
    private SqlServerDb _sqlServerDb;

    public PlaylistSongRepository(SqlServerDb sqlServerDb)
    {
        _sqlServerDb = sqlServerDb;
    }

    // TODO : implement playlist_song repository
    public void InsertOne(PlaylistSong data)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText = "INSERT INTO playlists_songs (playlist_id, song_id) VALUES (@playlist_id, @song_id);";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@playlist_id",
                    SqlValue = data.PlaylistId,
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter
                {
                    ParameterName = "@song_id",
                    SqlValue = data.SongId,
                    SqlDbType = SqlDbType.Int
                }
            });

            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to insert playlist_song");
        }
    }

    public PlaylistSong FindOneById(int id)
    {
        throw new NotImplementedException();
    }

    public List<PlaylistSong> ListAll()
    {
        throw new NotImplementedException();
    }

    public void UpdateOneById(int id, PlaylistSong data)
    {
        throw new NotImplementedException();
    }

    public void DeleteOneById(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteOneByPlaylistIdAndSongId(int playlistId, int songId)
    {
        var rowAffected = _sqlServerDb.WithTx(command =>
        {
            command.CommandText =
                "DELETE FROM playlists_songs WHERE playlist_id = @playlist_id AND song_id = @song_id;";
            command.Parameters.AddRange(new[]
            {
                new SqlParameter
                {
                    ParameterName = "@playlist_id",
                    SqlValue = playlistId,
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter
                {
                    ParameterName = "@song_id",
                    SqlValue = songId,
                    SqlDbType = SqlDbType.Int
                }
            });

            return command;
        });

        if (rowAffected < 1)
        {
            throw new Exception("Failed to delete playlist_song");
        }
    }

    public PlaylistSongs FindOneByIdWithSongs(int playlistId)
    {
        PlaylistSongs playlistSongs = new PlaylistSongs(new List<Song>());
        try
        {
            DataTable dataTablePlaylist = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText = "SELECT * FROM playlists WHERE id = @id;";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@id",
                    SqlValue = playlistId,
                    SqlDbType = SqlDbType.Int
                });

                return command;
            });

            if (dataTablePlaylist.Rows.Count < 1)
            {
                throw new Exception("Playlist not found");
            }

            playlistSongs.Id = dataTablePlaylist.Rows[0].Field<int>("id");
            playlistSongs.Name = dataTablePlaylist.Rows[0].Field<string>("name");
            playlistSongs.Owner = dataTablePlaylist.Rows[0].Field<int>("owner");

            DataTable dataTable = _sqlServerDb.WithDataTable(command =>
            {
                command.CommandText =
                    "SELECT p.*, s.id sid ,s.* FROM playlists p JOIN playlists_songs ps on p.id = ps.playlist_id JOIN songs s on ps.song_id = s.id WHERE p.id = @playlist_id;";
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@playlist_id",
                    SqlValue = playlistId,
                    SqlDbType = SqlDbType.Int
                });

                return command;
            });

            if (dataTable.Rows.Count < 1)
            {
                throw new Exception("Playlist songs is empty");
            }

            foreach (DataRow row in dataTable.Rows)
            {
                playlistSongs.Songs.Add(new Song
                {
                    Id = row.Field<int>("sid"),
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

        return playlistSongs;
    }
}