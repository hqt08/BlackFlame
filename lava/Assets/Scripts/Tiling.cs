using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tiling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class TileManager {
	int height;
	int width;
	List<Tile> tiles;

	public TileManager(int height, int width) {
		this.height = height;
		this.width = width;
		tiles = new List<Tile> ();
	}
}

public class Tile {
	Vector3 position;
}
