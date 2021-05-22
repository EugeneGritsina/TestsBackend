using System;
using TestsBackend.Entities;
using TestsBackend.Models;

namespace TestsBackend.Services
{
    public class UserService : IUserService
    {
        UserContext _userContext;
        public UserService(UserContext userContext) => _userContext = userContext;

        public User GetUser(int id) => _userContext.Users.Find(id);
    }
}
