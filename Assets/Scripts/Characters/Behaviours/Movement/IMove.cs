namespace Character.Behaviours.Movement
{
    public interface IMove
    {
        void Move(float moveX, float moveY);

        void Sit();

        void Jump();

        void Stay();
    }
}
