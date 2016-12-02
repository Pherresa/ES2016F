using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[System.Serializable]
/*
 * Class that stores the basic game info to be store
 */
public class Game
{

    public static Game current;
    public LifeAmountManager lifeAM;
    public int savedScore;
    public int savedTowerLife;
    public int savedMoney;
    public GameObject[] defensesList;

    public Game()
    {
        lifeAM = GameObject.FindObjectOfType<LifeAmountManager>();
        savedScore = lifeAM.calculateFinalScore();
        savedTowerLife = lifeAM.life;
        savedMoney = lifeAM.amount;
        defensesList = GameObject.FindGameObjectsWithTag("Defense");
    }
}
