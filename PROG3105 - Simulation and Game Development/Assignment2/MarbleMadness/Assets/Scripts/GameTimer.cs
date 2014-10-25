using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = string.Format ("{0:F1}", Time.realtimeSinceStartup);
	}
}
