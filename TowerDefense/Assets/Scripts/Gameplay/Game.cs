using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[System.Serializable]
public class Game {

    public static Game current;
    public LifeAmountManager lifeAM;
    public int savedScore;
    public int savedTowerLife;
    public int savedMoney;
    
    //TODO: List of updated towers in the scene.
    
    

    public Game () {
        lifeAM = GameObject.FindObjectOfType<LifeAmountManager>();
        savedScore = lifeAM.calculateFinalScore();
        savedTowerLife = lifeAM.life;
        savedMoney = lifeAM.amount;
    }
}
