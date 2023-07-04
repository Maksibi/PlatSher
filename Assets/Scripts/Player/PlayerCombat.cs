using System.Collections;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        IEnumerator HeavyAttackCoroutine()
        {
            anim.SetTrigger("HeavyAttack");
            yield return new WaitForSeconds(stats.rollLength);
            anim.ResetTrigger("HeavyAttack");
        }

        [SerializeField] private PlayerStats stats;

        private PlayerMovement movement;
        private Animator anim;
        private bool isInvincible = false;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0)) FastAttack();
            if (Input.GetMouseButtonDown(1)) PowerAttack();
        }

        private void PowerAttack()
        {
            if(movement.IsMoving)
            StartCoroutine(HeavyAttackCoroutine());
        }

        private void FastAttack()
        {
            throw new NotImplementedException();
        }
    }
}