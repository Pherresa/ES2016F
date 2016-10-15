using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeAmountManager : MonoBehaviour
{

    public int life = 1000; // TODO: Initial life value?
    public int amount = 999; // TODO: Initial money value?

    public Text amountText;
    public Text lifeText;

    // Use this for initialization
    void Start()
    {

    }


    void UpdateLifeText()
    {

        lifeText.text = "Tower Life: " + life.ToString();

    }
    void UpdateAmountText()
    {
        amountText.text = "Amount: $" + amountText.ToString();

    }

    public void LoseAmount(int a)
    {
        if (amount > 0)
        {
            amount -= a;
        }
        UpdateAmountText();
    }

    public void LoseLife(int l = 1)
    {
        life -= l;
        if (life <= 0)
        {
            Die();
        }

        UpdateLifeText();
    }

    public void Die()
    {
        Debug.Log("Game Over");

    }


}
