using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

	public Camera camera;
	public PlayerController player;
	public Texture2D pauseTexture;
	public Texture2D playTexture;

	private bool lastPauseState;

	// Use this for initialization
	void Start () {
		lastPauseState = player.gamePaused;
	}
	
	// Update is called once per frame
	void Update ()
	{
				UpdatePauseButton ();
	}

	void UpdatePauseButton ()
	{
		if (player.gamePaused != lastPauseState)
		{
			switch (player.gamePaused)
			{
			case true:
				guiTexture.texture = playTexture;
				break;
			case false:
				guiTexture.texture = pauseTexture;
				break;
			}
			
			lastPauseState = player.gamePaused;
		}
	}

	void OnMouseUp () 
	{
		player.gamePaused = !player.gamePaused;
	}
}
