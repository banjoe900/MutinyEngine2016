using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowStallIndicator : MonoBehaviour {

    public Image img;
	
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            if(!img.enabled) img.enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (img.enabled) img.enabled = false;
        }
    }

}
