namespace Exam_Final_Project.models;

public class PlaylistSongs : Playlist
{
    public List<Song> Songs { get; set; }

    public PlaylistSongs()
    {
    }

    public PlaylistSongs(List<Song> songs)
    {
        Songs = songs;
    }

    public PlaylistSongs(string name, int owner, List<Song> songs) : base(name, owner)
    {
        Songs = songs;
    }

    public override string ToString()
    {
        var str = $"Id : {Id}\n" +
                  $"Name : {Name}\n" +
                  $"Owner : {Owner}\n" +
                  $"Songs : \n";
        Songs.ForEach(song =>
        {
            str += $"####################\n" +
                   song +
                   $"####################\n\n";
        });

        return str;
    }
}