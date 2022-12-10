using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Models.DTO
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public long PAN { get; set; }
        public long Contact { get; set; }
        public DateTime DOB { get; set; }
        public string AccountType { get; set; }


        //Navigation Properties
        // public IEnumerable<LoanDetailDTO> LoanDetails { get; set; }
    }
}
