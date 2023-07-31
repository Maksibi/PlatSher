using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Enemy enemy;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            enemy = collider.GetComponentInParent<Enemy>();

            if (enemy)
            {
                EnemyStats _target  = enemy.GetComponent<EnemyStats>();

                player.stats.DoDamage(_target);

                //enemy.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());
            }
        }
    }
}
