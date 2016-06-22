using UnityEngine;
using System;
using System.Collections;

class WorldManager : MonoBehaviour {

    public static string TAG;

    private int _lifes;
    private int _money;

    public Action<int> LifesChanged;
    public Action<int> MoneyChanged;

    public static WorldManager Instance { get; private set; }

    public int Lifes { get
        {
            return _lifes;
        }

        set
        {
            _lifes = value;

            if(LifesChanged != null)
            {
                LifesChanged(value);
            }
        }
    }
    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;

            if (MoneyChanged != null)
            {
                MoneyChanged(value);
            }
        }
    }

    public int StartLifes;
    public int StartMoney;

	// Use this for initialization
	void Start () {
        Lifes = StartLifes;
        Money = StartMoney;

	    Instance = this;
        TAG = transform.name;
	}
}
