using System.Collections;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private EntityStats stats;

        private PlayerMovement movement;

        private bool isInvincible = false;

        private bool isAttacking;
        public bool IsAttacking => isAttacking;

        private int comboCounter;
        public int ComboCounter => comboCounter;


        protected void Update()
        {
            CheckInput();
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

        public void AttackOver()
        {
            isAttacking = false;
            comboCounter++;

            if (comboCounter > 2) comboCounter = 0;
        }

        public void Init(PlayerMovement movement, EntityStats stats)
        {
            this.movement = movement;
            this.stats = stats;
        }
    }
}