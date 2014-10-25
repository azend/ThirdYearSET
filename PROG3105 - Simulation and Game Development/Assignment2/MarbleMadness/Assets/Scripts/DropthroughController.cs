using UnityEngine;
using System.Collections;

public class DropthroughController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			GameObject level = transform.parent.gameObject;
			
			level.SetActive(false);
			
		}
	}
	
}
