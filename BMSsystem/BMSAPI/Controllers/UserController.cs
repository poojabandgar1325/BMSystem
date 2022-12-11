using AutoMapper;
using BMSAPI.Models.Domains;
using BMSAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;


        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ActionName("GetUserAsync")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await userRepository.GetUserAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            var userDTO = mapper.Map<Models.DTO.UserDTO>(user);

            return Ok(userDTO);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            bool response = await userRepository.AddUserAsync(user);

            if (response)
                return Ok("Added Succesfully");

            return BadRequest("User Already Exists");

        }

        [HttpPut]
        //[Route("api/[controller]/user/{userName}")]
        [Route("UpdateUser/{userName}")]
        public async Task<IActionResult> Put([FromRoute] string userName, [FromBody] Models.DTO.UserUpdateDTO userUpdateDTO)
        {
            //convert DTO to Domain model
            var user = new Models.Domains.User()
            {
                Name = userUpdateDTO.Name,
                Password = userUpdateDTO.Password,
                Address = userUpdateDTO.Address,
                State = userUpdateDTO.State,
                Country = userUpdateDTO.Country,
                Email = userUpdateDTO.Email,
                PAN = userUpdateDTO.PAN,
                Contact = userUpdateDTO.Contact,
                DOB = userUpdateDTO.DOB,
                AccountType = userUpdateDTO.AccountType
            };

            //Update User using repository
            bool response = await userRepository.UpdateUserAsync(userName, user);


            //if null then notfound

            if (response)
                return Ok("Updated Successfully ");

            return BadRequest("Something went wrong..!");


        }
    }

}