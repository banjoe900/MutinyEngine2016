using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {
    public Slider player1SugarLevelText;
    public Slider player2SugarLevelText;
    public Slider player3SugarLevelText;
    public Slider player4SugarLevelText;

	public GameObject blueWinScreen;
	public GameObject orangeWinScreen;

    public GameObject blueRoundScreen;
    public GameObject orangeRoundScreen;

    public Image[] cupcakes = new Image[5];
    public Sprite orangeCupCake;
    public Sprite blueCupCake;

    // Use this for initialization
    void Start () {
        init();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void init() {
        if (blueWinScreen == null || orangeWinScreen == null) return;
		blueWinScreen.SetActive(false);
		orangeWinScreen.SetActive(false);
        blueRoundScreen.SetActive(false);
        orangeRoundScreen.SetActive(false);
    }

    /// <summary>
    /// updates the number of rounds the orange team has won in the ui
    /// </summary>
    /// <param name="newWinsNumber">the orange wins number that should be displayed in the ui</param>
    public void changeOrangeWins(int newWinsNumber) {

        for (int i = 0; i < newWinsNumber; i++)
        {
            cupcakes[i].sprite = orangeCupCake;
        }
    }

    /// <summary>
    /// updates the number of rounds the blue team has won in the ui
    /// </summary>
    /// <param name="newWinsNumber">the blue wins number that should be displayed in the ui</param>
    public void changeBlueWins(int newWinsNumber)
    {
        for (int i = 0; i < newWinsNumber; i++)
        {
            cupcakes[cupcakes.Length - 1 - i].sprite = blueCupCake;
        }
    }

    public void changePlayer1SugarLevel(float newSugarLevel) {
        player1SugarLevelText.value = newSugarLevel;
    }

    public void changePlayer2SugarLevel(float newSugarLevel) {
        player2SugarLevelText.value = newSugarLevel;
    }

    public void changePlayer3SugarLevel(float newSugarLevel) {
        player3SugarLevelText.value = newSugarLevel;
    }

    public void changePlayer4SugarLevel(float newSugarLevel) {
        player4SugarLevelText.value = newSugarLevel;
    }
}
