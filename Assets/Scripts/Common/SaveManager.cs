using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;

    private List<ISaveManager> saveManagers;
    private FileDataHandler fileDataHandler;

    private GameData gameData;

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        saveManagers = FindAllSaveManagers();

        LoadGame();
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No saves");
            NewGame();
        }

        foreach(ISaveManager manager in saveManagers)
        {
            manager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete save file")]
    public void DeleteSavedData()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        fileDataHandler.Delete();
    }

    public bool HasSavedData()
    {
        if(fileDataHandler.Load() != null)
        {
            return true;
        }
        return false;
    }
}
