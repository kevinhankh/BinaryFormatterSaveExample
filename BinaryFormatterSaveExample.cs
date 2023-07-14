using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int gold;
    public float xPos;
    public float yPos;
    public float zPos;
}

public class BinaryFormatterSaveExample : MonoBehaviour
{
    PlayerData playerData;
    BinaryFormatter binaryFormatter;
    string saveFilePath;
    // Start is called before the first frame update
    void Start()
    {
        playerData = new PlayerData();
        binaryFormatter = new BinaryFormatter();
        saveFilePath = Application.persistentDataPath + "/PlayerData.dat";
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.N))
            NewGame();

        if (Input.GetKeyDown(KeyCode.D))
            DeleteSaveFile();

        if (Input.GetKeyDown(KeyCode.C))
            ChangeData();
    }

    public void SaveGame()
    {
        FileStream file = File.Create(saveFilePath);
        binaryFormatter.Serialize(file, playerData);
        file.Close();
        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            playerData = (PlayerData)binaryFormatter.Deserialize(file);
            file.Close();

            Debug.Log("Load game complete! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.xPos + ", y = " + playerData.yPos + ", z = " + playerData.zPos + ")");
        }
        else
            Debug.Log("There is no save files to load!");

    }

    public void NewGame()
    {
        playerData.health = 100;
        playerData.gold = 5;
        playerData.xPos = 0;
        playerData.yPos = 0;
        playerData.zPos = 0;

        Debug.Log("New game! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.xPos + ", y = " + playerData.yPos + ", z = " + playerData.zPos + ")");
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }

    public void ChangeData()
    {
        playerData.health = 42;
        playerData.gold = 123;
        playerData.xPos = 4;
        playerData.yPos = 5;
        playerData.zPos = 6;

        Debug.Log("Data has been updated! \nPlayer health: " + playerData.health + ", Player gold: " + playerData.gold + ", Player Position: (x = " + playerData.xPos + ", y = " + playerData.yPos + ", z = " + playerData.zPos + ")");
    }
}
