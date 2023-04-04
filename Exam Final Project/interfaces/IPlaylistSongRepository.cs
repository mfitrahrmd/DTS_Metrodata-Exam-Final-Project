using Exam_Final_Project.models;

namespace Exam_Final_Project.interfaces;

public interface IPlaylistSongRepository : IBaseRepository<PlaylistSong>
{
    void DeleteOneByPlaylistIdAndSongId(int playlistId, int songId);
    PlaylistSongs FindOneByIdWithSongs(int playlistId);
}