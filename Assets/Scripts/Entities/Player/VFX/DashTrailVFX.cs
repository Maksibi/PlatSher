using UnityEngine;

public class DashTrailVFX : MonoBehaviour
{
    [SerializeField] private float colorLoosingSpeed = 1.5f;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.color = new Color(sr.color.r + Time.deltaTime, sr.color.g + Time.deltaTime, sr.color.b + Time.deltaTime, 
            sr.color.a - Time.deltaTime * colorLoosingSpeed);
    }
}
