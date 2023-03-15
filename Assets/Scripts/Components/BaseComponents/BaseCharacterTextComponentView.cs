using UniRx;
using UnityEngine;
using Views;

namespace Components.BaseComponent
{
    public class BaseCharacterTextComponentView : BaseTextComponent
    {
        [SerializeField]
        protected CharacterView _characterView;
        
        protected readonly CompositeDisposable _characterViewModelNameDisposable = new();
    }
}
