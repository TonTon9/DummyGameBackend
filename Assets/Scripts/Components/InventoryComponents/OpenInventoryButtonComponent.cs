using Components.BaseComponent;
using UnityEngine;
using Views;

namespace Components.InventoryComponent
{
    public class OpenInventoryButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private CharacterInventoryView _inventoryView;

        protected override void Subscribe()
        {
            base.Subscribe();
            if (_inventoryView == null) return;
            _inventoryView.OnHide += InventoryViewOnHide;
        }
        
        protected override void UnSubscribe()
        {
            base.Subscribe();
            if (_inventoryView == null) return;
            _inventoryView.OnHide -= InventoryViewOnHide;
        }

        private void InventoryViewOnHide()
        {
            gameObject.SetActive(true);
        }

        protected override void Button_OnClick()
        {
            if (_inventoryView == null) return;
            _inventoryView.Show();
            gameObject.SetActive(false);
        }
    }
}
