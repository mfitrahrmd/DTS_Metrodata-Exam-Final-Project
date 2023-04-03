namespace Exam_Final_Project.models;

public class Song
{
    public Song()
    {
    }

    public Song(string title, Int16 year, string genre, string performer, Int16 duration, bool isExplicit, int albumId)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Year = year;
        Genre = genre ?? throw new ArgumentNullException(nameof(genre));
        Performer = performer ?? throw new ArgumentNullException(nameof(performer));
        Duration = duration;
        IsExplicit = isExplicit;
        AlbumId = albumId;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public Int16 Year { get; set; }
    public string Genre { get; set; }
    public string Performer { get; set; }
    public Int16 Duration { get; set; }
    public bool IsExplicit { get; set; }
    public int AlbumId { get; set; }

    public override string ToString()
    {
        return $"Id : {Id}\n" +
               $"Title : {Title}\n" +
               $"Year : {Year}\n" +
               $"Genre : {Genre}\n" +
               $"Performer : {Performer}\n" +
               $"Duration : {Duration}\n" +
               $"IsExplicit : {IsExplicit}\n" +
               $"AlbumId : {AlbumId}\n";
    }
}