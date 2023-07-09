using UnityEngine;

namespace Player
{
    public class Idle : IPlayerMovementState
    {
        private readonly MovementStateController controller;

        public Idle(MovementStateController controller)
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