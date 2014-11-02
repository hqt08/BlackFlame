using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void destroyFunc(GameObject t);

public class Tiling : MonoBehaviour {
	public GameObject tile_obj;
	TileManager tm;
	float startTime;
	public float interval = 0.5f;
	public int height = 8;
	public int width = 8;
	
	// Use this for initialization
	void Start () {
		tm = new TileManager (width, height);
		tm.addDestroyFunc (destroyTile);
		//tiles_list = new List <GameObject> ();
		drawTiles ();
		startTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad - startTime > interval) { 
			tm.updateTiles ();
			startTime = Time.timeSinceLevelLoad;
		}
	}
	
	void drawTiles() {
		foreach (Tile t in tm.tiles) {
			Vector3 position = t.getCenter();
			Debug.Log(position);
			Quaternion rotation = Quaternion.identity;
			GameObject tile = (GameObject) Instantiate(tile_obj, position, rotation);
			t.setGameObject(tile);
		}
	}

	void destroyTiles(List<GameObject> tilesToDestroy) {
		foreach (GameObject t in tilesToDestroy) {
			Destroy(t);
		}
	}

	void destroyTile(GameObject t) {
		Destroy(t);
	}
}

public class TileManager {
	int height;
	int width;
	public List<Tile> tiles;
	destroyFunc destroyTileFunc;
	
	public TileManager(int height, int width) {
		this.height = height;
		this.width = width;
		tiles = new List<Tile> ();
		createRandomTileList ();
	}

	public void addDestroyFunc(destroyFunc df) {
		destroyTileFunc = df;
	}
	
	public void createRandomTileList() {
		int size = this.height * this.width;
		
		// fill up tiles list 
		for (int i = 0; i<width; i++) {
			for (int j = 0; j<height; j++) {
				tiles.Add(new Tile(i,j));
			}
		}
		
		// mix it up randomly
		for (int i = 0; i < size; i++) {
			Tile temp = tiles[i];
			int randomIndex = Random.Range(i, tiles.Count);
			tiles[i] = tiles[randomIndex];
			tiles[randomIndex] = temp;
		}
		
	}
	
	public void updateTiles() {
		int numberToRemove = Random.Range (1, 3);
		if (tiles.Count >= numberToRemove) {
			for (int i = 0; i< numberToRemove; i++) {
				Tile t = tiles[0];
				destroyTileFunc(t.obj); //destroy gameobject
				tiles.RemoveAt (0); //remove from list
			}
		}
	}
}

public class Tile {
	Vector3 position;
	int x;
	int y;
	float h = 0.61f;
	float h2 = 1.22f;
	float h3 = 0.9f;
	float x_offset = -5f;
	float y_offset = -2.5f;
	public GameObject obj;
	
	public Tile(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public override bool Equals(object obj) {
		Tile other = (Tile)obj;
		return other.x == this.x && other.y == this.y;
	}
	
	public Vector3 getCenter() {
		Vector3 position = new Vector3 (0,0,0);
		position [0] = (y % 2 == 0) ? x * h2 + h + x_offset : x * h2 + h2 + x_offset; // x position
		position [1] = y * h3  + y_offset;  // y position
		return position;
	}

	public void setGameObject(GameObject obj) {
		this.obj = obj;
	}
}
