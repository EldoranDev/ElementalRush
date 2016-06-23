using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

class WorldManager : MonoBehaviour {
    
    public static WorldManager Instance { get; private set; }

    public Index2 WorldSize;

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

    public GameObject[] AvailableTiles;

    private int _lifes;
    private int _money;

    private GameBoard _board;

    void Start () {
        Lifes = StartLifes;
        Money = StartMoney;

	    Instance = this;

        _board = new GameBoard(WorldSize.X, WorldSize.Y);
        _board.BoardChanged += RebuildBoard;

        //RebuildBoard();
    }

    public void RebuildBoard()
    {
        Tile t;
        GameObject g;

        for (int x = 0; x < _board.Width; x++)
        {
            for (var y = 0; y < _board.Height; y++)
            {
                t = _board.GetTileInfo(x, y);

                g = (GameObject)Instantiate(AvailableTiles[t.TileType], new Vector3(x, 0, y), Quaternion.identity);
                g.transform.SetParent(transform);
            }
        }
    }

    public void EndGame(bool won)
    {
        // End the game (GameOver screen
    }
}
