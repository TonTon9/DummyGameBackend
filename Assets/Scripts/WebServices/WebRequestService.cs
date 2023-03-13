using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Cysharp.Threading.Tasks;
using JsonModels;
using UnityEngine;

namespace HTTP.WebRequest
{
    public static class WebRequestService
    {
        public static string BASE_URL = "http://localhost:8080";
        public static string ACCESS_TOKEN_PLAYER_PREFS_KEY = "AccessToken";
        
        public static async UniTask<ResponseJsonModel> PostRequest(string url, string requestBody)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                return new ResponseJsonModel
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }
        public static async UniTask<ResponseJsonModel> PostRequestWithToken(string url, string requestBody, string token)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                
                client.DefaultRequestHeaders.Add("Authorization", "Bearer  " + token);
                
                var response = await client.PostAsync(url, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                return new ResponseJsonModel
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }
        
        public static async UniTask<ResponseJsonModel> GetRequest(string url, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer  " + token);
                
                var response = await client.GetAsync(url);
                string responseJson = await response.Content.ReadAsStringAsync();

                return new ResponseJsonModel
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }
        

        public static async UniTask<ResponseJsonModel> GetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string responseJson = await response.Content.ReadAsStringAsync();

                return new ResponseJsonModel
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }
    }
}
