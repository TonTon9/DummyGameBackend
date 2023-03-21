using System;
using Components;
using Models;
using UniRx;

namespace Views
{
    public class BaseView<T> : BaseMonoBehaviour, IView<T> where T : IModel
    {
        public ReactiveProperty<T> Model { get; set; }
        public bool IsInitialize { get; private set; }

        private IDisposable _baseViewModelChangedSubscription;

        protected override void Initialize()
        {
            Model ??= new ReactiveProperty<T>();
            IsInitialize = true;
        }
        
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            _baseViewModelChangedSubscription = Model.Subscribe(BaseView_ModelChanged);
            BaseView_ModelChanged(Model.Value);
        }

        protected override void UnSubscribe()
        {
            _baseViewModelChangedSubscription?.Dispose();
        }

        protected virtual void Redraw(T model)
        {
        }

        protected virtual void BaseView_ModelChanged(T model)
        {
            Redraw(model);
        }
    }
}
