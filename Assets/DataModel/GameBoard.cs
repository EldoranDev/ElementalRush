using System;
using UnityEngine;
using System.Collections;

class GameBoard
{
    public Action BoardChanged;

    public int Width
    {
        get { return _data.GetLength(0); }
    }

    public int Height
    {
        get { return _data.GetLength(1); }
    }

    private readonly Tile[,] _data;

    public GameBoard(int height, int width)
    {
        _data = SampleBoard();
    }

    public Tile GetTileInfo(float x, float y)
    {
        return GetTileInfo(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }


    public Tile GetTileInfo(int x, int y)
    {
        if (x >= 0 && x < _data.GetLength(0) && y >= 0 && y < _data.GetLength(1))
        {
            return _data[x, y];
        }

        return null;
    }

    private Tile[,] SampleBoard()
    {
        var a = new int[][]
        {
            new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            new [] {1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0},
            new [] {0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0},
            new [] {0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0},
            new [] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            new [] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            new [] {0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0},
            new [] {0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0},
            new [] {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        Tile[,] t = new Tile[a.Length, a.Length];

        for (var x = 0; x < a.Length; x++)
        {
            for (var y = 0; y < a.Length; y++)
            {
                t[x, y] = (a[x][y] == 0) ? Tile.GetBuildTile() : Tile.GetPathTile();
            }
        }

        return t;
    }
}
