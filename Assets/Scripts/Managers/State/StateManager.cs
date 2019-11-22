﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StateManager
{
    public static string savePath = Application.persistentDataPath;
    static GameState gameState = new GameState();
    static TmpState tmpState = new TmpState();
    public static int slot = 0;
    static TeleportConfig teleportConfig = new TeleportConfig();

    public TeleportConfig GetTelePortConfig()
    {
        return teleportConfig;
    }

    public TmpState GetTmpState()
    {
        return tmpState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void Teleport(string teleporterName)
    {
        string sceneName = GetTelePortConfig().mapping[teleporterName];
        GetTmpState().loadSpot = teleporterName;

        EventManager.TriggerRemove();
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeGearNumber(int gears)
    {
        GetGameState().gears += gears;
    }

    public void LoadState()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
            gameState = formatter.Deserialize(stream) as GameState;
            stream.Close();
        }
        else
        {
            Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            gameState = new GameState();

        }
    }

    public void SaveState()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetSavePath(), FileMode.Create);
        formatter.Serialize(stream, gameState);
        stream.Close();
    }

    public string GetSavePath()
    {
        return savePath + "/save" + slot + ".tllo";
    }

    public void SetSlot(int s)
    {
        slot = s;
    }
}