using Data.Entities;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<User> SignIn(SignInModel request);
        Task<User> SignUp(SignUpModel request);
        Task<User> GetCurrentUser(string email);
        Task<UserInfoDto> GetUserInfo(Guid userId);
        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel, string email);
        //Task<bool> ResetPassword(string email, string password);
    }
}
