using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// references
	public GUIText countText;
	public GUIText winText;

	// public
	public float speed;
	public bool gamePaused;

	// private
	private int count;
	private bool lastPauseState;
	private Vector3 pauseMovement;

	void Start ()
	{
		count = 0;
		gamePaused = false;
		lastPauseState = gamePaused;


		UpdateCount ();
	}

	void FixedUpdate ()
	{
		Debug.Log (rigidbody.angularVelocity);
		if (Input.GetKeyDown(KeyCode.Space)) {

			rigidbody.AddForce(new Vector3(0, 25, 0) * speed * Time.deltaTime);
		}

		if (!gamePaused)
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
			
			rigidbody.AddForce (movement * speed * Time.deltaTime);
		}

		lastPauseState = gamePaused;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			other.gameObject.SetActive(false);
			count ++;

			UpdateCount();
		}
	}

	void UpdateCount ()
	{
		countText.text = "Count: " + count.ToString ();

		if (count >= 8) {
				winText.text = "You've won!";
		} else {
				winText.text = string.Empty;
		}
	}
}
