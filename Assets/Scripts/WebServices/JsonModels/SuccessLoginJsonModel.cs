using System;

namespace JsonModels
{
    [Serializable]
    public class SuccessLoginJsonModel
    {
        public string AccessToken;
        public string ExpireToken;
        public string Login;
    }
}
