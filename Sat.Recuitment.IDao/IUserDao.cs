using Sat.Recruitment.Models;

namespace Sat.Recruitment.Dao.Interfaces
{
    public interface IUserDao
    {
        bool ExistUser(User user);
        IUserResponse InsertUser(User user);
        
    }
}
