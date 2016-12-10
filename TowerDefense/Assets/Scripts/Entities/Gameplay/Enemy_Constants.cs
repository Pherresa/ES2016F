using UnityEngine;
using System.Collections;

public class Enemy_Constants{

    // Enemy
    private int life_little = 100;
    private int life_medium = 200;
    private int life_big = 400;
    private int life_4 = 500;

    private int attack_little = 1;
    private int attack_medium = 2;
    private int attack_big = 5;
    private int attack_4 = 10;

    private int speed_little = 16;
    private int speed_medium = 15;
    private int speed_big = 12;
    private int speed_4 = 10;

    private  int money_little = 25;
    private int money_medium = 50;
    private int money_big = 75;
    private int money_4 = 100;


    // Tower
    private int t_range_little = 50;
    private int t_range_medium = 40;
    private int t_range_big = 60;
    private int t_range_4 = 60;
    private int t_range_5 = 60;

    private int t_attack_little = 10;
    private int t_attack_medium = 20;
    private int t_attack_big = 30;
    private int t_attack_4 = 150;
    private int t_attack_5 = 250;

    private int t_money_little = 80;
    private int t_money_medium = 100;
    private int t_money_big = 150;
    private int t_money_4 = 1500;
    private int t_money_5 = 2700;

    // General
    private int time = 60;
    private float time_decrease = 0.9f;
    private int wallet = 200;

    // Main Tower

    private int mt_life = 100;
    private float mt_range = 13f;
    private int mt_attack = 10;


    // Constructor
    public Enemy_Constants() { }


    // Getters

    public int Mt_attack
    {
        get
        {
            return mt_attack;
        }
    }

    public float Mt_range
    {
        get
        {
            return mt_range;
        }
    }

    public int Mt_life
    {
        get
        {
            return mt_life;
        }
    }

    public int Wallet
    {
        get
        {
            return wallet;
        }
    }

    public float Time_decrease
    {
        get
        {
            return time_decrease;
        }
    }

    public int Time
    {
        get
        {
            return time;
        }
    }

    public int T_money_big
    {
        get
        {
            return t_money_big;
        }
    }

    public int T_money_medium
    {
        get
        {
            return t_money_medium;
        }
    }

    public int T_money_little
    {
        get
        {
            return t_money_little;
        }
    }

    public int T_attack_big
    {
        get
        {
            return t_attack_big;
        }
    }

    public int T_attack_medium
    {
        get
        {
            return t_attack_medium;
        }
    }

    public int T_attack_little
    {
        get
        {
            return t_attack_little;
        }
    }

    public int T_range_big
    {
        get
        {
            return t_range_big;
        }
    }

    public int T_range_medium
    {
        get
        {
            return t_range_medium;
        }
    }

    public int T_range_little
    {
        get
        {
            return t_range_little;
        }
    }

    public int Money_big
    {
        get
        {
            return money_big;
        }
    }

    public int Money_medium
    {
        get
        {
            return money_medium;
        }
    }

    public int Money_little
    {
        get
        {
            return money_little;
        }
    }

    public int Speed_big
    {
        get
        {
            return speed_big;
        }
    }

    public int Speed_medium
    {
        get
        {
            return speed_medium;
        }
    }

    public int Speed_little
    {
        get
        {
            return speed_little;
        }
    }

    public int Attack_big
    {
        get
        {
            return attack_big;
        }
    }

    public int Attack_medium
    {
        get
        {
            return attack_medium;
        }
    }

    public int Attack_little
    {
        get
        {
            return attack_little;
        }
    }

    public int Life_big
    {
        get
        {
            return life_big;
        }
    }

    public int Life_medium
    {
        get
        {
            return life_medium;
        }
    }

    public int Life_little
    {
        get
        {
            return life_little;
        }
    }

    public int T_range_4
    {
        get
        {
            return t_range_4;
        }
    }

    public int T_range_5
    {
        get
        {
            return t_range_5;
        }
    }

    public int T_attack_4
    {
        get
        {
            return t_attack_4;
        }
    }

    public int T_attack_5
    {
        get
        {
            return t_attack_5;
        }
    }

    public int T_money_4
    {
        get
        {
            return t_money_4;
        }
    }

    public int T_money_5
    {
        get
        {
            return t_money_5;
        }
    }

    public int Attack_4
    {
        get
        {
            return attack_4;
        }
    }

    public int Speed_4
    {
        get
        {
            return speed_4;
        }
    }

    public int Money_4
    {
        get
        {
            return money_4;
        }
    }

    public int Life_4
    {
        get
        {
            return life_4;
        }
    }
}
