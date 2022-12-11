using BMSWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BMSWPF.ViewModel.Helpers
{
    class ApplyLoanHelper
    {
        public const string BASE_URL = "https://localhost:5001/api/"; 
        public const string POST_URL = "Loan";

        public static async Task<string> CreateLoan(Loan loanDetail)
        {
            string agent;
            string URL = BASE_URL + POST_URL;

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(URL, loanDetail, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }
    }
}
