using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public class CharacterModel : IModel
    {
        public ReactiveProperty<float> Health { get; }
        public ReactiveProperty<float> Stamina { get; }
        public ReactiveProperty<float> MoveSpeed { get; }
        
        public ReactiveProperty<bool> IsAlive { get; }

        public CharacterModel(float health, float stamina, float moveSpeed)
        {
            Health = new ReactiveProperty<float>(health);
            Stamina = new ReactiveProperty<float>(stamina);
            MoveSpeed = new ReactiveProperty<float>(moveSpeed);
            IsAlive = new ReactiveProperty<bool>(true);
        }
    }
}
