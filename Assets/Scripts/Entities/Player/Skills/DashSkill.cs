using UnityEngine;

public class DashSkill : Skill
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private float trailDuration;

    private float trailTimer;

    public override void UseSkill()
    {
        base.UseSkill();
    }

    public void CreateDashTrail(Transform transform, Sprite sprite)
    {
        GameObject newClone = Instantiate(trailPrefab, transform.position, transform.rotation);

        if (newClone.TryGetComponent (out SpriteRenderer sr))
        {
            sr.sprite = sprite;
        }


        Destroy(newClone, trailDuration);
    }
}
