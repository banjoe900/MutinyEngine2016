using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public float sugarLimit = 100;
    public float sugarLevel = 0;

    private PlayerMovement playerMovement;

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
    }	

    public void AddSugar(float damage)
    {
        sugarLevel += damage;
        if(sugarLevel >= sugarLimit)
        {
            IsAlive = false;
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
        /*if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
        else
        {
            playerMovement.isEnabled = false;
        }*/
    }
}
