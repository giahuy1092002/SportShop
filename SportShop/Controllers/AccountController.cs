using AutoMapper;
using Data.Model;
using Data.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interface;
using System.Security.Claims;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            ICartService cartService,
            TokenService tokenService,
            IMapper mapper)
        {
            _accountService = accountService;
            _cartService = cartService;
            _tokenService = tokenService;
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
    }
}
