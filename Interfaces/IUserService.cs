using TestsBackend.Entities;

namespace TestsBackend
{
    public interface IUserService
    {
        public User GetUser(int id);
    }
}