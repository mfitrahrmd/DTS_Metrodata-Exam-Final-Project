namespace Exam_Final_Project.models;

public class Album
{
    public Album()
    {
    }

    public Album(string name, Int16 year)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Year = year;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Int16 Year { get; set; }

    public override string ToString()
    {
        return $"Id : {Id}\n" +
               $"Name : {Name}\n" +
               $"Year : {Year}\n";
    }
}