using Components.BaseComponent;
using Cysharp.Threading.Tasks;
using Models;
using UniRx;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class CharacterImageComponent : BaseImageComponent<CharacterModel>, IInitializedComponent
    {
        [SerializeField]
        private string _imagePath;
        public bool IsInitialized { get; private set; } = false;
        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => _view.IsInitialize &&
                                          _view.Model.Value != null);

            _view.Model.Value.CharacterName.CurrentValue.Subscribe(CharacterViewModelNameChanged).AddTo(_characterViewModelNameDisposable);
            CharacterViewModelNameChanged(_view.Model.Value.CharacterName.CurrentValue.Value);
        }

        private void CharacterViewModelNameChanged(string newName)
        {
            var sprite = Resources.Load<Sprite>($"{_imagePath}{newName}Icon");
            Redraw(sprite);
            IsInitialized = true;
        }
    }
}
