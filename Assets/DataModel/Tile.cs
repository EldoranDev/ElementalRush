using UnityEngine;
using System.Collections;

enum TileType { Gras, Path }

class Tile{

	public TileType Type { get; private set; }

    public bool BuildSlot { get; private set; }

    public bool IsPathTile { get; private set; }

	public bool Spawn { get; private set; }


    public static Tile GetPathTile()
    {
        return new Tile
        {
            BuildSlot = false,
            IsPathTile = true,
			Type = TileType.Path,
        };
    }

    public static Tile GetBuildTile()
    {
        return new Tile
        {
            BuildSlot = true,
            IsPathTile = false,
			Type = TileType.Gras
        };
    }

	public static Tile GetSpawn() {
		var t = GetPathTile ();
		t.Spawn = true;
		return t;
	}

    protected Tile()
    {
        
    }
}
