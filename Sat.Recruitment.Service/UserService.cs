using Sat.Recruitment.Service.Interfaces;
using Sat.Recruitment.Dao.Interfaces;
using Sat.Recruitment.Dao;
using Sat.Recruitment.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Service
{
    public class UserService : IUserService
    {

        private IUserDao UserDao { set; get; }
        public UserService(IUserDao p_UserDao)
        {
            UserDao = p_UserDao;
        }

        //Validate errors
        private void ValidateErrors(User user ,ref string errors)
        {
         
            if (user.Name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (user.Email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (user.Address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (user.Phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
            var mailValid = new EmailAddressAttribute().IsValid(user.Email);
            if(!mailValid)
            { 
                errors = errors + " The email is invalid";
            }
        }

        public IUserResponse InsertUser(User user)
        {
            var errors = "";

            ValidateErrors(user,ref errors);

            if (errors != null && errors != "")
            {
                return new UserResponse()
                {
                    Status = false,
                    Message = errors
                };
            }
            switch (user.UserType)
            {
                case UserTypeEnum.Normal:
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            var gif = user.Money * percentage;
                            user.Money = user.Money + gif;
                        }
                        if (user.Money < 100)
                        {
                            if (user.Money > 10)
                            {
                                var percentage = Convert.ToDecimal(0.8);
                                var gif = user.Money * percentage;
                                user.Money = user.Money + gif;
                            }
                        }
                    }
                    break;
                case UserTypeEnum.SuperUser:
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = user.Money * percentage;
                            user.Money = user.Money + gif;
                        }
                    }
                    break;
                case UserTypeEnum.Premium:
                    {
                        if (user.Money > 100)
                        {
                            var gif = user.Money * 2;
                            user.Money = user.Money + gif;
                        }
                    }
                    break;
            }
            IUserResponse result =  UserDao.InsertUser(user);
            return result;
        }

        public bool ExistUser(User user)
        {
            return UserDao.ExistUser(user);
        }
    }
}
