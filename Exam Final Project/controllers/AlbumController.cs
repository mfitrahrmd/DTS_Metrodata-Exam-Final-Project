using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.controllers;

public class AlbumController
{
    private IAlbumRepository _albumRepository;

    public AlbumController(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public void CreateAlbum(Album album)
    {
        try
        {
            _albumRepository.InsertOne(album);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Album> ReadAllAlbums()
    {
        List<Album> albums = new List<Album>();
        try
        {
            albums = _albumRepository.ListAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return albums;
    }

    public List<Album> SearchAlbums(string name)
    {
        List<Album> albums = new List<Album>();
        try
        {
            albums = _albumRepository.FindAlbumsContainingName(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return albums;
    }
}