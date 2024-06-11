using AutoMapper;
using Data.Interface;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Service;
using Service.Interface;
using System.Security.Claims;
using System.Text;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        private readonly TokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            ICartService cartService,
            TokenService tokenService,
            IMailService mailService,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountService = accountService;
            _cartService = cartService;
            _tokenService = tokenService;
            _mailService = mailService;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserCartDto>> Login(SignInModel request)
        {
            var user = await _accountService.SignIn(request);
            var nonCart = await _cartService.Retrieve(Request.Cookies["buyerId"]);
            var userCart = await _cartService.Retrieve(request.Email);
            if(nonCart != null)
            {
                if(userCart!=null) await _cartService.Remove(userCart);
                nonCart.BuyerId = request.Email;
                Response.Cookies.Delete("buyerId");
                await _cartService.Update(nonCart);
            }
            return new UserCartDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Cart = nonCart != null ? _mapper.Map<CartDto>(nonCart) : _mapper.Map<CartDto>(userCart),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate.ToShortDateString(),
                UserId = user.Id
            };
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpModel request)
        {
            return Ok(await _accountService.SignUp(request));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserInfo(Guid userId)
        {
            return Ok(await _accountService.GetUserInfo(userId));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _accountService.GetCurrentUser(User.Identity.Name);
            var userCart = await _cartService.Retrieve(user.Email);
            var userDto = new UserCartDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Cart = _mapper.Map<CartDto>(userCart),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate.ToShortDateString(),
                UserId = user.Id
            };
            return Ok(userDto);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return Ok(await _accountService.ChangePassword(model, User.Identity.Name));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var mail = await _accountRepository.ForgetPassword(email);
            var request = new MailRequest();
            request.ToEmail = email;
            request.Subject = "ForgetPassword";
            request.Body = mail.Link;
            var result = await _mailService.SendEmail(request);
            return Ok(result);
        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var decodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);
            model.Token = decodedToken;
            var result = await _accountRepository.ResetPassword(model);
            if (result) return Ok("Lấy mật khẩu thành công");
            return Ok("Thất bại");
         
        }
    }
}
