using Components.BaseComponent;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using UniRx;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class CharacterStatTextComponent : BaseTextComponent<CharacterModel>, IInitializedComponent
    {
        [SerializeField]
        private CharacterStatType _statType;

        public bool IsInitialized { get; private set; } = false;
        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => _view.IsInitialize &&
                                          _view.Model.Value != null);

            switch (_statType)
            {
                case CharacterStatType.Health:
                    SetAndSubscribeCharacterStat(_view.Model.Value.Health);
                    break;
                case CharacterStatType.Damage:
                    SetAndSubscribeCharacterStat(_view.Model.Value.Stamina);
                    break;
                case CharacterStatType.Stamina:
                    SetAndSubscribeCharacterStat(_view.Model.Value.MoveSpeed);
                    break;
            }
        }

        private void CharacterViewModelNameChanged(float newName)
        {
            Redraw(newName.ToString());
            IsInitialized = true;
        }

        private void SetAndSubscribeCharacterStat(CharacterProperty<float> property)
        {
            property.CurrentValue.Subscribe(CharacterViewModelNameChanged).AddTo(_characterViewModelNameDisposable);
            CharacterViewModelNameChanged(property.CurrentValue.Value);
        }
    }
}
