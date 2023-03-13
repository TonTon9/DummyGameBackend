using Components.BaseComponent;
using UnityEngine;
using Views;

namespace Components.InventoryComponent
{
    public class CloseInventoryButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private CharacterInventoryView _inventory;
        protected override void Button_OnClick()
        {
            if (_inventory != null)
            {
                _inventory.Hide();
            }
        }
    }
}
