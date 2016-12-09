using UnityEngine;
using System.Collections;

public class Enemy_Values_Gene{

    private Enemy_Constants value_ene;


    public Enemy_Values_Gene() {
        value_ene = new Enemy_Constants();
    }


    private int m_little_enemy(string selector) {
        switch (selector) {
            case "l":
                return value_ene.Life_little;
            case "a":
                return value_ene.Attack_little;
            case "s":
                return value_ene.Speed_little;
            case "m":
                return value_ene.Money_little;
            default:
                Debug.Log("Small enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_medium_enemy(string selector)
    {
        switch (selector)
        {
            case "l":
                return value_ene.Life_medium;
            case "a":
                return value_ene.Attack_medium;
            case "s":
                return value_ene.Speed_medium;
            case "m":
                return value_ene.Money_medium;
            default:
                Debug.Log("Medium enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_big_enemy(string selector)
    {
        switch (selector)
        {
            case "l":
                return value_ene.Life_big;
            case "a":
                return value_ene.Attack_big;
            case "s":
                return value_ene.Speed_big;
            case "m":
                return value_ene.Money_medium;
            default:
                Debug.Log("Big enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_little_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.T_attack_little;
            case "r":
                return value_ene.T_range_little;
            case "m":
                return value_ene.T_money_little;
            default:
                Debug.Log("Little tower does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_medium_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.T_attack_medium;
            case "r":
                return value_ene.T_range_medium;
            case "m":
                return value_ene.T_money_medium;
            default:
                Debug.Log("Medium tower does not have this characteristic");
                break;
        }
        return 0;
    }



    private int m_big_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.T_attack_big;
            case "r":
                return value_ene.T_range_big;
            case "m":
                return value_ene.T_money_big;
            default:
                Debug.Log("Big tower does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_4_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.T_attack_4;
            case "r":
                return value_ene.T_range_4;
            case "m":
                return value_ene.T_money_4;
            default:
                Debug.Log("Lvl 4 tower does not have this characteristic");
                break;
        }
        return 0;
    }

    private int m_5_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.T_attack_5;
            case "r":
                return value_ene.T_range_5;
            case "m":
                return value_ene.T_money_5;
            default:
                Debug.Log("Big tower does not have this characteristic");
                break;
        }
        return 0;
    }

    public int m_mt_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return value_ene.Mt_attack;
            case "l":
                return value_ene.Mt_life;
            case "r":
                return (int)value_ene.Mt_range;
            default:
                Debug.Log("Main tower does not have this characteristic");
                break;
        }
        return 0;
    }
    /*
    public int obt_price(Action_Defense actionDefense)
    {
        switch (actionDefense.towerTama)
        {
            case 1:
                return m_little_tower("m");
            case 2:
                return m_medium_tower("m");
            case 3:
                return m_big_tower("m");
            default:
                Debug.Log("Error");
                break;
        }
        return -1;
    }*/

    private void tower_lvl1(ref Tower.Values val)
    {
        val.range = m_little_tower("r");
        val.strenght = m_little_tower("a");
        val.towerPrice = m_little_tower("m");
    }

    private void tower_lvl2(ref Tower.Values val)
    {
        val.range = m_medium_tower("r");
        val.strenght = m_medium_tower("a");
        val.towerPrice = m_medium_tower("m");
    }

    private void tower_lvl3(ref Tower.Values val)
    {
        val.range = m_big_tower("r");
        val.strenght = m_big_tower("a");
        val.towerPrice = m_big_tower("m");
    }

    private void tower_lvl4(ref Tower.Values val)
    {
        val.range = m_4_tower("r");
        val.strenght = m_4_tower("a");
        val.towerPrice = m_4_tower("m");
    }

    private void tower_lvl5(ref Tower.Values val)
    {
        val.range = m_5_tower("r");
        val.strenght = m_5_tower("a");
        val.towerPrice = m_5_tower("m");
    }

    public void asig_values_tower(ref Tower.Values valu) {
        switch (valu.type) {
            case Tower.TowerType.MERCENARYHUMAN_I:
                tower_lvl1(ref valu);
                break;
            case Tower.TowerType.ORCARCHER_I:
                tower_lvl3(ref valu);
                break;
            case Tower.TowerType.ROHANBARRACKS_MT:
                tower_lvl1(ref valu);
                break;
            case Tower.TowerType.TREBUCHET_MT:
                tower_lvl2(ref valu);
                break;
            case Tower.TowerType.GANDALF_MT:
                tower_lvl4(ref valu);
                break;
            case Tower.TowerType.ARAGORN_MT:
                tower_lvl3(ref valu);
                break;
            case Tower.TowerType.SARUMAN_I:
                tower_lvl5(ref valu);
                break;
            case Tower.TowerType.ORCWARRIOR:
                tower_lvl2(ref valu);
                break;
            case Tower.TowerType.LURTZ_I:
                tower_lvl4(ref valu);
                break;
            default:
                valu.range = 0;
                valu.strenght = 0;
                valu.towerPrice = 0;
                break;
        }
    }

    public int getTime() {
        return value_ene.Time;
    }

    public float getTimeDecrement()
    {
        return value_ene.Time_decrease;
    }

    public int getWallet() {
        return value_ene.Wallet;
    }

    public int getTmLife() {
        return value_ene.Mt_life;
    }
}
