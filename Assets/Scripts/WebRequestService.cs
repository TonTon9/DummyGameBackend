using System.Net.Http;
using System.Text;
using Cysharp.Threading.Tasks;

namespace HTTP.WebRequest
{
    public class WebRequestService
    {
        public static string BASE_URL = "http://localhost:8080";
        public static async UniTask<ResponseObject> PostRequest(string url, string requestBody)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                return new ResponseObject
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }

        public static async UniTask<ResponseObject> GetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string responseJson = await response.Content.ReadAsStringAsync();

                return new ResponseObject
                {
                    code = (int) response.StatusCode,
                    content = responseJson
                };
            }
        }
    }
}
