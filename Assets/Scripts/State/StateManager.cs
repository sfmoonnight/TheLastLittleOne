using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StateManager
{
    public static string savePath = Application.persistentDataPath;
    public static GameState gameState;
    public static int slot = 0;

    public static void LoadState()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(getSavePath(), FileMode.Open);
            gameState = formatter.Deserialize(stream) as GameState;
            stream.Close();
        }
        else
        {
            Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            gameState = new GameState();

        }
    }

    public static void SaveState()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(getSavePath(), FileMode.Create);
        formatter.Serialize(stream, gameState);
        stream.Close();
    }

    public static string getSavePath()
    {
        return savePath + "/save" + slot + ".tllo";
    }

    public static void SetSlot(int s)
    {
        slot = s;
    }
}
