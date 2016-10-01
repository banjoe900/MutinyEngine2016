using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoolDownBar : MonoBehaviour {

    public Stall stall;
    public Image fill;

    private float lerpCooldown;

	// Use this for initialization
	void Start () {
        lerpCooldown = stall.timer;
    }
	
	// Update is called once per frame
	void Update () {
        lerpCooldown = Mathf.Lerp(lerpCooldown, stall.timer, Time.deltaTime * 10);
        fill.fillAmount = 1 - lerpCooldown / stall.pickUpCooldown;
    }
}
