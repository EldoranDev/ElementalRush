using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;

class GameBoard
{
    public Action BoardChanged;

	private TextAsset _map;

    public int Width
    {
        get { return _data.GetLength(1); }
    }

    public int Height
    {
        get { return _data.GetLength(0); }
    }

	public Index2 SpawnPosition { get; private set; }

    private readonly Tile[,] _data;

	public GameBoard(string name)
    {
		_map = (TextAsset)Resources.Load ("Maps/" + name);

		string map = _map.text;

        Debug.Log(map);
		string[] mapLines = Regex.Split (map, "\r\n|\n");

       

		if (mapLines.Length > 0) {
			
			_data = new Tile[mapLines.Length, mapLines [0].Length];

			for (var y = 0; y < mapLines.Length; y++) {
				for (var x = 0; x < mapLines[y].Length; x++) {
					if (mapLines [y] [x].Equals ('0')) {
						_data [y, x] = Tile.GetBuildTile ();
					} else if (mapLines [y] [x].Equals ('S')) {
						_data [y, x] = Tile.GetSpawn ();
						SpawnPosition = new Index2 { X = x, Y = y };
					} else {
						_data [y, x] = Tile.GetPathTile ();
					}
				}
			}

			Debug.Log ("Build Map Data");
		} else {
			Debug.LogError ("Invalid map");
		}
    }

    public Tile GetTileInfo(float x, float y)
    {
        return GetTileInfo(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }


    public Tile GetTileInfo(int x, int y)
    {

        if (x >= 0 && x < _data.GetLength(1) && y >= 0 && y < _data.GetLength(0))
        {
            return _data[y, x];
        }

        return null;
    }
}
