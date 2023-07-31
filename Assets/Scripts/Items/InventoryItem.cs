using System;

[Serializable]
public class InventoryItem
{
    public ItemData ItemData;
    public int StackSize;

    public InventoryItem(ItemData newItemData)
    {
        ItemData = newItemData;
        AddStack();
    }

    public void AddStack() => StackSize++;
    public void RemoveStack() => StackSize--;
}
