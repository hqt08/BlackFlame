using UnityEngine;
using System.Collections;

public class InstructionScript : MonoBehaviour {

	public Sprite [] instructions;
	int index = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void next()
	{
		GetComponent<SpriteRenderer> ().sprite = instructions [index];
		index = (index + 1) % instructions.Length;
	}
}
