using Exam_Final_Project.models;

namespace Exam_Final_Project.interfaces;

public interface ISongRepository : IBaseRepository<Song>
{
    List<Song> FindSongsContainingTitle(string title);
}