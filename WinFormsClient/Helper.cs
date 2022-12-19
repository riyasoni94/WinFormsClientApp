using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WinFormsClient.Models;

namespace WinFormsClient
{
    public static class Helper
    {

        static HttpClient client = new HttpClient();
        /// <summary>
        /// Http client request setup
        /// </summary>
        /// <returns></returns>
        public static HttpClient getHttpClient()
        {
            client.BaseAddress = new Uri("https://localhost:7154/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        /// <summary>
        /// Set header with token
        /// </summary>
        /// <returns></returns>
        public static HttpClient getHttpClientWithToken()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + LoginInfo.Access_Token);
            return client;
        }
    }
}
