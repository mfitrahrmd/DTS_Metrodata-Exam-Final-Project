using Exam_Final_Project.controllers;
using Exam_Final_Project.models;

namespace Exam_Final_Project.views;

public class CLI
{
    private UserController _userController;
    private AlbumController _albumController;
    private SongController _songController;
    private PlaylistController _playlistController;

    // save currently logged in user information
    public static User _loggedInUserState;

    public CLI(UserController userController, AlbumController albumController, SongController songController,
        PlaylistController playlistController)
    {
        _userController = userController;
        _albumController = albumController;
        _songController = songController;
        _playlistController = playlistController;
    }

    public void Start()
    {
        while (true)
        {
            if (_loggedInUserState is not null)
            {
                MenuHandler(new List<Menu>(new[]
                {
                    new Menu(1, "Explore All Playlists", ReadAllPlaylistsHandler),
                    new Menu(2, "Explore All Albums", ReadAllAlbumsHandler),
                    new Menu(3, "Explore All Songs", ReadAllSongsHandler),
                    new Menu(4, "My Playlists", ReadAllUserPlaylistsHandler),
                    new Menu(5, "Logout", LogoutHandler),
                    new Menu(6, "Exit", () => Environment.Exit(1)),
                }));
            }
            else
            {
                MenuHandler(new List<Menu>(new[]
                {
                    new Menu(1, "Register", RegisterHandler),
                    new Menu(2, "Login", LoginHandler),
                    new Menu(3, "Exit", () => Environment.Exit(1)),
                }));
            }
        }
    }

    public record Menu(int no, string name, Action handler);

    public void MenuHandler(List<Menu> menus)
    {
        if (_loggedInUserState is not null)
        {
            Console.WriteLine($@"
   *** 
  ******     Id : {_loggedInUserState.Id}
   ***       Username : {_loggedInUserState.Username}
 ********    Fullname : {_loggedInUserState.Fullname}
**********
**********
");
        }

        menus.ForEach(menu => { Console.WriteLine($"{menu.no}. {menu.name}"); });

        Console.Write("\nInput Menu : ");
        var inputMenu = Convert.ToInt32(Console.ReadLine());

        try
        {
            Console.Clear();
            menus.First(menu => menu.no.Equals(inputMenu)).handler();
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid menu");
        }
    }

    public void RequiredLogin(Action handler)
    {
        if (_loggedInUserState is null)
        {
            throw new Exception("Login required");
        }

        handler();
    }

    public void RegisterHandler()
    {
        Console.Write("Username : ");
        var username = Console.ReadLine();
        Console.Write("Password : ");
        var password = Console.ReadLine();
        Console.Write("Fullname : ");
        var fullname = Console.ReadLine();
        _userController.Register(new User(username, password, fullname));
        Console.WriteLine("Register successfully!");
    }

    public void LoginHandler()
    {
        Console.Write("Username : ");
        var username = Console.ReadLine();
        Console.Write("Password : ");
        var password = Console.ReadLine();
        User loggedInUser = _userController.Login(username, password);
        _loggedInUserState = loggedInUser;
        Console.WriteLine("Login successfully!");
    }

    public void LogoutHandler()
    {
        _loggedInUserState = null;
        Console.WriteLine("Logout successfully!");
    }

    public void CreateAlbumHandler()
    {
        Console.Write("Name : ");
        var name = Console.ReadLine();
        Console.Write("Year : ");
        var year = Convert.ToInt16(Console.ReadLine());
        _albumController.CreateAlbum(new Album(name, year));
    }

    public void SearchAlbumHandler()
    {
        Console.Write("Name : ");
        var name = Console.ReadLine();

        var albums = _albumController.SearchAlbums(name);
        
        albums.ForEach(Console.WriteLine);
    }

    public void ReadAllAlbumsHandler()
    {
        var albums = _albumController.ReadAllAlbums();
        
        albums.ForEach(Console.WriteLine);
        
        MenuHandler(new List<Menu>(new []
        {
            new Menu(1, "Create Album", CreateAlbumHandler),
            new Menu(2, "Search Album", SearchAlbumHandler),
            new Menu(3, "Back", () => { }),
        }));
    }

    public void CreateSongHandler()
    {
        Console.Write("Title : ");
        var title = Console.ReadLine();
        Console.Write("Genre : ");
        var genre = Console.ReadLine();
        Console.Write("Performer : ");
        var performer = Console.ReadLine();
        Console.Write("Year : ");
        var year = Convert.ToInt16(Console.ReadLine());
        Console.Write("Duration : ");
        var duration = Convert.ToInt16(Console.ReadLine());
        Console.Write("Is Explicit (y/n) : ");
        var readIsExplicit = Console.ReadLine();
        bool isExplicit;
        switch (readIsExplicit)
        {
            case "Y":
            case "y":
                isExplicit = true;
                break;
            case "N":
            case "n":
                isExplicit = false;
                break;
            default:
                isExplicit = false;
                break;
        }

        Console.Write("Album Id : ");
        var albumId = Convert.ToInt32(Console.ReadLine());
        _songController.CreateSong(new Song(title, year, genre, performer, duration, isExplicit, albumId));
    }

    public void SearchSongHandler()
    {
        Console.Write("Title : ");
        var title = Console.ReadLine();

        var songs = _songController.SearchSong(title);
        
        songs.ForEach(Console.WriteLine);
    }

    public void ReadAllSongsHandler()
    {
        var songs = _songController.ReadAllSongs();
        
        songs.ForEach(Console.WriteLine);
        
        MenuHandler(new List<Menu>(new []
        {
            new Menu(1, "Create Song", CreateSongHandler),
            new Menu(2, "Search Song", SearchSongHandler),
            new Menu(3, "Back", () => { }),
        }));
    }

    public void ReadAllPlaylistsHandler()
    {
        var playlists = _playlistController.ReadAllPlaylists();
        
        playlists.ForEach(Console.WriteLine);
    }

    public void ReadAllUserPlaylistsHandler()
    {
        var playlists = _playlistController.ReadAllUserPlaylists(_loggedInUserState);
        
        playlists.ForEach(Console.WriteLine);
        
        MenuHandler(new List<Menu>(new []
        {
            new Menu(1, "Playlist Details", ReadAllSongsFromPlaylist),
            new Menu(2, "Create Playlist", CreatePlaylistHandler),
            new Menu(3, "Delete Playlist", DeletePlaylistHandler),
            new Menu(4, "Back", () => {})
        }));
    }

    public void CreatePlaylistHandler()
    {
        Console.Write("Name : ");
        var name = Console.ReadLine();

        _playlistController.CreatePlaylist(_loggedInUserState, new Playlist(name, _loggedInUserState.Id));
    }

    public void DeletePlaylistHandler()
    {
        Console.Write("Playlist Id : ");
        var id = Convert.ToInt32(Console.ReadLine());
        
        _playlistController.DeletePlaylist(_loggedInUserState, id);
    }

    public void AddSongToPlaylistHandler()
    {
        Console.Write("Playlist Id : ");
        var playlistId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Song Id : ");
        var songId = Convert.ToInt32(Console.ReadLine());

        _playlistController.AddSongToPlaylist(_loggedInUserState, new PlaylistSong(playlistId, songId));
    }
    
    public void RemoveSongFromPlaylistHandler()
    {
        Console.Write("Playlist Id : ");
        var playlistId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Song Id : ");
        var songId = Convert.ToInt32(Console.ReadLine());

        _playlistController.RemoveSongFromPlaylist(_loggedInUserState, new PlaylistSong(playlistId, songId));
    }

    public void ReadAllSongsFromPlaylist()
    {
        Console.Write("Playlist Id : ");
        var playlistId = Convert.ToInt32(Console.ReadLine());

        var playlistSongs = _playlistController.ReadAllSongsFromPlaylist(_loggedInUserState, playlistId);

        Console.WriteLine(playlistSongs);
        
        MenuHandler(new List<Menu>(new []
        {
            new Menu(1, "Add Song To Playlist", AddSongToPlaylistHandler),
            new Menu(2, "Remove Song From Playlist", RemoveSongFromPlaylistHandler),
            new Menu(3, "Back", () => {}),
        }));
    }
}
