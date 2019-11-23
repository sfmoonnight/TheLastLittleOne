using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static string savePath;
    static GameState gameState = new GameState();
    static TmpState tmpState = new TmpState();
    public static int slot = 0;
    static TeleportConfig teleportConfig = new TeleportConfig();
    GameObject character;

    public void Start()
    {
        character = GameObject.Find("body");
        LoadState();
    }

    public void Awake()
    {
        savePath = Application.persistentDataPath;
    }

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
        print("Loading saved file");
        if (File.Exists(GetSavePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetSavePath(), FileMode.Open);
            gameState = formatter.Deserialize(stream) as GameState;
            stream.Close();

            character.transform.position = new Vector2(gameState.x, gameState.y);
        }
        else
        {
            Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            gameState = new GameState();

        }
    }

    public void SaveState()
    {

        gameState.x = character.transform.position.x;
        gameState.y = character.transform.position.y;

        BinaryFormatter formatter = new BinaryFormatter();
        string sp = GetSavePath();
        print("Saving to: " + sp);
        FileStream stream = new FileStream(sp, FileMode.Create);
        formatter.Serialize(stream, gameState);
        stream.Close();
    }

    void OnApplicationQuit()
    {
        print("Application about to quit");

        SaveState();
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
