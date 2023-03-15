namespace Components.Common
{
    public class DontDestroyOnLoadGameobject : BaseMonoBehaviour
    {
        protected override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
        }

        protected override void UnSubscribe()
        {
        }
    }
}
