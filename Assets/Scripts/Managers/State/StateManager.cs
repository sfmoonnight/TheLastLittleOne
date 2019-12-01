using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static string savePath;
    GameState gameState;
    static TmpState tmpState = new TmpState();
    public static int slot = 0;
    static TeleportConfig teleportConfig = new TeleportConfig();
    public GameObject character;

    public void Start()
    {
        //gameState = Toolbox.GetInstance().GetGameState();
        character = GameObject.Find("body");
        //gameState = new GameState();
        LoadState();
        //character = Toolbox.GetInstance().GetGameManager().littleOne;
    }

    public void Awake()
    {
        savePath = Application.persistentDataPath;
        character = GameObject.Find("body");
        gameState = new GameState();
    }

    public void SetUpState(GameObject body)
    {
        character = body;
        
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
            SceneManager.LoadScene(gameState.sceneName);
            //character = GameObject.Find("body");
            character = Toolbox.GetInstance().GetGameManager().littleOne;
            print("999999999999" + gameState.x + "00000" + gameState.y);
            character.transform.position = new Vector2(gameState.x, gameState.y);
            //GameManager.health = gameState.health;
            //print(gameState.sceneName);
            
        }
        else
        {
            //Debug.Log("Save file not found in " + savePath + ". This must be a new game!");
            gameState = new GameState();
            //SaveState();

        }
    }

    public void SaveState()
    {
        //character = GameObject.Find("body");
        gameState.x = character.transform.position.x;
        gameState.y = character.transform.position.y;
        //gameState.health = GameManager.health;
        gameState.sceneName = SceneManager.GetActiveScene().name;

        print("scene name: " + SceneManager.GetActiveScene().name);
        BinaryFormatter formatter = new BinaryFormatter();
        string sp = GetSavePath();
        //print("Saving to: " + sp);
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
