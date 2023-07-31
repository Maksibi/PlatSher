using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private float cloneTimer;

    private SpriteRenderer sr;
    private Animator animator;
    private Transform closestEnemy;

    private PlayerStats playerStats;

    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.5f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        //sr.color = new Color(87, 110, 220);
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * colorLoosingSpeed);

            if (sr.color.a < 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _transform, float _cloneDuration, bool _canAttack, PlayerStats _stats)
    {
        playerStats = _stats;

        if (_canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
        cloneTimer = _cloneDuration;

        //FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -1;
    }

    private void AttackTrigger()
    {
        Enemy enemy;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            enemy = collider.GetComponentInParent<Enemy>();

            if (enemy)
            {
                EnemyStats _target = enemy.GetComponent<EnemyStats>();
                playerStats.DoDamage(_target);
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, collider.transform.position);

                if (distanceToEnemy < closestDistance)
                    closestEnemy = collider.transform;
            }
        }
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
