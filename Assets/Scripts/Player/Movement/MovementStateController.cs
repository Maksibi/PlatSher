using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MovementStateController : MonoBehaviour
    {
        public MovementStates State
        {
            get => currentState;
            set
            {
                currentState = value;

                state.Close();
                state = states[currentState];
                state.Initialize();
            }
        }

        private IPlayerMovementState state = null;
        private MovementStates currentState;
        private Dictionary<MovementStates, IPlayerMovementState> states;

        private void Start()
        {
            states = new Dictionary<MovementStates, IPlayerMovementState>()
            {

            };

            state = states[MovementStates.Idle];
            state.Initialize();
        }

        private void Update()
        {
            state.Update();
        }
    }
}