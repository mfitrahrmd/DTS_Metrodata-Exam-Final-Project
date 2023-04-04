using Exam_Final_Project.models;

namespace Exam_Final_Project.interfaces;

public interface IAlbumRepository : IBaseRepository<Album>
{
    List<Album> FindAlbumsContainingName(string name);
}