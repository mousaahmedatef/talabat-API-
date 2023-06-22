using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.DAL.Entities.Identity;
using Talabat.DTOs;
using Talabat.Errors;
using Talabat.Extentions;

namespace Talabat.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user , _userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (!CheckEmailExist(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "this email is already in use!" } });

            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = new Address()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    City = registerDto.City,
                    Country = registerDto.Country,
                    Street = registerDto.Street,
                }
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet]  
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user =await _userManager.FindUserWithAddressByEmailAsync(User);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "An Error Occured While Updating User Address" } });

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null ;
        }
    }
}
