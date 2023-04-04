using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.controllers;

public class SongController
{
    private ISongRepository _songRepository;

    public SongController(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public void CreateSong(Song song)
    {
        try
        {
            _songRepository.InsertOne(song);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Song> ReadAllSongs()
    {
        List<Song> songs = new List<Song>();
        try
        {
            songs = _songRepository.ListAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return songs;
    }

    public List<Song> SearchSong(string title)
    {
        List<Song> songs = new List<Song>();
        try
        {
            songs = _songRepository.FindSongsContainingTitle(title);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return songs;
    }
}