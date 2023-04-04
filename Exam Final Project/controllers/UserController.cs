using Exam_Final_Project.interfaces;
using Exam_Final_Project.models;

namespace Exam_Final_Project.controllers;

public class UserController
{
    private IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Register(User user)
    {
        try
        {
            _userRepository.InsertOne(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public User Login(string username, string password)
    {
        User user = null;
        try
        {
            var foundUser = _userRepository.FindOneByUsername(username);

            if (!foundUser.Password.Equals(password))
            {
                throw new Exception("Invalid password");
            }

            user = foundUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return user;
    }
}