using System;
using HTTP.WebRequest;
using UnityEngine;

public class LoginManager : BaseManager<LoginManager>
{
    public event Action OnLoginSuccess = delegate {};
    public event Action<ResponseContentModel> OnLoginFail = delegate {};
    
    public event Action OnRegistrationSuccess = delegate {};
    public event Action<ResponseContentModel> OnRegistrationFail = delegate {};

    protected override void Initialize()
    {
        base.Initialize();
        IsInitialize = true;
    }

    public async void EnterAccount(LoginUserModel loginModel)
    {
        var jsonModel = JsonUtility.ToJson(loginModel);
        var responseObject = await WebRequestService.PostRequest($"{WebRequestService.BASE_URL}/login", jsonModel);
        
        if (responseObject.code == 200)
        {
            OnLoginSuccess.Invoke();
        } else
        {
            var responseContent = JsonUtility.FromJson<ResponseContentModel>(responseObject.content);
            OnLoginFail.Invoke(responseContent);
        }
    }
    
    public async void CreateAccount(LoginUserModel loginModel)
    {
        var jsonModel = JsonUtility.ToJson(loginModel);
        var responseObject = await WebRequestService.PostRequest($"{WebRequestService.BASE_URL}/register", jsonModel);
        
        if (responseObject.code == 200)
        {
            OnRegistrationSuccess.Invoke();
            EnterAccount(loginModel);
        } else
        {
            var responseContent = JsonUtility.FromJson<ResponseContentModel>(responseObject.content);
            OnRegistrationFail.Invoke(responseContent);
        }
    }
}
