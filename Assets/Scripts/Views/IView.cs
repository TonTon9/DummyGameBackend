using Models;
using UniRx;

namespace Views
{
    public interface IView<T> where T : IModel
    {
        public ReactiveProperty<T> Model { get; set; }
    }
}
