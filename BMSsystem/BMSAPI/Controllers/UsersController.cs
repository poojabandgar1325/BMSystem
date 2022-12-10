using AutoMapper;
using BMSAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Controllers
{
    
        [ApiController]
        [Route("Users")]
        public class UsersController : Controller
        {
            private readonly IUserRepository userRepository;
            private readonly IMapper mapper;

            public UsersController(IUserRepository userRepository, IMapper mapper)
            {
                this.userRepository = userRepository;
                this.mapper = mapper;
            }
            [HttpGet]
            public async Task<IActionResult> GeAllUsersAsync()
            {
                var users = await userRepository.GetAllAsync();

                //var usersDTO = new List<Models.DTO.User>();
                //users.ToList().ForEach(user =>
                //{
                //var userDTO = new Models.DTO.User()
                //{

                //};
                //usersDTO.Add(userDTO);

                // });

                var usersDTO = mapper.Map<List<Models.DTO.User>>(users);

                return Ok(usersDTO);

            }

            [HttpGet]
            [Route("{accountNo:int}")]
            [ActionName("GetUserAsync")]
            public async Task<IActionResult> GetUserAsync(int accountNo)
            {
                var user = await userRepository.GetAsync(accountNo);

                if (user == null)
                {
                    return NotFound();
                }
                var userDTO = mapper.Map<Models.DTO.User>(user);

                return Ok(userDTO);

            }

            [HttpPost]
            public async Task<IActionResult> AddUserAsync(Models.DTO.AddUserRequest addUserRequest)
            {
                var user = new Models.Domain.User()
                {
                    Name = addUserRequest.Name,
                    UserName = addUserRequest.UserName,
                    Password = addUserRequest.Password,
                    Address = addUserRequest.Address,
                    Country = addUserRequest.Country,
                    State = addUserRequest.State,
                    EmailID = addUserRequest.EmailID,
                    PAN = addUserRequest.PAN,
                    ContactNo = addUserRequest.ContactNo,
                    DOB = addUserRequest.DOB,
                    AccountType = addUserRequest.AccountType
                };
                user = await userRepository.AddAsync(user);

                var userDTO = new Models.DTO.User
                {
                    AccountNo = user.AccountNo,
                    Name = user.Name,
                    UserName = user.UserName,
                    Password = user.Password,
                    Address = user.Address,
                    Country = user.Country,
                    State = user.State,
                    EmailID = user.EmailID,
                    PAN = user.PAN,
                    ContactNo = user.ContactNo,
                    DOB = user.DOB,
                    AccountType = user.AccountType
                };

                return CreatedAtAction(nameof(GetUserAsync), new { accountNo = userDTO.AccountNo }, userDTO);
                // return Ok(userDTO);
            }

            [HttpPut]
            [Route("{accountNo:int}")]
            public async Task<IActionResult> UpdateUserAsync([FromRoute] int accountNo, [FromBody] Models.DTO.UpdateUserRequest updateUserRequest)

            {
                var user = new Models.Domain.User()
                {
                    Name = updateUserRequest.Name,
                    UserName = updateUserRequest.UserName,
                    Password = updateUserRequest.Password,
                    Address = updateUserRequest.Address,
                    Country = updateUserRequest.Country,
                    State = updateUserRequest.State,
                    EmailID = updateUserRequest.EmailID,
                    PAN = updateUserRequest.PAN,
                    ContactNo = updateUserRequest.ContactNo,
                    DOB = updateUserRequest.DOB,
                    AccountType = updateUserRequest.AccountType
                };

                user = await userRepository.UpdateAsync(accountNo, user);

                if (user == null)
                {
                    return NotFound();
                }

                var userDTO = new Models.DTO.User
                {
                    AccountNo = user.AccountNo,
                    Name = user.Name,
                    UserName = user.UserName,
                    Password = user.Password,
                    Address = user.Address,
                    Country = user.Country,
                    State = user.State,
                    EmailID = user.EmailID,
                    PAN = user.PAN,
                    ContactNo = user.ContactNo,
                    DOB = user.DOB,
                    AccountType = user.AccountType
                };

                return Ok(userDTO);

            }

        }
    }
