using UnityEngine.EventSystems;

namespace Components.BaseComponent
{
    public abstract class BasePointerEnterExitComponent : BaseMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit();
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
    }
}
