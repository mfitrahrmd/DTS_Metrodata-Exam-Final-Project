using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.controllers;

public class PlaylistController
{
    private IPlaylistRepository _playlistRepository;
    private IPlaylistSongRepository _playlistSongRepository;

    public PlaylistController(IPlaylistRepository playlistRepository, IPlaylistSongRepository playlistSongRepository)
    {
        _playlistRepository = playlistRepository;
        _playlistSongRepository = playlistSongRepository;
    }

    public void CreatePlaylist(User userCredential, Playlist playlist)
    {
        try
        {
            _playlistRepository.InsertOne(new Playlist(playlist.Name, userCredential.Id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Playlist> ReadAllPlaylists()
    {
        List<Playlist> playlists = new List<Playlist>();
        try
        {
            playlists =  _playlistRepository.ListAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlists;
    }
    
    public List<Playlist> ReadAllUserPlaylists(User userCredential)
    {
        List<Playlist> playlists = new List<Playlist>();
        try
        {
            playlists =  _playlistRepository.ListByOwner(userCredential.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlists;
    }

    public void AddSongToPlaylist(User userCredential, PlaylistSong playlistSong)
    {
        try
        {
            var foundPlaylist = _playlistRepository.FindOneById(playlistSong.PlaylistId);

            if (!userCredential.Id.Equals(foundPlaylist.Owner)) throw new ControllerException("Access denied", ControllerStatus.Forbidden);

            _playlistSongRepository.InsertOne(playlistSong);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void RemoveSongFromPlaylist(User userCredential, PlaylistSong playlistSong)
    {
        try
        {
            var foundPlaylist = _playlistRepository.FindOneById(playlistSong.PlaylistId);

            if (!userCredential.Id.Equals(foundPlaylist.Owner)) throw new ControllerException("Access denied", ControllerStatus.Forbidden);

            _playlistSongRepository.DeleteOneByPlaylistIdAndSongId(playlistSong.PlaylistId, playlistSong.SongId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public PlaylistSongs ReadAllSongsFromPlaylist(User userCredential, int playlistId)
    {
        PlaylistSongs playlistSongs = null;
        try
        {
            var foundPlaylist = _playlistRepository.FindOneById(playlistId);

            if (!userCredential.Id.Equals(foundPlaylist.Owner)) throw new ControllerException("Access denied", ControllerStatus.Forbidden);

            playlistSongs = _playlistSongRepository.FindOneByIdWithSongs(playlistId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return playlistSongs;
    }

    public void DeletePlaylist(User userCredential, int playlistId)
    {
        try
        {
            var foundPlaylist = _playlistRepository.FindOneById(playlistId);

            if (!userCredential.Id.Equals(foundPlaylist.Owner)) throw new ControllerException("Access denied", ControllerStatus.Forbidden);

            _playlistRepository.DeleteOneById(playlistId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}