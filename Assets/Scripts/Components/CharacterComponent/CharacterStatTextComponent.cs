using Components.BaseComponent;
using Cysharp.Threading.Tasks;
using Enums;
using Models;
using UniRx;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class CharacterStatTextComponent : BaseCharacterTextComponentView
    {
        [SerializeField]
        private CharacterStatType _statType; 
        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => _characterView.IsInitialize &&
                                          _characterView.Model.Value != null);

            switch (_statType)
            {
                case CharacterStatType.Health:
                    SetAndSubscribeCharacterStat(_characterView.Model.Value.Health);
                    break;
                case CharacterStatType.Damage:
                    SetAndSubscribeCharacterStat(_characterView.Model.Value.Stamina);
                    break;
                case CharacterStatType.Stamina:
                    SetAndSubscribeCharacterStat(_characterView.Model.Value.MoveSpeed);
                    break;
            }
        }

        private void CharacterViewModelNameChanged(float newName)
        {
            Redraw(newName.ToString());
        }

        private void SetAndSubscribeCharacterStat(CharacterProperty<float> property)
        {
            property.CurrentValue.Subscribe(CharacterViewModelNameChanged).AddTo(_characterViewModelNameDisposable);
            CharacterViewModelNameChanged(property.CurrentValue.Value);
        }
    }
}
