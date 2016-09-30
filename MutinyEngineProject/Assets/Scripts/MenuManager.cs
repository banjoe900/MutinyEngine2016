using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject teamSelect;
	public Scene level;
	bool teamScreen = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TeamSelect(){

		mainMenu.SetActive = false;
		teamSelect.SetActive = true;


	}

	void MainMenu(){



	}


	void StartGame(){

		SceneManager.LoadScene(level);

	}



}
