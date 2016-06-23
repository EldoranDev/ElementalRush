using UnityEngine;
using System.Collections;

class Tile{

    public int TileType { get; private set; }

    public bool BuildSlot { get; private set; }

    public bool IsPathTile { get; private set; }

    public static Tile GetPathTile()
    {
        return new Tile
        {
            BuildSlot = false,
            IsPathTile = true,
            TileType = 1,
        };
    }

    public static Tile GetBuildTile()
    {
        return new Tile
        {
            BuildSlot = true,
            IsPathTile = false,
            TileType = 0
        };
    }

    protected Tile()
    {
        
    }
}
