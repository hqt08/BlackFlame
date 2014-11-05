using UnityEngine;
using System.Collections;

public class SpriteController35 : MonoBehaviour {

	private Vector3 current_Pos;
	public SpriteRenderer result;
	public GameObject victory_Prompt;
	public GameObject failure_Prompt;
	public GameObject replayButton;
	public Tiling3 tiling;
	float timeLastRedraw;

	private bool hasEnded = false;
	public PigManager pigManager;

	// Use this for initialization
	void Start () {
		current_Pos = transform.position;
		victory_Prompt.SetActive (false);
		failure_Prompt.SetActive (false);
		replayButton.SetActive (false);
		timeLastRedraw = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) 
		{
			if (touch.phase == TouchPhase.Began && !hasEnded) {

				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					
					float distance = Mathf.Sqrt(Mathf.Pow((hit.collider.gameObject.transform.position.x - current_Pos.x), 2) + Mathf.Pow((hit.collider.gameObject.transform.position.y - current_Pos.y), 2));
					if (distance < 1.6f)
					{
						transform.position = hit.collider.gameObject.transform.position;
						current_Pos = hit.collider.gameObject.transform.position;
					}
				}
			}
		}

		// Lose Condition, use 3D colliders & Raycasts because 2D ones are still buggy beta
		RaycastHit[] hits;
		Vector3 pos = transform.position;
		pos.z = -5;
		hits = Physics.RaycastAll(pos, new Vector3(0,0,1), 10f);
		Debug.Log ("Hits: " + hits.Length);
		bool lava = true;
		for (int i = 0; i < hits.Length; i++) {
			Debug.Log (hits[i].collider.gameObject.tag);
			if (hits[i].collider.gameObject.tag == "Tile" || hits[i].collider.gameObject.tag == "Victory") {
				lava = false;
			}

			// Redraw all tiles if backtracked to start tile after some time
			if (hits[i].collider.gameObject.name == "BeginTile2" && Time.timeSinceLevelLoad - timeLastRedraw > 5) {
				redrawTiles();
				timeLastRedraw = Time.timeSinceLevelLoad;
			}
		}
		if (lava) {
			SendMessage("Failure");
		}

	}

	void OnTriggerEnter(Collider other) {
		// Win Condition
		if (other.gameObject.tag == "Victory")
		{
			pigManager.pig2 = true;
			//SendMessage("Victory");
		}
	}

	void redrawTiles() {
		tiling.redrawTiles ();
	}

	IEnumerator Victory ()
	{
		victory_Prompt.SetActive (true);
		
		hasEnded = true;
		yield return new WaitForSeconds(2.0f);
		
		if (GameManager.Instance) {
			Application.LoadLevel (0);

		} else {
			print("no gameManager");
		}
	}
	
	IEnumerator Failure ()
	{
		failure_Prompt.SetActive (true);
		replayButton.SetActive (true);
		hasEnded = true;
		yield return new WaitForSeconds(2.0f);
	}
	
	public void replayPressed ()
	{
		GameManager.Instance.LoadSameLevel ();
	}


}
