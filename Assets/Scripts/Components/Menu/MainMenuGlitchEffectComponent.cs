using UnityEngine;

namespace Components.Menu
{
    public class MainMenuGlitchEffectComponent : BaseMonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;

        protected override void Initialize()
        {
            base.Initialize();
            _renderer.enabled = true;
        }
    }
}

