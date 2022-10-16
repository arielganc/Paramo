using System;
using System.Collections.Generic;
using System.IO;
using Sat.Recruitment.Models;
using Sat.Recruitment.Dao.Interfaces;

namespace Sat.Recruitment.Dao
{
    public class UserDao : IUserDao
    {
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        private bool WriteUserInFile(string user)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
                FileStream fileStream = new FileStream(path, FileMode.Open);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(user);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExistUser(User user)
        {
            List<User> _users = new List<User>();
            var reader = ReadUsersFromFile();

            //Normalize email
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            
            user.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userRead = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(userRead);
            }
            reader.Close();

            try
            {
                foreach (var userRead in _users)
                {
                    if (userRead.Email == user.Email || userRead.Phone == user.Phone)
                    {
                        return true;
                    }
                    else if (userRead.Name == user.Name)
                    {
                        if (userRead.Address == user.Address)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return true;
            }
        }

        public IUserResponse InsertUser(User user)
        {
            if (!this.ExistUser(user))
            {
                string userLine = user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.UserType + "," + user.Money;
                WriteUserInFile(userLine);
                return new UserResponse() { Status = true, Message = "User Created" };
            }
            else
            {
               return new UserResponse() { Status = false, Message = "The user is duplicated" };
            }

        }
    }
}
