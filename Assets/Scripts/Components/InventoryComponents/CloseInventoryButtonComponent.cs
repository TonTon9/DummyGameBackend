using Components.BaseComponent;
using UI.Container;
using UnityEngine;

namespace Components.InventoryComponent
{
    public class CloseInventoryButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private CharacterInventoryContainer _inventory;
        protected override void Button_OnClick()
        {
            if (_inventory != null)
            {
                _inventory.Hide();
            }
        }
    }
}
