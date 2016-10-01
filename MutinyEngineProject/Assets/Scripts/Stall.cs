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

    public void GiveUpTheGoods(GameObject goods, int ammo)
    {
        if(timer <= 0)
        {
            goods = bakedGood;
            ammo = bakedGood_Ammo;
            timer = pickUpCooldown;
        }
    }
}
