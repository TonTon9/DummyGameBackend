using Cysharp.Threading.Tasks;
using JsonModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountWindow : BaseMonoBehaviour
{
    [SerializeField]
    private Button _createAccountButton;

    [SerializeField]
    private TMP_InputField _loginInput;

    [SerializeField]
    private TMP_InputField _passwordInput;

    [SerializeField]
    private TMP_InputField _repeatPasswordInput;

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

        _createAccountButton.onClick.AddListener(PressCreateAccountButton);
        LoginManager.Instance.OnRegistrationSuccess += LoginManager_OnRegistrationSuccess;
        LoginManager.Instance.OnRegistrationFail += LoginManager_OnRegistrationFail;
    }

    protected override void UnSubscribe()
    {
        _createAccountButton.onClick.RemoveListener(PressCreateAccountButton);
        if (LoginManager.Instance != null)
        {
            LoginManager.Instance.OnRegistrationSuccess -= LoginManager_OnRegistrationSuccess;
            LoginManager.Instance.OnRegistrationFail -= LoginManager_OnRegistrationFail;    
        }
    }

    private async void PressCreateAccountButton()
    {
        await UniTask.WaitUntil(() => LoginManager.Instance != null &&
                                      LoginManager.Instance.IsInitialize);

        if (_repeatPasswordInput.text != _passwordInput.text)
        {
            return;
        }
        
        var loginModel = new LoginUserJsonModel()
        {
            login = _loginInput.text,
            password = _passwordInput.text
        };

        _createAccountButton.interactable = false;
        _loadingWindow.gameObject.SetActive(true);

        LoginManager.Instance.CreateAccount(loginModel);
    }

    private void LoginManager_OnRegistrationSuccess()
    {
        _createAccountButton.interactable = true;
        _loadingWindow.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void LoginManager_OnRegistrationFail(ResponseContentJsonModel responseContentModel)
    {
        _loadingWindow.gameObject.SetActive(false);
        var responseContent = JsonUtility.FromJson<ResponseContentJsonModel>(responseContentModel.content);
        Debug.Log(responseContent);
    }
}
