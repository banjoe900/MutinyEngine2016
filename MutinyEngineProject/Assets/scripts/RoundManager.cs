using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {
    private int currentRound;
    private int blueWins;
    private int orangeWins;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private UiManager uiManager;

    // Use this for initialization
    void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void init() {
        currentRound = 1;
        blueWins = 0;
        orangeWins = 0;

        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
        uiManager.changeRoundNumber(currentRound);
        uiManager.changeBlueWins(blueWins);
        uiManager.changeBlueWins(orangeWins);
    }

    private void newRound() {

    }
}
