using UnityEngine;
using System.Collections;

public static class Enemy_Values_Gene{


    public static int m_little_enemy(string selector) {
        switch (selector) {
            case "l":
                return Enemy_Constants.LIFE_LITTLE;
                break;
            case "a":
                return Enemy_Constants.ATTACK_LITTLE;
                break;
            case "s":
                return Enemy_Constants.SPEED_LITTLE;
                break;
            case "m":
                return Enemy_Constants.MONEY_LITTLE;
                break;
            default:
                Debug.Log("Small enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    public static int m_medium_enemy(string selector)
    {
        switch (selector)
        {
            case "l":
                return Enemy_Constants.LIFE_MEDIUM;
                break;
            case "a":
                return Enemy_Constants.ATTACK_MEDIUM;
                break;
            case "s":
                return Enemy_Constants.SPEED_MEDIUM;
                break;
            case "m":
                return Enemy_Constants.MONEY_MEDIUM;
                break;
            default:
                Debug.Log("Medium enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    public static int m_big_enemy(string selector)
    {
        switch (selector)
        {
            case "l":
                return Enemy_Constants.LIFE_BIG;
                break;
            case "a":
                return Enemy_Constants.ATTACK_BIG;
                break;
            case "s":
                return Enemy_Constants.SPEED_BIG;
                break;
            case "m":
                return Enemy_Constants.MONEY_BIG;
                break;
            default:
                Debug.Log("Big enemy does not have this characteristic");
                break;
        }
        return 0;
    }

    public static int m_little_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return Enemy_Constants.T_ATTACK_LITTLE;
                break;
            case "r":
                return Enemy_Constants.T_RANGE_LITTLE;
                break;
            default:
                Debug.Log("Little tower does not have this characteristic");
                break;
        }
        return 0;
    }

    public static int m_medium_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return Enemy_Constants.T_ATTACK_MEDIUM;
                break;
            case "r":
                return Enemy_Constants.T_RANGE_MEDIUM;
                break;
            default:
                Debug.Log("Medium tower does not have this characteristic");
                break;
        }
        return 0;
    }



    public static int m_big_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return Enemy_Constants.ATTACK_BIG;
                break;
            case "r":
                return Enemy_Constants.T_RANGE_BIG;
                break;
            default:
                Debug.Log("Big tower does not have this characteristic");
                break;
        }
        return 0;
    }
    public static int m_mt_tower(string selector)
    {
        switch (selector)
        {
            case "a":
                return Enemy_Constants.MT_ATTACK;
                break;
            case "l":
                return Enemy_Constants.MT_LIFE;
                break;
            case "r":
                return (int)Enemy_Constants.MT_RANGE;
                break;
            default:
                Debug.Log("Main tower does not have this characteristic");
                break;
        }
        return 0;
    }
}
