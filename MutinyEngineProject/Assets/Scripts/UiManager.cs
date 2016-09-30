using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {
    public Text roundNumbnerText;
    public Text blueWinsText;
    public Text orangeWinsText;

	// Use this for initialization
	void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void init() {

    }

    /// <summary>
    /// updates the round number in the ui
    /// </summary>
    /// <param name="newRoundNumber">the round number that should be displayed in the ui</param>
    public void changeRoundNumber(int newRoundNumber) {
        roundNumbnerText.text = newRoundNumber.ToString();
    }

    /// <summary>
    /// updates the number of rounds the blue team has won in the ui
    /// </summary>
    /// <param name="newWinsNumber">the blue wins number that should be displayed in the ui</param>
    public void changeBlueWins(int newWinsNumber) {
        blueWinsText.text = newWinsNumber.ToString();
    }

    /// <summary>
    /// updates the number of rounds the orange team has won in the ui
    /// </summary>
    /// <param name="newWinsNumber">the orange wins number that should be displayed in the ui</param>
    public void changeOrangeWins(int newWinsNumber) {
        orangeWinsText.text = newWinsNumber.ToString();
    }
}
