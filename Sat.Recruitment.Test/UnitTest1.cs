using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Dao;
using Sat.Recruitment.Dao.Interfaces;
using Sat.Recruitment.Service;
using Sat.Recruitment.Service.Interfaces;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]

    public class UnitTest1
    {
        public UnitTest1()
        {
            //this.ServiceProvider = p_Service;
        }
        private IServiceProvider ServiceProvider { set; get; }
        
        [Fact]
        public void Test1()
        {
            IUserDao p_UserDao = new UserDao();
            IUserService p_Service = new UserService(p_UserDao);
            var userController = new UsersController(p_Service);
            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215",  Models.UserTypeEnum.Normal, "124").Result;
            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            IUserDao p_UserDao = new UserDao();
            IUserService p_Service = new UserService(p_UserDao);
            var userController = new UsersController(p_Service);
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", Models.UserTypeEnum.Normal, "124").Result;
            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
