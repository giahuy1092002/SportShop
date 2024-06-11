using Common;
using Common.Exceptions;
using Data.DataContext;
using Data.Entities;
using Data.Interface;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Buffers.Text;
using System.Globalization;
using System.Text;
using System.Web;

namespace Data.Repositories
{
    public class AccountRepository : Repository<User>,IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IUrlHelper _urlHelper;
        public AccountRepository(SportStoreContext context,UserManager<User> userManager, IUrlHelper urlHelper) :base(context)
        {
            _userManager = userManager;
            _urlHelper = urlHelper;
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

        public async Task<LinkMailModel> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user!=null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                Console.WriteLine(token);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var link = _urlHelper.Action("ResetPassword", new { encodedToken });
                var result = new LinkMailModel
                {
                    Email = email,
                    Link = link
                };
                return await Task.FromResult(result);

            }
            throw new KeyNotFoundException("Email is not exist");
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

        public async Task<bool> ResetPassword(ResetPassword request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }

                }
            }
            return false;

        }
    }
}
