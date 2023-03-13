using Cysharp.Threading.Tasks;
using JsonModels;
using UnityEngine;

namespace HTTP.WebRequest
{
    public static class WebRequestWrapper
    {
        public static UniTask<ResponseJsonModel> EnterAccountPost(LoginUserJsonModel loginModel)
        {
            var jsonModel = JsonUtility.ToJson(loginModel);
            return WebRequestService.PostRequest($"{WebRequestService.BASE_URL}/login", jsonModel);
        }

        public static UniTask<ResponseJsonModel> CreateAccountPost(LoginUserJsonModel loginModel)
        {
            var jsonModel = JsonUtility.ToJson(loginModel);
            return WebRequestService.PostRequest($"{WebRequestService.BASE_URL}/register", jsonModel);
        }

        public static UniTask<ResponseJsonModel> CreateCharacterPost(CharacterJsonModel characterJsonModel)
        {
            var jsonModel = JsonUtility.ToJson(characterJsonModel);
            var token = PlayerPrefs.GetString(WebRequestService.ACCESS_TOKEN_PLAYER_PREFS_KEY);
            return WebRequestService.PostRequestWithToken($"{WebRequestService.BASE_URL}/character/create", jsonModel, token);
        }

        public static UniTask<ResponseJsonModel> GetCharacters()
        {
            var token = PlayerPrefs.GetString(WebRequestService.ACCESS_TOKEN_PLAYER_PREFS_KEY);
            return WebRequestService.GetRequest($"{WebRequestService.BASE_URL}/characters", token);
        }

        public static UniTask<ResponseJsonModel> HealthCheckGet()
        {
            return WebRequestService.GetRequest($"{WebRequestService.BASE_URL}/");
        }
    }
}
