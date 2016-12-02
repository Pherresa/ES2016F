using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static Game savedGame;
    
    //Static so it can be called from everywhere :)
    public static void SaveData()
    {
        savedGame = Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGame);
        file.Close();
    }

    public static void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGame = (Game)bf.Deserialize(file);
            file.Close();
            SaveLoad.LoadGame();
        }
        else
        {
            Debug.Log("Error al cargar la partida");
        }
    }

    public static void LoadGame()
    {
        
    }

}