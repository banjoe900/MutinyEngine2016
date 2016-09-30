using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject teamSelect;
	bool teamScreen = false;

	[Range(1, 4)]
	public int playerNumber = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TeamSelect(){

		teamSelect.SetActive(true);
		mainMenu.SetActive(false);

	}

	void MainMenu(){



	}


	void StartGame(){

		SceneManager.LoadScene("bake sale");

	}
}
