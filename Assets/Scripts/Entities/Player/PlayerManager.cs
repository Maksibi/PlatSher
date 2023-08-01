using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void LoadData(GameData data)
    {
        currency = data.currency;
    }

    public void SaveData(ref GameData data)
    {
        data.currency = currency;
    }
}
