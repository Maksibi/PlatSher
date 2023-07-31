using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material originalMat;

    [Header("FlashFX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private float flashDuration = 0.2f;
    [Space]
    [Header("Ailments colors")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    public void IgniteFXFor(float seconds)
    {
        InvokeRepeating("IgniteColorFX", 0, 0.3f);
        Invoke("CancelColorChange", seconds);
    }

    public void ChillFXFor(float seconds)
    {
        InvokeRepeating("ChillColorFX", 0, 0.3f);
        Invoke("CancelColorChange", seconds);
    }
    
    public void ShockFXFor(float seconds)
    {
        InvokeRepeating("ShockColorFX", 0, 0.3f);
        Invoke("ShockColorFX", seconds);
    }

    private void IgniteColorFX()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }

    private void ChillColorFX()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }

    private void ShockColorFX()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
}
