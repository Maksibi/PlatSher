namespace Player
{
    public interface IPlayerMovementState
    {
        void Initialize();

        void Sense();

        void Think();

        void Act();

        void Update()
        {
            Sense();
            Think();
            Act();
        }

        void Close();
    }
}