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
        throw new NotImplementedException();
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
}