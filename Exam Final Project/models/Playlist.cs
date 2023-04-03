namespace Exam_Final_Project.models;

public class Playlist
{
    public Playlist()
    {
    }

    public Playlist(string name, int owner)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Owner = owner;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Owner { get; set; }
    public override string ToString()
    {
        return $"Id : {Id}\n" +
               $"Name : {Name}\n" +
               $"Owner : {Owner}\n";
    }
}