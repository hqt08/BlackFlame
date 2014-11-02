using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour {

	//private Vector3 current_Pos;
	public GameObject victory_Prompt;
	public GameObject failure_Prompt;

	// Use this for initialization
	void Start () {
		//current_Pos = transform.position;
		victory_Prompt.SetActive (false);
		failure_Prompt.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
//		foreach (Touch touch in Input.touches) 
//		{
//			if (touch.phase == TouchPhase.Began) {
//				
//				//location.guiText.text = touch.position.x + "     " + touch.position.y;
//				//print (touch.position.x);
//				
//				// Construct a ray from the current touch coordinates
//				Ray ray = Camera.main.ScreenPointToRay (touch.position);
//				RaycastHit hit;
//				if (Physics.Raycast (ray, out hit)) {
//					
//					//location.guiText.text = touch.position.x + "     " + touch.position.y;
//					//Destroy(hit.collider.gameObject);
//					
//					float distance = Mathf.Sqrt(Mathf.Pow((hit.collider.gameObject.transform.position.x - current_Pos.x), 2) + Mathf.Pow((hit.collider.gameObject.transform.position.y - current_Pos.y), 2));
//					if (distance < 1.6f)
//					{
//						transform.position = hit.collider.gameObject.transform.position;
//						current_Pos = hit.collider.gameObject.transform.position;
//					}
//				}
//			}
//		}

		// Just using mouse click for debugging for the moment
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			pos.z = transform.position.z;
			transform.position = pos;

			// Lose Condition
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
			if (hits.Length == 1 && hits[0].collider.tag == "Lava") {
				Debug.Log ("hit something");
				failure_Prompt.SetActive (true);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//Win Condition
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Victory") {
			victory_Prompt.SetActive (true);
		} 
	}
}
