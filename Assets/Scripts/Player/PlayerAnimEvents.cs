using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;

    private void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        combat = GetComponentInParent<PlayerCombat>();
    }

    private void AnimationTrigger()
    {
        combat.AttackOver();
    }
}
