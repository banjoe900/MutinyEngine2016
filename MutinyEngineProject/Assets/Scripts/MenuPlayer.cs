using UnityEngine;
using System.Collections;

public class MenuPlayer : MonoBehaviour {

	public GameObject icon;
	public GameObject ready;

	private bool playerReady = false;

	[Range(1, 4)]
	public int playerNumber = 1;
	public string horizontalAxis;
	public string playerReadyButton;
	public float leftThumbstickAngle = 0;


	public bool isOrange;


	// Use this for initialization
	void Start () {

		horizontalAxis = string.Format("P{0} Horizontal", playerNumber);
		playerReadyButton = string.Format("P{0} Attack", playerNumber);
	}
	
	// Update is called once per frame
	void Update () {

		if (!playerReady){
			//Find angle of thumbstick
			if (Input.GetAxisRaw(horizontalAxis) == 1)
			{
				this.gameObject.transform.Translate(new Vector2(90, this.gameObject.transform.position.y));
				isOrange = false;
			}
			else if(Input.GetAxisRaw(horizontalAxis) == -1)
			{
				this.gameObject.transform.Translate(new Vector2(-90, this.gameObject.transform.position.y));
				isOrange = true;
			}

			if(Input.GetButtonDown(playerReadyButton)){

				ready.SetActive(true);
				playerReady = true;

			}
		}

	
	}
}
