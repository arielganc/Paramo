using Sat.Recruitment.Dao.Interfaces;

namespace Sat.Recruitment.Dao
{
    public class UserResponse : IUserResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
