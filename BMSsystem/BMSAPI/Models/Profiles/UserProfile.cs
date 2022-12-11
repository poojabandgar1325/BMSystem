using AutoMapper;
using BMSAPI.Models.Domains;
using BMSAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Loan, LoanDTO>().ReverseMap();
        }
    }
}
