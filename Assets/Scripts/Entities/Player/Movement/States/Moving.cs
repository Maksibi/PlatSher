using UnityEngine;

namespace Player
{
    public class Moving : IPlayerMovementState
    {
        private readonly MovementStateController controller;

        public Moving(MovementStateController controller)
        {
            this.controller = controller;
        }

        public void Act()
        {

        }

        public void Close()
        {

        }

        public void Initialize()
        {

        }

        public void Sense()
        {

        }

        public void Think()
        {

        }
    }
}
