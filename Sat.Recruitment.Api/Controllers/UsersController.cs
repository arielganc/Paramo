using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Models;
using Sat.Recruitment.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Dao.Interfaces;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private IUserService Service { set; get; }
        public UsersController(IUserService p_Service)
        {
            Service = p_Service;
        }

        private IUserService UserService
        {
            get
            {
                return this.Service;
            }
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, UserTypeEnum userType, string money)
        {
            Result result = new Result();
           
            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            if(this.UserService != null)
            {
                IUserResponse response = this.UserService.InsertUser(newUser);
                result.IsSuccess = response.Status;
                result.Errors = response.Message;
            }
            else
            {
                result.IsSuccess = false;
                result.Errors = "no service User service";
            }

            return result;


        }

     
    }
}
