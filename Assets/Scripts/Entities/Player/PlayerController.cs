using UnityEngine;

namespace Player
{
    public class PlayerController : Entity
    {
        [SerializeField] private EntityStats stats;

        private PlayerCombat combat;
        private PlayerMovement movement;
        private AnimatorController animator;

        /*
        public PlayerStateMachine stateMachine { get; private set; }
        public Idle idleState { get; private set; }
        public Moving movingState { get; private set; }
        */

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponentInChildren<AnimatorController>();
            combat = GetComponent<PlayerCombat>();
            movement = GetComponent<PlayerMovement>();

            combat.Init(movement, stats);
            movement.Init(this, stats, combat);
            animator.Init(movement, combat, this);
            /*
            stateMachine = new PlayerStateMachine();
            idleState = new Idle(this, stateMachine, "Idle");
            movingState = new Moving(this, stateMachine, "Moving");
            */
        }

        protected void Start()
        {
            //stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();

            FlipControl();

            //stateMachine.currentState.Update();
        }

        private void FlipControl()
        {
            float xMotion = Rigid.velocity.x;

            if (movement.IsWallSliding & IsLeftWallDetected)
            {
                Flip(true);
            }
            else if (movement.IsWallSliding & IsRightWallDetected)
            {
                Flip(false);
            }
            else
            {
                if (xMotion > 0.1f) Flip(true);
                else if (xMotion < -0.1f) Flip(false);
            }
        }
    }
}
