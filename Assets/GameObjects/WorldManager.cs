using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

class WorldManager : MonoBehaviour {
    
    public static WorldManager Instance { get; private set; }

    public int Points { get; set; }
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

            if (value <= 0)
            {
                EndGame(false);   
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

    public Action<int> LifesChanged;
    public Action<int> MoneyChanged;

    public Tower[] AvailableTower;
    public GameObject[] AvailableTiles;

    private int _lifes;
    private int _money;

	private WorldBuilder _builder;
    private GameBoard _board;

    void Start () {
        Lifes = StartLifes;
        Money = StartMoney;

	    Instance = this;

		_builder = GetComponent<WorldBuilder> ();

		_board = new GameBoard ("001");
        RebuildBoard();
    }

    public void RebuildBoard()
    {
		_builder.BuildLevel (_board);
		_builder.BuildPath (_board);
    }

    public void EndGame(bool won)
    {
		if (won) {
			// Show Score & End Screen
		} else {
			SceneManager.LoadScene ("GameOverScreen");
		}
    }

	public bool BuildArea(Vector3 pos) {
        return (pos.x < _board.Width && pos.x >= 0 && pos.y >= 0 && pos.y < _board.Height);
	}
}
