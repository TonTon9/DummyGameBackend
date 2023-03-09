using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginWindow : BaseMonoBehaviour
{
    [SerializeField]
    private Button _enterAccountButton;

    [SerializeField]
    private TMP_InputField _loginInput;

    [SerializeField]
    private TMP_InputField _passwordInput;

    [SerializeField]
    private GameObject _successWindow;

    [SerializeField]
    private GameObject _loadingWindow;

    protected override void Initialize()
    {
    }

    protected override void UnInitialize()
    {
    }

    protected override async void Subscribe()
    {
        await UniTask.WaitUntil(() => LoginManager.Instance != null &&
                                      LoginManager.Instance.IsInitialize);

        _enterAccountButton.onClick.AddListener(PressEnterButtonAccount);
        LoginManager.Instance.OnLoginSuccess += LoginManager_OnLoginSuccess;
        LoginManager.Instance.OnLoginFail += LoginManager_OnLoginFail;
        LoginManager.Instance.OnRegistrationSuccess += LoginManager_OnRegistrationSuccess;
    }

    protected override void UnSubscribe()
    {
        _enterAccountButton.onClick.RemoveListener(PressEnterButtonAccount);
        if (LoginManager.Instance != null)
        {
            LoginManager.Instance.OnLoginSuccess += LoginManager_OnLoginSuccess;
            LoginManager.Instance.OnLoginFail += LoginManager_OnLoginFail;    
        }
    }
    
    private async void PressEnterButtonAccount()
    {
        await UniTask.WaitUntil(() => LoginManager.Instance != null &&
                                      LoginManager.Instance.IsInitialize);

        var loginModel = new LoginUserModel()
        {
            login = _loginInput.text,
            password = _passwordInput.text
        };

        _enterAccountButton.interactable = false;
        _loadingWindow.gameObject.SetActive(true);
        
        LoginManager.Instance.EnterAccount(loginModel);
    }

    private async void LoginManager_OnLoginSuccess()
    {
        _enterAccountButton.interactable = true;
        _loadingWindow.gameObject.SetActive(false);
        
        _successWindow.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        SceneManager.LoadScene("Menu");
    }

    private void LoginManager_OnLoginFail(ResponseContentModel responseContentModel)
    {
        _loadingWindow.gameObject.SetActive(false);
        var responseContent = JsonUtility.FromJson<ResponseContentModel>(responseContentModel.content);
        Debug.Log(responseContent);
    }

    private void LoginManager_OnRegistrationSuccess()
    {
        gameObject.SetActive(true);
    }
}