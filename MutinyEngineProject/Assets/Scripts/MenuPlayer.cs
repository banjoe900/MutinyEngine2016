﻿using UnityEngine;
using System.Collections;

public class MenuPlayer : MonoBehaviour {



	public GameObject icon;
	public GameObject ready;

	public bool playerReady = false;

	[Range(1, 4)]
	public int playerNumber;
	public string horizontalAxis;
	public string playerReadyButton;
	public float leftThumbstickAngle = 0;
	int currentState = 0;

	bool moved;


	// Use this for initialization
	void Start () {

		horizontalAxis = string.Format("P{0} Horizontal", playerNumber);
		playerReadyButton = string.Format("P{0} Attack", playerNumber);
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetAxisRaw(horizontalAxis) == 0){

			moved = false;

		}

		if (!playerReady && !moved){
			//Check which way the player wants to move and see if they meet the requirements
			if (Input.GetAxisRaw(horizontalAxis) == 1 && currentState != 1)
			{
				if(currentState == -1 || MenuManager.instance.tealTeam < 2)
				{
					this.gameObject.transform.Translate(new Vector2(350, 0));

					if(transform.localPosition.x > 0){
						currentState = 1;
						MenuManager.instance.tealTeam += 1;
						Debug.Log(MenuManager.instance.orangeTeam);
						Debug.Log(MenuManager.instance.tealTeam);
					} else if (currentState == 0 && MenuManager.instance.tealTeam < 2){
						Debug.Log(MenuManager.instance.orangeTeam);
						Debug.Log(MenuManager.instance.tealTeam);
					} else {
						currentState = 0;
						MenuManager.instance.orangeTeam -= 1;
						Debug.Log(MenuManager.instance.orangeTeam);
						Debug.Log(MenuManager.instance.tealTeam);
					}
				}

				moved = !moved;

			}
			else if(Input.GetAxisRaw(horizontalAxis) == -1 && currentState != -1)
			{
				if(currentState == 1 || MenuManager.instance.orangeTeam < 2)
				{
					this.gameObject.transform.Translate(new Vector2(-350, 0));

					if(transform.localPosition.x < 0){
						currentState = -1;
						MenuManager.instance.orangeTeam += 1;
					} else if (currentState == 0 && MenuManager.instance.orangeTeam < 2){
						//don't minus stuff
					} else {
						currentState = 0;
						MenuManager.instance.tealTeam -= 1;

					moved = !moved;
					}
				}
			
			}

			if(Input.GetButtonDown(playerReadyButton))
			{
				Debug.Log(playerReadyButton);
				if(currentState != 0){
					ready = Instantiate(ready, this.transform.position, Quaternion.identity) as GameObject;
					ready.transform.SetParent(this.transform);
					playerReady = true;
				}
			}
		}
	}
}
