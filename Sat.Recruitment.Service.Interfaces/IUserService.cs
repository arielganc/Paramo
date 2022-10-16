using System;
using Sat.Recruitment.Models;
using Sat.Recruitment.Dao.Interfaces;

namespace Sat.Recruitment.Service.Interfaces
{
    public interface IUserService
    {
        bool ExistUser(User user);

        IUserResponse InsertUser(User user);

       
    }
}
