namespace Exam_Final_Project.models;

public class PlaylistSong
{
    public PlaylistSong()
    {
    }

    public PlaylistSong(int playlistId, int songId)
    {
        PlaylistId = playlistId;
        SongId = songId;
    }

    public int Id { get; set; }
    public int PlaylistId { get; set; }
    public int SongId { get; set; }

    public override string ToString()
    {
        return $"Id : {Id}\n" +
               $"PlaylistId : {PlaylistId}\n" +
               $"SongId : {SongId}\n";
    }
}