using Components.BaseComponent;
using Models;

namespace Views
{
    public class CharacterView : BaseView<CharacterModel>
    {
        public IInitializedComponent[] InitializedComponents { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            InitializedComponents = GetComponentsInChildren<IInitializedComponent>();
        }
    }
}
