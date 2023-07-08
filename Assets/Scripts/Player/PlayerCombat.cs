using System.Collections;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : Entity
    {
        [SerializeField] private PlayerStats stats;

        private PlayerMovement movement;

        private bool isInvincible = false;

        private bool isAttacking;
        private int comboCounter;

        protected override void Start()
        {
            base.Start();

            anim = GetComponentInChildren<Animator>();
            movement = GetComponent<PlayerMovement>();
        }

        protected override void Update()
        {
            base.Update();

            CheckInput();
            AnimatorControl();
        }

        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0)) FastAttack();
           // if (Input.GetMouseButtonDown(1)) PowerAttack();
        }

        //private void PowerAttack()
        //{
           // if(movement.IsMoving)
            //StartCoroutine(HeavyAttackCoroutine());
        //}

        private void FastAttack()
        {
            //if (!movement.IsMoving && !movement.IsRolling)
            {
                isAttacking = true;
            }
        }

        private void AnimatorControl()
        {
            anim.SetBool("isAttacking", isAttacking);
            anim.SetInteger("comboCounter", comboCounter);
        }

        public void AttackOver()
        {
            isAttacking = false;
            comboCounter++;

            if (comboCounter > 2) comboCounter = 0;
        }
    }
}