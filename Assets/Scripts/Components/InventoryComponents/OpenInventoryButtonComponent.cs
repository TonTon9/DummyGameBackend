using Components.BaseComponent;
using UI.Container;
using UnityEngine;

namespace Components.InventoryComponent
{
    public class OpenInventoryButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private CharacterInventoryContainer inventoryContainer;

        protected override void Subscribe()
        {
            base.Subscribe();
            if (inventoryContainer == null) return;
            inventoryContainer.OnHide += InventoryContainerOnHide;
        }
        
        protected override void UnSubscribe()
        {
            base.Subscribe();
            if (inventoryContainer == null) return;
            inventoryContainer.OnHide -= InventoryContainerOnHide;
        }

        private void InventoryContainerOnHide()
        {
            gameObject.SetActive(true);
        }

        protected override void Button_OnClick()
        {
            if (inventoryContainer == null) return;
            inventoryContainer.Show();
            gameObject.SetActive(false);
        }
    }
}
