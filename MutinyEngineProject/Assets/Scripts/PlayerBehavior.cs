﻿using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
    
    public float sugarLimit = 100;
    public float sugarLevel = 0;

    private PlayerMovement playerMovement;

    private RoundManager roundManager;
    private UiManager uiManager;

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }

        set
        {
            _isAlive = value;

            if (_isAlive)
            {

            }
            else
            {
                Death();
            }   
        }
    }

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
<<<<<<< Updated upstream
        roundManager = GameObject.FindGameObjectWithTag("roundManager").GetComponent<RoundManager>();
        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
=======
        //roundManager = GameObject.FindGameObjectWithTag("roundManager").GetComponent<RoundManager>();
>>>>>>> Stashed changes
    }	

    public void AddSugar(float damage)
    {
        sugarLevel += damage;
        if(sugarLevel >= sugarLimit)
        {
            sugarLevel = sugarLimit;
            IsAlive = false;
            
        }
        updateUi();
    }

    void Death()
    {
        //Destroy(this.gameObject);
<<<<<<< Updated upstream
        
=======
        //roundManager.killPlayer(playerMovement.playerNumber);
        this.enabled = false;
>>>>>>> Stashed changes
        this.transform.Rotate(new Vector3(90, 0, 0));
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
        else
        {
            playerMovement.isEnabled = false;
        }
        roundManager.killPlayer(playerMovement.playerNumber);
    }

    public void updateUi() {
        switch(GetComponent<PlayerMovement>().playerNumber) {
            case 1:
                uiManager.changePlayer1SugarLevel(sugarLevel);
                break;
            case 2:
                uiManager.changePlayer2SugarLevel(sugarLevel);
                break;
            case 3:
                uiManager.changePlayer3SugarLevel(sugarLevel);
                break;           
            case 4:
                uiManager.changePlayer4SugarLevel(sugarLevel);
                break;
        }
    }
}
