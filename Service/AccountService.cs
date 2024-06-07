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
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository )
        {
           _accountRepository = accountRepository;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel, string email)
        {
            return await _accountRepository.ChangePassword(changePasswordModel, email); 
        }

        public async Task<User> GetCurrentUser(string email)
        {
            return await _accountRepository.GetCurrentUser(email);
        }

        public async Task<UserInfoDto> GetUserInfo(Guid userId)
        {
            return await _accountRepository.GetUserInfo(userId);
        }

        public async Task<User> SignIn(SignInModel request)
        {
            return await _accountRepository.SignIn(request);
        }

        public async Task<User> SignUp(SignUpModel request)
        {
            return await _accountRepository.SignUp(request);
        }
    }
}
