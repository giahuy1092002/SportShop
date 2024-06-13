using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService _userAddressService;

        public UserAddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAddress(Address address,Guid userId)
        {
            return Ok(await _userAddressService.Create(address, userId));
        }
        [HttpPatch("[action]")]
        public async Task UpdateAddress(Address address,int addressId)
        {
            await _userAddressService.Update(address, addressId);
        }
        [HttpDelete("[action]")]
        public async Task DeleteAddress(int addressId)
        {
            await _userAddressService.Delete(addressId);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var addresses = await _userAddressService.GetByUser(userId);
            return Ok(addresses);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeAddress(int addressId, Guid userId)
        {
            return Ok(await _userAddressService.ChangeDefault(addressId, userId));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDefault(Guid userId)
        {
            return Ok(await _userAddressService.GetDefault(userId));
        }
    }
}
