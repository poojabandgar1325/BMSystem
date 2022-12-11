using BMSWPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BMSWPF.ViewModel.Helpers
{
    class LoginHelper
    {
        public const string BASE_URL = "https://localhost:5001/api/";
        public const string GET_URL = "Login/{0}";
        public const string POST_URL = "Login";

        public static async Task<User> GetUserDetail(string userName)
        {
            User userDetail;

            string URL = BASE_URL + string.Format(GET_URL, userName);

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string json = await response.Content.ReadAsStringAsync();
                userDetail = JsonConvert.DeserializeObject<User>(json);
            };

            return userDetail;
        }

        public static async Task<string> LoginAgent(LoginData login)
        {
            string agent;
            string URL = BASE_URL + POST_URL;

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(URL, login, default);
                var json = await response.Content.ReadAsStringAsync();
                agent = json.ToString();
            }
            return agent;
        }
    }
}
