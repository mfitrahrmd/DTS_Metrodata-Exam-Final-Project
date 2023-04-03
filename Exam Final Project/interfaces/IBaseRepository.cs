namespace Exam_Final_Project.interfaces;

public interface IBaseRepository<T>
{
    void InsertOne(T data);
    T FindOneById(int id);
    List<T> ListAll();
    void UpdateOneById(int id, T data);
    void DeleteOneById(int id);
}