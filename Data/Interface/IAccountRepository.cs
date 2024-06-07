using Data.Entities;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> SignIn(SignInModel request);
        Task<User> SignUp(SignUpModel request);
        Task<User> GetUserByName(string username);
        Task<User> GetUser(Guid userId);
        Task<UserInfoDto> GetUserInfo(Guid userId);
        Task<User> GetCurrentUser(string email);
        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel,string email);
    }
}
