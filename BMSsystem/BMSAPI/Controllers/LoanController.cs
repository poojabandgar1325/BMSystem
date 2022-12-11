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
    [ApiController]
    public class LoanController : Controller
    {
        private readonly ILoanRepository loanRepository;

        private readonly IMapper mapper;

        public LoanController(ILoanRepository loanRepository, IMapper mapper)
        {
            this.loanRepository = loanRepository;
            this.mapper = mapper;
        }

        // GET: api/<ApplyLoanController>
        [Route("api/[controller]/{loanId}")]
        [HttpGet]
        public async Task<LoanDTO> Get(int loanId)
        {
            Loan loanDetail = await loanRepository.GetLoanAsync(loanId);
            LoanDTO loanDetailDTO = mapper.Map<LoanDTO>(loanDetail);

            return loanDetailDTO;
        }

        // GET: api/<ApplyLoanController>
        [Route("api/[controller]/all")]
        [HttpGet]
        public async Task<List<LoanDTO>> Get()
        {
            List<Loan> loanDetails = await loanRepository.GetAllLoanAsync();
            List<LoanDTO> loanDetailDTO = mapper.Map<List<LoanDTO>>(loanDetails);

            return loanDetailDTO;
        }

        // GET: api/<ApplyLoanController>
        [Route("api/[controller]/all/{userName}")]
        [HttpGet]
        public async Task<List<LoanDTO>> Get(string userName)
        {
            List<Loan> loanDetails = await loanRepository.GetAllLoanByUsernameAsync(userName);
            List<LoanDTO> loanDetailDTO = mapper.Map<List<LoanDTO>>(loanDetails);

            return loanDetailDTO;
        }

        // POST api/<ApplyLoanController>
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Post([FromBody] Loan value)
        {
            bool response = await loanRepository.AddLoanDetailAsync(value);

            if (response)
                return Ok("Added Succesfully");

            return BadRequest("Something Went Wrong");

        }

        [HttpPut]
        [Route("api/[controller]/statusUpdate/{userName}")]

        public async Task<IActionResult> Put(int loandId, string status)
        {
            //convert DTO to Domain model
            var user = new Models.Domains.Loan()
            {
                Status = status,
            };

            //Update User using repository
            bool response = await loanRepository.UpdateStatusAsync(loandId, status);

            if (response)
                return Ok("Updated Successfully ");

            return BadRequest("Something went wrong..!");


        }
    }
}
