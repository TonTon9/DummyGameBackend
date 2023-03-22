using UniRx;

namespace Models
{
    public class CharacterModel : IModel
    {
        public CharacterProperty<string> CharacterName { get; }
        public CharacterProperty<float> Health { get; }
        public CharacterProperty<float> Stamina { get; }
        public CharacterProperty<float> MoveSpeed { get; }
        
        public CharacterProperty<bool> IsAlive { get; }

        public CharacterModel(string name, float health, float stamina, float moveSpeed)
        {
            CharacterName = new CharacterProperty<string>(name);
            
            Health = new CharacterProperty<float>(health);
            Stamina = new CharacterProperty<float>(stamina);
            MoveSpeed = new CharacterProperty<float>(moveSpeed);
            IsAlive = new CharacterProperty<bool>(true);
        }
    }

    public class CharacterProperty<T>
    {
        public ReactiveProperty<T> CurrentValue { get; }
        public ReactiveProperty<T> MaxValue { get; }

        public CharacterProperty(T initValue)
        {
            CurrentValue = new ReactiveProperty<T>(initValue);
            MaxValue = new ReactiveProperty<T>(initValue);
        }
    }
}
