using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        AudioManager.instance.PlaySFX(Random.Range(31, 34));

        player.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }
}
