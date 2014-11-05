using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tiling3 : MonoBehaviour {
	public GameObject tile_obj;
	TileManager tm;
	float startTime;
	public float interval = 0.5f;
	public int height = 8;
	public int width = 8;
	public float xoffset = 1;
	public float yoffset = 1;
	
	// Use this for initialization
	void Start () {
		tm = new TileManager (width, height);
		tm.addDestroyFunc (destroyTile);
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
			position.x = position.x + xoffset;
			position.y = position.y + yoffset;
			Debug.Log(position);
			Quaternion rotation = Quaternion.identity;
			GameObject tile = (GameObject) Instantiate(tile_obj, position, rotation);
			t.setGameObject(tile);
		}
	}

	public void redrawTiles() {
		tm.destroyAllTiles ();
		tm = new TileManager (width, height);
		tm.addDestroyFunc (destroyTile);
		drawTiles ();
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
