using System;
using System.Net.Http;
using System.Threading.Tasks;
using HTTP.WebRequest;
using UnityEngine;

public class TestNetworking : MonoBehaviour
{
    [SerializeField]
    private string _login;
    
    [SerializeField]
    private string _password;

    [ContextMenu("CreateAccount")]
    private async void CreateAccountFromInspector()
    {
        LoginUserModel loginModel = new LoginUserModel()
        {
            login = _login,
            password = _password
        };
        await CreateAccount(loginModel);
    }

    [ContextMenu("LoginAccount")]
    private async void LoginIntoAccountFromInspector()
    {
        LoginUserModel loginModel = new LoginUserModel()
        {
            login = _login,
            password = _password
        };
        await LoginAccount(loginModel);
    }
    
    [ContextMenu("HealthCheck")]
    private async void HealthCheckFromInspector()
    {
        await HealthCheck();
    }

    private async Task CreateAccount(LoginUserModel loginModel)
    {
        var jsonModel = JsonUtility.ToJson(loginModel);
        var responseObject = await WebRequestService.PostRequest("http://localhost:8080/register", jsonModel);
        
        if (responseObject.code == 200)
        {
            Debug.Log("CreateAccount Success");
        } else
        {
            var responseContent = JsonUtility.FromJson<ResponseContentModel>(responseObject.content);
            Debug.Log(responseContent.content);    
        }
    }

    private async Task HealthCheck()
    {
        var response = await WebRequestService.GetRequest("http://localhost:8080/");
        
        if (response.code == 200)
        {
            Debug.Log("HealthCheck Success");
        } else
        {
            var responseContent = JsonUtility.FromJson<ResponseContentModel>(response.content);
            Debug.Log(responseContent.content);
        }
    }

    private async Task LoginAccount(LoginUserModel loginModel)
    {
        var jsonModel = JsonUtility.ToJson(loginModel);
        var responseObject = await WebRequestService.PostRequest("http://localhost:8080/login", jsonModel);
        
        if (responseObject.code == 200)
        {
            Debug.Log("LoginAccount Success");
        } else
        {
            var responseContent = JsonUtility.FromJson<ResponseContentModel>(responseObject.content);
            Debug.Log(responseContent.content);
        }
    }

    private async Task SendGet()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync("https://anakron-apigateway.dev.fancybirds.io/api/v1/matchmaking");

            Debug.Log(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.Log(content);
            }
            else
            {
                Debug.Log("Request failed");
            }
        }
    }
}

[Serializable]
public class LoginUserModel
{
    public string login;
    public string password;
}

[Serializable]
public class ResponseObject
{
    public string content;
    public int code;
}

[Serializable]
public class ResponseContentModel
{
    public string content;
}