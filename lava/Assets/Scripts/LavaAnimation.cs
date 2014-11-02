using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LavaAnimation : MonoBehaviour {

	public Sprite[] lavas;
	int loop = 0;

	// Use this for initialization
	void Start () {
		lavas =  Resources.LoadAll<Sprite>("Lava");
		InvokeRepeating("updateBackground", 0, 1);
	}

	void updateBackground() {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sprite = lavas [loop];
		loop = (loop + 1) % lavas.Length;
	}
}
