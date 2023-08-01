using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = itemData.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
        {
            Debug.Log("Pickuped  " + itemData.ItemName);
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
