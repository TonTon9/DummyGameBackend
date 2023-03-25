using UniRx;

namespace Models
{

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
