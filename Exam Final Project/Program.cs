using Exam_Final_Project.config.database;
using Exam_Final_Project.controllers;
using Exam_Final_Project.models;
using Exam_Final_Project.repositories;
using Exam_Final_Project.views;

internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // inject all the required dependencies
            var sqlServerDb = new SqlServerDb("sa", "@DTS2023", "openmusic");
            var userRepository = new UserRepository(sqlServerDb);
            var albumRepository = new AlbumRepository(sqlServerDb);
            var songRepository = new SongRepository(sqlServerDb);
            var playlistRepository = new PlaylistRepository(sqlServerDb);
            var playlistSongRepository = new PlaylistSongRepository(sqlServerDb);
            var userController = new UserController(userRepository);
            var albumController = new AlbumController(albumRepository);
            var songController = new SongController(songRepository);
            var playlistController = new PlaylistController(playlistRepository, playlistSongRepository);
            var cliApp = new CLI(userController, albumController, songController, playlistController);
            cliApp.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}