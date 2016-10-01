using UnityEngine;
using System.Collections;

public class Stall : MonoBehaviour {

    public GameObject bakedGood;
    public string goodName;
    public int bakedGood_Ammo;
    public float pickUpCooldown;
    public float timer = 0;
    public string goodsName;


    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            Mathf.Clamp(timer, 0, pickUpCooldown);
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
