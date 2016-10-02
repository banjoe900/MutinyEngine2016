using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    public Text uiPlayerName;
    public Slider uiPlayerSugarLevel;
    public Image uiPlayerPort;
    
    public float sugarLimit = 100;
    public float sugarLevel = 0;
    public float sugarDecay = 4;

    private PlayerMovement playerMovement;
    private CharacterAudioManager audioManager;
    public SkinnedMeshRenderer playerSkinMesh;

    private RoundManager roundManager;
    private UiManager uiManager;

    public bool canBeHit = true;

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
        
       audioManager = GetComponentInChildren<CharacterAudioManager>();
        playerMovement = GetComponent<PlayerMovement>();
        roundManager = GameObject.FindGameObjectWithTag("roundManager").GetComponent<RoundManager>();
        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
    }

    void Update()
    {
        float fattyFattyBoomBah = 1 - sugarLevel / sugarLimit / 1.5f;
        playerMovement.speed = playerMovement.initialSpeed * fattyFattyBoomBah;
        playerSkinMesh.SetBlendShapeWeight(0, sugarLevel / sugarLimit * 100);
    }

    public void AddSugar(float damage, Player_Projectiles.ProjectileWeightClass weight = Player_Projectiles.ProjectileWeightClass.Other)
    {
        sugarLevel += damage;
        sugarLevel = Mathf.Clamp(sugarLevel, 0, sugarLimit);
        
        updateUi();
        if (sugarLevel >= sugarLimit)
        {
            sugarLevel = sugarLimit;
            IsAlive = false;            
        }

        if (damage > 0 && IsAlive)
        {
            audioManager.PlayTakeDamageAudio();
        }

        switch (weight)
        {
            case Player_Projectiles.ProjectileWeightClass.Big:
                playerMovement.animator.SetTrigger("BigHit");
                break;

            case Player_Projectiles.ProjectileWeightClass.Small:
                playerMovement.animator.SetTrigger("SmallHit");
                break;
        }
    }

    void Death()
    {
        //Destroy(this.gameObject);
        //roundManager.killPlayer(playerMovement.playerNumber);
        this.transform.Rotate(new Vector3(90, 0, 0));
        audioManager.PlayDeathAudio();
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
        Start();
        if(uiPlayerName != null) uiPlayerName.text = "Player " + playerMovement.playerNumber.ToString();
        if (uiPlayerSugarLevel != null) uiPlayerSugarLevel.value = sugarLevel;
    }

    public void changePortColour(string colour) {
        switch (colour) {
            case "orange":
                uiPlayerPort.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
                break;
            case "blue":
                uiPlayerPort.GetComponent<Image>().color = Color.blue;
                break;
            case "grey":

                break;
        }
    }
}
