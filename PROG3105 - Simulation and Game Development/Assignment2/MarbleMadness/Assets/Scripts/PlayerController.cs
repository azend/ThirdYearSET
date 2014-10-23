using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GUIText countText;
	public GUIText winText;
	public float speed;
	private int count;

	void Start ()
	{
		count = 0;


		UpdateCount ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rigidbody.AddForce (movement * speed * Time.deltaTime);
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
