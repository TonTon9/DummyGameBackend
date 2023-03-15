using System;
using HTTP.WebRequest;
using JsonModels;
using UnityEngine;

namespace Managers
{
    public class LoginManager : BaseManager<LoginManager>
    {
        public event Action OnLoginSuccess = delegate { };
        public event Action<ResponseContentJsonModel> OnLoginFail = delegate { };

        public event Action OnRegistrationSuccess = delegate { };
        public event Action<ResponseContentJsonModel> OnRegistrationFail = delegate { };

        protected override void Initialize()
        {
            base.Initialize();
            IsInitialize = true;
        }

        public async void EnterAccount(LoginUserJsonModel loginModel)
        {
            var responseObject = await WebRequestWrapper.EnterAccountPost(loginModel);

            if (responseObject.code == 200)
            {
                var successLoginModel = JsonUtility.FromJson<SuccessLoginJsonModel>(responseObject.content);
                PlayerPrefs.SetString(WebRequestService.ACCESS_TOKEN_PLAYER_PREFS_KEY, successLoginModel.AccessToken);
                OnLoginSuccess.Invoke();

                // var character = new CharacterJsonModel()
                // {
                //     Ability = "test",
                //     Damage = 10,
                //     MaxHealth = 100,
                //     Name = "test"
                // };
                //
                //
                // var characterResponse = await WebRequestWrapper.CreateCharacterPost(character);
            } else
            {
                var responseContent = JsonUtility.FromJson<ResponseContentJsonModel>(responseObject.content);
                OnLoginFail.Invoke(responseContent);
            }
        }

        public async void CreateAccount(LoginUserJsonModel loginModel)
        {
            var responseObject = await WebRequestWrapper.CreateAccountPost(loginModel);

            if (responseObject.code == 200)
            {
                OnRegistrationSuccess.Invoke();
                EnterAccount(loginModel);
            } else
            {
                var responseContent = JsonUtility.FromJson<ResponseContentJsonModel>(responseObject.content);
                OnRegistrationFail.Invoke(responseContent);
            }
        }
    }
}
