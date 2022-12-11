using AutoMapper;
using BMSAPI.Models.Domains;
using BMSAPI.Models.DTO;
using BMSAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        //Getting User Details

        [HttpGet("{username}")]
        public async Task<UserDTO> Get(string username)
        {
            User userDetail = await userRepository.GetUserAsync(username);
            UserDTO userDetailDTO = mapper.Map<UserDTO>(userDetail);

            if (userDetailDTO != null)
                userDetailDTO.Token = null;

            return userDetailDTO;
        }

        //Validate User

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDTO value)
        {
            int role = await userRepository.UserCredentialsAsync(value.UserName, value.Password);

            if (role == 0)
                return Ok("User");
            else if (role == 1)
                return Ok("Admin");
            else
                return NotFound("User Not Found");

        }

    }
}

