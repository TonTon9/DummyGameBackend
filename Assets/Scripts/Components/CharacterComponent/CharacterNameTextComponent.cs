using Components.BaseComponent;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Components.CharacterComponents
{
    public class CharacterNameTextComponent : BaseCharacterTextComponentView
    {
        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => _characterView.IsInitialize &&
                                          _characterView.Model.Value != null);

            _characterView.Model.Value.CharacterName.CurrentValue.Subscribe(CharacterViewModelNameChanged).AddTo(_characterViewModelNameDisposable);
            CharacterViewModelNameChanged(_characterView.Model.Value.CharacterName.CurrentValue.Value);
        }

        private void CharacterViewModelNameChanged(string newName)
        {
            Redraw(newName);
        }
    }
}
