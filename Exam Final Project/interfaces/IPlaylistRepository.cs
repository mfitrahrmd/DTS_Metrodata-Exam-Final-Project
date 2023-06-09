using Exam_Final_Project.models;

namespace Exam_Final_Project.interfaces;

public interface IPlaylistRepository : IBaseRepository<Playlist>
{
    List<Playlist> ListByOwner(int owner);
}