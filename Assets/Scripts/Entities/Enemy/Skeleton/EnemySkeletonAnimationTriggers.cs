using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(0);

        Player player;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponentInParent<Player>();

            if (player)
            {
                PlayerStats _target = player.GetComponent<PlayerStats>();
                
                enemy.stats.DoDamage(_target);
            }
        }
    }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
}
