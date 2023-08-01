using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int currency;

    public Dictionary<string, bool> checkpoints;
    public string ClosestCheckpointID;

    public GameData()
    {
        this.currency = 0;
        ClosestCheckpointID = string.Empty;
        checkpoints = new Dictionary<string, bool>();
    }
}
