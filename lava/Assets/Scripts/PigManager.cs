using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PigManager : MonoBehaviour {

	public bool pig1 = false;

	public bool pig2 = false;

	public GameObject victory_Prompt;

//	public Text guitext;

	// Use this for initialization
	void Start () {
		victory_Prompt.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (pig1 && pig2) {
			SendMessage("Victory");
		} else if (pig2) {
//			guitext.text =  "pig2";
		} else if (pig1) {
//			guitext.text = "pig1";

		} else {
		}
	}

	void Win()
	{
//		guitext.text = "WIN";
		SendMessage ("Victory");
	}

	IEnumerator Victory ()
	{
		victory_Prompt.SetActive (true);

		yield return new WaitForSeconds(2.0f);
		
		if (GameManager.Instance) {
			Application.LoadLevel (0);
			
		} else {
			print("no gameManager");
		}
	}

}
