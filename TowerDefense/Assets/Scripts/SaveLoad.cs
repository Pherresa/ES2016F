using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
/**
 * Class to Save/Load the Game's data
 * */
public static class SaveLoad
{
    public static Game savedGame;

    
    /*
     *  Method to save the data in disk, static so it can be called from everywhere
     */
    public static void SaveData()
    {
        savedGame = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedGame);
        file.Close();
    }

    /*
     *  Method to load the data from disk, static so it can be called from everywhere
     */
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

    /*
     *  Method to update the actual data with the one saved in disk.
     */
    public static void LoadGame()
    {
        LifeAmountManager lifeAM = Object.FindObjectOfType<LifeAmountManager>();
        lifeAM.currentScore = savedGame.savedScore;
        lifeAM.life = savedGame.savedTowerLife;
        lifeAM.amount = savedGame.savedMoney;
        GameObject[] defenses = savedGame.defensesList;

        foreach (GameObject defense in defenses)
        {
            //TODO: Instatiate defenses where they belong in the map
        }
    }

}