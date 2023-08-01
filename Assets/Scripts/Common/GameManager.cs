using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint[] checkpoints;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.ID == pair.Key && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (data.ClosestCheckpointID == checkpoint.ID)
            {
                PlayerManager.instance.player.transform.position = checkpoint.transform.position;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.ClosestCheckpointID = FindClosestCheckpoint().ID;
        data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (data.checkpoints.ContainsKey(checkpoint.ID))
            {
                data.checkpoints[checkpoint.ID] = checkpoint.activated;
            }
            else
                data.checkpoints.Add(checkpoint.ID, checkpoint.activated);
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;

        Checkpoint closestCheck = null;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activated == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheck = checkpoint;
            }
        }

        if(closestCheck == null) 
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                float distanceToCheckpoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkpoint.transform.position);

                if (distanceToCheckpoint < closestDistance)
                {
                    closestDistance = distanceToCheckpoint;
                    closestCheck = checkpoint;
                }
            }
        }

        return closestCheck;
    }
}
