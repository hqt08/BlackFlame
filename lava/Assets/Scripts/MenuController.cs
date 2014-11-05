using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextLevel ()
	{
		GameManager.Instance.levelNum = 2;
		Application.LoadLevel (2);
	}

	public void EnterInstruction ()
	{
		Application.LoadLevel (1);
	}
}
