using UnityEngine;
using System.Collections;

public class Stall : MonoBehaviour {

    public GameObject bakedGood;
    public int bakedGood_Ammo;
    public float pickUpCooldown;
    private float timer = 0;

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public GameObject GiveUpTheGoods(out int ammo)
    {
        ammo = 0;
        if(timer <= 0)
        {
            ammo = bakedGood_Ammo;
            timer = pickUpCooldown;
            return bakedGood;
        }
        return null;
    }
}
