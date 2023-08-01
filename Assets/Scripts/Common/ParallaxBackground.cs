using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;

    private float xPos;
    private float length;

    private GameObject _camera;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPos = transform.position.x;
    }

    private void Update()
    {
        float distanceMoved = _camera.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = _camera.transform.position.x * parallaxEffect;

        transform.position = new Vector2(xPos + distanceToMove, transform.position.y);

        if(distanceMoved > xPos + length )
            xPos += length;
        else if (distanceMoved < xPos - length )
            xPos -= length;
    }
}
