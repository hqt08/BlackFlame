using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {

	private Vector3 current_Pos;

	// Use this for initialization
	void Start () {
		current_Pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) 
		{
			if (touch.phase == TouchPhase.Began) {
				
				//location.guiText.text = touch.position.x + "     " + touch.position.y;
				//print (touch.position.x);
				
				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					
					//location.guiText.text = touch.position.x + "     " + touch.position.y;
					//Destroy(hit.collider.gameObject);
					
					float distance = Mathf.Sqrt(Mathf.Pow((hit.collider.gameObject.transform.position.x - current_Pos.x), 2) + Mathf.Pow((hit.collider.gameObject.transform.position.y - current_Pos.y), 2));
					if (distance < 1.6f)
					{
						transform.position = hit.collider.gameObject.transform.position;
						current_Pos = hit.collider.gameObject.transform.position;
					}
				}
			}
		}
	}
}
