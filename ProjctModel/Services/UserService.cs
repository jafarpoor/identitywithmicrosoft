using ProjctModel.Context;
using ProjctModel.Entites;
using ProjctModel.Tools;
using ProjctModel.VeiwModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjctModel.Services
{
   public class UserService
    {
        private readonly DataBaseContext context;
        GetHashPassword getHash = new GetHashPassword();

        public UserService(DataBaseContext baseContext)
        {
            context = baseContext;
        }

        public bool RegisterUser(UserDto userDto)
        {
            User user = new User
            {
                FullName = userDto.FullName,
                PasswordHash = getHash.Getsha256Hash(userDto.Password),
                UserName = userDto.UserName ,
              //  Role = userDto.Role ,
                 UId =Guid.NewGuid()  ,
                IsActive =true
            };
            context.Add(user);
            context.SaveChanges();
            return true;
        }

        public UserLogin Login(string UserName , string Password)
        {
            var Result = context.Users.Where(p => p.UserName == UserName
                                             && p.PasswordHash == getHash.Getsha256Hash(Password)).FirstOrDefault();
            if (Result != null)
                return new UserLogin
                {
                    Id = Result.Id,
                    UId = Result.UId,
                    FullName = Result.FullName,
                    Password = Result.PasswordHash,
                   // Role = Result.Role,
                    UserName = Result.UserName
                };

            else
                return null;
        }
    }
}
