using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

class WorldBuilder : MonoBehaviour {

	public GameObject Spawn;
	public GameObject Path;
	public GameObject PathNode;

	public GameTile[] AvailableTiles;

	public int PathLength { get; private set; }

	public void BuildLevel(GameBoard board) {

		for (var x = 0; x < board.Width; x++) {
			for (var y = 0; y < board.Height; y++) {

				var tile = board.GetTileInfo (x, y);
				GameObject spawn = null;

				if (tile.Type == TileType.Path) {
					var o = new [] {
						board.GetTileInfo (x, y - 1),
						board.GetTileInfo (x + 1, y),
						board.GetTileInfo (x, y + 1),
						board.GetTileInfo (x - 1, y)
					};

					if (
						(IsPath (o [0]) && IsPath (o [2])) ||
						(IsPath (o [0]) && !IsPath (o [1]) && !IsPath (o [3])) ||
						(IsPath (o [2]) && !IsPath (o [1]) && !IsPath (o [3]))) {
						spawn = AvailableTiles.First (p => p.Name == "Path").Rotations [0];
					} else if (
						(IsPath (o [1]) && IsPath (o [3])) ||
						(IsPath (o [1]) && !IsPath (o [0]) && !IsPath (o [2])) ||
						(IsPath (o [3]) && !IsPath (o [0]) && !IsPath (o [2]))) {
						spawn = AvailableTiles.First (p => p.Name == "Path").Rotations [1];
					} else if(
						(IsPath (o [0]) && IsPath (o [1]))
					){
						spawn = AvailableTiles.First (p => p.Name == "Angle").Rotations [3];
					}else if(
						(IsPath (o [1]) && IsPath (o [2]))
					){
						spawn = AvailableTiles.First (p => p.Name == "Angle").Rotations [2];
					}else if(
						(IsPath (o [2]) && IsPath (o [3]))
					){
						spawn = AvailableTiles.First (p => p.Name == "Angle").Rotations [0];
					}else if(
						(IsPath (o [3]) && IsPath (o [0]))
					){
						spawn = AvailableTiles.First (p => p.Name == "Angle").Rotations [1];
					}
							
					PathLength++;
				} else {
					spawn = AvailableTiles.First (p => p.Name == "Gras").Rotations[0];
				}

				var t = (GameObject)Instantiate (spawn);

                t.transform.SetParent(transform);
                t.transform.localPosition = new Vector3 (x, 0, y);
				

				Spawn.transform.localPosition = new Vector3 (board.SpawnPosition.X - 0.5f, Spawn.transform.position.y, board.SpawnPosition.Y - 0.5f);
			}
		}
	}

	public void BuildPath(GameBoard board) {
		List<Index2> visited = new List<Index2> ();

		Index2 current = board.SpawnPosition;

		for (int i = 0; i < PathLength; i++) {
			var p = Instantiate (PathNode);

            p.transform.SetParent(Path.transform);
            p.transform.localPosition = new Vector3 (current.X-0.5f, Spawn.transform.position.y, current.Y-0.5f);
            
			visited.Add (current);

			var t = board.GetTileInfo (current.X, current.Y + 1);

			if (t != null && t.Type == TileType.Path && !visited.Any(x => x.X == current.X && x.Y == current.Y + 1)) {
				current = new Index2(current.X, current.Y + 1);
				visited.Add (current);
				continue;
			}

			t = board.GetTileInfo (current.X, current.Y - 1);

			if (t != null && t.Type == TileType.Path && !visited.Any(x => x.X == current.X && x.Y == current.Y - 1)) {
				current = new Index2 (current.X, current.Y - 1);
				visited.Add (current);
				continue;
			}

			t = board.GetTileInfo (current.X + 1, current.Y);

			if (t != null && t.Type == TileType.Path && !visited.Any(x => x.X == current.X + 1 && x.Y == current.Y)) {
				current = new Index2 (current.X + 1, current.Y);
				visited.Add (current);
				continue;
			}

			t = board.GetTileInfo (current.X - 1, current.Y);

			if (t != null && t.Type == TileType.Path && !visited.Any(x => x.X == current.X -1 && x.Y == current.Y)) {
				current = new Index2 (current.X - 1, current.Y);
				visited.Add (current);
				continue;
			}
		}	
	}

	bool IsPath(Tile t) {
		return (t != null && t.Type == TileType.Path);
	}
}
