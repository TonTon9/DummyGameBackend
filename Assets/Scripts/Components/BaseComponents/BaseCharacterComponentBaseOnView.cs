﻿using Models;
using UniRx;
using UnityEngine;
using Views;

namespace Components.BaseComponent
{
    public abstract class BaseCharacterComponentBaseOnView<T> : BaseMonoBehaviour where T : IModel
    {
        [SerializeField]
        protected BaseView<T> _view;
        
        protected readonly CompositeDisposable _characterViewModelNameDisposable = new();
    }
}