using Data;
using Data.Entities;
using Data.Interface;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel, string email)
        {
            return await _unitOfWork.Accounts.ChangePassword(changePasswordModel, email); 
        }

        public async Task<User> GetCurrentUser(string email)
        {
            return await _unitOfWork.Accounts.GetCurrentUser(email);
        }

        public async Task<UserInfoDto> GetUserInfo(Guid userId)
        {
            return await _unitOfWork.Accounts.GetUserInfo(userId);
        }

        public async Task<User> SignIn(SignInModel request)
        {
            return await _unitOfWork.Accounts.SignIn(request);
        }

        public async Task<User> SignUp(SignUpModel request)
        {
            return await _unitOfWork.Accounts.SignUp(request);
        }
        //public async Task<bool> ResetPassword(string email, string password)
        //{
        //    return await _accountRepository.ResetPassword(email, password);
        //}
    }
}
