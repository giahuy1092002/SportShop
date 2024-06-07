using Common;
using Common.Exceptions;
using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AccountRepository : Repository<User>,IAccountRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            SportStoreContext context,
            IUnitOfWork uow,
            UserManager<User> userManager,
            IConfiguration configuration
            ):base(context)
        {
            _uow = uow;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel,string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            if (result.Succeeded)
            {
                return await Task.FromResult(true);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordMismatch")
                    {
                        throw new PasswordException("Current password is incorrect.");
                    }
                }
                
            }
            return false;
        }

        public async Task<User> GetCurrentUser(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await Entities
                .Include(u=>u.AddressBook)
                .FirstOrDefaultAsync(u => u.Id == userId);

        }

        public async Task<User> GetUserByName(string username)
        {
            return await Entities
                .Include(u=>u.AddressBook)
                .FirstOrDefaultAsync(u=>u.UserName==username);
        }

        public async Task<UserInfoDto> GetUserInfo(Guid userId)
        {
            var user = await Entities.FirstOrDefaultAsync(u => u.Id == userId);
            return new UserInfoDto
            {
                Email = user.Email,
                FirstName = user.FirstName, 
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate.ToShortDateString()
            };
        }

        public async Task<User> SignIn(SignInModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user==null || !await _userManager.CheckPasswordAsync(user,request.Password))
             {
                throw new KeyNotFoundException(ExceptionMessage.WrongPasswordOrEmail);
            }
            return user;
        }
        public async Task<User> SignUp(SignUpModel request)
        {
            var userTest = await _userManager.FindByEmailAsync(request.Email);
            if (userTest != null)
            {
                throw new DuplicateException("Email exist");
            }
            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = DateTime.ParseExact(request.BirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, "Customer");
            return user;
        }
        
    }
}
