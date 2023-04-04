namespace Exam_Final_Project.models;

public class User
{
    public User()
    {
    }

    public User(string username, string password, string fullname)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Fullname = fullname ?? throw new ArgumentNullException(nameof(fullname));
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }

    public override string ToString()
    {
        return $"Id : {Id}\n" +
               $"Username : {Username}\n" +
               $"Fullname : {Fullname}\n";
    }
}