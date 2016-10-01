using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateAmmoText : MonoBehaviour {

	Player_Projectiles playerProjRef;
	public Text text; 

	// Use this for initialization
	void Start () {
		playerProjRef = GetComponentInParent<Player_Projectiles>();
	}
	
	// Update is called once per frame
	void Update () {
	
		text.text = playerProjRef.GetPlayerAmmo().ToString();

		if (text.text == "0")
		{
			text.text = "";
		}

	}
}
